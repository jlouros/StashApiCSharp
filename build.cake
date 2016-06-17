//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var msBuildVerbosity = Argument<Verbosity>("msBuildVerbosity", Verbosity.Normal);


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

Task("Create-NuGet-Package")
    .IsDependentOn("Build")
    .Does(() =>
{
    var net40DirOutput = Directory("./src/Atlassian.Stash.Net40/bin/" + configuration);
	var net45DirOutput = Directory("./src/Atlassian.Stash.Net45/bin/" + configuration);


    CleanDirectory("./buildOutput/");

    CreateDirectory("./buildOutput/lib/net40/");
    CreateDirectory("./buildOutput/lib/net45/");

    CopyDirectory(net40DirOutput, "./buildOutput/lib/net40/");
    CopyDirectory(net45DirOutput, "./buildOutput/lib/net45/");

    CopyFileToDirectory("./.nuspec/Atlassian.Stash.nuspec", "./buildOutput/");

    var nuGetPackSettings = new NuGetPackSettings 
    { 
        Version = "3.0",
        OutputDirectory = "./buildOutput"
    };

    NuGetPack("./buildOutput/Atlassian.Stash.nuspec", nuGetPackSettings);
    
});



//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Create-NuGet-Package");


//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);