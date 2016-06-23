using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class Permission
    {
        [JsonProperty("group")]
        public Group Group { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("permission")]
        public string permission { get; set; }
    }
}
