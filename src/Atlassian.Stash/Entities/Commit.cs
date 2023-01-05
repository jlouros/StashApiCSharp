using Atlassian.Stash.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Atlassian.Stash.Entities
{
    public class Commit
    {
        public string Id { get; set; }

        public string DisplayId { get; set; }

        public Author Author { get; set; }

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime AuthorTimestamp { get; set; }

        public string Message { get; set; }

        public Commit[] Parents { get; set; }

        public Author Committer { get; set; }

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CommitterTimestamp { get; set; }

        public CommitProperties Properties { get; set; }
    }
}
