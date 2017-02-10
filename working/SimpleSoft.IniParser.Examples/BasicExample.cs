using System;
using Microsoft.Extensions.Logging;
using SimpleSoft.IniParser.Impl;

namespace SimpleSoft.IniParser.Examples
{
    public class BasicExample : IExample
    {
        private readonly ILogger<BasicExample> _logger;

        public BasicExample(ILogger<BasicExample> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            const string initialIni = @"

;This is a comment
SomeGP=This is a global property
[SomeSection]
;This is a comment inside a section
SomeSP=This is a property inside a section
[AnotherSection]
;Another comment...
AnotherSP=More?
Response=YES!!!
";
            _logger.LogInformation("Initial INI string: '{initialIniString}'", initialIni);

            _logger.LogDebug("Deserializing string as an IniContainer...");
            var deserializer = new IniDeserializer
            {
                Options = {NormalizeAfterDeserialization = false}
            };
            var iniContainer = deserializer.DeserializeAsContainer(initialIni);

            _logger.LogDebug("Normalizing IniContainer...");
            var normalizer = new IniNormalizer();
            iniContainer = normalizer.Normalize(iniContainer);

            _logger.LogDebug("Serializing IniContainer as a string...");
            var serializer = new IniSerializer
            {
                Options = {EmptyLineBeforeSection = true, NormalizeBeforeSerialization = false}
            };
            var finalIni = serializer.SerializeAsString(iniContainer);

            _logger.LogInformation("Final INI string: " + Environment.NewLine + "'{finalIniString}'", finalIni);
            /*
;This is a comment
SOMEGP=This is a global property

[SOMESECTION]
;This is a comment inside a section
SOMESP=This is a property inside a section

[ANOTHERSECTION]
;Another comment...
ANOTHERSP=More?
RESPONSE=YES!!!
            */
        }
    }
}
