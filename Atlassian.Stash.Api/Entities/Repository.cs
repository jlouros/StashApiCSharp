
namespace Atlassian.Stash.Api.Entities
{
    public class Repository
    {
        public string Slug { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScmId { get; set; }
        public string State { get; set; }
        public string StatusMessage { get; set; }
        public bool Forkable { get; set; }
        public bool @Public { get; set; }
        public string CloneUrl { get; set; }
        public Link Link { get; set; }
        public Links Links { get; set; }
        public Project Project { get; set; }
    }
}
