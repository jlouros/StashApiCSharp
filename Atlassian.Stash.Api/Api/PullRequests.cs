using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;

namespace Atlassian.Stash.Api.Api
{
    public class PullRequests
    {
        private const string PULL_REQUEST = "/rest/api/1.0/projects/{0}/repos/{1}/pull-requests";
        private HttpCommunicationWorker _httpWorker;

        internal PullRequests(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<PullRequest> Create(string projectKey, string repositorySlug,
            PullRequest pullRequest)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULL_REQUEST, null, projectKey, repositorySlug);

            PullRequest pr = await _httpWorker.PostAsync(requestUrl, pullRequest);

            return pr;
        }
    }
}
