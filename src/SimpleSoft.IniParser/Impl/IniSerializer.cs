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

            foreach (var section in container.Sections)
            {
                builder.Append('[');
                builder.Append(section.Name);
                builder.AppendLine("]");

                AppendCommentsAndProperties(builder, section.Comments, section.Properties);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Serializes the <see cref="IniContainer"/> into the provided <see cref="StreamWriter"/>
        /// in the INI format.
        /// </summary>
        /// <param name="container">The container to serialize</param>
        /// <param name="writer">The writer to output the serialization result</param>
        public void SerializeToStream(IniContainer container, TextWriter writer)
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
        }

        /// <summary>
        /// Serializes the <see cref="IniContainer"/> into the provided <see cref="StreamWriter"/>
        /// in the INI format.
        /// </summary>
        /// <param name="container">The container to serialize</param>
        /// <param name="writer">The writer to output the serialization result</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        public async Task SerializeToStreamAsync(IniContainer container, TextWriter writer, CancellationToken ct = new CancellationToken())
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            //  TODO    Performance tests
            var serializationResult = SerializeAsString(container);
            await writer.WriteAsync(serializationResult);
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

    /// <summary>
    /// INI serialization options
    /// </summary>
    public class IniSerializationOptions
    {
        /// <summary>
        /// The character used to represent comments. Defaults to <value>';'</value>.
        /// </summary>
        public char CommentIndicator { get; set; } = ';';

        /// <summary>
        /// The character used delimit name/value properties. Defaults to <value>'='</value>.
        /// </summary>
        public char PropertyNameValueDelimiter { get; set; } = '=';

        /// <summary>
        /// Should properties with empty values be included? Defaults to <value>false</value>.
        /// </summary>
        public bool IncludeEmptyProperties { get; set; } = false;
    }
}
