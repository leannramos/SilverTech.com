using SitefinityWebApp.Mvc.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    ///     This class is used as a model for the image controller.
    /// </summary>
    public class CustomImageModel : ImageModel
    {
        #region Constructor
        public CustomImageModel()
            : base()
        {
            SvgMarkup = this.SvgMarkup;
        }
        #endregion

        #region Public Properties
        public string SvgMarkup { get; set; }
        #endregion

        #region Public Methods
        new public CustomImageViewModel GetViewModel()
        {
            var viewModel = new CustomImageViewModel()
            {
                AlternativeText = this.AlternativeText,
                Title = this.Title,
                DisplayMode = this.DisplayMode,
                ThumbnailName = this.ThumbnailName,
                ThumbnailUrl = this.ThumbnailUrl,
                CustomSize = this.CustomSize != null ? new JavaScriptSerializer().Deserialize<CustomSizeModel>(this.CustomSize) : null,
                UseAsLink = this.UseAsLink,
                CssClass = this.CssClass,
            };

            SfImage image;
            if (this.Id != Guid.Empty)
            {
                image = this.GetImage();
                if (image != null)
                {
                    viewModel.SelectedSizeUrl = this.GetSelectedSizeUrl(image);
                    viewModel.LinkedContentUrl = GetLinkedUrl(image);

                    try
                    {
                        var markupObject = Telerik.Sitefinity.Model.DataExtensions.GetValue(image, "SvgMarkup");

                        if (markupObject != null)
                        {
                            viewModel.SvgMarkup = markupObject.ToString();
                        }
                    }
                    catch { }
                }
            }
            else
            {
                image = new SfImage();
            }

            viewModel.Item = new ItemViewModel(image);

            return viewModel;
        }
        #endregion

        #region Private members
        private string GetLinkedUrl(SfImage image)
        {
            string linkedUrl = null;

            if (this.UseAsLink && this.LinkedPageId != Guid.Empty)
            {
                var pageManager = PageManager.GetManager();
                var node = pageManager.GetPageNode(this.LinkedPageId);
                if (node != null)
                {
                    var provider = SiteMapBase.GetCurrentProvider();
                    var siteMapNode = provider.FindSiteMapNodeFromKey(node.Id.ToString());
                    linkedUrl = UrlPath.ResolveUrl(siteMapNode.Url, true);
                }
            }
            else if (this.UseAsLink && this.LinkedPageId == Guid.Empty)
            {
                linkedUrl = image.ResolveMediaUrl(true);
            }

            return linkedUrl;
        }

        private CultureInfo GetCurrentCulture()
        {
            if (SystemManager.CurrentContext.AppSettings.Multilingual)
            {
                var currentCulture = CultureInfo.CurrentUICulture;
                if (SystemManager.CurrentContext.AppSettings.DefinedFrontendLanguages.Contains(currentCulture))
                    return currentCulture;
            }

            return SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
        }
        #endregion
    }
}