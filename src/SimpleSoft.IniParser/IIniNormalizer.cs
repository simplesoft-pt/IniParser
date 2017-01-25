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

namespace SimpleSoft.IniParser
{
    /// <summary>
    /// Ini format normalizer.
    /// </summary>
    public interface IIniNormalizer
    {
        #region IniContainer

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> instance storing the 
        /// result into the destination container.
        /// </summary>
        /// <param name="source">The container to normalize</param>
        /// <param name="destination">The destination container</param>
        void NormalizeInto(IniContainer source, IniContainer destination);

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> instance storing the 
        /// result into the destination container.
        /// </summary>
        /// <param name="source">The source to normalize</param>
        /// <param name="destination">The normalized source</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalizeInto(IniContainer source, IniContainer destination);

        #endregion

        #region IniSection

        /// <summary>
        /// Normalizes the collection of <see cref="IniSection"/> into the 
        /// destination collection.
        /// </summary>
        /// <param name="source">The source collection to normalize</param>
        /// <param name="destination">The destination collection</param>
        void NormalizeInto(IReadOnlyCollection<IniSection> source, ICollection<IniSection> destination);

        /// <summary>
        /// Normalizes the collection of <see cref="IniSection"/> into the 
        /// destination collection.
        /// </summary>
        /// <param name="source">The source collection to normalize</param>
        /// <param name="destination">The destination collection</param>
        void NormalizeInto(IEnumerable<IniSection> source, ICollection<IniSection> destination);

        /// <summary>
        /// Normalizes the collection of <see cref="IniSection"/> into the 
        /// destination collection.
        /// </summary>
        /// <param name="source">The source collection to normalize</param>
        /// <param name="destination">The destination collection</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalizeInto(IReadOnlyCollection<IniSection> source, ICollection<IniSection> destination);

        /// <summary>
        /// Normalizes the collection of <see cref="IniSection"/> into the 
        /// destination collection.
        /// </summary>
        /// <param name="source">The source collection to normalize</param>
        /// <param name="destination">The destination collection</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalizeInto(IEnumerable<IniSection> source, ICollection<IniSection> destination);

        /// <summary>
        /// Normalizes the <see cref="IniSection"/> returning
        /// a new section with the normalization result.
        /// </summary>
        /// <param name="source">The source to normalize</param>
        /// <returns>The new section with the normalization result</returns>
        IniSection Normalize(IniSection source);

        /// <summary>
        /// Normalizes the <see cref="IniSection"/> returning
        /// a new section with the normalization result.
        /// </summary>
        /// <param name="source">The source to normalize</param>
        /// <param name="destination">The new section with the normalization result</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalize(IniSection source, out IniSection destination);

        #endregion

        #region IniProperty

        /// <summary>
        /// Normalizes the collection of <see cref="IniProperty"/> into the 
        /// destination collection.
        /// </summary>
        /// <param name="source">The source collection to normalize</param>
        /// <param name="destination">The destination collection</param>
        void NormalizeInto(IReadOnlyCollection<IniProperty> source, ICollection<IniProperty> destination);

        /// <summary>
        /// Normalizes the collection of <see cref="IniProperty"/> into the 
        /// destination collection.
        /// </summary>
        /// <param name="source">The source collection to normalize</param>
        /// <param name="destination">The destination collection</param>
        void NormalizeInto(IEnumerable<IniProperty> source, ICollection<IniProperty> destination);

        /// <summary>
        /// Normalizes the collection of <see cref="IniProperty"/> into the 
        /// destination collection.
        /// </summary>
        /// <param name="source">The source collection to normalize</param>
        /// <param name="destination">The destination collection</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalizeInto(IReadOnlyCollection<IniProperty> source, ICollection<IniProperty> destination);

        /// <summary>
        /// Normalizes the collection of <see cref="IniProperty"/> into the 
        /// destination collection.
        /// </summary>
        /// <param name="source">The source collection to normalize</param>
        /// <param name="destination">The destination collection</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalizeInto(IEnumerable<IniProperty> source, ICollection<IniProperty> destination);

        /// <summary>
        /// Normalizes the <see cref="IniProperty"/> returning
        /// a new property with the normalization result.
        /// </summary>
        /// <param name="source">The source to normalize</param>
        /// <returns>The new property with the normalization result</returns>
        IniProperty Normalize(IniProperty source);

        /// <summary>
        /// Normalizes the <see cref="IniProperty"/> returning
        /// a new property with the normalization result.
        /// </summary>
        /// <param name="source">The source to normalize</param>
        /// <param name="destination">The new property with the normalization result</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalize(IniProperty source, out IniProperty destination);

        #endregion
    }
}
