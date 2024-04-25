---
description: Discover how to integrate unpackaged apps with the Windows Share.
title: Integrate unpackaged apps with Windows Share
ms.topic: article
ms.date: 04/16/2024
ms.localizationpriority: medium
---

# Integrate unpackaged apps with Windows Share

The Windows Share Sheet is a system-provided UI that enables users to share content from your app with other apps. The Share Sheet is available in the Windows shell and is accessible from any app that supports sharing. It provides a consistent and familiar experience for users, and it's a great way to increase the discoverability of your app.

How to onboard an unpackaged app as a Share Target:

- Provide the app with package identity
- Implement the Share contract

## Provide unpackaged apps package identity

An app can get package identity in two ways:  

- Make a new MSIX installation package (preferred method) **OR**
- Make apps packaged with external location compatible with the current installer. This is only recommended for apps that have a existing installer and which can't switch to MSIX installation.

### Make a new MSIX installation package

It's recommended to package the app with MSIX using the **Windows Application Packaging Project** template in Visual Studio. This will include all the binaries in the MSIX package and provide a clean and trusted install experience.

Things to note before packaging desktop apps: [Prepare to package a desktop application (MSIX)](/windows/msix/desktop/desktop-to-uwp-prepare).

Follow the steps in [Set up your desktop application for MSIX packaging in Visual Studio](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) to generate a package for your existing app's project.

> [!NOTE]
> When creating the packaging project, select **Windows 10, version 2004 (10.0; Build 19041)** or later as the **Minimum version**.

When that is completed, you will create the package by following [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps).

### Make packaging with external location compatible with the current installer

The second way to give your app package identity is to add a package with external location to your application and register it with your existing installer. The package with external location is an empty MSIX package that contains the .appxmanifest having identity, share target registration, and visual assets. The app's binaries are still managed by app's existing installer. When registering the package, you need to provide the app's install location in the API. It is important to keep the identity in the MSIX package manifest and the Win32 app manifest in sync with the certificate used for signing the app.

#### Steps to grant package identity to an unpackaged app

In this walkthrough on package identity, registration & share activation for unpackaged Win32 Applications, you will learn how to grant package identity to an unpackaged Win32 application by creating a package with external location. You will take the following steps using the PhotoStoreDemo sample:

- Generate the `AppxManifest.xml` file
- Create a package
- Sign the package
- Register the package
- Handle app activation

Start by creating the `AppxManifest.xml` file, which includes necessary properties like `<AllowExternalContent>`, identity, and capabilities & share target extension. Make sure the `Publisher`, `PackageName` & `ApplicationId` values in the `AppxManifest.xml` file match the values in the `PhotoStoreDemo.exe.manifest` file. The `Publisher` value should also match the value in the certificate used to sign the package. Add visual assets as required and referenced in `AppxManifest.xml`. In Visual Studio, you can use the **Visual Assets** node when editing `package.manifest` in the Application Packaging project to generate required the visual assets.

This is a sample `AppxManifest.xml` snippet with external content allowed:

