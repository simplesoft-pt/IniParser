using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Normalization
{
    public class IniNormalizationOptionsTests
    {
        [Fact]
        public void GivenANormalizationOptionsInstanceUsingTheDefaultConstrutorThenIncludeEmptyCommentsMustBeFalse()
        {
            var options = new IniNormalizationOptions();
            Assert.False(options.IncludeEmptyComments);
        }

        [Fact]
        public void GivenANormalizationOptionsInstanceUsingTheDefaultConstrutorThenIncludeEmptySectionsMustBeFalse()
        {
            var options = new IniNormalizationOptions();
            Assert.False(options.IncludeEmptySections);
        }

        [Fact]
        public void GivenANormalizationOptionsInstanceUsingTheDefaultConstrutorThenIncludeEmptyPropertiesMustBeFalse()
        {
            var options = new IniNormalizationOptions();
            Assert.False(options.IncludeEmptyProperties);
        }

        [Fact]
        public void GivenANormalizationOptionsInstanceUsingTheDefaultConstrutorThenIsCaseSensitiveMustBeFalse()
        {
            var options = new IniNormalizationOptions();
            Assert.False(options.IsCaseSensitive);
        }

        [Fact]
        public void GivenANormalizationOptionsInstanceUsingTheDefaultConstrutorThenReplaceOnDuplicatedPropertiesMustBeFalse()
        {
            var options = new IniNormalizationOptions();
            Assert.False(options.ReplaceOnDuplicatedProperties);
        }

        [Fact]
        public void GivenANormalizationOptionsInstanceUsingTheDefaultConstrutorThenMergeOnDuplicatedSectionsMustBeFalse()
        {
            var options = new IniNormalizationOptions();
            Assert.False(options.MergeOnDuplicatedSections);
        }

        [Fact]
        public void GivenANormalizationOptionsInstanceUsingTheDefaultConstrutorThenIgnoreErrorsMustBeFalse()
        {
            var options = new IniNormalizationOptions();
            Assert.False(options.IgnoreErrors);
        }
    }
}
