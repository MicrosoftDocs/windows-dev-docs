---
description: Learn to develop accessible Windows apps that include programmatic access, keyboard navigation, and color/contrast behavior.
ms.assetid: 9311D23A-B340-42F0-BEFE-9261442AF108
title: Developing inclusive Windows apps
label: Developing inclusive Windows apps
template: detail.hbs
ms.date: 03/17/2026
ms.topic: how-to
keywords: windows 11, winui, winappsdk, windows app sdk
ms.localizationpriority: medium
---

# Developing inclusive Windows apps

This article describes how to build accessible Windows apps for real-world usage with assistive technology. It assumes you already understand your app's logical hierarchy and interaction model, and focuses on implementation areas that most directly affect usability: programmatic access, keyboard navigation, and color/contrast behavior.

The guidance is intended for engineering teams that treat accessibility as a product requirement and automated accessibility verification as part of standard development and release quality gates.

If you have not yet done so, please start by reading [Designing inclusive software](designing-inclusive-software.md).

There are three core areas to validate in your accessibility implementation:

1. Expose your UI elements to [programmatic access](#programmatic-access).
2. Ensure that your app supports [keyboard navigation](#keyboard-navigation) for people who are unable to use a mouse or touchscreen.
3. Make sure that your app supports accessible [color and contrast](#color-and-contrast) settings.

In practice, these areas should be validated continuously. Add automated accessibility checks for core experiences to your CI pipeline, then use manual assistive technology testing to confirm nuanced behavior that automation alone cannot verify.

## Programmatic access

Programmatic access is the foundation for accessibility in any modern UI. In practice, this means providing an accessible name (required) and description (optional) for content and interactive elements so they are correctly represented in the UI Automation (UIA) tree. This allows assistive technology (AT), such as screen readers (for example, Narrator) and Braille displays, to reliably discover element purpose, state, and available actions.

Without accurate programmatic metadata, AT cannot interpret the UI correctly causing users to experience incomplete or misleading output and AT to attempt fallback techniques that were never intended for accessibility. When controls are exposed correctly, AT can present the same actionable model for all users.

For more information about making your app UI elements available to assistive technologies (AT), see [Expose basic accessibility information](basic-accessibility-information.md).

## Keyboard navigation

Robust keyboard navigation is essential for users who are blind, have low vision, or have mobility-related input constraints. At the same time, only controls that support meaningful interaction should participate in the tab sequence. Non-actionable content, such as static decorative images, should generally not receive focus.  

Unlike pointer interaction, keyboard interaction is linear and stateful. Define a predictable navigation order that reflects your information architecture and task flow. For example, in most left-to-right locales, a top-to-bottom, left-to-right progression is expected.

When designing keyboard behavior, examine your UI and answer these questions:

* How are the controls laid out or grouped in the UI?
* Are there a few significant groups of controls?
* If yes, do those groups contain another level of groups?
* Among peer controls, should navigation use tabbing, directional keys (such as arrow keys), or both?

Your goal is to help users quickly understand layout and reach actionable controls. If a screen requires too many tab stops to complete a common task, regroup related controls and reevaluate focus boundaries. Composite or hybrid controls often need explicit keyboard design early; redesigning keyboard behavior late in development is often expensive and error-prone.

To learn more about keyboard navigation among UI elements, see [Keyboard accessibility](keyboard-accessibility.md) and the [Engineering Software for Accessibility](https://www.microsoft.com/download/details.aspx?id=19262) eBook chapter on this subject titled *Designing the Logical Hierarchy*.

## Color and contrast

High contrast is a built-in Windows accessibility feature that increases the perceived distinction between foreground and background content. For many users, this reduces visual fatigue and improves readability. When validating your UI in high contrast, verify that controls rely on system resources rather than hard-coded colors so that all content remains visible and usable.  

```xaml
<Button Background="{ThemeResource ButtonBackgroundThemeBrush}">OK</Button>
```

For more information about using system colors and resources, see [XAML theme resources](../../develop/platform/xaml/xaml-theme-resources.md).

If you have not overridden system color behavior, Windows apps generally support high-contrast themes by default. When a user enables a high-contrast theme in system settings or accessibility tools, the framework applies high-contrast-aware resources and styles to produce an accessible rendering of controls and UI components.

For more information, see [High-contrast themes](high-contrast-themes.md).  

If you choose to use your own color theme instead of system colors, apply these guidelines:  

**Color contrast ratio** - [Section 508](https://www.section508.gov/) requirements in the United States, along with similar international regulations, require sufficient default contrast between text and background. In this guidance, target a minimum of 5:1 for standard text, and 3:1 for large text (18-point or 14-point bold).  

**Color combinations** - About 7 percent of males (and less than 1 percent of females) have some form of color vision deficiency. Because some color pairings are difficult to distinguish, never use color alone to communicate status or meaning. For decorative visuals (such as icons or backgrounds), prefer combinations that preserve legibility and shape recognition across common color vision profiles.  

## Accessibility checklist

The following is an abbreviated accessibility checklist:

1. Set the accessible name (required) and description (optional) for content and interactive UI elements in your app.
1. Implement keyboard accessibility.
1. Ensure text is a readable size.
1. Ensure that the text contrast is adequate, elements render correctly in the high-contrast themes, and colors are used correctly.
1. Run accessibility tools, address reported issues, and verify the screen reading experience. (See Accessibility testing topic.)
1. Make sure your app manifest settings follow accessibility guidelines.
1. Declare your app as accessible in the Microsoft Store. (See the [Accessibility in the store](accessibility-in-the-store.md) topic.)

For more detail, please see the full [Accessibility checklist](accessibility-checklist.md) topic.

## Related topics

* [Designing inclusive software](designing-inclusive-software.md)  
* [Inclusive design](https://www.microsoft.com/design/inclusive/)
* [Accessibility practices to avoid](practices-to-avoid.md)
* [Engineering Software for Accessibility](https://www.microsoft.com/download/details.aspx?id=19262)
* [Microsoft accessibility developer hub](https://developer.microsoft.com/windows/accessible-apps)
* [Accessibility overview](accessibility-overview.md)
