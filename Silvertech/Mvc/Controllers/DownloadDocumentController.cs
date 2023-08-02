using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEngines]
    [ControllerToolboxItem(Name = "DownloadDocument_MVC", Title = "Download Documents", SectionName = "Documents")]
    public class DownloadDocumentController : DocumentController
    {
        public DownloadDocumentController()
            : base()
        {

        }
    }
}