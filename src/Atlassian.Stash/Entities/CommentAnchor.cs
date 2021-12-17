
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Atlassian.Stash.Entities
{
    public enum DiffType
    {
        NONE,
        COMMIT,
        EFFECTIVE,
        RANGE,
    }

    public enum FileType
    {
        NONE,
        FROM,
        TO
    }

    public enum LineType
    {
        NONE,
        ADDED,
        REMOVED,
        CONTEXT
    }

    public class CommentAnchor
    {
        public string FromHash { get; set; }

        public string ToHash { get; set; }

        public int Line { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LineType LineType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FileType FileType { get; set; }

        public string Path { get; set; }

        public string Srcpath { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DiffType DiffType { get; set; }

        public bool Orphaned { get; set; }
    }
}
