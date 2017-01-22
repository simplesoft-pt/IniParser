using System;

namespace SimpleSoft.IniParser
{
    /// <summary>
    /// Represents a property
    /// </summary>
    public sealed class IniProperty
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The property value</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public IniProperty(string name, string value = null)
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