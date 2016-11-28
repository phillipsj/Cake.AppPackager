using Cake.Core;
using Cake.Core.IO;

namespace Cake.AppPackager
{
    public static class ProcessBuilderExtensionsMethods
    {
        public static void AppendSwitch(this ProcessArgumentBuilder builder, bool include, string text)
        {
            if (include)
            {
                builder.AppendSwitch("/", text);
            }
        }
    }
}
