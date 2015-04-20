[![Build status](https://ci.appveyor.com/api/projects/status/e3wnmyfoklqc306u?svg=true)](https://ci.appveyor.com/project/jlouros/stashapicsharp)

[![NuGet](https://img.shields.io/nuget/v/Atlassian.Stash.Api.svg)](https://www.nuget.org/packages/Atlassian.Stash.Api/)


Atlassian Stash API .Net
=================

C# API wrapper for Atlassian Stash

For more information on TeamCity visit: https://www.atlassian.com/software/stash


##Installation
There are 2 ways to use this library:

* Install-Package Atlassian.Stash.Api -Pre (via Nuget)
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

