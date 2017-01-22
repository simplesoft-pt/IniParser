namespace SimpleSoft.IniParser.Impl
{
    /// <summary>
    /// INI normalization options
    /// </summary>
    public class IniNormalizationOptions
    {
        /// <summary>
        /// Should empty comments be included? Defaults to <value>false</value>.
        /// </summary>
        public bool IncludeEmptyComments { get; set; } = false;

        /// <summary>
        /// Should empty sections be included? Defaults to <value>false</value>.
        /// </summary>
        public bool IncludeEmptySections { get; set; } = false;

        /// <summary>
        /// Should empty properties be included? Defaults to <value>false</value>.
        /// </summary>
        public bool IncludeEmptyProperties { get; set; } = false;

        /// <summary>
        /// Are section and property names case sensitive? Defaults to <value>false</value>.
        /// </summary>
        public bool IsCaseSensitive { get; set; } = false;

        /// <summary>
        /// Replace a duplicated property for the last found value? Defaults to <value>false</value>.
        /// </summary>
        public bool ReplaceOnDuplicatedProperties { get; set; } = false;
    }
}