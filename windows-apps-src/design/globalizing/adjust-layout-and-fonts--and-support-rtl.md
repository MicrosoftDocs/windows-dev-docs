---
author: stevewhims
Description: Design your app to support the layouts and fonts of multiple languages, including RTL (right-to-left) flow direction.
title: Adjust layout and fonts, and support RTL
ms.assetid: F2522B07-017D-40F1-B3C8-C4D0DFD03AC3
label: Adjust layout and fonts, and support RTL
template: detail.hbs
ms.author: stwhi
ms.date: 11/09/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, localizability, localization, rtl, ltr
localizationpriority: medium
---

# Adjust layout and fonts, and support RTL

Design your app to support the layouts and fonts of multiple languages, including RTL (right-to-left) flow direction. Flow direction is the direction in which script is written and displayed, and the UI elements on the page are scanned by the eye.

## Layout guidelines

Languages such as German and Finnish typically use more characters than English does. Far Eastern fonts typically require more height. And languages such as Arabic and Hebrew require that layout panels and text elements be laid out in right-to-left (RTL) reading order.

Because of the variable length of translated text, you should use dynamic UI layout mechanisms instead of absolute positioning, fixed widths, or fixed heights. Pseudo-localizing your app will uncover any problematic edge cases where your UI elements don't size to content properly.

For RTL languages, use the [**FrameworkElement.FlowDirection**](/uwp/api/Windows.UI.Xaml.FrameworkElement?branch=live#Windows_UI_Xaml_FrameworkElement_FlowDirection) property, and set symmetrical padding and margins. Layout panels such as [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid?branch=live) scale and flip automatically with the value of **FlowDirection** that you set.

Here's an example of how you can expose a FlowDirection property in your app as a resource that localizers can set appropriately.

First, in your app's Resources File (.resw), add a property identifier with the name "MainPage.FlowDirection" (instead of "MainPage", you can use any name you like).

Then, use an **x:Uid** to associate your main **Page** element with this property identifier.

```xaml
<Page x:Uid="MainPage">
```

For more info about Resources Files (.resw), property identifiers, and **x:Uid**, see [Localize strings in your UI and app package manifest](../../app-resources/localize-strings-ui-manifest.md).

You should avoid setting absolute layout values on any UI element based on language. But if it's absolutely unavoidable, then you can create a property identifier of the form "TitleText.Width".

```xaml
<TextBlock x:Uid="TitleText">
```

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

If your app requires a different image to flip the image correctly, then you can use the resource management system with the `LayoutDirection` qualifier (see the LayoutDirection section of [Tailor your resources for language, scale, and other qualifiers](../../app-resources/tailor-resources-lang-scale-contrast.md#layoutdirection)). The system chooses an image named `file.layoutdir-rtl.png` when the app runtime language (see [Understand user profile languages and app manifest languages](manage-language-and-region.md)) is set to an RTL language. This approach may be necessary when some part of the image is flipped, but another part isn't.

## Best practices for handling right-to-left (RTL) languages

When your app is localized for right-to-left (RTL) languages, use APIs to set the default text direction for the root layout panel of your Page. This causes all of the controls contained within the root panel to respond appropriately to the default text direction. When more than one language is supported, use `LayoutDirection` for the top app runtime language to set the [**FlowDirection**](/uwp/api/Windows.UI.Xaml.FrameworkElement?branch=live#Windows_UI_Xaml_FrameworkElement_FlowDirection) property (see code example below). Most controls included in Windows use **FlowDirection** already. If you are implementing custom controls, they should use **FlowDirection** to make appropriate layout changes for RTL and LTR languages.

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

### RTL FAQ 

**Q:** Is **FlowDirection** set automatically based on the current language selection? For example, if I select English will it display left to right, and if I select Arabic, will it display right to left?

> **A:** **FlowDirection** does not take into account the language. You set **FlowDirection** appropriately for the language you are currently displaying. See the sample code above.

**Q:** I'm not very familiar with localization. Do the resources already contain flow direction? Is it possible to determine the flow direction from the current language?

> **A:** If you are using current best practices, then resources do not contain flow direction directly. You must determine flow direction for the current language. Here are two ways to do this.
> 
> The preferred way is to use the **LayoutDirection** for the top preferred language to set the **FlowDirection** property of the RootFrame. All the controls in the RootFrame inherit FlowDirection from the RootFrame.
> 
> Another way is to set the FlowDirection in the .resw file for the RTL languages you are localizing for. For example, you might have an Arabic resw file and a Hebrew resw file. In these files you could use x:UID to set the FlowDirection. This method is more prone to errors than the programmatic method, though.

## Important APIs

* [FrameworkElement.FlowDirection](/uwp/api/Windows.UI.Xaml.FrameworkElement?branch=live#Windows_UI_Xaml_FrameworkElement_FlowDirection)
* [LanguageFont](/uwp/api/Windows.Globalization.Fonts.LanguageFont?branch=live)

## Related topics

* [Localize strings in your UI and app package manifest](../../app-resources/localize-strings-ui-manifest.md)
* [Tailor your resources for language, scale, and other qualifiers](../../app-resources/tailor-resources-lang-scale-contrast.md)
* [Understand user profile languages and app manifest languages](manage-language-and-region.md)