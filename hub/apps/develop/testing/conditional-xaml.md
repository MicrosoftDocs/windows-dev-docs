---
title: Conditional XAML
description: Use new APIs in XAML markup while maintaining compatibility with previous Windows versions in your Windows App SDK application.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 10/13/2026
---

# Conditional XAML

*Conditional XAML* provides a way to use the [ApiInformation.IsApiContractPresent](/uwp/api/windows.foundation.metadata.apiinformation.isapicontractpresent) method directly in XAML markup. You can set properties based on the presence of an OS-level API contract without writing code behind. Conditional statements are evaluated at runtime — elements qualified with a conditional XAML tag are parsed if they evaluate to `true` and ignored otherwise.

Starting in Windows App SDK 2.0, you can also define your own conditions by implementing the [IXamlCondition](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.markup.ixamlcondition) interface. Custom conditions let you include or exclude markup based on app-specific state, such as feature flags or configuration settings. For more info, see [Create custom conditions](#create-custom-conditions).

Conditional XAML requires Windows 10 version 1809 (build 17763) or later, which is the minimum OS version for Windows App SDK apps.

## Prerequisites

- A Windows App SDK project. For setup steps, see [Create and run a WinUI 3 project](../../get-started/start-here.md).
- Familiarity with [Version adaptive apps](version-adaptive-apps.md) and the `ApiInformation` class.

> [!IMPORTANT]
> The built-in conditional methods use `ApiInformation` methods, which check for the presence of **Windows Runtime** (`Windows.*`) API contracts and types supplied by the OS. These checks do **not** apply to WinUI 3 (`Microsoft.UI.Xaml.*`) controls, because WinUI 3 ships with your app through the Windows App SDK rather than with the OS — every WinUI 3 control your app is built against is always present at runtime, regardless of which Windows 10/11 build the device is running. `#if` preprocessor directives don't help here either: they're evaluated at compile time based on the target framework, not at runtime based on the OS or Windows App SDK version actually installed. To gate a feature on the Windows App SDK version your app is running against, check the SDK version at build time or use a try/catch around the API call. See [Version adaptive code](version-adaptive-code.md) for details.

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

You can also use a condition that you define in your app. For more info, see [Create custom conditions](#create-custom-conditions).

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

To include or exclude elements based on logic that your app defines, such as a feature flag, use a [custom condition](#create-custom-conditions).

## Create custom conditions

Starting in Windows App SDK 2.0, you can define your own conditions and use them with the conditional namespace syntax. A custom condition lets you include or exclude markup based on app-specific state, such as feature flags, configuration settings, or device capabilities. Like the built-in conditional methods, custom conditions are evaluated when the XAML is parsed at runtime.

> [!div class="checklist"]
>
> - **Important APIs**: [IXamlCondition interface](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.markup.ixamlcondition), [IXamlCondition.Evaluate method](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.markup.ixamlcondition.evaluate)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see custom XAML conditionals in action](winui3gallery://item/CustomXamlConditionals)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

> [!NOTE]
> `IXamlCondition` replaces the `IXamlPredicate` interface from experimental releases of the Windows App SDK 2.0.

### Implement a condition

To create a condition, define a class that derives from [DependencyObject](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.dependencyobject) and implements `IXamlCondition`. The XAML parser calls the [Evaluate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.markup.ixamlcondition.evaluate) method and passes the text between the parentheses of the conditional namespace declaration as the `argument` parameter. Return `true` to include the qualified markup, or `false` to ignore it.

This example, from the WinUI 3 Gallery sample, evaluates feature flags that the app stores in a dictionary:

```csharp
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;

namespace MyApp;

public sealed partial class FeatureFlagCondition : DependencyObject, IXamlCondition
{
    public static IDictionary<string, bool> FeatureFlags { get; } = new Dictionary<string, bool>
    {
        ["NewExperience"] = true,
        ["LegacyMode"] = false,
    };

    // The XAML parser calls Evaluate with the text between the parentheses
    // in the conditional namespace declaration, for example "NewExperience".
    public bool Evaluate(string argument)
    {
        return FeatureFlags.TryGetValue(argument, out bool enabled) && enabled;
    }
}
```

### Declare a conditional namespace for the condition

First, map the namespace that contains your condition class by using a `using:` declaration. Then, after the `?` delimiter of a conditional namespace, reference the class through that prefix and pass the argument in parentheses:

```xml
xmlns:cond="using:MyApp"
xmlns:newExp="http://schemas.microsoft.com/winfx/2006/xaml/presentation?cond:FeatureFlagCondition(NewExperience)"
xmlns:legacy="http://schemas.microsoft.com/winfx/2006/xaml/presentation?cond:FeatureFlagCondition(LegacyMode)"
```

In this example, the parser calls `Evaluate("NewExperience")` for markup that uses the `newExp` prefix, and `Evaluate("LegacyMode")` for markup that uses the `legacy` prefix.

### Use the condition on elements, attributes, and setters

Use the conditional prefix the same way you use the built-in conditional namespaces. These examples are based on the WinUI 3 Gallery sample.

To include an element only when a condition is `true`, prefix the element:

```xml
<StackPanel Spacing="8">
    <!-- Included only when FeatureFlagCondition.Evaluate("NewExperience") returns true. -->
    <newExp:InfoBar Title="New experience" IsOpen="True" Severity="Success"
                    Message="This InfoBar is included because the NewExperience flag is true." />

    <!-- Included only when FeatureFlagCondition.Evaluate("LegacyMode") returns true. -->
    <legacy:InfoBar Title="Legacy mode" IsOpen="True" Severity="Warning"
                    Message="This InfoBar is included because the LegacyMode flag is true." />
</StackPanel>
```

To set a property only when a condition is `true`, prefix the attribute:

```xml
<!-- Make sure that no more than one of these conditions is true at runtime. -->
<Button newExp:Content="Open the new editor"
        legacy:Content="Open the classic editor" />
```

To apply a style setter only when a condition is `true`, prefix the `Setter` element:

```xml
<Page.Resources>
    <Style x:Key="ConditionalHeadingStyle" TargetType="TextBlock">
        <newExp:Setter Property="FontWeight" Value="SemiBold" />
        <newExp:Setter Property="FontSize" Value="28" />
        <legacy:Setter Property="FontWeight" Value="Normal" />
        <legacy:Setter Property="FontSize" Value="18" />
    </Style>
</Page.Resources>
```

### Set the state before the XAML is parsed

The parser caches the result of `Evaluate` for each combination of condition and argument for the lifetime of the process. If you change the state that a condition reads after the result is cached, markup that's already parsed doesn't change, and markup that's parsed later still uses the cached result. To use a different value, restart the app.

Set the state that your condition reads before the parser evaluates it, for example at app startup or before you call `InitializeComponent` in a page constructor:

```csharp
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        // Set the flags before InitializeComponent so that the XAML parser
        // uses these values when it evaluates the conditions.
        FeatureFlagCondition.FeatureFlags["NewExperience"] = true;
        FeatureFlagCondition.FeatureFlags["LegacyMode"] = false;

        this.InitializeComponent();
    }
}
```

### Limitations

- Two elements can't have the same `x:Name`, even if only one of them is included at runtime.
- If you apply more than one condition to the same attribute, make sure that only one of them evaluates to `true` at runtime. If more than one evaluates to `true`, the parser throws an exception.
- Because conditions are evaluated at runtime, the XAML compiler can't check them for logical consistency. Test each combination of condition values that your app supports.
- Custom conditions don't work with items from the `x:` namespace (`http://schemas.microsoft.com/winfx/2006/xaml`), such as `x:Uid` and `x:Key`.

## Related content

- [Version adaptive apps](version-adaptive-apps.md)
- [Version adaptive code](version-adaptive-code.md)
- [IXamlCondition interface](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.markup.ixamlcondition)
