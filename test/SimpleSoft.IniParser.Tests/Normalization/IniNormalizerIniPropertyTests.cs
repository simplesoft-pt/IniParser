using System.Collections.Generic;
using System.Linq;
using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Normalization
{
    public class IniNormalizerIniPropertyTests
    {
        #region IniProperty

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedPropertyThenNameMustBeUpperCase()
        {
            var normalizer = new IniNormalizer();

            var source = new IniProperty("p01", "value");
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.NotEqual(source.Name, result.Name);
            Assert.Equal(source.Name.ToUpperInvariant(), result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenNormalizedPropertyThenNameMustBeOriginal()
        {
            var normalizer = new IniNormalizer {Options = {IsCaseSensitive = true}};

            var source = new IniProperty("p01", "value");
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizePropertyThenNameMustBeUpperCase()
        {
            var normalizer = new IniNormalizer();

            var source = new IniProperty("p01", "value");
            IniProperty result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.NotEqual(source.Name, result.Name);
            Assert.Equal(source.Name.ToUpperInvariant(), result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenTryedToNormalizePropertyThenNameMustBeOriginal()
        {
            var normalizer = new IniNormalizer { Options = { IsCaseSensitive = true } };

            var source = new IniProperty("p01", "value");
            IniProperty result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        #endregion

        #region Collections of IniProperty

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedPropertyCollectionThenEmptyMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = new[]
            {
                new IniProperty("p01", "value"),
                new IniProperty("p02", "value"),
                new IniProperty("p03"),
            };
            var destination = new List<IniProperty>();
            normalizer.NormalizeInto(source, destination);

            Assert.NotEmpty(destination);
            Assert.False(destination.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerIncludingEmptyPropertiesWhenNormalizedPropertyCollectionThenEmptyMustBeRemoved()
        {
            var normalizer = new IniNormalizer { Options = { IncludeEmptyProperties = true } };

            var source = new[]
            {
                new IniProperty("p01", "value"),
                new IniProperty("p02", "value"),
                new IniProperty("p03"),
            };
            var destination = new List<IniProperty>();
            normalizer.NormalizeInto(source, destination);

            Assert.Equal(source.Length, destination.Count);
            Assert.True(destination.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizePropertyCollectionThenEmptyMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = new[]
            {
                new IniProperty("p01", "value"),
                new IniProperty("p02", "value"),
                new IniProperty("p03"),
            };
            var destination = new List<IniProperty>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.NotEmpty(destination);
            Assert.False(destination.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerIncludingEmptyPropertiesWhenTryedToNormalizePropertyCollectionThenEmptyMustBeRemoved()
        {
            var normalizer = new IniNormalizer {Options = {IncludeEmptyProperties = true}};

            var source = new[]
            {
                new IniProperty("p01", "value"),
                new IniProperty("p02", "value"),
                new IniProperty("p03"),
            };
            var destination = new List<IniProperty>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(source.Length, destination.Count);
            Assert.True(destination.Any(e => e.IsEmpty));
        }

        #endregion
    }
}
