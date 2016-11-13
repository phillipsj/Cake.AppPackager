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

        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, string outputPackageName, IDirectory contentDirectory, AppPackagerSettings settings) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var packer = new AppPacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            packer.Pack(outputPackageName, contentDirectory, settings);
        }
        
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.AppPackager.Pack")]
        public static void AppPack(this ICakeContext context, string outputPackageName, IFile mappingFile, AppPackagerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var packer = new AppPacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            packer.Pack(outputPackageName, mappingFile, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Unpack")]
        [CakeNamespaceImport("Cake.AppPackager.Unpack")]
        public static void AppUnpack(this ICakeContext context, string inputPackageName, IDirectory outputDirectory, AppPackagerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var unPacker = new AppUnpacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            unPacker.Unpack(inputPackageName, outputDirectory, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, string outputBundleName, IDirectory contentDirectory, AppPackagerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var bundler = new AppBundler(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            bundler.Bundle(outputBundleName, contentDirectory, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Bundle")]
        [CakeNamespaceImport("Cake.AppPackager.Bundle")]
        public static void AppBundle(this ICakeContext context, string outputBundleName, IFile mappingFile, AppPackagerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var bundler = new AppBundler(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            bundler.Bundle(outputBundleName, mappingFile, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Unbundle")]
        [CakeNamespaceImport("Cake.AppPackager.Unbundle")]
        public static void AppUnbundle(this ICakeContext context, string inputBundleName, IDirectory outputDirectory, AppPackagerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var unBundler = new AppUnbundler(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            unBundler.Unpack(inputBundleName, outputDirectory, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Encrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Encrypt")]
        public static void AppEncrypter(this ICakeContext context, string inputPackageName, string outputPackageName, IFile keyFile, AppPackagerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var encrypter = new AppEncrypter(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            encrypter.Encrypt(inputPackageName, outputPackageName, keyFile, settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Decrypter")]
        [CakeNamespaceImport("Cake.AppPackager.Decrypt")]
        public static void AppDecrypter(this ICakeContext context, string inputPackageName, string outputPackageName, IFile keyFile, AppPackagerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var resolver = new AppPackagerResolver(context.FileSystem, context.Environment, context.Tools, context.Registry);
            var decrypter = new AppDecrypter(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, resolver);
            decrypter.Decrypt(inputPackageName, outputPackageName, keyFile, settings);
        }
    }
}