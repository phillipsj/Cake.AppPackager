using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using Xunit;

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
            Assert.IsType<CakeException>(result);
            Assert.Equal("App Packager: Could not locate executable.", result.Message);
        }

        [Fact]
        public void ShouldThrowIfInputPackageIsNull()
        {
            // Given
            var fixture = new AppDecrypterFixture() { OutputPackage = "test.appx" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("inputPackage", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void ShouldThrowIfOutputPackageIsNull()
        {
            // Given
            var fixture = new AppDecrypterFixture() { InputPackage = "test.appx" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("outputPackage", ((ArgumentNullException)result).ParamName);
        }
    }
}
