namespace SimpleSoft.IniParser.Impl
{
    /// <summary>
    /// INI serialization options
    /// </summary>
    public class IniSerializationOptions
    {
        /// <summary>
        /// The character used to represent comments. Defaults to <value>';'</value>.
        /// </summary>
        public char CommentIndicator { get; set; } = ';';

        /// <summary>
        /// The character used delimit name/value properties. Defaults to <value>'='</value>.
        /// </summary>
        public char PropertyNameValueDelimiter { get; set; } = '=';

        /// <summary>
        /// Should properties with empty values be included? Defaults to <value>false</value>.
        /// </summary>
        public bool IncludeEmptyProperties { get; set; } = false;

        /// <summary>
        /// Should sections without comments or properties be included? Defaults to <value>false</value>.
        /// </summary>
        public bool IncludeEmptySections { get; set; } = false;
    }
}