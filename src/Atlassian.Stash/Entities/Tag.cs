using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Atlassian.Stash.Entities
{
    public class Tag
    {
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public string LatestChangeset { get; set; }
        public string Hash { get; set; }

        // used for "create"
        [JsonProperty("force")]
        public bool Force { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Either a <see cref="Commit.Id"/> or a <see cref="Branch.Id"/>
        /// </summary>
        [JsonProperty("startPoint")]
        public string StartPoint { get; set; } 

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(TagTypeEnumConverter))]
        public TagType? Type { get; set; }    // 'LIGHTWEIGHT' or 'ANNOTATED'
    }

    public enum TagType
    {
        Unknown,
        LIGHTWEIGHT,
        ANNOTATED
    }

    /// <summary>
    /// Handles 'Unknown' and 'nulls' convertions of <see cref="TagType"/>
    /// </summary>
    public class TagTypeEnumConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.Equals(TagType.Unknown))
            {
                value = null;
            }

            base.WriteJson(writer, value, serializer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null ||
                (!(reader?.Value?.ToString()).Equals(TagType.ANNOTATED.ToString(), StringComparison.OrdinalIgnoreCase) &&
                !(reader?.Value?.ToString()).Equals(TagType.LIGHTWEIGHT.ToString(), StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}
