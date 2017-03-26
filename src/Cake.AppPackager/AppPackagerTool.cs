using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.AppPackager {
    /// <summary>
    /// Base class for all App Packager related tools.
    /// </summary>
    /// <typeparam name="TSettings">The settings type.</typeparam>
    public abstract class AppPackagerTool<TSettings> : Tool<TSettings> where TSettings : ToolSettings {
        private readonly IAppPackagerResolver _resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppPackagerTool{TSettings}" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        /// <param name="resolver">The App Packager tool resolver.</param>
        protected AppPackagerTool(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools, IAppPackagerResolver resolver) : base(fileSystem, environment, processRunner, tools) {
            _resolver = resolver;
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>
        /// The name of the tool (<c>App Packager</c>).
        /// </returns>
        protected sealed override string GetToolName() {
            return "App Packager";
        }

        /// <summary>
        /// Gets the possible names of the tool executable.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected sealed override IEnumerable<string> GetToolExecutableNames() {
            return new[] {"makeappx.exe"};
        }

        /// <summary>
        /// Gets alternative file paths which the tool may exist in
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The default tool path.</returns>
        protected sealed override IEnumerable<FilePath> GetAlternativeToolPaths(TSettings settings) {
            var path = _resolver.ResolvePath();
            return path != null
                ? new[] {path}
                : Enumerable.Empty<FilePath>();
        }
    }
}
