using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Deserialization
{
    public class IniDeserializerTests
    {
        [Fact]
        public void GivenADeserializerWithDefaultConstructorThenDefaultsAreUsed()
        {
            var serializer = new IniDeserializer();

            Assert.Same(IniDeserializationOptions.Default, serializer.Options);
            Assert.Same(IniNormalizer.Default, serializer.Normalizer);
        }

        [Fact]
        public void GivenADeserializerReceivingCustomOptionsThenDefaultNormalizerIsUsed()
        {
            var options = new IniDeserializationOptions();
            var serializer = new IniDeserializer(options);

            Assert.Same(options, serializer.Options);
            Assert.Same(IniNormalizer.Default, serializer.Normalizer);
        }

        [Fact]
        public void GivenADeserializerReceivingCustomNormalizerThenDefaultOptionsAreUsed()
        {
            var normalizer = new IniNormalizer();
            var serializer = new IniDeserializer(normalizer);

            Assert.Same(IniDeserializationOptions.Default, serializer.Options);
            Assert.Same(normalizer, serializer.Normalizer);
        }

        [Fact]
        public void GivenADeserializerReceivingCustomParametersThenReferencesAreTheSame()
        {
            var options = new IniDeserializationOptions();
            var normalizer = new IniNormalizer();
            var serializer = new IniDeserializer(options, normalizer);

            Assert.Same(options, serializer.Options);
            Assert.Same(normalizer, serializer.Normalizer);
        }
    }
}
