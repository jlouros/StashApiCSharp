using Atlassian.Stash.Entities;
using Atlassian.Stash.Helpers;
using Atlassian.Stash.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api
{
    public class Groups
    {
        private const string MANY_GROUPS = "rest/api/1.0/admin/groups";
        private const string USERS_IN_GROUP = "rest/api/1.0/admin/groups/more-members?context={0}";
        private const string SINGLE_GROUP = MANY_GROUPS + "?name={0}";

        private HttpCommunicationWorker _httpWorker;

        internal Groups(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Group>> Get(RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_GROUPS, requestOptions);

            ResponseWrapper<Group> response = await _httpWorker.GetAsync<ResponseWrapper<Group>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<Group>> Get(string filter, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_GROUPS + "?filter={0}", requestOptions, filter);

            ResponseWrapper<Group> response = await _httpWorker.GetAsync<ResponseWrapper<Group>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<User>> GetUsers(string name, RequestOptions requestOptions = null)
        {
            var requestUrl = UrlBuilder.FormatRestApiUrl(USERS_IN_GROUP, requestOptions, name);

            var response = await _httpWorker.GetAsync<ResponseWrapper<User>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Group> Create(string name)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(SINGLE_GROUP, null, name);

            Group response = await _httpWorker.PostAsync<Group>(requestUrl, null).ConfigureAwait(false);

            return response;
        }

        public async Task Delete(string name)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(SINGLE_GROUP, null, name);

            await _httpWorker.DeleteAsync(requestUrl).ConfigureAwait(false);
            
        }
    }
}
