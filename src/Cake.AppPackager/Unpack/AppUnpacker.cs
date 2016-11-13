using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppPackager.Unpack {
    /// <summary>
    /// The App Packager unpacker used to unpack applications.
    /// </summary>
    public sealed class AppUnpacker : AppPackagerTool<AppPackagerSettings> {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUnpacker"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="resolver">The App Packager tool resolver.</param>
        public AppUnpacker(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            IAppPackagerResolver resolver) : base(fileSystem, environment, processRunner, tools, resolver) {
            _environment = environment;
        }

        /// <summary>
        /// Unpacks an application package with the same structure as installed package.
        /// </summary>
        /// <param name="inputPackage">Application package to unpack.</param>
        /// <param name="outputDirectory">Output directory to unpack the application.</param>
        /// <param name="settings">The settings.</param>
        public void Unpack(IFile inputPackage, DirectoryPath outputDirectory, AppPackagerSettings settings) {
            if (inputPackage == null) {
                throw new ArgumentNullException(nameof(inputPackage));
            }
            if (outputDirectory == null) {
                throw new ArgumentNullException(nameof(outputDirectory));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(inputPackage, outputDirectory, settings));
        }
        
        private ProcessArgumentBuilder GetArguments(IFile inputPackage, DirectoryPath outputDirectory, AppPackagerSettings settings) {
            var builder = new ProcessArgumentBuilder();
            builder.Append("unpack");

            builder.Append("/p");
            builder.AppendQuoted(inputPackage.Path.MakeAbsolute(_environment).FullPath);

            builder.Append("/d");
            builder.AppendQuoted(outputDirectory.MakeAbsolute(_environment).FullPath);

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
    }
}
