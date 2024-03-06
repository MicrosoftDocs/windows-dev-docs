Follow these guidelines to prepare your app's packages for submission to the Microsoft Store.

## Before you build your app's package for the Microsoft Store

Make sure to [test your app with the Windows App Certification Kit](/windows/uwp/debug-test-perf/windows-app-certification-kit). We also recommend that you test your app on different types of hardware. Note that until we certify your app and make it available from the Microsoft Store, it can only be installed and run on computers that have developer licenses.

## Building the app package using Microsoft Visual Studio

If you're using Microsoft Visual Studio as your development environment, you already have built-in tools that make creating an app package a quick and easy process. For more info, see [Packaging apps](/windows/uwp/packaging/).

> [!NOTE]
> Be sure that all your filenames use ANSI.

When you create your package in Visual Studio, make sure you are signed in with the same account associated with your developer account. Some parts of the package manifest have specific details related to your account. This info is detected and added automatically. Without the additional information added to the manifest, you may encounter package upload failures.

When you build your app's UWP packages, Visual Studio can create an .msix or appx file, or a .msixupload or .appxupload file. For UWP apps, we recommend that you always upload the .msixupload or .appxupload file in the [Packages](../../../apps/publish/publish-your-app/upload-app-packages.md) page. For more info about packaging UWP apps for the Store, see [Package a UWP app with Visual Studio](/windows/msix/package/packaging-uwp-apps).

Your app's packages don't have to be signed with a certificate rooted in a trusted certificate authority.

### App bundles

For UWP apps, Visual Studio can generate an app bundle (.msixbundle or .appxbundle) to reduce the size of the app that users download. This can be helpful if you've defined language-specific assets, a variety of image-scale assets, or resources that apply to specific versions of Microsoft DirectX.

> [!NOTE]
> One app bundle can contain your packages for all architectures.

With an app bundle, a user will only download the relevant files, rather than all possible resources. For more info about app bundles, see [Packaging apps](/windows/uwp/packaging/) and [Package a UWP app with Visual Studio](/windows/msix/package/packaging-uwp-apps).

## Building the app package manually

If you don't use Visual Studio to create your package, you must [create your package manifest manually](/uwp/schemas/appxpackage/how-to-create-a-package-manifest-manually).

Be sure to review the [App package manifest](/uwp/schemas/appxpackage/appx-package-manifest) documentation for complete manifest details and requirements. Your manifest must follow the package manifest schema in order to pass certification.

Your manifest must include some specific info about your account and your app. You can find this info by looking at [View app identity details](../../../apps/publish/view-app-identity-details.md) in the **Product management** section of your app's overview page in the dashboard.

> [!NOTE]
> Values in the manifest are case-sensitive. Spaces and other punctuation must also match. Enter the values carefully and review them to ensure that they are correct.

App bundles (.msixbundle or .appxbundle) use a different manifest. Review the [Bundle manifest](/uwp/schemas/bundlemanifestschema/bundle-manifest) documentation for the details and requirements for app bundle manifests. Note that in a .msixbundle or .appxbundle, the manifest of each included package must use the same elements and attributes, except for the **ProcessorArchitecture** attribute of the [Identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element.

> [!TIP]
> Be sure to run the [Windows App Certification Kit](/windows/uwp/debug-test-perf/windows-app-certification-kit) before you submit your packages. This can you help determine if your manifest has any problems that might cause certification or submission failures.

## Package format requirements

Your app’s packages must comply with these requirements.

| App package property | Requirement        |
|----------------------|--------------------|
| Package size         | .msixbundle or .appxbundle: 25 GB maximum per bundle<br>.msix or .appx packages targeting Windows 10 or Windows 11: 25 GB maximum per package |
| Block map hashes     | SHA2-256 algorithm |

> [!IMPORTANT]
> You can no longer upload new XAP packages built using the Windows Phone 8.x SDK(s). Apps that are already in Store with XAP packages will continue to work on Windows 10 Mobile devices. For more info, see this [blog post](https://blogs.windows.com/windowsdeveloper/2018/08/20/important-dates-regarding-apps-with-windows-phone-8-x-and-earlier-and-windows-8-8-1-packages-submitted-to-microsoft-store).

## Supported versions

For UWP apps, all packages must target a version of Windows 10 or Windows 11 supported by the Store. The versions your package supports must be indicated in the **MinVersion** and **MaxVersionTested** attributes of the [TargetDeviceFamily](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) element of the app manifest.

The versions currently supported range from:

- Minimum: 10.0.10240.0
- Maximum: 10.0.22621.0

## StoreManifest XML file

StoreManifest.xml is an optional configuration file that may be included in app packages. Its purpose is to enable features, such as declaring your app as a Microsoft Store device app or declaring requirements that a package depends on to be applicable to a device, that the package manifest does not cover. If used, StoreManifest.xml is submitted with the app package and must be in the root folder of your app's main project. For more info, see [StoreManifest schema](/uwp/schemas/storemanifest/store-manifest-schema-portal).
