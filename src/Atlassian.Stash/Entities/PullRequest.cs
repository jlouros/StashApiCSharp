using System;
using Atlassian.Stash.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Atlassian.Stash.Entities
{
    public enum PullRequestState
    {
        OPEN,
        DECLINED,
        MERGED,
        ALL
    }

    public class PullRequest
    {
        public string Id { get; set; }

        public string Version { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PullRequestState State { get; set; }

        public bool Open { get; set; }

        public bool Closed { get; set; }

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedDate { get; set; }

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdatedDate { get; set; }

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ClosedDate { get; set; }

        public Ref FromRef { get; set; }

        public Ref ToRef { get; set; }

        public bool Locked { get; set; }

        public AuthorWrapper Author { get; set; }

        public AuthorWrapper[] Reviewers { get; set; }

        public List<AuthorWrapper> Participants { get; set; }

        public Link Link { get; set; }

        public Links Links { get; set; }
    }
}
