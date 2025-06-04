---
description: Prepare your MSIX app's packages for submission to the Microsoft Store by following these guidelines. Be aware that the store enforces specific rules related to version numbers, which may vary across different OS versions. Additionally, you can refer to a table of supported languages and their corresponding language codes for app submission.
title: App package requirements for MSIX app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# App package requirements for MSIX app

## Requirements

Follow these guidelines to prepare your app's packages for submission to the Microsoft Store.

### Before you build your app's package for the Microsoft Store

Make sure to [test your app with the Windows App Certification Kit](/windows/uwp/debug-test-perf/windows-app-certification-kit). We also recommend that you test your app on different types of hardware. Note that until we certify your app and make it available from the Microsoft Store, it can only be installed and run on computers that have developer licenses.

### Building the app package using Microsoft Visual Studio

If you're using Microsoft Visual Studio as your development environment, you already have built-in tools that make creating an app package a quick and easy process. For more info, see [Packaging apps](/windows/uwp/packaging/).

> [!NOTE]
> Be sure that all your filenames use ANSI.

When you create your package in Visual Studio, make sure you are signed in with the same account associated with your developer account. Some parts of the package manifest have specific details related to your account. This info is detected and added automatically. Without the additional information added to the manifest, you may encounter package upload failures.

When you build your app's UWP packages, Visual Studio can create an .msix or appx file, or a .msixupload or .appxupload file. For UWP apps, we recommend that you always upload the .msixupload or .appxupload file in the [Packages](./upload-app-packages.md) page. For more info about packaging UWP apps for the Store, see [Package a UWP app with Visual Studio](/windows/msix/package/packaging-uwp-apps).

Your app's packages don't have to be signed with a certificate rooted in a trusted certificate authority.

#### App bundles

For UWP apps, Visual Studio can generate an app bundle (.msixbundle or .appxbundle) to reduce the size of the app that users download. This can be helpful if you've defined language-specific assets, a variety of image-scale assets, or resources that apply to specific versions of Microsoft DirectX.

> [!NOTE]
>  One app bundle can contain your packages for all architectures.

With an app bundle, a user will only download the relevant files, rather than all possible resources. For more info about app bundles, see [Packaging apps](/windows/uwp/packaging/) and [Package a UWP app with Visual Studio](/windows/msix/package/packaging-uwp-apps).

### Building the app package manually

If you don't use Visual Studio to create your package, you must [create your package manifest manually](/uwp/schemas/appxpackage/how-to-create-a-package-manifest-manually).

Be sure to review the [App package manifest](/uwp/schemas/appxpackage/appx-package-manifest) documentation for complete manifest details and requirements. Your manifest must follow the package manifest schema in order to pass certification.

Your manifest must include some specific info about your account and your app. You can find this info by looking at [View app identity details](../../view-app-identity-details.md) in the **Product management** section of your app's overview page in the dashboard.

> [!NOTE]
>  Values in the manifest are case-sensitive. Spaces and other punctuation must also match. Enter the values carefully and review them to ensure that they are correct.

