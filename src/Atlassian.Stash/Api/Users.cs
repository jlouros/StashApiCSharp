using Atlassian.Stash.Entities;
using Atlassian.Stash.Helpers;
using Atlassian.Stash.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api
{
    public class Users
    {
        private const string ADMIN_USERS = "/rest/api/1.0/admin/users";
        private const string GET_USERS = ADMIN_USERS + "?filter={0}";
        private const string CREATE_USER_SILENT = ADMIN_USERS + "?name={0}&displayName={1}&emailAddress={2}&addToDefaultGroup=false&notify=false&password={3}";
        //private const string CREATE_USER_NOTIFY = ADMIN_USERS + "?name={0}&displayName={1}&emailAddress={2}&addToDefaultGroup=false&notify=true";
        private const string DELETE_USER = ADMIN_USERS + "?name={0}";

        private HttpCommunicationWorker _httpWorker;

        internal Users(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        
        public async Task<ResponseWrapper<User>> Get(string filter, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(GET_USERS, requestOptions, filter);

            ResponseWrapper<User> response = await _httpWorker.GetAsync<ResponseWrapper<User>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task Create(string name, string displayName, string emailAddress, string password)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(CREATE_USER_SILENT, null, name, displayName, emailAddress, password);

            await _httpWorker.PostAsync<string>(requestUrl, null).ConfigureAwait(false);
        }

        public async Task<User> Delete(string name)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(DELETE_USER, null, name);

            User response = await _httpWorker.DeleteWithResponseContentAsync<User>(requestUrl).ConfigureAwait(false);
            return response;
        }
    }
}
