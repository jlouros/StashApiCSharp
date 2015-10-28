using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Api
{
    public class Forks
    {
        private const string MANY_FORKS = "/rest/api/1.0/projects/{0}/repos/{1}/forks";

        private HttpCommunicationWorker _httpWorker;

        internal Forks(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Fork>> Get(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_FORKS, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<Fork> response = await _httpWorker.GetAsync<ResponseWrapper<Fork>>(requestUrl).ConfigureAwait(false);

            return response;
        }
    }
}
