---
description: Design your app to support the layouts and fonts of multiple languages, including RTL (right-to-left) flow direction.
title: Adjust layout and fonts, and support RTL
ms.assetid: F2522B07-017D-40F1-B3C8-C4D0DFD03AC3
label: Adjust layout and fonts, and support RTL
template: detail.hbs
ms.date: 05/11/2018
ms.topic: article
keywords: windows 10, uwp, localizability, localization, rtl, ltr
ms.localizationpriority: medium
---
# Adjust layout and fonts, and support RTL
Design your app to support the layouts and fonts of multiple languages, including RTL (right-to-left) flow direction. Flow direction is the direction in which script is written and displayed, and the UI elements on the page are scanned by the eye.

## Layout guidelines
Languages such as German and Finnish typically use more characters than English does. East Asian fonts typically require more height. And languages such as Arabic and Hebrew require that layout panels and text elements be laid out in right-to-left (RTL) reading order.

Because of these variations in the metrics of translated text, we recommend that you don't bake absolute positioning, fixed widths, or fixed heights into your user interface (UI). Instead, take advantage of the dynamic layout mechanisms that are built into the Windows UI elements. For example, content controls (such as buttons), items controls (such as grid views and list views), and layout panels (such as grids and stackpanels) automatically resize and reflow by default to fit their content. Pseudo-localize your app to uncover any problematic edge cases where your UI elements don't size to content properly.

Dynamic layout is the recommended technique, and you'll be able to use it in the majority of cases. Less preferable, but still better than baking sizes into your UI markup, is to set layout values as a function of language. Here's an example of how you can expose a Width property in your app as a resource that localizers can set appropriately per language. First, in your app's Resources File (.resw), add a property identifier with the name "TitleText.Width" (instead of "TitleText", you can use any name you like). Then, use an **x:Uid** to associate one or more UI elements with this property identifier.

```xaml
<TextBlock x:Uid="TitleText">
```

For more info about Resources Files (.resw), property identifiers, and **x:Uid**, see [Localize strings in your UI and app package manifest](/windows/uwp/app-resources/localize-strings-ui-manifest).

## Fonts
Use the [**LanguageFont**](/uwp/api/Windows.Globalization.Fonts.LanguageFont?branch=live) font-mapping class for programmatic access to the recommended font family, size, weight, and style for a particular language. The **LanguageFont** class provides access to the correct font info for various categories of content including UI headers, notifications, body text, and user-editable document body fonts.

## Mirroring images
If your app has images that must be mirrored (that is, the same image can be flipped) for RTL, then you can use **FlowDirection**.

```xaml
<!-- en-US\localized.xaml -->
<Image ... FlowDirection="LeftToRight" />

<!-- ar-SA\localized.xaml -->
<Image ... FlowDirection="RightToLeft" />
```

