namespace Atlassian.Stash.Entities
{
    public class LikedBy
    {
        public int Total { get; set; }

        public Author[] Likers { get; set; }
    }
}
