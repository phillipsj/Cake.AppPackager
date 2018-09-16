using Cake.Core.IO;

namespace Cake.AppPackager
{
    /// <summary>
    /// Represents an AppPackager tool resolver.
    /// </summary>
    public interface IAppPackagerResolver
    {
        /// <summary>
        /// Resolves the path to the AppPackager (MakeAppx) tool.
        /// </summary>
        /// <returns>The path to the AppPackager tool.</returns>
        FilePath ResolvePath();
    }
}
