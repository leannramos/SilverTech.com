using System;

namespace BankCore.Mvc.Models.LogFile
{
    public class LogFileViewModel : ILogFileModel
    {
        public string ParentFileName { get; set; }
        public DateTime Timestamp { get; set; }
        public CategoryType Category { get; set; }
        public SeverityType Severity { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string RequestedUrl { get; set; }
        public string Exception { get; set; }
    }
}
