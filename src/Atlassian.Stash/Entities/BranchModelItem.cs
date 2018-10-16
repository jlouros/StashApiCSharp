using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class BranchModelItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        [JsonProperty("prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string Prefix { get; set; }
    }
}
