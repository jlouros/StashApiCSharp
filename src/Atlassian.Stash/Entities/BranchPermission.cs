using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Atlassian.Stash.Entities
{
    public class BranchPermission
    {
        [JsonProperty("id", NullValueHandling=NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BranchPermissionType Type { get; set; }

        [JsonProperty("matcher")]
        public BranchPermissionMatcher Matcher { get; set; }

        [JsonProperty("users", NullValueHandling = NullValueHandling.Ignore)]
        public List<User> Users { get; set; }

        [JsonProperty("groups")]
        public string[] Groups { get; set; }
    }

    public enum BranchPermissionType
    {
        [EnumMember(Value = "pull-request-only")]
        PULL_REQUEST_ONLY,
        [EnumMember(Value = "fast-forward-only")]
        FAST_FORWARD_ONLY,
        [EnumMember(Value = "no-deletes")]
        NO_DELETES,
        [EnumMember(Value = "read-only")]
        READ_ONLY,
    }

    public class BranchPermissionMatcher
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayId")]
        public string DisplayId { get; set; }

        [JsonProperty("type")]
        public BranchPermissionMatcherType Type { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }

    public class BranchPermissionMatcherType
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BranchPermissionMatcherTypeName Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public enum BranchPermissionMatcherTypeName
    {
        BRANCH,
        PATTERN,
        MODEL_CATEGORY,
        MODEL_BRANCH
    }
}