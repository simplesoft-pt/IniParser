using System;
using System.Collections.Generic;

namespace SimpleSoft.IniParser
{
    /// <summary>
    /// Represents a section
    /// </summary>
    public sealed class IniSection
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="name">The section name</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public IniSection(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be whitespace.", nameof(name));

            Name = name;
        }

        /// <summary>
        /// The section name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The section comments
        /// </summary>
        public ICollection<string> Comments { get; } = new List<string>();

        /// <summary>
        /// The section properties
        /// </summary>
        public ICollection<IniProperty> Properties { get; } = new List<IniProperty>();
    }
}
