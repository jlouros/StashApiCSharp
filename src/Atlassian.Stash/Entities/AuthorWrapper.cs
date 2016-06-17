using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class AuthorWrapper
    {
        [JsonProperty("approved")]
        public bool Approved { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("user")]
        public Author User { get; set; }
    }
}
