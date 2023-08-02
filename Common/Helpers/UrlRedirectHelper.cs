using Common.Classes.Services;
using Common.Classes.ViewModels;
using Telerik.Sitefinity.Model;

namespace Common.Classes.Helpers
{
    public class UrlRedirectHelper
    {

        public UrlRedirectModel GetRedirect(string currentDirectory)
        {
            var redirectService = new UrlRedirectService(false);
            redirectService.Initialize();

            var redirect = redirectService.GetItem(x => x.GetValue<string>("OldUrl") == currentDirectory.ToString());

            return redirect;
        }
    }
}
