using Cake.AppPackager.Decrypt;
using Cake.Core.IO;

namespace Cake.AppPackager.Tests.Fixtures {
    internal sealed class AppDecrypterFixture : AppPackagerFixture {
        public FilePath InputPackage { get; set; }
        public FilePath OutputPackage { get; set; }
        public FilePath KeyFile { get; set; }

        protected override void RunTool() {
            var tool = new AppDecrypter(FileSystem, Environment, ProcessRunner, Tools, Resolver);
            tool.Decrypt(InputPackage, OutputPackage, KeyFile, Settings);
        }
    }
}