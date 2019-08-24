# Cake.AppPackager

A Cake Addin for [App packager](https://msdn.microsoft.com/en-us/library/windows/desktop/hh446767(v=vs.85).aspx).

[![Build status](https://ci.appveyor.com/api/projects/status/ml27muqhq94g4ixy?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-apppackager)

[![beta-cake-addins MyGet Build Status](https://www.myget.org/BuildSource/Badge/beta-cake-addins?identifier=c7cc134c-76de-4521-866e-77369a097ab0)](https://www.myget.org/)

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)

[![Join the chat at https://gitter.im/cake-build/cake](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/cake-build/cake?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Functionality

Supports all the current command line options provided by App Packager

```cmd

```

## Usage

To use the addin just add it to Cake call the aliases and configure any settings you want.

```csharp

#addin"nuget:?package=Cake.AppPackager"

Task("BuildAppPackage")
    .Does(() => {
        AppPack("test.appx", "package-content", new AppPackagerSettings { OverwriteOutput = true });
});
...

```

Thats it.

Hope you enjoy using.
