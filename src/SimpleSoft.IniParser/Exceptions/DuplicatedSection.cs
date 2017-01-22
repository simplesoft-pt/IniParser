namespace SimpleSoft.IniParser.Exceptions
{
    /// <summary>
    /// Exception thrown when multiple sections with the same name are found
    /// </summary>
    public class DuplicatedSection : IniParserException
    {
        /// <summary>
        /// The duplicated section name
        /// </summary>
        public string SectionName { get; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="sectionName">The duplicated section name</param>
        public DuplicatedSection(string sectionName) : base($"The section [{sectionName}] was found multiple times")
        {
            SectionName = sectionName;
        }
    }
}