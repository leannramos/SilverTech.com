using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace SitefinityWebApp.Sitefinity.Public.Admin.Config
{
    [DescriptionResource(typeof(ConfigDescriptions), "SiteConfig")]
    public class SiteConfig : ConfigSection
    {

        [ConfigurationProperty("NewsHeader", DefaultValue = "", IsRequired = true)]
        public virtual string NewsHeader
        {
            get
            {
                return (string)base["NewsHeader"];
            }
            set
            {
                base["NewsHeader"] = value;
            }
        }

        [ConfigurationProperty("NewsSubheader", DefaultValue = "", IsRequired = true)]
        public virtual string NewsSubheader
        {
            get
            {
                return (string)base["NewsSubheader"];
            }
            set
            {
                base["NewsSubheader"] = value;
            }
        }

        [ConfigurationProperty("NewsImageUrl", DefaultValue ="", IsRequired = true)]
        public virtual string NewsImageUrl
        {
            get
            {                
                return (string)base["NewsImageUrl"];
            }
            set
            {
                base["NewsImageUrl"] = value;
            }
        }

        [ConfigurationProperty("GTMHead", DefaultValue = "", IsRequired = true)]
        public string GTMHead
        {
            get
            {
                return (string)this["GTMHead"];
            }
            set
            {
                this["GTMHead"] = value;
            }
        }

        [ConfigurationProperty("GTMBody", DefaultValue = "", IsRequired = true)]
        public string GTMBody
        {
            get
            {
                return (string)this["GTMBody"];
            }
            set
            {
                this["GTMBody"] = value;
            }
        }

        [ConfigurationProperty("TwitterHandle", DefaultValue = "@silvertech", IsRequired = true)]
        public string TwitterHandle
        {
            get
            {
                return (string)this["TwitterHandle"];
            }
            set
            {
                this["TwitterHandle"] = value;
            }
        }

        [ConfigurationProperty("TwitterCardImagePath", DefaultValue = "/resourcepackages/st/library/img/logo.png", IsRequired = true)]
        public string TwitterCardImagePath
        {
            get
            {
                return (string)this["TwitterCardImagePath"];
            }
            set
            {
                this["TwitterCardImagePath"] = value;
            }
        }

        [ConfigurationProperty("TwitterCard", DefaultValue = "", IsRequired = true)]
        public string TwitterCard
        {
            get
            {
                return (string)this["TwitterCard"];
            }
            set
            {
                this["TwitterCard"] = value;
            }
        }

        [ConfigurationProperty("TwitterSite", DefaultValue = "", IsRequired = true)]
        public string TwitterSite
        {
            get
            {
                return (string)this["TwitterSite"];
            }
            set
            {
                this["TwitterSite"] = value;
            }
        }

        [ConfigurationProperty("TwitterTitle", DefaultValue = "", IsRequired = true)]
        public string TwitterTitle
        {
            get
            {
                return (string)this["TwitterTitle"];
            }
            set
            {
                this["TwitterTitle"] = value;
            }
        }

        [ConfigurationProperty("TwitterDescription", DefaultValue = "", IsRequired = true)]
        public string TwitterDescription
        {
            get
            {
                return (string)this["TwitterDescription"];
            }
            set
            {
                this["TwitterDescription"] = value;
            }
        }

        [ConfigurationProperty("TwitterImage", DefaultValue = "", IsRequired = true)]
        public string TwitterImage
        {
            get
            {
                return (string)this["TwitterImage"];
            }
            set
            {
                this["TwitterImage"] = value;
            }
        }



        [ConfigurationProperty("BlogUrl", DefaultValue = "", IsRequired = true)]
        public string BlogUrl
        {
            get
            {
                return (string)this["BlogUrl"];
            }
            set
            {
                this["BlogUrl"] = value;
            }
        }
        [ConfigurationProperty("ExpertListingUrl", DefaultValue = "", IsRequired = true)]
        public string ExpertListingUrl
        {
            get
            {
                return (string)this["ExpertListingUrl"];
            }
            set
            {
                this["ExpertListingUrl"] = value;
            }
        }
        [ConfigurationProperty("RssFeedUrl", DefaultValue = "", IsRequired = true)]
        public string RssFeedUrl
        {
            get
            {
                return (string)this["RssFeedUrl"];
            }
            set
            {
                this["RssFeedUrl"] = value;
            }
        }
        
    }
}