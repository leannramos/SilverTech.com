using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Web;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEngines]
    [ControllerToolboxItem(Name = "PageTitle_MVC", Title = "PageTitle", SectionName = "Custom")]
    public class PageTitleController : Controller
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
            var currentNode = SiteMapBase.GetActualCurrentNode();
            var pageTitle = currentNode == null ? "Placeholder Title" : currentNode.Title;
           
            return View(this.templateName,null, pageTitle);
         
        }


        private string templateName = "PageTitle";
    }
}