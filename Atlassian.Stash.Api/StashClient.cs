using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Wrappers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api
{
    public class StashClient
    {
        private HttpClient _httpClient;

        public StashClient(string baseUrl, string base64Auth)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);

            SetBasicAuthentication(base64Auth);
        }

        public StashClient(string baseUrl, string username, string password)
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

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", userPassBase64);
        }

        public async Task<T> GetSingleTAsync<T>(params string[] inputs)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(RestUrlsMap.GetSingleTUrl<T>(inputs));

            string json = await response.Content.ReadAsStringAsync();

            var projectsResponse = JsonConvert.DeserializeObject<T>(json);

            return projectsResponse;
        }

        public async Task<ResponseWrapper<T>> GetManyTAsync<T>(params string[] inputs)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(RestUrlsMap.GetManyTUrl<T>(inputs));

            string json = await response.Content.ReadAsStringAsync();

            var projectsResponse = JsonConvert.DeserializeObject<ResponseWrapper<T>>(json);

            return projectsResponse;
        }
    }

    
}
