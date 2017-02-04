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
    /// The INI deserializer
    /// </summary>
    public interface IIniDeserializer
    {
        /// <summary>
        /// Deserializes the string as an <see cref="IniContainer"/>.
        /// </summary>
        /// <param name="value">The value to deserialize</param>
        /// <returns>The resulting container</returns>
        IniContainer DeserializeAsContainer(string value);

        /// <summary>
        /// Reads the content of the given <see cref="TextReader"/> and deserializes
        /// as an <see cref="IniContainer"/>.
        /// </summary>
        /// <param name="reader">The reader to extract</param>
        /// <returns>The resulting container</returns>
        IniContainer DeserializeAsContainer(TextReader reader);

        /// <summary>
        /// Reads the content of the given <see cref="TextReader"/> and deserializes
        /// as an <see cref="IniContainer"/>.
        /// </summary>
        /// <param name="reader">The reader to extract</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The resulting container</returns>
        Task<IniContainer> DeserializeAsContainerAsync(TextReader reader, CancellationToken ct);
    }
}
