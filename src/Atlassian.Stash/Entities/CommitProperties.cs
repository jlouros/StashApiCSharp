
using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class CommitProperties
    {
        [JsonProperty("jira-key")]
        public string[] JiraKey { get; set; }

        public int CommentCount { get; set; }
    }
}
