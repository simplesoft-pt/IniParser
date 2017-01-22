using System;

namespace SimpleSoft.IniParser.Exceptions
{
    /// <summary>
    /// Base exception for all exceptions thrown by the IniParser library
    /// </summary>
    public abstract class IniParserException : Exception
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        protected IniParserException(string message) : base(message)
        {

        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">The exception that caused the current exception</param>
        protected IniParserException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
