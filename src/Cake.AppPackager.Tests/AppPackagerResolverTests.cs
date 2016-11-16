using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Should;
using Should.Core.Assertions;
using UglyToad.Fixie.DataDriven;

namespace Cake.AppPackager.Tests
{
    public sealed class AppPackagerResolverTests
    {
        public void ShouldThrowIfFileSystemIsNull() {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.FileSystem = null;

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("fileSystem");
        }

        public void ShouldThrowIfEnvironmentIsNull() {
            // Given
            var fixture = new AppPackagerResolverFixture();
            fixture.Environment = null;

            // When
            var result = Record.Exception(() => fixture.Resolve());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("environment");
        }

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
        
        [InlineData(false)]
       // [InlineData(false)]
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
