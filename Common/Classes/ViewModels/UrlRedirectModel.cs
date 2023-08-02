using Common.Classes.Enums;

namespace Common.Classes.ViewModels
{
    public class UrlRedirectModel
    {
        public string SiteID { get; set; }
        public string OriginalUrl { get; set; }
        public string TargetUrl { get; set; }
        public RedirectType RedirectType { get; set; }
    }
}
