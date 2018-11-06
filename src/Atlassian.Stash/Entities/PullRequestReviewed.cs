using Atlassian.Stash.Entities;
using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    public class PullRequestReviewed
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("lastReviewedCommit")]
        public string LastReviewedCommit { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("approved")]
        public bool Approved { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
