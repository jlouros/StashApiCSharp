//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var msBuildVerbosity = Argument<Verbosity>("msBuildVerbosity", Verbosity.Normal);
var packageVersion = Argument<string>("packageVersion", "");
var nugetApiKey = Argument<string>("nugetApiKey", "");


//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var mainSln = "./Atlassian.Stash.sln";


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore(mainSln, new NuGetRestoreSettings 
    { 
        MSBuildVersion = NuGetMSBuildVersion.MSBuild14
    });
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        MSBuild(mainSln, new MSBuildSettings 
        {
            Configuration = configuration,
            Verbosity = msBuildVerbosity,
        });
    }
    else
    {
        // don't want to provide this option before test it
        throw new Exception("Unsupported platform: build must be performed on a Windows machine.");
    }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    MSTest("./test/**/bin/**/*.UnitTests.dll");
});

Task("Prepare-NuGet-Package")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    var net40Files = GetFiles("./src/Atlassian.Stash.Net40/bin/" + configuration + "/Atlassian.Stash.*");
    var net45Files = GetFiles("./src/Atlassian.Stash.Net45/bin/" + configuration + "/Atlassian.Stash.*");

    CleanDirectory("./buildOutput/");

    CreateDirectory("./buildOutput/lib/net40/");
    CreateDirectory("./buildOutput/lib/net45/");

    CopyFiles(net40Files, "./buildOutput/lib/net40/");
    CopyFiles(net45Files, "./buildOutput/lib/net45/");

    CopyFileToDirectory("./.nuspec/Atlassian.Stash.nuspec", "./buildOutput/");
});

Task("Create-NuGet-Package")
    .IsDependentOn("Prepare-NuGet-Package")
    .Does(() =>
{

    // remember to set 'packageVersion' argument, otherwise the next step will fail
    var nuGetPackSettings = new NuGetPackSettings 
    { 
        Version = packageVersion,
        OutputDirectory = "./buildOutput"
    };

    NuGetPack("./buildOutput/Atlassian.Stash.nuspec", nuGetPackSettings);
    
});

Task("Publish-NuGet-Package")
    .IsDependentOn("Create-NuGet-Package")
    .Does(() =>
{

    // Get the path to the package.
    var package = "./buildOutput/Atlassian.Stash.Api." + packageVersion +".nupkg";
                
    // Push the package.
    NuGetPush(package, new NuGetPushSettings 
    {
        Source = "https://www.nuget.org/",
        ApiKey = nugetApiKey
    });
    
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Prepare-NuGet-Package");


//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);