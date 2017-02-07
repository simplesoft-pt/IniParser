using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Deserialization
{
    public class IniDeserializationOptionsTests
    {
        [Fact]
        public void GivenADeserializationOptionsInstanceUsingTheDefaultConstrutorThenNormalizeAfterDeserializationMustBeTrue()
        {
            var options = new IniDeserializationOptions();
            Assert.True(options.NormalizeAfterDeserialization);
        }

        [Fact]
        public void GivenADeserializationOptionsInstanceUsingTheDefaultConstrutorThenCommentIndicatorMustBeSemicolon()
        {
            var options = new IniDeserializationOptions();
            Assert.Equal(';', options.CommentIndicator);
        }

        [Fact]
        public void GivenADeserializationOptionsInstanceUsingTheDefaultConstrutorThenPropertyNameValueDelimiterMustBeEqualSign()
        {
            var options = new IniDeserializationOptions();
            Assert.Equal('=', options.PropertyNameValueDelimiter);
        }

        [Fact]
        public void GivenADeserializationOptionsInstanceUsingTheDefaultConstrutorThenFailOnInvalidLinesMustBeTrue()
        {
            var options = new IniDeserializationOptions();
            Assert.True(options.FailOnInvalidLines);
        }
    }
}
