using Cake.Core.Annotations;

namespace Cake.AppPackager {
    /// <summary>
    /// <para>Contains functionality related to create app packages using <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/hh446767(v=vs.85).aspx">MakeAppx.exe</see>.</para>
    /// <para>
    /// In order to use the commands for this alias, App packager (MakeAppx) will need to be installed on the machine where
    /// the Cake script is being executed.  This is typically achieved by installing the correct Windows SDK.
    /// </para>
    /// </summary>
    [CakeAliasCategory("AppPacakager")]
    public static class AppPackagerAliases {}
}