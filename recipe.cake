#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context, 
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.AppPackager",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.AppPackager",
                            appVeyorAccountName: "cake-contrib",
                            shouldRunDupFinder: false,
                            shouldRunInspectCode: false,
                            shouldRunGitVersion: DirectoryExists(".git"), // This would allow building even without using a git repository
                            shouldRunDotNetCorePack: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] { 
                                BuildParameters.RootDirectoryPath + "/src/Cake.AppPackager.Tests/*.cs" },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[DotNet.Xdt]* -[FluentAssertions]*",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs;*/Microsoft.Build.Framework.Version.cs");

Build.RunDotNetCore();