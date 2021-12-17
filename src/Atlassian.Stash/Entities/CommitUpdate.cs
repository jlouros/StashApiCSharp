namespace Atlassian.Stash.Entities
{
    public class CommitUpdate
    {
        public int Total { get; set; }

        public Commit[] Commits { get; set; }
    }
}
