using System.Collections.Generic;

namespace BankCore.Mvc.Models.Rates
{
    public class TableRequestModel
    {
        public string CurrentUrlPath { get; set; }
        public List<RowDataViewModel> RowData { get; set; }
        public TableRequestModel()
        {
            RowData = new List<RowDataViewModel>();
        }
    }
}
