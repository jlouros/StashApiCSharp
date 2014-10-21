Atlassian Stash API .Net
=================

C# API wrapper for Atlassian Stash

For more information on TeamCity visit: https://www.atlassian.com/software/stash


##Installation
There are 2 ways to use this library:

* Install-Package Atlassian.Stash.Api -Pre (via Nuget)
* Download source code and compile

##Sample Usage
To get a list of projects

    var client = new StashClient("http://your_stash_server_url:7990/", "username", "password");
	// we recommend use of async/await instead of forcing synchronous execution
    var projects = client.GetProjectsAsync().Result;
	
Also take a look at the integration tests project for more samples.