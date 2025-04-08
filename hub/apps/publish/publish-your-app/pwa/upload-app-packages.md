---
description: The Packages page allows you to upload the installers and files for your PWA app. Once uploaded, a table will display the packages, showing which will be offered to specific Windows 10 device families (and earlier OS versions if applicable), in ranked order.
title: Upload app packages for PWA
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Upload app packages for PWA

## Upload your app's packages

The **Packages** page of the [app submission process](./create-app-submission.md) is where you upload all of the package files (.msix, .msixupload, .msixbundle, .appx, .appxupload, and/or .appxbundle) for the app that you're submitting. You can upload all your packages for the same app on this page, and when a customer downloads your app, the Store will automatically provide each customer with the package that works best for their device. After you upload your packages, you’ll see a table indicating [which packages will be offered to specific Windows 10 or Windows 11 device families](#device-family-availability) (and earlier OS versions, if applicable) in ranked order.

:::image type="content" source="../msix/images/msix-packages-overview.png" lightbox="../msix/images/msix-packages-overview.png" alt-text="A screenshot showing the overview of packages page for MSIX/PWA app.":::

### Uploading packages to your submission

To upload packages, drag them into the upload field or click to browse your files. The **Packages** page will let you upload .msix, .msixupload, .msixbundle, .appx, .appxupload, and/or .appxbundle files.

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

## Device family availability

After your packages have been [successfully uploaded on the **Packages** page](./upload-app-packages.md), the **Device family availability** section will display a table that indicates which packages will be offered to specific Windows 10 or Windows 11 device families (and earlier OS versions, if applicable), in ranked order. This section also lets you choose whether or not to offer the submission to customers on specific Windows 10 or Windows 11 device families.

> [!NOTE]
> If you haven't uploaded packages yet, the **Device family availability** section will show the Windows 10 or Windows 11 device families with checkboxes that let you indicate whether or not the submission will be offered to customers on those device families. The table will appear after you upload one or more packages.

This section also includes a checkbox where you can indicate whether you want to allow Microsoft to make the app available to any future Windows 10 or Windows 11 device families. We recommend keeping this box checked so that your app can be available to more potential customers as new device families are introduced.

