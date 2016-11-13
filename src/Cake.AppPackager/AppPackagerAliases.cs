using System;
using Cake.AppPackager.Bundle;
using Cake.AppPackager.Decrypt;
using Cake.AppPackager.Encrypt;
using Cake.AppPackager.Pack;
using Cake.AppPackager.Unbundle;
using Cake.AppPackager.Unpack;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.AppPackager {
    /// <summary>
    /// <para>Contains functionality related to create app packages using <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/hh446767(v=vs.85).aspx">MakeAppx.exe</see>.</para>
    /// <para>
    /// In order to use the commands for this alias, App packager (MakeAppx) will need to be installed on the machine where
    /// the Cake script is being executed.  This is typically achieved by installing the correct Windows SDK.
    /// </para>
    /// </summary>
    [CakeAliasCategory("AppPacakager")]
    public static class AppPackagerAliases {

        /// <summary>
        /// Create an application package using the specificed output name, content, and setttings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputPackageName">Output name of the application package.</param>
        /// <param name="contentDirectory">Directory for the content to be pack.</param>
        /// <example>
        /// <code>
        ///     AppPack("test.appx", Directory("package-content")); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, string outputPackageName, DirectoryPath contentDirectory)
        {
            AppPack(context, outputPackageName, contentDirectory, new AppPackagerSettings());
        }

        /// <summary>
        /// Create an application package using the specificed output name, content, and setttings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputPackageName">Output name of the application package.</param>
        /// <param name="contentDirectory">Directory for the content to be pack.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppPack("test.appx", Directory("package-content"), new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, string outputPackageName, DirectoryPath contentDirectory, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var packer = new AppPacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            packer.Pack(outputPackageName, contentDirectory, settings);
        }

        /// <summary>
        /// Create an application package using the specificed output name, AppxManifest, and setttings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputPackageName">Output name of the application package.</param>
        /// <param name="mappingFile">A a valid package manifest, AppxManifest.xml.</param>
        /// <example>
        /// <code>
        ///     AppPack("test.appx", File("AppXManfist.xml")); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, string outputPackageName, IFile mappingFile)
        {
           AppPack(context, outputPackageName, mappingFile, new AppPackagerSettings());
        }

        /// <summary>
        /// Create an application package using the specificed output name, AppxManifest, and setttings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputPackageName">Output name of the application package.</param>
        /// <param name="mappingFile">A a valid package manifest, AppxManifest.xml.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppPack("test.appx", File("AppXManfist.xml"), new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, string outputPackageName, IFile mappingFile, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var packer = new AppPacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            packer.Pack(outputPackageName, mappingFile, settings);
        }

        /// <summary>
        /// The App Packager unpacker used to unpack applications.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Package to unpack.</param>
        /// <param name="outputDirectory">Location to unpack.</param>
        /// <example>
        /// <code>
        ///     AppUnpack(File("test.appx"), Directory("unpacked")); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Unpack")]
        [CakeNamespaceImport("Cake.AppPackager.Unpack")]
        public static void AppUnpack(this ICakeContext context, IFile inputPackage, DirectoryPath outputDirectory)
        {
           AppUnpack(context, inputPackage, outputDirectory, new AppPackagerSettings());
        }

        /// <summary>
        /// The App Packager unpacker used to unpack applications.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Package to unpack.</param>
        /// <param name="outputDirectory">Location to unpack.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppUnpack(File("test.appx"), Directory("unpacked"), new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Unpack")]
        [CakeNamespaceImport("Cake.AppPackager.Unpack")]
        public static void AppUnpack(this ICakeContext context, IFile inputPackage, DirectoryPath outputDirectory, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var unPacker = new AppUnpacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            unPacker.Unpack(inputPackage, outputDirectory, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, string outputBundleName, DirectoryPath contentDirectory)
        {
            AppBundle(context, outputBundleName, contentDirectory, new AppPackagerSettings());
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, string outputBundleName, DirectoryPath contentDirectory, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var bundler = new AppBundler(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            bundler.Bundle(outputBundleName, contentDirectory, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, string outputBundleName, IFile mappingFile)
        {
           AppBundle(context, outputBundleName, mappingFile, new AppPackagerSettings());
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, string outputBundleName, IFile mappingFile, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var bundler = new AppBundler(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            bundler.Bundle(outputBundleName, mappingFile, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Unbundle")]
        [CakeNamespaceImport("Cake.AppPackager.Unbundle")]
        public static void AppUnbundle(this ICakeContext context, IFile inputBundle, DirectoryPath outputDirectory)
        {
          AppUnbundle(context, inputBundle, outputDirectory, new AppPackagerSettings());
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Unbundle")]
        [CakeNamespaceImport("Cake.AppPackager.Unbundle")]
        public static void AppUnbundle(this ICakeContext context, IFile inputBundle, DirectoryPath outputDirectory, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var unBundler = new AppUnbundler(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            unBundler.Unpack(inputBundle, outputDirectory, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Encrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Encrypt")]
        public static void AppEncrypter(this ICakeContext context, IFile inputPackage, string outputPackageName, IFile keyFile)
        {
           AppEncrypter(context, inputPackage, outputPackageName, keyFile, new AppPackagerSettings());
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Encrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Encrypt")]
        public static void AppEncrypter(this ICakeContext context, IFile inputPackage, string outputPackageName, IFile keyFile, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var encrypter = new AppEncrypter(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            encrypter.Encrypt(inputPackage, outputPackageName, keyFile, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Decrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Decrypt")]
        public static void AppDecrypter(this ICakeContext context, IFile inputPackage, string outputPackageName, IFile keyFile)
        {
           AppDecrypter(context, inputPackage, outputPackageName, keyFile, new AppPackagerSettings());
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Decrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Decrypt")]
        public static void AppDecrypter(this ICakeContext context, IFile inputPackage, string outputPackageName, IFile keyFile, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var decrypter = new AppDecrypter(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            decrypter.Decrypt(inputPackage, outputPackageName, keyFile, settings);
        }
    }
}