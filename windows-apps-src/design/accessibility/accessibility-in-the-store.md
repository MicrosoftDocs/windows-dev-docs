---
Description: Describes the requirements for declaring your Windows app as accessible in the Microsoft Store.
ms.assetid: 59FA3B87-75A6-4B30-BA7C-A0E769D68050
title: Accessibility in the Store
label: Accessibility in the Store
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Accessibility in the Store  



Describes the requirements for declaring your Windows app as accessible in the Microsoft Store.

While submitting your app to the Microsoft Store for certification, you can declare your app as accessible. Declaring your app as accessible makes it easier to discover for users who are interested in accessible apps, such as users who have visual impairments. Users discover accessible apps by using the **Accessible** filter while searching the Microsoft Store. Declaring your app as accessible also adds the **Accessible** tag to your app’s description.

By declaring your app as accessible, you state that it has the [basic accessibility information](basic-accessibility-information.md) that users need for primary scenarios using one or more of the following:

* The keyboard.
* A high contrast theme.
* A variable dots per inch (dpi) setting.
* Common assistive technology such as the Windows accessibility features, including Narrator, Magnifier, and On-Screen Keyboard.

You should declare your app as accessible if you built and tested it for accessibility. This means that you did the following:

* Set all the relevant accessibility information for UI elements, including name, role, value, and so on.
* Implemented full keyboard accessibility, enabling the user to:
    * Accomplish primary app scenarios by using only the keyboard.
    * Tab among UI elements in a logical order.
    * Navigate among UI elements within a control by using the arrow keys.
    * Use keyboard shortcuts to reach primary app functionality.
    * Use Narrator touch gestures for Tab and arrow equivalency for devices with no keyboard.
* Ensured that your app UI is visually accessible: has a minimum text contrast ratio of 4.5:1, does not rely on color alone to convey information, and so on.
* Used accessibility testing tools such as [**Inspect**](/windows/desktop/WinAuto/inspect-objects) and [**UIAVerify**](/windows/desktop/WinAuto/ui-automation-verify) to verify your accessibility implementation, and resolved all priority 1 errors reported by such tools.
* Verified your app’s primary scenarios from end to end by using Narrator, Magnifier, On-Screen Keyboard, a high contrast theme, and adjusted dpi settings.

See the [Accessibility checklist](accessibility-checklist.md) for a review of these procedures and links to resources that will help you accomplish them.

<span id="related_topics"/>

## Related topics    
* [Accessibility](accessibility.md)