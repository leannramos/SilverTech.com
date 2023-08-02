using BankCore.Mvc.Models.Rates;
using BankCore.Mvc.Services.Rates;
using Common.Classes;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace BankCore.Mvc.Controllers
{



    [EnhanceViewEngines]
    [ControllerToolboxItem(Name = "FeaturedRates_MVC", Title = "FeaturedRates", SectionName = "Rates")]
    public class FeaturedRatesController : BaseController
    {
        #region properties
        public string tableId { get; set; }
        public Guid rowId { get; set; }
        public Guid ColumnId { get; set; }
        public string FeaturedRateTitle { get; set; }
        public string RowTitle { get; set; }
        public string RowValue { get; set; }
        public string ItemType { get; } = "Telerik.Sitefinity.DynamicTypes.Model.Rates.Table";

        public string RowItemType { get; } = "Telerik.Sitefinity.DynamicTypes.Model.Rates.Rowdata";
        public string ActionName { get; set; }
        public string ActionTitle { get; set; }
        public string SelectedPage { get; set; }
        public Guid SelectedPageId { get; set; }
        public string SelectedPageUrl { get; set; }

        #endregion

        public ActionResult Index()
        {
            //TODO: RowData as it's own service
            var selectedPage = JsonSerializer.DeserializeFromString<ExpandoObject>(SelectedPage);
            string selectedPageUrl = "";
            selectedPage.ToStringDictionary().TryGetValue("FullUrl", out selectedPageUrl);
            SelectedPageUrl = selectedPageUrl;

            var model = new FeaturedRateViewModel()
            {
                ActionName = this.ActionName,
                ActionTitle = this.ActionTitle,
                ActionURL = $"{this.SelectedPageUrl}#{tableId}",
                FeaturedRateTitle = FeaturedRateTitle
               };

       

            var table = new TableViewModel();
            Guid tableGuid = Guid.Empty;
            if (Guid.TryParse(tableId.Trim('\"'), out tableGuid))
            {
                table = new TableService().GetItem(tableGuid);
            }

            var Column = table.Columns.Where(x => x.OriginalContentId == ColumnId).FirstOrDefault();

                if (Column != null && rowId != null && rowId != Guid.Empty)
                {
                    var itemType = TypeResolutionService.ResolveType(RowItemType);
                    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager("", new Guid().ToString());
                    DynamicContent rowdataItem = dynamicModuleManager.GetDataItem(itemType, rowId);
                    var rowdataValue = rowdataItem.GetStringValue("RowData");

                    if (!string.IsNullOrEmpty(rowdataValue))
                    {
                        List<RowDataViewModel> rowdatas = new RowDataViewModel().GetList(rowdataValue);
                        var selectedRow = rowdatas.Where(x => x.ColId == Column.Id).FirstOrDefault();

                       
                        model.RowTitle = selectedRow?.Label;
                        model.RowValue = selectedRow?.Value;

                    }

      

            }


                     return View(this.TemplateName,model);
        }
    }
}
