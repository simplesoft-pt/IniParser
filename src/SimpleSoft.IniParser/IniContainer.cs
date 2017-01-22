using System.Collections.Generic;

namespace SimpleSoft.IniParser
{
    /// <summary>
    /// Represents an ini container
    /// </summary>
    public sealed class IniContainer
    {
        /// <summary>
        /// Global comments
        /// </summary>
        public ICollection<string> GlobalComments { get; } = new List<string>();

        /// <summary>
        /// Global properties
        /// </summary>
        public ICollection<IniProperty> GlobalProperties { get; } = new List<IniProperty>();

        /// <summary>
        /// The ini container sections
        /// </summary>
        public ICollection<IniSection> Sections { get; } = new List<IniSection>();
    }
}