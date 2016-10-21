using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.AppPackager
{
    internal sealed class AppPackagerResolver : IAppPackagerResolver
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;
        private readonly IRegistry _registry;
        private FilePath _appPackagerToolPath;

        public AppPackagerResolver(IFileSystem fileSystem, ICakeEnvironment environment, IRegistry registry)
        {
            _fileSystem = fileSystem;
            _environment = environment;
            _registry = registry;

            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }
        }

        public FilePath GetPath()
        {
            if (_appPackagerToolPath != null)
            {
                return _appPackagerToolPath;
            }

            _appPackagerToolPath = GetFromDisc() ?? GetFromRegistry();
            if (_appPackagerToolPath == null)
            {
                throw new CakeException("Failed to find MakeAppx.exe.");
            }

            return _appPackagerToolPath;
        }

        private FilePath GetFromDisc()
        {
            var programFilesPath = _environment.Platform.Is64Bit
                ? _environment.GetSpecialPath(SpecialPath.ProgramFilesX86)
                : _environment.GetSpecialPath(SpecialPath.ProgramFiles);
            
            var files = new List<FilePath>();
            if (_environment.Platform.Is64Bit)
            {
                files.Add(programFilesPath.Combine(@"Windows Kits\10\bin\x64").CombineWithFilePath("makeappx.exe"));
                files.Add(programFilesPath.Combine(@"Windows Kits\8.1\bin\x64").CombineWithFilePath("makeappx.exe"));
                files.Add(programFilesPath.Combine(@"Windows Kits\8.0\bin\x64").CombineWithFilePath("makeappx.exe"));
            }
            else
            {
                files.Add(programFilesPath.Combine(@"Windows Kits\10\bin\x64").CombineWithFilePath("makeappx.exe"));
                files.Add(programFilesPath.Combine(@"Windows Kits\8.1\bin\x86").CombineWithFilePath("makeappx.exe"));
                files.Add(programFilesPath.Combine(@"Windows Kits\8.0\bin\x86").CombineWithFilePath("makeappx.exe"));
            }

            return files.FirstOrDefault(file => _fileSystem.Exist(file));
        }

        private FilePath GetFromRegistry()
        {
            using (var root = _registry.LocalMachine.OpenKey("Software\\Microsoft\\Microsoft SDKs\\Windows"))
            {
                if (root == null)
                {
                    return null;
                }

                var keyName = root.GetSubKeyNames();
                foreach (var key in keyName)
                {
                    var sdkKey = root.OpenKey(key);
                    var installationFolder = sdkKey?.GetValue("InstallationFolder") as string;
                    if (string.IsNullOrWhiteSpace(installationFolder)) continue;
                    var installationPath = new DirectoryPath(installationFolder);
                    var signToolPath = installationPath.CombineWithFilePath("bin\\makeappx.exe");

                    if (_fileSystem.Exist(signToolPath))
                    {
                        return signToolPath;
                    }
                }
            }
            return null;
        }
    }
}
