using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Atlassian.Stash.Entities;
using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    public class PullRequestStatus
    {
        [JsonProperty("canMerge")]
        public bool CanMerge { get; set; }

        [JsonProperty("conflicted")]
        public bool RequiredAllApprovers { get; set; }

        [JsonProperty("outcome")]
        public string Outcome { get; set; }

        [JsonProperty("vetoes")]
        public IEnumerable<Vetoe> Vetoes { get; set; }
    }

    public class Vetoe
    {
        [JsonProperty("summaryMessage")]
        public string SummaryMessage { get; set; }

        [JsonProperty("detailedMessage")]
        public string DetailedMessage { get; set; }
    }
}
