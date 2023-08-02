using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;

namespace SitefinityWebApp.Mvc.Models
{
    public class CustomImageViewModel : ImageViewModel
    {
        public CustomImageViewModel()
          : base()
        {
            SvgMarkup = this.SvgMarkup;
        }

        public string SvgMarkup { get; set; }
    }
}