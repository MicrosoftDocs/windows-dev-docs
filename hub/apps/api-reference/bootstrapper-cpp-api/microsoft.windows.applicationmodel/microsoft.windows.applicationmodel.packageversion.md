---
title: PackageVersion class
description: Represents a version of the Windows App SDK framework package (C++).
ms.topic: article
ms.date: 03/22/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, app sdk, bootstrapper, bootstrapper api
ms.localizationpriority: low
---

# PackageVersion class

Represents a version of the Windows App SDK framework package.

## Syntax
```cpp
class PackageVersion : public PACKAGE_VERSION;
```

## Requirements
**Minimum supported SDK:** Windows App SDK version 1.1

**Namespace:** Microsoft::Windows::ApplicationModel

**Header:** MddBootstrap.h

## Constructors
|Constructor|Description|
|------------|-----------------|
|[PackageVersion constructor](#packageversionpackageversion-constructor)|Initializes a new instance of the **PackageVersion** class.|

## Member functions
|Function|Description|
|------------|-----------------|
|[PackageVersion::ToString function](#packageversiontostring-function)|Retrieves the version as a **std::wstring**.|
|[PackageVersion::ToVersion function](#packageversiontoversion-function)|Retrieves the version as a **uint64_t**.|

## PackageVersion::PackageVersion constructor
Initializes a new instance of the **PackageVersion** class.

### Syntax
```cpp
PackageVersion();
PackageVersion(uint16_t major, uint16_t minor = 0, uint16_t build = 0, uint16_t revision = 0);
PackageVersion(uint64_t version);
```

### Parameters
`major`
A **uint16_t** value representing the `major` position of a `major.minor.build.revision` sequence.

`minor`
An optional **uint16_t** value representing the `minor` position of a `major.minor.build.revision` sequence. Defaults to 0.

`build`
An optional **uint16_t** value representing the `build` position of a `major.minor.build.revision` sequence. Defaults to 0.

`revision`
An optional **uint16_t** value representing the `revision` position of a `major.minor.build.revision` sequence. Defaults to 0.

`version`
A `major.minor.build.revision` sequence encoded as a (little-endian) **uint64_t** (so that the first 16 bits contain the revision, and so on).

## PackageVersion::ToString function
Retrieves the version as a **std::wstring**.

### Syntax
```cpp
std::wstring ToString() const;
```

### Return value 
The `major.minor.build.revision` sequence encoded as a string (**std::wstring**) in the format "12345.12345.12345.12345" + null-terminator.

## PackageVersion::ToVersion function
Retrieves the version as a **uint64_t**.

### Syntax
```cpp
uint64_t ToVersion() const;
```

### Return value 
The `major.minor.build.revision` sequence encoded as a (little-endian) **uint64_t** (so that the first 16 bits contain the revision, and so on).

## See also
* [Microsoft::Windows::ApplicationModel namespace](microsoft.windows.applicationmodel.md)
