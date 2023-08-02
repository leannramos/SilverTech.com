using Telerik.Sitefinity.Services;
using SitefinityWebApp.Sitefinity.Public.Admin.Config;

namespace SitefinityWebApp.Sitefinity.Public.Admin
{
    public class SiteSettingsHelper
    {
        public static CustomSiteSettingsContract GetConfig()
        {
            return SystemManager.CurrentContext.GetSetting<CustomSiteSettingsContract, CustomSiteSettingsContract>();
        }
    }
}