---
description: This is a hub topic covering the full developer picture of how Windows Information Protection (WIP) relates to files, buffers, clipboard, networking, background tasks, and data protection under lock.
MS-HAID: dev\_enterprise.edp\_hub
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: Windows Information Protection (WIP)
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, Windows Information Protection, enterprise data, enterprise data protection, edp, enlightened apps
ms.assetid: 08f0cfad-f15d-46f7-ae7c-824a8b1c44ea
ms.localizationpriority: medium
---
# Windows Information Protection (WIP)

__Note__ Windows Information Protection (WIP) policy can be applied to Windows 10, version 1607 or later.

WIP protects data that belongs to an organization by enforcing policies that are defined by the organization. If your app is included in those polices, all data produced by your app is subject to policy restrictions. This topic helps you to build apps that more gracefully enforce these policies without having any impact on the user's personal data.

## First, what is WIP?

WIP is a set of features on desktops, laptops, tablets, and phones that support the organization's Mobile Device Management (MDM) and Mobile Application Management (MAM) system.

WIP together with MDM gives the organization greater control over how its data is handled on devices that the organization manages. Sometimes users bring devices to work and do not enroll them in the organization's MDM.  In those cases, organizations can use MAM to achieve greater control over how their data is handled on specific line of business apps that users install on their device.

using MDM or MAM, administrators can identify which apps are allowed to access files that belong to the organization and whether users can copy data from those files and then paste that data into personal documents.

Here's how it works. Users enroll their devices into the organization's mobile device management (MDM) system. An administrator in the managing organization uses Microsoft Intune or System Center Configuration Manager (SCCM) to define and then deploy a policy to the enrolled devices.

If users aren't required to enroll their devices, administrators will use their MAM system to define and deploy a policy that applies to specific apps. When users install any of those apps, they'll receive the associated policy.

That policy identifies the apps that can access enterprise data (called the policy's *allowed list*). These apps can access enterprise protected files, Virtual Private Networks (VPN) and enterprise data on the clipboard or through a share contract. The policy also defines the rules that govern the data. For example, whether data can be copied from enterprise-owned files and then pasted into non enterprise-owned files.

If users unenroll their device from the organization's MDM system, or uninstall apps identified by the organizations MAM system, administrators can remotely wipe enterprise data from the device.

![Wip Lifecycle](images/wip-lifecycle.png)

> **Read more about WIP** <br>
* [Introducing Windows Information Protection](https://techcommunity.microsoft.com/t5/Windows-IT-Pro-Blog/bg-p/Windows10Blog)
* [Protect your enterprise data using Windows Information Protection (WIP)](/windows/whats-new/edp-whats-new-overview)

If your app is on the allowed list, all data produced by your app is subject to policy restrictions. That means that if administrators revoke the user's access to enterprise data, those users lose access to all of the data that your app produced.

This is fine if your app is designed only for enterprise use. But if your app creates data that users consider personal to them, you'll want to *enlighten* your app to intelligently discern between enterprise and personal data. We call this type of an app *enterprise-enlightened* because it can gracefully enforce enterprise policy while preserving the integrity of the user's personal data.

## Create an enterprise-enlightened app

Use WIP APIs to enlighten your app, and then declare your app as enterprise-enlightened.

Enlighten your app if it'll be used for both organizational and personal purposes.

Enlighten your app if you want to gracefully handle the enforcement of policy elements.

For example, if policy allows users to paste enterprise data into a personal document, you can prevent users from having to respond to a consent dialog before they paste the data. Similarly, you can present custom informational dialog boxes in response to these sorts of events.

If you're ready to enlighten your app, see one of these guides:

**For Universal Windows Platform (UWP) apps that you build by using C#**

[Windows Information Protection (WIP) developer guide](wip-dev-guide.md).

**For Desktop apps that you build by using C++**

[Windows Information Protection (WIP) developer guide (C++)](/previous-versions/windows/desktop/EDP/wip-developer-guide).


## Create non-enlightened enterprise app

if you're creating a Line of Business (LOB) app that is not intended for personal use, you might not have to enlighten it.

### Windows desktop apps
You don't need to enlighten a Windows desktop app but you should test your app to ensure that it functions properly under policy. For example, start your app, use it, then unenroll the device from MDM. Then, make sure the app can start again. If files critical to the operation of the app are encrypted, the app might not start. Also, review the files that your app interacts with to ensure that your app won't inadvertently encrypt files that are personal to the user. This might include metadata files, images and other things.

After you've tested your app, add this flag to the resource file or your project, and then recompile the app.

```cpp
MICROSOFTEDPAUTOPROTECTIONALLOWEDAPPINFO EDPAUTOPROTECTIONALLOWEDAPPINFOID
BEGIN
    0x0001
END
```
While MDM policies don't require the flag, MAM policies do.

### UWP apps

If you expect your app to be included in a MAM policy, you should enlighten it. Policies deployed to devices under MDM won't require it, but if you distribute your app to organizational consumers, it's difficult if not impossible to determine what type of policy management system they'll use. To ensure that your app will work in both types of policy management systems (MDM and MAM), you should enlighten your app.






 
