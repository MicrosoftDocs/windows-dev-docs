---
description: Introduces accessibility concepts that relate to Windows apps.
ms.assetid: C89D79C2-B830-493D-B020-F3FF8EB5FFDD
title: Accessibility
label: Accessibility
template: detail.hbs
ms.date: 03/03/2026
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Accessibility

Design and build Windows applications that provide full and successful experiences for as many people as possible.

Accessible Windows applications support not only people with disabilities (both temporary and permanent) but also those with personal preferences, specific work styles, or situational and environmental constraints (such as shared work spaces, low bandwidth, bright sunlight, noisy or quiet surroundings, while cooking, and so on).

**Everyone should have access to the same rooms in a building, whether they need to use the stairs or the elevator.**

This article provides information on accessibility for developers building Windows applications, assistive technology developers building tools such as screen readers and magnifiers, and software test engineers creating automated scripts for testing applications.

| Article | Description |
| ------- | ----------- |
| [Accessibility overview](accessibility-overview.md) | An overview of the concepts and technologies related to accessibility scenarios for Windows apps. |
| [Designing inclusive software](designing-inclusive-software.md) | Learn about evolving inclusive design with Windows apps for Windows.  Design and build inclusive software with accessibility in mind. |
| [Developing inclusive Windows apps](developing-inclusive-windows-apps.md) | A roadmap for developing accessible Windows apps. |
| [Accessibility testing](accessibility-testing.md) | Testing procedures to follow to ensure that your Windows app is accessible. |
| [Accessibility in the Store](accessibility-in-the-store.md) | Describes the requirements for declaring your Windows app as accessible in the Microsoft Store. |
| [Accessibility checklist](accessibility-checklist.md) | Provides a checklist to help you ensure that your Windows app is accessible. |
| [Expose basic accessibility information](basic-accessibility-information.md) | Basic accessibility info is often categorized into name, role, and value. This topic describes code to help your app expose the basic information that assistive technologies need. |
| [Keyboard accessibility](keyboard-accessibility.md) | If your app doesn't provide good keyboard access, users who are blind or have mobility problems can have difficulty using your app or might not be able to use it at all. |
| [Screen readers and hardware system buttons](system-button-narration.md) | Screen readers, such as [Narrator](https://support.microsoft.com/en-us/help/22798/windows-10-complete-guide-to-narrator), must be able to recognize and handle hardware system button events and communicate their state to users. In some cases, the screen reader might need to handle button events exclusively and not let them bubble up to other handlers. |
| [Landmarks and Headings](landmarks-and-headings.md) | Landmarks and headings define sections of a user interface that aid in efficient navigation for users of assistive technology such as screen readers. |
| [High-contrast themes](high-contrast-themes.md) | Describes the steps needed to ensure your Windows app is usable when a high-contrast theme is active. |
| [Accessible text requirements](accessible-text-requirements.md) | This topic describes best practices for accessibility of text in an app, by assuring that colors and backgrounds satisfy the necessary contrast ratio. This topic also discusses the Microsoft UI Automation roles that text elements in a Windows app can have, and best practices for text in graphics. |
| [Accessibility practices to avoid](practices-to-avoid.md) | Lists the practices to avoid if you want to create an accessible Windows app. |
| [Custom automation peers](custom-automation-peers.md) | Describes the concept of automation peers for UI Automation, and how you can provide automation support for your own custom UI class. |
| [Control patterns and interfaces](control-patterns-and-interfaces.md) | Lists the Microsoft UI Automation control patterns, the classes that clients use to access them, and the interfaces providers use to implement them. |

## Related topics

- [Accessibility in the Store](accessibility-in-the-store.md)
- [Microsoft.UI.Xaml.Automation](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation)
