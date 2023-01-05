namespace Atlassian.Stash.Entities
{
    public class PermittedOperations
    {
        public bool Editable { get; set; }

        public bool Transitionable { get; set; }

        public bool Deletable { get; set; }
    }
}
