using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class BranchModelConfigurationItem
    {
        [JsonProperty("refId")]
        public string RefId { get; set; }
        [JsonProperty("useDefault")]
        public bool UseDefault { get; set; }

    }
}
