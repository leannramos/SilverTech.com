using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;
namespace SitefinityWebApp.Mvc.Models
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PortfolioModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ActionName { get; set; }
        public string ActionUrl { get; set; }
        public string ActionTarget { get; set; }
        public string ActionTitle { get; set; }
        public Guid LinkedPageId { get; set; }
        public string LinkedUrl { get; set; }
        public bool IsPageSelectMode { get; set; }
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string SelectedSizeUrl { get; set; }
        public string ImageTitle { get; set; }
        public string ImageAltText { get; set; }
        public string ImageDescription { get; set; }
        public Guid[] ImageIds { get; set; }
        public string[] ImageTitles { get; set; }
        public string[] ImageAltTexts { get; set; }
        public string[] ImageDescriptions { get; set; }

        public class PortfolioEntry
        {
            public Guid ImageId { get; set; }
            public string ImageTitle { get; set; }
            public string ImageUrl { get; set; }
            public string SelectedSizeUrl { get; set; }
            public string ImageDescription { get; set; }
            public string ImageAltText { get; set; }
        }

        public PortfolioViewModel GetViewModel()
        {
            SfImage image;

            var viewModel = new PortfolioViewModel()
            {
                Title = Title,
                Description = Description,
                ActionName = ActionName,
                ActionUrl = GetLinkedUrl(LinkedPageId, LinkedUrl, IsPageSelectMode),
                ActionTarget = ActionTarget,
                ActionTitle = ActionTitle
            };

            // Single Image for Portfolio.CarouselImage
            if (ImageId != Guid.Empty)
            {
                image = GetImage(ImageId);

                if (image != null)
                {
                    viewModel.ImageId = ImageId != null ? ImageId : new Guid();
                    viewModel.ImageAltText = ImageAltText ?? "";
                    viewModel.ImageDescription = ImageDescription ?? "";
                    viewModel.ImageTitle = ImageTitle ?? "";
                    viewModel.ImageUrl = image.MediaUrl;
                    viewModel.SelectedSizeUrl = GetSelectedSizeUrl(image);
                }
            }

            // Multiple Images loop for Portfolio.Default
            for (int i = 0; i < ImageIds.Count(); i++)
            {
                if (ImageIds[i] != Guid.Empty)
                {
                    image = GetImage(ImageIds[i]);

                    if (image != null)
                    {
                        viewModel.PortfolioEntries.Add(
                            new PortfolioEntry
                            {
                                ImageId = ImageIds[i] != null ? ImageIds[i] : new Guid(),
                                ImageAltText = ImageAltTexts[i] ?? "",
                                ImageDescription = ImageDescriptions[i] ??  "",
                                ImageTitle = ImageTitles[i] ?? "",
                                ImageUrl = image.MediaUrl,
                                SelectedSizeUrl = GetSelectedSizeUrl(image)
                            });
                        }
                    }
                }

            return viewModel;
        }

        /// <summary>
        /// Gets the selected size URL.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        protected virtual string GetSelectedSizeUrl(SfImage image)
        {
            if (image.Id == Guid.Empty)
                return string.Empty;

            string imageUrl;

            var urlAsAbsolute = Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
            var originalImageUrl = image.ResolveMediaUrl(urlAsAbsolute);
            imageUrl = originalImageUrl;

            return imageUrl;
        }

        protected SfImage GetImage(Guid imageId)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            return librariesManager.GetImages().Where(i => i.Id == imageId).Where(PredefinedFilters.PublishedItemsFilter<Telerik.Sitefinity.Libraries.Model.Image>()).FirstOrDefault();
        }

        /// <summary>
        /// Custom variant of GetLinkedURL that takes parameters. OOTb version has hardcoded references to internal properties.
        /// </summary>
        /// <param name="linkedPageID"></param>
        /// <param name="linkedURL"></param>
        /// <param name="pageSelectMode"></param>
        /// <returns></returns>
        protected string GetLinkedUrl(Guid linkedPageID, string linkedURL, bool pageSelectMode)
        {
            if (pageSelectMode)
            {
                if (linkedPageID == Guid.Empty)
                    return null;

                var pageManager = PageManager.GetManager();
                var node = pageManager.GetPageNodes().Where(x => x.Id == linkedPageID && !x.IsDeleted).FirstOrDefault();
                if (node != null)
                {
                    string relativeUrl;
                    if (SystemManager.CurrentContext.AppSettings.Multilingual)
                    {
                        relativeUrl = node.GetFullUrl(CultureInfo.CurrentUICulture, false);
                    }
                    else
                    {
                        relativeUrl = node.GetFullUrl();
                    }

                    return UrlPath.ResolveUrl(relativeUrl, Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls);
                }
            }
            else if (!string.IsNullOrEmpty(linkedURL))
            {
                return linkedURL;
            }

            return null;
        }
    }
}
