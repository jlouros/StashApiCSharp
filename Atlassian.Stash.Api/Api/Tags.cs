using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Api
{
    public class Tags
    {
        private const string MANY_TAGS = "/rest/api/1.0/projects/{0}/repos/{1}/tags";

        private HttpCommunicationWorker _httpWorker;

        internal Tags(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }
        public async Task<ResponseWrapper<Tags>> GetAll(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_TAGS, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<Tags> response = await _httpWorker.GetAsync<ResponseWrapper<Tags>>(requestUrl);

            return response;
        }
    }
}
