using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;


namespace Common.CustomConfig
{
    public class AzureCdnConfig : ConfigSection
    {

        [ConfigurationProperty("InvalidateOnPublish", DefaultValue = false, IsRequired = false)]
        public bool InvalidateOnPublish
        {
            get
            {
                return (bool)this["InvalidateOnPublish"];
            }
            set
            {
                this["InvalidateOnPublish"] = value;
            }
        }

        [ConfigurationProperty("TenantID", DefaultValue = "", IsRequired = true)]
        public string TenantID
        {
            get
            {
                return (string)this["TenantID"];
            }
            set
            {
                this["TenantID"] = value;
            }
        }

        [ConfigurationProperty("SubscriptionID", DefaultValue = "", IsRequired = true)]
        public string SubscriptionID
        {
            get
            {
                return (string)this["SubscriptionID"];
            }
            set
            {
                this["SubscriptionID"] = value;
            }
        }

        [ConfigurationProperty("AppClientId", DefaultValue = "", IsRequired = true)]
        public string AppClientId
        {
            get
            {
                return (string)this["AppClientId"];
            }
            set
            {
                this["AppClientId"] = value;
            }
        }

        [ConfigurationProperty("AppSecret", DefaultValue = "", IsRequired = true)]
        public string AppSecret
        {
            get
            {
                return (string)this["AppSecret"];
            }
            set
            {
                this["AppSecret"] = value;
            }
        }


        [ConfigurationProperty("ResourceGroup", DefaultValue = "", IsRequired = true)]
        public string ResourceGroup
        {
            get
            {
                return (string)this["ResourceGroup"];
            }
            set
            {
                this["ResourceGroup"] = value;
            }
        }

        [ConfigurationProperty("CdnProfileName", DefaultValue = "", IsRequired = true)]
        public string CdnProfileName
        {
            get
            {
                return (string)this["CdnProfileName"];
            }
            set
            {
                this["CdnProfileName"] = value;
            }

        }

        [ConfigurationProperty("CdnEndPoint", DefaultValue = "", IsRequired = true)]
        public string CdnEndPoint
        {
            get
            {
                return (string)this["CdnEndPoint"];
            }
            set
            {
                this["CdnEndPoint"] = value;
            }
                
        }

    }
}
