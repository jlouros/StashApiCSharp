using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Api
{
    public class Repositories
    {
        private const string MANY_REPOSITORIES = "/rest/api/1.0/projects/{0}/repos";
        private const string ONE_REPOSITORY = "/rest/api/1.0/projects/{0}/repos/{1}";

        private HttpCommunicationWorker _httpWorker;

        internal Repositories(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }
        public async Task<ResponseWrapper<Repository>> GetAll(string projectKey, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_REPOSITORIES, requestOptions, projectKey);

            ResponseWrapper<Repository> response = await _httpWorker.GetAsync<ResponseWrapper<Repository>>(requestUrl);

            return response;
        }

        public async Task<Repository> GetById(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_REPOSITORY, null, projectKey, repositorySlug);

            Repository response = await _httpWorker.GetAsync<Repository>(requestUrl);

            return response;
        }
    }
}
