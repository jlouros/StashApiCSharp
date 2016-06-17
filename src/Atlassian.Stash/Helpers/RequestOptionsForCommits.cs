
namespace Atlassian.Stash.Helpers
{
    public class RequestOptionsForCommits
    {
        public string Path { get; set; }
        public string Since { get; set; }
        public string Until { get; set; }
        public bool WithCounts { get; set; }
    }
}
