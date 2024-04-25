---
description: Learn how to grant package identity to an unpackaged app so that you can use modern Windows features in that app.
title: Grant package identity by packaging with external location
ms.date: 10/13/2023
ms.topic: article
keywords: windows 10, desktop, package, identity, MSIX, Win32
ms.localizationpriority: medium
ms.custom: RS5
---

# Grant package identity by packaging with external location

If you have an existing desktop app, with its own installer, there's very little you need to change in order to benefit from [package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity).

Many Windows extensibility features&mdash;including background tasks, notifications, live tiles, custom context menu extensions, and share targets&mdash;can be used by a desktop app only if that app has package identity at runtime. That's because the operating system (OS) needs to be able to identify the caller of the corresponding API. See [Features that require package identity](./modernize-packaged-apps.md).

Only packaged apps have package identity at runtime. For definitions of apps that are packaged, unpackaged, and packaged with external location, see [Deployment overview](../../package-and-deploy/index.md).

* In Windows 10, version 2004, and earlier the only way to grant package identity to an app is to package it in a signed MSIX package (see [Building an MSIX package from your code](/windows/msix/desktop/source-code-overview)). In that case, identity is specified in the package manifest, and identity registration is handled by the MSIX deployment pipeline based on the information in the manifest. All content referenced in the package manifest is present inside the MSIX package.
* But starting in Windows 10, version 2004, you can grant package identity to an app simply by building and registering a *package with external location* with your app. Doing so turns it into a packaged app; specifically, *a packaged app with external location*. That's because some desktop apps aren't yet ready for all of their content to be present inside an MSIX package. So this support enables such apps to have package identity; thereby being able to use Windows extensibility features that require package identity. For more background info, see the blog post [Identity, Registration and Activation of Non-packaged Win32 Apps](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/).

To build and register a package with external location (which grants package identity to your app), follow these steps.

