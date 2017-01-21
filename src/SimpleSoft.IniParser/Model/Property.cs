using System;

namespace SimpleSoft.IniParser.Model
{
    /// <summary>
    /// Represents a property
    /// </summary>
    public sealed class Property
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The property value</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Property(string name, string value = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be whitespace.", nameof(name));

            Name = name;
            Value = value;
        }

        /// <summary>
        /// The property name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The property value
        /// </summary>
        public string Value { get; set; }
    }
}