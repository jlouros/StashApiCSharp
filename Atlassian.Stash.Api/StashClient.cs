using Atlassian.Stash.Api.Api;
using Atlassian.Stash.Api.Workers;

namespace Atlassian.Stash.Api
{
    public class StashClient
    {
        private HttpCommunicationWorker _httpWorker;

        public StashClient(string baseUrl, string base64Auth)
        {
            _httpWorker = new HttpCommunicationWorker(baseUrl, base64Auth);
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
            this.Repositories = new Repositories(_httpWorker);
            this.Branches = new Branches(_httpWorker);
            this.Commits = new Commits(_httpWorker);
            this.PullRequests = new PullRequests(_httpWorker);
        }

        public Projects Projects { get; private set; }
        public Groups Groups { get; set; }
        public Repositories Repositories { get; private set; }
        public Branches Branches { get; private set; }
        public Commits Commits { get; private set; }
        public PullRequests PullRequests { get; private set; }

    }
}
