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
            Assert.Equal("gc 01", container.GlobalComments[0]);
            Assert.Equal("gc 02", container.GlobalComments[1]);

            Assert.Equal(2, container.GlobalProperties.Count);
            Assert.Equal("GP01", container.GlobalProperties[0].Name);
            Assert.Equal("gp 01 value", container.GlobalProperties[0].Value);
            Assert.Equal("GP02", container.GlobalProperties[1].Name);
            Assert.Equal("gp 02 value", container.GlobalProperties[1].Value);

            Assert.Equal(2, container.Sections.Count);
        }

        #region Test Data

        private const string StandardNoErrors =
@";gc 01
;gc 02
gp01=gp 01 value
gp02=gp 02 value
[s01]
;s01 c01
;s01 c02
s01p01=s01 p01 value
s01p02=s01 p02 value
[s02]
;s02 c01
;s02 c02
s02p01=s02 p01 value
s02p02=s02 p02 value
";

        #endregion
    }
}
