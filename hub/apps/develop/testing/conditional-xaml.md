---
title: Conditional XAML
description: Use new APIs in XAML markup while maintaining compatibility with previous Windows versions in your Windows App SDK application.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 05/22/2026
---

# Conditional XAML

*Conditional XAML* provides a way to use the [ApiInformation.IsApiContractPresent](/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent) method directly in XAML markup. You can set properties based on the presence of an OS-level API contract without writing code behind. Conditional statements are evaluated at runtime — elements qualified with a conditional XAML tag are parsed if they evaluate to `true` and ignored otherwise.

Conditional XAML requires Windows 10 version 1809 (build 17763) or later, which is the minimum OS version for Windows App SDK apps.

## Prerequisites

- A Windows App SDK project. For setup steps, see [Create your first WinUI 3 app](../../get-started/start-here.md).
- Familiarity with [Version adaptive apps](version-adaptive-apps.md) and the `ApiInformation` class.

> [!IMPORTANT]
> Conditional XAML uses `ApiInformation` methods, which check for the presence of **Windows Runtime** (`Windows.*`) API contracts and types supplied by the OS. These checks do **not** apply to WinUI 3 (`Microsoft.UI.Xaml.*`) controls, because WinUI 3 ships with your app through the Windows App SDK rather than with the OS — every WinUI 3 control your app is built against is always present at runtime, regardless of which Windows 10/11 build the device is running. `#if` preprocessor directives don't help here either: they're evaluated at compile time based on the target framework, not at runtime based on the OS or Windows App SDK version actually installed. To gate a feature on the Windows App SDK version your app is running against, check the SDK version at build time or use a try/catch around the API call. See [Version adaptive code](version-adaptive-code.md) for details.

For background information about `ApiInformation` and API contracts, see [Version adaptive apps](version-adaptive-apps.md).

## Conditional namespaces

To use a conditional method in XAML, declare a conditional [XAML namespace](/windows/uwp/xaml-platform/xaml-namespaces-and-namespace-mapping) at the top of your page:

```xml
xmlns:myNamespace="schema?conditionalMethod(parameter)"
```

The content before the `?` delimiter is the namespace or schema. The content after `?` is the conditional method that determines whether the namespace evaluates to `true` or `false`.

In most cases, the schema is the default XAML namespace:

```xml
xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
```

Conditional XAML supports these conditional methods:

| Method | Inverse |
|---|---|
| `IsApiContractPresent(ContractName, VersionNumber)` | `IsApiContractNotPresent(ContractName, VersionNumber)` |
| `IsTypePresent(ControlType)` | `IsTypeNotPresent(ControlType)` |
| `IsPropertyPresent(ControlType, PropertyName)` | `IsPropertyNotPresent(ControlType, PropertyName)` |

> [!NOTE]
> Use `IsApiContractPresent` and `IsApiContractNotPresent` for the best design-time experience. Other conditionals are not fully supported in the Visual Studio design experience.

## Set a property conditionally

This example displays text in a `TextBlock` only when the app runs on Windows 10 version 1903 (May 2019 Update, build 18362) or later — a contract check that's meaningful because it's *newer* than the Windows App SDK's 1809 floor.

First, define a conditional namespace:

```xml
xmlns:contract8Present="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractPresent(Windows.Foundation.UniversalApiContract,8)"
```

Then prefix the property with the conditional namespace:

```xml
<TextBlock contract8Present:Text="Hello, Conditional XAML"/>
```

Here is the complete markup:

```xml
<Page
    x:Class="ConditionalTest.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:contract8Present="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractPresent(Windows.Foundation.UniversalApiContract,8)">

    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <TextBlock contract8Present:Text="Hello, Conditional XAML"/>
    </Grid>
</Page>
```

The equivalent check in code behind:

```csharp
TextBlock textBlock = new TextBlock();

if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
{
    textBlock.Text = "Hello, Conditional XAML";
}
```

> [!NOTE]
> Even though `IsApiContractPresent` takes a string for the contract name parameter, you don't put it in quotes in the XAML namespace declaration.
>
> Because the Windows App SDK's minimum supported OS version is 1809 (`UniversalApiContract` version 7), checking for a contract version at or below 7 always evaluates to `true` in a Windows App SDK app and provides no useful information. Only check for contract versions **higher** than 7.

## Use if/else conditions

To set different values depending on the API contract, define both the positive and negative conditional namespaces:

```xml
xmlns:contract8NotPresent="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractNotPresent(Windows.Foundation.UniversalApiContract,8)"
xmlns:contract8Present="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractPresent(Windows.Foundation.UniversalApiContract,8)"
```

Then set the property twice, each with a different conditional prefix. Only one is applied at runtime:

```xml
<TextBlock contract8NotPresent:Text="Hello, World"
           contract8Present:Text="Hello, May 2019 Update or later"/>
```

## Instantiate controls conditionally

> [!NOTE]
> Conditionally instantiating an *element* based on an OS API contract — as opposed to conditionally setting a *property* — is a UWP-specific pattern. It doesn't apply to WinUI 3 controls.
>
> In UWP, this pattern let you fall back to an older `Windows.UI.Xaml.Controls` control when a newer one wasn't available on the OS. In a Windows App SDK app, WinUI 3 controls (`Microsoft.UI.Xaml.Controls.*`) ship with your app through the Windows App SDK, not with the OS. Every control your app is built against — including `ColorPicker` — is guaranteed to be present at runtime, so there's no OS version to check before instantiating it.
>
> If you need to gate a WinUI 3 control or API on the version of the Windows App SDK your app targets, do that check at build time (by targeting a minimum Windows App SDK package version) or wrap the runtime call in a `try`/`catch` — not with conditional XAML.

## Related content

- [Version adaptive apps](version-adaptive-apps.md)
- [Version adaptive code](version-adaptive-code.md)
