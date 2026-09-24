---
title: Install your desktop app from Phone Link notifications
description: Learn how Phone Link notifications can launch or promote installation of your Microsoft Store desktop app, and request feature access.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 09/24/2026
ms.localizationpriority: medium
---

# Install your desktop app from Phone Link notifications

If you publish both a mobile app and a Windows desktop app, you can connect your mobile notifications to the desktop experience. When a notification from your Android or iOS app appears in Phone Link, selecting the notification can open your Windows app instead of keeping the user in a phone-only experience.

If your desktop app isn't installed, Phone Link can offer to install it from the Microsoft Store. You can also attribute the Store acquisition to your Phone Link promotion by using a Partner Center campaign ID.

## How the experience works

Phone Link and Link to Windows forward notifications from a connected Android or iOS device to the user's Windows PC. The notifications appear in the Phone Link notification surface.

For an app approved for this integration, the experience works as follows:

1. Your mobile app posts a notification on the connected phone.
1. The notification appears in Phone Link on the PC.
1. The user selects the notification body.
1. Phone Link opens your Windows desktop app. If the app isn't installed, Phone Link offers to install it from the Microsoft Store.

> [!NOTE]
> Only selections of the notification body route to the desktop app. Phone Link handles inline notification actions, such as replying to a message, without opening the app.

## Request access

Desktop app integration with Phone Link notifications is a Limited Access Feature (LAF). Microsoft must approve and enable your app before it can participate.

To request access, email [crossdeviceexperiences@microsoft.com](mailto:crossdeviceexperiences@microsoft.com) and provide the following information:

- **Mobile app identifiers:** Provide the Android package name, such as `com.contoso.messaging`, and the iOS bundle identifier, such as `com.contoso.Messaging`, for each app whose notifications should open the desktop app.
- **Windows app identifiers:** Provide the package family name (PFN) and Microsoft Store product ID for your Windows desktop app. Phone Link uses the PFN to detect and open the installed app. It uses the Store product ID to create the install link.
- **Store campaign ID (optional):** Provide the campaign ID that you want Phone Link to append to the Microsoft Store link as the `cid` parameter. Partner Center uses this value to attribute acquisitions that begin from a Phone Link notification.
- **User experience description:** Explain what the user is doing when the notification arrives and what they can do in the desktop app after the handoff.
- **Screenshot or short recording:** Show the mobile notification in Phone Link and the destination that opens in your Windows desktop app.

Microsoft evaluates the request based on the information you provide. If the request is approved, you receive instructions to unlock the feature, and Microsoft adds your app to the Phone Link app catalog.

## Prerequisites

Before you request access, make sure your apps meet these requirements:

- **Publish a publicly available Windows desktop app in the Microsoft Store.** Users who don't have the app must be able to install it from the Phone Link promotion.
- **Package the desktop app with MSIX and use a stable PFN.** Phone Link uses the PFN to detect and open the app. This integration doesn't support unpackaged desktop apps. If the PFN changes, for example because the publisher identity changes, contact Microsoft to update the catalog entry.
- **Publish an Android or iOS app.** The mobile app must use the standard platform notification APIs so that Link to Windows can forward its notifications.
- **Share account state between the mobile and desktop apps.** After the handoff, the user should arrive in a signed-in, equivalent desktop experience. An app that requires a new sign-in or pairing step on every launch isn't a good fit.
- **Meet Microsoft Store policy and Windows quality requirements.**

## Privacy and data handling

Phone Link notification forwarding is controlled by the user's connected-device and notification settings. This integration changes the destination of an approved notification-body selection; it doesn't provide a client-side API that gives your desktop app access to the forwarded notification.

Design notification content with the connected PC in mind. Don't include sensitive information that the user wouldn't expect to appear on the PC, and describe your app's cross-device data practices in your privacy disclosures.

Microsoft processes Phone Link data in accordance with the [Microsoft Services Agreement](https://www.microsoft.com/servicesagreement) and the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement). Your apps remain responsible for their notification content, account data, and data-handling practices.

## Availability and limitations

- The experience requires a supported version of Phone Link on Windows and a connected Android or iOS device.
- On Android devices that support app remoting, Phone Link continues to use the existing app-remoting experience when that is the primary path for the app.
- Microsoft enables the feature by market. Availability can be staged and can differ by region.
- Microsoft can enable, stage, or disable the experience for an app through server-side configuration.
- Only approved apps in the Phone Link app catalog can participate. An app can't register itself through a client-side API.

