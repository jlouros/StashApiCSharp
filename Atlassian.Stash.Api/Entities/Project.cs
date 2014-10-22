
namespace Atlassian.Stash.Api.Entities
{
    //paged API https://developer.atlassian.com/static/rest/stash/3.4.0/stash-rest.html#paging-params
    public class Project
    {
        public string Key { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool @Public { get; set; }
        public string Type { get; set; }
        public Link Link { get; set; }
        public Links Links { get; set; }
    }
}