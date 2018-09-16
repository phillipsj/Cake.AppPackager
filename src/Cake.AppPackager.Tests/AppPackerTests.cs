using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using Xunit;

namespace Cake.AppPackager.Tests
{
    public sealed class AppPackerTests
    {
        [Fact]
        public void ShouldThrowIfAppPackerExecutableWasNotFound()
        {
            // Given
            var fixture = new AppPackerFixture() { OutputPackage = "test.appx", ContentDirectory = "content" };
            fixture.GivenDefaultToolDoNotExist();

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("App Packager: Could not locate executable.", result.Message);
        }

        [Fact]
        public void ShouldThrowIfOutputPackageIsNull()
        {
            // Given
            var fixture = new AppPackerFixture() { ContentDirectory = "content" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("outputPackage", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void ShouldThrowIfContentDirectoryIsNull()
        {
            // Given
            var fixture = new AppPackerFixture() { OutputPackage = "test.appx" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("contentDirectory", ((ArgumentNullException)result).ParamName);
        }
    }
}
