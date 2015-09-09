using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Stash.Api.Entities
{
    public class Permission
    {
        [JsonProperty("group")]
        public Group Group { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("permission")]
        public string permission { get; set; }
    }
}
