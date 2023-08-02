using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace BankCore.Mvc.Models.Rates
{
    public class RowDataViewModel
    {
        public RowDataViewModel() { }
        /// <summary>
        /// Deserializes a JSON interpretation of rowdata
        /// </summary>
        /// <param name="rowData"></param>
      
        public List<RowDataViewModel> GetList(string rowData)
            {
            return new JavaScriptSerializer().Deserialize<List<RowDataViewModel>>(rowData);

            }
        public string Label { get; set; }
        public Guid ColId { get; set; }
        public string Value { get; set; }
    }
}
