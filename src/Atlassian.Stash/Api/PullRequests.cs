using System;
using Atlassian.Stash.Entities;
using Atlassian.Stash.Helpers;
using Atlassian.Stash.Workers;
using System.Threading.Tasks;
using Atlassian.Stash.Api.Entities;
using Newtonsoft.Json;
using Atlassian.Stash.Api.Exceptions;

namespace Atlassian.Stash.Api
{
    public class PullRequests
    {

        public enum Order
        {
            OLDEST,
            NEWEST
        }

        public enum Direction
        {
            INCOMING,
            OUTGOING
        }

        private const string PULL_REQUEST = "rest/api/1.0/projects/{0}/repos/{1}/pull-requests";
        private const string PULL_REQUEST_MERGEABLE = "rest/api/1.0/projects/{0}/repos/{1}/pull-requests/{2}/merge";
        private const string PULL_REQUEST_MERGE = "rest/api/1.0/projects/{0}/repos/{1}/pull-requests/{2}/merge?version={3}";

        private HttpCommunicationWorker _httpWorker;

        internal PullRequests(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<PullRequest> Create(string projectKey, string repositorySlug, PullRequest pullRequest)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULL_REQUEST, null, projectKey, repositorySlug);

            PullRequest pr = await _httpWorker.PostAsync(requestUrl, pullRequest);

            return pr;
        }

        public async Task<PullRequestStatus> Status(PullRequest pullRequest, string projectKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULL_REQUEST_MERGEABLE, null, projectKey,
                pullRequest.FromRef.Repository.Slug, pullRequest.Id);

            PullRequestStatus pr = await _httpWorker.GetAsync<PullRequestStatus>(requestUrl).ConfigureAwait(false);

            return pr;
        }

        public async Task<PullRequest> Merge(PullRequest pullRequest, string projectKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULL_REQUEST_MERGE, null, projectKey,
                pullRequest.FromRef.Repository.Slug, pullRequest.Id, pullRequest.Version);
            try
            { 
                PullRequest pr = await _httpWorker.PostAsync<PullRequest>(requestUrl,null).ConfigureAwait(false);
                return pr;
            }
            catch (Exception ex)
            {
                if (ex.Data["json"] != null)
                {
                    MergeErrorResponse error = JsonConvert.DeserializeObject<MergeErrorResponse>((string)ex.Data["json"]);
                    throw new StashMergeException("Merge failure",ex, error);
                }
                throw;
            }
        }

        public async Task<ResponseWrapper<PullRequest>> Get(string projectKey, string repositorySlug, RequestOptions options = null, Direction direction = Direction.INCOMING,
            PullRequestState state = PullRequestState.OPEN, bool withAttributes = true, bool withProperties = true)
        {
            string requestUrl = UrlBuilder.ToRestApiUrl(string.Format(PULL_REQUEST, projectKey, repositorySlug))
                                          .WithOptions(options)
                                          .WithQueryParam("direction", direction.ToString())
                                          .WithQueryParam("state", state.ToString())
                                          .WithQueryParam("withAttributes", withAttributes.ToString())
                                          .WithQueryParam("withProperties", withProperties.ToString());

            var pr = await _httpWorker.GetAsync<ResponseWrapper<PullRequest>>(requestUrl).ConfigureAwait(false);

            return pr;

        }

        public async Task<ResponseWrapper<PullRequest>> Get(string projectKey, string repositorySlug, Order order, RequestOptions options = null, Direction direction = Direction.INCOMING,
            PullRequestState state = PullRequestState.OPEN, bool withAttributes = true, bool withProperties = true)
        {
            string requestUrl = UrlBuilder.ToRestApiUrl(string.Format(PULL_REQUEST, projectKey, repositorySlug))
                                          .WithOptions(options)
                                          .WithQueryParam("direction", direction.ToString())
                                          .WithQueryParam("state", state.ToString())
                                          .WithQueryParam("withAttributes", withAttributes.ToString())
                                          .WithQueryParam("withProperties", withProperties.ToString())
                                          .WithQueryParam("order", order.ToString());

            var pr = await _httpWorker.GetAsync<ResponseWrapper<PullRequest>>(requestUrl).ConfigureAwait(false);

            return pr;

        }

    }
}
