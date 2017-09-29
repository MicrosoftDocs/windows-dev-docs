---
author: jnHs
Description: The Packages page is where you upload all of the package files (.xap, .appx, .appxupload, and/or .appxbundle) for the app that you're submitting.
title: Upload app packages
ms.assetid: B1BB810D-3EAA-4FB5-B03C-1F01AFB2DE36
ms.author: wdg-dev-content
ms.date: 08/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, packages, upload, device family availability
---

# Upload app packages


The **Packages** page is where you upload all of the package files (.appx, .appxupload, .appxbundle, and/or .xap) for the app that you're submitting. You can upload packages for any operating system that your app targets in this step. When a customer downloads your app, the Store will automatically provide each customer with the package that works best for their device. After you upload your packages, you’ll see a table indicating [which packages will be offered to specific Windows 10 device families](#device-family-availability) (and earlier OS versions, if applicable) in ranked order.

For details about what a package includes and how it must be structured, see [App package requirements](app-package-requirements.md). You'll also want to learn about [how version numbers may impact which packages are delivered to specific customers](package-version-numbering.md), and [how packages are distributed to different operating systems](guidance-for-app-package-management.md).

## Uploading packages to your submission

To upload packages, drag them into the upload field or click to browse your files. The **Packages** page will let you upload .xap, .appx, .appxupload, and/or .appxbundle files.

If you have created any [package flights](package-flights.md) for your app, you’ll see a drop-down with the option to copy packages from one of your package flights. Select the package flight that has the packages you want to pull in. You can then select any or all of its packages to include in this submission.

> [!IMPORTANT]
> For Windows 10, we recommend uploading the .appxupload file here rather than an .appx or .appxbundle.  For more info about packaging UWP apps for the Store, see [Packaging a UWP app with Visual Studio](../packaging/packaging-uwp-apps.md).

If we detect issues with your packages while validating them, you'll need to remove the package, fix the issue, and then try uploading it again. For more info, see [Resolve package upload errors](resolve-package-upload-errors.md).

You may also see warnings to let you know about issues that may cause problems but won't block you from continuing with your submission.

## Device family availability

After your packages have been successfully uploaded, the **Device family availability** section will display a table that indicates which packages will be offered to specific Windows 10 device families (and earlier OS versions, if applicable), in ranked order. This section also lets you choose whether to offer the submission to customers on specific Windows 10 device families.

> [!NOTE]
> If you haven't uploaded packages yet, the **Device family availability** section will show the Windows 10 device families with checkboxes to indicate whether or not the submission will be offered to customers on those device families. The table will not appear until you upload your packages.

You'll also see a checkbox where you can indicate whether you want to allow Microsoft to make the app available to future Windows 10 device families. We recommend keeping this box checked so that your app can be available to more potential customers as new device families are introduced.

### Choosing which device families to support

You can uncheck the box for any Windows 10 device family if you don’t want to offer your submission to customers on that type of device. If a device family’s box is unchecked, new customers on that type of device won’t be able to acquire the app (though customers who already have the app can still use it, and will get any updates you submit). 

> [!NOTE]
> There are no checkboxes for **Windows 8/8.1** and **Windows Phone 8.x and earlier**. If your submission includes packages that can run on those OS versions, those packages will be made available to customers. To stop offering your app to customers on earlier OS versions, you’ll need to remove the corresponding packages from your submission.

If your app supports the mobile and desktop device families, we recommend keeping the boxes for **Windows 10 Mobile** and **Windows 10 Desktop** checked unless you have a specific reason to limit the types of Windows 10 devices which can acquire your app. For example, you may have created Windows Universal packages, but you know that you still need to test some issues with the app on mobile devices. To prevent new customers from downloading the app on Windows 10 mobile devices, you can uncheck the **Windows 10 Mobile** checkbox here. Then if you later decide you're ready to offer it to customers on Windows 10 mobile devices, you can create a new submission with the **Windows 10 Mobile** box checked.

<span id="xbox" />
If your app is not a game (or if it is a game and you have enabled the [Xbox Live Creators Program](../xbox-live/get-started-with-creators/get-started-with-xbox-live-creators.md) or gone through the [concept approval](../gaming/concept-approval.md) process), and your submission includes neutral and/or x64 UWP packages compiled using Windows 10 SDK version 14393 or later, you can check the **Windows 10 Xbox** box to offer the app to customers on Xbox. 

> [!IMPORTANT]
> In order for your app to launch on Xbox devices, you must include a neutral or x64 package that is compiled with Windows SDK version 14393 or higher. However, if you check **Windows 10 Xbox**, your highest-versioned package that’s applicable to Xbox (that is, a neutral or x64 package that targets the Xbox or Universal device family) will always be offered to customers on Xbox, even if it is compiled with an earlier SDK version. Because of this, it’s critical to ensure that the highest-versioned package applicable to Xbox is compiled with Windows SDK version 14393 or higher. If it is not, you will see an error message indicating that Xbox customers will not be able to launch your app. 
> 
> To resolve this error, you can do one of the following:
> -	Replace the applicable packages with new ones that are compiled using Windows SDK version 14393 or higher.
> -	If you already have a package that supports Xbox and is compiled with Windows SDK version 14393 or higher, increase its version number so that it is the highest-versioned package in the submission.
> -	Uncheck the box for **Windows 10 Xbox**.
> 	
> If you are still unable to resolve the issue, contact support.

