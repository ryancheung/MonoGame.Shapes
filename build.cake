//#tool nuget:?package=vswhere&version=2.6.7

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("build-target", "Default");
var version = Argument("build-version", EnvironmentVariable("BUILD_NUMBER") ?? "0.3.0");
var configuration = Argument("build-configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

MSBuildSettings msPackSettings, mdPackSettings;
DotNetCoreMSBuildSettings dnBuildSettings;
DotNetCorePackSettings dnPackSettings;

private void PackProject(string filePath)
{
    MSBuild(filePath, msPackSettings);
}

//private bool GetMSBuildWith(string requires)
//{
//    if (IsRunningOnWindows())
//    {
//        DirectoryPath vsLatest = VSWhereLatest(new VSWhereLatestSettings { Requires = requires });
//
//        if (vsLatest != null)
//        {
//            var files = GetFiles(vsLatest.FullPath + "/**/MSBuild.exe");
//            if (files.Any())
//            {
//                msPackSettings.ToolPath = files.First();
//                return true;
//            }
//        }
//    }
//
//    return false;
//}

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Prep")
    .Does(() =>
{
    // We tag the version with the build branch to make it
    // easier to spot special builds in NuGet feeds.
    var branch = EnvironmentVariable("GIT_BRANCH") ?? string.Empty;
    if (branch == "develop")
	version += "-develop";

    Console.WriteLine("Build Version: {0}", version);

    msPackSettings = new MSBuildSettings();
    msPackSettings.Verbosity = Verbosity.Minimal;
    msPackSettings.Configuration = configuration;
    msPackSettings.WithProperty("Version", version);
    msPackSettings.WithTarget("Pack");

    mdPackSettings = new MSBuildSettings();
    mdPackSettings.Verbosity = Verbosity.Minimal;
    mdPackSettings.Configuration = configuration;
    mdPackSettings.WithProperty("Version", version);
    mdPackSettings.WithTarget("PackageAddin");

    dnBuildSettings = new DotNetCoreMSBuildSettings();
    dnBuildSettings.WithProperty("Version", version);

    dnPackSettings = new DotNetCorePackSettings();
    dnPackSettings.MSBuildSettings = dnBuildSettings;
    dnPackSettings.Verbosity = DotNetCoreVerbosity.Minimal;
    dnPackSettings.Configuration = configuration;
});

Task("Default")
    .IsDependentOn("Prep")
    .Does(() =>
{
    DotNetCoreRestore("MonoGame.Shapes/MonoGame.Shapes.csproj");
    PackProject("MonoGame.Shapes/MonoGame.Shapes.csproj");
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
