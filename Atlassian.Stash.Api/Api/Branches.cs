using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Api
{
    public class Branches
    {
        private const string MANY_BRANCHES = "/rest/api/1.0/projects/{0}/repos/{1}/branches";
        private const string MANAGE_BRANCHES = "/rest/branch-utils/1.0/projects/{0}/repos/{1}/branches";
        private const string BRANCHES_FOR_COMMIT = "/rest/branch-utils/1.0/projects/{0}/repos/{1}/branches/info/{2}";
        private const string BRANCH_PERMISSIONS = "/rest/branch-permissions/1.0/projects/{0}/repos/{1}/restricted";
        private const string BRANCH_DELETE_PERMISSIONS = "/rest/branch-permissions/1.0/projects/{0}/repos/{1}/restricted/{2}";

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
    }
}
