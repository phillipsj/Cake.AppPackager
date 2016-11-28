using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.AppPackager.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using Should;
using Should.Core.Assertions;

namespace Cake.AppPackager.Tests
{
    public sealed class AppUnbundlerTests
    {
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

        public void ShouldThrowIfInputPackageIsNull()
        {
            // Given
            var fixture = new AppUnbundlerFixture() { OutputDirectory = "output" };

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("inputBundle");
        }

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
