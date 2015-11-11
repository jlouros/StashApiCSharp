
namespace Atlassian.Stash.Api.Helpers
{
    public class RequestOptionsForCommits
    {
        public string Path { get; set; }
        public string Since { get; set; }
        public string Until { get; set; }
        public bool WithCounts { get; set; }
    }
}
