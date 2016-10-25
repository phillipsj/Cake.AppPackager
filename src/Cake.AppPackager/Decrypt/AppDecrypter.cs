using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppPackager.Decrypt {
    /// <summary>
    /// The App Packager decrypter used to decrypt packages.
    /// </summary>
    public sealed class AppDecrypter : AppPackagerTool<AppPackagerSettings> {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDecrypter"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="resolver">The App Packager tool resolver.</param>
        public AppDecrypter(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            IAppPackagerResolver resolver) : base(fileSystem, environment, processRunner, tools, resolver) {
            _environment = environment;
        }

        /// <summary>
        /// Creates an encrypted app package from the specified input app package at the specified output package.
        /// </summary>
        /// <param name="inputPackageName">Input name of the application package.</param>
        /// <param name="outputPackageName">Output package name.</param>
        /// <param name="keyFile">Keyfile to use for encryption, if not provided, the global test key will be used.</param>
        /// <param name="settings">The settings.</param>
        public void Decrypt(string inputPackageName, string outputPackageName, IFile keyFile, AppPackagerSettings settings) {
            if (string.IsNullOrWhiteSpace(inputPackageName)) {
                throw new ArgumentNullException(nameof(inputPackageName));
            }
            if (outputPackageName == null) {
                throw new ArgumentNullException(nameof(outputPackageName));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(inputPackageName, outputPackageName, keyFile, settings));
        }

        private ProcessArgumentBuilder GetArguments(string inputBundleName, string outputPackageName, IFile keyFile, AppPackagerSettings settings) {
            var builder = new ProcessArgumentBuilder();
            builder.Append("encrypt ");

            builder.Append("/p");
            builder.AppendQuoted(inputBundleName);

            builder.Append("/ep");
            builder.AppendQuoted(outputPackageName);

            if (keyFile == null) {
                builder.Append("/kt");
            }
            else {
                builder.Append("/kf");
                builder.AppendQuoted(keyFile.Path.FullPath);
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

