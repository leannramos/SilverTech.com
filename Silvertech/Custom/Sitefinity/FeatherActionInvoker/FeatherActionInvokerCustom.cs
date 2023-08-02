using System.ComponentModel;
using System.Web;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Routing;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Pages;
using Common.Helpers;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using SitefinityWebApp.Sitefinity.Public.Admin.Config;
using Telerik.Sitefinity.Libraries.Model;

namespace SitefinityWebApp
{
    public class FeatherActionInvokerCustom : FeatherActionInvoker
    {
        protected override void RestoreHttpContext(string output, HttpContext initialContext)
        {
            this.PopulateResponseStatus(System.Web.HttpContext.Current, initialContext);
            base.RestoreHttpContext(output, initialContext);
        }

        private void PopulateResponseStatus(HttpContext httpContext, HttpContext initialContext)
        {
            if (!SystemManager.IsDesignMode && httpContext.Response.StatusCode != 200)
            {
                initialContext.Response.Status = httpContext.Response.Status;
                initialContext.Response.StatusCode = httpContext.Response.StatusCode;
                initialContext.Response.StatusDescription = httpContext.Response.StatusDescription;
            }
        }

        protected override void SetPageProperties(MvcProxyBase proxyControl)
        {
            var page = proxyControl.Page;
            if (page == null)
                return;

            var controller = proxyControl.GetController();
            var viewBag = controller.ViewBag;
            var initialPageTitle = page.Title;
            var metadata = (MetadataModel)viewBag.Metadata;
            CustomMetadataModel customMetadata = new CustomMetadataModel();
            var pageHelper = new CustomPageHelpers();

            //if (!string.IsNullOrEmpty(viewBag.Title) && (metadata == null || metadata.SEOEnabledPerWidget || !metadata.SEOEnabled))
            //{
            //    page.Header.Title = viewBag.Title;
            //    proxyControl.PageTitle = viewBag.Title;
            //}

            if (metadata != null)
            {
                var properties = TypeDescriptor.GetProperties(metadata)
               .OfType<PropertyDescriptor>()
               .Where(p => !ExcludedMetaProperties.Contains(p.Name));

                this.SetPageSeoProperties(proxyControl, metadata, properties, initialPageTitle);
                this.SetPageOpenGraphProperties(proxyControl, metadata, properties, initialPageTitle);

                // The below logic retrieves content items' custom field data
                if (controller.ModelState.FirstOrDefault().Value == null) {
                    return;
                }

                var contentItem = controller.ModelState.FirstOrDefault().Value.Value.RawValue;

                if (contentItem as BlogPost != null)
                {
                    var item = contentItem as BlogPost;
                    var blogsManager = BlogsManager.GetManager();
                    BlogPost dynamicItem = blogsManager.GetBlogPost(item.Id);

                    // The following iterates through all properties defined in the CustomMetadataModel's dictionary and appropriately sets variables based on the dictionary.
                    // This is easily extensible by adding new entries into the dictionary.
                    customMetadata = new CustomMetadataModel();

                    foreach (var metaField in CustomMetadataModel.Settings)
                    {
                        var property = customMetadata.GetType().GetProperty(metaField.Key);
                        if (property != null)
                        {
                            try
                            {
                                var value = DataExtensions.GetValue(dynamicItem, metaField.Key).ToString();
                                property.SetValue(customMetadata, value == "" ? null : value);
                            }
                            catch
                            {
                                property.SetValue(customMetadata, null);
                            }
                        }
                    }
                    var imageField = dynamicItem.GetRelatedItems<Image>("TwitterImage").FirstOrDefault();
                    customMetadata.TwitterImage = imageField != null ? imageField.MediaUrl : null;

                    if (!customMetadata.IsEmpty())
                    {
                        this.SetPageExtendedMetadataProperties(proxyControl, customMetadata, true);
                    }
                    return;
                }

                if (contentItem as NewsItem != null)
                {
                    var item = contentItem as NewsItem;
                    var newsManager = NewsManager.GetManager();
                    NewsItem dynamicItem = newsManager.GetNewsItem(item.Id);

                    // The following iterates through all properties defined in the CustomMetadataModel's dictionary and appropriately sets variables based on the dictionary.
                    // This is easily extensible by adding new entries into the dictionary.
                    customMetadata = new CustomMetadataModel();

                    foreach (var metaField in CustomMetadataModel.Settings)
                    {
                        var property = customMetadata.GetType().GetProperty(metaField.Key);
                        if (property != null)
                        {
                            try
                            {
                                var value = DataExtensions.GetValue(dynamicItem, metaField.Key).ToString();
                                property.SetValue(customMetadata, value == "" ? null : value);
                            }
                            catch
                            {
                                property.SetValue(customMetadata, null);
                            }
                        }
                    }
                    var imageField = dynamicItem.GetRelatedItems<Image>("TwitterImage").FirstOrDefault();
                    customMetadata.TwitterImage = imageField != null ? imageField.MediaUrl : null;

                    if (!customMetadata.IsEmpty())
                    {
                        this.SetPageExtendedMetadataProperties(proxyControl, customMetadata, true);
                    }
                    return;
                }

                if (contentItem as DynamicContent != null)
                {
                    DynamicContent dynamicItem = GetDynamicContentItem(contentItem);

                    // The following iterates through all properties defined in the CustomMetadataModel's dictionary and appropriately sets variables based on the dictionary.
                    // This is easily extensible by adding new entries into the dictionary.
                    customMetadata = new CustomMetadataModel();

                    foreach (var metaField in CustomMetadataModel.Settings)
                    {
                        var property = customMetadata.GetType().GetProperty(metaField.Key);
                        if (property != null)
                        {
                            try
                            {
                                var value = DataExtensions.GetValue(dynamicItem, metaField.Key).ToString();
                                property.SetValue(customMetadata, value == "" ? null : value);
                            }
                            catch
                            {
                                property.SetValue(customMetadata, null);
                            }
                        }
                    }
                    var imageField = dynamicItem.GetRelatedItems<Image>("TwitterImage").FirstOrDefault();
                    customMetadata.TwitterImage = imageField != null ? imageField.MediaUrl : null;

                    if (!customMetadata.IsEmpty())
                    {
                        this.SetPageExtendedMetadataProperties(proxyControl, customMetadata, true);
                    }
                    return;
                }
            }

            // The following should only apply page-level properties if no item-level properties are set.
            if (pageHelper != null && customMetadata.IsEmpty())
            {
                string pageSetting;
                Image pageImage;
                var siteConfig = SystemManager.CurrentContext.GetSetting<CustomSiteSettingsContract, CustomSiteSettingsContract>();

                // The following iterates through all properties defined in the CustomMetadataModel's dictionary and appropriately sets variables based on the dictionary.
                // This is easily extensible by adding new entries into the dictionary.
                foreach (var metaField in CustomMetadataModel.Settings)
                {
                    pageHelper.HasCustomPageValue<string>(metaField.Key, out pageSetting);
                    var property = customMetadata.GetType().GetProperty(metaField.Key);
                    var siteProperty = siteConfig.GetType().GetProperty(metaField.Key);

                    if (property != null)
                    {
                        property.SetValue(customMetadata, pageSetting.IsNullOrEmpty() ?
                            (siteProperty.GetValue(siteConfig).ToString() == "" ? null : siteProperty.GetValue(siteConfig).ToString()) : pageSetting);
                    }
                }

                pageHelper.HasCustomPageObject<Image>("TwitterImage", out pageImage);
                customMetadata.TwitterImage = pageImage == null ? (siteConfig.TwitterImage == "" ? null : siteConfig.TwitterImage) : pageImage.MediaUrl;

                if (!customMetadata.IsEmpty())
                {
                    this.SetPageExtendedMetadataProperties(proxyControl, customMetadata);
                }
            }
        }

