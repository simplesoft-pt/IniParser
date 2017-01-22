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
