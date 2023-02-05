using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Atlassian.Stash.Entities
{
    public class LabelRepository : Repository
    {
        [JsonProperty("labelableType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LabelableType LabelableType { get; set; }
    }

    public enum LabelableType
    {
        REPOSITORY
    }
}