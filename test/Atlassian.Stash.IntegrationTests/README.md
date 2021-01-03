# Integration tests project

This project is designed to run against a local instance of Bitbucket Server.
Modify 'App.config' with your local configuration settings.

## Setting up Integrations to run locally

### Step 1: Setting up Bitbucket instance with Docker containers by running the following commands:
	1. docker volume create --name bitbucketVolume
	2. docker run -v bitbucketVolume:/var/atlassian/application-data/bitbucket --name="bitbucket" -d -p 7990:7990 -p 7999:7999 atlassian/bitbucket-server

### Step 2: Setting up Bitbucket for integration tests:
	1. Setup license for Bitbucket
	2. Create a new user account using the following credentials 'TestUser':'password'
	3. Go to global permissions and add 'Administrator' access to 'TestUser' (need to revisit this)
	4. Create a new user account using the following credentials 'OtherTestUser':'password'
	5. Go to global permissions and add 'Administrator' access to 'OtherTestUser' (need to revisit this)
	6. Create a new group named 'TestGroup'
	7. Add user 'TestUser' to group 'TestGroup'
	8. Login as 'TestUser' and create a personal access token (you need this in Step 4)
	9. Create new project named 'test'
	10 In 'test' project, create a repository named 'TestRepository'
	11. On the repository permission settings add 'TestGroup' with "Write" permissions
	12. Create a pullrequest from the 'develop' to the 'master' branch (This pullrequest need to be recreated everytime the 'Merge_PullRequest' test has run)

### Step 3: Run script for creating a Git repository containing history
	```
	mkdir TestRepository
	cd TestRepository
	echo "initial set of text">test.txt
	mkdir folder
	cd folder
	echo "in subfolder">test.txt
	cd ..
	mkdir "my folder"
	cd "my folder"
	echo "in subfolder with spaces">"my test.txt"

	cd..
	git init
	git add --all
	git commit -m "Initial Commit"
	git remote add origin http://TestUser@localhost:7990/scm/test/testrepository.git
	git push -u origin master

	git tag -a TestTag -m "my test tag"
	git push origin --tags

	echo "more text">test.txt
	git add --all
	git commit -m "small change"
	git push -u origin master

	git branch develop
	git checkout develop

	echo "even more text">test.txt
	git add --all
	git commit -m "develop change"
	git push --set-upstream origin develop
	```

### Step 4: Setting up App.config with correct configuration
	1. Add the last and second last commit SHA as the value for the keys 'existing-commit' and 'existing-older-commit'
	2. Add the personal access token as the value for 'personal-access-token'

## To be done
	* Create different groups and users for specific permission access (TestGroup-Read, TestUser-Admin, so on...)
	* Automate creation/validation initial setup process.
	* Block any test execution if the validation script fails.
	* Add more tests.
	* Fix 'Merge_PullRequest' test to create a commit and pullrequest, because this need to be done manually every time the test has ran.
	* Initial script set branch permissions
	* Create a admin user, do not use TestUser as admin

## Docker

```
docker run --name="bitbucket" -d -p 7990:7990 -p 7999:7999 atlassian/bitbucket-server
```