---
author: jnHs
Description: Learn how your app's packages are made available to your customers, and how to manage specific package scenarios.
title: Guidance for app package management
ms.assetid: 55405D0B-5C1E-43C8-91A1-4BFDD336E6AB
ms.author: wdg-dev-content
ms.date: 03/28/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: high
---

# Guidance for app package management

Learn how your app's packages are made available to your customers, and how to manage specific package scenarios.

-   [OS versions and package distribution](#os-versions-and-package-distribution)
-   [Adding packages for Windows 10 to a previously-published app](#adding-packages-for-windows-10-to-a-previously-published-app)
-   [Maintaining package compatibility for Windows Phone 8.1](#maintaining-package-compatibility-for-windows-phone-81)
-   [Removing an app from the Store](#removing-an-app-from-the-store)
-   [Removing packages for a previously-supported device family](#removing-packages-for-a-previously-supported-device-family)


## OS versions and package distribution

Different operating systems can run different types of packages. If more than one of your packages can run on a customer's device, the Microsoft Store will provide the best available match.

Generally speaking, later OS versions can run packages that target previous OS versions for the same device family. However, customers will only get those packages if the app doesn't include a package that targets their current OS version.

For example, Windows 10 devices can run all previous supported OS versions (per device family). Windows 10 desktop devices can run apps that were built for Windows 8.1 or Windows 8; Windows 10 mobile devices can run apps that were built for Windows Phone 8.1, Windows Phone 8, and even Windows Phone 7.x. 

The following examples illustrate various scenarios for an app that includes packages targeting different OS versions (unless specific constraints of your packages don't allow them to run on every OS version/device type listed here; for example, the package's architecture must be appropriate for the device). 

### Example app 1

| Package's targeted operating system | Operating systems that will get this package |
|-------------------------------------|----------------------------------------------|
| Windows 8.1                         | Windows 10 desktop devices, Windows 8.1      |
| Windows Phone 8.1                   | Windows 10 mobile devices, Windows Phone 8.1 |
| Windows Phone 8                     | Windows Phone 8                              |
| Windows Phone 7.1                   | Windows Phone 7.x                            |

In example app 1, the app does not yet have Universal Windows Platform (UWP) packages that are specifically built for Windows 10 devices, but customers on Windows 10 can still get the app. Those customers will get the best packages available for their device type.

### Example app 2

| Package's targeted operating system  | Operating systems that will get this package |
|--------------------------------------|----------------------------------------------|
| Windows 10 (universal device family) | Windows 10 (all device families)             |
| Windows 8.1                          | Windows 8.1                                  |
| Windows Phone 8.1                    | Windows Phone 8.1                            |
| Windows Phone 7.1                    | Windows Phone 7.x, Windows Phone 8           |

In example app 2, there is no package that can run on Windows 8. Customers who are running any other OS version can get the app. All customers on Windows 10 will get the same package.

### Example app 3

| Package's targeted operating system | Operating systems that will get this package                  |
|-------------------------------------|---------------------------------------------------------------|
| Windows 10 (desktop device family)  | Windows 10 desktop devices                                    |
| Windows Phone 8                     | Windows 10 mobile devices, Windows Phone 8, Windows Phone 8.1 |

In example app 3, since there is no UWP package that targets the mobile device family, customers on Windows 10 mobile devices will get the Windows Phone 8 package. If this app later adds a package that targets the mobile device family (or the universal device family), that package will then be available to customers on Windows 10 mobile devices instead of the Windows Phone 8 package.

Also note that this example app does not include any package that can run on Windows Phone 7.x.

### Example app 4

| Package's targeted operating system  | Operating systems that will get this package |
|--------------------------------------|----------------------------------------------|
| Windows 10 (universal device family) | Windows 10 (all device families)             |

In example app 4, any device that is running Windows 10 can get the app, but it will not be available to customers on any previous OS version. Because the UWP package targets the universal device family, it will be available to any Windows 10 device (per your [device family availability selections](device-family-availability.md)).


## Removing an app from the Store

At times, you may want to stop offering an app to customers, effectively "unpublishing" it. To do so, click **Make app unavailable** from the **App overview** page. After you confirm that you want to make the app unavailable, within a few hours it will no longer be visible in the Store, and no new customers will be able to get it (unless they have a [promotional code](generate-promotional-codes.md) and are using a Windows 10 device).

> [!IMPORTANT]
> This option will override any [visibility](choose-visibility-options.md#discoverability) settings that you have selected in your submissions. 

This option has the same effect as if you created a submission and chose **Make this product available but not discoverable in the Store** with the **Stop acquisition** option. However, it does not require you to create a new submission.

Note that any customers who already have the app will still be able to use it and can download it again (and could even get updates if you submit new packages later).

After making the app unavailable, you'll still see it in your dashboard. If you decide to offer the app to customers again, you can click **Make app available** from the App overview page. After you confirm, the app will be available to new customers (unless restricted by the settings in your last submission) within a few hours.

> [!NOTE]
> If you want to keep your app available, but don't want to continuing offering it to new customers on a particular OS version, you can create a new submission and remove all packages for the OS version on which you want to prevent new acquisitions. For example, if you previously had packages for Windows Phone 8.1 and Windows 10, and you don't want to keep offering the app to new customers on Windows Phone 8.1, remove all of your Windows Phone 8.1 packages from the submission. After the update is published, no new customers on Windows Phone 8.1 will be able to acquire the app though customers who already have it can continue to use it). However, the app will still be available for new customers on Windows 10.


## Removing packages for a previously-supported device family

If you remove all packages for a certain [device family](https://docs.microsoft.com/uwp/extension-sdks/device-families-overview) that your app previously supported, you'll be prompted to confirm that this is your intention before you can save your changes on the **Packages** page.

When you publish a submission that removes all of the packages that could run on a device family that your app previously supported, new customers will not be able to acquire the app on that device family. You can always publish another update later to provide packages for that device family again.

Be aware that even if you remove all of the packages that support a certain device family, any existing customers who have already installed the app on that type of device can still use it, and they will get any updates you provide later.


<a name="adding-packages-for-windows-10-to-a-previously-published-app"></a>

## Adding packages for Windows 10 to a previously-published app

If you have an app in the Store that targets Windows 8.x and/or Windows Phone 8.x, and you want to update your app for Windows 10, create a new submission and add your UWP .appxupload package(s) during the [Packages](upload-app-packages.md) step. After your app goes through the certification process, customers who already had your app and are now on Windows 10 will get your UWP package as an update from the Store. The UWP package will also be available for new acquisitions by customers on Windows 10.

> [!NOTE]
> Once a customer on Windows 10 gets your UWP package, you can't roll that customer back to using a package for any previous OS version. 

Note that the version number of your Windows 10 packages must be higher than those for any Windows 8, Windows 8.1, and/or Windows Phone 8.1 packages you include (or packages for those OS versions that you have previously published). For more info, see [Package version numbering](package-version-numbering.md).

For more info about packaging UWP apps for the Store, see [Packaging apps](../packaging/index.md).

> [!IMPORTANT]
> Keep in mind that if you provide packages that target the universal device family, every customer who already had your app on any earlier operating system (Windows Phone 8, Windows 8.1, etc.) and then upgrades to Windows 10 will be updated to get your Windows 10 package.
> 
> This happens even if you have excluded a specific device family in the [Device family availability](device-family-availability.md) step of your submission, since that section only applies to new acquisitions. If you don't want every previous customer to get your universal Windows 10 package, be sure to update the [**TargetDeviceFamily**](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) element in your appx manifest to include only the particular device family you wish to support.
> 
> For example, say you want your Windows 8 and Windows 8.1 customers who have upgraded to a Windows 10 desktop device to get your new UWP app, but you want any Windows Phone customers who are now on Windows 10 Mobile devices to keep the packages you'd previously made available (targeting Windows Phone 8 or Windows Phone 8.1). To do this, you'll need to update the [**TargetDeviceFamily**](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) in your appx manifest to include only **Windows.Desktop** (for the desktop device family), rather than leaving it as the **Windows.Universal** value (for the universal device family) that Microsoft Visual Studio includes in the manifest by default. Do not submit any UWP packages that target either the Universal or Mobile device families (**Windows.Universal** or **Windows.Universal**). This way, your Windows 10 Mobile customers will not get any of your UWP packages.


## Maintaining package compatibility for Windows Phone 8.1

Certain requirements for package types apply when updating apps that were previously published for Windows Phone 8.1:

-   After an app has a published Windows Phone 8.1 package, all subsequent updates must also contain a Windows Phone 8.1 package.
-   After an app has a published Windows Phone 8.1 XAP, subsequent updates must either have a Windows Phone 8.1 XAP, Windows Phone 8.1 appx, or Windows Phone 8.1 appxbundle.
-   When an app has a published Windows Phone 8.1 .appx, subsequent updates must either have a Windows Phone 8.1 .appx or Windows Phone 8.1 .appxbundle. In other words, a Windows Phone 8.1 XAP is not allowed. This applies to an .appxupload that contains a Windows Phone 8.1 .appx as well.
-   After an app has a published Windows Phone 8.1 .appxbundle, subsequent updates must have a Windows Phone 8.1 .appxbundle. In other words, a Windows Phone 8.1 XAP or Windows Phone 8.1 .appx is not allowed. This applies to an .appxupload that contains a Windows Phone 8.1 .appxbundle as well.

Failure to follow these rules may result in package upload errors that will prevent you from completing your submission.