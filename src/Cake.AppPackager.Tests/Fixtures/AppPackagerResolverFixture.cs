using Cake.Core;
using Cake.Core.IO;
using NSubstitute;
using Cake.Core.Tooling;

namespace Cake.AppPackager.Tests.Fixtures
{
    internal sealed class AppPackagerResolverFixture {
        private readonly bool _is64Bit;

        public IFileSystem FileSystem { get; set; }
        public ICakeEnvironment Environment { get; set; }
        public IRegistry Registry { get; set; }
        public IToolLocator Tools { get; set; }

        public AppPackagerResolverFixture(bool is64Bit = true) {
            _is64Bit = is64Bit;

            FileSystem = Substitute.For<IFileSystem>();
            Environment = Substitute.For<ICakeEnvironment>();
            Registry = Substitute.For<IRegistry>();
            Tools = Substitute.For<IToolLocator>();

            Environment.Platform.Is64Bit.Returns(_is64Bit);
            Environment.GetSpecialPath(SpecialPath.ProgramFiles).Returns(@"C:\Program Files");
            Environment.GetSpecialPath(SpecialPath.ProgramFilesX86).Returns(@"C:\Program Files (x86)");
        }

        public void GivenThatToolExistInKnownPath()
        {
            if (_is64Bit)
            {
                FileSystem.Exist(Arg.Is<FilePath>(p => p.FullPath == @"C:/Program Files (x86)/Windows Kits/10/bin/x64/makeappx.exe")).Returns(true);
            }
            else
            {
                FileSystem.Exist(Arg.Is<FilePath>(p => p.FullPath == @"C:/Program Files (x86)/Windows Kits/10/bin/x86/makeappx.exe")).Returns(true);
            }
        }

        public void GivenThatToolHasRegistryKey()
        {
            var appPackagerKey = Substitute.For<IRegistryKey>();
            appPackagerKey.GetValue("KitsRoot10").Returns(@"C:/Program Files (x86)/Windows Kits/10/");

            var windowsKey = Substitute.For<IRegistryKey>();
            windowsKey.GetSubKeyNames().Returns(new[] { "KitsRoot10" });
            windowsKey.OpenKey("KitsRoot10").Returns(appPackagerKey);

            var localMachine = Substitute.For<IRegistryKey>();
            localMachine.OpenKey("Software\\Microsoft\\Windows Kits\\Install Roots").Returns(windowsKey);
            var toolPath = _is64Bit ? @"C:/Program Files (x86)/Windows Kits/10/bin/x64/makeappx.exe" : @"C:/Program Files (x86)/Windows Kits/10/bin/x86/makeappx.exe";
            FileSystem.Exist(Arg.Is<FilePath>(p => p.FullPath == toolPath)).Returns(true);
            Registry.LocalMachine.Returns(localMachine);
        }

        public void GivenThatNoSdkRegistryKeyExist()
        {
            var localMachine = Substitute.For<IRegistryKey>();
            localMachine.OpenKey("Software\\Microsoft\\Windows Kits\\Install Roots").Returns((IRegistryKey)null);
            Registry.LocalMachine.Returns(localMachine);
        }

        public FilePath Resolve()
        {
            var resolver = new AppPackagerResolver(FileSystem, Environment, Tools, Registry);
            return resolver.ResolvePath();
        }
    }
}
