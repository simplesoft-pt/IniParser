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

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.IniParser
{
    /// <summary>
    /// The INI serializer
    /// </summary>
    public interface IIniSerializer
    {
        /// <summary>
        /// Serializes the <see cref="IniContainer"/> as a string in the INI format.
        /// </summary>
        /// <param name="container">The container to serialize</param>
        /// <returns>The container serialized as a string</returns>
        string SerializeAsString(IniContainer container);

        /// <summary>
        /// Serializes the <see cref="IniContainer"/> into the provided <see cref="StreamWriter"/>
        /// in the INI format.
        /// </summary>
        /// <param name="container">The container to serialize</param>
        /// <param name="writer">The writer to output the serialization result</param>
        void SerializeToTextWriter(IniContainer container, TextWriter writer);

        /// <summary>
        /// Serializes the <see cref="IniContainer"/> into the provided <see cref="StreamWriter"/>
        /// in the INI format.
        /// </summary>
        /// <param name="container">The container to serialize</param>
        /// <param name="writer">The writer to output the serialization result</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        Task SerializeToTextWriterAsync(
            IniContainer container, TextWriter writer, CancellationToken ct = default(CancellationToken));
    }
}
