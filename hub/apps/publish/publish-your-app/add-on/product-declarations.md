---
description: Product declarations help make sure your app add-on is displayed appropriately in the Microsoft Store and offered to the right set of customers.
title: Product declarations for add-on
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Product declarations for add-on

The **Product declarations** section of the [Properties](./enter-app-properties.md) page of the [submission process](./create-app-submission.md) helps make sure your app is displayed appropriately and offered to the right set of customers, and helps them understand how they can use your app.

The following sections describe some of the declarations and what you need to consider when determining whether each declaration applies to your app. Note that two of these declarations are checked by default (as described below.) Depending on your product's category, you may also see additional declarations. Be sure to review all of the declarations and ensure they accurately reflect your submission.

## This app allows users to make purchases, but does not use the Microsoft Store commerce system

1. If your app is using Microsoft Commerce, do not tick this box.
2. If your app does not use Microsoft Commerce, tick this box.
3. Per the [App Developer Agreement](https://go.microsoft.com/fwlink/?linkid=528905), apps that were created and submitted prior to June 29, 2015, could continue to offer in-app purchasing functionality without using Microsoft's commerce engine, so long as the purchase functionality complies with the [Microsoft Store Policies](../../store-policies.md#108-financial-transactions). If this applies to your app, you must check this box.

## This app has been tested to meet accessibility guidelines

Checking this box makes your app discoverable to customers who are specifically looking for accessible apps in the Store.

You should only check this box if you have done all of the following items:

- Set all the relevant accessibility info for UI elements, such as accessible names.
- Implemented keyboard navigation and operations, taking into account tab order, keyboard activation, arrow keys navigation, shortcuts.
- Ensured an accessible visual experience by including such things as a 4.5:1 text contrast ratio, and don't rely on color alone to convey info to the user.
- Used accessibility testing tools, such as Inspect or AccChecker, to verify your app, and resolve all high-priority errors detected by those tools.
- Verified the app’s key scenarios from end to end using such facilities and tools as Narrator, Magnifier, On Screen Keyboard, High Contrast, and High DPI.

When you declare your app as accessible, you agree that your app is accessible to all customers, including those with disabilities. For example, this means you have tested the app with high-contrast mode and with a screen reader. You've also verified that the user interface functions correctly with a keyboard, the Magnifier, and other accessibility tools.

For more info, see [Accessibility](../../../../apps/design/accessibility/accessibility.md), [Accessibility testing](../../../../apps/design/accessibility/accessibility-testing.md), and [Accessibility in the Store](../../../../apps/design/accessibility/accessibility-in-the-store.md).

> [!IMPORTANT]
> Don't list your app as accessible unless you have specifically engineered and tested it for that purpose. If your app is declared as accessible, but it doesn’t actually support accessibility, you'll probably receive negative feedback from the community.

## Customers can install this app to alternate drives or removable storage

This box is checked by default, to allow customers to install your app to external or removable storage media such as an SD card, or to a non-system volume drive such as an external drive.

If you want to prevent your app from being installed to alternate drives or removable storage, and only allow installation to the internal hard drive on their device, uncheck this box. (Note that there is no option to restrict installation so that an app can _only_ be installed to removable storage media.)

## Windows can include this app's data in automatic backups to OneDrive

This box is checked by default, to allow your app's data to be included when a customer chooses to have Windows make automated backups to OneDrive.

If you want to prevent your app's data from being included in automated backups, uncheck this box.

## This app sends Kinect data to external services

If your app uses Kinect data and sends it to any external service, you must check this box.
