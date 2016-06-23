using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class Group
    {
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
