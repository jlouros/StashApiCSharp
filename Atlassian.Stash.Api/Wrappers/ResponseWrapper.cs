using System.Collections.Generic;

namespace Atlassian.Stash.Api.Wrappers
{
    public class ResponseWrapper
    {
        public int Size { get; set; }
        public int Limit { get; set; }
        public bool IsLastPage { get; set; }
        public List<object> Values { get; set; }
        public int Start { get; set; }
        public int NextPageStart { get; set; }
    }
}
