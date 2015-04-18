using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Atlassian.Stash.Api.Entities
{
    public class BranchPermission
    {
        // todo: review this is a enum of 'BRANCH' 'PATTERN' 
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BranchPermissionType Type { get; set; }
        [JsonProperty("users")]
        public string[] Users { get; set; }
        [JsonProperty("groups")]
        public string[] Groups { get; set; }

        //todo: review response has this props
        [JsonProperty("Id")]
        public int Id { get; set; }
        //public Branch branch { get; set; }
    }

    public enum BranchPermissionType
    {
        BRANCH,
        PATTERN
    }
}
