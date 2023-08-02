using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Utilities.TypeConverters;
using System;
using Telerik.Sitefinity.Model;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using System.Net;
using Telerik.Sitefinity.Security.Claims;
using System.Web;
using CsvHelper;
using SitefinityWebApp.Mvc.Models;
using System.IO;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Mvc.ActionFilters;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEngines]
    [ControllerToolboxItem(Name = "RedirectUpload_MVC", Title = "RedirectUpload", SectionName = "Custom (Admin)")]
    public class RedirectUploadController : Controller
    {

        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
        /// </summary>
        /// <value></value>
        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }
        public ActionResult Index()
        {


            return View(this.templateName);
         
        }

        [HttpPost]
        [StandaloneResponseFilter]
        public ActionResult UploadRedirects(HttpPostedFileBase file)
        {
            var user = ClaimsManager.GetCurrentIdentity();

            if (!user.IsAuthenticated)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Content("Not Authenticated");
            }

            if (file == null || file.ContentLength == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Content("Missing or empty file");

            }


            var csv = new CsvReader(new StreamReader(file.InputStream));
            var incomingRecords = csv.GetRecords<RedirectUploadModel>().ToList();

            //TODO: Abstract all of these out. This was a last minute 'really nice to have' addition and only validated for functionality

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type redirectType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Redirects.Redirect");
            var sfRedirects = dynamicModuleManager.GetDataItems(redirectType).Where(x => x.Status == ContentLifecycleStatus.Live).ToList();

            foreach (var newRedirect in incomingRecords)
            {
                var testUrl = newRedirect.OldUrl.TrimEnd('/');

                var existingRedirect = sfRedirects.Where(x => x.GetString("OldUrl").TrimEnd('/') == testUrl).FirstOrDefault();

                if (existingRedirect != null)
                {
                    existingRedirect.SetValue("OldUrl", newRedirect.OldUrl);
                    existingRedirect.SetValue("NewUrl", newRedirect.NewUrl);
                    dynamicModuleManager.SaveChanges();

                }
                else
                {
                    DynamicContent redirectItem = dynamicModuleManager.CreateDataItem(redirectType);

                    redirectItem.SetValue("OldUrl", newRedirect.OldUrl);
                    redirectItem.SetValue("NewUrl", newRedirect.NewUrl);

                    dynamicModuleManager.Lifecycle.Publish(redirectItem);
                    redirectItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
                    dynamicModuleManager.SaveChanges();

                }
               
            }

            return Content("");
        }

        private string templateName = "RedirectUpload";
    }
}