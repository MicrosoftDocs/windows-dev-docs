﻿---
author: jnHs
Description: After your packages have been successfully uploaded, you'll see a table that indicates which packages will be offered to specific Windows 10 device families (and earlier OS versions, if applicable), in ranked order.
title: Device family availability
ms.author: wdg-dev-content
ms.date: 03/16/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, packages, upload, device family availability
ms.localizationpriority: high
---

# Device family availability

After your packages have been successfully uploaded on the **Packages** page, the **Device family availability** section will display a table that indicates which packages will be offered to specific Windows 10 device families (and earlier OS versions, if applicable), in ranked order. This section also lets you choose whether or not to offer the submission to customers on specific Windows 10 device families.

> [!NOTE]
> If you haven't uploaded packages yet, the **Device family availability** section will show the Windows 10 device families with checkboxes that let you indicate whether or not the submission will be offered to customers on those device families. The table will appear after you upload one or more packages.

This section also includes a checkbox where you can indicate whether you want to allow Microsoft to make the app available to any future Windows 10 device families. We recommend keeping this box checked so that your app can be available to more potential customers as new device families are introduced.


## Choosing which device families to support

If you upload packages targeting one individual device family, we'll check the box to make those packages available to new customers on that type of device. For example, if a package targets Windows.Desktop, the **Windows 10 Desktop** box will be checked for that package (and you won't be able to check the boxes for other device families).

Packages targeting the Windows.Universal device family can run on any Windows 10 device (including Xbox One). By default, we'll make those packages available to new customers on all device types *except* for Xbox.

You can uncheck the box for any Windows 10 device family if you don’t want to offer your submission to customers on that type of device. If a device family’s box is unchecked, new customers on that type of device won’t be able to acquire the app (though customers who already have the app can still use it, and will get any updates you submit).

If your app supports them, we recommend keeping all of the boxes checked, unless you have a specific reason to limit the types of Windows 10 devices which can acquire your app. For instance, if you know that your app doesn't offer a good experience on [Surface Hub](https://developer.microsoft.com/windows/surfacehub) and/or [Microsoft HoloLens](http://dev.windows.com/holographic/development_overview), you can uncheck the **Windows 10 Team** and/or **Windows 10 Holographic** box. This prevents any new customers from acquiring the app on those devices. If you later decide you're ready to offer it to those customers, you can create a new submission with the boxes checked.

<span id="xbox" />

The only Windows 10 device family that is not checked by default for Windows.Universal packages is **Windows 10 Xbox**. If your app is not a game (or if it is a game and you have enabled the [Xbox Live Creators Program](../xbox-live/get-started-with-creators/get-started-with-xbox-live-creators.md) or gone through the [concept approval](../gaming/concept-approval.md) process), and your submission includes neutral and/or x64 UWP packages compiled using Windows 10 SDK version 14393 or later, you can check the **Windows 10 Xbox** box to offer the app to customers on Xbox One.

> [!IMPORTANT]
> In order for your app to launch on Xbox devices, you must include a neutral or x64 package that is compiled with Windows SDK version 14393 or higher. However, if you check **Windows 10 Xbox**, your highest-versioned package that’s applicable to Xbox (that is, a neutral or x64 package that targets the Xbox or Universal device family) will always be offered to customers on Xbox, even if it is compiled with an earlier SDK version. Because of this, it’s critical to ensure that the highest-versioned package applicable to Xbox is compiled with Windows SDK version 14393 or higher. If it is not, you will see an error message indicating that Xbox customers will not be able to launch your app. 
> 
> To resolve this error, you can do one of the following:
> -	Replace the applicable packages with new ones that are compiled using Windows SDK version 14393 or higher.
> -	If you already have a package that supports Xbox and is compiled with Windows SDK version 14393 or higher, increase its version number so that it is the highest-versioned package in the submission.
> -	Uncheck the box for **Windows 10 Xbox**.
> 	
> If you are still unable to resolve the issue, contact support.

If you're submitting a UWP app for Windows 10 IoT Core, you should not make changes to the default selections after uploading your packages; there is no separate checkbox for Windows 10 IoT. For more about publishing IoT Core UWP apps, see [Microsoft Store support for IoT Core UWP apps](https://docs.microsoft.com/windows/iot-core/commercialize-your-device/installingandservicing).

If your submission includes packages that can run on **Windows 8/8.1** and **Windows Phone 8.x and earlier**, those packages will be made available to customers, as shown in the table. There are no checkboxes for these OS versions. To stop offering your app to these customers, remove the corresponding packages from your submission.

> [!IMPORTANT]
> To completely prevent a specific Windows 10 device family from getting your submission, update the [**TargetDeviceFamily**](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) element in your appx manifest to target only the device family that you want to support (i.e., Windows.Mobile or Windows.Desktop), rather than leaving it as the Windows.Universal value (for the universal device family) that Microsoft Visual Studio includes in the appx manifest by default.

It's important to be aware that selections you make in the **Device family availability** section apply only to new acquisitions. Anyone who already has your app can continue to use it, and will get any updates you submit, even if you remove their device family here. This applies even to customers who acquired your app before upgrading to Windows 10.

For example, if you have a published app with Windows Phone 8.1 packages, and you later add a Windows 10 (UWP) package to the same app that targets the universal device family, Windows 10 mobile customers who had your Windows Phone 8.1 package will be offered an update to this Windows 10 (UWP) package, even if you've unchecked the box for **Windows 10 Mobile** (since this is not a new acquisition, but an update). However, if you don't provide any Windows 10 (UWP) package that targets the universal or mobile device family, your Windows 10 mobile customers will remain on the Windows Phone 8.1 package.

For more info about device families, see [**Device families overview**](https://docs.microsoft.com/uwp/extension-sdks/device-families-overview).

## Understanding ranking

Aside from letting you indicate which Windows 10 device families can download your submission, the **Device family availability** section also shows you which of your specific packages will be made available to different device families. If you have more than one package that can run on a certain device family, the table will indicate the order in which packages will be offered, based on the version numbers of the packages. For more info about how the Store ranks packages based on version numbers, see [Package version numbering](package-version-numbering.md). 

For example, say that you have two packages: Package_A.appxupload and Package_B.appxupload. For a given device family, if Package_A.appxupload is ranked 1 and Package_B.appxupload is ranked 2, that means when a customer on that type of device acquires your app, the Store will first attempt to deliver Package_A.appxupload. If the customer’s device is unable to run Package_A.appxupload, the Store will offer Package_B.appxupload. If the customer’s device can’t run any of the packages for that device family—for example, if the **MinVersion** your app supports is higher than the version on the customer’s device—the customer won’t be able to download the app on that device.

> [!NOTE]
> The version numbers in .xap packages are not considered when determining which package to provide a given customer. Because of this, if you have more than one .xap package of equal rank, you will see an asterisk rather than a number, and customers may receive either package. To update customers from one .xap package to a newer one, make sure to remove the older .xap in the new submission.

