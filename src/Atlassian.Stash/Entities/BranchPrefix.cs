using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class BranchPrefix
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool Enabled { get; set; }

        [JsonProperty("prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string Prefix { get; set; }
    }
}
