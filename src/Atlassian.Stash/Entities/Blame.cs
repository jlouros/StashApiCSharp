namespace Atlassian.Stash.Entities
{
    public class Blame
    {
        public Author Author { get; set; }

        public long AuthorTimeStamp { get; set; }

        public string CommitDisplayId { get; set; }

        public string CommitHash { get; set; }

        public string CommitId { get; set; }

        public string DisplayCommitHash { get; set; }

        public string FileName { get; set; }

        public int LineNumber { get; set; }

        public int SpannedLines { get; set; }
    }
}