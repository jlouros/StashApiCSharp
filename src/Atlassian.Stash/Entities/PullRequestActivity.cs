
using System;
using Atlassian.Stash.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Atlassian.Stash.Entities
{
    public enum PullRequestActivityType
    {
        NONE,
        MERGED,
        UNAPPROVED,
        APPROVED,
        COMMENTED,
        RESCOPED,
        REVIEWED,
        OPENED,
        REOPENED,
        DECLINED,
        UPDATED
    }

    public enum CommentActionType
    {
        NONE,
        ADDED,
        REMOVED
    }

    public class PullRequestActivity
    {
        public string Id { get; set; }

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedDate { get; set; }
        
        public User User { get; set; }
        
        public Commit Commit { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public PullRequestActivityType Action { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public CommentActionType CommentAction { get; set; }
        
        public Comment Comment { get; set; }
        
        public CommentAnchor CommentAnchor { get; set; }
        
        public Diff Diff { get; set; }
        
        public string PreviousFromHash { get; set; }
        
        public string FromHash { get; set; }
        
        public string PreviousToHash { get; set; }
        
        public string ToHash { get; set; }

        public CommitUpdate Added { get; set; }

        public CommitUpdate Removed { get; set; }

        public Author[] AddedReviewers { get; set; }

        public Author[] RemovedReviewers { get; set; }
    }
}
