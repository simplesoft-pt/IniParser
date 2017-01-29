using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Normalization
{
    public class IniNormalizerIniPropertyTests
    {
        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedPropertyThenNameMustBeUpperCase()
        {
            var normalizer = new IniNormalizer();

            var source = new IniProperty("someproperty", "some value");
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.NotEqual(source.Name, result.Name);
            Assert.Equal(source.Name.ToUpperInvariant(), result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedPropertyThenNameMustBeOriginal()
        {
            var normalizer = new IniNormalizer {Options = {IsCaseSensitive = true}};

            var source = new IniProperty("someproperty", "some value");
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizePropertyThenNameMustBeUpperCase()
        {
            var normalizer = new IniNormalizer();

            var source = new IniProperty("someproperty", "some value");
            IniProperty result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.NotEqual(source.Name, result.Name);
            Assert.Equal(source.Name.ToUpperInvariant(), result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizePropertyThenNameMustBeOriginal()
        {
            var normalizer = new IniNormalizer { Options = { IsCaseSensitive = true } };

            var source = new IniProperty("someproperty", "some value");
            IniProperty result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Value, result.Value);
        }
    }
}
