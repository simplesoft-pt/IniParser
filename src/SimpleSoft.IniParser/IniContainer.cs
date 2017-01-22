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

        /// <summary>
        /// Is the container empty?
        /// </summary>
        public bool IsEmpty => GlobalComments.Count == 0 && GlobalProperties.Count == 0 && Sections.Count == 0;
    }
}