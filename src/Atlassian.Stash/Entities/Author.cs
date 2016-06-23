using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class Author
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public Link Link { get; set; }
        public Links Links { get; set; }
    }
}
