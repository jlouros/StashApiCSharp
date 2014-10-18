using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Wrappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(RestUrlsMap.GetProjectsUrl());

            string json = await response.Content.ReadAsStringAsync();

            var projectsResponse = JsonConvert.DeserializeObject<ResponseWrapper<Project>>(json);

            return projectsResponse.Values;
        }

        public async Task<IEnumerable<T>> GetTAsync<T>(params string[] inputs)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(RestUrlsMap.GetTUrl<T>(inputs));

            string json = await response.Content.ReadAsStringAsync();

            var projectsResponse = JsonConvert.DeserializeObject<ResponseWrapper<T>>(json);

            return projectsResponse.Values;
            
        }

    }

    
}
