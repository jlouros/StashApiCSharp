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
        private const string PULL_REQUEST_UPDATE = "rest/api/1.0/projects/{0}/repos/{1}/pull-requests/{2}";
        private const string PULL_REQUEST_MERGEABLE = "rest/api/1.0/projects/{0}/repos/{1}/pull-requests/{2}/merge";
        private const string PULL_REQUEST_MERGE = "rest/api/1.0/projects/{0}/repos/{1}/pull-requests/{2}/merge?version={3}";
        private const string PULL_REQUEST_APPROVE = "rest/api/1.0/projects/{0}/repos/{1}/pull-requests/{2}/approve?version={3}";
        private const string PULL_REQUEST_DECLINE = "rest/api/1.0/projects/{0}/repos/{1}/pull-requests/{2}/decline?version={3}";

        private HttpCommunicationWorker _httpWorker;

        internal PullRequests(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        /// <summary>
        /// Create a new pull request between two branches.
        /// The branches may be in the same repository, or different ones.
        /// When using different repositories, they must still be in the same {@link Repository#getHierarchyId() hierarchy}.
        /// </summary>
        /// <param name="projectKey"></param>
        /// <param name="repositorySlug"></param>
        /// <param name="pullRequest"></param>
        /// <returns>Newly created pull request</returns>
        public async Task<PullRequest> Create(string projectKey, string repositorySlug, PullRequest pullRequest)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULL_REQUEST, null, projectKey, repositorySlug);

            PullRequest pr = await _httpWorker.PostAsync(requestUrl, pullRequest).ConfigureAwait(false);

            return pr;
        }

        /// <summary>
        /// Test whether a pull request can be merged.
        /// </summary>
        /// <param name="pullRequest"></param>
        /// <param name="projectKey"></param>
        /// <returns>Status of the pull request</returns>
        public async Task<PullRequestStatus> Status(PullRequest pullRequest, string projectKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULL_REQUEST_MERGEABLE, null, projectKey,
                pullRequest.FromRef.Repository.Slug, pullRequest.Id);

            PullRequestStatus pr = await _httpWorker.GetAsync<PullRequestStatus>(requestUrl).ConfigureAwait(false);

            return pr;
        }

        /// <summary>
        /// Approve a pull request as the current user. Implicitly adds the user as a participant if they are not already.
        /// </summary>
        /// <param name="pullRequest"></param>
        /// <param name="projectKey"></param>
        /// <returns>Approved pull request status</returns>
        public async Task<PullRequestReviewed> Approve(PullRequest pullRequest, string projectKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULL_REQUEST_APPROVE, null, projectKey,
                pullRequest.FromRef.Repository.Slug, pullRequest.Id, pullRequest.Version);

            PullRequestReviewed approvedPullRequest = await _httpWorker.PostAsync<PullRequestReviewed>(requestUrl, null).ConfigureAwait(false);

            return approvedPullRequest;
        }

        /// <summary>
        /// Decline a pull request.
        /// </summary>
        /// <param name="pullRequest"></param>
        /// <param name="projectKey"></param>
        /// <returns>Declined pull request</returns>
        public async Task<PullRequestReviewed> Decline(PullRequest pullRequest, string projectKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULL_REQUEST_DECLINE, null, projectKey,
                pullRequest.FromRef.Repository.Slug, pullRequest.Id, pullRequest.Version);

            PullRequestReviewed declinedPullRequest = await _httpWorker.PostAsync<PullRequestReviewed>(requestUrl, null).ConfigureAwait(false);

            return declinedPullRequest;
        }

        /// <summary>
        /// Update the title, description, reviewers or destination branch of an existing pull request.
        /// </summary>
        /// <param name="pullRequest"></param>
        /// <param name="projectKey"></param>
        /// <returns>Updated pull request</returns>
        public async Task<PullRequest> Update(PullRequest pullRequest, string projectKey, string slug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULL_REQUEST_UPDATE, null, projectKey,
                slug, pullRequest.Id);

            PullRequest updatedPullRequest = await _httpWorker.PutAsync(requestUrl, pullRequest, true).ConfigureAwait(false);

            return updatedPullRequest;
        }

        /// <summary>
        /// Merge the specified pull request.
        /// </summary>
        /// <param name="pullRequest"></param>
        /// <param name="projectKey"></param>
        /// <returns>Merged pull request</returns>
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

        /// <summary>
        /// Retrieve a page of pull requests to or from the specified repository.
        /// </summary>
        /// <param name="projectKey"></param>
        /// <param name="repositorySlug"></param>
        /// <param name="options"></param>
        /// <param name="direction"></param>
        /// <param name="state"></param>
        /// <param name="withAttributes"></param>
        /// <param name="withProperties"></param>
        /// <returns>List of pull requests</returns>
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

        /// <summary>
        /// Retrieve a page of pull requests to or from the specified repository.
        /// </summary>
        /// <param name="projectKey"></param>
        /// <param name="repositorySlug"></param>
        /// <param name="order"></param>
        /// <param name="options"></param>
        /// <param name="direction"></param>
        /// <param name="state"></param>
        /// <param name="withAttributes"></param>
        /// <param name="withProperties"></param>
        /// <returns>List of ordered pull requests</returns>
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
