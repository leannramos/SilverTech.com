using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;


namespace Common.CustomConfig
{
    public class GTMConfig : ConfigSection
    {
        [ConfigurationProperty("GTMHead", DefaultValue = "GTM Head Value", IsRequired = true)]
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
        [ConfigurationProperty("GTMBody", DefaultValue = "GTM Body Value", IsRequired = true)]
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
