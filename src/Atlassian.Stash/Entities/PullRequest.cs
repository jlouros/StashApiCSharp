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

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PullRequestState State { get; set; }
        [JsonProperty("open")]
        public bool Open { get; set; }
        [JsonProperty("closed")]
        public bool Closed { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        [JsonProperty("fromRef")]
        public Ref FromRef { get; set; }
        [JsonProperty("toRef")]
        public Ref ToRef { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        public AuthorWrapper Author { get; set; }
        [JsonProperty("reviewers")]
        public AuthorWrapper[] Reviewers { get; set; }
        public List<AuthorWrapper> Participants { get; set; }

        public Link Link { get; set; }
        public Links Links { get; set; }
    }
}
