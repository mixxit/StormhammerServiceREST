using Newtonsoft.Json;
using StormhammerLibrary.Models;
using StormhammerLibrary.Models.Request;
using StormhammerLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StormhammerAPIClient
{
    public partial class StormhammerClient : IDisposable
    {
        readonly private Uri serviceUri;
        private string jwtToken = "";

        public StormhammerClient(String userName, String password, SystemTypeEnum systemType = SystemTypeEnum.Dev)
        {
            this.serviceUri = GetServiceUriForSystemType(systemType);

            try
            {
                var response = Task.Run(async () =>
                {
                    return await LoginAndSetTokenAsync(userName, password).ConfigureAwait(true);
                }).Result;
            }
            catch (Exception e)
            {
                // Do nothing, probably not logged in
            }
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

        public async Task<LoginResponse> LoginAndSetTokenAsync(string userName, string password)
        {
            using (var client = HttpClientFactory.CreateClient(serviceUri, jwtToken))
            {
                var request = new LoginRequest()
                {
                    email = userName,
                    password = password
                };

                var json = JsonConvert.SerializeObject(request);

                HttpResponseMessage responseMessage = await client.PostAsync($"/Auth/Login", new StringContent(json, Encoding.UTF8, "application/json")).ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var response = JsonConvert.DeserializeObject<LoginResponse>(responseJson);
                    this.jwtToken = response.token;
                    return JsonConvert.DeserializeObject<LoginResponse>(responseJson);
                }
                else
                {
                    this.jwtToken = "";
                    return new LoginResponse()
                    {
                        token = ""
                };
            }
            }
        }

        public async Task<RegisterResponse> RegisterAsync(string userName, string password, string confirmPassword)
        {
            using (var client = HttpClientFactory.CreateClient(serviceUri, jwtToken))
            {
                var request = new RegisterRequest()
                {
                    email = userName,
                    password = password,
                    confirmPassword = confirmPassword
                };

                var json = JsonConvert.SerializeObject(request);

                HttpResponseMessage responseMessage = await client.PostAsync($"/Register", new StringContent(json, Encoding.UTF8, "application/json")).ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseJson = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<RegisterResponse>(responseJson);
                }
                else
                {
                    var errors = new List<string>();
                    try
                    {
                        var responseJson = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                        errors = JsonConvert.DeserializeObject<RegisterResponse>(responseJson).errors;
                    } catch (Exception)
                    {

                    }

                    return new RegisterResponse()
                    {
                        succeeded = false,
                        errors = errors
                    };
                }
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

        public async Task<bool> DeleteRequestAsync(string endpoint, long id)
        {
            using (var client = HttpClientFactory.CreateClient(serviceUri, jwtToken))
            {
                HttpResponseMessage responseMessage = await client.DeleteAsync($"/{endpoint}/?id={id}").ConfigureAwait(false);
                
                var responseJson = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
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
