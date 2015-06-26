using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    public class AuthorWrapper
    {
        [JsonProperty("approved")]
        public bool Approved { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("user")]
        public Author User { get; set; }
    }
}
