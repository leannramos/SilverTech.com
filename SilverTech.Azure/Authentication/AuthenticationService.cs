
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SilverTech.Azure.Authentication
{
    public class AuthenticationService
    {


        /// <summary>
        /// Fetches the Azure Authentication Token From Azure Active Directory (AAD) using credentials
        /// Note: For this method to work follow the section "Authenticate with password - PowerShell" from the below URL
        /// https://azure.microsoft.com/en-us/documentation/articles/resource-group-authenticate-service-principal/
        /// </summary>
        /// <param name="tenantId">Tenant ID from your Azure Subscription</param>
        /// <param name="clientId">GUID for AAD application configured as Native Client App in AAD tenant specified above</param>
        /// <param name="secret">Password configured for Service Principal</param>
        /// <returns>Authentication Token</returns>
        public string GetOAuthTokenFromAAD_ByCredentials(string tenantId, string clientId, string secret)
        {
      
            string token = string.Empty;
            var authenticationContext = new AuthenticationContext("https://login.windows.net/" + tenantId, false);
            var credential = new ClientCredential(clientId, secret);

            var result = authenticationContext.AcquireTokenAsync(resource: "https://management.core.windows.net/", clientCredential: credential);

            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain token");
            }

          
            token = result.Result.AccessToken;
            return token;
        }

        /// <summary>
        /// Fetches the Azure Authentication Token From Azure Active Directory (AAD) using a Certificate
        /// Note: For this method to work follow the section "Authenticate with certificate - PowerShell" from the below URL
        /// https://azure.microsoft.com/en-us/documentation/articles/resource-group-authenticate-service-principal/
        /// </summary>
        /// <param name="tenantId">Tenant ID from your Azure Subscription</param>
        /// <param name="clientId">GUID for AAD application configured as Native Client App in AAD tenant specified above</param>
        /// <param name="certificateName">Name of certificate. This should be available where the service is run.</param>
        /// <returns>Authentication Token</returns>
        public string GetOAuthTokenFromAAD_ByCertificate(string tenantId, string clientId, string certificateName)
        {
            var authContext = new AuthenticationContext(string.Format("https://login.windows.net/{0}", tenantId));
            X509Certificate2 cert = null;

            //Assuming the Cert is installed under current user under 
            //Not sure how we would do this in Azure....  but im sure theres a way
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            try
            {
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates;
                var certs = certCollection.Find(X509FindType.FindBySubjectName, certificateName, false);

                if (certs == null || certs.Count <= 0)
                {
                    throw new Exception("Certificate " + certificateName + " not found.");
                }
                cert = certs[0];
            }
            finally
            {
                store.Close();
            }

            var certCred = new ClientAssertionCertificate(clientId, cert);
            var token = authContext.AcquireTokenAsync("https://management.core.windows.net/", certCred);

            return token.Result.AccessToken;
        }
    }
}
