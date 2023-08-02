using System;
using System.Collections.Generic;

namespace BankCore.Mvc.Models.Rates
{
    public class TableViewModel
    {
        public string SectionTitle { get; set; }
        public Guid Id { get; set; }
        public List<ColumnViewModel> Columns { get; set; }
        public List<List<RowDataViewModel>> Rows { get; set; }

        public TableViewModel()
        {
            Columns = new List<ColumnViewModel>();
            Rows = new List<List<RowDataViewModel>>();
        }
    }
}
