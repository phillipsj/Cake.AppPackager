using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppPackager.Unbundle
{
    public sealed class AppUnbundler : AppPackagerTool<AppPackagerSettings>
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUnbundler"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="resolver">The App Packager tool resolver.</param>
        public AppUnbundler(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools,
            IAppPackagerResolver resolver) : base(fileSystem, environment, processRunner, tools, resolver) {
            _environment = environment;
        }

        /// <summary>
        /// Unpacks all packages to a subdirectory under the specified output path, named after the bundle full name. The output has the same directory structure as the installed package bundle.
        /// </summary>
        /// <param name="inputBundleName">Input name of the application bundle..</param>
        /// <param name="outputDirectory">Output directory to unbundle the application.</param>
        /// <param name="settings">The settings.</param>
        public void Unpack(string inputBundleName, IDirectory outputDirectory, AppPackagerSettings settings)
        {
            if (string.IsNullOrWhiteSpace(inputBundleName))
            {
                throw new ArgumentNullException(nameof(inputBundleName));
            }
            if (outputDirectory == null)
            {
                throw new ArgumentNullException(nameof(outputDirectory));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(inputBundleName, outputDirectory, settings));
        }

        private ProcessArgumentBuilder GetArguments(string inputBundleName, IDirectory outputDirectory, AppPackagerSettings settings)
        {
            var builder = new ProcessArgumentBuilder();
            builder.Append("unbundle");

            builder.Append("/p");
            builder.AppendQuoted(inputBundleName);

            builder.Append("/d");
            builder.AppendQuoted(outputDirectory.ToString());

            AddSwitchArguments(builder, settings);
            return builder;
        }

        private static void AddSwitchArguments(ProcessArgumentBuilder builder, AppPackagerSettings settings)
        {
            builder.AppendSwitch(settings.Localized, "l");
            builder.AppendSwitch(settings.OverwriteOutput, "o");
            builder.AppendSwitch(settings.PreventOverwriteOutput, "no");
            builder.AppendSwitch(settings.SkipSemanticValidation, "nv");
            builder.AppendSwitch(settings.EnableVerboseLogging, "v");
        }
    }
}
