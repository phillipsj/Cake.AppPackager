using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Should;
using Xunit;
using Record = Should.Core.Assertions.Record;

namespace Cake.AppPackager.Tests
{
    public sealed class AppPackagerResolverTests
    {
        [Fact]
        public void ShouldThrowIfFileSystemIsNull() {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.FileSystem = null;

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("fileSystem");
        }

        [Fact]
        public void ShouldThrowIfEnvironmentIsNull() {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.Environment = null;

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("environment");
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
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("registry");
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
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("tools");
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
            result.ShouldNotBeNull();
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
            result.ShouldNotBeNull();
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
            result.ShouldBeType<CakeException>().Message.ShouldContain("Failed to find MakeAppx.exe.");
        }

        [Fact]
        public void ShouldThrowIfAppPackagerCannotBeResolved()
        {
            // Given
            var fixture = new AppPackagerResolverFixture();

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            result.ShouldBeType<CakeException>().Message.ShouldContain("Failed to find MakeAppx.exe.");
        }
    }
}
