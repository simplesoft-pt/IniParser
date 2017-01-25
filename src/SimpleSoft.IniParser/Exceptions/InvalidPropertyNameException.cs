namespace SimpleSoft.IniParser.Exceptions
{
    /// <summary>
    /// Exception thrown when an invalid property name is found
    /// </summary>
    public class InvalidPropertyNameException : IniParserException
    {
        /// <summary>
        /// The unexpected character
        /// </summary>
        public char UnexpectedChar { get; }

        /// <summary>
        /// The property
        /// </summary>
        public IniProperty Property { get; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="unexpectedChar">The unexpected character</param>
        /// <param name="property">The property name</param>
        public InvalidPropertyNameException(char unexpectedChar, IniProperty property) 
            : base($"Invalid character '{unexpectedChar}' in the property with name '{property.Name}'")
        {
            UnexpectedChar = unexpectedChar;
            Property = property;
        }
    }
}
