﻿using Atlassian.Stash.Entities;
using Atlassian.Stash.Helpers;
using Atlassian.Stash.Workers;
using System.Threading.Tasks;
using System;

namespace Atlassian.Stash.Api
{
    public class Repositories
    {
        private const string MANY_REPOSITORIES = "rest/api/1.0/projects/{0}/repos";
        private const string ONE_REPOSITORY = "rest/api/1.0/projects/{0}/repos/{1}";
        private const string MANY_TAGS = "rest/api/1.0/projects/{0}/repos/{1}/tags";
        private const string ONE_TAG = "rest/git/1.0/projects/{0}/repos/{1}/tags/{2}";
        private const string MANY_FILES = "rest/api/1.0/projects/{0}/repos/{1}/files";
        private const string MANY_FILES_FROM_PATH = "rest/api/1.0/projects/{0}/repos/{1}/files/{2}";
        private const string ONE_FILE = "rest/api/1.0/projects/{0}/repos/{1}/browse/{2}";
        private const string ONE_FILE_FROM_BRANCH = "rest/api/1.0/projects/{0}/repos/{1}/browse/{2}?at=refs%2Fheads%2F{3}";
        private const string MANY_HOOKS = "rest/api/1.0/projects/{0}/repos/{1}/settings/hooks";
        private const string PULLREQUEST_SETTINGS = "rest/api/1.0/projects/{0}/repos/{1}/settings/pull-requests";
        private const string ONE_HOOK = "rest/api/1.0/projects/{0}/repos/{1}/settings/hooks/{2}";
        private const string HOOK_ENABLE = "rest/api/1.0/projects/{0}/repos/{1}/settings/hooks/{2}/enabled";
        private const string HOOK_SETTINGS = "rest/api/1.0/projects/{0}/repos/{1}/settings/hooks/{2}/settings";
        private const string PERMISSION_GROUPS = ONE_REPOSITORY + "/permissions/groups";
        private const string PERMISSION_GRANT_GROUP = PERMISSION_GROUPS + "?permission={2}&name={3}";
        private const string PERMISSION_USERS = ONE_REPOSITORY + "/permissions/users";
        private const string PERMISSION_REVOKE_USER = PERMISSION_USERS + "?name={2}";
        private const string PERMISSION_GRANT_USER = PERMISSION_USERS + "?permission={2}&name={3}";
        private const string REPO_SIZES = "projects/{0}/repos/{1}/sizes";

        private HttpCommunicationWorker _httpWorker;

        internal Repositories(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<Sizes> GetSizes(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(REPO_SIZES, requestOptions, projectKey, repositorySlug);

            Sizes response = await _httpWorker.GetAsync<Sizes>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<Repository>> Get(string projectKey, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_REPOSITORIES, requestOptions, projectKey);

            ResponseWrapper<Repository> response = await _httpWorker.GetAsync<ResponseWrapper<Repository>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Repository> GetById(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_REPOSITORY, null, projectKey, repositorySlug);

            Repository response = await _httpWorker.GetAsync<Repository>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Repository> Create(string projectKey, Repository repository)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_REPOSITORIES, null, projectKey);

            Repository response = await _httpWorker.PostAsync<Repository>(requestUrl, repository).ConfigureAwait(false);

            return response;
        }

        public async Task Delete(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_REPOSITORY, null, projectKey, repositorySlug);

            await _httpWorker.DeleteAsync(requestUrl).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<Permission>> GetGroups(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PERMISSION_GROUPS, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<Permission> response = await _httpWorker.GetAsync<ResponseWrapper<Permission>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task GrantGroup(string projectKey, string repository, string group, RepositoryPermissions permission)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PERMISSION_GRANT_GROUP, null, projectKey, repository, permission.ToString(), group);

            await _httpWorker.PutAsync<Object>(requestUrl, new Object()).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<Permission>> GetUsers(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PERMISSION_USERS, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<Permission> response = await _httpWorker.GetAsync<ResponseWrapper<Permission>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task GrantUser(string projectKey, string repository, string group, RepositoryPermissions permission)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PERMISSION_GRANT_USER, null, projectKey, repository, permission.ToString(), group);

            await _httpWorker.PutAsync<Object>(requestUrl, new Object()).ConfigureAwait(false);
        }

