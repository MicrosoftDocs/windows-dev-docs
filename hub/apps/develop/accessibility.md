---
title: Develop accessible Windows apps
description: Learn how to implement accessibility in your Windows apps, including keyboard navigation, screen reader support, contrast themes, and UI Automation.
ms.topic: article
ms.date: 03/26/2026
keywords: accessibility, windows app sdk, winui, screen reader, keyboard, contrast, ui automation
---

# Develop accessible Windows apps

![Accessibility hero image](images/hero-accessibility-bar-smaller.png)

Build accessible and inclusive Windows applications with improved functionality and usability for people of all abilities. There are three core pillars to making your app accessible:

1. **Programmatic access** — Expose accessible names, roles, and values for all UI elements so that assistive technologies like screen readers can interpret your app.
2. **Keyboard navigation** — Ensure every part of your app can be operated with a keyboard for users who cannot use a mouse or touchscreen.
3. **Color and contrast** — Support high contrast themes and ensure your text meets minimum contrast ratios (4.5:1 for normal text, 3:1 for large text).

For design principles and guidelines on building inclusive apps, see [Accessibility overview](/windows/apps/design/accessibility/accessibility-overview) and [Designing inclusive software](/windows/apps/design/accessibility/designing-inclusive-software).

## Implementation guides

### Screen readers and UI Automation

| Article | Description |
|---------|-------------|
| [Expose basic accessibility information](../design/accessibility/basic-accessibility-information.md) | Set accessible names, roles, and values so assistive technologies can interpret your UI. |
| [Landmarks and headings](../design/accessibility/landmarks-and-headings.md) | Use UI Automation landmarks and headings to help users navigate content efficiently. |
| [Screen readers and hardware system buttons](../design/accessibility/system-button-narration.md) | Handle hardware button events for screen readers such as Narrator. |
| [Custom automation peers](../design/accessibility/custom-automation-peers.md) | Implement automation peers for custom controls to provide UI Automation support. |
| [Control patterns and interfaces](../design/accessibility/control-patterns-and-interfaces.md) | Reference for UI Automation control patterns and provider interfaces. |

### Keyboard navigation

| Article | Description |
|---------|-------------|
| [Keyboard accessibility](../design/accessibility/keyboard-accessibility.md) | Implement tab order, arrow-key navigation, access keys, and keyboard activation. |

### Visual accessibility

| Article | Description |
|---------|-------------|
| [Contrast themes](../design/accessibility/high-contrast-themes.md) | Ensure your app works with high contrast themes using theme resources and resource dictionaries. |
| [Accessible text requirements](../design/accessibility/accessible-text-requirements.md) | Meet contrast ratios, use correct text element roles, and support text scaling. |

### Testing and verification

| Article | Description |
|---------|-------------|
| [Accessibility testing](../design/accessibility/accessibility-testing.md) | Test with Accessibility Insights, Inspect, and Narrator to verify your app is accessible. |
| [Accessibility checklist](../design/accessibility/accessibility-checklist.md) | Step-by-step checklist to ensure your app meets accessibility requirements, including Store declaration. |

## Samples

Download and run full Windows samples that demonstrate various accessibility features and functionality.

:::row:::
   :::column:::
      [Code sample browser](/samples/browse/)

      The new samples browser replacing the MSDN Code Gallery.
   :::column-end:::
   :::column:::
      [Windows App SDK samples on GitHub](https://github.com/microsoft/WindowsAppSDK-Samples)

      These samples demonstrate the API usage patterns for Windows App SDK and WinUI.
   :::column-end:::
:::row-end:::
:::row:::
   :::column span="2":::
      [WinUI 3 Gallery](https://github.com/Microsoft/WinUI-Gallery)

      This app demonstrates the various WinUI controls supported in the Fluent Design System.
   :::column-end:::
:::row-end:::

## Videos

Various videos covering how to build accessible Windows applications to general accessibility concerns and how Microsoft addresses them.

:::row:::
   :::column:::
      **Microsoft's Accessibility API**
   :::column-end:::
   :::column:::
      **Introduction to disability and accessibility**
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      > [!VIDEO https://www.youtube.com/embed/6b0K2883rXA]
   :::column-end:::
   :::column:::
      > [!VIDEO https://www.youtube.com/embed/Kl4CT4DaypM]
   :::column-end:::
:::row-end:::

## Other resources

:::row:::
   :::column span="3":::
      **Blogs and news**

      The latest from the world of Microsoft accessibility.
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      [In the news](https://news.microsoft.com/presskits/accessibility/)
   :::column-end:::
   :::column:::
      [Accessibility blogs](https://blogs.microsoft.com/accessibility/)
   :::column-end:::
   :::column:::
      [Windows UI Automation blogs](/archive/blogs/winuiautomation/)
   :::column-end:::
:::row-end:::
:::row:::
   :::column span="3":::
      **Community and support**

      A place where Windows developers and users meet and learn together.
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      [Windows community - Accessibility](https://answers.microsoft.com/en-us/search/search?SearchTerm=windows%20accessibility)
   :::column-end:::
   :::column:::
      [Disability Answer Desk](https://www.microsoft.com/Accessibility/disability-answer-desk)
   :::column-end:::
:::row-end:::