        protected DynamicContent GetDynamicContentItem(object item)
        {
            var dynamicContentItem = item as DynamicContent;

            if (dynamicContentItem == null)
            {
                return dynamicContentItem;
            }
            // If content item is not a built in blog or news type, the following checks all other dynamic types for the requested content.
            var dynamicManager = DynamicModuleManager.GetManager();
            var providers = dynamicManager.GetContextProviders();
            var listItems = new List<DynamicContent>();

            foreach (var provider in providers)
            {
                var manager = DynamicModuleManager.GetManager(provider.Name);
                DynamicContent tempItem = manager.GetItems(typeof(DynamicContent), string.Empty, string.Empty, 0, 0)
                    .OfType<DynamicContent>()
                    .Where(i => i.Status == ContentLifecycleStatus.Live && i.Visible == true && i.Id == dynamicContentItem.Id).FirstOrDefault();

                if (tempItem != null)
                {
                    return tempItem;
                }
            }

            // If no matching content item is found, the following return a null object
            return null;
        }

        // The following method sets extended metadata properties based on a previously set custom metadata object
        protected virtual void SetPageExtendedMetadataProperties(MvcProxyBase proxyControl, CustomMetadataModel customMetadata, bool force = false)
        {
            Page page = proxyControl.Page;
            Controller controller = proxyControl.GetController();
            var handler = controller.HttpContext.Handler.GetPageHandler();
            var pageControls = handler.Header.Controls.OfType<System.Web.UI.LiteralControl>();
            CustomMetadataModel existingSettings = new CustomMetadataModel();

            foreach (var control in pageControls)
            {
                foreach (var metaField in CustomMetadataModel.Settings)
                {
                    if (control.Text.IndexOf(metaField.Value[0]) != -1)
                    {
                        var property = existingSettings.GetType().GetProperty(metaField.Key);
                        property.SetValue(existingSettings, control.Text.Split(new string[] { "\"" }, StringSplitOptions.None)[3]);
                    }
                }
            }

            /* 
             * The following actually sets each non-null value found in the custom metadata model 
             * and overwrites any previously set values if the force flag is set, as in the case of blog posts or news items.
            */
            foreach (var metaField in CustomMetadataModel.Settings)
            {
                var property = customMetadata.GetType().GetProperty(metaField.Key);
                var value = property.GetValue(customMetadata);

                if (value != null)
                {
                    if (property.GetValue(existingSettings) == null || property.GetValue(existingSettings).ToString() == "" || force)
                    {
                        PageHelper.TryAddMetaControlToPage(page, metaField.Value[0], property.GetValue(customMetadata).ToString(), metaField.Value[1]);
                    }
                }
            }
        }

        private static readonly ICollection<string> ExcludedMetaProperties = new HashSet<string>()
        {
            nameof(MetadataModel.PageTitleMode),
            nameof(MetadataModel.SEOEnabled),
            nameof(MetadataModel.OpenGraphEnabled),
            nameof(MetadataModel.SEOEnabledPerWidget),
            nameof(MetadataModel.OpenGraphEnabledPerWidget)
        };

        internal static void Register()
        {
            ObjectFactory.Container.RegisterType<IControllerActionInvoker, FeatherActionInvokerCustom>();
        }
    }
}