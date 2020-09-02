---
Description: Learn how to grant identity to non-packaged desktop apps so you can use modern Windows 10 features in those apps.
title: Grant identity to non-packaged desktop apps
ms.date: 04/23/2020
ms.topic: article
keywords: windows 10, desktop, package, identity, MSIX, Win32
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
ms.custom: RS5
---

# Grant identity to non-packaged desktop apps

Many Windows 10 extensibility features require [package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) to be used from non-UWP desktop apps, including background tasks, notifications, live tiles, and share targets. For these scenarios, the OS requires identity so that it can identify the caller of the corresponding API.

In OS releases before Windows 10, version 2004, the only way to grant identity to a desktop app is to [package it in a signed MSIX package](/windows/msix/desktop/desktop-to-uwp-root). For these apps, identity is specified in the package manifest and identity registration is handled by the MSIX deployment pipeline based on the information in the manifest. All content referenced in the package manifest is present inside the MSIX package.

Starting in Windows 10, version 2004, you can grant package identity to desktop apps that are not packaged in an MSIX package by building and registering a *sparse package* with your app. This support enables desktop apps that are not yet able to adopt MSIX packaging for deployment to use Windows 10 extensibility features that require package identity. For more background info, see [this blog post](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/#HBMFEM843XORqOWx.97).

To build and register a sparse package that grants package identity to your desktop app, follow these steps.

1. [Create a package manifest for the sparse package](#create-a-package-manifest-for-the-sparse-package)
2. [Build and sign the sparse package](#build-and-sign-the-sparse-package)
3. [Add the package identity metadata to your desktop application manifest](#add-the-package-identity-metadata-to-your-desktop-application-manifest)
4. [Register your sparse package at run time](#register-your-sparse-package-at-run-time)

## Important concepts

The following features enable non-packaged desktop apps to acquire package identity.

### Sparse packages

A *sparse package* contains a package manifest but no other app binaries and content. The manifest of a sparse package can reference files outside the package in a predetermined external location. This allows applications that are not yet able to adopt MSIX packaging for their entire app to acquire package identity as required by some Windows 10 extensibility features.

> [!NOTE]
> A desktop app that uses a sparse package does not receive some benefits of being fully deployed via an MSIX package. These benefits include tamper protection, installation in a locked-down location, and full management by the OS at deployment, run time, and uninstall.

### Package external location

To support sparse packages, the package manifest schema now supports an optional [**uap10:AllowExternalContent**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-allowexternalcontent) element under the [**Properties**](/uwp/schemas/appxpackage/uapmanifestschema/element-properties) element. This allows your package manifest to reference content outside the package, in a specific location on disk.

For example, if you have your existing non-packaged desktop app that installs the app executable and other content in C:\Program Files\MyDesktopApp\, you can create a sparse package that includes the **uap10:AllowExternalContent** element in the manifest. During the install process for your app or the first time your apps, you can install the sparse package and declare C:\Program Files\MyDesktopApp\ as the external location your app will use.

## Create a package manifest for the sparse package

Before you can build a sparse package, you must first create a [package manifest](/uwp/schemas/appxpackage/appx-package-manifest) (a file named AppxManifest.xml) that declares package identity metadata for your desktop app and other required details. The easiest way to create a package manifest for the sparse package is to use the example below and customize it for your app by using the [schema reference](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).

Make sure the package manifest includes these items:

* An [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element that describes the identity attributes for your desktop app.
* An [**uap10:AllowExternalContent**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-allowexternalcontent) element under the [**Properties**](/uwp/schemas/appxpackage/uapmanifestschema/element-properties) element. This element should be assigned the value `true`, which allows your package manifest to reference content outside the package, in a specific location on disk. In a later step, you'll specify the path of the external location when you register your sparse package from code that runs in your installer or your app. Any content that you reference in the manifest that isn’t located in the package itself should be installed to the external location.
* The **MinVersion** attribute of the [**TargetDeviceFamily**](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) element should be set to `10.0.19000.0` or a later version.
* The **TrustLevel=mediumIL** and **RuntimeBehavior=Win32App** attributes of the [**Application**](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element declare that the desktop app associated with the sparse package will run similar to a standard unpackaged desktop app, without registry and file system virtualization and other run time changes.

The following example shows the complete contents of a sparse package manifest (AppxManifest.xml). This manifest includes a `windows.sharetarget` extension, which requires package identity.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Package 
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap2="http://schemas.microsoft.com/appx/manifest/uap/windows10/2"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  xmlns:uap10="http://schemas.microsoft.com/appx/manifest/uap/windows10/10"
  IgnorableNamespaces="uap uap2 uap3 rescap desktop uap10">
  <Identity Name="ContosoPhotoStore" ProcessorArchitecture="x64" Publisher="CN=Contoso" Version="1.0.0.0" />
  <Properties>
    <DisplayName>ContosoPhotoStore</DisplayName>
    <PublisherDisplayName>Contoso</PublisherDisplayName>
    <Logo>Assets\storelogo.png</Logo>
    <uap10:AllowExternalContent>true</uap10:AllowExternalContent>
  </Properties>
  <Resources>
    <Resource Language="en-us" />
  </Resources>
  <Dependencies>
    <TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.19000.0" MaxVersionTested="10.0.19000.0" />
  </Dependencies>
  <Capabilities>
    <rescap:Capability Name="runFullTrust" />
    <rescap:Capability Name="unvirtualizedResources"/>
  </Capabilities>
  <Applications>
    <Application Id="ContosoPhotoStore" Executable="ContosoPhotoStore.exe" uap10:TrustLevel="mediumIL" uap10:RuntimeBehavior="win32App"> 
      <uap:VisualElements AppListEntry="none" DisplayName="Contoso PhotoStore" Description="Demonstrate photo app" BackgroundColor="transparent" Square150x150Logo="Assets\Square150x150Logo.png" Square44x44Logo="Assets\Square44x44Logo.png">
        <uap:DefaultTile Wide310x150Logo="Assets\Wide310x150Logo.png" Square310x310Logo="Assets\LargeTile.png" Square71x71Logo="Assets\SmallTile.png"></uap:DefaultTile>
        <uap:SplashScreen Image="Assets\SplashScreen.png" />
      </uap:VisualElements>
      <Extensions>
        <uap:Extension Category="windows.shareTarget">
          <uap:ShareTarget Description="Send to ContosoPhotoStore">
            <uap:SupportedFileTypes>
              <uap:FileType>.jpg</uap:FileType>
              <uap:FileType>.png</uap:FileType>
              <uap:FileType>.gif</uap:FileType>
            </uap:SupportedFileTypes>
            <uap:DataFormat>StorageItems</uap:DataFormat>
            <uap:DataFormat>Bitmap</uap:DataFormat>
          </uap:ShareTarget>
        </uap:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

## Build and sign the sparse package

After you create your package manifest, build the sparse package by using the [MakeAppx.exe tool](/windows/msix/package/create-app-package-with-makeappx-tool) in the Windows SDK. Because the sparse package doesn’t contain the files referenced in the manifest, you must specify the `/nv` option, which skips semantic validation for the package.

The following example demonstrates how to create a sparse package from the command line.  

```Console
MakeAppx.exe pack /d <path to directory that contains manifest> /p <output path>\MyPackage.msix /nv
```

Before your sparse package can be successfully installed on a target computer, you must sign it with a certificate that is trusted on the target computer. You can create a new self-signed certificate for development purposes and sign your sparse package using [SignTool](/windows/msix/package/sign-app-package-using-signtool), which is available in the Windows SDK.

The following example demonstrates how to sign a sparse package from the command line.

```Console
SignTool.exe sign /fd SHA256 /a /f <path to certificate>\MyCertificate.pfx /p <certificate password> <path to sparse package>\MyPackage.msix
```

### Add the package identity metadata to your desktop application manifest

You must also include a [side-by-side application manifest](/windows/win32/sbscs/application-manifests) with your desktop app and include an [**msix**](/windows/win32/sbscs/application-manifests#msix) element with attributes that declare the identity attributes of your app. The values of these attributes are used by the OS to determine your app's identity when the executable is launched.

The following example shows a side-by-side application manifest with an **msix** element.

```xml
<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <assemblyIdentity version="1.0.0.0" name="Contoso.PhotoStoreApp"/>
  <msix xmlns="urn:schemas-microsoft-com:msix.v1"
          publisher="CN=Contoso"
          packageName="ContosoPhotoStore"
          applicationId="ContosoPhotoStore"
        />
</assembly>
```

The attributes of the **msix** element must match these values in the package manifest for your sparse package:

* The **packageName** and **publisher** attributes must match the **Name** and **Publisher** attributes in the [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in your package manifest, respectively.
* The **applicationId** attribute must match the **Id** attribute of the [**Application**](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element in your package manifest.

The side-by-side application manifest must exist in the same directory as the executable file for your desktop app, and by convention it should have the same name as your app's executable file with the `.manifest` extension appended to it. For example, if your app's executable name is `ContosoPhotoStore`, the application manifest filename should be `ContosoPhotoStore.exe.manifest`.

## Register your sparse package at run time

To grant package identity to your desktop app, your app must register the sparse package by using the [**AddPackageByUriAsync**](/uwp/api/windows.management.deployment.packagemanager.addpackagebyuriasync) method of the [**PackageManager**](/uwp/api/windows.management.deployment.packagemanager) class. This method is available starting in Windows 10, version 2004. You can add code to your app to register the sparse package when your app is run for the first time, or you can run code to register the package while your desktop app is installed (for example, if you're using MSI to install your desktop app, you can run this code from a custom action).

The following example demonstrates how to register a sparse package. This code creates an [**AddPackageOptions**](/uwp/api/windows.management.deployment.addpackageoptions) object that contains the path to the external location where your package manifest can reference content outside the package. Then, the code passes this object to the **AddPackageByUriAsync**  method to register the sparse package. This method also receives the location of your signed sparse package as a URI. For a more complete example, see the `StartUp.cs` code file in the related [sample](#sample).

```csharp
private static bool registerSparsePackage(string externalLocation, string sparsePkgPath)
{
    bool registration = false;
    try
    {
        Uri externalUri = new Uri(externalLocation);
        Uri packageUri = new Uri(sparsePkgPath);

        Console.WriteLine("exe Location {0}", externalLocation);
        Console.WriteLine("msix Address {0}", sparsePkgPath);

        Console.WriteLine("  exe Uri {0}", externalUri);
        Console.WriteLine("  msix Uri {0}", packageUri);

        PackageManager packageManager = new PackageManager();

        // Declare use of an external location
        var options = new AddPackageOptions();
        options.ExternalLocationUri = externalUri;

        Windows.Foundation.IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation = packageManager.AddPackageByUriAsync(packageUri, options);

        // Other progress and error-handling code omitted for brevity...
    }
}
```

## Sample

See the [SparesePackages](https://github.com/microsoft/AppModelSamples/tree/master/Samples/SparsePackages) sample for a fully functional sample app that demonstrates how to grant package identity to a desktop app using a sparse package. More information about building and running the sample is provided in [this blog post](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/#HBMFEM843XORqOWx.97).

This sample includes the following:

* The source code for a WPF app named PhotoStoreDemo. During startup, the app checks to see whether it is running with identity. If it is not running with identity, it registers the sparse package and then restarts the app. See `StartUp.cs` for the code that performs these steps.
* A side-by-side application manifest named `PhotoStoreDemo.exe.manifest`.
* A package manifest named `AppxManifest.xml`.