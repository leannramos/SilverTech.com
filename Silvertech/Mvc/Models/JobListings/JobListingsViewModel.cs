using System.Collections.Generic;
using System.Web;

namespace SitefinityWebApp.Mvc.Models
{
    public class JobListingsViewModel
    {
        public string Heading { get; set; }
        public string Heading2 { get; set; }
        public string Description { get; set; }
        public List<HtmlString> Listings { get; set; }
        public List<HtmlString> Listings2 { get; set; }
    }
}