1. [Create a package manifest for the package with external location](#create-a-package-manifest-for-the-package-with-external-location)
2. [Build and sign the package with external location](#build-and-sign-the-package-with-external-location)
3. [Add the package identity metadata to your desktop application manifest](#add-the-package-identity-metadata-to-your-desktop-application-manifest)
4. [Register your package with external location at run time](#register-your-package-with-external-location-at-run-time)

## Important concepts

The following features enable unpackaged desktop apps to acquire package identity.

### Package with external location

A *package with external location* contains a package manifest, but no other app binaries and content. The manifest of a package with external location can reference files outside the package in a predetermined external location. As mentioned above, this support enables apps that aren't yet ready for all of their content to be present inside an MSIX package to use Windows extensibility features that require package identity.

> [!NOTE]
> A desktop app that uses a package with external location doesn't receive some benefits of being fully deployed via an MSIX package. These benefits include tamper protection, installation in a locked-down location, and full management by the OS at deployment, run time, and uninstall.

### Allowing external content

To support packages with external location, the package manifest schema now supports an optional [**uap10:AllowExternalContent**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-allowexternalcontent) element under the [**Properties**](/uwp/schemas/appxpackage/uapmanifestschema/element-properties) element. This allows your package manifest to reference content outside the package, in a specific location on disk.

For example, if you have your existing unpackaged desktop app that installs the app executable and other content in C:\Program Files\MyDesktopApp\, you can create a package with external location that includes the **uap10:AllowExternalContent** element in the manifest. During the install process for your app, or the first time your app runs, you can install the package with external location and declare C:\Program Files\MyDesktopApp\ as the external location your app will use.

## Create a package manifest for the package with external location

Before you can build a package with external location, you must first create a [package manifest](/uwp/schemas/appxpackage/appx-package-manifest) (a file named AppxManifest.xml) that declares package identity metadata for your desktop app and other required details. The easiest way to create a package manifest for the package with external location is to use the example below and customize it for your app by using the [schema reference](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).

Make sure the package manifest includes these items:

* An [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element that describes the identity attributes for your desktop app.
* An [**uap10:AllowExternalContent**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-allowexternalcontent) element under the [**Properties**](/uwp/schemas/appxpackage/uapmanifestschema/element-properties) element. This element should be assigned the value `true`, which allows your package manifest to reference content outside the package, in a specific location on disk. In a later step, you'll specify the path of the external location when you register your package with external location from code that runs in your installer or your app. Any content that you reference in the manifest that isn’t located in the package itself should be installed to the external location.
* The **MinVersion** attribute of the [**TargetDeviceFamily**](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) element should be set to `10.0.19000.0` or a later version.
* The **TrustLevel=mediumIL** and **RuntimeBehavior=Win32App** attributes of the [**Application**](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element declare that the desktop app associated with the package with external location will run similar to a standard unpackaged desktop app, without registry and file system virtualization and other run time changes.

The following example shows the complete contents of a package with external location manifest (`AppxManifest.xml`). This manifest includes a `windows.sharetarget` extension, which requires package identity.

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

## Build and sign the package with external location

After you create your package manifest, build the package with external location by using the [MakeAppx.exe tool](/windows/msix/package/create-app-package-with-makeappx-tool) in the Windows SDK. Because the package with external location doesn’t contain the files referenced in the manifest, you must specify the `/nv` option, which skips semantic validation for the package.

The following example demonstrates how to create a package with external location from the command line.  

```Console
MakeAppx.exe pack /d <path to directory that contains manifest> /p <output path>\MyPackage.msix /nv
```

Before your package with external location can be successfully installed on a target computer, you must sign it with a certificate that is trusted on the target computer. You can create a new self-signed certificate for development purposes and sign your package with external location using [SignTool](/windows/msix/package/sign-app-package-using-signtool), which is available in the Windows SDK.

The following example demonstrates how to sign a package with external location from the command line.

```Console
SignTool.exe sign /fd SHA256 /a /f <path to certificate>\MyCertificate.pfx /p <certificate password> <path to package with external location>\MyPackage.msix
```

### Add the package identity metadata to your desktop application manifest

You must also include a side-by-side application manifest with your desktop app. See [Application manifests](/windows/win32/sbscs/application-manifests) (it's the file that declares things like DPI awareness, and is embedded into your app's `.exe` during build). In that file, include an [**msix**](/windows/win32/sbscs/application-manifests#msix) element with attributes that declare the identity attributes of your app. The values of these attributes are used by the OS to determine your app's identity when the executable is launched.

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

The attributes of the **msix** element must match these values in the package manifest for your package with external location:

* The **packageName** and **publisher** attributes must match the **Name** and **Publisher** attributes in the [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in your package manifest, respectively.
* The **applicationId** attribute must match the **Id** attribute of the [**Application**](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element in your package manifest.

## Register your package with external location at run time

To grant package identity to your desktop app, your app must register the package with external location by using the [**AddPackageByUriAsync**](/uwp/api/windows.management.deployment.packagemanager.addpackagebyuriasync) method of the [**PackageManager**](/uwp/api/windows.management.deployment.packagemanager) class. This method is available starting in Windows 10, version 2004. You can add code to your app to register the package with external location when your app is run for the first time, or you can run code to register the package while your desktop app is installed (for example, if you're using MSI to install your desktop app, you can run this code from a custom action).

The following example demonstrates how to register a package with external location. This code creates an [**AddPackageOptions**](/uwp/api/windows.management.deployment.addpackageoptions) object that contains the path to the external location where your package manifest can reference content outside the package. Then, the code passes this object to the **AddPackageByUriAsync**  method to register the package with external location. This method also receives the location of your signed package with external location as a URI. For a more complete example, see the `StartUp.cs` code file in the related sample app (see the [Sample app](#sample-app) section in this topic).

```csharp
private static bool registerPackageWithExternalLocation(string externalLocation, string pkgPath)
{
    bool registration = false;
    try
    {
        Uri externalUri = new Uri(externalLocation);
        Uri packageUri = new Uri(pkgPath);

        Console.WriteLine("exe Location {0}", externalLocation);
        Console.WriteLine("msix Address {0}", pkgPath);

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

## Sample app

See the [SparsePackages](https://github.com/microsoft/AppModelSamples/tree/master/Samples/SparsePackages) sample for a fully functional sample app that demonstrates how to grant package identity to a desktop app using a package with external location. More information about building and running the sample is provided in the blog post [Identity, Registration and Activation of Non-packaged Win32 Apps](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/).

This sample includes the following:

* The source code for a WPF app named PhotoStoreDemo. During startup, the app checks to see whether it is running with identity. If it is not running with identity, it registers the package with external location, and then restarts the app. See `StartUp.cs` for the code that performs these steps.
* A side-by-side application manifest named `PhotoStoreDemo.exe.manifest`.
* A package manifest named `AppxManifest.xml`.
