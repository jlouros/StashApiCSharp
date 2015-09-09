using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    public class Group
    {
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
