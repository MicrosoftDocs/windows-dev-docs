---
author: jnHs
Description: You can provide additional information about your app in the App declarations section of the App properties page during the submission process.
title: App declarations
ms.assetid: 3AF618F3-2B47-4A57-B7E8-1DF979D4A82C
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# App declarations

You can provide additional information about your app in the **App declarations** section of the **App properties** page during the [submission process](app-submissions.md). These declarations can help make sure your app is displayed appropriately and offered to the right set of customers, or can indicate how customers can use your app.

The following sections describe each declaration and what you need to consider when determining whether each declaration applies to your app.

## This app allows users to make purchases, but does not use the Windows Store commerce system.

Most apps should leave this box unchecked, since apps which offer opportunities to make in-app purchases generally use the Microsoft in-app purchase API to create and [submit the add-ons](add-on-submissions.md). Per the [App Developer Agreement](https://msdn.microsoft.com/library/windows/apps/hh694058), apps that were created and submitted prior to June 29, 2015, may continue to offer in-app purchasing functionality without using Microsoft's commerce engine, so long as the purchase functionality complies with the [Windows Store Policies](https://msdn.microsoft.com/library/windows/apps/dn764944.aspx#pol_10_8). If this applies to your app, you must check this box. Otherwise, leave it unchecked.

## This app has been tested to meet accessibility guidelines.

Checking this box makes your app discoverable to customers who are specifically looking for accessible apps in the Store.

You should only check this box if you have done all of the following items:

-   Set all the relevant accessibility info for UI elements, such as accessible names.
-   Implemented keyboard navigation and operations, taking into account tab order, keyboard activation, arrow keys navigation, shortcuts.
-   Ensured an accessible visual experience by including such things as a 4.5:1 text contrast ratio, and don't rely on color alone to convey info to the user.
-   Used accessibility testing tools, such as Inspect or AccChecker, to verify your app, and resolve all high-priority errors detected by those tools.
-   Verified the app’s key scenarios from end to end using such facilities and tools as Narrator, Magnifier, On Screen Keyboard, High Contrast, and High DPI.

When you declare your app as accessible, you agree that your app is accessible to all customers, including those with disabilities. For example, this means you have tested the app with high-contrast mode and with a screen reader. You've also verified that the user interface functions correctly with a keyboard, the Magnifier, and other accessibility tools.

For more info, see [Accessibility for Windows Runtime apps](https://msdn.microsoft.com/library/windows/apps/dn263101), [Accessibility testing](https://msdn.microsoft.com/library/windows/apps/mt297664), and [Accessibility in the Store](https://msdn.microsoft.com/library/windows/apps/mt297663).

> **Important**  Don't list your app as accessible unless you have specifically engineered and tested it for that purpose. If your app is declared as accessible, but it doesn’t actually support accessibility, you'll probably receive negative feedback from the community.

## Customers can install this app to alternate drives or removable storage.

This box is checked by default, to allow customers to install your app to removable storage media such as an SD card, or to a non-system volume drive such as an external drive.

If you want to prevent your app from being installed to alternate drives or removable storage, uncheck this box.

Note that there is no option to restrict installation so that an app can only be installed to removable storage media.

> **Note**  For Windows Phone 8.1, this was previously indicated via StoreManifest.xml.

## Windows can include this app's data in automatic backups to OneDrive.

This box is checked by default, to allow your app's data to be included when a customer chooses to have Windows make automated backups to OneDrive.

If you want to prevent your app's data from being included in automated backups, uncheck this box.

> **Note**  For Windows Phone 8.1, this was previously indicated via StoreManifest.xml.

 

 

 




