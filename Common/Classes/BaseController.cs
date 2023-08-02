using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;

namespace Common.Classes
{
    public class BaseController : Controller
    {
        #region Properties

        private string templateName;
        private string _templates;
        private string FirstTemplate { get; set; }

        public string TemplateName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(templateName))
                {
                    templateName = FirstTemplate;
                }
                return templateName;
            }
            set
            {
                templateName = value;
            }
        }
        public string Templates
        {
            get
            {
                if (_templates == null)
                {
                    var jsonSerialiser = new JavaScriptSerializer();
                    var temps = this.GetViews();

                    FirstTemplate = temps.Where(x=>!x.Contains("DesignerView")).FirstOrDefault() ?? "";
                    _templates = jsonSerialiser.Serialize(temps.ToList());
                }
                return _templates;
            }
            set { _templates = value; }
        }
        #endregion

        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
            return;
        }

    }
}
