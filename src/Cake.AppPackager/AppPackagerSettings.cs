using Cake.Core.Tooling;

namespace Cake.AppPackager
{
    /// <summary>
    /// Contains settings used by <see cref="AppPackagerRunner"/>
    /// </summary>
    public sealed class AppPackagerSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets if the validation should be disabled for localized packages.
        /// </summary>
        public bool Localized { get; set; }

        /// <summary>
        /// Gets or sets if the output should be overwritten.
        /// </summary>
        public bool OverwriteOutput { get; set; }
        /// <summary>
        /// Gets or sets if overwritting the output should be prevented.
        /// </summary>
        public bool PreventOverwriteOutput { get; set; }

        /// <summary>
        /// Gets or sets if the validation should be skipped.
        /// </summary>
        public bool SkipSemanticValidation { get; set; }

        /// <summary>
        /// Gets or sets if verbose logging is enabled.
        /// </summary>
        public bool EnableVerboseLogging { get; set; }
    }
}
