using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppPackager.Pack {
    /// <summary>
    /// The App Packager packer used to pack applications.
    /// </summary>
    public sealed class AppPacker : AppPackagerTool<AppPackagerSettings> {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppPacker"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="resolver">The App Packager tool resolver.</param>
        public AppPacker(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            IAppPackagerResolver resolver) : base(fileSystem, environment, processRunner, tools, resolver) {
            _environment = environment;
        }

        /// <summary>
        /// Create an application package using the specificed output name, content, and setttings.
        /// </summary>
        /// <param name="outputPackage">Output name of the application package.</param>
        /// <param name="contentDirectory">Directory for the content to be pack.</param>
        /// <param name="settings">The settings.</param>
        public void Pack(FilePath outputPackage, DirectoryPath contentDirectory, AppPackagerSettings settings) {
            if (outputPackage == null) {
                throw new ArgumentNullException(nameof(outputPackage));
            }
            if (contentDirectory == null) {
                throw new ArgumentNullException(nameof(contentDirectory));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(outputPackage, contentDirectory, null, settings));
        }

        /// <summary>
        /// Create an application package using the specificed output name, mapping file, and setttings.
        /// </summary>
        /// <param name="outputPackage">Output name of the application package.</param>
        /// <param name="mappingFile">Mapping file to be used.</param>
        /// <param name="settings">The settings.</param>
        public void Pack(FilePath outputPackage, FilePath mappingFile, AppPackagerSettings settings) {
            if (outputPackage == null) {
                throw new ArgumentNullException(nameof(outputPackage));
            }
            if (mappingFile == null) {
                throw new ArgumentNullException(nameof(mappingFile));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(outputPackage, null, mappingFile, settings));
        }

        private ProcessArgumentBuilder GetArguments(FilePath outputPackage, DirectoryPath contentDirectory, FilePath mappingFile, AppPackagerSettings settings) {
            var builder = new ProcessArgumentBuilder();
            builder.Append("pack");

            builder.Append("/p");
            builder.AppendQuoted(outputPackage.MakeAbsolute(_environment).FullPath);

            if (contentDirectory != null) {
                builder.Append("/d");
                builder.AppendQuoted(contentDirectory.MakeAbsolute(_environment).FullPath);
            }

            if (mappingFile != null) {
                builder.Append("/f");
                builder.AppendQuoted(mappingFile.MakeAbsolute(_environment).FullPath);
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
    }
}
