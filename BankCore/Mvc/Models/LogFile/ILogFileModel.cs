using System;

namespace BankCore.Mvc.Models.LogFile
{
    public enum CategoryType
    {
        ErrorLog,
        Custom
    }

    public enum SeverityType
    {
        Information,
        Warning,
        Error,
    }
    public interface ILogFileModel
    {
        string ParentFileName { get; set; }
        DateTime Timestamp { get; set; }
        CategoryType Category { get; set; }
        string Message { get; set; }
        SeverityType Severity { get; set; }
    }

    public static class CategoryExtensions
    {
        public static string PrintString(this CategoryType cat)
        {
            switch(cat)
            {
                case CategoryType.Custom:
                    return "Custom";
                case CategoryType.ErrorLog:
                    return "Error Log";
            }

            return string.Empty;
        }
    }
    public static class SeverityTypeExtensions
    {
        public static string PrintString(this SeverityType ex)
        {
            switch (ex)
            {
                case SeverityType.Information:
                    return "Information";
                case SeverityType.Warning:
                    return "Warning";
                case SeverityType.Error:
                    return "Error";
            }

            return string.Empty;
        }
    }
}
