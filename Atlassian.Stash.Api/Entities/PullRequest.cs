using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    public class PullRequest
    {
        public string Id { get; set; }
        public string Version { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
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
