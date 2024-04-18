---
description: Discover how to integrate unpackaged apps with the Windows Share Sheet.
title: Integrate unpackaged apps with Windows Share Sheet
ms.topic: article
ms.date: 04/16/2024
ms.localizationpriority: medium
---

# Integrate unpackaged apps with Windows Share Sheet

The Windows Share Sheet is a system-provided UI that enables users to share content from your app with other apps. The Share Sheet is available in the Windows shell and is accessible from any app that supports sharing. The Share Sheet provides a consistent and familiar experience for users, and it's a great way to increase the discoverability of your app.

How to onboard an unpackaged app as a Share Target:

- Provide the app with package identity
- Implement the Share contract

## Provide unpackaged apps package identity

An app can get package identity in two ways:  

- Make a new MSIX installation package (preferred method) **OR**
- Make Sparse Packaging compatible with the current installer. This is only recommended for apps that have a existing installer and switch to MSIX installation.

### Make a new MSIX installation package

It's recommended to package the app with MSIX using the **Windows Application Packaging Project** template in Visual Studio. This will include all the binaries in the MSIX package and provide a clean and trusted install experience.

Things to note before packaging desktop apps: [Prepare to package a desktop application (MSIX)](/windows/msix/desktop/desktop-to-uwp-prepare).

Follow the steps in [Set up your desktop application for MSIX packaging in Visual Studio](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) to generate a package for your existing app's project.

> [!NOTE]
> When creating the packaging project, select **Windows 10, version 2004 (10.0; Build 19041)** or later as the **Minimum version**.

When that is completed, you will create the package by following [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps).

### Make Sparse Packaging compatible with the current installer

The second way to give your app package identity is to add a sparse package to your application and register it with your existing installer. The sparse package is an empty MSIX package that contains the .appxmanifest having identity, share target registration, and visual assets. The app's binaries are still managed by app's existing installer. When registering the package, you need to provide the app's install location in the API. It is important to keep the identity in the MSIX package manifest and the Win32 app manifest in sync with the certificate used for signing the app.

Documentation on how to create a sparse package is available here, including information on templates to use: [Grant package identity by packaging with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).

A sample app is available on GitHub: [SparsePackages](https://github.com/microsoft/AppModelSamples/tree/master/Samples/SparsePackages).

## Register as a Share Target

Once the app has package identity, the next step is to implement the Share contract. The Share contract allows your app to receive data from another app. The Share contract is a system-provided UI that enables users to share content from your app with other apps.

You can follow the same steps in the [Register as a Share Target](integrate-sharesheet-packaged.md#register-as-a-share-target) section of the documentation for packaged apps to integrate with Share Sheet.

## See also

- [Windows App SDK deployment overview](/windows/apps/package-and-deploy/deploy-overview)
- [Create your first WinUI 3 project](/windows/apps/winui/winui3/create-your-first-winui3-app)
- [Migrate from UWP to the Windows App SDK](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw)
- [Advantages and Disadvantages of packaging an application - Deployment overview](/windows/apps/package-and-deploy/#advantages-and-disadvantages-of-packaging-your-app)
- [Identity, Registration and Activation of Non-packaged Win32 Apps](https://blogs.windows.com/windowsdeveloper/2019/10/29/identity-registration-and-activation-of-non-packaged-win32-apps/)
- [Share Contract Implementation for WinAppSDK App](https://github.com/kmahone/WindowsAppSDK-Samples/tree/user/kmahone/shareapp/Samples/AppLifecycle/ShareTarget/WinUI-CS-ShareTargetSampleApp)
- [Share Contract Implementation for Sparse Packaged based Apps](https://github.com/microsoft/AppModelSamples/blob/master/Samples/SparsePackages/PhotoStoreDemo/StartUp.cs)