App bundles (.msixbundle or .appxbundle) use a different manifest. Review the [Bundle manifest](/uwp/schemas/bundlemanifestschema/bundle-manifest) documentation for the details and requirements for app bundle manifests. Note that in a .msixbundle or .appxbundle, the manifest of each included package must use the same elements and attributes, except for the **ProcessorArchitecture** attribute of the [Identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element.

> [!TIP]
>  Be sure to run the [Windows App Certification Kit](/windows/uwp/debug-test-perf/windows-app-certification-kit) before you submit your packages. This can you help determine if your manifest has any problems that might cause certification or submission failures.

### Package format requirements

Your app’s packages must comply with these requirements.

| App package property | Requirement                                                                                                                                   |
| -------------------- | --------------------------------------------------------------------------------------------------------------------------------------------- |
| Package size         | .msixbundle or .appxbundle: 25 GB maximum per bundle<br>.msix or .appx packages targeting Windows 10 or Windows 11: 25 GB maximum per package |
| Block map hashes     | SHA2-256 algorithm                                                                                                                            
### Supported versions

For UWP apps, all packages must target a version of Windows 10 or Windows 11 supported by the Store. The versions your package supports must be indicated in the **MinVersion** and **MaxVersionTested** attributes of the [TargetDeviceFamily](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) element of the app manifest.

### StoreManifest XML file

StoreManifest.xml is an optional configuration file that may be included in app packages. Its purpose is to enable features, such as declaring your app as a Microsoft Store device app or declaring requirements that a package depends on to be applicable to a device, that the package manifest does not cover. If used, StoreManifest.xml is submitted with the app package and must be in the root folder of your app's main project. For more info, see [StoreManifest schema](/uwp/schemas/storemanifest/store-manifest-schema-portal).

## Package version numbering

Each package you provide must have a version number (provided as a value in the **Version** attribute of the [Package/Identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in the app manifest). The Microsoft Store enforces certain rules related to version numbers, which work somewhat differently in different OS versions.

> [!NOTE]
> This topic refers to "packages", but unless noted, the same rules apply to version numbers for both .msix/.appx and .msixbundle/.appxbundle files.

### Version numbering for Windows 10 and 11 packages

> [!IMPORTANT]
> For Windows 10 or Windows 11 (UWP) packages, the last (fourth) section of the version number is reserved for Store use and must be left as 0 when you build your package (although the Store may change the value in this section). The other sections must be set to an integer between 0 and 65535 (except for the first section, which cannot be 0).

When choosing a UWP package from your published submission, the Microsoft Store will always use the highest-versioned package that is applicable to the customer’s Windows 10 or Windows 11 device. This gives you greater flexibility and puts you in control over which packages will be provided to customers on specific device types. Importantly, you can submit these packages in any order; you are not limited to providing higher-versioned packages with each subsequent submission.

You can provide multiple UWP packages with the same version number. However, packages that share a version number cannot also have the same architecture, because the full identity that the Store uses for each of your packages must be unique. For more info, see [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity).

When you provide multiple UWP packages that use the same version number, the architecture (in the order x64, x86, Arm, neutral) will be used to decide which one is of higher rank (when the Store determines which package to provide to a customer's device). When ranking app bundles that use the same version number, the highest architecture rank within the bundle is considered: an app bundle that contains an x64 package will have a higher rank than one that only contains an x86 package.

This gives you a lot of flexibility to evolve your app over time. You can upload and submit new packages that use lower version numbers to add support for Windows 10 or Windows 11 devices that you did not previously support, you can add higher-versioned packages that have stricter dependencies to take advantage of hardware or OS features, or you can add higher-versioned packages that serve as updates to some or all of your existing customer base.

The following example illustrates how version numbering can be managed to deliver the intended packages to your customers over multiple submissions.

#### Example: Moving to a single package over multiple submissions

Windows 10 enables you to write a single codebase that runs everywhere. This makes starting a new cross-platform project much easier. However, for a number of reasons, you might not want to merge existing codebases to create a single project right away.

You can use the package versioning rules to gradually move your customers to a single package for the Universal device family, while shipping a number of interim updates for specific device families (including ones that take advantage of Windows 10 APIs). The example below illustrates how the same rules are consistently applied over a series of submissions for the same app.

| Submission | Contents                                                                                                                                                                                                                                                                                        | Customer experience                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
| ---------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| 1          | - Package version: 1.1.10.0<br> - Device family: Windows.Desktop, minVersion 10.0.10240.0                                                                                                                                                                                                       | - Devices on Windows 10 and 11 Desktop build 10.0.10240.0 and above will get 1.1.10.0<br> - Other device families will not be able to purchase and install the app                                                                                                                                                                                                                                                                                                                                                       |
| 2          | - Package version: 1.1.10.0<br> - Device family: Windows.Desktop, minVersion 10.0.10240.0<br><br> - Package version: 1.0.0.0<br> - Device family: Windows.Universal, minVersion 10.0.10240.0                                                                                                    | - Devices on Windows 10 and 11 Desktop build 10.0.10240.0 and above will get 1.1.10.0<br> - Other (non-desktop) device families when they are introduced will get 1.0.0.0<br> - Desktop devices that already have the app installed will not see any update (because they already have the best available version 1.1.10.0 and are higher than 1.0.0.0)                                                                                                                                                                  |
| 3          | - Package version: 1.1.10.0<br> - Device family: Windows.Desktop, minVersion 10.0.10240.0<br><br> - Package version: 1.1.5.0<br> - Device family: Windows.Universal, minVersion 10.0.10250.0<br><br> - Package version: 1.0.0.0<br> - Device family: Windows.Universal, minVersion 10.0.10240.0 | - Devices on Windows 10 and 11 Desktop build 10.0.10240.0 and above will get 1.1.10.0<br> - Other (non-desktop) device families when introduced with build 10.0.10250.0 and above will get 1.1.5.0<br> - Other (non-desktop) device families when introduced with build >=10.0.10240.0 and < 10.010250.0 will get 1.1.0.0<br> - Desktop devices that already have the app installed will not see any update (because they already have the best available version 1.1.10.0 which is higher than both 1.1.5.0 and 1.0.0.0) |
| 4          | - Package version: 2.0.0.0<br> - Device family: Windows.Universal, minVersion 10.0.10240.0                                                                                                                                                                                                      | - All customers on all device families on Windows 10 and 11 build v10.0.10240.0 and above will get package 2.0.0.0                                                                                                                                                                                                                                                                                                                                                                                                       |

> [!NOTE]
>  In all cases, customer devices will receive the package that has the highest possible version number that they qualify for. For example, in the third submission above, all desktop devices will get v1.1.10.0, even if they have OS version 10.0.10250.0 or later and thus could also accept v1.1.5.0. Since 1.1.10.0 is the highest version number available to them, that is the package they will get.

#### Using version numbering to roll back to a previously-shipped package for new acquisitions

If you keep copies of your packages, you'll have the option to roll back your app’s package in the Store to an earlier Windows 10 package if you should discover problems with a release. This is a temporary way to limit the disruption to your customers while you take time to fix the issue.

To do this, create a new [submission](./create-app-submission.md). Remove the problematic package and upload the old package that you want to provide in the Store. Customers who have already received the package you are rolling back will still have the problematic package (since your older package will have an earlier version number). But this will stop anyone else from acquiring the problematic package, while allowing the app to still be available in the Store.

To fix the issue for the customers who have already received the problematic package, you can submit a new Windows 10 package that has a higher version number than the bad package as soon as you can. After that submission goes through the certification process, all customers will be updated to the new package, since it will have a higher version number.

