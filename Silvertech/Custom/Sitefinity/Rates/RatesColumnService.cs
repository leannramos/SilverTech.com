using BankCore.Mvc.Models.Rates;
using BankCore.Mvc.Services.Rates;
using SitefinityWebApp.Sitefinity.Services.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Mvc;
namespace SitefinityWebApp.Custom.Sitefinity.Rates
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class RatesColumnService : Controller, IRatesColumnService
    {
        public List<RowDataViewModel> GetColumns(TableRequestModel tableRequest)
        {
            var result = new JsonResult();
            var splitUrl = tableRequest.CurrentUrlPath.Split(new[] { "tables/" }, StringSplitOptions.RemoveEmptyEntries);
            if (splitUrl.Length <= 1)
            {
                return tableRequest.RowData;
            }
                var secondSplit = splitUrl[1].Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (secondSplit.Length <= 1)
            {

                return tableRequest.RowData;
            }

            var tableId = secondSplit[0];
            Guid tableGuid;
            if(Guid.TryParse(tableId, out tableGuid))
            {
                var tableService = new TableService();
                var table = tableService.GetItem(tableGuid);
                var colsChecked = new List<ColumnViewModel>();
                var rowdataToRemove = new List<RowDataViewModel>();

                if (tableRequest.RowData == null)
                {
                    tableRequest.RowData = new List<RowDataViewModel>();
                    foreach (var col in table.Columns)
                    {
                        tableRequest.RowData.Add(new RowDataViewModel() { ColId = col.Id, Label = col.Title, Value = "" });
                    }
                    //result.Data = new JavaScriptSerializer().Serialize(tableRequest.RowData);
                    return tableRequest.RowData; 
                }

                foreach(var rowdata in tableRequest.RowData)
                {
                    var col = table.Columns.FirstOrDefault(x => x.Id == rowdata.ColId);
                    if(col != null)
                    {
                        rowdata.Label = col.Title;
                        colsChecked.Add(col);
                    }
                    else
                    {
                        rowdataToRemove.Add(rowdata);
                    }
                }

                foreach(var rowdata in rowdataToRemove)
                {
                    tableRequest.RowData.Remove(rowdata);
                }

                foreach(var col in table.Columns.Where(x => !colsChecked.Contains(x)))
                {
                    tableRequest.RowData.Add(new RowDataViewModel() { Label = col.Title, ColId = col.Id });
                }


                return tableRequest.RowData;
                //                return result;
            }

            return tableRequest.RowData;
        }
    }
}
    
