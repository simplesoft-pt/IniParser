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

using System.Collections.Generic;
using System.Linq;
using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Normalization
{
    public class IniNormalizerTests
    {
        [Fact]
        public void GivenANormalizerReceivingCustomOptionsThenOptionsPropertyAndOriginAreTheSame()
        {
            var options = new IniNormalizationOptions();
            var normalizer = new IniNormalizer(options);

            Assert.Same(options, normalizer.Options);
        }

        [Fact]
        public void GivenANormalizerWithDefaultOptionsThenEmptyCommentsAreRemoved()
        {
            var container = BuildNonNormalizedContainer();
            var normalizer = BuildNormalizerIgnoringErrors();

            var normalizedContainer = normalizer.Normalize(container);

            var allComments = new List<string>(normalizedContainer.GlobalComments);
            foreach (var section in normalizedContainer.Sections)
            {
                allComments.AddRange(section.Comments);
            }

            Assert.False(allComments.Any(string.IsNullOrWhiteSpace));
        }

        [Fact]
        public void GivenANormalizerWithIncludeEmptyCommentsOptionThenEmptyCommentsAreRemoved()
        {
            var container = BuildNonNormalizedContainer();
            var normalizer = BuildNormalizerIgnoringErrors();
            normalizer.Options.IncludeEmptyComments = true;

            var normalizedContainer = normalizer.Normalize(container);

            var allComments = new List<string>(normalizedContainer.GlobalComments);
            foreach (var section in normalizedContainer.Sections)
            {
                allComments.AddRange(section.Comments);
            }

            Assert.True(allComments.Any(string.IsNullOrWhiteSpace));
        }

        private static IniNormalizer BuildNormalizerIgnoringErrors()
        {
            return new IniNormalizer {Options = {ThrowExceptions = false}};
        }

        private static IniContainer BuildNonNormalizedContainer()
        {
            return new IniContainer
            {
                GlobalComments =
                {
                    "Global comment 01",
                    string.Empty,
                    null,
                    "Global comment 02"
                },
                GlobalProperties =
                {
                    new IniProperty("GProp01", "GProp01-Value"),
                    new IniProperty("GProp02", string.Empty),
                    new IniProperty("GProp03"),
                    new IniProperty("GProp04", "GProp04-Value"),
                    new IniProperty("GPropDuplicated", "GPropDuplicated-Value01"),
                    new IniProperty("GPropDuplicated", "GPropDuplicated-Value02"),
                },
                Sections =
                {
                    new IniSection("SectionEmpty"),
                    new IniSection("Section01")
                    {
                        Comments =
                        {
                            "Section01 comment 01",
                            string.Empty,
                            null,
                            "Section01 comment 02"
                        },
                        Properties =
                        {
                            new IniProperty("Section01Prop01", "Section01Prop01-Value"),
                            new IniProperty("Section01Prop02", string.Empty),
                            new IniProperty("Section01Prop03"),
                            new IniProperty("Section01Prop04", "Section01Prop01-Value"),
                            new IniProperty("Section01PropDuplicated", "Section01PropDuplicated-Value01"),
                            new IniProperty("Section01PropDuplicated", "Section01PropDuplicated-Value02"),
                        }
                    },
                    new IniSection("Section02")
                    {
                        Comments =
                        {
                            "Section02 comment 01",
                            string.Empty,
                            null,
                            "Section02 comment 02"
                        },
                        Properties =
                        {
                            new IniProperty("Section02Prop01", "Section02Prop01-Value"),
                            new IniProperty("Section02Prop02", string.Empty),
                            new IniProperty("Section02Prop03"),
                            new IniProperty("Section02Prop04", "Section02Prop01-Value"),
                            new IniProperty("Section02PropDuplicated", "Section02PropDuplicated-Value01"),
                            new IniProperty("Section02PropDuplicated", "Section02PropDuplicated-Value02"),
                        }
                    },
                    new IniSection("SectionDuplicated")
                    {
                        Properties =
                        {
                            new IniProperty("SectionDuplicatedProp01", "SectionDuplicatedProp01-Value01"),
                            new IniProperty("SectionDuplicatedProp02", "SectionDuplicatedProp02-Value01"),
                            new IniProperty("SectionDuplicatedProp03", "SectionDuplicatedProp03-Value01"),
                        }
                    },
                    new IniSection("SectionDuplicated")
                    {
                        Properties =
                        {
                            new IniProperty("SectionDuplicatedProp02", "SectionDuplicatedProp02-Value02")
                        }
                    }
                }
            };
        }
    }
}
