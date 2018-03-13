---
author: Xansky
Description: Provides a checklist to help you ensure that your Universal Windows Platform (UWP) app is accessible.
ms.assetid: BB8399E2-7013-4F77-AF2C-C1A0E5412856
title: Accessibility checklist
label: Accessibility checklist
template: detail.hbs
ms.author: mhopkins
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Accessibility checklist



Provides a checklist to help you ensure that your Universal Windows Platform (UWP) app is accessible .

Here we provide a checklist you can use to ensure that your app is accessible.

1.  Set the accessible name (required) and description (optional) for content and interactive UI elements in your app.

    An accessible name is a short, descriptive text string that a screen reader uses to announce a UI element. Some UI elements such as [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/BR209652) and [**TextBox**](https://msdn.microsoft.com/library/windows/apps/BR209683) promote their text content as the default accessible name; see [Basic accessibility information](basic-accessibility-information.md#name_from_inner_text).

    You should set the accessible name explicitly for images or other controls that do not promote inner text content as an implicit accessible name. You should use labels for form elements so that the label text can be used as a [**LabeledBy**](https://msdn.microsoft.com/library/windows/apps/Hh759769) target in the Microsoft UI Automation model for correlating labels and inputs. If you want to provide more UI guidance for users than is typically included in the accessible name, accessible descriptions and tooltips help users understand the UI.

    For more info, see [Accessible name](basic-accessibility-information.md#accessible_name) and [Accessible description](basic-accessibility-information.md).

2.  Implement keyboard accessibility:

    * Test the default tab index order for a UI. Adjust the tab index order if necessary, which may require enabling or disabling certain controls, or changing the default values of [**TabIndex**](https://msdn.microsoft.com/library/windows/apps/BR209461) on some of the UI elements.
    * Use controls that support arrow-key navigation for composite elements. For default controls, the arrow-key navigation is typically already implemented.
    * Use controls that support keyboard activation. For default controls, particularly those that support the UI Automation [**Invoke**](https://msdn.microsoft.com/library/windows/apps/BR242582) pattern, keyboard activation is typically available; check the documentation for that control.
    * Set access keys or implement accelerator keys for specific parts of the UI that support interaction.
    * For any custom controls that you use in your UI, verify that you have implemented these controls with correct [**AutomationPeer**](https://msdn.microsoft.com/library/windows/apps/BR209185) support for activation, and defined overrides for key handling as needed to support activation, traversal and access or accelerator keys.

    For more info, see [Keyboard interactions](https://msdn.microsoft.com/library/windows/apps/Mt185607).

3.  Visually verify your UI to ensure that the text contrast is adequate, elements render correctly in the high-contrast themes, and colors are used correctly.

    * Use the system display options that adjust the display's dots per inch (dpi) value, and ensure that your app UI scales correctly when the dpi value changes. (Some users change dpi values as an accessibility option, it's available from **Ease of Access**.)
    * Use a color analyzer tool to verify that the visual text contrast ratio is at least 4.5:1.
    * Switch to a high contrast theme and verify that the UI for your app is readable and usable.
    * Ensure that your UI doesn’t use color as the only way to convey information.

    For more info, see [High-contrast themes](high-contrast-themes.md) and [Accessible text requirements](accessible-text-requirements.md).

4.  Run accessibility tools, address reported issues, and verify the screen reading experience.

    Use tools such as [**Inspect**](https://msdn.microsoft.com/library/windows/desktop/Dd318521) to verify programmatic access, run diagnostic tools such as [**AccChecker**](https://msdn.microsoft.com/library/windows/desktop/Hh920985) to discover common errors, and verify the screen reading experience with Narrator.

    For more info, see [Accessibility testing](accessibility-testing.md).

5.  Make sure your app manifest settings follow accessibility guidelines.

6.  Declare your app as accessible in the Microsoft Store.

    If you implemented the baseline accessibility support, declaring your app as accessible in the Microsoft Store can help reach more customers and get some additional good ratings.

    For more info, see [Accessibility in the Store](accessibility-in-the-store.md).

<span id="related_topics"/>

## Related topics  
* [Accessibility](accessibility.md)
* [Design for accessibility](https://msdn.microsoft.com/library/windows/apps/Hh700407)
* [Practices to avoid](practices-to-avoid.md) 
