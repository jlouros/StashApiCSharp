using Atlassian.Stash.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Atlassian.Stash.Converters
{
    public class TimestampConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long val;
            DateTime? dt = value as DateTime?;

            if (dt == null)
            {
                throw new InvalidOperationException("Expected DateTime got " + value.GetType().Name);
            }

            val = dt.Value.ToTimestamp();
            writer.WriteValue(val);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception("Wrong Token Type");
            }

            long ticks = (long)reader.Value;
            return ticks.FromTimestamp();
        }
    }
}
