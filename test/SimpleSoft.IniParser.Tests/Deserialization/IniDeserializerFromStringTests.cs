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
