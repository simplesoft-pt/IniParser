#region License
// The MIT License (MIT)
// 
// Copyright (c) 2017 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSoft.IniParser
{
    /// <summary>
    /// Extensions for model classes
    /// </summary>
    public static class ModelExtensions
    {
        #region Comments

        /// <summary>
        /// Adds a comment to the <see cref="IniContainer"/>.
        /// </summary>
        /// <param name="container">The container to use</param>
        /// <param name="comment">The comment to add</param>
        /// <returns>The container after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IniContainer AddComment(this IniContainer container, string comment)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            container.GlobalComments.Add(comment);

            return container;
        }

        /// <summary>
        /// Adds a comment to the <see cref="IniSection"/>.
        /// </summary>
        /// <param name="section">The section to use</param>
        /// <param name="comment">The comment to add</param>
        /// <returns>The section after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IniSection AddComment(this IniSection section, string comment)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));

            section.Comments.Add(comment);

            return section;
        }

        #endregion

        #region Sections

        /// <summary>
        /// Gets the first section with the given name from the 
        /// <see cref="IniContainer.Sections"/> collection.
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="name">The section name</param>
        /// <returns>The first section with the given name, or null</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IniSection GetSection(this IniContainer container, string name)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be whitespace.", nameof(name));

            return container.Sections.FirstOrDefault(e => e.Name.Equals(name));
        }

        /// <summary>
        /// Gets all sections with the given name from the 
        /// <see cref="IniContainer.Sections"/> collection.
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="name">The section name</param>
        /// <returns>The collection of sections with the given name</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<IniSection> GetSections(this IniContainer container, string name)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be whitespace.", nameof(name));

            return container.Sections.Where(e => e.Name.Equals(name));
        }

        /// <summary>
        /// Gets a section from the <see cref="IniContainer"/> or adds
        /// a new section if none exists.
        /// </summary>
        /// <param name="container">The container to use</param>
        /// <param name="name">The section name</param>
        /// <returns>The section instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IniSection GetOrAddSection(this IniContainer container, string name)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be whitespace.", nameof(name));

            var section = container.GetSection(name);
            if (section != null)
                return section;

            section = new IniSection(name);
            container.Sections.Add(section);

            return section;
        }

        /// <summary>
        /// Removes the first section with the given name from the 
        /// <see cref="IniContainer.Sections"/> collection.
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="name">The section name</param>
        /// <returns>True if section removed, otherwise false</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool RemoveSection(this IniContainer container, string name)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be whitespace.", nameof(name));

            var idxToRemove = -1;
            for (var i = 0; i < container.Sections.Count; i++)
            {
                if (container.Sections[i].Name.Equals(name))
                {
                    idxToRemove = i;
                    break;
                }
            }

            if (idxToRemove == -1)
                return false;

            container.Sections.RemoveAt(idxToRemove);
            return true;
        }

        /// <summary>
        /// Removes all sections with the given name from the 
        /// <see cref="IniContainer.Sections"/> collection.
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="name">The section name</param>
        /// <returns>True if any section removed, otherwise false</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool RemoveSections(this IniContainer container, string name)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be whitespace.", nameof(name));

            var indexesToRemove = new List<int>(container.Sections.Count);
            for (var i = container.Sections.Count - 1; i >= 0; i--)
                if (container.Sections[i].Name.Equals(name))
                    indexesToRemove.Add(i);

            if (indexesToRemove.Count == 0)
                return false;

            foreach (var idx in indexesToRemove)
                container.Sections.RemoveAt(idx);

            return true;
        }

        #endregion
    }
}
