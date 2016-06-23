using Atlassian.Stash.Entities;
using Atlassian.Stash.Helpers;
using Atlassian.Stash.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api
{
    public class Groups
    {
        private const string MANY_GROUPS = "rest/api/1.0/admin/groups";

        private HttpCommunicationWorker _httpWorker;

        internal Groups(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Project>> Get(RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_GROUPS, requestOptions);

            ResponseWrapper<Project> response = await _httpWorker.GetAsync<ResponseWrapper<Project>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<Project>> Get(string filter, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_GROUPS + "?filter={0}", requestOptions, filter);

            ResponseWrapper<Project> response = await _httpWorker.GetAsync<ResponseWrapper<Project>>(requestUrl).ConfigureAwait(false);

            return response;
        }
    }
}