If your app requires a different image to flip the image correctly, then you can use the resource management system with the `LayoutDirection` qualifier (see the LayoutDirection section of [Tailor your resources for language, scale, and other qualifiers](/windows/uwp/app-resources/tailor-resources-lang-scale-contrast#layoutdirection)). The system chooses an image named `file.layoutdir-rtl.png` when the app runtime language (see [Understand user profile languages and app manifest languages](manage-language-and-region.md)) is set to an RTL language. This approach may be necessary when some part of the image is flipped, but another part isn't.

## Handling right-to-left (RTL) languages
When your app is localized for right-to-left (RTL) languages, use the [**FrameworkElement.FlowDirection**](/uwp/api/Windows.UI.Xaml.FrameworkElement.FlowDirection) property, and set symmetrical padding and margins. Layout panels such as [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid?branch=live) scale and flip automatically with the value of **FlowDirection** that you set.

Set **FlowDirection** on the root layout panel (or frame) of your Page, or on the Page itself. This causes all of the controls contained within to inherit that property.

> [!IMPORTANT]
> However, **FlowDirection** is *not* set automatically based on the user's selected display language in Windows settings; nor does it change dynamically in response to the user switching display language. If the user switches Windows settings from English to Arabic, for example, then the **FlowDirection** property will *not* automatically change from left-to-right to right-to-left. As the app developer, you have to set **FlowDirection** appropriately for the language that you are currently displaying.

The programmatic technique is to use the `LayoutDirection` property of the preferred user display language to set the [**FlowDirection**](/uwp/api/Windows.UI.Xaml.FrameworkElement.FlowDirection) property (see the code example below). Most controls included in Windows use **FlowDirection** already. If you're implementing a custom control, it should use **FlowDirection** to make appropriate layout changes for RTL and LTR languages.

```csharp    
this.languageTag = Windows.Globalization.ApplicationLanguages.Languages[0];

// For bidirectional languages, determine flow direction for the root layout panel, and all contained UI.

var flowDirectionSetting = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues["LayoutDirection"];
if (flowDirectionSetting == "LTR")
{
    this.layoutRoot.FlowDirection = Windows.UI.Xaml.FlowDirection.LeftToRight;
}
else
{
    this.layoutRoot.FlowDirection = Windows.UI.Xaml.FlowDirection.RightToLeft;
}
```

```cppwinrt
#include <winrt/Windows.ApplicationModel.Resources.Core.h>
#include <winrt/Windows.Globalization.h>
...

m_languageTag = Windows::Globalization::ApplicationLanguages::Languages().GetAt(0);

// For bidirectional languages, determine flow direction for the root layout panel, and all contained UI.

auto flowDirectionSetting = Windows::ApplicationModel::Resources::Core::ResourceContext::GetForCurrentView().QualifierValues().Lookup(L"LayoutDirection");
if (flowDirectionSetting == L"LTR")
{
    layoutRoot().FlowDirection(Windows::UI::Xaml::FlowDirection::LeftToRight);
}
else
{
    layoutRoot().FlowDirection(Windows::UI::Xaml::FlowDirection::RightToLeft);
}
```

```cpp
this->languageTag = Windows::Globalization::ApplicationLanguages::Languages->GetAt(0);

// For bidirectional languages, determine flow direction for the root layout panel, and all contained UI.

auto flowDirectionSetting = Windows::ApplicationModel::Resources::Core::ResourceContext::GetForCurrentView()->QualifierValues->Lookup("LayoutDirection");
if (flowDirectionSetting == "LTR")
{
    this->layoutRoot->FlowDirection = Windows::UI::Xaml::FlowDirection::LeftToRight;
}
else
{
    this->layoutRoot->FlowDirection = Windows::UI::Xaml::FlowDirection::RightToLeft;
}
```

The technique above makes **FlowDirection** a function of the `LayoutDirection` property of the preferred user display language. If for whatever reason you don't want that logic, then you can expose a FlowDirection property in your app as a resource that localizers can set appropriately for each language they translate into.

First, in your app's Resources File (.resw), add a property identifier with the name "MainPage.FlowDirection" (instead of "MainPage", you can use any name you like). Then, use an **x:Uid** to associate your main **Page** element (or its root layout panel or frame) with this property identifier.

```xaml
<Page x:Uid="MainPage">
```

Instead of a single line of code for all languages, this depends on the translator "translating" this property resource correctly for each translated language; so be aware that there's that extra opportunity for human error when you use this technique.

## Important APIs
* [FrameworkElement.FlowDirection](/uwp/api/Windows.UI.Xaml.FrameworkElement.FlowDirection)
* [LanguageFont](/uwp/api/Windows.Globalization.Fonts.LanguageFont?branch=live)

## Related topics
* [Localize strings in your UI and app package manifest](/windows/uwp/app-resources/localize-strings-ui-manifest)
* [Tailor your resources for language, scale, and other qualifiers](/windows/uwp/app-resources/tailor-resources-lang-scale-contrast)
* [Understand user profile languages and app manifest languages](manage-language-and-region.md)
