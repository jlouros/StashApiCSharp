using Atlassian.Stash.Entities;
using Atlassian.Stash.Helpers;
using Atlassian.Stash.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api
{
    public class Commits
    {
        private const string MANY_COMMITS = "rest/api/1.0/projects/{0}/repos/{1}/commits";
        private const string ONE_COMMIT = "rest/api/1.0/projects/{0}/repos/{1}/commits/{2}";
        private const string COMMITS_UNTIL = "rest/api/1.0/projects/{0}/repos/{1}/commits?until={2}";
        private const string COMMITS_UNTIL_AND_SINCE = "rest/api/1.0/projects/{0}/repos/{1}/commits?until={2}&since={3}";
        private const string CHANGES_UNTIL = "rest/api/1.0/projects/{0}/repos/{1}/changes?until={2}";
        private const string CHANGES_UNTIL_AND_SINCE = "rest/api/1.0/projects/{0}/repos/{1}/changes?until={2}&since={3}";

        private HttpCommunicationWorker _httpWorker;

        internal Commits(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        /// <summary>
        /// Get one or many commits
        /// </summary>
        public async Task<ResponseWrapper<Commit>> Get(string projectKey, string repositorySlug, RequestOptions requestOptions = null, RequestOptionsForCommits commitRequestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrlWithCommitOptions(MANY_COMMITS, requestOptions, commitRequestOptions, projectKey, repositorySlug);

            ResponseWrapper<Commit> response = await _httpWorker.GetAsync<ResponseWrapper<Commit>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Commit> GetById(string projectKey, string repositorySlug, string commitId)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_COMMIT, null, projectKey, repositorySlug, commitId);

            Commit response = await _httpWorker.GetAsync<Commit>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Changes> GetChanges(string projectKey, string repositorySlug, string untilCommit, string sinceCommit = null, RequestOptions requestOptions = null)
        {
            string requestUrl = "";

            if (string.IsNullOrWhiteSpace(sinceCommit))
                requestUrl = UrlBuilder.FormatRestApiUrl(CHANGES_UNTIL, requestOptions, projectKey, repositorySlug, untilCommit);
            else
                requestUrl = UrlBuilder.FormatRestApiUrl(CHANGES_UNTIL_AND_SINCE, requestOptions, projectKey, repositorySlug, untilCommit, sinceCommit);

            Changes response = await _httpWorker.GetAsync<Changes>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<Commit>> GetCommits(string projectKey, string repositorySlug, string untilCommit, string sinceCommit = null, RequestOptions requestOptions = null) {
            string requestUrl = "";

            if (string.IsNullOrWhiteSpace(sinceCommit))
                requestUrl = UrlBuilder.FormatRestApiUrl(COMMITS_UNTIL, requestOptions, projectKey, repositorySlug, untilCommit);
            else
                requestUrl = UrlBuilder.FormatRestApiUrl(COMMITS_UNTIL_AND_SINCE, requestOptions, projectKey, repositorySlug, untilCommit, sinceCommit);

            ResponseWrapper<Commit> response = await _httpWorker.GetAsync<ResponseWrapper<Commit>>(requestUrl).ConfigureAwait(false);

            return response;
        }
    }
}
