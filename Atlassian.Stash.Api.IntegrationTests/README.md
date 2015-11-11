# Integration tests project

This project is designed to run against a local instance of Bitbucket Server.
Modify 'App.config' with your local configuration settings.


## Setting up Integrations to run locally

 * Local running instance at 'http://localhost:7990/'
 * Create a new user account using the following credentials 'TestUser':'password'
 * Go to global permissions and add 'Administrator' access to 'TestUser' (need to revisit this)
 * Create a new group named 'TestGroup'
 * Add user 'TestUser' to group 'TestGroup'
 * Create new project named 'test'
 * In 'test' project, create a repository named 'TestRepository'
 * On the repository permission settings add 'TestGroup' 
 * create new local git repository
 * add a file the local repository called 'test.txt'
 * commit and push the changes to your local Bitbucket Server, targeting 'TestRepository'
 * make a change in 'text.txt' file; commit and push this changes (now you should have at least 2 commits)
 * create a new tag using the following command => git tag -a TestTag -m 'my test tag'
 * push the new tag to the server using => git push origin --tags
 * check all commits http://localhost:7990/projects/TEST/repos/testrepository/commits and modify 'App.config' to set the commits information
 * Go to http://localhost:7990/plugins/servlet/branch-permissions/TEST/testrepository and enable 'branch permissions'
 * 'Add branch permission' to prevent 'master' branch deletion for everyone
 * create a new branch locally
 * make another change in 'text.txt'; commit and push this change to the server (you should have 2 branches now)
 * create a pull request from the new branch targeting master

 
## To be done

Create different groups and users for specific permission access (TestGroup-Read, TestUser-Admin, so on...)
Automate creation/validation initial setup process.
Block any test execution if the validation script fails.
Add more tests.