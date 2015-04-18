using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    // todo: Review class, since Create != Get != Delete objects
    public class Branch
    {
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public string LatestChangeset { get; set; }
        public bool IsDefault { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("startPoint")]
        public string StartPoint { get; set; }
        [JsonProperty("dryRun")]
        public bool? DryRun { get; set; }

    }
}
