using System.Collections.Generic;

namespace BankCore.Mvc.Models.LogFile
{
    public class LogModel
    {
        public List<LogFileViewModel> LogFiles { get; set; }
        public List<string> FileNames { get; set; }
    }
}
