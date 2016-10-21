using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppPackager {
    /// <summary>
    /// The App Packager (makeappx) assembly runner.
    /// </summary>
    public sealed class AppPackagerRunner : Tool<AppPackagerSettings> {
        private readonly IAppPackagerResolver _resolver;
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppPackagerRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="registry">The registry.</param>
        public AppPackagerRunner(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            IRegistry registry) : this(fileSystem, environment, processRunner, tools, registry, null) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="AppPackagerRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="registry">The registry.</param>
        /// <param name="resolver">The resolver.</param>
        internal AppPackagerRunner(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            IRegistry registry,
            IAppPackagerResolver resolver) : base(fileSystem, environment, processRunner, tools) {
            _fileSystem = fileSystem;
            _environment = environment;
            _resolver = resolver ?? new AppPackagerResolver(_fileSystem, _environment, registry);
        }

        public void Run(AppPackagerCommand command, AppPackagerSettings settings) {
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(command, settings));
        }

        private ProcessArgumentBuilder GetArguments(AppPackagerCommand command, AppPackagerSettings settings)
        {
            var builder = new ProcessArgumentBuilder();
            switch(command){
                case AppPackagerCommand.Pack:
                    builder.Append("pack");
                    break;
                case AppPackagerCommand.Unpack:
                    builder.Append("unpack");
                    break;
                case AppPackagerCommand.Bundle:
                    builder.Append("bundle");
                    break;
                case AppPackagerCommand.Unbundle:
                    builder.Append("unbundle");
                    break;
                case AppPackagerCommand.Encrypt:
                    builder.Append("encrypt");
                    break;
                case AppPackagerCommand.Decrypt:
                    builder.Append("decrypt");
                    break;
                default:
                    throw new ArgumentNullException(nameof(command), "A valid command was not specified.");
            }

            AddSwitchArguments(builder, settings);
            return builder;
        }

        private static void AddSwitchArguments(ProcessArgumentBuilder builder, AppPackagerSettings settings) {
            builder.AppendSwitch(settings.Localized, "l");
            builder.AppendSwitch(settings.OverwriteOutput, "o");
            builder.AppendSwitch(settings.PreventOverwriteOutput, "no");
            builder.AppendSwitch(settings.SkipSemanticValidation, "nv");
            builder.AppendSwitch(settings.EnableVerboseLogging, "v");
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>
        /// The name of the tool (<c>App Packager</c>).
        /// </returns>
        protected override string GetToolName() {
            return "App Packager";
        }

        /// <summary>
        /// Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames() {
            return new[] {"makeappx.exe"};
        }

        /// <summary>
        /// Gets alternative file paths which the tool may exist in
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The default tool path.</returns>
        protected override IEnumerable<FilePath> GetAlternativeToolPaths(AppPackagerSettings settings) {
            var path = _resolver.GetPath();
            return path != null
                ? new[] {path}
                : Enumerable.Empty<FilePath>();
        }
    }
}
