using Common.Classes.Enums;
using System;
using System.Web;
using Common.Classes.Helpers;

namespace SitefinityWebApp
{
    public class UrlRedirectModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(Applicatition_BeginRequest);
        }

        private void Applicatition_BeginRequest(Object source, EventArgs e)
        {
            var urlRedirectHelper = new UrlRedirectHelper();

            var currentDirectory = HttpContext.Current.Request.Url.PathAndQuery;
            var redirect = urlRedirectHelper.GetRedirect(currentDirectory);

            if (redirect != null && !string.IsNullOrWhiteSpace(redirect.TargetUrl))
            {
                HandleRedirect(redirect.TargetUrl, RedirectType.Permanent, HttpContext.Current);
            }
        }

        private void HandleRedirect(string redirectUrl, RedirectType redirectType, HttpContext context)
        {
            if (redirectType == RedirectType.Permanent)
            {
                context.Response.RedirectPermanent(redirectUrl);
            }
            else if (redirectType == RedirectType.Temporary)
            {
                context.Response.Redirect(redirectUrl);
            }
        }

    }
}