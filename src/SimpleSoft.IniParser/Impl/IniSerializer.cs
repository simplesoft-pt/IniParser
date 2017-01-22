using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleSoft.IniParser.Model;

namespace SimpleSoft.IniParser.Impl
{
    /// <summary>
    /// The INI serializer
    /// </summary>
    public class IniSerializer : IIniSerializer
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="options">The serialization options</param>
        public IniSerializer(IniSerializationOptions options)
        {
            Options = options;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public IniSerializer() : this(new IniSerializationOptions())
        {
            
        }

        /// <summary>
        /// Serialization options
        /// </summary>
        public IniSerializationOptions Options { get; }

        /// <summary>
        /// Serializes the <see cref="IniContainer"/> as a string in the INI format.
        /// </summary>
        /// <param name="container">The container to serialize</param>
        /// <returns>The container serialized as a string</returns>
        public string SerializeAsString(IniContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            if (container.GlobalComments.Count == 0 &&
                container.GlobalProperties.Count == 0 &&
                container.Sections.Count == 0)
                return string.Empty;

            var builder = new StringBuilder();

            AppendCommentsAndProperties(builder, container.GlobalComments, container.GlobalProperties);

            if (Options.IncludeEmptySections)
            {
                foreach (var section in container.Sections)
                {
                    AppendSection(builder, section);
                }
            }
            else
            {
                foreach (var section in container.Sections)
                {
                    if(section.Comments.Count == 0 && section.Properties.Count == 0)
                        continue;
                    AppendSection(builder, section);
                }
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

            if (container.GlobalComments.Count == 0 &&
                container.GlobalProperties.Count == 0 &&
                container.Sections.Count == 0)
                return;

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

        private void AppendSection(StringBuilder builder, Section section)
        {

            builder.Append('[');
            builder.Append(section.Name);
            builder.AppendLine("]");

            AppendCommentsAndProperties(builder, section.Comments, section.Properties);
        }

        private void AppendCommentsAndProperties(
            StringBuilder builder, IEnumerable<string> comments, IEnumerable<Property> properties)
        {
            foreach (var comment in comments)
            {
                builder.Append(Options.CommentIndicator);
                builder.AppendLine(comment);
            }

            if (Options.IncludeEmptyProperties)
            {
                foreach (var property in properties)
                {
                    builder.Append(property.Name);
                    builder.Append(Options.PropertyNameValueDelimiter);
                    builder.AppendLine(property.Value ?? string.Empty);
                }
            }
            else
            {
                foreach (var property in properties)
                {
                    if(string.IsNullOrWhiteSpace(property.Value))
                        continue;

                    builder.Append(property.Name);
                    builder.Append(Options.PropertyNameValueDelimiter);
                    builder.AppendLine(property.Value ?? string.Empty);
                }
            }
        }
    }
}
