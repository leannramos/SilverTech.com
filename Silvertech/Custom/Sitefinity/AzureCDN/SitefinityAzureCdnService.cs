using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Common.CustomConfig;
using SilverTech.Azure.Cdn;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;

namespace SilverTech.Azure.Cdn.Sitefinity
{
    /// <summary>
    /// Service for managing Azure CDN within Sitefinity.Relies on AzureCdnConfig settings.
    /// </summary>
    public class SitefinityAzureCdnService : CdnService
    {

        private AzureCdnConfig _config;
        private string _endpointname;

        public SitefinityAzureCdnService()
        {
            _config = Config.Get<AzureCdnConfig>();


            this.AppClientId = _config.AppClientId;
            this.AppSecret = _config.AppSecret;
            this.ProfileName = _config.CdnProfileName;
            this.ResourceGroup = _config.ResourceGroup;
            this.SubscriptionId = _config.SubscriptionID;
            this.TenantId = _config.TenantID;

            _endpointname = _config.CdnEndPoint;
        }

        /// <summary>
        /// Purges the supplied paths from Azure CDN. Uses EndPoint from Sitefintiy Settings.
        /// </summary>
        /// <param name="contentPaths">List of paths to purge.  Use * fro wildcard paths.  Use /* for all content.</param>
        public void Purge(IEnumerable<string> contentPaths)
        {
            try
            {
                Task task = base.Purge(_endpointname, contentPaths);
                task.Wait();

                if(task.Exception != null)
                {
                    Log.Write("Error Purging CDN Content: \r\n" + task.Exception.ToString());

                    task.Exception.Handle(ex =>
                    {
                        Log.Write(ex.ToString(), ConfigurationPolicy.ErrorLog);
                        return true;
                    });
                }
            }
            catch (AggregateException ae)
            {
                ae.Handle(inner =>
                {
                    Log.Write(inner.ToString(), ConfigurationPolicy.ErrorLog);
                    return true;
                });
            }
            catch (Exception e)
            {
                Log.Write("Error Purging CDN Content: \r\n" + e.ToString(), ConfigurationPolicy.ErrorLog);
            }
        }

    }
}