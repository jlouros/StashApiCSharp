using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Workers
{
    internal class HttpCommunicationWorker
    {
        private HttpClient _httpClient;

        public HttpCommunicationWorker(string baseUrl, string base64Auth)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);

            SetBasicAuthentication(base64Auth);
        }

        public HttpCommunicationWorker(string baseUrl, string username, string password)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);

            SetBasicAuthentication(username, password);
        }

        public void SetBasicAuthentication(string base64Auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);
        }

        public void SetBasicAuthentication(string username, string password)
        {
            byte[] userPassBytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password));
            string userPassBase64 = Convert.ToBase64String(userPassBytes);

            SetBasicAuthentication(userPassBase64);
        }

        public async Task<T> GetAsync<T>(string requestUrl)
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestUrl);

            string json = await httpResponse.Content.ReadAsStringAsync();

            T response = JsonConvert.DeserializeObject<T>(json);

            return response;
        }
    }
}
