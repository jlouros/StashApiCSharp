using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Atlassian.Stash.Api.IntegrationTests
{
    [TestClass]
    public class StashClientTester
    {
        private StashClient _stashClient;

        [TestInitialize]
        public void Initialize()
        {
            // WARNING: requires a real Stash instance and real User credentials
            _stashClient = new StashClient("http://ptr-vcs:7990/", "TestUser", "password");
        }

        [TestMethod]
        public void Can_GetProjects()
        {
            var projects = _stashClient.GetProjectsAsync().Result;
        }
    }
}
