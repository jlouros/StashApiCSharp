using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class Fork
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("scmId")]
        public string ScmId { get; set; }
        public string State { get; set; }
        public string StatusMessage { get; set; }
        public bool Forkable { get; set; }
        public Repository Origin { get; set; }
        public Project Project { get; set; }
        public string CloneUrl { get; set; }
        public Link Link { get; set; }
        public Links Links { get; set; }
    }
}
