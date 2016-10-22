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
        /// <param name="outputPackageName">Output name of the application package.</param>
        /// <param name="contentDirectory">Directory for the content to be pack.</param>
        /// <param name="settings">The settings.</param>
        public void Pack(string outputPackageName, IDirectory contentDirectory, AppPackagerSettings settings) {
            if (string.IsNullOrWhiteSpace(outputPackageName)) {
                throw new ArgumentNullException(nameof(outputPackageName));
            }
            if (contentDirectory == null) {
                throw new ArgumentNullException(nameof(contentDirectory));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(outputPackageName, contentDirectory, null, settings));
        }

        /// <summary>
        /// Create an application package using the specificed output name, mapping file, and setttings.
        /// </summary>
        /// <param name="outputPackageName">Output name of the application package.</param>
        /// <param name="mappingFile">Mapping file to be used.</param>
        /// <param name="settings">The settings.</param>
        public void Pack(string outputPackageName, IFile mappingFile, AppPackagerSettings settings) {
            if (string.IsNullOrWhiteSpace(outputPackageName)) {
                throw new ArgumentNullException(nameof(outputPackageName));
            }
            if (mappingFile == null) {
                throw new ArgumentNullException(nameof(mappingFile));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(outputPackageName, null, mappingFile, settings));
        }

        private ProcessArgumentBuilder GetArguments(string outputPackageName, IDirectory contentDirectory, IFile mappingFile, AppPackagerSettings settings) {
            var builder = new ProcessArgumentBuilder();
            builder.Append("pack");

            builder.Append("/p");
            builder.AppendQuoted(outputPackageName);

            if (contentDirectory != null) {
                builder.Append("/d");
                builder.AppendQuoted(contentDirectory.ToString());
            }

            if (mappingFile != null) {
                builder.Append("/f");
                builder.AppendQuoted(mappingFile.ToString());
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
