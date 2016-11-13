using Cake.AppPackager.Pack;
using Cake.Core.IO;

namespace Cake.AppPackager.Tests.Fixtures {
    internal sealed class AppPackerFixture : AppPackagerFixture {
        public FilePath OutputPackage { get; set; }
        public DirectoryPath ContentDirectory { get; set; }
        
        protected override void RunTool() {
            var tool = new AppPacker(FileSystem, Environment, ProcessRunner, Tools, Resolver);
            tool.Pack(OutputPackage, ContentDirectory, Settings);
        }
    }
}
