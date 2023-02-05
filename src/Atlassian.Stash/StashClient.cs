using Atlassian.Stash.Api;
using Atlassian.Stash.Workers;

namespace Atlassian.Stash
{
    public class StashClient
    {
        private HttpCommunicationWorker _httpWorker;

        public StashClient(string baseUrl, string base64Auth = null, bool usePersonalAccessTokenForAuthentication = false)
        {
	        var schemeToUse = usePersonalAccessTokenForAuthentication
		        ? Api.Enums.AuthScheme.Bearer 
		        : Api.Enums.AuthScheme.Basic;

            _httpWorker = new HttpCommunicationWorker(baseUrl, base64Auth, schemeToUse);
            InjectDependencies();
        }

        public StashClient(string baseUrl, string username, string password)
        {
            _httpWorker = new HttpCommunicationWorker(baseUrl, username, password);
            InjectDependencies();
        }

        private void InjectDependencies()
        {
            this.Projects = new Projects(_httpWorker);
            this.Groups = new Groups(_httpWorker);
            this.Users = new Users(_httpWorker);
            this.Repositories = new Repositories(_httpWorker);
            this.Branches = new Branches(_httpWorker);
            this.Commits = new Commits(_httpWorker);
            this.PullRequests = new PullRequests(_httpWorker);
            this.Forks = new Forks(_httpWorker);
            this.Labels = new Labels(_httpWorker);
        }

        public void SetBasicAuthentication(string username, string password)
        {
            _httpWorker.SetBasicAuthentication(username, password);
        }

        public Projects Projects { get; private set; }
        public Groups Groups { get; set; }
        public Users Users { get; private set; }
        public Repositories Repositories { get; private set; }
        public Branches Branches { get; private set; }
        public Commits Commits { get; private set; }
        public PullRequests PullRequests { get; private set; }
        public Forks Forks { get; private set; }
        public Labels Labels { get; private set; }

    }
}
