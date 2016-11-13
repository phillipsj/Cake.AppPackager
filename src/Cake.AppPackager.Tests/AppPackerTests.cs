using System;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using Should;
using Should.Core.Assertions;

namespace Cake.AppPackager.Tests {
    public sealed class AppPackerTests {
        public void ShouldThrowIfAppPackerExecutableWasNotFound() {
            // Given
            var fixture = new AppPackerFixture() {OutputPackage = "test.appx", ContentDirectory = "content"};
            fixture.GivenDefaultToolDoNotExist();

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<CakeException>();
            result.Message.ShouldEqual("App Packager: Could not locate executable.");
        }

        public void ShouldThrowIfOutputPackageIsNull() {
            // Given
            var fixture = new AppPackerFixture() {ContentDirectory = "content"};

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("outputPackage");
        }

        public void ShouldThrowIfContentDirectoryIsNull() {
            // Given
            var fixture = new AppPackerFixture() {OutputPackage = "test.appx"};

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("contentDirectory");
        }
    }
}
