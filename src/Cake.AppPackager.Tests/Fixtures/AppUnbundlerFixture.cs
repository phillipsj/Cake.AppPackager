using Cake.AppPackager.Unbundle;
using Cake.Core.IO;

namespace Cake.AppPackager.Tests.Fixtures {
    internal sealed class AppUnbundlerFixture : AppPackagerFixture {
        public FilePath InputBundle { get; set; }
        public DirectoryPath OutputDirectory { get; set; }

        protected override void RunTool() {
            var tool = new AppUnbundler(FileSystem, Environment, ProcessRunner, Tools, Resolver);
            tool.Unpack(InputBundle, OutputDirectory, Settings);
        }
    }
}
