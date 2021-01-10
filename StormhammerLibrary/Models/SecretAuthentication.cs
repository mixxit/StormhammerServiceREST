using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StormhammerLibrary.Models
{
    /*public class SecretAuthentication
    {
        public static async Task<AuthenticationResult> AuthAsync(String clientId, String authority, String clientSecret, List<String> scopes, string redirectUrl = "https://localhost")
        {
            var clientCredentials = ConfidentialClientApplicationBuilder.Create(clientId).WithClientSecret(clientSecret).WithAuthority(new Uri(authority)).WithRedirectUri(redirectUrl).Build();
            return await clientCredentials.AcquireTokenForClient(scopes).ExecuteAsync().ConfigureAwait(true);
        }
    }*/
}
