using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Utilities.TypeConverters;
using System;
using Telerik.Sitefinity.Model;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using System.Xml.Linq;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEngines]
    [ControllerToolboxItem(Name = "RedirectWrite_MVC", Title = "RedirectWrite", SectionName = "Custom (Admin)")]
    public class RedirectWriteController : Controller
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
        public ActionResult UpdateRedirects()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type redirectType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Redirects.Redirect");
            var sfRedirects = dynamicModuleManager.GetDataItems(redirectType).Where(x => x.Status == ContentLifecycleStatus.Live).ToList();


            try
            {
                var rewriteMapFile = XElement.Load(Server.MapPath("~/rewriteMaps.config"));
                var staticRedirectSection = rewriteMapFile?.Descendants().FirstOrDefault(x => x.Attribute("name")?.Value == "StaticRedirects");

                if (staticRedirectSection == null)
                    return Content("Unable to load redirect file or StaticRedirects section");

                List<XElement> newRedirects = new List<XElement>();

                foreach (var sfRedirect in sfRedirects)
                {
                    newRedirects.Add(new XElement("add",
                      new XAttribute("key", sfRedirect.GetValue("OldUrl")),
                      new XAttribute("value", sfRedirect.GetValue("NewUrl"))));
                }
                staticRedirectSection.DescendantNodes().Remove();
                staticRedirectSection.Add(newRedirects);
                rewriteMapFile.Save(Server.MapPath("~/rewriteMaps.config"));

                //Recycle web.config now
                var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                config.AppSettings.Settings["RedirectGenerationDate"].Value = DateTime.Now.ToString();
                config.Save();

            }
            catch (Exception ex)
            {
                Log.Write(ex);
                throw (ex);

            }

            return Content($"Redirects updated");
        }

        private string templateName = "RedirectWrite";
    }
}