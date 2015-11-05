
using System;
using Atlassian.Stash.Api.Converters;
using Newtonsoft.Json;

namespace Atlassian.Stash.Api.Entities
{
    public class Commit
    {
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public Author Author { get; set; }
		[JsonConverter(typeof(TimestampConverter))]
        public DateTime AuthorTimestamp { get; set; }
        public string Message { get; set; }
        public Parent[] Parents { get; set; }
    }
}
