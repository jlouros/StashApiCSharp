using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Atlassian.Stash.Api.IntegrationTests
{
    [TestClass]
    public class StashClientTester
    {
        // data required to run this tests
        // please review this variables before you run this tests
        private const string EXISTING_PROJECT_NAME = "test";
        private const string EXISTING_REPOSITORY_NAME = "testrepository";

        private StashClient _stashClient;

        [TestInitialize]
        public void Initialize()
        {
            // WARNING: requires a real Stash instance and real User credentials
            _stashClient = new StashClient("http://ptr-vcs:7990/", "TestUser", "password");
        }

        [TestMethod]
        public void Can_GetManyProjects_UsingGeneric()
        {
            var response = _stashClient.GetManyTAsync<Project>().Result;
            var projects = response.Values;

            Assert.IsNotNull(projects);
            Assert.IsInstanceOfType(projects, typeof(IEnumerable<Project>));
            Assert.IsTrue(projects.Any());
        }

        [TestMethod]
        public void Can_GetManyProjects_UsingGeneric_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = _stashClient.GetManyTAsync<Project>(new RequestOptions { Limit = requestLimit, Start = 1 }).Result;
            var projects = response.Values;

            Assert.IsNotNull(projects);
            Assert.IsInstanceOfType(projects, typeof(IEnumerable<Project>));
            Assert.AreEqual(requestLimit, projects.Count());
        }

        [TestMethod]
        public void Can_GetSingleProject_UsingGeneric()
        {
            var project = _stashClient.GetSingleTAsync<Project>(EXISTING_PROJECT_NAME).Result;

            Assert.IsNotNull(project);
            Assert.IsInstanceOfType(project, typeof(Project));
            Assert.AreEqual(EXISTING_PROJECT_NAME.ToLower(), project.Name.ToLower());
        }

        [TestMethod]
        public void Can_GetManyRepositories_UsingGeneric()
        {
            var response = _stashClient.GetManyTAsync<Repository>(null, EXISTING_PROJECT_NAME).Result;
            var repositories = response.Values;

            Assert.IsNotNull(repositories);
            Assert.IsInstanceOfType(repositories, typeof(IEnumerable<Repository>));
            Assert.IsTrue(repositories.Any());
        }

        [TestMethod]
        public void Can_GetManyRepositories_UsingGeneric_WithRequestOptions()
        {
            int requestLimit = 2;
            var response = _stashClient.GetManyTAsync<Repository>(new RequestOptions { Limit = requestLimit }, EXISTING_PROJECT_NAME).Result;
            var repositories = response.Values;

            Assert.IsNotNull(repositories);
            Assert.IsInstanceOfType(repositories, typeof(IEnumerable<Repository>));
            Assert.AreEqual(requestLimit, repositories.Count());
        }

        [TestMethod]
        public void Can_GetSingleRepository_UsingGeneric()
        {
            var repository = (_stashClient.GetSingleTAsync<Repository>(EXISTING_PROJECT_NAME, EXISTING_REPOSITORY_NAME).Result);

            Assert.IsNotNull(repository);
            Assert.IsInstanceOfType(repository, typeof(Repository));
            Assert.AreEqual(EXISTING_REPOSITORY_NAME.ToLower(), repository.Name.ToLower());
        }

        [TestMethod]
        public void Can_GetManyTags_UsingGeneric()
        {
            var response = _stashClient.GetManyTAsync<Tag>(null, EXISTING_PROJECT_NAME, EXISTING_REPOSITORY_NAME).Result;
            var tags = response.Values;

            Assert.IsNotNull(tags);
            Assert.IsInstanceOfType(tags, typeof(IEnumerable<Tag>));
            Assert.IsTrue(tags.Any());
        }

        [TestMethod]
        public void Can_GetManyTags_UsingGeneric_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = _stashClient.GetManyTAsync<Tag>(new RequestOptions { Limit = requestLimit }, EXISTING_PROJECT_NAME, EXISTING_REPOSITORY_NAME).Result;
            var tags = response.Values;

            Assert.IsNotNull(tags);
            Assert.IsInstanceOfType(tags, typeof(IEnumerable<Tag>));
            Assert.AreEqual(requestLimit, tags.Count());
        }
    }
}
