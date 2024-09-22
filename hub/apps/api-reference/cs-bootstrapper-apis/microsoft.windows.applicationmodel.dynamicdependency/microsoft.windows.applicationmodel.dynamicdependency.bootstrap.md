---
title: Bootstrap class
description: The **Bootstrap** class contains static helper methods that conveniently wrap calls to the [Bootstrapper API](/windows/windows-app-sdk/api/win32/_bootstrap/).
ms.topic: article
ms.date: 04/05/2022
keywords: windows 10, windows 11, Windows App SDK, desktop development, C#, interop, Bootstrapper, Bootstrapper API
ms.localizationpriority: low
---

# Bootstrap class

The **Bootstrap** class contains static helper methods that conveniently wrap calls to the [Bootstrapper API](/windows/windows-app-sdk/api/win32/_bootstrap/).

## Definition

Namespace: [Microsoft.Windows.ApplicationModel.DynamicDependency](microsoft.windows.applicationmodel.dynamicdependency.md)

Assembly: Microsoft.WindowsAppRuntime.Bootstrap.Net.dll

```csharp
public class Bootstrap
```

## Methods

* [Initialize methods](#initialize-methods)
* [Shutdown()](#shutdown-method)
* [TryInitialize methods](#tryinitialize-methods)

## Initialize methods
Initializes the calling process to use Windows App SDK's framework package. Finds a Windows App SDK framework package meeting the criteria provided in the arguments, and makes it available for use by the current process. If multiple packages meet the criteria, then the best candidate is selected.

```csharp
public static void Initialize(uint majorMinorVersion);
public static void Initialize(uint majorMinorVersion, string versionTag);
public static void Initialize(uint majorMinorVersion, string versionTag, PackageVersion minVersion);
```

### Parameters
`majorMinorVersion` [uint](/dotnet/api/system.uint32)

The major and minor version of the Windows App SDK framework package to load. The version is encoded as `0xMMMMNNNN`, where `M` = Major and `N` = Minor (for example, version 1.2 should be encoded as `0x00010002`).

`versionTag` [string](/dotnet/api/system.string)

The version tag of the Windows App SDK framework package to load (if any). For example, `"prerelease"`. Defaults to `null`.

`minVersion` [PackageVersion](microsoft.windows.applicationmodel.dynamicdependency.packageversion.md)

The minimum version of the Windows App SDK framework package to use. Defaults to a new default instance of **PackageVersion**.

## Shutdown method
Removes the changes made to the current process by [Initialize](#initialize-methods) or [TryInitialize](#tryinitialize-methods). After **Shutdown** is called, your app can no longer call Windows App SDK APIs, including the [Dynamic dependency API](/windows/windows-app-sdk/api/win32/_dynamicdependency/).

```csharp
public static void Shutdown();
```

## TryInitialize methods

Initializes the calling process to use Windows App SDK's framework package. Failure returns false with the failure **HRESULT** in the *hresult* parameter. Finds a Windows App SDK framework package meeting the criteria provided in the arguments, and makes it available for use by the current process. If multiple packages meet the criteria, then the best candidate is selected.

```csharp
public static bool TryInitialize(uint majorMinorVersion, out int hresult);
public static bool TryInitialize(uint majorMinorVersion, string versionTag, out int hresult);
public static bool TryInitialize(uint majorMinorVersion, string versionTag, PackageVersion minVersion, out int hresult);
```

### Parameters
`majorMinorVersion` [uint](/dotnet/api/system.uint32)

The major and minor version of the Windows App SDK framework package to load. The version is encoded as `0xMMMMNNNN`, where `M` = Major and `N` = Minor (for example, version 1.2 should be encoded as `0x00010002`).

`hresult` [uint](/dotnet/api/system.uint32)

The failure **HRESULT**, if the initialization failed.

`versionTag` [string](/dotnet/api/system.string)

The version tag of the Windows App SDK framework package to load (if any). For example, `"prerelease"`. Defaults to `null`.

`minVersion` [PackageVersion](microsoft.windows.applicationmodel.dynamicdependency.packageversion.md)

The minimum version of the Windows App SDK framework package to use. Defaults to a new default instance of **PackageVersion**.

## Applies to

| Product | Introduced in |
|-|-|
|**Windows App SDK**|Windows App SDK 1.0|

## See also

* [Microsoft.Windows.ApplicationModel.DynamicDependency namespace](microsoft.windows.applicationmodel.dynamicdependency.md)
