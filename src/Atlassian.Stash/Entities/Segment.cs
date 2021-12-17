
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Atlassian.Stash.Entities
{
    public enum ContextType
    {
        NONE,
        CONTEXT,
        ADDED,
        REMOVED
    }

    public class Segment
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ContextType Type { get; set; }

        public SegmentLine[] Lines { get; set; }

        public bool Truncated { get; set; }
    }
}
