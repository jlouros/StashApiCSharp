using Atlassian.Stash.Entities;
using Atlassian.Stash.Helpers;
using Atlassian.Stash.Workers;
using System;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api
{
    public class Branches
    {
        private const string MANY_BRANCHES = "rest/api/1.0/projects/{0}/repos/{1}/branches";
        private const string BRANCHES_DEFAULT = "rest/api/1.0/projects/{0}/repos/{1}/branches/default";
        private const string MANY_BRANCHES_WITH_FILTER = "rest/api/1.0/projects/{0}/repos/{1}/branches?filterText={2}";
        private const string MANAGE_BRANCHES = "rest/branch-utils/1.0/projects/{0}/repos/{1}/branches";
        private const string BRANCHES_FOR_COMMIT = "rest/branch-utils/1.0/projects/{0}/repos/{1}/branches/info/{2}";
        private const string BRANCH_PERMISSIONS = "rest/branch-permissions/2.0/projects/{0}/repos/{1}/restrictions";
        private const string BRANCH_DELETE_PERMISSIONS = "rest/branch-permissions/2.0/projects/{0}/repos/{1}/restrictions/{2}";
        private const string AUTOMERGE_ENABLING = "rest/branch-utils/1.0/projects/{0}/repos/{1}/automerge/enabled";
        private const string BRANCHINGMODEL = "rest/branch-utils/1.0/projects/{0}/repos/{1}/branchmodel";
        private const string BRANCHINGMODEL_CONFIGURATION = BRANCHINGMODEL + "/configuration";

        private HttpCommunicationWorker _httpWorker;

        internal Branches(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Branch>> Get(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_BRANCHES, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<Branch> response = await _httpWorker.GetAsync<ResponseWrapper<Branch>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<Branch>> Get(string projectKey, string repositorySlug, string filterText, RequestOptions requestOptions = null) 
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_BRANCHES_WITH_FILTER, requestOptions, projectKey, repositorySlug, filterText);

            ResponseWrapper<Branch> response = await _httpWorker.GetAsync<ResponseWrapper<Branch>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<Branch>> GetByCommitId(string projectKey, string repositorySlug, string commitId, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(BRANCHES_FOR_COMMIT, requestOptions, projectKey, repositorySlug,
                commitId);

            ResponseWrapper<Branch> response = await _httpWorker.GetAsync<ResponseWrapper<Branch>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        // branch Utils API
        public async Task<Branch> Create(string projectKey, string repositorySlug, Branch branch)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANAGE_BRANCHES, null, projectKey, repositorySlug);

            Branch response = await _httpWorker.PostAsync(requestUrl, branch).ConfigureAwait(false);

            return response;
        }

        public async Task Delete(string projectKey, string repositorySlug, Branch branch)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANAGE_BRANCHES, null, projectKey, repositorySlug);

            await _httpWorker.DeleteWithRequestContentAsync(requestUrl, branch).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<BranchPermission>> GetPermissions(string projectKey, string repositorySlug, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(BRANCH_PERMISSIONS, requestOptions, projectKey, repositorySlug);

            ResponseWrapper<BranchPermission> response = await _httpWorker.GetAsync<ResponseWrapper<BranchPermission>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<BranchPermission> SetPermissions(string projectKey, string repositorySlug, BranchPermission permissions) 
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(BRANCH_PERMISSIONS, null, projectKey, repositorySlug);

            BranchPermission response = await _httpWorker.PostAsync(requestUrl, permissions).ConfigureAwait(false);

            return response;
        }

        public async Task DeletePermissions(string projectKey, string repositorySlug, int permissionsId)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(BRANCH_DELETE_PERMISSIONS, null, projectKey, repositorySlug, permissionsId.ToString());

            await _httpWorker.DeleteAsync(requestUrl).ConfigureAwait(false);
        }

        public async Task<Branch> GetDefault(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(BRANCHES_DEFAULT, null, projectKey, repositorySlug);

            Branch response = await _httpWorker.GetAsync<Branch>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<Branch> SetDefault(string projectKey, string repositorySlug, Branch branch)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(BRANCHES_DEFAULT, null, projectKey, repositorySlug);

            Branch response = await _httpWorker.PutAsync(requestUrl, branch).ConfigureAwait(false);

            return response;
        }

        public async Task DisableAutomerge(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(AUTOMERGE_ENABLING, null, projectKey, repositorySlug);

            await _httpWorker.DeleteAsync(requestUrl).ConfigureAwait(false);
        }
        public async Task EnableAutomerge(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(AUTOMERGE_ENABLING, null, projectKey, repositorySlug);

            await _httpWorker.PutAsync(requestUrl, string.Empty).ConfigureAwait(false);
        }

        public async Task<BranchModel> GetBranchingModel(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(BRANCHINGMODEL, null, projectKey, repositorySlug);

            BranchModel response = await _httpWorker.GetAsync<BranchModel>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<BranchModelConfiguration> GetBranchingModelConfiguration(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(BRANCHINGMODEL_CONFIGURATION, null, projectKey, repositorySlug);

            BranchModelConfiguration response = await _httpWorker.GetAsync<BranchModelConfiguration>(requestUrl).ConfigureAwait(false);

            return response;
        }
        public async Task<BranchModelConfiguration> SetBranchingModelConfiguration(string projectKey, string repositorySlug, BranchModelConfiguration configuration)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(BRANCHINGMODEL_CONFIGURATION, null, projectKey, repositorySlug);

            BranchModelConfiguration response = await _httpWorker.PutAsync<BranchModelConfiguration>(requestUrl, configuration).ConfigureAwait(false);

            return response;
        }
    }
}
