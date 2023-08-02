using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.SiteSettings;

namespace SitefinityWebApp.Sitefinity.Public.Admin.Config
{
    [DataContract]
    public class CustomSiteSettingsContract : ISettingsDataContract
    {
        [DataMember]
        public string GTMHead
        {
            get;
            set;
        }

        [DataMember]
        public string GTMBody
        {
            get;
            set;
        }
        [DataMember]
        public string TwitterCard
        {
            get;
            set;
        }

        [DataMember]
        public string TwitterSite
        {
            get;
            set;
        }

        [DataMember]
        public string TwitterTitle
        {
            get;
            set;
        }

        [DataMember]
        public string TwitterDescription
        {
            get;
            set;
        }

        [DataMember]
        public string TwitterImage
        {
            get;
            set;
        }
        public void LoadDefaults(bool forEdit = false)
        {
            SiteConfig section = null;
            if (forEdit)
            {
                section = ConfigManager.GetManager().GetSection<SiteConfig>();
            }
            else
            {
                try
                {
                    section = Telerik.Sitefinity.Configuration.Config.Get<SiteConfig>();
                }catch(Exception e)
                {
                    Telerik.Sitefinity.Abstractions.Log.Write(e.Message);
                }
            }
               
            this.GTMBody = section?.GTMBody ?? "";
            this.GTMHead = section?.GTMHead ?? "";
            this.TwitterCard = section?.TwitterCard ?? "";
            this.TwitterSite = section?.TwitterSite ?? "";
            this.TwitterTitle = section?.TwitterTitle ?? "";
            this.TwitterDescription = section?.TwitterDescription ?? "";
            this.TwitterImage = section?.TwitterImage ?? "";
        }

        public void SaveDefaults()
        {
            var manager = ConfigManager.GetManager();
            var section = manager.GetSection<SiteConfig>();
            section.GTMBody = this.GTMBody;
            section.GTMHead = this.GTMHead;
            section.TwitterCard = this.TwitterCard;
            section.TwitterSite = this.TwitterSite;
            section.TwitterTitle = this.TwitterTitle;
            section.TwitterDescription = this.TwitterDescription;
            section.TwitterImage = this.TwitterImage;
            manager.SaveSection(section);
        }

    }
}