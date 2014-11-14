using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Atlassian.Stash.Api.IntegrationTests
{
    [TestClass]
    public class StashClientTester
    {
        // data required to run this tests, please check App.config and modify the values to mapping to your local stash instance
        private readonly string BASE_URL = ConfigurationManager.AppSettings.Get("base-url");
        private readonly string USERNAME = ConfigurationManager.AppSettings.Get("username");
        private readonly string PASSWORD = ConfigurationManager.AppSettings.Get("password");
        private readonly string EXISTING_PROJECT = ConfigurationManager.AppSettings.Get("existing-project");
        private readonly string EXISTING_REPOSITORY = ConfigurationManager.AppSettings.Get("existing-repository");
        private readonly string EXISTING_FILE = ConfigurationManager.AppSettings.Get("existing-file");
        private readonly string EXISTING_COMMIT = ConfigurationManager.AppSettings.Get("existing-commit");
        private readonly string EXISTING_OLDER_COMMIT = ConfigurationManager.AppSettings.Get("existing-older-commit");
        private readonly string EXISTING_BRANCH_REFERENCE = ConfigurationManager.AppSettings.Get("existing-branch-reference");
        private readonly string EXISTING_GROUP = ConfigurationManager.AppSettings.Get("existing-group");

        private StashClient stashClient;

        [TestInitialize]
        public void Initialize()
        {
            stashClient = new StashClient(BASE_URL, USERNAME, PASSWORD);
        }

        [TestMethod]
        public void Can_GetFileContents()
        {
            var response = stashClient.Repositories.GetFileContents(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_FILE).Result;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.FileContents.Count > 0);
        }

        [TestMethod]
        public void Can_GetBranchesForCommit()
        {
            var response = stashClient.Branches.GetByCommitId(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT).Result;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Values.Any(x => x.Id.Equals(EXISTING_BRANCH_REFERENCE)));
        }

        [TestMethod]
        public void Can_GetAllProjects()
        {
            var response = stashClient.Projects.Get().Result;
            var projects = response.Values;

            Assert.IsNotNull(projects);
            Assert.IsInstanceOfType(projects, typeof(IEnumerable<Project>));
            Assert.IsTrue(projects.Any());
        }

        [TestMethod]
        public void Can_GetAllProjects_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = stashClient.Projects.Get(new RequestOptions { Limit = requestLimit, Start = 1 }).Result;
            var projects = response.Values;

            Assert.IsNotNull(projects);
            Assert.IsInstanceOfType(projects, typeof(IEnumerable<Project>));
            Assert.AreEqual(requestLimit, projects.Count());
        }

        [TestMethod]
        public void Can_GetByIdProject()
        {
            var project = stashClient.Projects.GetById(EXISTING_PROJECT).Result;

            Assert.IsNotNull(project);
            Assert.IsInstanceOfType(project, typeof(Project));
            Assert.AreEqual(EXISTING_PROJECT.ToLower(), project.Name.ToLower());
        }

        [TestMethod]
        public void Can_GetAllRepositories()
        {
            var response = stashClient.Repositories.Get(EXISTING_PROJECT).Result;
            var repositories = response.Values;

            Assert.IsNotNull(repositories);
            Assert.IsInstanceOfType(repositories, typeof(IEnumerable<Repository>));
            Assert.IsTrue(repositories.Any());
        }

        [TestMethod]
        public void Can_GetAllRepositories_WithRequestOptions()
        {
            int requestLimit = 2;
            var response = stashClient.Repositories.Get(EXISTING_PROJECT, new RequestOptions { Limit = requestLimit }).Result;
            var repositories = response.Values;

            Assert.IsNotNull(repositories);
            Assert.IsInstanceOfType(repositories, typeof(IEnumerable<Repository>));
            Assert.AreEqual(requestLimit, repositories.Count());
        }

        [TestMethod]
        public void Can_GetByIdRepository()
        {
            var repository = stashClient.Repositories.GetById(EXISTING_PROJECT, EXISTING_REPOSITORY).Result;

            Assert.IsNotNull(repository);
            Assert.IsInstanceOfType(repository, typeof(Repository));
            Assert.AreEqual(EXISTING_REPOSITORY.ToLower(), repository.Name.ToLower());
        }

        [TestMethod]
        public void Can_GetAllTags()
        {
            var response = stashClient.Repositories.GetTags(EXISTING_PROJECT, EXISTING_REPOSITORY).Result;
            var tags = response.Values;

            Assert.IsNotNull(tags);
            Assert.IsInstanceOfType(tags, typeof(IEnumerable<Tag>));
            Assert.IsTrue(tags.Any());
        }

        [TestMethod]
        public void Can_GetAllTags_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = stashClient.Repositories.GetTags(EXISTING_PROJECT, EXISTING_REPOSITORY, new RequestOptions { Limit = requestLimit }).Result;
            var tags = response.Values;

            Assert.IsNotNull(tags);
            Assert.IsInstanceOfType(tags, typeof(IEnumerable<Tag>));
            Assert.AreEqual(requestLimit, tags.Count());
        }

        [TestMethod]
        public void Can_GetAllFiles()
        {
            var response = stashClient.Repositories.GetFiles(EXISTING_PROJECT, EXISTING_REPOSITORY).Result;
            var files = response.Values;

            Assert.IsNotNull(files);
            Assert.IsInstanceOfType(files, typeof(IEnumerable<string>));
            Assert.IsTrue(files.Any());
        }

        [TestMethod]
        public void Can_GetAllFiles_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = stashClient.Repositories.GetFiles(EXISTING_PROJECT, EXISTING_REPOSITORY, new RequestOptions { Limit = requestLimit }).Result;
            var files = response.Values;

            Assert.IsNotNull(files);
            Assert.IsInstanceOfType(files, typeof(IEnumerable<string>));
            Assert.AreEqual(requestLimit, files.Count());
        }

        [TestMethod]
        public void Can_GetAllBranches()
        {
            var response = stashClient.Branches.GetAll(EXISTING_PROJECT, EXISTING_REPOSITORY).Result;
            var branches = response.Values;

            Assert.IsNotNull(branches);
            Assert.IsInstanceOfType(branches, typeof(IEnumerable<Branch>));
            Assert.IsTrue(branches.Any());
        }

        [TestMethod]
        public void Can_GetAllBranches_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = stashClient.Branches.GetAll(EXISTING_PROJECT, EXISTING_REPOSITORY, new RequestOptions { Limit = requestLimit }).Result;
            var branches = response.Values;

            Assert.IsNotNull(branches);
            Assert.IsInstanceOfType(branches, typeof(IEnumerable<Branch>));
            Assert.AreEqual(requestLimit, branches.Count());
        }

        [TestMethod]
        public void Can_GetAllCommits()
        {
            var response = stashClient.Commits.GetAll(EXISTING_PROJECT, EXISTING_REPOSITORY).Result;
            var commits = response.Values;

            Assert.IsNotNull(commits);
            Assert.IsInstanceOfType(commits, typeof(IEnumerable<Commit>));
            Assert.IsTrue(commits.Any());
        }

        [TestMethod]
        public void Can_GetAllCommits_WithRequestOptions()
        {
            int requestLimit = 2;
            var response = stashClient.Commits.GetAll(EXISTING_PROJECT, EXISTING_REPOSITORY, new RequestOptions { Limit = requestLimit }).Result;
            var commits = response.Values;

            Assert.IsNotNull(commits);
            Assert.IsInstanceOfType(commits, typeof(IEnumerable<Commit>));
            Assert.AreEqual(requestLimit, commits.Count());
        }

        [TestMethod]
        public void Can_GetByIdCommit()
        {
            var commit = stashClient.Commits.GetById(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT).Result;

            Assert.IsNotNull(commit);
            Assert.IsInstanceOfType(commit, typeof(Commit));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), commit.Id.ToLower());
        }

        [TestMethod]
        public void Can_GetChangesUntil()
        {
            var changes = stashClient.Commits.GetChanges(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT).Result;

            Assert.IsNotNull(changes);
            Assert.IsInstanceOfType(changes, typeof(Changes));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), changes.ToHash.ToLower());
        }

        [TestMethod]
        public void Can_GetChangesUntil_WithRequestOptions()
        {
            int requestLimit = 1;
            var changes = stashClient.Commits.GetChanges(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT, null, new RequestOptions { Limit = requestLimit }).Result;

            Assert.IsNotNull(changes);
            Assert.IsInstanceOfType(changes, typeof(Changes));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), changes.ToHash.ToLower());
            Assert.AreEqual(requestLimit, changes.ListOfChanges.Count());
        }

        [TestMethod]
        public void Can_GetChangesUntil_And_Since()
        {
            var changes = stashClient.Commits.GetChanges(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT, EXISTING_OLDER_COMMIT).Result;

            Assert.IsNotNull(changes);
            Assert.IsInstanceOfType(changes, typeof(Changes));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), changes.ToHash.ToLower());
        }

        [TestMethod]
        public void Can_GetChangesUntil_And_Since_WithRequestOptions()
        {
            int requestLimit = 1;
            var changes = stashClient.Commits.GetChanges(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT, EXISTING_OLDER_COMMIT, new RequestOptions { Limit = requestLimit }).Result;

            Assert.IsNotNull(changes);
            Assert.IsInstanceOfType(changes, typeof(Changes));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), changes.ToHash.ToLower());
            Assert.AreEqual(requestLimit, changes.ListOfChanges.Count());
        }

        #region Feature tests

        [TestMethod]
        public void Can_SetBranchPermissions_Than_DeleteBranchPermissions()
        {
            var setBranchPerm = new BranchPermission
            {
                Type = BranchPermissionType.BRANCH,
                Value = "master",
                Groups = new string[] { EXISTING_GROUP },
                Users = new string[] { }
            };

            var response = stashClient.Branches.SetPermissions(EXISTING_PROJECT, EXISTING_REPOSITORY, setBranchPerm).Result;

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BranchPermission));
            Assert.AreEqual(setBranchPerm.Type, response.Type);
            Assert.AreEqual(setBranchPerm.Value, response.Value);
            Assert.IsTrue(response.Id > 0);

            stashClient.Branches.DeletePermissions(EXISTING_PROJECT, EXISTING_REPOSITORY, response.Id).Wait();
        }

        [TestMethod]
        public void Can_SetBranchPermissions_Than_DeleteBranchPermissions_Using_Pattern()
        {
            var setBranchPerm = new BranchPermission
            {
                Type = BranchPermissionType.PATTERN,
                Value = "**",
                Groups = new string[] { EXISTING_GROUP },
                Users = new string[] { }
            };

            var response = stashClient.Branches.SetPermissions(EXISTING_PROJECT, EXISTING_REPOSITORY, setBranchPerm).Result;

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BranchPermission));
            Assert.AreEqual(setBranchPerm.Type, response.Type);
            Assert.AreEqual(setBranchPerm.Value, response.Value);
            Assert.IsTrue(response.Id > 0);

            stashClient.Branches.DeletePermissions(EXISTING_PROJECT, EXISTING_REPOSITORY, response.Id).Wait();
        }

        [TestMethod]
        public void Can_CreateProject_Than_DeleteProject()
        {
            Project newProject = new Project { Key = "ZTEST", Name = "Project of Integration tests", Description = "Project created by integration tests, please delete!" };
            var createdProject = stashClient.Projects.Create(newProject).Result;

            Assert.IsNotNull(createdProject);
            Assert.IsInstanceOfType(createdProject, typeof(Project));
            Assert.AreEqual(newProject.Key.ToLower(), createdProject.Key.ToLower());

            stashClient.Projects.Delete(newProject.Key).Wait();
        }

        [TestMethod]
        public void Can_CreateRepository_Than_DeleteRepository()
        {
            Repository newRepository = new Repository { Name = "Repository of Integration tests" };
            var createdRepository = stashClient.Repositories.Create(EXISTING_PROJECT, newRepository).Result;

            Assert.IsNotNull(createdRepository);
            Assert.IsInstanceOfType(createdRepository, typeof(Repository));
            Assert.AreEqual(newRepository.Name.ToLower(), createdRepository.Name.ToLower());

            stashClient.Repositories.Delete(EXISTING_PROJECT, createdRepository.Slug).Wait();
        }

        [TestMethod]
        public void Can_CreateBranch_Than_DeleteBranch()
        {
            Branch newBranch = new Branch { Name = "test-repo", StartPoint = EXISTING_BRANCH_REFERENCE };
            var createdBranch = stashClient.Branches.Create(EXISTING_PROJECT, EXISTING_REPOSITORY, newBranch).Result;

            Assert.IsNotNull(createdBranch);
            Assert.IsInstanceOfType(createdBranch, typeof(Branch));
            Assert.AreEqual(newBranch.Name.ToLower(), createdBranch.DisplayId.ToLower());


            Branch deleteBranch = new Branch { Name = newBranch.Name, DryRun = false };

            stashClient.Branches.Delete(EXISTING_PROJECT, EXISTING_REPOSITORY, deleteBranch).Wait();
        }

        #endregion
    }
}
