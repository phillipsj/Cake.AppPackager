using Cake.Testing.Fixtures;
using NSubstitute;

namespace Cake.AppPackager.Tests.Fixtures {
    internal abstract class AppPackagerFixture : ToolFixture<AppPackagerSettings> {
        public IAppPackagerResolver Resolver { get; set; }

        protected AppPackagerFixture() : base("makeappx.exe") {
            Resolver = Substitute.For<IAppPackagerResolver>();
        }
    }
}
