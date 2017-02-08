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
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IsCaseSensitive = true
            });

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
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IsCaseSensitive = true
            });

            var source = PropertyForCaseSensitive;
            IniProperty result;
            Assert.True(normalizer.TryNormalize(source, out result));

            Assert.NotNull(result);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Value, result.Value);
        }

        #endregion

        #region Collections -> Empty

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
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IncludeEmptyProperties = true
            });

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
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IncludeEmptyProperties = true
            });

            var source = PropertiesWithEmptyValues;
            var destination = new List<IniProperty>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(source.Length, destination.Count);
            Assert.True(destination.Any(e => e.IsEmpty));
        }

        #endregion

        #region Collections -> Duplicated

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
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IsCaseSensitive = true
            });

            var source = PropertiesWithDuplicatedCaseInsensitiveKeys;
            var destination = new List<IniProperty>();
            normalizer.NormalizeInto(source, destination);

            Assert.Equal(source.Length, destination.Count);
        }

        [Fact]
        public void GivenANormalizerIgnoringExceptionsWhenNormalizedPropertyCollectionThenDuplicatedWillPass()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                ThrowExceptions = false
            });

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
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IsCaseSensitive = true
            });

            var source = PropertiesWithDuplicatedCaseInsensitiveKeys;
            var destination = new List<IniProperty>();
            Assert.True(normalizer.TryNormalizeInto(source, destination));

            Assert.Equal(source.Length, destination.Count);
        }

        [Fact]
        public void GivenANormalizerIgnoringExceptionsWhenTryedToNormalizePropertyCollectionThenDuplicatedWillPass()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                ThrowExceptions = false
            });

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
