namespace SimpleSoft.IniParser.Exceptions
{
    /// <summary>
    /// Exception thrown when multiple global properties with the same name are found
    /// </summary>
    public class DuplicatedGlobalProperty : IniParserException
    {
        /// <summary>
        /// The duplicated global property name
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="propertyName">The duplicated global property name</param>
        public DuplicatedGlobalProperty(string propertyName) : base($"The global property '{propertyName}' was found multiple times")
        {
            PropertyName = propertyName;
        }
    }
}