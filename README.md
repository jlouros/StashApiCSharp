[![Build status](https://ci.appveyor.com/api/projects/status/e3wnmyfoklqc306u?svg=true)](https://ci.appveyor.com/project/jlouros/stashapicsharp)

[![NuGet](https://img.shields.io/nuget/v/Atlassian.Stash.Api.svg)](https://www.nuget.org/packages/Atlassian.Stash.Api/)


Bitbucket Server (previously known as Stash) API wrapper for .Net
============================================================================

C# API wrapper for Atlassian Bitbucket Server (previously known as Stash)
For more information about Bitbucket Server visit: https://www.atlassian.com/software/bitbucket/server
API documentation can be found here: https://developer.atlassian.com/bitbucket/server/docs/latest/
Versions 2.0.0 and above of this project target Bitbucket Server version 4.1 and above.
If you are using a older version of Bitbucket Server (below version 4.0), please download any 1.0.* version from NuGet


##Installation

There are 2 ways to use this library:

* Install-Package Atlassian.Stash.Api (via Nuget)
* Download source code and compile
 


##Sample Usage

	//Create a Stash connection
	// Stash client connection using basic authentication
	var client = new StashClient("http://your_stash_server_url:7990/", "username", "password");

	//Gets a list of projects (by default a maximum of 25 results will be return)
	// we recommend use of async/await instead of forcing synchronous execution
	var projects = client.Projects.Get().Result;

	//Gets a list of repositories from project "PROJKEY" (by default a maximum of 25 results will be return)
	// using async
	var repositories = await client.Repositories.Get("PROJKEY");

	//Delete repository "REPOSLUG" from project "PROJKEY"
	await client.Repositories.Delete("PROJKEY", "REPOSLUG");


*Take a look at the integration tests project for more samples.*


[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/jlouros/stashapicsharp/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

