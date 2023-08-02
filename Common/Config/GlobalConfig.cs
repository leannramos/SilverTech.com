using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;


namespace Common.CustomConfig
{
    public class GlobalConfig : ConfigSection
    {
        [ConfigurationProperty("PhoneNumber", DefaultValue = "555-555-5555", IsRequired = true)]
        public string PhoneNumber
        {
            get
            {
                return (string)this["PhoneNumber"];
            }
            set
            {
                this["PhoneNumber"] = value;
            }
        }
    
        [ConfigurationProperty("SchemaLogo", DefaultValue = "https://www.silvertech.com/images/default-source/default-album/logos/silvertech-logo-sm.png?sfvrsn=8100e559_2", IsRequired = true)]
        public string SchemaLogo
        {
            get
            {
                return (string)this["SchemaLogo"];
            }
            set
            {
                this["SchemaLogo"] = value;
            }
        }    

        [ConfigurationProperty("SchemaImage", DefaultValue = "https://www.silvertech.com/images/default-source/default-album/footer-images/schoolhouse-from-outside2-min.jpg?sfvrsn=188c49c6_2", IsRequired = true)]
        public string SchemaImage
        {
            get
            {
                return (string)this["SchemaImage"];
            }
            set
            {
                this["SchemaImage"] = value;
            }
        }

        [ConfigurationProperty("Address", DefaultValue = "", IsRequired = false)]
        public string Address
        {
            get
            {
                return (string)this["Address"];
            }
            set
            {
                this["Address"] = value;
            }
        }

        [ConfigurationProperty("PageTitleSuffix", DefaultValue = " | Silvertech", IsRequired = true)]
        public string PageTitleSuffix
        {
            get
            {
                return (string)this["PageTitleSuffix"];
            }
            set
            {
                this["PageTitleSuffix"] = value;
            }
        }

        [ConfigurationProperty("BlogListingUrl", DefaultValue = "/blogs")]
        public string BlogListingUrl
        {
            get
            {
                return (string)this["BlogListingUrl"];
            }
            set
            {
                this["BlogListingUrl"] = value;
            }
        }

        [ConfigurationProperty("NewsletterPageUrl", DefaultValue = "/newsletter")]
        public string NewsletterPageUrl
        {
            get
            {
                return (string)this["NewsletterPageUrl"];
            }
            set
            {
                this["NewsletterPageUrl"] = value;
            }
        }

        [ConfigurationProperty("AppointmentBookingUrl", DefaultValue = "https://betterlobby.amhfcu.org/apptbookingtool/", IsRequired = true)]
        public string AppointmentBookingUrl
        {
            get
            {
                return (string)this["AppointmentBookingUrl"];
            }
            set
            {
                this["AppointmentBookingUrl"] = value;
            }
        }

        [ConfigurationProperty("CTASpeedbumpText", DefaultValue = "You are leaving our site. ", IsRequired = true)]
        public string CTASpeedbumpText
        {
            get
            {
                return (string)this["CTASpeedbumpText"];
            }
            set
            {
                this["CTASpeedbumpText"] = value;
            }
        }

        [ConfigurationProperty("EditMobileMenu", DefaultValue = false, IsRequired = true)]
        public bool EditMobileMenu
        {
            get
            {
                return (bool)this["EditMobileMenu"];
            }
            set
            {
                this["EditMobileMenu"] = value;
            }
        }

        [ConfigurationProperty("RecaptchaSiteKey", DefaultValue = "", IsRequired = true)]
        public string RecaptchaSiteKey
        {
            get
            {
                return (string)this["RecaptchaSiteKey"];
            }
            set
            {
                this["RecaptchaSiteKey"] = value;
            }
        }

        [ConfigurationProperty("RecaptchaSecretKey", DefaultValue = "", IsRequired = true)]
        public string RecaptchaSecretKey
        {
            get
            {
                return (string)this["RecaptchaSecretKey"];
            }
            set
            {
                this["RecaptchaSecretKey"] = value;
            }
        }

        [ConfigurationProperty(ConfigElement.ParametersPropertyName)]
        public NameValueCollection Parameters
        {
            get
            {
                return (NameValueCollection)this[ConfigElement.ParametersPropertyName];
            }
            set
            {
                this[ConfigElement.ParametersPropertyName] = value;
            }
        }
    }


}
