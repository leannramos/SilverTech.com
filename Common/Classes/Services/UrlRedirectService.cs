using Common.Classes.Enums;
using Common.Classes.ViewModels;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;

namespace Common.Classes.Services
{
    public class UrlRedirectService : DynamicContentService<UrlRedirectModel>
    {
        public UrlRedirectService(bool AutoInitialize = true) : base("Telerik.Sitefinity.DynamicTypes.Model.Redirects.Redirect", "")
        {
            var multisiteContext = SystemManager.CurrentContext as MultisiteContext;
            ///get the current site by name
            if (multisiteContext != null)
            {
                var currentSiteName = multisiteContext?.CurrentSite?.Name ?? "";

                //get the module's provider by the module's name
                ProviderName = multisiteContext.CurrentSite.GetProviders("Redirects").Select(p => p.ProviderName).FirstOrDefault();
            }
        }


        protected override UrlRedirectModel BuildModel(DynamicContent item)
        {
            var result = new UrlRedirectModel();

            if (item != null)
            {
                result.OriginalUrl = item.GetStringValue("OldUrl");
                result.TargetUrl = item.GetStringValue("NewUrl");
                result.RedirectType = item.GetEnum("RedirectType", RedirectType.Permanent);
            }

            return result;
        }
    }
}