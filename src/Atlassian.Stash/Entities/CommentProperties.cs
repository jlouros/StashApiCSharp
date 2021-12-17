
namespace Atlassian.Stash.Entities
{
    public class CommentProperties
    {
        public string RepositoryId { get; set; }

        public string SuggestionState { get; set; }

        public LikedBy LikedBy { get; set; }

        public Reactions[] Reactions { get; set; }

        public string[] Issues { get; set; }

        public string DiffAnchorPath { get; set; }

        public string SuggestionIndex { get; set; }
    }
}
