# SimpleSoft.IniParser
Library implemented in .NET using C# that helps developers to deserialize or serialize INI files and strings. This is a very small library with threem main principles:

* `IIniDeserializer` - interface that provides methods to deserialize strings or files into `IniContainer` instances. Implemented by `IniDeserializer` and can be configured using an `IniDeserializationOptions`;
* `IIniSerializer` - interface that provides methods to serialize `IniContainer` instances into strings or files. Implemented by `IniSerializer` and can be configured using an `IniSerializationOptions`;
* `IIniNormalizer` - interface that provides methods normalize `IniProperty`, `IniSection`, `IniContainer` instances, making sure they meet the standard. Implemented by `IniSerializer` and can be configured using an `IniSerializationOptions`;

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

