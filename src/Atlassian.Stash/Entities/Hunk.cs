
namespace Atlassian.Stash.Entities
{
    public class Hunk
    {
        public string Context { get; set; }

        public int SourceLine { get; set; }

        public int SourceSpan { get; set; }

        public int DestinationLine { get; set; }

        public int DestinationSpan { get; set; }

        public Segment[] Segments { get; set; }

        public bool Truncated { get; set; }
    }
}
