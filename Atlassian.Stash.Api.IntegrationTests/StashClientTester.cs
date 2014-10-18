using Atlassian.Stash.Api.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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

            Assert.IsNotNull(projects);
            Assert.IsInstanceOfType(projects, typeof(IEnumerable<Project>));
            Assert.IsTrue(projects.Count() > 0);
        }

        [TestMethod]
        public void Can_GetProjects_UsingGeneric()
        {
            var projects = _stashClient.GetTAsync<Project>().Result;

            Assert.IsNotNull(projects);
            Assert.IsInstanceOfType(projects, typeof(IEnumerable<Project>));
            Assert.IsTrue(projects.Count() > 0);
        }

        [TestMethod]
        public void Can_GetRepositories_UsingGeneric()
        {
            // todo: remove hardcoded value
            var repositories = _stashClient.GetTAsync<Repository>("test").Result;

            Assert.IsNotNull(repositories);
            Assert.IsInstanceOfType(repositories, typeof(IEnumerable<Repository>));
            Assert.IsTrue(repositories.Count() > 0);
        }
    }
}
