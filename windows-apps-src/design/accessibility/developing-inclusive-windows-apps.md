---
Description: Learn to develop accessible Windows apps that include keyboard navigation, color and contrast settings, and support for assistive technologies.
ms.assetid: 9311D23A-B340-42F0-BEFE-9261442AF108
title: Developing inclusive Windows 10 apps
label: Developing inclusive Windows 10 apps
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Developing inclusive Windows apps  

This article discusses how to develop accessible Windows apps. Specifically, it assumes that you understand how to design the logical hierarchy for your app. Learn to develop accessible Windows apps that include keyboard navigation, color and contrast settings, and support for assistive technologies.

If you have not yet done so, please start by reading [Designing inclusive software](designing-inclusive-software.md).

There are three things you should do to make sure that your app is accessible:

1. Expose your UI elements to [programmatic access](#programmatic-access).
2. Ensure that your app supports [keyboard navigation](#keyboard-navigation) for people who are unable to use a mouse or touchscreen.
3. Make sure that your app supports accessible [color and contrast](#color-and-contrast) settings.

## Programmatic access  
Programmatic access is critical for creating accessibility in apps. This is achieved by setting the accessible name (required) and description (optional) for content and interactive UI elements in your app. This ensures that UI controls are exposed to assistive technology (AT) such as screen readers (for example, Narrator) or alternative output devices (such as Braille displays). Without programmatic access, the APIs for assistive technology cannot interpret information correctly, leaving the user unable to use the products sufficiently, or forcing the AT to use undocumented programming interfaces or techniques never intended to be used as an accessibility interface. When UI controls are exposed to assistive technology, the AT is able to determine what actions and options are available to the user.  

For more information about making your app UI elements available to assistive technologies (AT), see [Expose basic accessibility information](basic-accessibility-information.md).

## Keyboard navigation  
For users who are blind or have mobility issues, being able to navigate the UI with a keyboard is extremely important. However, only those UI controls that require user interaction to function should be given keyboard focus. Components that don’t require an action, such as static images, do not need keyboard focus.  

It is important to remember that unlike navigating with a mouse or touch, keyboard navigation is linear. When considering keyboard navigation, think about how your user will interact with your product and what the logical navigation will be. In Western cultures, people read from left to right, top to bottom. It is, therefore, common practice to follow this pattern for keyboard navigation.  

When designing keyboard navigation, examine your UI, and think about these questions:
* How are the controls laid out or grouped in the UI?
* Are there a few significant groups of controls?
    * If yes, do those groups contain another level of groups?
* 	Among peer controls, should navigation be done by tabbing around, or via special navigation (such as arrow keys), or both?

The goal is to help the user understand how the UI is laid out and identify the controls that are actionable. If you are finding that there are too many tab stops before the user completes the navigation loop, consider grouping related controls together. Some controls that are related, such as a hybrid control, may need to be addressed at this early exploration stage. After you begin to develop your product, it is difficult to rework the keyboard navigation, so plan carefully and plan early!  

To learn more about keyboard navigation among UI elements, see [Keyboard accessibility](keyboard-accessibility.md).  

Also, the [Engineering Software for Accessibility](https://www.microsoft.com/download/details.aspx?id=19262) eBook has an excellent chapter on this subject titled _Designing the Logical Hierarchy_.

## Color and contrast  
One of the built-in accessibility features in Windows is the High Contrast mode, which heightens the color contrast of text and images on the computer screen. For some people, increasing the contrast in colors reduces eyestrain and makes it easier to read. When you verify your UI in high contrast, you want to check that controls have been coded consistently and with system colors (not with hard-coded colors) to ensure that they will be able to see all the controls on the screen that a user not using high contrast would see.  

XAML
```xaml
<Button Background="{ThemeResource ButtonBackgroundThemeBrush}">OK</Button>
```
For more information about using system colors and resources, see [XAML theme resources](../controls-and-patterns/xaml-theme-resources.md).

As long as you haven’t overridden system colors, a UWP app supports high-contrast themes by default. If a user has chosen that they want the system to use a high-contrast theme from system settings or accessibility tools, the framework automatically uses colors and style settings that produce a high-contrast layout and rendering for controls and components in the UI.   

For more information, see [High-contrast themes](high-contrast-themes.md).  

If you have decided to use you own color theme instead of system colors, consider these guidelines:  

**Color contrast ratio** – The updated Section 508 of the Americans with Disability Act, as well as other legislation, requires that the default color contrasts between text and its background must be 5:1. For large text (18-point font sizes, or 14 points and bolded), the required default contrast is 3:1.  

**Color  combinations** – About 7 percent of males (and less than 1 percent of females) have some form of color deficiency. Users with colorblindness have problems distinguishing between certain colors, so it is important that color alone is never used to convey status or meaning in an application. As for decorative images (such as icons or backgrounds), color combinations should be chosen in a manner that maximizes the perception of the image by colorblind users.  

## Accessibility checklist  
Following is an abbreviated version of the accessibility checklist:

1. Set the accessible name (required) and description (optional) for content and interactive UI elements in your app.
2. Implement keyboard accessibility.
3. Visually verify your UI to ensure that the text contrast is adequate, elements render correctly in the high-contrast themes, and colors are used correctly.
4. Run accessibility tools, address reported issues, and verify the screen reading experience. (See Accessibility testing topic.)
5. Make sure your app manifest settings follow accessibility guidelines.
6. Declare your app as accessible in the Microsoft Store. (See the [Accessibility in the store](accessibility-in-the-store.md) topic.)

For more detail, please see the full [Accessibility checklist](accessibility-checklist.md) topic.

## Related topics  
* [Designing inclusive software](designing-inclusive-software.md)  
* [Inclusive design](https://www.microsoft.com/design/inclusive/)
* [Accessibility practices to avoid](practices-to-avoid.md)
* [Engineering Software for Accessibility](https://www.microsoft.com/download/details.aspx?id=19262)
* [Microsoft accessibility developer hub](https://developer.microsoft.com/windows/accessible-apps)
* [Accessibility](accessibility.md)
