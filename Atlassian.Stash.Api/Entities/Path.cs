
namespace Atlassian.Stash.Api.Entities
{
    public class Path
    {
        public string[] Components { get; set; }
        public string Parent { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string ToString { get; set; }
    }
}
