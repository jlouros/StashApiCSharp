using Atlassian.Stash.Entities;
using Atlassian.Stash.Helpers;
using Atlassian.Stash.Workers;
using System.Threading.Tasks;

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