```xml
<Identity Name="PhotoStoreDemo" ProcessorArchitecture="neutral" Publisher="CN=YourPubNameHere" Version="1.0.0.0" />
  <Properties>
    <DisplayName>PhotoStoreDemo</DisplayName>
    <PublisherDisplayName>Sparse Package</PublisherDisplayName>
    <Logo>Assets\storelogo.png</Logo>
    <uap10:AllowExternalContent>true</uap10:AllowExternalContent>
  </Properties>
  <Resources>
    <Resource Language="en-us" />
  </Resources>
  <Dependencies>
    <TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.18950.0" MaxVersionTested="10.0.19000.0" />
  </Dependencies>
  <Capabilities>
    <rescap:Capability Name="runFullTrust" />
    <rescap:Capability Name="unvirtualizedResources"/>
  </Capabilities>
  <Applications>
    <Application Id="PhotoStoreDemo" Executable="PhotoStoreDemo.exe" uap10:TrustLevel="mediumIL" uap10:RuntimeBehavior="win32App">
      <uap:VisualElements AppListEntry="none" DisplayName="PhotoStoreDemo" Description="PhotoStoreDemo" BackgroundColor="transparent" Square150x150Logo="Assets\Square150x150Logo.png" Square44x44Logo="Assets\Square44x44Logo.png">
        <uap:DefaultTile Wide310x150Logo="Assets\Wide310x150Logo.png" Square310x310Logo="Assets\LargeTile.png" Square71x71Logo="Assets\SmallTile.png"></uap:DefaultTile>
        <uap:SplashScreen Image="Assets\SplashScreen.png" />
      </uap:VisualElements>
      <Extensions>
        <uap:Extension Category="windows.shareTarget">
          <uap:ShareTarget Description="Send to PhotoStoreDemo">
            <uap:SupportedFileTypes>
              <uap:FileType>.jpg</uap:FileType>
              <uap:FileType>.png</uap:FileType>
              <uap:FileType>.gif</uap:FileType>
            </uap:SupportedFileTypes>
            <uap:DataFormat>StorageItems</uap:DataFormat>
            <uap:DataFormat>Bitmap</uap:DataFormat>
          </uap:ShareTarget>
        </uap:Extension>
        ...
```

This is a sample Application.exe.manifest file:

```xml
﻿<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <assemblyIdentity version="1.0.0.0" name="PhotoStoreDemo.app"/>
  <msix xmlns="urn:schemas-microsoft-com:msix.v1"
          publisher="CN=YourPubNameHere"
          packageName="PhotoStoreDemo"
          applicationId="PhotoStoreDemo"
        />
</assembly>
```

Next, to create the package with external location, use the `MakeAppx.exe` tool with the `/nv` command to create a package containing the `AppxManifest.xml` file.​

Example:

```Console
MakeAppx.exe pack /d <Path to directory with AppxManifest.xml> /p <Output Path>\mypackage.msix /nv
```

> [!NOTE]
> A package with external location contains a package manifest, but no other app binaries and content. The manifest of a package with external location can reference files outside the package in a predetermined external location.

Sign your package with a trusted certificate using `SignTool.exe`.​

Example:

```Console
SignTool.exe sign /fd SHA256 /a /f <path to cert>  /p <cert key> <Path to Package>​
```

The cert used for signing the package should be installed in a trusted location on the machine.

