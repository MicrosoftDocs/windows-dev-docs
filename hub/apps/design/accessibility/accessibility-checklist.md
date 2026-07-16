---
description: Provides a checklist to help you ensure that your Windows app is accessible.
ms.assetid: BB8399E2-7013-4F77-AF2C-C1A0E5412856
title: Accessibility checklist
label: Accessibility checklist
template: detail.hbs
ms.date: 07/22/2026
ms.topic: checklist
keywords: windows 11, winui, winappsdk, windows app sdk
ms.localizationpriority: medium
---

# Accessibility checklist

This article provides recommendations for making your Windows app accessible and discoverable to customers looking for accessible apps in the Microsoft Store. Use the following recommendations during design, implementation, and testing. Together, they cover core accessibility expectations for keyboard use, readable content, assistive technology compatibility, and Microsoft Store discoverability.

## 1. Set accessible names and descriptions

Set the accessible name (required) and description (optional) for content and interactive UI elements in your app.

An accessible name is a short, descriptive text string that a screen reader uses to announce a UI element. Some UI elements such as [TextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock) and [TextBox](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox) promote their text content as the default accessible name; see [Name from inner text](basic-accessibility-information.md#name-from-inner-text).

You should set the accessible name explicitly for images or other controls that do not promote inner text content as an implicit accessible name. You should use labels for form elements so that the label text can be used as a [LabeledBy](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.labeledby) target in the Microsoft UI Automation model for correlating labels and inputs. If you want to provide more UI guidance for users than is typically included in the accessible name, accessible descriptions and tooltips help users understand the UI.

For more info, see [Accessible name](basic-accessibility-information.md#accessible-name) and [Accessible description](basic-accessibility-information.md).

## 2. Implement keyboard accessibility

- Test the default tab index order for a UI. Adjust the tab index order if necessary, which may require enabling or disabling certain controls, or changing the default values of [TabIndex](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex) on some of the UI elements.
- Use controls that support arrow-key navigation for composite elements. For default controls, the arrow-key navigation is typically already implemented.
- Use controls that support keyboard activation. For default controls, particularly those that support the UI Automation [Invoke](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.provider.iinvokeprovider.invoke) pattern, keyboard activation is typically available; check the documentation for that control.
- Set access keys or implement accelerator keys for specific parts of the UI that support interaction.
- For any custom controls that you use in your UI, verify that you have implemented these controls with correct [AutomationPeer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.peers.automationpeer) support for activation, and defined overrides for key handling as needed to support activation, traversal and access or accelerator keys.

For more info, see [Keyboard interactions](../../develop/input/keyboard-interactions.md).

## 3. Ensure text is a readable size

Windows includes various accessibility tools and settings that users can take advantage of and adjust to their own needs and preferences for reading text. These include:

* The Magnifier tool, which enlarges a selected area of the UI. You should ensure the layout of text in your app doesn\'t make it difficult to use Magnifier for reading.
* Global scale and resolution settings in **Settings->System->Display->Scale and layout**. Exactly which sizing options are available can vary as this depends on the capabilities of the display device.
* Text size settings in **Settings->Ease of access->Display**. Adjust the **Make text bigger** setting to specify only the size of text in supporting controls across all applications and screens (all WinUI text controls support the text scaling experience without any customization or templating).

## 4. Verify color and contrast

Visually verify your UI to ensure that the text contrast is adequate, elements render correctly in the high-contrast themes, and colors are used correctly.

* Use a color analyzer tool to verify that the visual text contrast ratio is at least 4.5:1.
* Switch to a high contrast theme and verify that the UI for your app is readable and usable.
* Ensure that your UI doesn\'t use color as the only way to convey information.

For more info, see [High-contrast themes](high-contrast-themes.md) and [Accessible text requirements](accessible-text-requirements.md).

## 5. Run accessibility tools and verify screen reading

Use tools such as [Inspect](/windows/desktop/WinAuto/inspect-objects) to verify programmatic access, run diagnostic tools such as [AccChecker](/windows/desktop/WinAuto/ui-accessibility-checker) to discover common errors, and verify the screen reading experience with Narrator.

For more info, see [Accessibility testing](accessibility-testing.md).


## 6. Add automated accessibility regression checks to your CI pipeline.

Run automated checks for critical screens and flows in pull requests and continuous integration. Configure your pipeline to block merges or releases when critical accessibility regressions are detected.

## 7. Verify app manifest settings
Make sure your app manifest settings follow accessibility guidelines, see [Security Considerations for Assistive Technologies](/windows/win32/winauto/uiauto-securityoverview).

## 8. Declare your app as accessible in the Microsoft Store.

If you implemented the baseline accessibility support, declaring your app as accessible in the Microsoft Store can help reach more customers and potentially improve ratings. See [Accessibility in the Store](accessibility-in-the-store.md)

## Related topics

- [Accessible text requirements](accessible-text-requirements.md)
- [Text scaling](../../develop/input/text-scaling.md)
- [Design for accessibility](./accessibility-overview.md)
- [Practices to avoid](practices-to-avoid.md)
