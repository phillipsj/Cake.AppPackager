using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using Should;
using Should.Core.Assertions;

namespace Cake.AppPackager.Tests
{
    public sealed class AppUnpackerTests
    {
        public void ShouldThrowIfAppUnpackerExecutableWasNotFound()
        {
            // Given
            var fixture = new AppUnpackerFixture() { InputPackage = "test.appx", OutputDirectory = "output" };
            fixture.GivenDefaultToolDoNotExist();

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<CakeException>();
            result.Message.ShouldEqual("App Packager: Could not locate executable.");
        }

        public void ShouldThrowIfInputPackageIsNull()
        {
            // Given
            var fixture = new AppUnpackerFixture() { OutputDirectory = "output" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("inputPackage");
        }

        public void ShouldThrowIfOutputDirectoryIsNull()
        {
            // Given
            var fixture = new AppUnpackerFixture() { InputPackage = "test.appx" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("outputDirectory");
        }
    }
}
