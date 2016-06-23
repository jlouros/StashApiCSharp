using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Atlassian.Stash.Entities
{
    public class File
    {
        public File()
        {
            ListOfLines = new List<FileLine>();
        }

        [JsonProperty("lines")]
        public IEnumerable<FileLine> ListOfLines { get; set; }

        public int Size { get; set; }
        public bool IsLastPage { get; set; }
        public int Start { get; set; }

        public List<string> FileContents
        {
            get { return ListOfLines.Select(x=>x.Text).ToList(); }
        }
    }

    public class FileLine
    {
        public string Text { get; set; }
    }
}