If you have tested your app to ensure that it runs appropriately on Microsoft HoloLens, you can also check the **Windows 10 Holographic** box to offer the app to HoloLens customers. For more about building, testing, and publishing holographic apps, see the [Windows Holographic Development Overview](http://dev.windows.com/holographic/development_overview).

> [!IMPORTANT]
> To completely prevent a specific Windows 10 device family from getting your app, you need to update the [**TargetDeviceFamily**](https://msdn.microsoft.com/library/windows/apps/dn986903) element in your appx manifest to target only the device family that you want to support (i.e., Windows.Mobile or Windows.Desktop), rather than leaving it as the Windows.Universal value (for the universal device family) that Microsoft Visual Studio includes in the appx manifest by default.

It's important to be aware that selections you make in the **Device family availability** section apply only to new acquisitions. Anyone who already has your app can continue to use it, and will get any updates you submit, even if you remove their device family here. This applies even to customers who acquired your app before upgrading to Windows 10.

For example, if you have a published app with Windows Phone 8.1 packages, and you later add a Windows 10 (UWP) package to the same app that targets the universal device family, Windows 10 mobile customers who had your Windows Phone 8.1 package will be offered an update to this Windows 10 (UWP) package, even if you've unchecked the box for **Windows 10 Mobile** (since this is not a new acquisition, but an update). However, if you don't provide any Windows 10 (UWP) package that targets the universal or mobile device family, your Windows 10 mobile customers will remain on the Windows Phone 8.1 package.

For more info about device families, see [Intro to the Universal Windows Platform](https://msdn.microsoft.com/library/windows/apps/dn894631) and [**TargetDeviceFamily**](https://msdn.microsoft.com/library/windows/apps/dn986903).

### Understanding ranking

Aside from letting you indicate which Windows 10 device families can download your submission, the **Device family availability** section also shows you which of your specific packages will be made available to different device families. If you have more than one package that can run on a certain device family, the table will indicate the order in which packages will be offered, based on the version numbers of the packages. For more info about how the Store ranks packages based on version numbers, see [Package version numbering](package-version-numbering.md). 

For example, say that you have two packages: Package_A.appxupload and Package_B.appxupload. For a given device family, if Package_A.appxupload is ranked 1 and Package_B.appxupload is ranked 2, that means when a customer on that type of device acquires your app, the Store will first attempt to deliver Package_A.appxupload. If the customer’s device is unable to run Package_A.appxupload, the Store will offer Package_B.appxupload. If the customer’s device can’t run any of the packages for that device family—for example, if the **MinVersion** your app supports is higher than the version on the customer’s device—the customer won’t be able to download the app on that device.

> [!NOTE]
> The version numbers in .xap packages are not considered when determining which package to provide a given customer. Because of this, if you have more than one .xap package of equal rank, you will see an asterisk rather than a number, and customers may receive either package. To update customers from one .xap package to a newer one, make sure to remove the older .xap in the new submission.


## Package details

After your packages have been successfully uploaded, we'll list them, grouped by target operating system. The name, version, and architecture of the package will be displayed. For more info such as the supported languages, app capabilities, and file size for each package, click **Show details**.

If you need to remove a package from your submission, click the **Remove** link at the bottom of each package's **Details** section.


## Removing redundant packages

If we detect that one or more of your packages is redundant, we'll display a warning suggesting that you remove the redundant packages from this submission. Often this happens when you have previously uploaded packages, and now you are providing higher-versioned packages that support the same set of customers. In this case, no customers would ever get the redundant package, because you now have a better (higher-versioned) package to support these customers.

When we detect that you have redundant packages, we'll provide an option to remove all of the redundant packages from this submission automatically. You can also remove packages from the submission individually if you prefer.


## Gradual package rollout

If your submission is an update to a previously published app, you'll see a checkbox that says **Roll out update gradually after this submission is published (to Windows 10 customers only)**. This allows you to choose a percentage of customers who will get the packages from the submission so that you can monitor feedback and analytic data  to make sure you’re confident about the update before rolling it out more broadly. You can increase the percentage (or halt the update) any time without having to create a new submission. 

For more info, see [Gradual package rollout](gradual-package-rollout.md).


## Mandatory update

If your submission is an update to a previously published app, you'll see a checkbox that says **Make this update mandatory**. This allows you to set the date and time for a mandatory update, assuming you have used the Windows.Services.Store APIs to allow your app to programmatically check for package updates and download and install the updated packages. Your app must target Windows 10, version 1607 or later in order to use this option.

For more info, see [Download and install package updates for your app](../packaging/self-install-package-updates.md).

 




