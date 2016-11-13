using Cake.AppPackager.Bundle;
using Cake.Core.IO;

namespace Cake.AppPackager.Tests.Fixtures {
    internal sealed class AppBundlerFixture : AppPackagerFixture {
        public FilePath OutputBundle { get; set; }
        public DirectoryPath ContentDirectory { get; set; }

        protected override void RunTool() {
            var tool = new AppBundler(FileSystem, Environment, ProcessRunner, Tools, Resolver);
            tool.Bundle(OutputBundle, ContentDirectory, Settings);
        }
    }
}
