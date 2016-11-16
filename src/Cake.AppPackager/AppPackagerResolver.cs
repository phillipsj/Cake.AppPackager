using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppPackager {
    internal sealed class AppPackagerResolver : IAppPackagerResolver {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;
        private readonly IToolLocator _tools;
        private readonly IRegistry _registry;
        private IFile _cachedPath;

        public AppPackagerResolver(IFileSystem fileSystem, ICakeEnvironment environment, IToolLocator tools, IRegistry registry) {
            if (fileSystem == null) {
                throw new ArgumentNullException(nameof(fileSystem));
            }
            if (environment == null) {
                throw new ArgumentNullException(nameof(environment));
            }
            if (tools == null) {
                throw new ArgumentNullException(nameof(tools));
            }
            if (registry == null) {
                throw new ArgumentNullException(nameof(registry));
            }

            _fileSystem = fileSystem;
            _environment = environment;
            _tools = tools;
            _registry = registry;
        }

        public FilePath ResolvePath() {
            if (_cachedPath != null && _cachedPath.Exists) {
                return _cachedPath.Path;
            }

            var toolsExe = _tools.Resolve("makeappx.exe");
            if (toolsExe != null) {
                var toolsFile = _fileSystem.GetFile(toolsExe);
                if (toolsFile.Exists) {
                    _cachedPath = toolsFile;
                    return _cachedPath.Path;
                }
            }

            _cachedPath = GetFromDisc() ?? GetFromRegistry();
            if (_cachedPath == null) {
                throw new CakeException("Failed to find MakeAppx.exe.");
            }

            return _cachedPath.Path;
        }

        private IFile GetFromDisc() {
            var programFilesPath = _environment.GetSpecialPath(SpecialPath.ProgramFilesX86);

            var files = new List<FilePath>();
            if (_environment.Platform.Is64Bit) {
                files.Add(programFilesPath.Combine(@"Windows Kits\10\bin\x64").CombineWithFilePath("makeappx.exe"));
                files.Add(programFilesPath.Combine(@"Windows Kits\8.1\bin\x64").CombineWithFilePath("makeappx.exe"));
                files.Add(programFilesPath.Combine(@"Windows Kits\8.0\bin\x64").CombineWithFilePath("makeappx.exe"));
            }
            else {
                files.Add(programFilesPath.Combine(@"Windows Kits\10\bin\x86").CombineWithFilePath("makeappx.exe"));
                files.Add(programFilesPath.Combine(@"Windows Kits\8.1\bin\x86").CombineWithFilePath("makeappx.exe"));
                files.Add(programFilesPath.Combine(@"Windows Kits\8.0\bin\x86").CombineWithFilePath("makeappx.exe"));
            }


            return _fileSystem.GetFile(files.FirstOrDefault(file => _fileSystem.Exist(file)));
        }

        private IFile GetFromRegistry() {
            using (var root = _registry.LocalMachine.OpenKey("Software\\Microsoft\\Windows Kits\\Install Roots")) {
                if (root == null) {
                    return null;
                }

                var keyName = root.GetSubKeyNames();
                foreach (var key in keyName) {
                    var sdkKey = root.OpenKey(key);
                    var installationFolder = sdkKey?.GetValue("KitsRoot10") as string;
                    if (string.IsNullOrWhiteSpace(installationFolder)) continue;
                    var installationPath = new DirectoryPath(installationFolder);
                    var appPackagerToolPath = installationPath.CombineWithFilePath("bin\\x64\\makeappx.exe");

                    if (_fileSystem.Exist(appPackagerToolPath)) {
                        return _fileSystem.GetFile(appPackagerToolPath);
                    }
                }
            }
            return null;
        }
    }
}
