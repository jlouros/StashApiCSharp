using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Api
{
    public class Users
    {
        private const string ADMIN_USERS = "/rest/api/1.0/admin/users";
        private const string CREATE_USER_SILENT = ADMIN_USERS + "?name={0}&displayName={1}&emailAddress={2}&addToDefaultGroup=false&notify=false&password={3}";
        private const string CREATE_USER_NOTIFY = ADMIN_USERS + "?name={0}&displayName={1}&emailAddress={2}&addToDefaultGroup=false&notify=true";

        private HttpCommunicationWorker _httpWorker;

        internal Users(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        /*
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
        }*/

        public async Task<bool> Create(string name, string displayName, string emailAddress, string password)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(CREATE_USER_SILENT, null, name, displayName, emailAddress, password);

            string response = await _httpWorker.PostAsync<string>(requestUrl, null).ConfigureAwait(false);
            return string.IsNullOrWhiteSpace(response);
        }

        public async Task<bool> Create(string name, string displayName, string emailAddress)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(CREATE_USER_NOTIFY, null, name, displayName, emailAddress);

            string response = await _httpWorker.PostAsync<string>(requestUrl, null).ConfigureAwait(false);
            return string.IsNullOrWhiteSpace(response);
        }
    }
}
