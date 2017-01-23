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
    /// Normalizes raw <see cref="IniContainer"/> instances to meet
    /// the INI standard.
    /// </summary>
    public interface IIniNormalizer
    {
        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> instance.
        /// </summary>
        /// <param name="source">The source to normalize</param>
        /// <returns>The normalized source</returns>
        IniContainer Normalize(IniContainer source);

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> instance.
        /// </summary>
        /// <param name="source">The source to normalize</param>
        /// <param name="destination">The normalized source</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalize(IniContainer source, out IniContainer destination);
    }
}
