using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Atlassian.Stash.IntegrationTests.NetCore
{
    [TestClass]
    public abstract class TestBase
    {
        IConfiguration config =>
            new ConfigurationBuilder()
            .AddJsonFile("testsettings.json", false, true)
            .Build()
            .GetSection("TestSettings");

        // data required to run this tests, please check App.config and modify the values to mapping to your local stash instance
        protected string BASE_URL => config["base-url"];
        protected string USERNAME => config["username"];
        protected string PASSWORD => config["password"];
        protected string EXISTING_PROJECT => config["existing-project"];
        protected string EXISTING_REPOSITORY => config["existing-repository"];
        protected string EXISTING_FILE => config["existing-file"];
        protected string EXISTING_FOLDER => config["existing-folder"];
        protected string EXISTING_FILE_IN_SUBFOLDER => config["existing-file-in-subfolder"];
        protected string EXISTING_FILE_IN_SUBFOLDER_WITH_SPACES => config["existing-file-in-subfolder-with-spaces"];
        protected string EXISTING_COMMIT => config["existing-commit"];
        protected string EXISTING_OLDER_COMMIT => config["existing-older-commit"];
        protected string EXISTING_BRANCH_REFERENCE => config["existing-branch-reference"];
        protected string EXISTING_BRANCH_NAME => config["existing-branch-name"];
        protected string EXISTING_GROUP => config["existing-group"];
        protected string EXISTING_HOOK => config["existing-hook"];
        protected int EXISTING_NUMBER_OF_CHANGES => int.Parse(config["existing-number-of-changes"]);

        protected StashClient stashClient;

        [TestInitialize]
        public void Initialize()
        {
            stashClient = new StashClient(BASE_URL, USERNAME, PASSWORD);
        }

    }
}
