using BankCore.Mvc.Models.Rates;
using Common.Classes;
using System;
using System.Linq;
using System.Web.Script.Serialization;
using Telerik.Sitefinity;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace BankCore.Mvc.Services.Rates
{
    public class TableService : DynamicContentService<TableViewModel>
    {
        public TableService(string type = "Telerik.Sitefinity.DynamicTypes.Model.Rates.Table", string providerName = "", bool AutoInitialize = true) : base(type, providerName, AutoInitialize)
        {
        }

        protected override TableViewModel BuildModel(DynamicContent item)
        {
            var tvm = new TableViewModel();
            tvm.Id = item.Id;

            /*
             * An ID is not selected directly because retrieving related columns in this fashion preserves ordering as set in the UI. 
             * The simplistic for loop implementation causes this to persist throughout the pipeline to the view.
             */
            var relatedColumns = item.GetRelatedItems("Columns");

            var columnService = new ColumnService();
            var allColumns = columnService.GetItems();
            
            foreach (var column in relatedColumns)
            {
                var columnToAdd = allColumns.Where(x => x.OriginalContentId == column.Id).FirstOrDefault();
                if (columnToAdd != null)
                    tvm.Columns.Add(columnToAdd);
            }

            //TODO: Extrapolate out when time permits
            Type rowdataType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Rates.Rowdata");

            IQueryable<DynamicContent> rowdataAllItems = _manager.GetDataItems(rowdataType)
                .Where(row => row.SystemParentId == item.Id)
                .Live();

            var serializer = new JavaScriptSerializer();
            foreach (var rowDataItem in rowdataAllItems)
            {
                tvm.Rows.Add(new RowDataViewModel().GetList(rowDataItem.GetStringValue("RowData")));
            }
            
            
            return tvm;
        }
    }
}
