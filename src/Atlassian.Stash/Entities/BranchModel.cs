using Newtonsoft.Json;
using System.Collections.Generic;

namespace Atlassian.Stash.Entities
{
    public class BranchModel
    {
        [JsonProperty("development", NullValueHandling = NullValueHandling.Ignore)]
        public Branch Development { get; set; }
        [JsonProperty("production", NullValueHandling = NullValueHandling.Ignore)]
        public Branch Production { get; set; }
        [JsonProperty("types", NullValueHandling = NullValueHandling.Ignore)]
        public List<BranchModelItem> Types { get; set; }
    }
}
