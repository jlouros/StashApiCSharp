using Atlassian.Stash.Api.Entities;
using Atlassian.Stash.Api.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly string EXISTING_HOOK = ConfigurationManager.AppSettings.Get("existing-hook");
        private readonly int EXISTING_NUMBER_OF_CHANGES = 3; // TODO: update to actual number of changes in test repo

        private StashClient stashClient;

        [TestInitialize]
        public void Initialize()
        {
            stashClient = new StashClient(BASE_URL, USERNAME, PASSWORD);
        }

        [TestMethod]
        public async Task Can_GetFileContents()
        {
            var response = await stashClient.Repositories.GetFileContents(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_FILE);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.FileContents.Count > 0);
        }

        [TestMethod]
        public async Task Can_GetBranchesForCommit()
        {
            var response = await stashClient.Branches.GetByCommitId(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Values.Any(x => x.Id.Equals(EXISTING_BRANCH_REFERENCE)));
        }

        [TestMethod]
        public async Task Can_GetAllProjects()
        {
            var response = await stashClient.Projects.Get();
            var projects = response.Values;

            Assert.IsNotNull(projects);
            Assert.IsInstanceOfType(projects, typeof(IEnumerable<Project>));
            Assert.IsTrue(projects.Any());
        }

        [TestMethod]
        public async Task Can_GetAllProjects_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = await stashClient.Projects.Get(new RequestOptions { Limit = requestLimit, Start = 1 });
            var projects = response.Values;

            Assert.IsNotNull(projects);
            Assert.IsInstanceOfType(projects, typeof(IEnumerable<Project>));
            Assert.AreEqual(requestLimit, projects.Count());
        }

        [TestMethod]
        public async Task Can_GetByIdProject()
        {
            var project = await stashClient.Projects.GetById(EXISTING_PROJECT);

            Assert.IsNotNull(project);
            Assert.IsInstanceOfType(project, typeof(Project));
            Assert.AreEqual(EXISTING_PROJECT.ToLower(), project.Name.ToLower());
        }

        [TestMethod]
        public async Task Can_GetAllRepositories()
        {
            var response = await stashClient.Repositories.Get(EXISTING_PROJECT);
            var repositories = response.Values;

            Assert.IsNotNull(repositories);
            Assert.IsInstanceOfType(repositories, typeof(IEnumerable<Repository>));
            Assert.IsTrue(repositories.Any());
        }

        [TestMethod]
        public async Task Can_GetAllRepositories_WithRequestOptions()
        {
            int requestLimit = 2;
            var response = await stashClient.Repositories.Get(EXISTING_PROJECT, new RequestOptions { Limit = requestLimit });
            var repositories = response.Values;

            Assert.IsNotNull(repositories);
            Assert.IsInstanceOfType(repositories, typeof(IEnumerable<Repository>));
            Assert.AreEqual(requestLimit, repositories.Count());
        }

        [TestMethod]
        public async Task Can_GetByIdRepository()
        {
            var repository = await stashClient.Repositories.GetById(EXISTING_PROJECT, EXISTING_REPOSITORY);

            Assert.IsNotNull(repository);
            Assert.IsInstanceOfType(repository, typeof(Repository));
            Assert.AreEqual(EXISTING_REPOSITORY.ToLower(), repository.Name.ToLower());
        }

        [TestMethod]
        public async Task Can_GetAllTags()
        {
            var response = await stashClient.Repositories.GetTags(EXISTING_PROJECT, EXISTING_REPOSITORY);
            var tags = response.Values;

            Assert.IsNotNull(tags);
            Assert.IsInstanceOfType(tags, typeof(IEnumerable<Tag>));
            Assert.IsTrue(tags.Any());
        }

        [TestMethod]
        public async Task Can_GetAllTags_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = await stashClient.Repositories.GetTags(EXISTING_PROJECT, EXISTING_REPOSITORY, new RequestOptions { Limit = requestLimit });
            var tags = response.Values;

            Assert.IsNotNull(tags);
            Assert.IsInstanceOfType(tags, typeof(IEnumerable<Tag>));
            Assert.AreEqual(requestLimit, tags.Count());
        }

        [TestMethod]
        public async Task Can_GetAllFiles()
        {
            var response = await stashClient.Repositories.GetFiles(EXISTING_PROJECT, EXISTING_REPOSITORY);
            var files = response.Values;

            Assert.IsNotNull(files);
            Assert.IsInstanceOfType(files, typeof(IEnumerable<string>));
            Assert.IsTrue(files.Any());
        }

        [TestMethod]
        public async Task Can_GetAllFiles_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = await stashClient.Repositories.GetFiles(EXISTING_PROJECT, EXISTING_REPOSITORY, new RequestOptions { Limit = requestLimit });
            var files = response.Values;

            Assert.IsNotNull(files);
            Assert.IsInstanceOfType(files, typeof(IEnumerable<string>));
            Assert.AreEqual(requestLimit, files.Count());
        }

        [TestMethod]
        public async Task Can_GetAllBranches()
        {
            var response = await stashClient.Branches.Get(EXISTING_PROJECT, EXISTING_REPOSITORY);
            var branches = response.Values;

            Assert.IsNotNull(branches);
            Assert.IsInstanceOfType(branches, typeof(IEnumerable<Branch>));
            Assert.IsTrue(branches.Any());
        }

        [TestMethod]
        public async Task Can_GetAllBranches_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = await stashClient.Branches.Get(EXISTING_PROJECT, EXISTING_REPOSITORY, new RequestOptions { Limit = requestLimit });
            var branches = response.Values;

            Assert.IsNotNull(branches);
            Assert.IsInstanceOfType(branches, typeof(IEnumerable<Branch>));
            Assert.AreEqual(requestLimit, branches.Count());
        }

        [TestMethod]
        public async Task Can_GetAllCommits()
        {
            var response = await stashClient.Commits.Get(EXISTING_PROJECT, EXISTING_REPOSITORY);
            var commits = response.Values;

            Assert.IsNotNull(commits);
            Assert.IsInstanceOfType(commits, typeof(IEnumerable<Commit>));
            Assert.IsTrue(commits.Any());
        }

        [TestMethod]
        public async Task Can_GetAllCommits_WithRequestOptions()
        {
            int requestLimit = 2;
            var response = await stashClient.Commits.Get(EXISTING_PROJECT, EXISTING_REPOSITORY, new RequestOptions { Limit = requestLimit });
            var commits = response.Values;

            Assert.IsNotNull(commits);
            Assert.IsInstanceOfType(commits, typeof(IEnumerable<Commit>));
            Assert.AreEqual(requestLimit, commits.Count());
        }

        [TestMethod]
        public async Task Can_GetByIdCommit()
        {
            var commit = await stashClient.Commits.GetById(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT);

            Assert.IsNotNull(commit);
            Assert.IsInstanceOfType(commit, typeof(Commit));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), commit.Id.ToLower());
        }

        [TestMethod]
        public async Task Can_GetChangesUntil()
        {
            var changes = await stashClient.Commits.GetChanges(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT);

            Assert.IsNotNull(changes);
            Assert.IsInstanceOfType(changes, typeof(Changes));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), changes.ToHash.ToLower());
        }

        [TestMethod]
        public async Task Can_GetChangesUntil_WithRequestOptions()
        {
            int requestLimit = 1;
            var changes = await stashClient.Commits.GetChanges(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT, null, new RequestOptions { Limit = requestLimit });

            Assert.IsNotNull(changes);
            Assert.IsInstanceOfType(changes, typeof(Changes));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), changes.ToHash.ToLower());
            Assert.AreEqual(requestLimit, changes.ListOfChanges.Count());
        }

        [TestMethod]
        public async Task Can_GetChangesUntil_And_Since()
        {
            var changes = await stashClient.Commits.GetChanges(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT, EXISTING_OLDER_COMMIT);

            Assert.IsNotNull(changes);
            Assert.IsInstanceOfType(changes, typeof(Changes));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), changes.ToHash.ToLower());
        }

        [TestMethod]
        public async Task Can_GetChangesUntil_And_Since_WithRequestOptions()
        {
            int requestLimit = 1;
            var changes = await stashClient.Commits.GetChanges(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT, EXISTING_OLDER_COMMIT, new RequestOptions { Limit = requestLimit });

            Assert.IsNotNull(changes);
            Assert.IsInstanceOfType(changes, typeof(Changes));
            Assert.AreEqual(EXISTING_COMMIT.ToLower(), changes.ToHash.ToLower());
            Assert.AreEqual(requestLimit, changes.ListOfChanges.Count());
        }

        [TestMethod]
        public async Task Can_GetChangesUntil_And_Since_MoreThanOneResult()
        {
            var changes = await stashClient.Commits.GetChanges(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_COMMIT, EXISTING_OLDER_COMMIT);

            Assert.IsNotNull(changes);
            Assert.IsInstanceOfType(changes, typeof(Changes));
            Assert.AreEqual(EXISTING_COMMIT, changes.ToHash, ignoreCase: true);
            Assert.AreEqual(EXISTING_NUMBER_OF_CHANGES, changes.ListOfChanges.Count());
        }

        [TestMethod]
        public async Task Can_GetRepository_Hooks()
        {
            var response = await stashClient.Repositories.GetHooks(EXISTING_PROJECT, EXISTING_REPOSITORY);
            var hooks = response.Values;

            Assert.IsNotNull(hooks);
            Assert.IsInstanceOfType(hooks, typeof(IEnumerable<Hook>));
            Assert.IsTrue(hooks.Any());
        }

        [TestMethod]
        public async Task Can_GetRepository_Hooks_WithRequestOptions()
        {
            int requestLimit = 1;
            var response = await stashClient.Repositories.GetHooks(EXISTING_PROJECT, EXISTING_REPOSITORY, new RequestOptions { Limit = requestLimit });
            var hooks = response.Values;

            Assert.IsNotNull(hooks);
            Assert.IsInstanceOfType(hooks, typeof(IEnumerable<Hook>));
            Assert.AreEqual(requestLimit, hooks.Count());
        }

        [TestMethod]
        public async Task Can_GetRepository_Hook_ById()
        {
            var response = await stashClient.Repositories.GetHookById(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_HOOK);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(Hook));
            Assert.AreEqual(EXISTING_HOOK, response.Details.Key);
        }

        #region Feature tests

        [TestMethod]
        public async Task Can_SetBranchPermissions_Than_DeleteBranchPermissions()
        {
            var setBranchPerm = new BranchPermission
            {
                Type = BranchPermissionType.BRANCH,
                Value = "master",
                Groups = new string[] { EXISTING_GROUP },
                Users = new string[] { }
            };

            var response = await stashClient.Branches.SetPermissions(EXISTING_PROJECT, EXISTING_REPOSITORY, setBranchPerm);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BranchPermission));
            Assert.AreEqual(setBranchPerm.Type, response.Type);
            Assert.AreEqual(setBranchPerm.Value, response.Value);
            Assert.IsTrue(response.Id > 0);

            await stashClient.Branches.DeletePermissions(EXISTING_PROJECT, EXISTING_REPOSITORY, response.Id);
        }

        [TestMethod]
        public async Task Can_SetBranchPermissions_Than_DeleteBranchPermissions_Using_Pattern()
        {
            var setBranchPerm = new BranchPermission
            {
                Type = BranchPermissionType.PATTERN,
                Value = "**",
                Groups = new string[] { EXISTING_GROUP },
                Users = new string[] { }
            };

            var response = await stashClient.Branches.SetPermissions(EXISTING_PROJECT, EXISTING_REPOSITORY, setBranchPerm);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BranchPermission));
            Assert.AreEqual(setBranchPerm.Type, response.Type);
            Assert.AreEqual(setBranchPerm.Value, response.Value);
            Assert.IsTrue(response.Id > 0);

            await stashClient.Branches.DeletePermissions(EXISTING_PROJECT, EXISTING_REPOSITORY, response.Id);
        }

        [TestMethod]
        public async Task Can_CreateProject_Than_DeleteProject()
        {
            Project newProject = new Project { Key = "ZTEST", Name = "Project of Integration tests", Description = "Project created by integration tests, please delete!" };
            var createdProject = await stashClient.Projects.Create(newProject);

            Assert.IsNotNull(createdProject);
            Assert.IsInstanceOfType(createdProject, typeof(Project));
            Assert.AreEqual(newProject.Key.ToLower(), createdProject.Key.ToLower());

            await stashClient.Projects.Delete(newProject.Key);
        }

        [TestMethod]
        public async Task Can_CreateRepository_Than_DeleteRepository()
        {
            Repository newRepository = new Repository { Name = "Repository of Integration tests" };
            var createdRepository = await stashClient.Repositories.Create(EXISTING_PROJECT, newRepository);

            Assert.IsNotNull(createdRepository);
            Assert.IsInstanceOfType(createdRepository, typeof(Repository));
            Assert.AreEqual(newRepository.Name.ToLower(), createdRepository.Name.ToLower());

            await stashClient.Repositories.Delete(EXISTING_PROJECT, createdRepository.Slug);
        }

        [TestMethod]
        public async Task Can_CreateBranch_Than_DeleteBranch()
        {
            Branch newBranch = new Branch { Name = "test-repo", StartPoint = EXISTING_BRANCH_REFERENCE };
            var createdBranch = await stashClient.Branches.Create(EXISTING_PROJECT, EXISTING_REPOSITORY, newBranch);

            Assert.IsNotNull(createdBranch);
            Assert.IsInstanceOfType(createdBranch, typeof(Branch));
            Assert.AreEqual(newBranch.Name.ToLower(), createdBranch.DisplayId.ToLower());


            Branch deleteBranch = new Branch { Name = newBranch.Name, DryRun = false };

            await stashClient.Branches.Delete(EXISTING_PROJECT, EXISTING_REPOSITORY, deleteBranch);
        }

        [TestMethod]
        public async Task Can_Get_Enable_And_Disable_Hook()
        {
            var enableHook = await stashClient.Repositories.EnableHook(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_HOOK);

            Assert.IsNotNull(enableHook);
            Assert.IsTrue(enableHook.Enabled);
            Assert.IsInstanceOfType(enableHook, typeof(Hook));
            Assert.AreEqual(EXISTING_HOOK, enableHook.Details.Key);

            var disableHook = await stashClient.Repositories.DisableHook(EXISTING_PROJECT, EXISTING_REPOSITORY, EXISTING_HOOK);

            Assert.IsNotNull(disableHook);
            Assert.IsFalse(disableHook.Enabled);
            Assert.IsInstanceOfType(disableHook, typeof(Hook));
            Assert.AreEqual(EXISTING_HOOK, disableHook.Details.Key);
        }

        #endregion
    }
}
