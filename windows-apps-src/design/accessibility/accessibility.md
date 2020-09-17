---
Description: Introduces accessibility concepts that relate to Windows apps.
ms.assetid: C89D79C2-B830-493D-B020-F3FF8EB5FFDD
title: Accessibility
label: Accessibility
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Accessibility  

Accessibility is about building experiences that make your Windows application usable by people who use technology in a wide range of environments and approach your UI with a range of needs and experiences. For some situations, accessibility requirements are imposed by law. However, it's a good idea to address accessibility issues regardless of legal requirements so that your apps have the largest possible audience.

> There's also a Microsoft Store declaration regarding accessibility for your app!

| Article | Description |
|---------|-------------|
| [Accessibility overview](accessibility-overview.md) | This article is an overview of the concepts and technologies related to accessibility scenarios for Windows apps. |
| [Designing inclusive software](designing-inclusive-software.md) | Learn about evolving inclusive design with Windows apps for Windows 10.  Design and build inclusive software with accessibility in mind. |
| [Developing inclusive Windows apps](developing-inclusive-windows-apps.md) | This article is a roadmap for developing accessible Windows apps. |
| [Accessibility testing](accessibility-testing.md) | Testing procedures to follow to ensure that your Windows app is accessible. |
| [Accessibility in the Store](accessibility-in-the-store.md) | Describes the requirements for declaring your Windows app as accessible in the Microsoft Store. |
| [Accessibility checklist](accessibility-checklist.md) | Provides a checklist to help you ensure that your Windows app is accessible. |
| [Expose basic accessibility information](basic-accessibility-information.md) | Basic accessibility info is often categorized into name, role, and value. This topic describes code to help your app expose the basic information that assistive technologies need. |
| [Keyboard accessibility](keyboard-accessibility.md) | If your app does not provide good keyboard access, users who are blind or have mobility issues can have difficulty using your app or may not be able to use it at all. |
| [Screen readers and hardware system buttons](system-button-narration.md) | Screen-readers, such as [Narrator](https://support.microsoft.com/en-us/help/22798/windows-10-complete-guide-to-narrator), must be able to recognize and handle hardware system button events and communicate their state to users. In some cases, the screen reader might need to handle button events exclusively and not let them bubble up to other handlers. |
| [Landmarks and Headings](landmarks-and-headings.md) | Landmarks and headings define sections of a user interface that aid in efficient navigation for users of assistive technology such as screen readers. |
| [High-contrast themes](high-contrast-themes.md) | Describes the steps needed to ensure your Windows app is usable when a high-contrast theme is active. |
| [Accessible text requirements](accessible-text-requirements.md) | This topic describes best practices for accessibility of text in an app, by assuring that colors and backgrounds satisfy the necessary contrast ratio. This topic also discusses the Microsoft UI Automation roles that text elements in a Windows app can have, and best practices for text in graphics. |
| [Accessibility practices to avoid](practices-to-avoid.md) | Lists the practices to avoid if you want to create an accessible Windows app. |
| [Custom automation peers](custom-automation-peers.md) | Describes the concept of automation peers for UI Automation, and how you can provide automation support for your own custom UI class. |
| [Control patterns and interfaces](control-patterns-and-interfaces.md) | Lists the Microsoft UI Automation control patterns, the classes that clients use to access them, and the interfaces providers use to implement them. |

## Related topics  
* [**Windows.UI.Xaml.Automation**](/uwp/api/Windows.UI.Xaml.Automation) 
* [Get started with Narrator](https://support.microsoft.com/help/22798/windows-10-complete-guide-to-narrator)