using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class Label
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}