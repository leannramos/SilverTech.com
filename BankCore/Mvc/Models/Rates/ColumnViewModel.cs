using System;

namespace BankCore.Mvc.Models.Rates
{
    public class ColumnViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public Guid OriginalContentId { get; set; }
       
    }
}
