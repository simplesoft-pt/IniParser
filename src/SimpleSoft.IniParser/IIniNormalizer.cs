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
        /// <param name="container">The container to normalize</param>
        void Normalize(IniContainer container);

        /// <summary>
        /// Normalizes the <see cref="IniContainer"/> instance.
        /// </summary>
        /// <param name="container">The container to normalize</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        bool TryNormalize(IniContainer container);
    }
}
