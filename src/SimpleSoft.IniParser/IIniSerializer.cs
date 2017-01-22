using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SimpleSoft.IniParser.Model;

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
