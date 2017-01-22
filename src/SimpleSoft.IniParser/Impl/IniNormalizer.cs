using System;

namespace SimpleSoft.IniParser.Impl
{
    /// <summary>
    /// Normalizes raw <see cref="IniContainer"/> instances to meet
    /// the INI standard.
    /// </summary>
    public class IniNormalizer : IIniNormalizer
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="options">The normalization options</param>
        public IniNormalizer(IniNormalizationOptions options)
        {
            Options = options;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public IniNormalizer() : this(new IniNormalizationOptions())
        {
            
        }

        /// <summary>
        /// The normalization options
        /// </summary>
        public IniNormalizationOptions Options { get; }

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> instance.
        /// </summary>
        /// <param name="container">The container to normalize</param>
        public void Normalize(IniContainer container)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> instance.
        /// </summary>
        /// <param name="container">The container to normalize</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public bool TryNormalize(IniContainer container)
        {
            throw new NotImplementedException();
        }
    }
}