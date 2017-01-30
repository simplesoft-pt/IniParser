using System.Collections.Generic;
using System.Linq;
using SimpleSoft.IniParser.Exceptions;
using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Normalization
{
    public class IniNormalizerIniPropertyTests
    {
        #region Single -> Name

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedPropertyThenNameMustBeUpperCase()
        {
            var normalizer = new IniNormalizer();

            var source = PropertyForCaseSensitive;
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

            var source = PropertyForCaseSensitive;
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizePropertyThenNameMustBeUpperCase()
        {
            var normalizer = new IniNormalizer();

            var source = PropertyForCaseSensitive;
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

            var source = PropertyForCaseSensitive;
            IniProperty result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        #endregion

        #region Collections -> Empty Value Tests

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedPropertyCollectionThenEmptyMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = PropertiesWithEmptyValues;
            var destination = new List<IniProperty>();
            normalizer.NormalizeInto(source, destination);

            Assert.NotEmpty(destination);
            Assert.False(destination.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerIncludingEmptyPropertiesWhenNormalizedPropertyCollectionThenEmptyMustBeKept()
        {
            var normalizer = new IniNormalizer { Options = { IncludeEmptyProperties = true } };

            var source = PropertiesWithEmptyValues;
            var destination = new List<IniProperty>();
            normalizer.NormalizeInto(source, destination);

            Assert.Equal(source.Length, destination.Count);
            Assert.True(destination.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizePropertyCollectionThenEmptyMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = PropertiesWithEmptyValues;
            var destination = new List<IniProperty>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.NotEmpty(destination);
            Assert.False(destination.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerIncludingEmptyPropertiesWhenTryedToNormalizePropertyCollectionThenEmptyMustBeKept()
        {
            var normalizer = new IniNormalizer {Options = {IncludeEmptyProperties = true}};

            var source = PropertiesWithEmptyValues;
            var destination = new List<IniProperty>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(source.Length, destination.Count);
            Assert.True(destination.Any(e => e.IsEmpty));
        }

        #endregion

        #region Collections -> Duplicated Keys

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedPropertyCollectionThenDuplicatedMustFaild()
        {
            var normalizer = new IniNormalizer();

            var source = PropertiesWithDuplicatedCaseInsensitiveKeys;
            var destination = new List<IniProperty>();

            var ex = Assert.Throws<DuplicatedProperty>(() =>
            {
                normalizer.NormalizeInto(source, destination);
            });

            Assert.NotNull(ex.PropertyName);
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenNormalizedPropertyCollectionThenCaseSensitiveKeysWillPass()
        {
            var normalizer = new IniNormalizer {Options = {IsCaseSensitive = true}};

            var source = PropertiesWithDuplicatedCaseInsensitiveKeys;
            var destination = new List<IniProperty>();
            normalizer.NormalizeInto(source, destination);

            Assert.Equal(source.Length, destination.Count);
        }

        [Fact]
        public void GivenANormalizerIgnoringExceptionsWhenNormalizedPropertyCollectionThenDuplicatedWillPass()
        {
            var normalizer = new IniNormalizer {Options = {ThrowExceptions = false}};

            var source = PropertiesWithDuplicatedCaseInsensitiveKeys;
            var destination = new List<IniProperty>();
            normalizer.NormalizeInto(source, destination);

            Assert.Equal(source.Length / 2, destination.Count);
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizePropertyCollectionThenDuplicatedMustFaild()
        {
            var normalizer = new IniNormalizer();

            var source = PropertiesWithDuplicatedCaseInsensitiveKeys;
            var destination = new List<IniProperty>();
            Assert.False(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(0, destination.Count);
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenTryedToNormalizePropertyCollectionThenCaseSensitiveKeysWillPass()
        {
            var normalizer = new IniNormalizer { Options = { IsCaseSensitive = true } };

            var source = PropertiesWithDuplicatedCaseInsensitiveKeys;
            var destination = new List<IniProperty>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(source.Length, destination.Count);
        }

        [Fact]
        public void GivenANormalizerIgnoringExceptionsWhenTryedToNormalizePropertyCollectionThenDuplicatedWillPass()
        {
            var normalizer = new IniNormalizer { Options = { ThrowExceptions = false } };

            var source = PropertiesWithDuplicatedCaseInsensitiveKeys;
            var destination = new List<IniProperty>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(source.Length / 2, destination.Count);
        }

        #endregion

        #region TestData

        private static readonly IniProperty PropertyForCaseSensitive = new IniProperty("p01", "value");

        private static readonly IniProperty[] PropertiesWithEmptyValues = {
            new IniProperty("p01", "value"),
            new IniProperty("p02", "value"),
            new IniProperty("p03"),
        };

        private static readonly IniProperty[] PropertiesWithDuplicatedCaseInsensitiveKeys =
        {
            new IniProperty("p01", "value0101"),
            new IniProperty("P01", "value0102")
        };

        #endregion
    }
}
