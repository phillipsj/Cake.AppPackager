using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppPackager.Bundle {
    /// <summary>
    /// The App Packager bundler used to bundle applications.
    /// </summary>
    public sealed class AppBundler : AppPackagerTool<AppPackagerSettings> {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBundler"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="resolver">The App Packager tool resolver.</param>
        public AppBundler(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            IAppPackagerResolver resolver) : base(fileSystem, environment, processRunner, tools, resolver) {
            _environment = environment;
        }

        /// <summary>
        /// Create an application bundle using the specificed output name, content, and setttings.
        /// </summary>
        /// <param name="outputBundleName">Output name of the application bundle.</param>
        /// <param name="contentDirectory">Directory for the content to be pack.</param>
        /// <param name="settings">The settings.</param>
        public void Bundle(string outputBundleName, DirectoryPath contentDirectory, AppPackagerSettings settings) {
            if (string.IsNullOrWhiteSpace(outputBundleName)) {
                throw new ArgumentNullException(nameof(outputBundleName));
            }
            if (contentDirectory == null) {
                throw new ArgumentNullException(nameof(contentDirectory));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(outputBundleName, contentDirectory, null, settings));
        }

        /// <summary>
        /// Create an application package using the specificed output name, mapping file, and setttings.
        /// </summary>
        /// <param name="outputBundleName">Output name of the application package.</param>
        /// <param name="mappingFile">Mapping file to be used.</param>
        /// <param name="settings">The settings.</param>
        public void Bundle(string outputBundleName, IFile mappingFile, AppPackagerSettings settings) {
            if (string.IsNullOrWhiteSpace(outputBundleName)) {
                throw new ArgumentNullException(nameof(outputBundleName));
            }
            if (mappingFile == null) {
                throw new ArgumentNullException(nameof(mappingFile));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(outputBundleName, null, mappingFile, settings));
        }

        private ProcessArgumentBuilder GetArguments(string outputBundleName, DirectoryPath contentDirectory, IFile mappingFile, AppPackagerSettings settings) {
            var builder = new ProcessArgumentBuilder();
            builder.Append("bundle");

            builder.Append("/p");
            builder.AppendQuoted(outputBundleName);

            if (contentDirectory != null) {
                builder.Append("/d");
                builder.AppendQuoted(contentDirectory.MakeAbsolute(_environment).FullPath);
            }

            if (mappingFile != null) {
                builder.Append("/f");
                builder.AppendQuoted(mappingFile.Path.MakeAbsolute(_environment).FullPath);
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
