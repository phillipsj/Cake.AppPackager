using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using Xunit;

namespace Cake.AppPackager.Tests
{
    public sealed class AppUnpackerTests
    {
        [Fact]
        public void ShouldThrowIfAppUnpackerExecutableWasNotFound()
        {
            // Given
            var fixture = new AppUnpackerFixture() { InputPackage = "test.appx", OutputDirectory = "output" };
            fixture.GivenDefaultToolDoNotExist();

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("App Packager: Could not locate executable.", result.Message);
        }

        [Fact]
        public void ShouldThrowIfInputPackageIsNull()
        {
            // Given
            var fixture = new AppUnpackerFixture() { OutputDirectory = "output" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("inputPackage", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void ShouldThrowIfOutputDirectoryIsNull()
        {
            // Given
            var fixture = new AppUnpackerFixture() { InputPackage = "test.appx" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("outputDirectory", ((ArgumentNullException)result).ParamName);
        }
    }
}
