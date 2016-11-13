using Cake.AppPackager.Encrypt;
using Cake.Core.IO;

namespace Cake.AppPackager.Tests.Fixtures {
    internal sealed class AppEncrypterFixture : AppPackagerFixture {
        public FilePath InputPackage { get; set; }
        public FilePath OutputPackage { get; set; }
        public FilePath KeyFile { get; set; }

        protected override void RunTool() {
            var tool = new AppEncrypter(FileSystem, Environment, ProcessRunner, Tools, Resolver);
            tool.Encrypt(InputPackage, OutputPackage, KeyFile, Settings);
        }
    }
}
