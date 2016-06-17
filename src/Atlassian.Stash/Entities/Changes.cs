using Newtonsoft.Json;
using System.Collections.Generic;

namespace Atlassian.Stash.Entities
{
    public class Changes
    {
        public Changes()
        {
            ListOfChanges = new List<Change>();
        }

        public object FromHash { get; set; }
        public string ToHash { get; set; }

        [JsonProperty("values")]
        public IEnumerable<Change> ListOfChanges { get; set; }
        public int Size { get; set; }
        public bool IsLastPage { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public object NextPageStart { get; set; }
    }
}
