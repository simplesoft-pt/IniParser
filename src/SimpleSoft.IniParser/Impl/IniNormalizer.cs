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
        /// Creates a new instance.
        /// </summary>
        public IniNormalizer() : this(new IniNormalizationOptions())
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
            //CopyProperties(source.GlobalProperties, destination.GlobalProperties);

            //CopySections(source.Sections, destination.Sections);
        }

        /// <inheritdoc />
        public bool TryNormalizeInto(IniContainer source, IniContainer destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            //  TODO Increase performance by not using exception handling
            try
            {
                NormalizeInto(source, destination);
                return true;
            }
            catch
            {
                return false;
            }
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IniSection Normalize(IniSection source)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryNormalize(IniSection source, out IniSection destination)
        {
            throw new NotImplementedException();
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
                source = source.Where(e => !string.IsNullOrWhiteSpace(e.Value));

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

            foreach (var value in dictionary.Values)
                destination.Add(value);
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
                source = source.Where(e => !string.IsNullOrWhiteSpace(e.Value));

            var itemsToCopy = new List<IniProperty>();
            foreach (var property in source)
            {
                IniProperty normalizedProperty;
                if(TryNormalize(property, out normalizedProperty))
                    itemsToCopy.Add(normalizedProperty);
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

            foreach (var value in dictionary.Values)
                destination.Add(value);
            return true;
        }

        /// <inheritdoc />
        public IniProperty Normalize(IniProperty source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            char? invalidChar;
            if (Options.ThrowExceptions &&
                (invalidChar = ExtractInvalidCharFromPropertyName(source)).HasValue)
                throw new InvalidPropertyNameException(invalidChar.Value, source);

            return Options.IsCaseSensitive
                ? new IniProperty(source.Name, source.Value)
                : new IniProperty(source.Name.ToUpperInvariant(), source.Value);
        }

        /// <inheritdoc />
        public bool TryNormalize(IniProperty source, out IniProperty destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var invalidChar = ExtractInvalidCharFromPropertyName(source);
            if (invalidChar.HasValue)
            {
                destination = null;
                return false;
            }

            destination = Options.IsCaseSensitive
                ? new IniProperty(source.Name, source.Value)
                : new IniProperty(source.Name.ToUpperInvariant(), source.Value);
            return true;
        }

        #endregion

        #region Helper methods

        private static char? ExtractInvalidCharFromPropertyName(IniProperty property)
        {
            foreach (var c in property.Name)
                if (c == ' ' || c == '=')
                    return c;
            return null;
        }

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

        #endregion
    }
}