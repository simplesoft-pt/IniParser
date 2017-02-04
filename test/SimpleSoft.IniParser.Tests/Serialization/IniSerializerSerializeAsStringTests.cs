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

using SimpleSoft.IniParser.Impl;
using Xunit;

namespace SimpleSoft.IniParser.Tests.Serialization
{
    public class IniSerializerSerializeAsStringTests
    {
        [Fact]
        public void GivenASerializerWithDefaultConstructorWhenSerializingAsStringThenMinimizedResultAreCreated()
        {
            var serializer = new IniSerializer();
            var result = serializer.SerializeAsString(ContainerToSerialize);

            Assert.Equal(ContainerSerializedAndMinimized, result);
        }

        [Fact]
        public void GivenASerializerAddingEmptyLinesWhenSerializingAsStringThenEmptyLinesAreAdded()
        {
            var serializer = new IniSerializer(new IniSerializationOptions
            {
                EmptyLineBeforeSection = true
            });
            var result = serializer.SerializeAsString(ContainerToSerialize);

            Assert.Equal(ContainerSerializedWithEmptyLines, result);
        }

        [Fact]
        public void GivenASerializerWithCustomPropertyDelimiterWhenSerializingAsStringThenDelimiterChanges()
        {
            var serializer = new IniSerializer(new IniSerializationOptions
            {
                PropertyNameValueDelimiter = ':'
            });
            var result = serializer.SerializeAsString(ContainerToSerialize);

            Assert.Equal(ContainerSerializedWithChangedPropertySeparator, result);
        }

        [Fact]
        public void GivenASerializerWithCustomCommentIndicatorWhenSerializingAsStringThenIndicatorChanges()
        {
            var serializer = new IniSerializer(new IniSerializationOptions
            {
                CommentIndicator = '-'
            });
            var result = serializer.SerializeAsString(ContainerToSerialize);

            Assert.Equal(ContainerSerializedWithChangedCommentIndicator, result);
        }

        #region Test Data

        private static readonly IniContainer ContainerToSerialize = new IniContainer
        {
            GlobalComments = {"gc 01", "gc 02"},
            GlobalProperties =
            {
                new IniProperty("gp01", "gp 01 value"),
                new IniProperty("gp02", "gp 02 value")
            },
            Sections =
            {
                new IniSection("s01")
                {
                    Comments = {"s01 c01", "s01 c02"},
                    Properties =
                    {
                        new IniProperty("s01p01", "s01 p01 value"),
                        new IniProperty("s01p02", "s01 p02 value")
                    }
                },
                new IniSection("s02")
                {
                    Comments = {"s02 c01", "s02 c02"},
                    Properties =
                    {
                        new IniProperty("s02p01", "s02 p01 value"),
                        new IniProperty("s02p02", "s02 p02 value")
                    }
                }
            }
        };

        private const string ContainerSerializedAndMinimized =
@";gc 01
;gc 02
GP01=gp 01 value
GP02=gp 02 value
[S01]
;s01 c01
;s01 c02
S01P01=s01 p01 value
S01P02=s01 p02 value
[S02]
;s02 c01
;s02 c02
S02P01=s02 p01 value
S02P02=s02 p02 value
";

        private const string ContainerSerializedWithEmptyLines =
@";gc 01
;gc 02
GP01=gp 01 value
GP02=gp 02 value

[S01]
;s01 c01
;s01 c02
S01P01=s01 p01 value
S01P02=s01 p02 value

[S02]
;s02 c01
;s02 c02
S02P01=s02 p01 value
S02P02=s02 p02 value
";

        private const string ContainerSerializedWithChangedPropertySeparator =
@";gc 01
;gc 02
GP01:gp 01 value
GP02:gp 02 value
[S01]
;s01 c01
;s01 c02
S01P01:s01 p01 value
S01P02:s01 p02 value
[S02]
;s02 c01
;s02 c02
S02P01:s02 p01 value
S02P02:s02 p02 value
";

        private const string ContainerSerializedWithChangedCommentIndicator =
@"-gc 01
-gc 02
GP01=gp 01 value
GP02=gp 02 value
[S01]
-s01 c01
-s01 c02
S01P01=s01 p01 value
S01P02=s01 p02 value
[S02]
-s02 c01
-s02 c02
S02P01=s02 p01 value
S02P02=s02 p02 value
";

        #endregion
    }
}
