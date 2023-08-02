using SilverTech.Azure;
using SilverTech.Azure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace SilverTech.Azure.Cdn
{
    /// <summary>
    /// Service for managing Azure CDN
    /// </summary>
    public class CdnService
    {

        /// <summary>
        /// Name of the CDN Profile
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Id of Azure Subscription
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Name of Resource Group containing CDN
        /// </summary>
        public string ResourceGroup { get; set; }

        /// <summary>
        /// ID of tenant containg CDN
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// ID of Azure AD Application to authenticate against.
        /// An Azure AD app must be created and given access to the CDN Profile.
        /// </summary>
        public string AppClientId { get; set; }

        /// <summary>
        /// Azure AD App secret to use for authentication.
        /// </summary>
        public string AppSecret { get; set; }



        public CdnService()
        {
          
        }

        /// <summary>
        /// Validates Service properties set.  Throws exception if invalid.
        /// </summary>
        protected void ValidateProperties()
        {

            if (string.IsNullOrEmpty(ProfileName)) {
                throw new InvalidOperationException("ProfileName property is empty.");
            }

            if (string.IsNullOrEmpty(SubscriptionId))
            {
                throw new InvalidOperationException("SubscriptionId property is empty.");
            }

            if (string.IsNullOrEmpty(ResourceGroup))
            {
                throw new InvalidOperationException("ResourceGroup property is empty.");
            }

            if (string.IsNullOrEmpty(TenantId))
            {
                throw new InvalidOperationException("TenantId property is empty.");
            }

            if (string.IsNullOrEmpty(AppClientId))
            {
                throw new InvalidOperationException("AppClientId property is empty.");
            }

            if (string.IsNullOrEmpty(AppSecret))
            {
                throw new InvalidOperationException("AppSecret property is empty.");
            }

        }

        /// <summary>
        /// Purges the supplied paths from Azure CDN. 
        /// </summary>
        /// <param name="endPointName">Name of endpoint to purge</param>
        /// <param name="contentPaths">List of paths to purge.  Use * fro wildcard paths.  Use /* for all content.</param>
        public async Task Purge(string endPointName, IEnumerable<string> contentPaths)
        {
            ValidateProperties();

            var purgeEndPointUrl = "https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Cdn/profiles/{profileName}/endpoints/{endpointName}/purge?api-version=2019-12-31";

            purgeEndPointUrl = purgeEndPointUrl.Replace("{subscriptionId}", SubscriptionId)
              .Replace("{resourceGroupName}", ResourceGroup)
              .Replace("{profileName}", ProfileName)
              .Replace("{endpointName}", endPointName);


            var authHelper = new AuthenticationService();
            var token = authHelper.GetOAuthTokenFromAAD_ByCredentials(TenantId, AppClientId, AppSecret);

            var contentToPurge = new { contentPaths = contentPaths.ToArray<string>() };

            await DoPost(contentToPurge, new Uri(purgeEndPointUrl), token);
        }

        private static async Task<string> DoPost(object data, Uri endpoint, string token)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = endpoint;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.PostAsJsonAsync(endpoint, data);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception("Invalid call to Azure CDN for Purge. \r\n" + result);
            }

            return result;

        }
    }
}
