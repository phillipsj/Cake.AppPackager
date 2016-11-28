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
        /// Creates an decrypted app package from the specified input app package at the specified output package.
        /// </summary>
        /// <param name="inputPackage">Input name of the application package.</param>
        /// <param name="outputPackage">Output package name.</param>
        /// <param name="keyFile">Keyfile to use for encryption, if not provided, the global test key will be used.</param>
        /// <param name="settings">The settings.</param>
        public void Decrypt(FilePath inputPackage, FilePath outputPackage, FilePath keyFile, AppPackagerSettings settings) {
            if (inputPackage == null) {
                throw new ArgumentNullException(nameof(inputPackage));
            }
            if (outputPackage == null) {
                throw new ArgumentNullException(nameof(outputPackage));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(inputPackage, outputPackage, keyFile, settings));
        }

        private ProcessArgumentBuilder GetArguments(FilePath inputPackage, FilePath outputPackage, FilePath keyFile, AppPackagerSettings settings) {
            var builder = new ProcessArgumentBuilder();
            builder.Append("encrypt ");

            builder.Append("/p");
            builder.AppendQuoted(inputPackage.MakeAbsolute(_environment).FullPath);

            builder.Append("/ep");
            builder.AppendQuoted(outputPackage.MakeAbsolute(_environment).FullPath);

            if (keyFile == null) {
                builder.Append("/kt");
            }
            else {
                builder.Append("/kf");
                builder.AppendQuoted(keyFile.MakeAbsolute(_environment).FullPath);
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

