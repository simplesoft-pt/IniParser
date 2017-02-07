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

namespace SimpleSoft.IniParser.Tests.Deserialization
{
    public class IniDeserializerFromStringTests
    {
        [Fact]
        public void GivenADeserializerWithDefaultConstructorWhenDeserializingFromStringThenStandardContainerMatch()
        {
            var deserializer = new IniDeserializer();
            var container = deserializer.DeserializeAsContainer(StandardNoErrors);

            Assert.NotNull(container);
            Assert.Equal(2, container.GlobalComments.Count);
            Assert.Equal(2, container.GlobalProperties.Count);
            Assert.Equal(2, container.Sections.Count);
        }

        #region Test Data

        private const string StandardNoErrors =
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

        #endregion
    }
}
