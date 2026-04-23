---
title: PackageVersion struct
description: Represents a version of the Windows App SDK framework package (C#).
ms.topic: article
ms.date: 04/05/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, C#, interop, Bootstrapper, Bootstrapper API
ms.localizationpriority: low
---

# PackageVersion struct

Represents a version of the Windows App SDK framework package.

## Definition

Namespace: [Microsoft.Windows.ApplicationModel.DynamicDependency](microsoft.windows.applicationmodel.dynamicdependency.md)

Assembly: Microsoft.WindowsAppRuntime.Bootstrap.Net.dll

```csharp
public struct PackageVersion
```

## Constructors

* [PackageVersion constructors](#packageversion-constructors)

## Fields

* [Build](#build-field)
* [Major](#major-field)
* [Minor](#minor-field)
* [Revision](#revision-field)

## Methods

* [ToString()](#tostring-method)
* [ToVersion()](#toversion-method)

## PackageVersion constructors
Initializes a new instance of the **PackageVersion** class.

```csharp
public PackageVersion(ushort major);
public PackageVersion(ushort major, ushort minor);
public PackageVersion(ushort major, ushort minor, ushort build);
public PackageVersion(ushort major, ushort minor, ushort build, ushort revision);
public PackageVersion(ulong version);
```

### Parameters
`major` [ushort](/dotnet/api/system.uint16)

The `major` position of a `major.minor.build.revision` sequence.

`minor` [ushort](/dotnet/api/system.uint16)

The `minor` position of a `major.minor.build.revision` sequence. Defaults to 0.

`build` [ushort](/dotnet/api/system.uint16)

The `build` position of a `major.minor.build.revision` sequence. Defaults to 0.

`revision` [ushort](/dotnet/api/system.uint16)

The `revision` position of a `major.minor.build.revision` sequence. Defaults to 0.

`version` [ulong](/dotnet/api/system.uint64)

A `major.minor.build.revision` sequence encoded as a (little-endian) **UInt64** (so that the first 16 bits contain the revision, and so on).

## Build field
Gets or sets the `build` position of a `major.minor.build.revision` sequence.

```csharp
public ushort Build;
```

## Major field
Gets or sets the `major` position of a `major.minor.build.revision` sequence.

```csharp
public ushort Major;
```

## Minor field
Gets or sets the `minor` position of a `major.minor.build.revision` sequence.

```csharp
public ushort Minor;
```

## Revision field
Gets or sets the `revision` position of a `major.minor.build.revision` sequence.

```csharp
public ushort Revision;
```

## ToString method
Retrieves the version as a string.

```csharp
public override string ToString();
```

### Returns
[string](/dotnet/api/system.string)

The `major.minor.build.revision` sequence encoded as a string.

## ToVersion method
Retrieves the version as a **UInt64**.

```csharp
public ulong ToVersion();
```

### Returns
[ulong](/dotnet/api/system.uint64)

The `major.minor.build.revision` sequence encoded as a (little-endian) **UInt64** (so that the first 16 bits contain the revision, and so on).

## Applies to

| Product | Introduced in |
|-|-|
|**Windows App SDK**|Windows App SDK 1.0|

## See also

* [Microsoft.Windows.ApplicationModel.DynamicDependency namespace](microsoft.windows.applicationmodel.dynamicdependency.md)
