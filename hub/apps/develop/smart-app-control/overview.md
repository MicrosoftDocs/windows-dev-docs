---
title: Smart App Control
description: Overview of Smart App Control
ms.topic: article
ms.date: 09/20/2022
---

# Smart App Control

Smart App Control is a new app execution control feature that combines Microsoft’s app intelligence services and Windows' code integrity features to protect users from untrusted or potentially dangerous code. Smart App Control selectively allows apps and binaries to run only if they're likely to be safe. Microsoft's app intelligence services provide safety predictions for many popular apps. If the app intelligence service is unable to make a prediction, then Smart App Control will still allow an app to run if it is signed with a certificate issued by a certificate authority (CA) within the Trusted Root Program. 

Malware, Potentially Unwanted Apps (PUA), and unknown, unsigned code are  blocked by default.

## Smart App Control requirements

Smart App Controlis designed to protect a device for its entire lifetime. As such, it can only be enabled on a clean install of a version of Windows that contains the Smart App Control feature. Additionally, Smart App Control is only enabled in certain regions. We hope to roll out additional regions soon.

 - Windows 10 or Windows 11, version 22572 or higher. 
 - A clean Windows install
   > [!NOTE]
   > [Resetting your device](/windows-hardware/service/desktop/resetting-the-pc) counts as a clean Windows install.
 - North America or Europe

## Smart App Control stages

Smart App Control can be running in either *evaluation mode* or *enforcement mode*.

In *evaluation mode*, Smart App Control is running in the background, observing activity on the device. During this time, Smart App Control is evaluating whether the device is a good fit for the protection it offers based the variety of apps installed and used on the device.

In *enforcement mode*, Smart App Control is actively protecting your device. Apps cannot be run unless they are recognized by Microsoft's app intelligence services, or signed with a trusted certificate.

## Frequently Asked Questions

### Is Smart App Control installed on my device?

![A settings window that contains an option to configure Smart App Control settings.](images/settings-smart-app-control.png)

Go to **Settings** > **Windows Security** > **App and Browser Control**. If Smart App Control is installed on your system, you will see a section called **Smart App Control**

### Is Smart App Control running in evaluation or enforcement mode?

Go to **Settings** > **Windows Security** > **App and Browser Control**. 

 - If **On** is selected, Smart App Control is running in enforcement mode. 
 - If **Evaluation** is selected, Smart App Control is running in evaluation mode.
 - If **Off** is selected, Smart App Control is not running on this device.

### Will I be notified when Smart App Control enters enforcement mode?

Yes. You will receive a [Toast Notification](/windows/apps/design/shell/tiles-and-notifications/toast-notifications-overview) when Smart App Control enters enforcement mode.
