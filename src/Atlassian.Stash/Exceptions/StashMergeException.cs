using System;
using System.Collections.Generic;
using System.Text;
using Atlassian.Stash.Api.Entities;

namespace Atlassian.Stash.Api.Exceptions
{
    public class StashMergeException : Exception
    {
        public MergeErrorResponse ErrorResponse { get; set; }

        public StashMergeException(string message, Exception exc, MergeErrorResponse response) : base(message,exc)
        {
            ErrorResponse = response;
        }

        public StashMergeException(string message, MergeErrorResponse response) : base(message)
        {
            ErrorResponse = response;
        }
    }
}
