using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;

namespace SitefinityWebApp.Mvc.Models.Document
{
    public class CustomDownloadViewModel : DocumentViewModel
    {
        public CustomDownloadViewModel()
            : base()
        {

        }

        public string LinkTitle { get; set; }
        public string LinkSubTitle { get; set; }
        public string LinkText { get; set; }
    }
}