On first run of the application, register the package with Windows. When an app has its own installer, it should also contain the signed MSIX as the payload and should place it to a specified location (for example, the app's install location). This location must be known to the app at runtime because the app will need the absolute path of MSIX to register it. Place the assets and `resources.pri` in the app's install location as well.

The following code is an example of unpackaged execution of the app's Main method:

```csharp
[STAThread]
public static void Main(string[] cmdArgs)
{
    //if app isn't running with identity, register its package with external identity
    if (!ExecutionMode.IsRunningWithIdentity())
    {
        //TODO - update the value of externalLocation to match the output location of your VS Build binaries and the value of 
        //externalPkgPath to match the path to your signed package with external identity (.msix). 
        //Note that these values cannot be relative paths and must be complete paths
        string externalLocation = Environment.CurrentDirectory;
        string externalPkgPath = externalLocation + @"\PhotoStoreDemo.package.msix";

        //Attempt registration
        bool bPackageRegistered = false;
        //bPackageRegistered = registerPackageWithExternalLocation(externalLocation, externalPkgPath);
        if (bPackageRegistered)
        {
            //Registration succeeded, restart the app to run with identity
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location, arguments: cmdArgs?.ToString());
        }
        else //Registration failed, run without identity
        {
            Debug.WriteLine("Package Registration failed, running WITHOUT Identity");
            SingleInstanceManager wrapper = new SingleInstanceManager();
            wrapper.Run(cmdArgs);
        }
    }
    ...
```

This example shows how to register the MSIX on first run of the app:

```csharp
[STAThread]
public static void Main(string[] cmdArgs)
{
    //If app isn't running with identity, register its package with external identity
    if (!ExecutionMode.IsRunningWithIdentity())
    {
        //TODO - update the value of externalLocation to match the output location of your VS Build binaries and the value of 
        //externalPkgPath to match the path to your signed package with external identity (.msix). 
        //Note that these values cannot be relative paths and must be complete paths
        string externalLocation = Environment.CurrentDirectory;
        string externalPkgPath = externalLocation + @"\PhotoStoreDemo.package.msix";

        //Attempt registration
        if (registerPackageWithExternalLocation(externalLocation, externalPkgPath))
        {
            //Registration succeeded, restart the app to run with identity
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location, arguments: cmdArgs?.ToString());
        }
        else //Registration failed, run without identity
        {
            Debug.WriteLine("Package Registration failed, running WITHOUT Identity");
            SingleInstanceManager wrapper = new SingleInstanceManager();
            wrapper.Run(cmdArgs);
        }
    }
    ...
```

Finally, handle the app's activation:

```csharp
[STAThread]
public static void Main(string[] cmdArgs)
{
    //if app isn't running with identity, register its sparse package
    if (!ExecutionMode.IsRunningWithIdentity())
    {
        ...
    }
    else //App is registered and running with identity, handle launch and activation
    {
        //Handle Sparse Package based activation e.g Share target activation or clicking on a Tile
        // Launching the .exe directly will have activationArgs == null
        var activationArgs = AppInstance.GetActivatedEventArgs();
        if (activationArgs != null)
        {
            switch (activationArgs.Kind)
            {
                case ActivationKind.Launch:
                    HandleLaunch(activationArgs as LaunchActivatedEventArgs);
                    break;
                case ActivationKind.ToastNotification:
                    HandleToastNotification(activationArgs as ToastNotificationActivatedEventArgs);
                    break;
                case ActivationKind.ShareTarget: // Handle the activation as a share target
                    HandleShareAsync(activationArgs as ShareTargetActivatedEventArgs);
                    break;
                default:
                    HandleLaunch(null);
                    break;
            }

        }
        //This is a direct exe based launch e.g. double click app .exe or desktop shortcut pointing to .exe
        else
        {
            SingleInstanceManager singleInstanceManager = new SingleInstanceManager();
            singleInstanceManager.Run(cmdArgs);
        }
    }
```

Additional documentation on how to create a package with external location is available here, including information on templates to use: [Grant package identity by packaging with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).

The full sample app is available on GitHub: [SparsePackages (Packaged with External Location)](https://github.com/microsoft/AppModelSamples/tree/master/Samples/SparsePackages).

## Register as a Share Target

Once the app has package identity, the next step is to implement the Share contract. The Share contract allows your app to receive data from another app.

You can follow the same steps in the [Register as a Share Target](integrate-sharesheet-packaged.md#register-as-a-share-target) section of the documentation for packaged apps to integrate with Share Sheet.

## See also

- [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview)
- [Create your first WinUI 3 project](/windows/apps/winui/winui3/create-your-first-winui3-app)
- [Migrate from UWP to the Windows App SDK](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw)
- [Advantages and Disadvantages of packaging an application - Deployment overview](/windows/apps/package-and-deploy/#advantages-and-disadvantages-of-packaging-your-app)
- [Identity, Registration and Activation of Non-packaged Win32 Apps](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/)
- [Share Contract Implementation for WinAppSDK App](https://github.com/kmahone/WindowsAppSDK-Samples/tree/user/kmahone/shareapp/Samples/AppLifecycle/ShareTarget/WinUI-CS-ShareTargetSampleApp)
- [Share Contract Implementation for Apps Packaged with External Location](https://github.com/microsoft/AppModelSamples/tree/master/Samples/SparsePackages)
