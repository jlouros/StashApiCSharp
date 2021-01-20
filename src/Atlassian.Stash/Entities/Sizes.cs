using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class Sizes
    {
        [JsonProperty("repository")]
        public long Repository { get; set; }

        [JsonProperty("attachments")]
        public long Attachments { get; set; }
    }
}
