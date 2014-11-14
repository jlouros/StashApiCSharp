
namespace Atlassian.Stash.Api.Entities
{
    public class BranchPermission
    {
        
        // todo: review this is a enum of 'BRANCH' 'PATTERN' 
        public string value { get; set; }
        public string type { get; set; }
        public string[] users { get; set; }
        public string[] groups { get; set; }

        //todo: review response has this props
        //public int id { get; set; }
        //public Branch branch { get; set; }
    }
}
