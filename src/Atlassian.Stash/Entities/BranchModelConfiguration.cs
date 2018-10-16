using Newtonsoft.Json;
using System.Collections.Generic;

namespace Atlassian.Stash.Entities
{
    public class BranchModelConfiguration
    {
        [JsonProperty("development", NullValueHandling = NullValueHandling.Ignore)]
        public BranchModelConfigurationItem Development { get; set; }
        [JsonProperty("production", NullValueHandling = NullValueHandling.Ignore)]
        public BranchModelConfigurationItem Production { get; set; }

        [JsonProperty("types", NullValueHandling = NullValueHandling.Ignore)]
        public IList<BranchPrefix> Types { get; set; }
    }
}
