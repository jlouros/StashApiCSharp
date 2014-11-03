using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Api
{
    public class Branches
    {
        private const string MANY_BRANCHES = "/rest/api/1.0/projects/{0}/repos/{1}/branches";

        private const string MANAGE_BRANCHES = "/rest/branch-utils/1.0/projects/{0}/repos/{1}/branches";

        private HttpCommunicationWorker _httpWorker;

        internal Branches(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Branch>> GetAll(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_BRANCHES, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<Branch> response = await _httpWorker.GetAsync<ResponseWrapper<Branch>>(requestUrl);

            return response;
        }

        // branch Utils API
        public async Task<Branch> Create(string projectKey, string repositorySlug, Branch branch)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANAGE_BRANCHES, null, projectKey, repositorySlug);

            Branch response = await _httpWorker.PostAsync(requestUrl, branch);

            return response;
        }

        public async Task Delete(string projectKey, string repositorySlug, Branch branch)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANAGE_BRANCHES, null, projectKey, repositorySlug);

            await _httpWorker.DeleteAsyncWithJsonContent(requestUrl, branch);
        }
    }
}
