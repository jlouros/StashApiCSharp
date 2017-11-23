using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    public class MergeErrorResponse
    {
        [JsonProperty("errors")]
        public IEnumerable<MergeError> Errors { get; set; }
    }

    public class MergeError
    {
        [JsonProperty("context")]
        public string Context { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("exceptionName")]
        public string ExceptionName { get; set; }
        [JsonProperty("conflicted")]
        public bool Conflicted { get; set; }
        [JsonProperty("vetoes")]
        public IEnumerable<Vetoe> Vetoes { get; set; }
    }
}
