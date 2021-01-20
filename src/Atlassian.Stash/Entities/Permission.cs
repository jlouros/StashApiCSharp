using System.Diagnostics;

using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Permission
    {
        [JsonProperty("group")]
        public Group Group { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("permission")]
        public string permission { get; set; }

        private string DebuggerDisplay
        {
            get
            {
                if (this.Group != null && this.User != null)
                {
                    return $"User: {this.User.Email} : {this.Group.Name} => {this.permission}";
                }

                if (this.Group == null)
                {
                    return $"User: {this.User.Name} : {this.User.Email} => {this.permission}";
                }

                if (this.User == null)
                {
                    return $"Group: {this.Group.Name} => {this.permission}";
                }

                return $"Permission: {this.permission}";

            }
        }
    }
}
