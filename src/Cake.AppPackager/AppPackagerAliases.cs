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
    [CakeAliasCategory("AppPackager")]
    public static class AppPackagerAliases {

        #region Packing 

        /// <summary>
        /// Create an application package using the specificed output name, content, and setttings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputPackage">Output name of the application package.</param>
        /// <param name="contentDirectory">Directory for the content to be pack.</param>
        /// <example>
        /// <code>
        ///     AppPack("test.appx", "package-content"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, FilePath outputPackage, DirectoryPath contentDirectory)
        {
            AppPack(context, outputPackage, contentDirectory, new AppPackagerSettings());
        }

        /// <summary>
        /// Create an application package using the specificed output name, content, and setttings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputPackage">Output name of the application package.</param>
        /// <param name="contentDirectory">Directory for the content to be pack.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppPack("test.appx", "package-content", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, FilePath outputPackage, DirectoryPath contentDirectory, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var packer = new AppPacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            packer.Pack(outputPackage, contentDirectory, settings);
        }

        /// <summary>
        /// Create an application package using the specificed output name, AppxManifest, and setttings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputPackage">Output name of the application package.</param>
        /// <param name="mappingFile">A a valid package manifest, AppxManifest.xml.</param>
        /// <example>
        /// <code>
        ///     AppPack("test.appx", "AppXManfist.xml"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, FilePath outputPackage, FilePath mappingFile)
        {
           AppPack(context, outputPackage, mappingFile, new AppPackagerSettings());
        }

        /// <summary>
        /// Create an application package using the specificed output name, AppxManifest, and setttings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputPackage">Output name of the application package.</param>
        /// <param name="mappingFile">A a valid package manifest, AppxManifest.xml.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppPack("test.appx", "AppXManfist.xml", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, FilePath outputPackage, FilePath mappingFile, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var packer = new AppPacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            packer.Pack(outputPackage, mappingFile, settings);
        }

        #endregion Packing

        #region Unpacking

        /// <summary>
        /// The App Packager unpacker used to unpack applications.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Package to unpack.</param>
        /// <param name="outputDirectory">Location to unpack.</param>
        /// <example>
        /// <code>
        ///     AppUnpack("test.appx", "unpacked-content"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Unpack")]
        [CakeNamespaceImport("Cake.AppPackager.Unpack")]
        public static void AppUnpack(this ICakeContext context, FilePath inputPackage, DirectoryPath outputDirectory)
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
        ///     AppUnpack("test.appx", "unpacked-content", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Unpack")]
        [CakeNamespaceImport("Cake.AppPackager.Unpack")]
        public static void AppUnpack(this ICakeContext context, FilePath inputPackage, DirectoryPath outputDirectory, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var unPacker = new AppUnpacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            unPacker.Unpack(inputPackage, outputDirectory, settings);
        }

        #endregion Unpacking

        #region Bundling

        /// <summary>
        /// Create an application bundle using the specificed output name, content.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputBundle">The name of the output bundle.</param>
        /// <param name="contentDirectory">Directory for the content to be bundled.</param>
        /// <example>
        /// <code>
        ///     AppBundle("test.appxbundle", "bundle-content"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, FilePath outputBundle, DirectoryPath contentDirectory)
        {
            AppBundle(context, outputBundle, contentDirectory, new AppPackagerSettings());
        }

        /// <summary>
        /// Create an application bundle using the specificed output name, content, and settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputBundle">The name of the output bundle.</param>
        /// <param name="contentDirectory">Directory for the content to be bundled.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppBundle("test.appxbundle", "unbundled-content", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, FilePath outputBundle, DirectoryPath contentDirectory, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var bundler = new AppBundler(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            bundler.Bundle(outputBundle, contentDirectory, settings);
        }

        /// <summary>
        /// Create an application bundle using the specificed output name, content, and AppxManifest.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputBundle">The name of the output bundle.</param>
        /// <param name="mappingFile"></param>
        /// <example>
        /// <code>
        ///     AppBundle("test.appxbundle", "bundle-content", "AppXManfist.xml"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, FilePath outputBundle, FilePath mappingFile)
        {
           AppBundle(context, outputBundle, mappingFile, new AppPackagerSettings());
        }

        /// <summary>
        /// Create an application bundle using the specificed output name, content, AppxManifest, and settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="outputBundle">The name of the output bundle.</param>
        /// <param name="mappingFile"></param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppBundle("test.appxbundle", "bundle-content", "AppXManfist.xml", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, FilePath outputBundle, FilePath mappingFile, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var bundler = new AppBundler(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            bundler.Bundle(outputBundle, mappingFile, settings);
        }

        #endregion Bundling

        #region Unbundling

        /// <summary>
        /// Create an application bundle using the specificed input name and location to unbundle.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputBundle">Bundle to be unbundled.</param>
        /// <param name="outputDirectory">Location to unbundle.</param>
        /// <example>
        /// <code>
        ///     AppUnbundle("test.appxbundle", "bundle-content"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Unbundle")]
        [CakeNamespaceImport("Cake.AppPackager.Unbundle")]
        public static void AppUnbundle(this ICakeContext context, FilePath inputBundle, DirectoryPath outputDirectory)
        {
          AppUnbundle(context, inputBundle, outputDirectory, new AppPackagerSettings());
        }

        /// <summary>
        /// Create an application bundle using the specificed input name, location to unbundle, and settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputBundle">Bundle to be unbundled.</param>
        /// <param name="outputDirectory">Location to unbundle.</param>
        /// <param name="settings">The settings</param>
        /// <example>
        /// <code>
        ///     AppUnbundle("test.appxbundle", "bundle-content", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Unbundle")]
        [CakeNamespaceImport("Cake.AppPackager.Unbundle")]
        public static void AppUnbundle(this ICakeContext context, FilePath inputBundle, DirectoryPath outputDirectory, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var unBundler = new AppUnbundler(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            unBundler.Unpack(inputBundle, outputDirectory, settings);
        }

        #endregion Unbundling

        #region Encryption

        /// <summary>
        /// Creates an encrypted app package from the specified input app package at the specified output package.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Input package.</param>
        /// <param name="outputPackage">Output package.</param>
        /// <param name="keyFile">Key file.</param>
        /// <example>
        /// <code>
        ///     AppEncrypter("test.appx", "encrypted.appx", "keyfile_name.txt"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Encrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Encrypt")]
        public static void AppEncrypter(this ICakeContext context, FilePath inputPackage, FilePath outputPackage, FilePath keyFile)
        {
           AppEncrypter(context, inputPackage, outputPackage, keyFile, new AppPackagerSettings());
        }

        /// <summary>
        /// Creates an encrypted app package from the specified input app package at the specified output package using global test key.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Input package.</param>
        /// <param name="outputPackage">Output package.</param>
        /// <example>
        /// <code>
        ///     AppEncrypter("test.appx", "encrypted.appx"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Encrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Encrypt")]
        public static void AppEncrypter(this ICakeContext context, FilePath inputPackage, FilePath outputPackage)
        {
            AppEncrypter(context, inputPackage, outputPackage, null, new AppPackagerSettings());
        }

        /// <summary>
        /// Creates an encrypted app package from the specified input app package at the specified output package using global test key.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Input package.</param>
        /// <param name="outputPackage">Output package.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppEncrypter("test.appx", "encrypted.appx", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Encrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Encrypt")]
        public static void AppEncrypter(this ICakeContext context, FilePath inputPackage, FilePath outputPackage, AppPackagerSettings settings)
        {
            AppEncrypter(context, inputPackage, outputPackage, null, settings);
        }

        /// <summary>
        /// Creates an encrypted app package from the specified input app package at the specified output package.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Input package.</param>
        /// <param name="outputPackage">Output package.</param>
        /// <param name="keyFile">Key file.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppEncrypter("test.appx", "encrypted.appx", "keyfile_name.txt", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Encrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Encrypt")]
        public static void AppEncrypter(this ICakeContext context, FilePath inputPackage, FilePath outputPackage, FilePath keyFile, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var encrypter = new AppEncrypter(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            encrypter.Encrypt(inputPackage, outputPackage, keyFile, settings);
        }

        #endregion Encryption

        #region Decryption

        /// <summary>
        /// Creates an decrypted app package from the specified input app package at the specified output package.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Input package.</param>
        /// <param name="outputPackage">Output package.</param>
        /// <param name="keyFile">Key file.</param>
        /// <example>
        /// <code>
        ///     AppDecrypter("test.appx", "decrypted.appx", "keyfile_name.txt"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Decrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Decrypt")]
        public static void AppDecrypter(this ICakeContext context, FilePath inputPackage, FilePath outputPackage, FilePath keyFile)
        {
           AppDecrypter(context, inputPackage, outputPackage, keyFile, new AppPackagerSettings());
        }

        /// <summary>
        /// Creates an decrypted app package from the specified input app package at the specified output package using global test key.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Input package.</param>
        /// <param name="outputPackage">Output package.</param>
        /// <example>
        /// <code>
        ///     AppDecrypter("test.appx", "decrypted.appx"); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Decrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Decrypt")]
        public static void AppDecrypter(this ICakeContext context, FilePath inputPackage, FilePath outputPackage)
        {
            AppDecrypter(context, inputPackage, outputPackage, null, new AppPackagerSettings());
        }

        /// <summary>
        /// Creates an decrypted app package from the specified input app package at the specified output package using global test key.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Input package.</param>
        /// <param name="outputPackage">Output package.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppDecrypter("test.appx", "decrypted.appx", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Decrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Decrypt")]
        public static void AppDecrypter(this ICakeContext context, FilePath inputPackage, FilePath outputPackage, AppPackagerSettings settings)
        {
            AppDecrypter(context, inputPackage, outputPackage, null, settings);
        }

        /// <summary>
        /// Creates an decrypted app package from the specified input app package at the specified output package.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputPackage">Input package.</param>
        /// <param name="outputPackage">Output package.</param>
        /// <param name="keyFile">Key file.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     AppDecrypter("test.appx", "decrypted.appx", "keyfile_name.txt", new AppPackagerSettings { OverwriteOutput = true }); 
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Decrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Decrypt")]
        public static void AppDecrypter(this ICakeContext context, FilePath inputPackage, FilePath outputPackage, FilePath keyFile, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var decrypter = new AppDecrypter(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            decrypter.Decrypt(inputPackage, outputPackage, keyFile, settings);
        }
        
        #endregion Decryption
    }
}
