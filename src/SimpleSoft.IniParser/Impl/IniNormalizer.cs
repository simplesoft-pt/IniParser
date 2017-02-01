#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 João Simões
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
using SimpleSoft.IniParser.Exceptions;

namespace SimpleSoft.IniParser.Impl
{
    /// <summary>
    /// Normalizes raw <see cref="IniContainer"/> instances to meet
    /// the INI standard.
    /// </summary>
    public class IniNormalizer : IIniNormalizer
    {
        /// <summary>
        /// Default normalizer instance
        /// </summary>
        public static readonly IniNormalizer Default = new IniNormalizer();

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="options">The normalization options</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IniNormalizer(IniNormalizationOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            Options = options;
        }

        /// <summary>
        /// Creates a new instance. Will use <see cref="IniNormalizationOptions.Default"/>.
        /// </summary>
        public IniNormalizer() : this(IniNormalizationOptions.Default)
        {
            
        }

        /// <summary>
        /// The normalization options
        /// </summary>
        public IniNormalizationOptions Options { get; }

        #region IniContainer

        /// <inheritdoc />
        public void NormalizeInto(IniContainer source, IniContainer destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            
            if (source.IsEmpty)
                return;

            CopyComments(source.GlobalComments, destination.GlobalComments);
            NormalizeInto(source.GlobalProperties, destination.GlobalProperties);

            NormalizeInto(source.Sections, destination.Sections);
        }

        /// <inheritdoc />
        public bool TryNormalizeInto(IniContainer source, IniContainer destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.IsEmpty)
                return true;

            CopyComments(source.GlobalComments, destination.GlobalComments);

            return TryNormalizeInto(source.GlobalProperties, destination.GlobalProperties) &&
                   TryNormalizeInto(source.Sections, destination.Sections);
        }

        #endregion

        #region IniSection

        /// <inheritdoc />
        public void NormalizeInto(IReadOnlyCollection<IniSection> source, ICollection<IniSection> destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            if(source.Count == 0)
                return;

            NormalizeInto((IEnumerable<IniSection>) source, destination);
        }

        /// <inheritdoc />
        public void NormalizeInto(IEnumerable<IniSection> source, ICollection<IniSection> destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            var itemsToCopy = source.Select(Normalize);

            if (!Options.IncludeEmptySections)
                itemsToCopy = itemsToCopy.Where(e => !e.IsEmpty);

            var dictionary = new Dictionary<string, IniSection>();
            if (Options.MergeOnDuplicatedSections)
            {
                foreach (var sectionGroup in itemsToCopy.GroupBy(e => e.Name))
                {
                    var mergedSection = new IniSection(sectionGroup.Key);
                    foreach (var section in sectionGroup)
                    {
                        CopyComments(section.Comments, mergedSection.Comments);
                        foreach (var property in section.Properties)
                            mergedSection.Properties.Add(property);
                    }
                    dictionary[sectionGroup.Key] = Normalize(mergedSection);
                }
            }
            else
            {
                foreach (var section in itemsToCopy)
                {
                    if (Options.ThrowExceptions &&
                        dictionary.ContainsKey(section.Name))
                        throw new DuplicatedSection(section.Name);
                    dictionary[section.Name] = section;
                }
            }

            CopyAndSortSections(dictionary.Values, destination);
        }

        /// <inheritdoc />
        public bool TryNormalizeInto(IReadOnlyCollection<IniSection> source, ICollection<IniSection> destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            return source.Count == 0 || TryNormalizeInto((IEnumerable<IniSection>) source, destination);
        }

        /// <inheritdoc />
        public bool TryNormalizeInto(IEnumerable<IniSection> source, ICollection<IniSection> destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            var itemsToCopy = new List<IniSection>();
            foreach (var section in source)
            {
                IniSection normalizedSection;
                if (TryNormalize(section, out normalizedSection))
                {
                    if (Options.IncludeEmptySections || !normalizedSection.IsEmpty)
                        itemsToCopy.Add(normalizedSection);
                }
                else
                    return false;
            }

            var dictionary = new Dictionary<string, IniSection>();
            if (Options.MergeOnDuplicatedSections)
            {
                foreach (var sectionGroup in itemsToCopy.GroupBy(e => e.Name))
                {
                    var mergedSection = new IniSection(sectionGroup.Key);
                    foreach (var section in sectionGroup)
                    {
                        CopyComments(section.Comments, mergedSection.Comments);
                        foreach (var property in section.Properties)
                            mergedSection.Properties.Add(property);
                    }

                    if (TryNormalize(mergedSection, out mergedSection))
                        dictionary[sectionGroup.Key] = mergedSection;
                    else
                        return false;
                }
            }
            else
            {
                foreach (var section in itemsToCopy)
                {
                    if (dictionary.ContainsKey(section.Name))
                        return false;
                    dictionary[section.Name] = section;
                }
            }

            CopyAndSortSections(dictionary.Values, destination);
            return true;
        }

        /// <inheritdoc />
        public IniSection Normalize(IniSection source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var destination = Options.IsCaseSensitive
                ? new IniSection(source.Name)
                : new IniSection(source.Name.ToUpperInvariant());

            if (source.IsEmpty)
                return destination;

            CopyComments(source.Comments, destination.Comments);
            NormalizeInto(source.Properties, destination.Properties);

            return destination;
        }

