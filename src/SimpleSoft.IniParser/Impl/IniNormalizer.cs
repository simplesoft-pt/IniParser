using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> instance.
        /// </summary>
        /// <param name="source">The container to normalize</param>
        /// <returns>The normalized container</returns>
        public IniContainer Normalize(IniContainer source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var destination = new IniContainer();
            if (source.IsEmpty)
                return destination;

            CopyComments(source.GlobalComments, destination.GlobalComments);
            CopyProperties(source.GlobalProperties, destination.GlobalProperties, "");

            throw new NotImplementedException();
        }

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> instance.
        /// </summary>
        /// <param name="source">The container to normalize</param>
        /// <param name="destination">The normalized container</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public bool TryNormalize(IniContainer source, out IniContainer destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            destination = new IniContainer();
            if (source.IsEmpty)
                return true;

            CopyComments(source.GlobalComments, destination.GlobalComments);
            CopyProperties(source.GlobalProperties, destination.GlobalProperties);

            throw new NotImplementedException();
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

        private void CopyProperties(ICollection<IniProperty> origin, ICollection<IniProperty> destination, string sectionName = null)
        {
            if(origin.Count == 0)
                return;

            //  Preventing bool validation for each iteration
            ICollection<IniProperty> itemsToCopy;
            if (Options.IncludeEmptyProperties)
            {
                if (Options.IsCaseSensitive)
                    itemsToCopy = origin;
                else
                {
                    var tmp = new List<IniProperty>(origin.Count);
                    foreach (var property in origin)
                        tmp.Add(new IniProperty(property.Name.ToUpperInvariant(), property.Value));
                    itemsToCopy = tmp;
                }
            }
            else
            {
                var tmp = new List<IniProperty>(origin.Count);
                if (Options.IsCaseSensitive)
                {
                    foreach (var property in origin)
                    {
                        if(string.IsNullOrWhiteSpace(property.Value))
                            continue;
                        tmp.Add(property);
                    }
                }
                else
                {
                    foreach (var property in origin)
                    {
                        if (string.IsNullOrWhiteSpace(property.Value))
                            continue;
                        tmp.Add(new IniProperty(property.Name.ToUpperInvariant(), property.Value));
                    }
                }
                itemsToCopy = tmp;
            }

            var dictionary = new Dictionary<string, IniProperty>(origin.Count);
            if (Options.ReplaceOnDuplicatedProperties)
                foreach (var property in itemsToCopy)
                    dictionary[property.Name] = property;
            else
            {
                foreach (var property in itemsToCopy)
                {
                    if (dictionary.ContainsKey(property.Name))
                    {
                        if (sectionName == null)
                            throw new DuplicatedGlobalProperty(property.Name);
                        throw new DuplicatedSectionProperty(sectionName, property.Name);
                    }
                    dictionary[property.Name] = property;
                }
            }

            foreach (var value in dictionary.Values)
                destination.Add(value);
        }
    }
}