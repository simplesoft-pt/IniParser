namespace SimpleSoft.IniParser
{
    /// <summary>
    /// Extensions for <see cref="IIniNormalizer"/> classes.
    /// </summary>
    public static class NormalizerExtensions
    {
        /// <summary>
        /// Creates a normalized <see cref="IniContainer"/> using the provided one.
        /// </summary>
        /// <param name="normalizer">The normalizer to use</param>
        /// <param name="source">The container to normalize</param>
        /// <returns>The new container with the normalization result</returns>
        public static IniContainer Normalize(this IIniNormalizer normalizer, IniContainer source)
        {
            var destination = new IniContainer();
            normalizer.NormalizeInto(source, destination);

            return destination;
        }

        /// <summary>
        /// Creates a normalized <see cref="IniContainer"/> using the provided one.
        /// </summary>
        /// <param name="normalizer">The normalizer to use</param>
        /// <param name="source">The container to normalize</param>
        /// <param name="destination">The container with the normalization result</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool TryNormalize(
            this IIniNormalizer normalizer, IniContainer source, out IniContainer destination)
        {
            var tmpDestination = new IniContainer();
            if (normalizer.TryNormalizeInto(source, tmpDestination))
            {
                destination = tmpDestination;
                return true;
            }

            destination = null;
            return false;
        }
    }
}