        /// <inheritdoc />
        public bool TryNormalize(IniSection source, out IniSection destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var tmpDestination = Options.IsCaseSensitive
                ? new IniSection(source.Name)
                : new IniSection(source.Name.ToUpperInvariant());

            if (source.IsEmpty)
            {
                destination = tmpDestination;
                return true;
            }

            CopyComments(source.Comments, tmpDestination.Comments);
            if (TryNormalizeInto(source.Properties, tmpDestination.Properties))
            {
                destination = tmpDestination;
                return true;
            }

            destination = null;
            return false;
        }

        #endregion

        #region IniProperty

        /// <inheritdoc />
        public void NormalizeInto(IReadOnlyCollection<IniProperty> source, ICollection<IniProperty> destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            if (source.Count == 0)
                return;

            NormalizeInto((IEnumerable<IniProperty>)source, destination);
        }

        /// <inheritdoc />
        public void NormalizeInto(IEnumerable<IniProperty> source, ICollection<IniProperty> destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            if (!Options.IncludeEmptyProperties)
                source = source.Where(e => !e.IsEmpty);

            //  creates a normalized copy of all properties
            var itemsToCopy = source.Select(Normalize);

            var dictionary = new Dictionary<string, IniProperty>();
            if (Options.ReplaceOnDuplicatedProperties)
                foreach (var property in itemsToCopy)
                    dictionary[property.Name] = property;
            else
            {
                foreach (var property in itemsToCopy)
                {
                    if (dictionary.ContainsKey(property.Name) && Options.ThrowExceptions)
                        throw new DuplicatedProperty(property.Name);
                    dictionary[property.Name] = property;
                }
            }

            CopyAndSortProperties(dictionary.Values, destination);
        }

        /// <inheritdoc />
        public bool TryNormalizeInto(IReadOnlyCollection<IniProperty> source, ICollection<IniProperty> destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            return source.Count == 0 || TryNormalizeInto((IEnumerable<IniProperty>)source, destination);
        }

        /// <inheritdoc />
        public bool TryNormalizeInto(IEnumerable<IniProperty> source, ICollection<IniProperty> destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            if (!Options.IncludeEmptyProperties)
                source = source.Where(e => !e.IsEmpty);

            var itemsToCopy = new List<IniProperty>();
            foreach (var property in source)
            {
                IniProperty normalizedProperty;
                if (TryNormalize(property, out normalizedProperty))
                    itemsToCopy.Add(normalizedProperty);
                else
                    return false;
            }

            var dictionary = new Dictionary<string, IniProperty>();
            if (Options.ReplaceOnDuplicatedProperties)
                foreach (var property in itemsToCopy)
                    dictionary[property.Name] = property;
            else
            {
                foreach (var property in itemsToCopy)
                {
                    if (dictionary.ContainsKey(property.Name) && Options.ThrowExceptions)
                        return false;
                    dictionary[property.Name] = property;
                }
            }

            CopyAndSortProperties(dictionary.Values, destination);
            return true;
        }

        /// <inheritdoc />
        public IniProperty Normalize(IniProperty source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Options.IsCaseSensitive
                ? new IniProperty(source.Name, source.Value)
                : new IniProperty(source.Name.ToUpperInvariant(), source.Value);
        }

        /// <inheritdoc />
        public bool TryNormalize(IniProperty source, out IniProperty destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            destination = Options.IsCaseSensitive
                ? new IniProperty(source.Name, source.Value)
                : new IniProperty(source.Name.ToUpperInvariant(), source.Value);
            return true;
        }

        #endregion

        #region Helper methods

        private void CopyComments(ICollection<string> origin, ICollection<string> destination)
        {
            if(origin.Count == 0)
                return;

            if (Options.IncludeEmptyComments)
                foreach (var comment in origin)
                    destination.Add(comment);
            else
            {
                foreach (var comment in origin)
                {
                    if (string.IsNullOrWhiteSpace(comment))
                        continue;
                    destination.Add(comment);
                }
            }
        }

        private void CopyAndSortProperties(ICollection<IniProperty> origin, ICollection<IniProperty> destination)
        {
            if(origin.Count == 0)
                return;

            switch (Options.SortProperties)
            {
                case IniNormalizationSortType.None:
                    foreach (var value in origin)
                        destination.Add(value);
                    break;
                case IniNormalizationSortType.Ascending:
                    foreach (var value in origin.OrderBy(e => e.Name))
                        destination.Add(value);
                    break;
                case IniNormalizationSortType.Descending:
                    foreach (var value in origin.OrderByDescending(e => e.Name))
                        destination.Add(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CopyAndSortSections(ICollection<IniSection> origin, ICollection<IniSection> destination)
        {
            if (origin.Count == 0)
                return;

            switch (Options.SortProperties)
            {
                case IniNormalizationSortType.None:
                    foreach (var value in origin)
                        destination.Add(value);
                    break;
                case IniNormalizationSortType.Ascending:
                    foreach (var value in origin.OrderBy(e => e.Name))
                        destination.Add(value);
                    break;
                case IniNormalizationSortType.Descending:
                    foreach (var value in origin.OrderByDescending(e => e.Name))
                        destination.Add(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}