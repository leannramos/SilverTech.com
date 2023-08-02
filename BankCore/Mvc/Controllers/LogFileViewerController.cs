using BankCore.Mvc.Models.LogFile;
using BankCore.Mvc.Services.LogFile;
using System.Collections.Generic;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;

namespace BankCore.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Log File Viewer", Title = "Log File Viewer", SectionName = "Custom (Administration)")]
    public class LogFileViewerController : Controller
    {
        public ActionResult Index()
        {
            var model = new LogModel();
            var logService = new LogFileService();
            model.FileNames = logService.PopulateFileNames();
            model.LogFiles = new List<LogFileViewModel>();
            foreach(var file in model.FileNames)
            {
                model.LogFiles.AddRange(logService.GenerateErrorsFromErrorLog(file));
            }
            
            return View("Index", model);
        }
    }
}
