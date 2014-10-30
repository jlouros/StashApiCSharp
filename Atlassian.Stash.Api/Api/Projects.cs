using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Atlassian.Stash.Api.Workers;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Api
{
    public class Projects
    {
        private const string MANY_PROJECTS = "/rest/api/1.0/projects";
        private const string ONE_PROJECT = "/rest/api/1.0/projects/{0}";

        private HttpCommunicationWorker _httpWorker;

        internal Projects(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Project>> Get(RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_PROJECTS, requestOptions);

            ResponseWrapper<Project> response = await _httpWorker.GetAsync<ResponseWrapper<Project>>(requestUrl);

            return response;
        }

        public async Task<Project> GetById(string projectKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_PROJECT, null, projectKey);

            Project response = await _httpWorker.GetAsync<Project>(requestUrl);

            return response;
        }

        public async Task<Project> Create(Project project)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_PROJECTS);

            Project response = await _httpWorker.PostAsync<Project>(requestUrl, project);

            return response;
        }

        public async Task Delete(string projectKey)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_PROJECT, null, projectKey);

            await _httpWorker.DeleteAsync(requestUrl);
        }
    }
}
