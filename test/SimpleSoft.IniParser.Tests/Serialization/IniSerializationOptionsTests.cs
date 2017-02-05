using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Serialization
{
    public class IniSerializationOptionsTests
    {
        [Fact]
        public void GivenASerializationOptionsInstanceUsingTheDefaultConstrutorThenNormalizeBeforeSerializationMustBeTrue()
        {
            var options = new IniSerializationOptions();
            Assert.True(options.NormalizeBeforeSerialization);
        }

        [Fact]
        public void GivenASerializationOptionsInstanceUsingTheDefaultConstrutorThenCommentIndicatorMustBeSemicolon()
        {
            var options = new IniSerializationOptions();
            Assert.Equal(';', options.CommentIndicator);
        }

        [Fact]
        public void GivenASerializationOptionsInstanceUsingTheDefaultConstrutorThenPropertyNameValueDelimiterMustBeEqualSign()
        {
            var options = new IniSerializationOptions();
            Assert.Equal('=', options.PropertyNameValueDelimiter);
        }

        [Fact]
        public void GivenASerializationOptionsInstanceUsingTheDefaultConstrutorThenEmptyLineBeforeSectionMustBeFalse()
        {
            var options = new IniSerializationOptions();
            Assert.False(options.EmptyLineBeforeSection);
        }
    }
}
