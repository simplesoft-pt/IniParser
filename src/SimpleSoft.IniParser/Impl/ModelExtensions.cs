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

using System.Collections.Generic;
using SimpleSoft.IniParser.Exceptions;

namespace SimpleSoft.IniParser.Impl
{
    /// <summary>
    /// Extensions for model classes
    /// </summary>
    public static class ModelExtensions
    {
        #region IniContainer

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> 
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="container">The container to normalize</param>
        /// <returns>The normalized container</returns>
        /// <exception cref="DuplicatedSection"></exception>
        /// <exception cref="DuplicatedProperty"></exception>
        public static IniContainer Normalize(this IniContainer container)
        {
            return IniNormalizer.Default.Normalize(container);
        }

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/>
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="container">The container to normalize</param>
        /// <param name="result">The normalized container</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool TryNormalize(this IniContainer container, out IniContainer result)
        {
            return IniNormalizer.Default.TryNormalize(container, out result);
        }

        #endregion

        #region IniSection

        /// <summary>
        /// Normalizes the <see cref="IniSection"/>
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The section to normalize</param>
        /// <returns>The normalized section</returns>
        /// <exception cref="DuplicatedProperty"></exception>
        public static IniSection Normalize(this IniSection section)
        {
            return IniNormalizer.Default.Normalize(section);
        }

        /// <summary>
        /// Normalizes the <see cref="IniSection"/>
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The section to normalize</param>
        /// <param name="result">The normalized section</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool TryNormalize(this IniSection section, out IniSection result)
        {
            return IniNormalizer.Default.TryNormalize(section, out result);
        }

        /// <summary>
        /// Normalizes the <see cref="IniSection"/> collection
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The sections to normalize</param>
        /// <returns>The normalized sections</returns>
        /// <exception cref="DuplicatedSection"></exception>
        /// <exception cref="DuplicatedProperty"></exception>
        public static ICollection<IniSection> Normalize(this IReadOnlyCollection<IniSection> section)
        {
            return IniNormalizer.Default.Normalize(section);
        }

        /// <summary>
        /// Normalizes the <see cref="IniSection"/> collection
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The sections to normalize</param>
        /// <param name="result">The normalized sections</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool Normalize(this IReadOnlyCollection<IniSection> section, out ICollection<IniSection> result)
        {
            return IniNormalizer.Default.TryNormalize(section, out result);
        }

        /// <summary>
        /// Normalizes the <see cref="IniSection"/> collection
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The sections to normalize</param>
        /// <returns>The normalized sections</returns>
        /// <exception cref="DuplicatedSection"></exception>
        /// <exception cref="DuplicatedProperty"></exception>
        public static ICollection<IniSection> Normalize(this IEnumerable<IniSection> section)
        {
            return IniNormalizer.Default.Normalize(section);
        }

        /// <summary>
        /// Normalizes the <see cref="IniSection"/> collection
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The sections to normalize</param>
        /// <param name="result">The normalized sections</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool Normalize(this IEnumerable<IniSection> section, out ICollection<IniSection> result)
        {
            return IniNormalizer.Default.TryNormalize(section, out result);
        }

        #endregion

        #region IniProperty

        /// <summary>
        /// Normalizes the <see cref="IniProperty"/>
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The section to normalize</param>
        /// <returns>The normalized section</returns>
        public static IniProperty Normalize(this IniProperty section)
        {
            return IniNormalizer.Default.Normalize(section);
        }

        /// <summary>
        /// Normalizes the <see cref="IniProperty"/>
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The section to normalize</param>
        /// <param name="result">The normalized section</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool TryNormalize(this IniProperty section, out IniProperty result)
        {
            return IniNormalizer.Default.TryNormalize(section, out result);
        }

        /// <summary>
        /// Normalizes the <see cref="IniProperty"/> collection
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The properties to normalize</param>
        /// <returns>The normalized properties</returns>
        /// <exception cref="DuplicatedProperty"></exception>
        public static ICollection<IniProperty> Normalize(this IReadOnlyCollection<IniProperty> section)
        {
            return IniNormalizer.Default.Normalize(section);
        }

        /// <summary>
        /// Normalizes the <see cref="IniProperty"/> collection
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The properties to normalize</param>
        /// <param name="result">The normalized properties</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool Normalize(this IReadOnlyCollection<IniProperty> section, out ICollection<IniProperty> result)
        {
            return IniNormalizer.Default.TryNormalize(section, out result);
        }

        /// <summary>
        /// Normalizes the <see cref="IniProperty"/> collection
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The properties to normalize</param>
        /// <returns>The normalized properties</returns>
        /// <exception cref="DuplicatedProperty"></exception>
        public static ICollection<IniProperty> Normalize(this IEnumerable<IniProperty> section)
        {
            return IniNormalizer.Default.Normalize(section);
        }

        /// <summary>
        /// Normalizes the <see cref="IniProperty"/> collection
        /// using the <see cref="IniNormalizer.Default"/> instance.
        /// </summary>
        /// <param name="section">The properties to normalize</param>
        /// <param name="result">The normalized properties</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool Normalize(this IEnumerable<IniProperty> section, out ICollection<IniProperty> result)
        {
            return IniNormalizer.Default.TryNormalize(section, out result);
        }

        #endregion
    }
}
