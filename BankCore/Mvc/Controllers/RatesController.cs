using BankCore.Mvc.Models.Rates;
using BankCore.Mvc.Services.Rates;
using Common.Classes;
using System;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace BankCore.Mvc.Controllers
{



    [EnhanceViewEngines]
    [ControllerToolboxItem(Name = "Rates_MVC", Title = "Rates", SectionName = "Rates")]
    public class RatesController : BaseController
    {
        #region properties
        public string tableId { get; set; }
        public string ItemType { get; } = "Telerik.Sitefinity.DynamicTypes.Model.Rates.Table";
        public string SectionTitle { get; set; }

        #endregion

        public ActionResult Index()
        {
            var model = new TableViewModel();
            Guid tableGuid = Guid.Empty;
            if (Guid.TryParse(tableId.Trim('\"'), out tableGuid))
            { 
                model = new TableService().GetItem(tableGuid);
                model.SectionTitle = this.SectionTitle;
            }

            return View(this.TemplateName,model);
        }
    }
}
