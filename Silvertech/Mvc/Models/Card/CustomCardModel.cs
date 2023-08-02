using Common.CustomConfig;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Card.Mvc.Models.Card;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;
namespace SitefinityWebApp.Mvc.Models
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CustomCardModel : CardModel
    {
        public string ActionTitle { get; set; }
        public string ActionTarget { get; set; }

        public Guid LinkedPageIdTwo { get; set; }
        public string LinkedUrlTwo { get; set; }
        public bool IsPageSelectModeTwo { get; set; }
        public string ActionTwoName { get; set; }
        public string ActionTwoTitle { get; set; }
        public string ActionTwoTarget { get; set; }
        public string ActionTwoUrl { get; set; }
        public bool IsActionSpeedbump { get; set; }
        public string ActionSpeedbumpMessage { get; set; }
        public bool IsActionTwoSpeedbump { get; set; }
        public string ActionTwoSpeedbumpMessage { get; set; }

        public Guid LinkedPageIdThree { get; set; }
        public string LinkedUrlThree { get; set; }
        public bool IsPageSelectModeThree { get; set; }
        public string ActionThreeName { get; set; }
        public string ActionThreeTitle { get; set; }
        public string ActionThreeTarget { get; set; }
        public string ActionThreeUrl { get; set; }
        public bool IsActionThreeSpeedbump { get; set; }
        public string ActionThreeSpeedbumpMessage { get; set; }

        public Guid ImageTwoId { get; set; }
        public string ImageTwoAlternativeText { get; set; }
        public string ImageTwoTitle { get; set; }
        public string ImageTwoUrl { get; set; }
        public string ClientSpotlightHeading { get; set; }
        public string SvgMarkup { get; set; }

        //Project specific properties below
        public string HeadingTwo { get; set; }

        public CustomCardModel()
            : base()
        {
            this.ActionTitle = ActionTitle;
            this.ActionTarget = ActionTarget;
            this.HeadingTwo = HeadingTwo;
            this.ActionTwoName = ActionTwoName;
            this.ActionTwoTitle = ActionTwoTitle;
            this.ActionTwoTarget = ActionTwoTarget;
            this.ActionTwoUrl = ActionTwoUrl;
            this.LinkedPageIdTwo = LinkedPageIdTwo;
            this.LinkedUrlTwo = LinkedUrlTwo;
            this.IsPageSelectModeTwo = this.IsPageSelectModeTwo;
            this.IsActionTwoSpeedbump = IsActionTwoSpeedbump;
            this.ActionThreeName = ActionThreeName;
            this.ActionThreeTitle = ActionThreeTitle;
            this.ActionThreeTarget = ActionThreeTarget;
            this.ActionThreeUrl = ActionThreeUrl;
            this.LinkedPageIdThree = LinkedPageIdThree;
            this.LinkedUrlThree = LinkedUrlThree;
            this.IsPageSelectModeThree = this.IsPageSelectModeThree;
            this.IsActionThreeSpeedbump = IsActionThreeSpeedbump;
            this.IsActionSpeedbump = IsActionSpeedbump;
            this.ActionSpeedbumpMessage = GetSpeedbumpText(this.ActionSpeedbumpMessage);
            this.ActionTwoSpeedbumpMessage = GetSpeedbumpText(this.ActionTwoSpeedbumpMessage);
            this.ClientSpotlightHeading = ClientSpotlightHeading;
            this.SvgMarkup = SvgMarkup;
        }

        public override CardViewModel GetViewModel()
        {
            var viewModel = new CustomCardViewModel()
            {
                Heading = this.Heading,
                Description = this.Description,
                ActionName = this.ActionName,
                ActionUrl = this.GetLinkedUrl(this.LinkedPageId, this.LinkedUrl, this.IsPageSelectMode),
                CssClass = this.CssClass,
                ActionTitle = this.ActionTitle,
                ActionTarget = this.ActionTarget,
                HeadingTwo = this.HeadingTwo,
                ActionTwoName = this.ActionTwoName,
                ActionTwoTitle = this.ActionTwoTitle,
                ActionTwoTarget = this.ActionTwoTarget,
                ActionTwoUrl = this.GetLinkedUrl(this.LinkedPageIdTwo, this.LinkedUrlTwo, this.IsPageSelectModeTwo),
                IsActionSpeedbump = this.IsActionSpeedbump,
                ActionSpeedbumpMessage = this.IsActionSpeedbump ? this.ActionSpeedbumpMessage : "",
                IsActionTwoSpeedbump = this.IsActionTwoSpeedbump,
                ActionTwoSpeedbumpMessage = this.IsActionTwoSpeedbump ? this.ActionTwoSpeedbumpMessage : "",
                ClientSpotlightHeading = this.ClientSpotlightHeading,
                ActionThreeName = this.ActionThreeName,
                ActionThreeTitle = this.ActionThreeTitle,
                ActionThreeTarget = this.ActionThreeTarget,
                ActionThreeUrl = this.GetLinkedUrl(this.LinkedPageIdThree, this.LinkedUrlThree, this.IsPageSelectModeThree),
                IsPageSelectModeThree = this.IsPageSelectModeThree,
                IsActionThreeSpeedbump = this.IsActionThreeSpeedbump,
                ActionThreeSpeedbumpMessage = this.IsActionThreeSpeedbump ? this.ActionThreeSpeedbumpMessage : "",
            };


            SfImage image;
            if (this.ImageId != Guid.Empty)
            {
                image = this.GetImage(ImageId);
                if (image != null)
                {
                    viewModel.SelectedSizeUrl = this.GetSelectedSizeUrl(image);
                    viewModel.ImageAlternativeText = image.AlternativeText;
                    viewModel.ImageTitle = image.Title;

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
            if (this.ImageTwoId != Guid.Empty)
            {
                image = this.GetImage(ImageTwoId);
                if (image != null)
                {
                    viewModel.ImageTwoUrl = this.GetSelectedSizeUrl(image);
                    viewModel.ImageTwoAlternativeText = image.AlternativeText;
                    viewModel.ImageTwoTitle = image.Title;
                }
            }

            return viewModel;
        }

        public string GetSpeedbumpText(string localValue)
        {
            if (localValue == null)
            {            
                var globalConfig = Config.Get<GlobalConfig>();
                localValue = globalConfig.CTASpeedbumpText;
            }

            return localValue;
        }
        /// <summary>
        /// Custom variant of GetLinkedURL that takes parameters. OOTb version has hardcoded references to internal properties.
        /// </summary>
        /// <param name="linkedPageID"></param>
        /// <param name="linkedURL"></param>
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

        protected SfImage GetImage(Guid imageId)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager(this.ImageProviderName);
            return librariesManager.GetImages().Where(i => i.Id == imageId).Where(PredefinedFilters.PublishedItemsFilter<Telerik.Sitefinity.Libraries.Model.Image>()).FirstOrDefault();
        }
    }
}
