using System;
using SitefinityWebApp.PropertyDescriptors;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend;
using Telerik.Sitefinity.Frontend.Card.Mvc.Models.Card;
using SitefinityWebApp.Mvc.Models;
using Telerik.Sitefinity.Configuration;
using Common.CustomConfig;
using SitefinityWebApp.Custom.Sitefinity.Search;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;
using SitefinityWebApp.Custom.Sitefinity.Rates;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Web.Events;
using Telerik.Sitefinity.Configuration.Web.UI.Basic;
using ConservationInternational.Sitefinity.Public.Admin;
using SitefinityWebApp.Sitefinity.Public.Admin.Config;
using SitefinityWebApp.App_Start;
using System.Web.Optimization;
using System.Web.Hosting;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;
using SitefinityWebApp.Mvc.Models.Document;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models;
using SitefinityWebApp.Mvc.Models.CustomSubscribe;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {
        #region GlobalVariables
        public static GlobalConfig globalConfig { get; set; }
        #endregion
        
        protected void Application_Start(object sender, EventArgs e)
        {
            SystemManager.RegisterWebService(typeof(RatesColumnService), "Sitefinity/Services/Rates/RatesColumnService.svc");

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.VirtualPathProvider = HostingEnvironment.VirtualPathProvider;
            BundleTable.EnableOptimizations = true;

            Bootstrapper.Bootstrapped += Bootstrapper_Bootstrapped;
            Bootstrapper.Initialized += new EventHandler<ExecutedEventArgs>(Bootstrapper_Initialized);
            ObjectFactory.Initialized += RegisterCommonTypesEventHandler;
        }

        private void RegisterCommonTypesEventHandler(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "RegisterCommonTypes")
            {
                ObjectFactory.Container.RegisterType<IHtmlSanitizer, SitefinityExtendedHtmlSanitizer>(new ContainerControlledLifetimeManager());
            }
        }

        protected void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                LocationsInboundPipe.Register();
                EventHub.Subscribe<IPagePreRenderCompleteEvent>(this.OnPagePreRenderCompleteEventHandler);
                // Registers the Content_Action method as a data event
                EventHub.Subscribe<IDataEvent>(Content_Action);
            }
        }

        protected void Bootstrapper_Bootstrapped(object sender, EventArgs e)
        {
            #region CustomConfigurations
            Config.RegisterSection<GlobalConfig>();
            Config.RegisterSection<SiteConfig>();
            Config.RegisterSection<AzureCdnConfig>();

            SystemManager.RegisterBasicSettings<GenericBasicSettingsView<CustomSiteSettings, CustomSiteSettingsContract>>("SiteConfig", "Site Settings", "", true);
            globalConfig = Config.Get<GlobalConfig>();
            #endregion

            #region DependencyInjection
            //The Custom Card Model contains the new properties we've added that are persisted and retrieved at runtime.
            FrontendModule.Current.DependencyResolver.Rebind<ICardModel>().To<CustomCardModel>();
            FrontendModule.Current.DependencyResolver.Rebind<IDocumentModel>().To<CustomDownloadDocumentModel>();
            FrontendModule.Current.DependencyResolver.Rebind<ISubscribeFormModel>().To<CustomSubscribeFormModel>();
            FrontendModule.Current.DependencyResolver.Rebind<IRecaptchaModel>().To<RecaptchaModel>();
            FrontendModule.Current.DependencyResolver.Rebind<IImageModel>().To<CustomImageModel>();
            #endregion

            #region SitefinityOverrides
            //Custom Property Description service so the designers can retrieve the actual model of a type at runtime instead of utilizing the interface
            ControllerSettingsPropertyDescriptorCustom.Install("Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy.Settings");
            ControllerSettingsPropertyDescriptorCustom.Install(string.Format("{0}.{1}", typeof(Telerik.Sitefinity.Mvc.Proxy.MvcProxyBase).FullName, "Settings"));
            ControllerSettingsPropertyDescriptorCustom.Install(string.Format("{0}.{1}", typeof(MvcWidgetProxy).FullName, "Settings"));
            
            //Allows for custom http status codes to be returned
            FeatherActionInvokerCustom.Register();
            #endregion
        }


        private void OnPagePreRenderCompleteEventHandler(IPagePreRenderCompleteEvent evt)
        {
            if (!evt.PageSiteNode.IsBackend)
            {
                var suffix = globalConfig.PageTitleSuffix ?? string.Empty;
                evt.Page.Title = evt.Page.Title + suffix;
            }
        }

        /* 
         * The Content_Action process is run after any file management call is made in the Sitefinity CMS.
         * This event processes and stores any SVG images' contents in the database when uploaded, to enable the use of fully-featured SVG images.
        */
        private void Content_Action(IDataEvent @event)
        {
            EventHandlers.SVGImageActionHandler.Content_Action(@event);
            EventHandlers.AzureCdnActionHandler.Content_Action(@event);
        }
    }
}


    public class SitefinityExtendedHtmlSanitizer : HtmlSanitizer
{
    public SitefinityExtendedHtmlSanitizer()
    {
        base.AllowedAttributes.Add("class");
        base.AllowedTags.Add("iframe");
        base.AllowedSchemes.Add("tel");
    }
}