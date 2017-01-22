namespace SimpleSoft.IniParser.Exceptions
{
    /// <summary>
    /// Exception thrown when multiple properties with the same name are found for the same section
    /// </summary>
    public class DuplicatedSectionProperty : IniParserException
    {
        /// <summary>
        /// The section name
        /// </summary>
        public string SectionName { get; }

        /// <summary>
        /// The duplicated property name
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="sectionName">The section name</param>
        /// <param name="propertyName">The duplicated property name</param>
        public DuplicatedSectionProperty(string sectionName, string propertyName) : base($"The property [{sectionName}].'{propertyName}' was found multiple times")
        {
            SectionName = sectionName;
            PropertyName = propertyName;
        }
    }
}