---
description: Learn how to grant package identity to an unpackaged Win32 app so that you can use modern Windows features in that app.
title: Grant package identity by packaging with external location
ms.date: 10/13/2023
ms.topic: article
keywords: windows 10, desktop, sparse, package, identity, external, location, MSIX, Win32
ms.localizationpriority: medium
ms.custom: RS5
---

# Grant package identity by packaging with external location

Many Windows features can be used by a desktop app only if that app has package identity at runtime. See [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps). If you have an existing desktop app, with its own installer, there's very little you need to change in order to benefit from package identity.

Starting in Windows 10, version 2004, you can grant package identity to an app simply by building and registering a *package with external location* with your app. Packaging with external location allows you to register a simple identity package in your existing installer without changing how or where you install your application. If you're familiar with full MSIX packaging, this is a much lighter-weight option as described below.

To build and register an identity package, follow these steps:

1. [Create a package manifest for the identity package](#create-a-package-manifest-for-the-identity-package)
2. [Build and sign the identity package](#build-and-sign-the-identity-package)
3. [Add identity metadata to your desktop application manifests](#add-identity-metadata-to-your-desktop-application-manifests)
4. [Register the identity package in your installer](#register-the-identity-package-in-your-installer)

## Create a package manifest for the identity package

The first step to creating an identity package is to create a package manifest based on the below template. This is an MSIX manifest but is only used for identity and doesn't alter the app's runtime behavior.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Package IgnorableNamespaces="uap uap10"
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap10="http://schemas.microsoft.com/appx/manifest/uap/windows10/10"
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities">
  <Identity Name="ContosoPhotoStore" Publisher="CN=Contoso" Version="1.0.0.0" ProcessorArchitecture="neutral" />
  <Properties>
    <DisplayName>Contoso PhotoStore</DisplayName>
    <PublisherDisplayName>Contoso</PublisherDisplayName>
    <Logo>Assets\storelogo.png</Logo>
    <uap10:AllowExternalContent>true</uap10:AllowExternalContent>
  </Properties>
  <Resources>
    <Resource Language="en-us" />
  </Resources>
  <Dependencies>
    <TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.19041.0" MaxVersionTested="10.0.26100.0" />
  </Dependencies>
  <Capabilities>
    <rescap:Capability Name="runFullTrust" />
    <rescap:Capability Name="unvirtualizedResources"/>
  </Capabilities>
  <Applications>
    <Application Id="ContosoPhotoStore" Executable="ContosoPhotoStore.exe" uap10:TrustLevel="mediumIL" uap10:RuntimeBehavior="win32App"> 
      <uap:VisualElements AppListEntry="none" DisplayName="Contoso PhotoStore" Description="Contoso PhotoStore App" BackgroundColor="transparent" Square150x150Logo="Assets\Square150x150Logo.png" Square44x44Logo="Assets\Square44x44Logo.png" />
    </Application>
  </Applications>
</Package>
```

Note the below important details about this manifest:

* Fill in the `Identity` element attributes with the details of your application
  * `Publisher` must match the `Subject` of the certificate used to sign the application
* Fill in the `DisplayName` and `PublisherDisplayName` elements with the details of your application
  * Unless you add additional features to the manifest beyond simple identity, these values are not displayed anywhere
* Update the `Logo` element to a relative path within your application's installation directory that will resolve to a .png, .jpg, or .jpeg image
* Ensure the `AllowExternalContent` element is set to `true` as shown which enables reusing your existing installer
* Set `TargetDeviceFamily` `MinVersion` and `MaxVersionTested` per below:
  * Set `MinVersion` to `10.0.19041.0` as shown for maximum reach and uniformity across Windows 10 and Windows 11 OS versions
  * Set `MinVersion` to `10.0.26100.0` to restrict the identity package to Windows 11, version 24H2 and above
  * Set `MaxVersionTested` to `10.0.26100.0` as shown
* Ensure the `runFullTrust` and `unvirtualizedResources` capabilities are declared as shown for Win32 compatibility
* Add an `Application` element as shown for each executable associated with your application
  * Ensure `TrustLevel` is `mediumIL` and `RuntimeBehavior` is `win32App` as shown for Win32 compatibility
* The `VisualElements` child element is required, but the `AppListEntry="none"` attribute ensures the identity package isn't shown among installed apps
  * Update the `DisplayName` and `Description` attributes with relevant details and leave the other attributes as shown (the referenced image paths do not need to resolve)

The identity package created from this manifest will be connected to your application's installation directory when you register the package in a later step.

## Build and sign the identity package

After you create your identity package manifest, build the identity package using the [MakeAppx.exe tool](/windows/msix/package/create-app-package-with-makeappx-tool) in the Windows SDK.

```Console
MakeAppx.exe pack /o /d <path to directory that contains manifest> /nv /p <output path>\MyPackage.msix
```

Note: The `/nv` flag is required to bypass validation of referenced file paths in the manifest.

In order to be installed on end user computers, the identity package must be signed with a certificate that is trusted on the target computer. You can [create a new self-signed certificate for development purposes](/windows/msix/package/create-certificate-package-signing) and sign your identity package using [SignTool](/windows/msix/package/sign-app-package-using-signtool), which is available in the Windows SDK, but a production certificate from an IT Department or a service like [Azure Trusted Signing](https://azure.microsoft.com/products/trusted-signing) will be required to register the package on end user computers.

```Console
SignTool.exe sign /fd SHA256 /a /f <path to certificate>\MyCertificate.pfx /p <certificate password> <path to package with external location>\MyPackage.msix
```

Note: For how to build and sign the identity package within a CI/CD pipeline with production certificates, see the [MSIX and CI/CD Pipeline Overview](/windows/msix/desktop/cicd-overview) for examples.

### Add identity metadata to your desktop application manifests

You connect the identity package with your application executables by including [application manifests](/windows/win32/sbscs/application-manifests) (a.k.a side-by-side or fusion manifests) with metadata that matches metadata from the identity package manifest.

In Visual Studio, you can add an [application manifest](/windows/win32/sbscs/application-manifests) to an executable project by opening the **Project** context menu, and selecting **Add** > **New Item** > **Application Manifest File**.

Below is an example application manifest snippet demonstrating the `msix` element required to connect your binaries with metadata from your identity package.

```xml
<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <assemblyIdentity version="0.0.0.0" name="ContosoPhotoStore"/>
  <msix xmlns="urn:schemas-microsoft-com:msix.v1"
          publisher="CN=Contoso"
          packageName="ContosoPhotoStore"
          applicationId="ContosoPhotoStore"
        />
</assembly>
```

The attributes of the `msix` element must match these values from the identity package manifest:

* The `packageName` and `publisher` attributes must match the `Name` and `Publisher` attributes in the [`Identity`](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in your identity package manifest, respectively
* The `applicationId` attribute must match the `Id` attribute of the corresponding [`Application`](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element in your identity package manifest

## Register the identity package in your installer

The last step to associate identity with your application is to register the identity package in your installer and associate it with your application's installation directory.

The below snippet demonstrates using the [`PackageManager.AddPackageByUriAsync`](/uwp/api/windows.management.deployment.packagemanager.addpackagebyuriasync) method to register the identity package.

```csharp
using Windows.Management.Deployment;

...

var externalUri = new Uri(externalLocation);
var packageUri = new Uri(packagePath);

var packageManager = new PackageManager();

var options = new AddPackageOptions();
options.ExternalLocationUri = externalUri;

await packageManager.AddPackageByUriAsync(packageUri, options);
```

Note the below important details about this code:

* Set `externalLocation` to the absolute path of your application's installation directory (without any executable names)
* Set `packagePath` to the absolute path of the identity package produced in the previous step (with the file name)

For a complete example including unregistering the package on uninstall, see [`StartUp.cs`](https://github.com/microsoft/AppModelSamples/blob/0c019d835d194dfc65ee0c0663086582d48165a9/Samples/SparsePackages/PhotoStoreDemo/StartUp.cs#L146-L220).

## Sample app

See the [SparsePackages](https://github.com/microsoft/AppModelSamples/tree/master/Samples/SparsePackages) sample for a fully functional sample app that demonstrates how to grant package identity to a desktop app by registering an identity package.
