using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Api
{
    public class Branches
    {
        private const string MANY_BRANCHES = "/rest/api/1.0/projects/{0}/repos/{1}/branches";
        
        private HttpCommunicationWorker _httpWorker;

        internal Branches(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Branch>> GetAll(string projectKey, string repositorySlug)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_BRANCHES, null, projectKey, repositorySlug);

            ResponseWrapper<Branch> response = await _httpWorker.GetAsync<ResponseWrapper<Branch>>(requestUrl);

            return response;
        }
    }
}
