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
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSoft.IniParser.Impl
{
    /// <summary>
    /// The INI serializer
    /// </summary>
    public class IniSerializer : IIniSerializer
    {
        /// <summary>
        /// Default serializer instance
        /// </summary>
        public static readonly IniSerializer Default = new IniSerializer();

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="options">The serialization options</param>
        /// <param name="normalizer">The <see cref="IniContainer"/> normalizer to use</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IniSerializer(IniSerializationOptions options, IIniNormalizer normalizer)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (normalizer == null)
                throw new ArgumentNullException(nameof(normalizer));

            Options = options;
            Normalizer = normalizer;
        }

        /// <summary>
        /// Creates a new instance. Will use <see cref="IniNormalizer.Default"/>.
        /// </summary>
        /// <param name="options">The serialization options</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IniSerializer(IniSerializationOptions options) : this(options, IniNormalizer.Default)
        {

        }

        /// <summary>
        /// Creates a new instance. Will use <see cref="IniSerializationOptions.Default"/>.
        /// </summary>
        /// <param name="normalizer">The <see cref="IniContainer"/> normalizer to use</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IniSerializer(IIniNormalizer normalizer) : this(IniSerializationOptions.Default, normalizer)
        {
            
        }

        /// <summary>
        /// Creates a new instance. Will use <see cref="IniSerializationOptions.Default"/> and
        /// <see cref="IniNormalizer.Default"/>.
        /// </summary>
        public IniSerializer() : this(IniSerializationOptions.Default, IniNormalizer.Default)
        {
            
        }

        #endregion

        /// <summary>
        /// Serialization options.
        /// </summary>
        public IniSerializationOptions Options { get; }

        /// <summary>
        /// The <see cref="IniContainer"/> normalizer.
        /// </summary>
        public IIniNormalizer Normalizer { get; }

        /// <summary>
        /// Serializes the <see cref="IniContainer"/> as a string in the INI format.
        /// </summary>
        /// <param name="container">The container to serialize</param>
        /// <returns>The container serialized as a string</returns>
        public string SerializeAsString(IniContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            if (container.IsEmpty)
                return string.Empty;

            if (Options.NormalizeBeforeSerialization)
                container = Normalizer.Normalize(container);

            var builder = new StringBuilder();

            AppendCommentsAndProperties(builder, container.GlobalComments, container.GlobalProperties);
            foreach (var section in container.Sections)
            {
                AppendSection(builder, section);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Serializes the <see cref="IniContainer"/> into the provided <see cref="StreamWriter"/>
        /// in the INI format.
        /// </summary>
        /// <param name="container">The container to serialize</param>
        /// <param name="writer">The writer to output the serialization result</param>
        public void SerializeToTextWriter(IniContainer container, TextWriter writer)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            //  TODO    Performance tests
            var serializationResult = SerializeAsString(container);
            writer.Write(serializationResult);

            writer.Flush();
        }

        /// <summary>
        /// Serializes the <see cref="IniContainer"/> into the provided <see cref="StreamWriter"/>
        /// in the INI format.
        /// </summary>
        /// <param name="container">The container to serialize</param>
        /// <param name="writer">The writer to output the serialization result</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        public async Task SerializeToTextWriterAsync(IniContainer container, TextWriter writer, CancellationToken ct = new CancellationToken())
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            //  TODO    Performance tests
            var serializationResult = SerializeAsString(container);
            await writer.WriteAsync(serializationResult);

            await writer.FlushAsync();
        }

        private void AppendSection(StringBuilder builder, IniSection section)
        {
            if (Options.EmptyLineBeforeSection)
                builder.AppendLine();

            builder.Append('[');
            builder.Append(section.Name);
            builder.AppendLine("]");

            AppendCommentsAndProperties(builder, section.Comments, section.Properties);
        }

        private void AppendCommentsAndProperties(
            StringBuilder builder, IEnumerable<string> comments, IEnumerable<IniProperty> properties)
        {
            foreach (var comment in comments)
            {
                builder.Append(Options.CommentIndicator);
                builder.AppendLine(comment);
            }

            foreach (var property in properties)
            {
                builder.Append(property.Name);
                builder.Append(Options.PropertyNameValueDelimiter);
                builder.AppendLine(property.Value ?? string.Empty);
            }
        }
    }
}
