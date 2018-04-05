﻿using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Stash.Workers
{
    internal class HttpCommunicationWorker
    {
        private Uri baseUrl;
        private AuthenticationHeaderValue authenticationHeader = null;

        public HttpCommunicationWorker(string baseUrl, string base64Auth)
        {
            this.baseUrl = new Uri(baseUrl);

            SetBasicAuthentication(base64Auth);
        }

        public HttpCommunicationWorker(string baseUrl, string username, string password)
        {
            this.baseUrl = new Uri(baseUrl);

            SetBasicAuthentication(username, password);
        }

        public void SetBasicAuthentication(string base64Auth)
        {
            this.authenticationHeader = new AuthenticationHeaderValue("Basic", base64Auth);
        }

        public void SetBasicAuthentication(string username, string password)
        {
            byte[] userPassBytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password));
            string userPassBase64 = Convert.ToBase64String(userPassBytes);

            SetBasicAuthentication(userPassBase64);
        }

        /// <summary>
        /// Creates a new instance of System.Net.Http.HttpClient
        /// </summary>
        /// <remarks>must be disposed by caller</remarks>
        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = this.baseUrl;

            if (this.authenticationHeader != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = this.authenticationHeader;
            }

            return httpClient;
        }

        public async Task<T> GetAsync<T>(string requestUrl)
        {
            using (HttpClient httpClient = CreateHttpClient())
            using (HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUrl).ConfigureAwait(false))
            {
                string json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception ex)
                {
                    ex.Data["json"] = json;
                    throw;
                }
            }
        }

        public async Task<string> GetAsync(string requestUrl)
        {
            using (HttpClient httpClient = CreateHttpClient())
            using (HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUrl).ConfigureAwait(false))
            {
                string json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                return json;
            }
        }

        public async Task<T> PostAsync<T>(string requestUrl, T data, bool ignoreNullFields = false)
        {
            string strData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                NullValueHandling = ignoreNullFields ? NullValueHandling.Ignore : NullValueHandling.Include
            });
            HttpContent contentToPost = new StringContent(strData, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = CreateHttpClient())
            using (HttpResponseMessage httpResponse = await httpClient.PostAsync(requestUrl, contentToPost))
            {
                string json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (httpResponse.StatusCode != HttpStatusCode.Created && httpResponse.StatusCode != HttpStatusCode.OK && httpResponse.StatusCode != HttpStatusCode.NoContent)
                {
                    var exc = new Exception(string.Format("POST operation unsuccessful. Got HTTP status code '{0}'", httpResponse.StatusCode));
                    exc.Data["json"] = json;
                    throw exc;
                }

                if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(T);
                }

                T response = JsonConvert.DeserializeObject<T>(json);

                return response;
            }
        }

        public async Task<T> PutAsync<T>(string requestUrl, T data, bool ignoreNullFields = false)
        {
            string strData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                NullValueHandling = ignoreNullFields ? NullValueHandling.Ignore : NullValueHandling.Include
            });
            HttpContent contentToPut = new StringContent(strData, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = CreateHttpClient())
            using (HttpResponseMessage httpResponse = (data != null) ?
                                    await httpClient.PutAsync(requestUrl, contentToPut) :
                                    await httpClient.PutAsync(requestUrl, null).ConfigureAwait(false))
            {
                if (httpResponse.StatusCode != HttpStatusCode.Created && httpResponse.StatusCode != HttpStatusCode.OK && httpResponse.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new Exception(string.Format("PUT operation unsuccessful. Got HTTP status code '{0}'", httpResponse.StatusCode));
                }

                if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(T);
                }

                string json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                T response = JsonConvert.DeserializeObject<T>(json);

                return response;
            }
        }

        public async Task<string> PutAsync(string requestUrl, string data)
        {
            using (HttpClient httpClient = CreateHttpClient())
            using (HttpResponseMessage httpResponse = (data != null) ?
                                    await httpClient.PutAsync(requestUrl, new StringContent(data, Encoding.UTF8, "application/json")).ConfigureAwait(false) :
                                    await httpClient.PutAsync(requestUrl, null).ConfigureAwait(false))
            {
                if (httpResponse.StatusCode != HttpStatusCode.Created && httpResponse.StatusCode != HttpStatusCode.OK && httpResponse.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new Exception(string.Format("PUT operation unsuccessful. Got HTTP status code '{0}'", httpResponse.StatusCode));
                }

                string json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                return json;
            }
        }

        public async Task DeleteAsync(string requestUrl)
        {
            using (HttpClient httpClient = CreateHttpClient())
            using (HttpResponseMessage httpResponse = await httpClient.DeleteAsync(requestUrl).ConfigureAwait(false))
            {
                if (httpResponse.StatusCode != HttpStatusCode.NoContent && httpResponse.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(string.Format("DELETE operation unsuccessful! Got HTTP status code '{0}'", httpResponse.StatusCode));
                }
            }
        }

        public async Task<T> DeleteWithResponseContentAsync<T>(string requestUrl)
        {
            using (HttpClient httpClient = CreateHttpClient())
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUrl))
            using (HttpResponseMessage httpResponse = await httpClient.SendAsync(requestMessage).ConfigureAwait(false))
            {
                if (httpResponse.StatusCode != HttpStatusCode.NoContent && httpResponse.StatusCode != HttpStatusCode.Accepted && httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(string.Format("DELETE operation unsuccessful! Got HTTP status code '{0}'", httpResponse.StatusCode));
                }

                string json = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                T response = JsonConvert.DeserializeObject<T>(json);

                return response;
            }
        }

        public async Task DeleteWithRequestContentAsync<T>(string requestUrl, T data)
        {
            using (HttpClient httpClient = CreateHttpClient())
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUrl))
            {
                string jsonData = JsonConvert.SerializeObject(data);

                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using (HttpResponseMessage httpResponse = await httpClient.SendAsync(requestMessage).ConfigureAwait(false))
                {
                    if (httpResponse.StatusCode != HttpStatusCode.NoContent && httpResponse.StatusCode != HttpStatusCode.Accepted)
                    {
                        throw new Exception(string.Format("DELETE operation unsuccessful! Got HTTP status code '{0}'", httpResponse.StatusCode));
                    }
                }
            }
        }
    }
}
