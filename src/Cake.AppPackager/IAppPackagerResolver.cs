using Cake.Core.IO;

namespace Cake.AppPackager
{
    public interface IAppPackagerResolver
    {
        /// <summary>
        /// Resolves the path to the AppPackager (MakeAppx) tool.
        /// </summary>
        /// <returns>The path to the AppPackager tool.</returns>
        FilePath ResolvePath();
    }
}
