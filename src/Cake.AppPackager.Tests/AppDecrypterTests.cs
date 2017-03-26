using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using Should;
using Xunit;
using Record = Should.Core.Assertions.Record;

namespace Cake.AppPackager.Tests
{
    public sealed class AppDecrypterTests
    {
        [Fact]
        public void ShouldThrowIfAppPackerExecutableWasNotFound()
        {
            //InputPackage, OutputPackage, KeyFile, Settings
            // Given
            var fixture = new AppDecrypterFixture() { InputPackage = "test.appx", OutputPackage = "test.appx" };
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
            var fixture = new AppDecrypterFixture() { OutputPackage = "test.appx" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("inputPackage");
        }

        [Fact]
        public void ShouldThrowIfOutputPackageIsNull()
        {
            // Given
            var fixture = new AppDecrypterFixture() { InputPackage = "test.appx" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("outputPackage");
        }
    }
}
