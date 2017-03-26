using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Xunit;

namespace Cake.AppPackager.Tests
{
    public sealed class AppPackagerResolverTests
    {
        [Fact]
        public void ShouldThrowIfFileSystemIsNull()
        {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.FileSystem = null;

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("fileSystem", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void ShouldThrowIfEnvironmentIsNull()
        {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.Environment = null;

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("environment", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void ShouldThrowIfRegistryIsNull()
        {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.Registry = null;

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("registry", ((ArgumentNullException)result).ParamName);
        }

        [Fact]
        public void ShouldThrowIfToolsIsNull()
        {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.Tools = null;

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            Assert.IsType<ArgumentNullException>(result);
            Assert.Equal("tools", ((ArgumentNullException)result).ParamName);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ShouldReturnFromDiscIfFound(bool is64Bit)
        {
            // Given
            var fixture = new AppPackagerResolverFixture(is64Bit);
            fixture.GivenThatToolExistInKnownPath();

            // When
            var result = fixture.Resolve();

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldReturnFromRegistryIfFound()
        {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.GivenThatToolHasRegistryKey();

            // When
            var result = fixture.Resolve();

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldThrowIfNotFoundOnDiscAndSdkRegistryPathCannotBeResolved()
        {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.GivenThatNoSdkRegistryKeyExist();

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("Failed to find MakeAppx.exe.", result.Message);
        }

        [Fact]
        public void ShouldThrowIfAppPackagerCannotBeResolved()
        {
            // Given
            var fixture = new AppPackagerResolverFixture();

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            Assert.IsType<CakeException>(result);
            Assert.Equal("Failed to find MakeAppx.exe.", result.Message);
        }
    }
}
