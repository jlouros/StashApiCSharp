
namespace Atlassian.Stash.Api.Entities
{
    public class Project
    {
        public string Key { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool @Public { get; set; }
        public string Type { get; set; }
    }
}
