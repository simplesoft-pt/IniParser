#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 João Simões
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

using System.Linq;
using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Normalization
{
    public class IniNormalizerIniContainerTests
    {
        #region IniContainer -> Empty Global Comments

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedContainerThenEmptyGlobalCommentsMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = ContainerForEmptyComments;
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.NotEmpty(result.GlobalComments);
            Assert.Equal(1, result.GlobalComments.Count);
            Assert.False(result.GlobalComments.Any(string.IsNullOrWhiteSpace));
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedContainerThenEmptyGlobalCommentsMustBeKept()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IncludeEmptyComments = true
            });

            var source = ContainerForEmptyComments;
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.NotEmpty(result.GlobalComments);
            Assert.Equal(source.GlobalComments.Count, result.GlobalComments.Count);
            Assert.True(result.GlobalComments.Any(string.IsNullOrWhiteSpace));
        }

        #endregion

        #region IniContainer -> Empty Global Properties

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedContainerThenEmptyGlobalPropertiesMustBeRemoved()
        {
            var normalizer = new IniNormalizer();

            var source = ContainerForEmptyProperties;
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.NotEmpty(result.GlobalProperties);
            Assert.Equal(1, result.GlobalProperties.Count);
            Assert.False(result.GlobalProperties.Any(e => e.IsEmpty));
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsWhenNormalizedContainerThenEmptyGlobalPropertiesMustBeKept()
        {
            var normalizer = new IniNormalizer(new IniNormalizationOptions
            {
                IncludeEmptyProperties = true
            });

            var source = ContainerForEmptyProperties;
            var result = normalizer.Normalize(source);

            Assert.NotNull(result);
            Assert.NotEmpty(result.GlobalProperties);
            Assert.Equal(source.GlobalProperties.Count, result.GlobalProperties.Count);
            Assert.True(result.GlobalProperties.Any(e => e.IsEmpty));
        }

        #endregion

        #region Test Data

        private static readonly IniContainer ContainerForEmptyComments = new IniContainer
        {
            GlobalComments =
            {
                "comment",
                "     ",
                string.Empty,
                null
            }
        };

        private static readonly IniContainer ContainerForEmptyProperties= new IniContainer
        {
            GlobalProperties =
            {
                new IniProperty("gp01", "value"),
                new IniProperty("gp02", "    "),
                new IniProperty("gp03", string.Empty),
                new IniProperty("gp04"),
            }
        };

        #endregion
    }
}