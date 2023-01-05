using Atlassian.Stash.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Atlassian.Stash.Entities
{
    public enum TaskSeverity
    {
        NONE,
        NORMAL,
        BLOCKER,
    }

    public enum TaskState
    {
        NONE,
        OPEN,
        RESOLVED,
    }

    public class Comment
    {
        public CommentProperties Properties { get; set; }

        public string Id { get; set; }
        
        public string Version { get; set; }

        public string Type { get; set; }

        public string Text { get; set; }

        public Author Author { get; set; }

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedDate { get; set; }

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdatedDate { get; set; }

        public Comment[] Comments { get; set; }

        public Comment[] Tasks { get; set; }

        public PermittedOperations PermittedOperations { get; set; }

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ResolvedDate { get; set; }

        public Author Resolver { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TaskSeverity Severity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TaskState State { get; set; }

        public Comment Anchor { get; set; }
    }
}
