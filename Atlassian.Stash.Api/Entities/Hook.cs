using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    public class Hook
    {
        [JsonProperty("details")]
        public HookDetails Details { get; set; }
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
        [JsonProperty("configured")]
        public bool Configured { get; set; }
    }

    public class HookDetails
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("configFormKey")]
        public object ConfigFormKey { get; set; }
    }

}
