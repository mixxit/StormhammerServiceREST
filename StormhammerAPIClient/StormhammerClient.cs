using Newtonsoft.Json;
using StormhammerLibrary.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StormhammerAPIClient
{
    public class StormhammerClient : IDisposable
    {
        readonly private Uri serviceUri;
        readonly private string jwtToken;

        public StormhammerClient(String appId, String userSecret, SystemTypeEnum systemType = SystemTypeEnum.Dev)
        {
            this.serviceUri = GetServiceUriForSystemType(systemType);
            this.jwtToken = GetAuthTokenForSystemType(appId, userSecret, systemType);
        }

        private Uri GetServiceUriForSystemType(SystemTypeEnum systemType)
        {
            switch (systemType)
            {
                case SystemTypeEnum.Local:
                    return new Uri("https://localhost:44383");
                case SystemTypeEnum.Dev:
                    return new Uri("https://stormhammerservicerestdev.azurewebsites.net");
                default:
                    return GetServiceUriForSystemType(SystemTypeEnum.Local);
            }
        }

        private string GetToken(string clientId, string authority, string secret)
        {
            /*return Task.Run(async () =>
            {
                return await SecretAuthentication.AuthAsync(clientId,
              authority,
              secret,
              new List<string>() { String.Format(clientId + "/.default") }).ConfigureAwait(false);
            }).Result.AccessToken;*/

            return "";
        }

        private string GetAuthTokenForSystemType(String appId, String secret, SystemTypeEnum systemType)
        {
            switch (systemType)
            {
                case SystemTypeEnum.Local:
                    return GetToken(appId, "https://login.microsoftonline.com/stormhammer.onmicrosoft.com/v2.0", secret);
                case SystemTypeEnum.Dev:
                    return GetToken(appId, "https://login.microsoftonline.com/stormhammer.onmicrosoft.com/v2.0", secret);
                default:
                    return GetAuthTokenForSystemType(appId, secret, SystemTypeEnum.Local);
            }
        }

        public async Task<R> GetRequestAsync<R>(string endpoint)
        {
            using (var client = HttpClientFactory.CreateClient(serviceUri, jwtToken))
            {
                HttpResponseMessage responseMessage = await client.GetAsync($"/{endpoint}").ConfigureAwait(false);

                var responseJson = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<R>(responseJson);
                }
                else
                {
                    Console.WriteLine("Received response code: " + responseMessage.StatusCode + " " + responseJson);
                    throw new HttpResponseException(responseMessage);
                }
            }
        }

        public async Task<R> PostRequestAsync<Q, R>(string endpoint, Q request)
        {
            using (var client = HttpClientFactory.CreateClient(serviceUri, jwtToken))
            {
                String json = JsonConvert.SerializeObject(request);
                HttpResponseMessage responseMessage = await client.PostAsync($"/{endpoint}", new StringContent(json, Encoding.UTF8, "application/json")).ConfigureAwait(false);

                var responseJson = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<R>(responseJson);
                }
                else
                {
                    Console.WriteLine("Received response code: " + responseMessage.StatusCode + " " + responseJson);
                    throw new HttpResponseException(responseMessage);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
