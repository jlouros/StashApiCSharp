
namespace Atlassian.Stash.Entities
{
    public class Diff
    {
        public Path Source { get; set; }

        public Path Destination { get; set; }

        public Hunk[] Hunks { get; set; }

        public DiffProperties Properties { get; set; }

        public bool Truncated { get; set; }
    }
}
