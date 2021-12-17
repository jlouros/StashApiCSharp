
namespace Atlassian.Stash.Entities
{
    public class Reactions
    {
        public Emoticon Emoticon { get; set; }

        public User[] Users { get; set; }

        public string[] Issues { get; set; }
    }
}
