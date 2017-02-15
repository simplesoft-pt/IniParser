# SimpleSoft.IniParser
Library implemented in .NET using C# that helps developers to deserialize or serialize INI files and strings.
It is compatible with a large range of .NET framework versions, from desktop, to mobile and servers.

## Installation 
This library can be installed via [NuGet](https://www.nuget.org/packages/SimpleSoft.IniParser) package. Just run the following command:

```powershell
Install-Package SimpleSoft.IniParser -Pre
```
The most recent version is in Release Candidate 1. It is considered very stable, just missing some extension methods to make it simpler to manage `IniContainer`, `IniSection` or `IniProperties` instances and compatibility with older PCL.

## Compatibility

This library is compatible with the folowing frameworks:

* .NET Framework 4.5
* .NET Core 5.0;
* .NET Standard 1.0;

## Usage (Version 1.0.0-rc01)

### Basic example:

```csharp
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

```

### Default global properties:

```csharp
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

```

