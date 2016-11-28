using Cake.AppPackager.Unpack;
using Cake.Core.IO;

namespace Cake.AppPackager.Tests.Fixtures {
    internal sealed class AppUnpackerFixture : AppPackagerFixture {
        public FilePath InputPackage { get; set; }
        public DirectoryPath OutputDirectory { get; set; }

        protected override void RunTool() {
            var tool = new AppUnpacker(FileSystem, Environment, ProcessRunner, Tools, Resolver);
            tool.Unpack(InputPackage, OutputDirectory, Settings);
        }
    }
}