        public async Task RevokeUser(string projectKey, string repository, string user)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PERMISSION_REVOKE_USER, null, projectKey, repository, user);

            await _httpWorker.DeleteAsync(requestUrl).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<Tag>> GetTags(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_TAGS, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<Tag> response = await _httpWorker.GetAsync<ResponseWrapper<Tag>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Tag> CreateTag(string projectKey, string repositorySlug, Tag tag)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_TAGS, null, projectKey, repositorySlug);

            Tag response = await _httpWorker.PostAsync<Tag>(requestUrl, tag, true).ConfigureAwait(false);

            return response;
        }

        public async Task DeleteTag(string projectKey, string repositorySlug, string tagName)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_TAG, null, projectKey, repositorySlug, tagName);

            await _httpWorker.DeleteAsync(requestUrl).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<string>> GetFiles(string projectKey, string repositorySlug, string path, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_FILES_FROM_PATH, false, requestOptions, projectKey, repositorySlug, path);

            ResponseWrapper<string> response = await _httpWorker.GetAsync<ResponseWrapper<string>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<string>> GetFiles(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_FILES, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<string> response = await _httpWorker.GetAsync<ResponseWrapper<string>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<File> GetFileContents(string projectKey, string repositorySlug, string path, RequestOptions requestOptions = null)
        {
            // must espace spaces, but can't escape slashes
            
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_FILE, false, requestOptions, projectKey, repositorySlug, path);
            File response = await _httpWorker.GetAsync<File>(requestUrl).ConfigureAwait(false);
            
            return response;
        }

        public async Task<File> GetFileContents(string projectKey, string repositorySlug, string path, FileContentsOptions requestOptions)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_FILE, false, requestOptions, projectKey, repositorySlug, path);
            File response = await _httpWorker.GetAsync<File>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<File> GetFileContents(string projectKey, string repositorySlug, string path, string branch, RequestOptions requestOptions = null)
        {
            // must escape spaces, but can't escape slashes
            
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_FILE_FROM_BRANCH, false, requestOptions, projectKey, repositorySlug, path, branch);
            File response = await _httpWorker.GetAsync<File>(requestUrl).ConfigureAwait(false);
            
            return response;
        }

        public async Task<File> GetFileContents(string projectKey, string repositorySlug, string path, string branch, FileContentsOptions requestOptions)
        {
            // must escape spaces, but can't escape slashes

            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_FILE_FROM_BRANCH, false, requestOptions, projectKey, repositorySlug, path, branch);
            File response = await _httpWorker.GetAsync<File>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<Hook>> GetHooks(string projectKey, string repositorySlug,RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_HOOKS, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<Hook> response = await _httpWorker.GetAsync<ResponseWrapper<Hook>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Hook> GetHookById(string projectKey, string repositorySlug, string hookKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_HOOK, null, projectKey, repositorySlug, hookKey);

            Hook response = await _httpWorker.GetAsync<Hook>(requestUrl).ConfigureAwait(false);

            return response;
        }

        // todo: extend with hook settings
        public async Task<Hook> EnableHook(string projectKey, string repositorySlug, string hookKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(HOOK_ENABLE, null, projectKey, repositorySlug, hookKey);

            Hook response = await _httpWorker.PutAsync<Hook>(requestUrl, null).ConfigureAwait(false);

            return response;
        }

        public async Task<Hook> DisableHook(string projectKey, string repositorySlug, string hookKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(HOOK_ENABLE, null, projectKey, repositorySlug, hookKey);

            Hook response = await _httpWorker.DeleteWithResponseContentAsync<Hook>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<string> ConfigureHook(string projectKey, string repositorySlug, string hookKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(HOOK_SETTINGS, null, projectKey, repositorySlug, hookKey);

            string response = await _httpWorker.GetAsync(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<string> ConfigureHook(string projectKey, string repositorySlug, string hookKey, string settings)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(HOOK_SETTINGS, null, projectKey, repositorySlug, hookKey);

            string response = await _httpWorker.PutAsync(requestUrl, settings).ConfigureAwait(false);

            return response;
        }

        public async Task<PullRequestSettings> GetPullRequestSettings(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULLREQUEST_SETTINGS, null, projectKey, repositorySlug);

            PullRequestSettings response = await _httpWorker.GetAsync<PullRequestSettings>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<PullRequestSettings> SetPullRequestSettings(string projectKey, string repositorySlug, PullRequestSettings settings)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(PULLREQUEST_SETTINGS, null, projectKey, repositorySlug);

            PullRequestSettings response = await _httpWorker.PostAsync<PullRequestSettings>(requestUrl, settings, true).ConfigureAwait(false);

            return response;
        }
    }

    public enum RepositoryPermissions
    {
        REPO_READ,
        REPO_WRITE,
        REPO_ADMIN
    }
}
