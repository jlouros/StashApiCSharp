using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Atlassian.Stash.Entities
{
    public class PullRequestSettings
    {
        [JsonProperty("mergeConfig")]
        public MergeConfig MergeConfig { get; set; }

        [JsonProperty("requiredAllApprovers")]
        public bool RequiredAllApprovers { get; set; }

        [JsonProperty("requiredAllTasksComplete")]
        public bool RequiredAllTasksComplete { get; set; }

        [JsonProperty("requiredApprovers")]
        public int RequiredApprovers { get; set; }

        [JsonProperty("requiredSuccessfulBuilds")]
        public int RequiredSuccessfulBuilds { get; set; }
    }

    public class MergeConfig
    {
        [JsonProperty("defaultStrategy")]
        public Strategy DefaultStrategy { get; set; }

        [JsonProperty("strategies")]
        public List<Strategy> Strategies { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Strategy
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StrategyIdType Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public enum StrategyIdType
    {
        [EnumMember(Value = "no-ff")]
        NO_FAST_FORWARD,
        [EnumMember(Value = "ff-only")]
        FAST_FORWARD_ONLY,
        [EnumMember(Value = "squash-ff-only")]
        SQUASH_FAST_FORWARD_ONLY,
        [EnumMember(Value = "ff")]
        FAST_FORWARD,
        [EnumMember(Value = "squash")]
        SQUASH,
    }

}
