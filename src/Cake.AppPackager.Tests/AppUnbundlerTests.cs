using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using Should;
using Xunit;
using Record = Should.Core.Assertions.Record;

namespace Cake.AppPackager.Tests
{
    public sealed class AppUnbundlerTests
    {
        [Fact]
        public void ShouldThrowIfAppUnbundlerExecutableWasNotFound()
        {
            // Given
            var fixture = new AppUnbundlerFixture() { InputBundle = "test.appx", OutputDirectory = "output" };
            fixture.GivenDefaultToolDoNotExist();

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<CakeException>();
            result.Message.ShouldEqual("App Packager: Could not locate executable.");
        }

        [Fact]
        public void ShouldThrowIfInputPackageIsNull()
        {
            // Given
            var fixture = new AppUnbundlerFixture() { OutputDirectory = "output" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("inputBundle");
        }

        [Fact]
        public void ShouldThrowIfOutputDirectoryIsNull()
        {
            // Given
            var fixture = new AppUnbundlerFixture() { InputBundle = "test.appx" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("outputDirectory");
        }
    }
}
