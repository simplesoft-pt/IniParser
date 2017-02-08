#region License
// The MIT License (MIT)
// 
// Copyright (c) 2017 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System.Collections.Generic;
using System.Linq;
using SimpleSoft.IniParser.Exceptions;
using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Normalization
{
    public class IniNormalizerIniSectionTests
    {
        #region Single -> Name

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedSectionThenNameMustBeUpperCase()
        {
            var normalizer = new IniNormalizer();

            var source = SectionForCaseSensitive;
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.NotEqual(source.Name, result.Name);
            Assert.Equal(source.Name.ToUpperInvariant(), result.Name);
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenNormalizedSectionThenNameMustBeOriginal()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IsCaseSensitive = true
            });

            var source = SectionForCaseSensitive;
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizeSectionThenNameMustBeUpperCase()
        {
            var normalizer = new IniNormalizer();

            var source = SectionForCaseSensitive;
            IniSection result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.NotEqual(source.Name, result.Name);
            Assert.Equal(source.Name.ToUpperInvariant(), result.Name);
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenTryedToNormalizeSectionThenNameMustBeOriginal()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IsCaseSensitive = true
            });

            var source = SectionForCaseSensitive;
            IniSection result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
        }

        #endregion

        #region Single -> Comments

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedSectionThenEmptyCommentsMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = SectionForEmptyComments;
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Comments);
            Assert.False(result.Comments.Any(string.IsNullOrWhiteSpace));
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenNormalizedSectionThenEmptyCommentsMustBeKept()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IncludeEmptyComments = true
            });

            var source = SectionForEmptyComments;
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Comments);
            Assert.True(result.Comments.Any(string.IsNullOrWhiteSpace));
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizeSectionThenEmptyCommentsMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = SectionForEmptyComments;
            IniSection result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.NotEmpty(result.Comments);
            Assert.False(result.Comments.Any(string.IsNullOrWhiteSpace));
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenTryedToNormalizeSectionThenEmptyCommentsMustBeKept()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IsCaseSensitive = true
            });

            var source = SectionForEmptyComments;
            IniSection result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
        }

        #endregion

        #region Collections -> Empty

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedSectionCollectionThenEmptyMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = SectionsWithEmptyContent;
            var destination = new List<IniSection>();
            normalizer.NormalizeInto(source, destination);

            Assert.NotEmpty(destination);
            Assert.False(destination.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerIncludingEmptyPropertiesWhenNormalizedSectionCollectionThenEmptyMustBeKept()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IncludeEmptySections = true
            });

            var source = SectionsWithEmptyContent;
            var destination = new List<IniSection>();
            normalizer.NormalizeInto(source, destination);

            Assert.Equal(source.Length, destination.Count);
            Assert.True(destination.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizeSectionCollectionThenEmptyMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = SectionsWithEmptyContent;
            var destination = new List<IniSection>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.NotEmpty(destination);
            Assert.False(destination.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerIncludingEmptyPropertiesWhenTryedToNormalizeSectionCollectionThenEmptyMustBeKept()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IncludeEmptySections = true
            });

            var source = SectionsWithEmptyContent;
            var destination = new List<IniSection>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(source.Length, destination.Count);
            Assert.True(destination.Any(e => e.IsEmpty));
        }

        #endregion

        #region Collections -> Duplicated

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedSectionCollectionThenDuplicatedMustFaild()
        {
            var normalizer = new IniNormalizer();

            var source = SectionsWithDuplicatedCaseInsensitiveNames;
            var destination = new List<IniSection>();

            var ex = Assert.Throws<DuplicatedSection>(() =>
            {
                normalizer.NormalizeInto(source, destination);
            });

            Assert.NotNull(ex.SectionName);
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenNormalizedSectionCollectionThenCaseSensitiveKeysWillPass()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IsCaseSensitive = true
            });

            var source = SectionsWithDuplicatedCaseInsensitiveNames;
            var destination = new List<IniSection>();
            normalizer.NormalizeInto(source, destination);

            Assert.Equal(source.Length, destination.Count);
        }

        [Fact]
        public void GivenANormalizerIgnoringExceptionsWhenNormalizedSectionCollectionThenDuplicatedWillPass()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                ThrowExceptions = false
            });

            var source = SectionsWithDuplicatedCaseInsensitiveNames;
            var destination = new List<IniSection>();
            normalizer.NormalizeInto(source, destination);

            Assert.Equal(1, destination.Count);
        }

        [Fact]
        public void GivenANormalizerMergingSectionsWhenNormalizedSectionCollectionThenDuplicatedWillPass()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                MergeOnDuplicatedSections = true
            });

            var source = SectionsWithDuplicatedCaseInsensitiveNames;
            var destination = new List<IniSection>();
            normalizer.NormalizeInto(source, destination);

            Assert.Equal(1, destination.Count);
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenTryedToNormalizeSectionCollectionThenDuplicatedMustFaild()
        {
            var normalizer = new IniNormalizer();

            var source = SectionsWithDuplicatedCaseInsensitiveNames;
            var destination = new List<IniSection>();
            Assert.False(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(0, destination.Count);
        }

        [Fact]
        public void GivenANormalizerCaseSensitiveWhenTryedToNormalizeSectionCollectionThenCaseSensitiveKeysWillPass()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IsCaseSensitive = true
            });

            var source = SectionsWithDuplicatedCaseInsensitiveNames;
            var destination = new List<IniSection>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(source.Length, destination.Count);
        }

        [Fact]
        public void GivenANormalizerIgnoringExceptionsWhenTryedToNormalizeSectionCollectionThenDuplicatedWillPass()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                ThrowExceptions = false
            });

            var source = SectionsWithDuplicatedCaseInsensitiveNames;
            var destination = new List<IniSection>();
            Assert.False(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(0, destination.Count);
        }

        [Fact]
        public void GivenANormalizerMergingSectionsWhenTryedToNormalizeSectionCollectionThenDuplicatedWillPass()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                MergeOnDuplicatedSections = true
            });

            var source = SectionsWithDuplicatedCaseInsensitiveNames;
            var destination = new List<IniSection>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(1, destination.Count);
        }

        #endregion

        #region TestData

        private static readonly IniSection SectionForCaseSensitive = new IniSection("s01");

        private static readonly IniSection SectionForEmptyComments = new IniSection("s01")
        {
            Comments =
            {
                "comment",
                string.Empty,
                "comment",
                null
            }
        };

        private static readonly IniSection[] SectionsWithEmptyContent =
        {
            new IniSection("s01") {Comments = {"comment", "Comment"}},
            new IniSection("s02") {Properties = {new IniProperty("p01", "value")}},
            new IniSection("s03"),
        };

        private static readonly IniSection[] SectionsWithDuplicatedCaseInsensitiveNames =
        {
            new IniSection("s01") {Comments = {"s01c01"}, Properties = {new IniProperty("p01", "value")}},
            new IniSection("S01") {Comments = {"S01c01"}, Properties = {new IniProperty("p02", "value")}},
        };

        #endregion
    }
}
