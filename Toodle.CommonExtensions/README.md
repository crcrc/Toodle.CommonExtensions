# Toodle.CommonExtensions
![NuGet Version](https://img.shields.io/nuget/v/Toodle.CommonExtensions)
![NuGet Downloads](https://img.shields.io/nuget/dt/Toodle.CommonExtensions)
A collection of useful extension methods for caching, enum handling, geographic calculations, JSON serialization, and string manipulation.

## Installation

Install the package via NuGet:

```bash
dotnet add package Toodle.CommonExtensions
```

## Features

### Cache Extensions

The `CacheExtensions` class provides methods for generating cache keys:

#### Fast Cache Keys
```csharp
using Toodle.CommonExtensions.Cache;

var person = new Person { Id = 1, Name = "John" };
string key = person.ToCacheKeyFast(); // Returns "Person_[hash]"
string keyWithPrefix = person.ToCacheKeyFast("App1"); // Returns "App1_Person_[hash]"
```

**Note**: Hash codes from `ToCacheKeyFast` are not guaranteed to be consistent across application restarts.

#### Stable Cache Keys
```csharp
using Toodle.CommonExtensions.Cache;

var person = new Person { Id = 1, Name = "John" };
string key = person.ToCacheKeyStable(); // Returns "Person_[hash]"
string keyWithPrefix = person.ToCacheKeyStable("App1"); // Returns "App1_Person_[hash]"
```

Uses FNV-1a hash algorithm to ensure consistency across application restarts.

### Enum Extensions

The `EnumExtensions` class provides methods for working with enums:

```csharp
using Toodle.CommonExtensions;

public enum Status { Active, Inactive, Pending }

// Get all enum values sorted alphabetically
var allStatuses = EnumExtensions.GetEnumValues<Status>();

// Get enum values excluding specific items
var filteredStatuses = EnumExtensions.GetEnumValues<Status>(
    new[] { Status.Inactive }
);
```

### Geography Helper

The `GeographyHelper` class provides methods for geographic calculations:

```csharp
using Toodle.CommonExtensions.Helpers;

// Calculate distance between London and Paris
double distance = GeographyHelper.GetDistance(
    51.5074, -0.1278,  // London coordinates
    48.8566, 2.3522    // Paris coordinates
);
// Returns distance in meters using the Haversine formula
```

### JSON Extensions

The `JsonExtensions` class provides methods for JSON serialization:

```csharp
using Toodle.CommonExtensions.Json;

var person = new Person { Name = "John", Age = 30 };

// Basic serialization
string json = person.ToJson();

// Serialization with custom options
var options = new JsonSerializerOptions
{
    WriteIndented = true
};
string prettyJson = person.ToJson(options);
```

### String Extensions

The `StringExtensions` class provides string manipulation methods:

#### Converting to Initials
```csharp
using Toodle.CommonExtensions.String;

string initials = "hello world".ToInitials(); // Returns "HW"
string multipleWords = "United States of America".ToInitials(); // Returns "USOA"
string multipleSpaces = "  multiple   spaces   ".ToInitials(); // Returns "MS"
```

#### Removing Non-Alphabetic Characters
```csharp
using Toodle.CommonExtensions.String;

string cleaned = "Hello123World!".RemoveNonAlphabeticCharacters(); // Returns "HelloWorld"
string numbersOnly = "12345".RemoveNonAlphabeticCharacters(); // Returns ""
string withSpaces = "Hello World!".RemoveNonAlphabeticCharacters(); // Returns "HelloWorld"
```