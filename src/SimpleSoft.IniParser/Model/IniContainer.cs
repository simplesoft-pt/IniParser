using System.Collections.Generic;

namespace SimpleSoft.IniParser.Model
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
        public ICollection<Property> GlobalProperties { get; } = new List<Property>();

        /// <summary>
        /// The ini container sections
        /// </summary>
        public ICollection<Section> Sections { get; } = new List<Section>();
    }
}