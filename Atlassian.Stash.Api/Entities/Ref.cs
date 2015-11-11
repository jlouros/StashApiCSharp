using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    public class Ref
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public string LatestChangeset { get; set; }
        [JsonProperty("repository")]
        public Repository Repository { get; set; }
    }
}
