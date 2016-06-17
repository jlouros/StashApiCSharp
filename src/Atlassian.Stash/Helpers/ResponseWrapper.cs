using System.Collections.Generic;

namespace Atlassian.Stash.Helpers
{
    public class ResponseWrapper<T>
    {
        public int Size { get; set; }
        public int Limit { get; set; }
        public bool IsLastPage { get; set; }
        public IEnumerable<T> Values { get; set; }
        public int Start { get; set; }
        public int? NextPageStart { get; set; }
    }

#warning JSON Response error
    public class ErrorResponse
    {
        public Error[] errors { get; set; }
    }

    public class Error
    {
        public object Context { get; set; }
        public string Message { get; set; }
        public object ExceptionName { get; set; }
    }

#warning Missing HTTP error message handlers (use unknown Content-Type header)
}
