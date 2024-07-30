---
description: The Packages page allows you to upload the installers and files for your MSIX app. Once uploaded, a table will display the packages, showing which will be offered to specific Windows 10 device families (and earlier OS versions if applicable), in ranked order.
title: Upload MSIX app packages
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Upload MSIX app packages

## Upload your app's packages

The **Packages** page of the [app submission process](./create-app-submission.md) is where you upload all of the package files (.msix, .msixupload, .msixbundle, .appx, .appxupload, and/or .appxbundle) for the app that you're submitting. You can upload all your packages for the same app on this page, and when a customer downloads your app, the Store will automatically provide each customer with the package that works best for their device. After you upload your packages, you’ll see a table indicating [which packages will be offered to specific Windows 10 or Windows 11 device families](#device-family-availability) (and earlier OS versions, if applicable) in ranked order.

:::image type="content" source="images/msix-packages-overview.png" lightbox="images/msix-packages-overview.png" alt-text="A screenshot showing the overview of packages page for MSIX/PWA app.":::

For details about what a package includes and how it must be structured, see [App package requirements](./app-package-requirements.md). You'll also want to learn about [how version numbers impact which packages are delivered to specific customers](./app-package-requirements.md#package-version-numbering) and [how to manage packages for various scenarios](./app-package-management.md).

### Uploading packages to your submission

To upload packages, drag them into the upload field or click to browse your files. The **Packages** page will let you upload .msix, .msixupload, .msixbundle, .appx, .appxupload, and/or .appxbundle files.

> [!IMPORTANT]
> For Windows 10 and above, we recommend uploading the .msixupload or .appxupload file here rather than .msix, .appx, .msixbundle, or .appxbundle. For more info about packaging UWP apps for the Store, see [Packaging a UWP app with Visual Studio](/windows/msix/package/packaging-uwp-apps).

If you have created any [package flights](../../package-flights.md) for your app, you’ll see a drop-down with the option to copy packages from one of your package flights. Select the package flight that has the packages you want to pull in. You can then select any or all of its packages to include in this submission.

If we detect errors with a package while validating it, we'll display a message to let you know what's wrong. You'll need to remove the package, fix the issue, and then try uploading it again. You may also see warnings to let you know about issues that may cause problems but won't block you from continuing with your submission.

### Package details

Your uploaded packages are listed here, grouped by target operating system. The name, version, and architecture of the package will be displayed. For more info such as the supported languages, app capabilities, and file size for each package, click **Show details**.

If you need to remove a package from your submission, click the **Remove** link at the bottom of each package's **Details** section.

### Removing redundant packages

If we detect that one or more of your packages is redundant, we'll display a warning suggesting that you remove the redundant packages from this submission. Often this happens when you have previously uploaded packages, and now you are providing higher-versioned packages that support the same set of customers. In this case, no customers would ever get the redundant package, because you now have a better (higher-versioned) package to support these customers.

When we detect that you have redundant packages, we'll provide an option to remove all of the redundant packages from this submission automatically. You can also remove packages from the submission individually if you prefer.

### Gradual package rollout

If your submission is an update to a previously published app, you'll see a checkbox that says **Roll out update gradually after this submission is published (to Windows 10 or Windows 11 customers only)**. This allows you to choose a percentage of customers who will get the packages from the submission so that you can monitor feedback and analytic data to make sure you’re confident about the update before rolling it out more broadly. You can increase the percentage (or halt the update) any time without having to create a new submission.

For more info, see [Gradual package rollout](../../gradual-package-rollout.md).

### Mandatory update

If your submission is an update to a previously published app, you'll see a checkbox that says **Make this update mandatory**. This allows you to set the date and time for a mandatory update, assuming you have used the Windows.Services.Store APIs to allow your app to programmatically check for package updates and download and install the updated packages. Your app must target Windows 10, version 1607 or later in order to use this option.

For more info, see [Download and install package updates for your app](/windows/uwp/packaging/self-install-package-updates).

## Device family availability

After your packages have been [successfully uploaded on the **Packages** page](./upload-app-packages.md), the **Device family availability** section will display a table that indicates which packages will be offered to specific Windows 10 or Windows 11 device families (and earlier OS versions, if applicable), in ranked order. This section also lets you choose whether or not to offer the submission to customers on specific Windows 10 or Windows 11 device families.

> [!NOTE]
> If you haven't uploaded packages yet, the **Device family availability** section will show the Windows 10 or Windows 11 device families with checkboxes that let you indicate whether or not the submission will be offered to customers on those device families. The table will appear after you upload one or more packages.

This section also includes a checkbox where you can indicate whether you want to allow Microsoft to make the app available to any future Windows 10 or Windows 11 device families. We recommend keeping this box checked so that your app can be available to more potential customers as new device families are introduced.

### Choosing which device families to support

If you upload packages targeting one individual device family, we'll check the box to make those packages available to new customers on that type of device. For example, if a package targets Windows.Desktop, the **Windows 10/11 Desktop** box will be checked for that package (and you won't be able to check the boxes for other device families).

