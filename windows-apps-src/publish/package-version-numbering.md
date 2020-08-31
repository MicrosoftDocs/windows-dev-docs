---
Description: The Microsoft Store enforces certain rules related to version numbers, which work somewhat differently in different OS versions.
title: Package version numbering
ms.assetid: DD7BAE5F-C2EE-44EE-8796-055D4BCB3152
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Package version numbering

Each package you provide must have a version number (provided as a value in the **Version** attribute of the [Package/Identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in the app manifest). The Microsoft Store enforces certain rules related to version numbers, which work somewhat differently in different OS versions.

> [!NOTE]
> This topic refers to "packages", but unless noted, the same rules apply to version numbers for both .msix/.appx and .msixbundle/.appxbundle files.


## Version numbering for Windows 10 packages

> [!IMPORTANT]
> For Windows 10 (UWP) packages, the last (fourth) section of the version number is reserved for Store use and must be left as 0 when you build your package (although the Store may change the value in this section). The other sections must be set to an integer between 0 and 65535 (except for the first section, which cannot be 0).

When choosing a UWP package from your published submission, the Microsoft Store will always use the highest-versioned package that is applicable to the customer’s Windows 10 device. This gives you greater flexibility and puts you in control over which packages will be provided to customers on specific device types. Importantly, you can submit these packages in any order; you are not limited to providing higher-versioned packages with each subsequent submission.

You can provide multiple UWP packages with the same version number. However, packages that share a version number cannot also have the same architecture, because the full identity that the Store uses for each of your packages must be unique. For more info, see [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity).

When you provide multiple UWP packages that use the same version number, the architecture (in the order x64, x86, ARM, neutral) will be used to decide which one is of higher rank (when the Store determines which package to provide to a customer's device). When ranking app bundles that use the same version number, the highest architecture rank within the bundle is considered: an app bundle that contains an x64 package will have a higher rank than one that only contains an x86 package.

This gives you a lot of flexibility to evolve your app over time. You can upload and submit new packages that use lower version numbers to add support for Windows 10 devices that you did not previously support, you can add higher-versioned packages that have stricter dependencies to take advantage of hardware or OS features, or you can add higher-versioned packages that serve as updates to some or all of your existing customer base.

The following example illustrates how version numbering can be managed to deliver the intended packages to your customers over multiple submissions.

### Example: Moving to a single package over multiple submissions

Windows 10 enables you to write a single codebase that runs everywhere. This makes starting a new cross-platform project much easier. However, for a number of reasons, you might not want to merge existing codebases to create a single project right away.

You can use the package versioning rules to gradually move your customers to a single package for the Universal device family, while shipping a number of interim updates for specific device families (including ones that take advantage of Windows 10 APIs). The example below illustrates how the same rules are consistently applied over a series of submissions for the same app.

| Submission | Contents                                                  | Customer experience                                                                                                                                                                             |
|------------|-----------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 1          | -   Package version: 1.1.10.0 <br> -   Device family: Windows.Desktop, minVersion 10.0.10240.0 <br> <br> -   Package version: 1.1.0.0 <br> -   Device family: Windows.Mobile, minVersion 10.0.10240.0     | -   Devices on Windows 10 Desktop build 10.0.10240.0 and above will get 1.1.10.0 <br> -   Devices on Windows 10 Mobile build 10.0.10240.0 and above will get 1.1.0.0 <br> -   Other device families will not be able to purchase and install the app |
| 2          | -   Package version: 1.1.10.0 <br> -   Device family: Windows.Desktop, minVersion 10.0.10240.0 <br> <br> -   Package version: 1.1.0.0 <br> -   Device family: Windows.Mobile, minVersion 10.0.10240.0 <br> <br> -   Package version: 1.0.0.0 <br> -   Device family: Windows.Universal, minVersion 10.0.10240.0    | -   Devices on Windows 10 Desktop build 10.0.10240.0 and above will get 1.1.10.0 <br> -   Devices on Windows 10 Mobile build 10.0.10240.0 and above will get 1.1.0.0 <br> -   Other (non-desktop, non-mobile) device families when they are introduced will get 1.0.0.0 <br> -   Desktop and mobile devices that already have the app installed will not see any update (because they already have the best available version—1.1.10.0 and 1.1.0.0 are both higher than 1.0.0.0) |
| 3          | -   Package version: 1.1.10.0 <br> -   Device family: Windows.Desktop, minVersion 10.0.10240.0 <br> <br> -   Package version: 1.1.5.0 <br> -   Device family: Windows.Universal, minVersion 10.0.10250.0 <br> <br> -   Package version: 1.0.0.0 <br> -   Device family: Windows.Universal, minVersion 10.0.10240.0    | -   Devices on Windows 10 Desktop build 10.0.10240.0 and above will get 1.1.10.0 <br> -   Devices on Windows 10 Mobile build 10.0.10250.0 and above will get 1.1.5.0 <br> -   Devices on Windows 10 Mobile build >=10.0.10240.0 and < 10.010250.0 will get 1.1.0.0 
| 4          | -   Package version: 2.0.0.0 <br> -   Device family: Windows.Universal, minVersion 10.0.10240.0   | -   All customers on all device families on Windows 10 build v10.0.10240.0 and above will get package 2.0.0.0 | 

> [!NOTE]
>  In all cases, customer devices will receive the package that has the highest possible version number that they qualify for. For example, in the third submission above, all desktop devices will get v1.1.10.0, even if they have OS version 10.0.10250.0 or higher and thus could also accept v1.1.5.0. Since 1.1.10.0 is the highest version number available to them, that is the package they will get.

### Using version numbering to roll back to a previously-shipped package for new acquisitions

If you keep copies of your packages, you'll have the option to roll back your app’s package in the Store to an earlier Windows 10 package if you should discover problems with a release. This is a temporary way to limit the disruption to your customers while you take time to fix the issue.

To do this, create a new [submission](app-submissions.md). Remove the problematic package and upload the old package that you want to provide in the Store. Customers who have already received the package you are rolling back will still have the problematic package (since your older package will have an earlier version number). But this will stop anyone else from acquiring the problematic package, while allowing the app to still be available in the Store.

To fix the issue for the customers who have already received the problematic package, you can submit a new Windows 10 package that has a higher version number than the bad package as soon as you can. After that submission goes through the certification process, all customers will be updated to the new package, since it will have a higher version number.


## Version numbering for Windows 8.1 (and earlier) and Windows Phone 8.1 packages

> [!IMPORTANT]
> You can no longer upload new XAP packages built using the Windows Phone 8.x SDK(s). Apps that are already in Store with XAP packages will continue to work on Windows 10 Mobile devices. For more info, see this [blog post](https://blogs.windows.com/windowsdeveloper/2018/08/20/important-dates-regarding-apps-with-windows-phone-8-x-and-earlier-and-windows-8-8-1-packages-submitted-to-microsoft-store).

For .appx packages that target Windows Phone 8.1, the version number of the package in a new submission must always be greater than that of the package included in the last submission (or any previous submission).

For .appx packages that target Windows 8 and Windows 8.1, the same rule applies per architecture: the version number of the package in a new submission must always be greater than that of the package last published to the Store for the same architecture.

Additionally, the version number of Windows 8.1 packages must always be greater than the version numbers of any of your Windows 8 packages for the same app. In other words, the version number of any Windows 8 package that you submit must be lower than the version number of any Windows 8.1 package that you've submitted for the same app.

> [!NOTE]
> If your app also has Windows 10 packages, the version number of the Windows 10 packages must be higher than those for any of your Windows 8, Windows 8.1, and/or Windows Phone 8.1 packages. For more info, see [Adding packages for Windows 10 to a previously-published app](guidance-for-app-package-management.md#adding-packages-for-windows-10-to-a-previously-published-app).

Here are some examples of what happens in different version number update scenarios for packages targeting Windows 8 and Windows 8.1.

| With this version of your app in the Store  | And you upload this version | After the new version is in the Store, this will be installed in a new acquisition | After the new version is in the Store, this will be updated if a customer already has the app |
|---------------------------------------------|-----------------------------|--------------------------------------------------------------------------------------------|----------|
| Nothing                                     | x86, v1.0.0.0               | x86, v1.0.0.0 on both x86 and x64 computers                                                | Nothing. |
| x86, v1.0.0.0                               | x64, v1.0.0.0               | v1.0.0.0 for the customer's architecture                                                   | Nothing. The version numbers are the same. |
| x86, v1.0.0.0 <br> x64, v1.0.0.0            | x64, v1.0.0.1               | v1.0.0.0 for customers with an x86 <br> v1.0.0.1 for customers with an x64                 | Nothing for customers running the app on an x86 computer. <br> v1.0.0.0 will be updated to v1.0.0.1 for customers running the app on an x64 computer. <br> **Note**  If the x86 version of the app is running on an x64 computer, the app won’t get updated to the x64 version unless the customer uninstalls and reinstalls. |
| Nothing                                     | neutral, v1.0.0.1           | neutral, v1.0.0.1 on all computers                                                         | Nothing. |
| neutral, v1.0.0.1                           | x86, v1.0.0.0 <br> x64, v1.0.0.0 <br> ARM, v1.0.0.0 | v1.0.0.0 for the architecture of the customer's computer.          | Nothing. Those who have the neutral, v1.0.0.1 version of the app will continue to use it. |
| neutral, v1.0.0.1 <br> x86, v1.0.0.0 <br> x64, v1.0.0.0 <br> ARM, v1.0.0.0 | x86, v1.0.0.1 <br> x64, v1.0.0.1 <br> ARM, v1.0.0.1 | v1.0.0.1 for the architecture of the customer's computer. | Nothing for customers running the neutral, v1.0.0.1 version app. <br> v1.0.0.0 will be updated to v1.0.0.1 for customers running v1.0.0.0 of the app built for their computer's specific architecture. |
| x86, v1.0.0.1 <br> x64, v1.0.0.1 <br> ARM, v1.0.0.1 | x86, v1.0.0.2 <br> x64, v1.0.0.2 <br> ARM, v1.0.0.2 | v1.0.0.2 for the architecture of the customer's computer.  | v1.0.0.1 will be updated to v1.0.0.2 for customers running either v1.0.0.1 of the app built for their computer's specific architecture. |

> [!NOTE]
> Unlike .appx packages, the version numbers in any .xap packages are not considered when determining which package to provide a given customer. To update a customer from one .xap package to a newer one, make sure to remove the older .xap in the new submission.