---
author: stevewhims
Description: Develop your app to support the layouts and fonts of multiple languages, including RTL (right-to-left) flow direction.
title: Adjust layout and fonts, and support RTL
ms.assetid: F2522B07-017D-40F1-B3C8-C4D0DFD03AC3
label: Adjust layout and fonts, and support RTL
template: detail.hbs
ms.author: stwhi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Adjust layout and fonts, and support RTL
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

Develop your app to support the layouts and fonts of multiple languages, including RTL (right-to-left) flow direction.

## Layout guidelines


Some languages, such as German and Finnish, require more space than English for their text. The fonts for some languages, such as Japanese, require more height. And some languages, such as Arabic and Hebrew, require that text layout and app layout must be in right-to-left (RTL) reading order.

Use flexible layout mechanisms instead of absolute positioning, fixed widths, or fixed heights. When necessary, particular UI elements can be adjusted based on language.

Specify a **Uid** for an element:

```XML
<TextBlock x:Uid="Block1">
```

Ensure that your app's ResW file has a resource for Block1.Width, which you can set for each language that you localize into.

For Windows Store apps using C++, C\#, or Visual Basic, use the [**FlowDirection**](https://msdn.microsoft.com/library/windows/apps/br208716) property, with symmetrical padding and margins, to enable localization for other layout directions.

XAML layout controls such as [**Grid**](https://msdn.microsoft.com/library/windows/apps/br242704) scale and flip automatically with the [**FlowDirection**](https://msdn.microsoft.com/library/windows/apps/br208716) property. Expose your own **FlowDirection** property in your app as a resource for localizers.

Specify a **Uid** for the main page of your app:

```XML
<Page x:Uid="MainPage">
```

Ensure that your app's **ResW** file has a resource for MainPage.FlowDirection, which you can set for each language that you localize into.


## Mirroring images

If your app has images that must be mirrored (that is, the same image can be flipped) for RTL, you can apply the [**FlowDirection**](https://msdn.microsoft.com/library/windows/apps/br208716) property:

```XML
<!-- en-US\localized.xaml -->
<Image ... FlowDirection="LeftToRight" />

<!-- ar-SA\localized.xaml -->
<Image ... FlowDirection="RightToLeft" />
```


If your app requires a different image to flip the image correctly, you can use the resource management system with the LayoutDirection qualifier (see the LayoutDirection section of [Tailor your resources for language, scale, and other qualifiers](../app-resources/tailor-resources-lang-scale-contrast.md)). The system chooses an image named file.layoutdir-rtl.png when the [application language](manage-language-and-region.md) is set to an RTL language. This approach may be necessary when some part of the image is flipped, but another part isn't.

## Fonts

Use the [**LanguageFont**](https://msdn.microsoft.com/library/windows/apps/br206864) font-mapping APIs for programmatic access to the recommended font family, size, weight, and style for a particular language. The **LanguageFont** object provides access to the correct font info for various categories of content including UI headers, notifications, body text, and user-editable document body fonts.

## Best practices for handling Right to Left (RTL) languages

When your app is localized for Right to Left (RTL) languages, use APIs to set the default text direction for the RootFrame. This will cause all of the controls contained within the RootFrame to respond appropriately to the default text direction.  When more than one language is supported, use the LayoutDirection for the top preferred language to set the FlowDirection property. Most controls included in Windows use FlowDirection already. If you are implementing custom controls, they should use FlowDirection to make appropriate layout changes for RTL and LTR languages.

C#
```csharp    
// For bidirectional languages, determine flow direction for RootFrame and all derived UI.

    string resourceFlowDirection = ResourceContext.GetForCurrentView().QualifierValues["LayoutDirection"];
    if (resourceFlowDirection == "LTR")
    {
       RootFrame.FlowDirection = FlowDirection.LeftToRight;
    }
    else
    {
       RootFrame.FlowDirection = FlowDirection.RightToLeft;
    }
```

C++:
```
	// Get preferred app language
	m_language = Windows::Globalization::ApplicationLanguages::Languages->GetAt(0);
	 
	// Set flow direction accordingly
	m_flowDirection = ResourceManager::Current->DefaultContext->QualifierValues->Lookup("LayoutDirection") != "LTR" ? 
       FlowDirection::RightToLeft : FlowDirection::LeftToRight;
```


### RTL FAQ 

<dl>
  <dt> <p><b>Q:</b> Is <b>FlowDirection</b> set automatically based on the current language selection? For example, if I select English will it display left to right, and if I select Arabic, will it display right to left?</p></dt>

  <dd><p><b>A:</b> <b>FlowDirection</b> does not take into account the language. You set <b>FlowDirection</b> appropriately for the language you are currently displaying. See the sample code above.</p></dd> 

  <dt> <p><b>Q:</b> I’m not too familiar with localization. Do the resources already contain flow direction? Is it possible to determine the flow direction from the current language?</p></dt>

  <dd> <p><b>A:</b> If you are using current best practices, resources do not contain flow direction directly. You must determine flow direction for the current language. Here are two ways to do this: </p>
   <p>The preferred way is to use the LayoutDirection for the top preferred language to set the FlowDirection property of the RootFrame. All the controls in the RootFrame inherit FlowDirection from the RootFrame.</p>
   <p>Another way is to set the FlowDirection in the resw file for the RTL languages you are localizing for. For example, you might have an Arabic resw file and a Hebrew resw file. In these files you could use x:UID to set the FlowDirection. This method is more prone to errors than the programmatic method, though.</p></dd>
</dl>


## Related topics
* [FlowDirection](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.frameworkelement.flowdirection.aspx)
* [Tailor your resources for language, scale, and other qualifiers](../app-resources/tailor-resources-lang-scale-contrast.md)