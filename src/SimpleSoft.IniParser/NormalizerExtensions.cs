using System;
using System.Collections.Generic;

namespace SimpleSoft.IniParser
{
    /// <summary>
    /// Extensions for <see cref="IIniNormalizer"/> classes.
    /// </summary>
    public static class NormalizerExtensions
    {
        #region IniContainer

        /// <summary>
        /// Creates a normalized <see cref="IniContainer"/> using the provided one.
        /// </summary>
        /// <param name="normalizer">The normalizer to use</param>
        /// <param name="source">The container to normalize</param>
        /// <returns>The new container with the normalization result</returns>
        public static IniContainer Normalize(this IIniNormalizer normalizer, IniContainer source)
        {
            if (normalizer == null)
                throw new ArgumentNullException(nameof(normalizer));

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
            if (normalizer == null)
                throw new ArgumentNullException(nameof(normalizer));

            var tmpDestination = new IniContainer();
            if (normalizer.TryNormalizeInto(source, tmpDestination))
            {
                destination = tmpDestination;
                return true;
            }

            destination = null;
            return false;
        }

        #endregion

        #region IniSection

        /// <summary>
        /// Normalizes the collection of <see cref="IniSection"/>.
        /// </summary>
        /// <param name="normalizer">The normalizer to use</param>
        /// <param name="source">The source to normalize</param>
        /// <returns>The normalized collection</returns>
        public static ICollection<IniSection> Normalize(
            this IIniNormalizer normalizer, IEnumerable<IniSection> source)
        {
            if (normalizer == null)
                throw new ArgumentNullException(nameof(normalizer));

            var destination = new List<IniSection>();
            normalizer.NormalizeInto(source, destination);

            return destination;
        }

        /// <summary>
        /// Normalizes the collection of <see cref="IniSection"/>.
        /// </summary>
        /// <param name="normalizer">The normalizer to use</param>
        /// <param name="source">The source to normalize</param>
        /// <param name="destination">The normalized collection</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool TryNormalize(
            this IIniNormalizer normalizer, IEnumerable<IniSection> source, out ICollection<IniSection> destination)
        {
            if (normalizer == null)
                throw new ArgumentNullException(nameof(normalizer));

            destination = new List<IniSection>();
            return normalizer.TryNormalizeInto(source, destination);
        }

        #endregion

        #region IniProperty

        /// <summary>
        /// Normalizes the collection of <see cref="IniProperty"/>.
        /// </summary>
        /// <param name="normalizer">The normalizer to use</param>
        /// <param name="source">The source to normalize</param>
        /// <returns>The normalized collection</returns>
        public static ICollection<IniProperty> Normalize(
            this IIniNormalizer normalizer, IEnumerable<IniProperty> source)
        {
            if (normalizer == null)
                throw new ArgumentNullException(nameof(normalizer));

            var destination = new List<IniProperty>();
            normalizer.NormalizeInto(source, destination);

            return destination;
        }

        /// <summary>
        /// Normalizes the collection of <see cref="IniSection"/>.
        /// </summary>
        /// <param name="normalizer">The normalizer to use</param>
        /// <param name="source">The source to normalize</param>
        /// <param name="destination">The normalized collection</param>
        /// <returns>True if instance normalized successfully, otherwise false</returns>
        public static bool TryNormalize(
            this IIniNormalizer normalizer, IEnumerable<IniProperty> source, out ICollection<IniProperty> destination)
        {
            if (normalizer == null)
                throw new ArgumentNullException(nameof(normalizer));

            destination = new List<IniProperty>();
            return normalizer.TryNormalizeInto(source, destination);
        }

        #endregion
    }
}