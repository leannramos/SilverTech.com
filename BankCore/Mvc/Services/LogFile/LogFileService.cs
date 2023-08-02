using BankCore.Mvc.Models.LogFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BankCore.Mvc.Services.LogFile
{
    public class LogFileService
    {
        public string Path = HttpContext.Current.Server.MapPath("~/App_Data/Sitefinity/Logs/");
        public const string ErrorLogFileName = "Error.log";
        public const string Seperator = "----------------------------------------";
        public List<string> FileNames = new List<string>();

        public List<LogFileViewModel> GenerateErrorsFromErrorLog(string fileName = ErrorLogFileName)
        {
            var result = new List<LogFileViewModel>();

            if (!File.Exists(Path + fileName))
            {
                return result;
            }

            var inAnError = false;
            var doingStackTrace = false;
            var errorEntry = new LogFileViewModel();
            errorEntry.ParentFileName = fileName;

            using (FileStream fs = new FileStream(Path + fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (sr.Peek() >= 0)
                    {
                        var line = sr.ReadLine();

                        if (line == Seperator)
                        {
                            if (inAnError)
                            {
                                inAnError = false;
                                result.Add(errorEntry);
                                errorEntry = new LogFileViewModel();
                                errorEntry.ParentFileName = fileName;
                            }
                            else
                            {
                                inAnError = true;
                            }
                        }

                        if (!inAnError)
                        {
                            continue;
                        }

                        if (line.Contains("Stack Trace :    ") && !doingStackTrace)
                        {
                            doingStackTrace = true;
                            errorEntry.StackTrace += GetValue(line, "Stack Trace :    ");
                        }

                        if (!line.Contains("Stack Trace :    ") && doingStackTrace)
                        {
                            if (line != string.Empty)
                            {
                                errorEntry.StackTrace += "..." + GetValue(line, "   ");
                            }
                            else
                            {
                                doingStackTrace = false;
                            }
                        }

                        if (!doingStackTrace)
                        {
                            FillInModel(errorEntry, line);
                        }
                    }
                }
            }

            return result;
        }

        public ILogFileModel GenerateError(CategoryType c, SeverityType s, string m)
        {
            var error = new LogFileViewModel
            {
                Timestamp = DateTime.Now,
                Category = c,
                Severity = s,
                Message = m
            };

            return error;
        }

        private string GetValue(string line, string lineIdentifier, int length = 0)
        {
            if (length == 0)
            {
                return line.Substring(lineIdentifier.Length, line.Length - lineIdentifier.Length);
            }
            else
            {
                return line.Substring(lineIdentifier.Length, length);
            }
        }

        private void FillInModel(LogFileViewModel model, string line)
        {
            if (line.Contains("Timestamp: "))
            {
                var timeStamp = new DateTime();
                DateTime.TryParse(GetValue(line, "Timestamp: "), out timeStamp);

                if (timeStamp != null)
                {
                    model.Timestamp = timeStamp;
                }
                return;
            }
            if (line.Contains("Message : "))
            {
                model.Message = GetValue(line, "Message : ");
                return;
            }
            if (line.Contains("Requested URL : "))
            {
                model.RequestedUrl = GetValue(line, "Requested URL : ");
                return;
            }
            if (line.Contains("An exception of type '"))
            {
                var lineIdentifier = "An exception of type '";
                var index = line.IndexOf("'", lineIdentifier.Length);
                model.Exception = GetValue(line, lineIdentifier, index - lineIdentifier.Length);
                return;
            }
            if (line.Contains("Severity: "))
            {
                var value = GetValue(line, "Severity: ");
                if (value == "Warning")
                {
                    model.Severity = SeverityType.Warning;
                }
                else if (value == "Information")
                {
                    model.Severity = SeverityType.Information;
                }
                else if (value == "Error")
                {
                    model.Severity = SeverityType.Error;
                }
                return;
            }
        }

        public List<string> PopulateFileNames()
        {
            FileNames.Add(ErrorLogFileName);
            var filepaths = Directory.GetFiles(Path).Where(w => w.Contains("Error") && w != "Error");
            foreach(var file in filepaths)
            {
                FileNames.Add(System.IO.Path.GetFileName(file));
            }

            return FileNames;
        }
    }
}
