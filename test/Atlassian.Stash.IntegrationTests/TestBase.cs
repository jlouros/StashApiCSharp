using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Atlassian.Stash.IntegrationTests
{
    [TestClass]
    public abstract class TestBase
    {
        // data required to run this tests, please check App.config and modify the values to mapping to your local stash instance
        protected readonly string BASE_URL = ConfigurationManager.AppSettings.Get("base-url");
        protected readonly string TEST_USERNAME = ConfigurationManager.AppSettings.Get("test-username");
        protected readonly string TEST_PASSWORD = ConfigurationManager.AppSettings.Get("test-password");
        protected readonly string OTHERTEST_USERNAME = ConfigurationManager.AppSettings.Get("othertest-username");
        protected readonly string OTHERTEST_PASSWORD = ConfigurationManager.AppSettings.Get("othertest-password");
        protected readonly string EXISTING_PROJECT = ConfigurationManager.AppSettings.Get("existing-project");
        protected readonly string EXISTING_REPOSITORY = ConfigurationManager.AppSettings.Get("existing-repository");
        protected readonly string EXISTING_FILE = ConfigurationManager.AppSettings.Get("existing-file");
        protected readonly string EXISTING_FOLDER = ConfigurationManager.AppSettings.Get("existing-folder");
        protected readonly string EXISTING_FILE_IN_SUBFOLDER = ConfigurationManager.AppSettings.Get("existing-file-in-subfolder");
        protected readonly string EXISTING_FILE_IN_SUBFOLDER_WITH_SPACES = ConfigurationManager.AppSettings.Get("existing-file-in-subfolder-with-spaces");
        protected readonly string EXISTING_COMMIT = ConfigurationManager.AppSettings.Get("existing-commit");
        protected readonly string EXISTING_OLDER_COMMIT = ConfigurationManager.AppSettings.Get("existing-older-commit");
        protected readonly string MASTER_BRANCH_REFERENCE = ConfigurationManager.AppSettings.Get("master-branch-reference");
        protected readonly string DEVELOP_BRANCH_REFERENCE = ConfigurationManager.AppSettings.Get("develop-branch-reference");
        protected readonly string MASTER_BRANCH_NAME = ConfigurationManager.AppSettings.Get("master-branch-name");
        protected readonly string EXISTING_GROUP = ConfigurationManager.AppSettings.Get("existing-group");
        protected readonly string EXISTING_HOOK = ConfigurationManager.AppSettings.Get("existing-hook");
        protected readonly int EXISTING_NUMBER_OF_CHANGES = int.Parse(ConfigurationManager.AppSettings.Get("existing-number-of-changes"));

        protected StashClient stashClient;

        [TestInitialize]
        public void Initialize()
        {
            stashClient = new StashClient(BASE_URL, TEST_USERNAME, TEST_PASSWORD);
        }

    }
}
