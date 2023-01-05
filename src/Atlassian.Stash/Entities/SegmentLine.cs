namespace Atlassian.Stash.Entities
{
    public class SegmentLine
    {
        public int Destination { get; set; }

        public int Source { get; set; }

        public string Line { get; set; }

        public bool Truncated { get; set; }

        public int[] CommentIds { get; set; }
    }
}
