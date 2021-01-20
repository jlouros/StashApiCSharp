﻿using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("emailAddress")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("lastAuthenticationTimestamp")]
        public long LastAuthenticationTimestamp { get; set; }
    }
}
