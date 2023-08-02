using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Helpers.ViewModels;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Mvc.StringResources;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Controllers;

namespace SitefinityWebApp.Mvc.Controllers
{
    public class WorkPagerController : ContentPagerController
    {
        /// <summary>Returns view with pager.</summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="totalPagesCount">The total pages count.</param>
        /// <param name="redirectUrlTemplate">The template of the URL used for redirecting.</param>
        /// <returns></returns>
        [OutputCache(Duration = 1)]
        public PartialViewResult Work(
          int currentPage,
          int totalPagesCount,
          string redirectUrlTemplate)
        {
            PagerViewModel model = new PagerViewModel(currentPage, totalPagesCount, redirectUrlTemplate);
            int num1 = 1;
            if (model.CurrentPage > model.DisplayCount)
            {
                if (model.CurrentPage <= 0)
                    model.CurrentPage = 1;
                num1 = (int)Math.Floor((double)(model.CurrentPage - 1) / (double)model.DisplayCount) * model.DisplayCount + 1;
            }
            int num2 = Math.Min(model.TotalPagesCount, num1 + model.DisplayCount - 1);
            model.PreviousNode = num1 <= model.DisplayCount ? (Pager.PagerNumericItem)null : new Pager.PagerNumericItem(Math.Max(num1 - 1, 1));
            for (int index = num1; index <= num2; ++index)
            {
                string text = string.Format(model.RedirectUrlTemplate, (object)index);
                Pager.PagerNumericItem pagerNumericItem = new Pager.PagerNumericItem(index, text);
                model.PagerNodes.Add(pagerNumericItem);
            }
            model.NextNode = num2 >= model.TotalPagesCount ? (Pager.PagerNumericItem)null : new Pager.PagerNumericItem(num2 + 1);
            this.TryStorePaginationUrls(model);
            return this.PartialView("Pager", (object)model);
        }


        internal static object GetPaginationUrls(string nextUrl, string previousUrl)
        {
            Type type = Type.GetType("Telerik.Sitefinity.ContentLocations.PaginationUrls, Telerik.Sitefinity");
            object instance = Activator.CreateInstance(type, true);
            type.GetProperty("PreviousUrl").SetValue(instance, (object)previousUrl);
            type.GetProperty("NextUrl").SetValue(instance, (object)nextUrl);
            return instance;
        }
        internal static MethodInfo GetTryStorePaginationUrlsMethod()
        {
            return Type.GetType("Telerik.Sitefinity.ContentLocations.CanonicalUrlPageExtensions, Telerik.Sitefinity").GetMethod("TryStorePaginationUrls", BindingFlags.Static | BindingFlags.NonPublic);
        }

        private void TryStorePaginationUrls(PagerViewModel model)
        {
            int index = model.CurrentPage % model.DisplayCount;
            WorkPagerController.GetTryStorePaginationUrlsMethod().Invoke((object)null, new object[2]
            {
        (object) this.HttpContext.Handler.GetPageHandler(),
        WorkPagerController.GetPaginationUrls(model.CurrentPage <= 0 || model.CurrentPage >= model.PagerNodes.Count ? (string) null : WorkPagerController.PageNodeUrl(model.PagerNodes[index], model.RedirectUrlTemplate), index <= 1 ? (string) null : WorkPagerController.PageNodeUrl(model.PagerNodes[index - 2], model.RedirectUrlTemplate))
            });
        }
        private static string PageNodeUrl(Pager.PagerNumericItem node, string template)
        {
            if (node == null)
                return (string)null;
            return RouteHelper.ResolveUrl(string.Format(template, (object)node.PageNumber), UrlResolveOptions.Absolute);
        }
    }
}