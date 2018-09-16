using Cake.Core;
using Cake.Core.IO;

namespace Cake.AppPackager
{
    /// <summary>
    /// Contains extension methods for <see cref="ProcessArgumentBuilder" />.
    /// </summary>
    public static class ProcessBuilderExtensionsMethods
    {
        /// <summary>
        /// Appends the specified switch to the argument builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="include">Appends the switch only if <c>true</c></param>
        /// <param name="text">The text to be appended.</param>
        public static void AppendSwitch(this ProcessArgumentBuilder builder, bool include, string text)
        {
            if (include)
            {
                builder.AppendSwitch("/", text);
            }
        }
    }
}
