namespace SimpleSoft.IniParser.Exceptions
{
    /// <summary>
    /// Exception thrown when a line can not be parsed into a comment, section or property.
    /// </summary>
    public class InvalidLineFormatException : IniParserException
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="line">The line position were parse failed</param>
        /// <param name="value">The line value</param>
        public InvalidLineFormatException(int line, string value) 
            : base($"The line at position '{line}' could not be parsed into a comment, section or property")
        {
            Line = line;
            Value = value;
        }

        /// <summary>
        /// The line position were parse failed.
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// The line value.
        /// </summary>
        public string Value { get; }
    }
}
