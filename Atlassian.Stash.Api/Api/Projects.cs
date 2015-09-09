using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;
using System;

namespace Atlassian.Stash.Api.Api
{
    public class Projects
    {
        private const string MANY_PROJECTS = "/rest/api/1.0/projects";
        private const string ONE_PROJECT = "/rest/api/1.0/projects/{0}";
        private const string GRANT_GROUP_PERMISSION = ONE_PROJECT + "/permissions/groups?permission={1}&name={2}";
        private const string REVOKE_USER_PERMISSION = ONE_PROJECT + "/permissions/users?name={1}";
        public string PERMISSION_GROUPS = ONE_PROJECT + "/permissions/groups";
        public string PERMISSION_USERS = ONE_PROJECT + "/permissions/users";

        private HttpCommunicationWorker _httpWorker;


        internal Projects(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Project>> Get(RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_PROJECTS, requestOptions);

            ResponseWrapper<Project> response = await _httpWorker.GetAsync<ResponseWrapper<Project>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Project> GetById(string projectKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_PROJECT, null, projectKey);

            Project response = await _httpWorker.GetAsync<Project>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Project> Create(Project project)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_PROJECTS);

            Project response = await _httpWorker.PostAsync<Project>(requestUrl, project).ConfigureAwait(false);

            return response;
        }

        public async Task Delete(string projectKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_PROJECT, null, projectKey);

            await _httpWorker.DeleteAsync(requestUrl).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<Permission>> GetGroups(string projectKey, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PERMISSION_GROUPS, requestOptions, projectKey);

            ResponseWrapper<Permission> response = await _httpWorker.GetAsync<ResponseWrapper<Permission>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task GrantGroup(string projectKey, string group, ProjectPermissions permission)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(GRANT_GROUP_PERMISSION, null, projectKey, permission.ToString(), group);

            await _httpWorker.PutAsync<Object>(requestUrl, new Object()).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<Permission>> GetUsers(string projectKey, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PERMISSION_USERS, requestOptions, projectKey);

            ResponseWrapper<Permission> response = await _httpWorker.GetAsync<ResponseWrapper<Permission>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task RevokeUser(string projectKey, string user)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(REVOKE_USER_PERMISSION, null, projectKey, user);

            await _httpWorker.DeleteAsync(requestUrl).ConfigureAwait(false);
        }
    }

    public enum ProjectPermissions
    {
        PROJECT_READ,
        PROJECT_WRITE,
        PROJECT_ADMIN
    }
}
