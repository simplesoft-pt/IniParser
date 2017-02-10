using System;
using Microsoft.Extensions.Logging;
using SimpleSoft.IniParser.Impl;

namespace SimpleSoft.IniParser.Examples
{
    public class DefaultGlobalInstanceExample : IExample
    {
        private readonly ILogger<DefaultGlobalInstanceExample> _logger;

        public DefaultGlobalInstanceExample(ILogger<DefaultGlobalInstanceExample> logger)
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
            var iniContainer = IniDeserializer.Default.DeserializeAsContainer(initialIni);

            _logger.LogDebug("Normalizing IniContainer...");
            iniContainer = IniNormalizer.Default.Normalize(iniContainer);

            _logger.LogDebug("Serializing IniContainer as a string...");
            var finalIni = IniSerializer.Default.SerializeAsString(iniContainer);

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