Packages targeting the Windows.Universal device family can run on any Windows 10 or Windows 11 device (including Xbox One). By default, we'll make those packages available to new customers on all device types _except_ for Xbox.

You can uncheck the box for any Windows 10 or Windows 11 device family if you don’t want to offer your submission to customers on that type of device. If a device family’s box is unchecked, new customers on that type of device won’t be able to acquire the app (though customers who already have the app can still use it, and will get any updates you submit).

If your app supports them, we recommend keeping all of the boxes checked, unless you have a specific reason to limit the types of Windows 10 or Windows 11 devices which can acquire your app. For instance, if you know that your app doesn't offer a good experience on [Surface Hub](https://developer.microsoft.com/windows/surfacehub) and/or [Microsoft HoloLens](https://developer.microsoft.com/mixed-reality), you can uncheck the **Windows 10 Team** and/or **Windows 10 Holographic** box. This prevents any new customers from acquiring the app on those devices. If you later decide you're ready to offer it to those customers, you can create a new submission with the boxes checked.

#### Xbox devices

The only Windows 10 or Windows 11 device family that is not checked by default for Windows.Universal packages is **Windows 10 Xbox**. If your app is not a game (or if it is a game and you have enabled the [Xbox Live Creators Program](/gaming/xbox-live/get-started-with-creators/get-started-with-xbox-live-creators) or gone through the [concept approval](/windows/uwp/gaming/concept-approval) process), and your submission includes neutral and/or x64 UWP packages compiled using Windows 10 SDK version 14393 or later, you can check the **Windows 10 Xbox** box to offer the app to customers on Xbox One.

> [!IMPORTANT]
> In order for your app to launch on Xbox devices, you must include a neutral or x64 package that is compiled with Windows SDK version 14393 or later. However, if you check **Windows 10 Xbox**, your highest-versioned package that’s applicable to Xbox (that is, a neutral or x64 package that targets the Xbox or Universal device family) will always be offered to customers on Xbox, even if it is compiled with an earlier SDK version. Because of this, it’s critical to ensure that the highest-versioned package applicable to Xbox is compiled with Windows SDK version 14393 or later. If it is not, you will see an error message indicating that Xbox customers will not be able to launch your app.
>
> To resolve this error, you can do one of the following:
>
> - Replace the applicable packages with new ones that are compiled using Windows SDK version 14393 or later.
> - If you already have a package that supports Xbox and is compiled with Windows SDK version 14393 or later, increase its version number so that it is the highest-versioned package in the submission.
> - Uncheck the box for **Windows 10 Xbox**.
>
> If you are still unable to resolve the issue, contact support.

If you're submitting a UWP app for Windows 10 IoT Core, you should not make changes to the default selections after uploading your packages; there is no separate checkbox for Windows 10 IoT. For more about publishing IoT Core UWP apps, see [Microsoft Store support for IoT Core UWP apps](/windows/iot-core/commercialize-your-device/installingandservicing).

> [!IMPORTANT]
> To completely prevent a specific Windows 10 or Windows 11 device family from getting your submission, update the [**TargetDeviceFamily**](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) element in your manifest to target only the device family that you want to support (i.e. Windows.Desktop), rather than leaving it as the Windows.Universal value (for the universal device family) that Microsoft Visual Studio includes in the manifest by default.

It's important to be aware that selections you make in the **Device family availability** section apply only to new acquisitions. Anyone who already has your app can continue to use it, and will get any updates you submit, even if you remove their device family here. This applies even to customers who acquired your app before upgrading to Windows 10 or Windows 11.

For more info about device families, see [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview).

### Understanding ranking

Aside from letting you indicate which Windows 10 or Windows 11 device families can download your submission, the **Device family availability** section shows the specific packages that will be made available to different device families. If you have more than one package that can run on a certain device family, the table will indicate the order in which packages will be offered, based on the version numbers of the packages. For more info about how the Store ranks packages based on version numbers, see [Package version numbering](./app-package-requirements.md#package-version-numbering).

For example, say that you have two packages: Package_A.appxupload and Package_B.appxupload. For a given device family, if Package_A.appxupload is ranked 1 and Package_B.appxupload is ranked 2, that means when a customer on that type of device acquires your app, the Store will first attempt to deliver Package_A.appxupload. If the customer’s device is unable to run Package_A.appxupload, the Store will offer Package_B.appxupload. If the customer’s device can’t run any of the packages for that device family (for example, if the **MinVersion** your app supports is higher than the version on the customer’s device) then the customer won’t be able to download the app on that device.

> [!NOTE]
> The version numbers in .xap packages (for previously published apps) are not considered when determining which package to provide a given customer. Because of this, if you have more than one .xap package of equal rank, you will see an asterisk rather than a number, and customers may receive either package. To update customers from one .xap package to a newer one, make sure to remove the older .xap in the new submission.
