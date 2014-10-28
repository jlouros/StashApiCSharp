using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Api
{
    public class Commits
    {
        private const string MANY_COMMITS = "/rest/api/1.0/projects/{0}/repos/{1}/commits";
        private const string ONE_COMMIT = "/rest/api/1.0/projects/{0}/repos/{1}/commits/{2}";
        
        private HttpCommunicationWorker _httpWorker;

        internal Commits(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Commit>> GetAll(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_COMMITS, null, projectKey, repositorySlug);

            ResponseWrapper<Commit> response = await _httpWorker.GetAsync<ResponseWrapper<Commit>>(requestUrl);

            return response;
        }

        public async Task<Commit> GetById(string projectKey, string repositorySlug, string commitId)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_COMMITS, null, projectKey, repositorySlug, commitId);

            Commit response = await _httpWorker.GetAsync<Commit>(requestUrl);

            return response;
        }
    }
}
