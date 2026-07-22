---
title: Version adaptive apps
description: Learn how to take advantage of new Windows APIs while maintaining compatibility with previous versions in your Windows App SDK application.
author: GrantMeStrength
ms.author: jken
ms.topic: concept-article
ms.date: 07/08/2026
---

# Version adaptive apps

Each release of the Windows SDK adds new functionality that you might want to take advantage of. However, not all your customers update their devices to the latest version of Windows at the same time. You want your app to work on the broadest possible range of devices while also taking advantage of new features when available.

## Prerequisites

- Windows App SDK 1.0 or later
- Visual Studio 2022 or later with the Windows application development workload

## Overview

Follow these three steps to support the broadest range of Windows devices:

1. Configure your Visual Studio project to target the latest APIs. This affects what the compiler has access to at build time.
2. Perform runtime checks to ensure you only call APIs that are present on the device your app runs on.
3. Test your app on the minimum version and the target version of Windows.

## Configure your Visual Studio project

The first step in supporting multiple Windows versions is to specify the *Target* and *Minimum* supported OS/SDK versions in your Visual Studio project.

- **Target** — the SDK version that Visual Studio compiles your app code against. All APIs and resources in this SDK version are available in your app code at compile time.
- **Minimum** — the SDK version that supports the earliest OS version your app can run on.

During runtime, your app runs against the OS version it is deployed to. Your app throws exceptions if you use APIs that are not available in that version. Use runtime checks to call the correct APIs, as described later in this article.

> [!TIP]
> Visual Studio does not warn you about API compatibility. It is your responsibility to test and verify that your app works correctly on all OS versions between and including the Minimum and Target.

When you create a new project in Visual Studio, set the Target and Minimum versions your app supports. By default, the Target Version is the highest installed SDK version, and the Minimum Version is the lowest installed SDK.

To change the Minimum and Target version for an existing project, go to **Project** > **Properties** > **Application** > **Targeting**.

## Perform API checks

The key to version adaptive apps is the combination of API contracts and the [ApiInformation](/uwp/api/windows.foundation.metadata.apiinformation) class. This class lets you detect whether a specified API contract, type, or member is present so that you can safely make API calls across a variety of devices and OS versions.

### API contracts

The set of APIs within a device family is broken down into subdivisions known as API contracts. Use the `ApiInformation.IsApiContractPresent` method to test for the presence of an API contract. This is useful when you want to test for many APIs that all exist in the same version of an API contract.

```csharp
bool isScannerContractPresent =
    Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent(
        "Windows.Devices.Scanners.ScannerDeviceContract", 1);
```

An API contract represents a feature — a set of related APIs that together deliver a particular capability. A platform that implements any API in an API contract must implement every API in that contract. Test for the contract once and then safely call any of its APIs.

The largest and most commonly used contract is `Windows.Foundation.UniversalApiContract`, which contains the majority of the Windows Runtime APIs. For a list of available contracts, see [Device family extension SDKs and API contracts](/uwp/extension-sdks/).

### Version adaptive code and conditional XAML

Use the `ApiInformation` class in a condition in your code to test for the presence of an API before calling it. Methods such as `IsTypePresent`, `IsEventPresent`, `IsMethodPresent`, and `IsPropertyPresent` let you test at the granularity you need.

For more information and examples, see [Version adaptive code](version-adaptive-code.md).

If your app's Minimum Version is build 17763 (version 1809) or later, you can use *conditional XAML* to set properties and instantiate objects in markup without code behind. Conditional XAML provides a way to use `ApiInformation.IsApiContractPresent` directly in markup.

For more information and examples, see [Conditional XAML](conditional-xaml.md).

## Test your version adaptive app

When you use version adaptive code or conditional XAML, test your app on a device running the Minimum Version and on a device running the Target Version of Windows.

You can't test all conditional code paths on a single device. Deploy and test your app on a remote device (or virtual machine) running the minimum supported OS version to ensure all code paths work correctly.

## Related content

- [Version adaptive code](version-adaptive-code.md)
- [Conditional XAML](conditional-xaml.md)
