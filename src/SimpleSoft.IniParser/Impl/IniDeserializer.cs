#region License
// The MIT License (MIT)
// 
// Copyright (c) 2017 João Simões
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
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SimpleSoft.IniParser.Exceptions;

namespace SimpleSoft.IniParser.Impl
{
    /// <summary>
    /// The INI deserializer
    /// </summary>
    public class IniDeserializer : IIniDeserializer
    {
        /// <summary>
        /// Default deserializer instance
        /// </summary>
        public static readonly IniDeserializer Default = new IniDeserializer();

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="options">The deserialization options</param>
        /// <param name="normalizer">The <see cref="IniContainer"/> normalizer</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IniDeserializer(IniDeserializationOptions options, IIniNormalizer normalizer)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (normalizer == null)
                throw new ArgumentNullException(nameof(normalizer));

            Options = options;
            Normalizer = normalizer;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="options">The deserialization options</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IniDeserializer(IniDeserializationOptions options) :this(options, IniNormalizer.Default)
        {

        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="normalizer">The <see cref="IniContainer"/> normalizer</param>
        /// <exception cref="ArgumentNullException"></exception>
        public IniDeserializer(IIniNormalizer normalizer) : this(IniDeserializationOptions.Default, normalizer)
        {

        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public IniDeserializer() : this(IniDeserializationOptions.Default, IniNormalizer.Default)
        {

        }

        #endregion

        /// <summary>
        /// Deserialization options
        /// </summary>
        public IniDeserializationOptions Options { get; }

        /// <summary>
        /// The <see cref="IniContainer"/> normalizer.
        /// </summary>
        public IIniNormalizer Normalizer { get; }

        /// <summary>
        /// Deserializes the string as an <see cref="IniContainer"/>.
        /// </summary>
        /// <param name="value">The value to deserialize</param>
        /// <returns>The resulting container</returns>
        public IniContainer DeserializeAsContainer(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            using (var reader = new StringReader(value))
            {
                return DeserializeAsContainer(reader);
            }
        }

        /// <summary>
        /// Reads the content of the given <see cref="TextReader"/> and deserializes
        /// as an <see cref="IniContainer"/>.
        /// </summary>
        /// <param name="reader">The reader to extract</param>
        /// <returns>The resulting container</returns>
        public IniContainer DeserializeAsContainer(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            var container = new IniContainer();
            IniSection currentSection = null;

            var linePosition = 0;
            string line;
            while ((line = reader.ReadLine()?.Trim()) != null)
            {
                if(CanIgnoreLine(line))
                    continue;

                string comment;
                IniSection section;
                IniProperty property;
                if (TryExtractComment(line, out comment))
                {
                    if (currentSection == null)
                        container.GlobalComments.Add(comment);
                    else
                        currentSection.Comments.Add(comment);
                }
                else if(TryExtractSection(line, out section))
                {
                    currentSection = section;
                    container.Sections.Add(currentSection);
                }
                else if(TryExtractProperty(line, out property))
                {
                    if(currentSection == null)
                        container.GlobalProperties.Add(property);
                    else
                        currentSection.Properties.Add(property);
                }
                else if(Options.FailOnInvalidLines)
                {
                    throw new InvalidLineFormatException(linePosition, line);
                }

                ++linePosition;
            }

            if (Options.NormalizeAfterDeserialization)
                container = Normalizer.Normalize(container);

            return container;
        }

        /// <summary>
        /// Reads the content of the given <see cref="TextReader"/> and deserializes
        /// as an <see cref="IniContainer"/>.
        /// </summary>
        /// <param name="reader">The reader to extract</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The resulting container</returns>
        public async Task<IniContainer> DeserializeAsContainerAsync(TextReader reader, CancellationToken ct)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            var container = new IniContainer();
            IniSection currentSection = null;

            var linePosition = 0;
            string line;
            while ((line = (await reader.ReadLineAsync())?.Trim()) != null)
            {
                if (CanIgnoreLine(line))
                    continue;

                string comment;
                IniSection section;
                IniProperty property;
                if (TryExtractComment(line, out comment))
                {
                    if (currentSection == null)
                        container.GlobalComments.Add(comment);
                    else
                        currentSection.Comments.Add(comment);
                }
                else if (TryExtractSection(line, out section))
                {
                    currentSection = section;
                    container.Sections.Add(currentSection);
                }
                else if (TryExtractProperty(line, out property))
                {
                    if (currentSection == null)
                        container.GlobalProperties.Add(property);
                    else
                        currentSection.Properties.Add(property);
                }
                else if (Options.FailOnInvalidLines)
                {
                    throw new InvalidLineFormatException(linePosition, line);
                }

                ++linePosition;
            }

            if (Options.NormalizeAfterDeserialization)
                container = Normalizer.Normalize(container);

            return container;
        }

        #region Private methods

        private static bool CanIgnoreLine(string line)
        {
            return string.IsNullOrWhiteSpace(line) || line.Length < 2;
        }

        private bool TryExtractComment(string line, out string comment)
        {
            if (line[0] == Options.CommentIndicator)
            {
                comment = line.Substring(1, line.Length - 1);
                return true;
            }

            comment = null;
            return false;
        }

        private static bool TryExtractSection(string line, out IniSection section)
        {
            if (line.Length > 2 && line[0] == '[' && line[line.Length - 1] == ']')
            {
                section = new IniSection(line.Substring(1, line.Length - 2));
                return true;
            }

            section = null;
            return false;
        }

        private bool TryExtractProperty(string line, out IniProperty property)
        {
            int signIdx;
            if (line.Length > 1 &&
                (signIdx = line.IndexOf(
                    Options.PropertyNameValueDelimiter.ToString(),
                    StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                var name = line.Substring(0, signIdx);
                var value = signIdx == line.Length - 1
                    ? null
                    : line.Substring(signIdx + 1, line.Length - 1 - signIdx);

                property = new IniProperty(name, value);
                return true;
            }

            property = null;
            return false;
        }

        #endregion
    }
}
