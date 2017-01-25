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
namespace SimpleSoft.IniParser
{
    /// <summary>
    /// Ini format normalizer.
    /// </summary>
    public interface IIniNormalizer
    {
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

        /// <summary>
        /// Normalizes the <see cref="IniSection"/> instance storing the 
        /// result into the destination section.
        /// </summary>
        /// <param name="source">The section to normalize</param>
        /// <param name="destination">The destination section</param>
        void NormalizeInto(IniSection source, IniSection destination);

        /// <summary>
        /// Normalizes the <see cref="IniSection"/> instance storing the 
        /// result into the destination section.
        /// </summary>
        /// <param name="source">The section to normalize</param>
        /// <param name="destination">The normalized section</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalizeInto(IniSection source, IniSection destination);
    }
}
