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
                if (Group != null && User != null)
                {
                    return $"User: {User.Email} : {Group.Name} => {permission}";
                }

                if (Group == null)
                {
                    return $"User: {User.Name} : {User.Email} => {permission}";
                }

                if (User == null)
                {
                    return $"Group: {Group.Name} => {permission}";
                }

                return $"Permission: {permission}";
            }
        }
    }
}
