using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Atlassian.Stash.IntegrationTests
{
    [TestClass]
    public abstract class TestBase
    {
        public static readonly IConfiguration Configuration;

        static TestBase()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        // data required to run this tests, please check App.config and modify the values to mapping to your local stash instance
        protected readonly string BASE_URL = Configuration["testSettings:base-url"];
        protected readonly string TEST_USERNAME = Configuration["testSettings:test-username"];
        protected readonly string TEST_PASSWORD = Configuration["testSettings:test-password"];
        protected readonly string OTHERTEST_USERNAME = Configuration["testSettings:othertest-username"];
        protected readonly string OTHERTEST_PASSWORD = Configuration["testSettings:othertest-password"];
        protected readonly string EXISTING_PROJECT = Configuration["testSettings:existing-project"];
        protected readonly string EXISTING_REPOSITORY = Configuration["testSettings:existing-repository"];
        protected readonly string EXISTING_FILE = Configuration["testSettings:existing-file"];
        protected readonly string EXISTING_FOLDER = Configuration["testSettings:existing-folder"];
        protected readonly string EXISTING_FILE_IN_SUBFOLDER = Configuration["testSettings:existing-file-in-subfolder"];
        protected readonly string EXISTING_FILE_IN_SUBFOLDER_WITH_SPACES = Configuration["testSettings:existing-file-in-subfolder-with-spaces"];
        protected readonly string EXISTING_COMMIT = Configuration["testSettings:existing-commit"];
        protected readonly string EXISTING_OLDER_COMMIT = Configuration["testSettings:existing-older-commit"];
        protected readonly string MASTER_BRANCH_REFERENCE = Configuration["testSettings:master-branch-reference"];
        protected readonly string DEVELOP_BRANCH_REFERENCE = Configuration["testSettings:develop-branch-reference"];
        protected readonly string MASTER_BRANCH_NAME = Configuration["testSettings:master-branch-name"];
        protected readonly string EXISTING_GROUP = Configuration["testSettings:existing-group"];
        protected readonly string EXISTING_HOOK = Configuration["testSettings:existing-hook"];
        protected readonly int EXISTING_NUMBER_OF_CHANGES = int.Parse(Configuration["testSettings:existing-number-of-changes"]);
        protected readonly string PERSONAL_ACCESS_TOKEN = Configuration["testSettings:personal-access-token"];

        protected StashClient stashClient;

        [TestInitialize]
        public void Initialize()
        {
            stashClient = new StashClient(BASE_URL, TEST_USERNAME, TEST_PASSWORD);
        }

    }
}
