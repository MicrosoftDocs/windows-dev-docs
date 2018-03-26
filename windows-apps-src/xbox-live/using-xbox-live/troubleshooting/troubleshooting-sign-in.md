---
title: Troubleshooting Xbox Live sign-in
author: KevinAsgari
description: Learn how to troubleshoot issues with Xbox Live sign-in.
ms.assetid: 87b70b4c-c9c1-48ba-bdea-b922b0236da4
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, sign-in, troubleshoot
ms.localizationpriority: low
---
# Troubleshooting Xbox Live sign-in

There are several issues that can cause difficulty signing-in.  If you follow the steps in [Get started with Visual Studio for UWP games](../../get-started-with-partner/get-started-with-visual-studio-and-uwp.md) you can minimize the chance to any unexpected errors.

## Common Issues

### Sandbox Problems

Generally speaking, you should familiarize yourself with the concept of Sandboxes and how they pertain to Xbox Live.  You can see more information in the [Xbox Live Sandboxes](../../xbox-live-sandboxes.md) guide.

Briefly, sandboxes enforce content isolation and access control before retail release.  Users without access to your development sandbox cannot perform any read or write operations that pertain to your title.  You can also publish variations of service config to different sandboxes for testing.

Some things to watch out for with regards to sandboxes are discussed below.

#### Developer account doesn't have access to the right sandbox for run-time access

* A test account (also known as development account) must be used for sign-in to a title that is in development.  Make sure you are attempting to sign-in with a test account.  These are created on XDP at [https://xdp.xboxlive.com/User/Contact/MyAccess?selectedMenu=devaccounts](https://xdp.xboxlive.com/User/Contact/MyAccess?selectedMenu=devaccounts)
* Ensure that the XDP account has access to the sandbox your title is published to.  The test accounts you create in XDP inherit the permissions of the XDP account that created them

#### Your device is not on the correct sandbox

The device you are developing on must be set to a development sandbox.  On Xbox One you can set your sandbox using *Xbox One Manager*.  For Windows 10 Desktop, you can use the SwitchSandbox.cmd script that's located in the Tools directory of the Xbox Live SDK installation.

#### Your title's service configuration is not published to the correct development sandbox

Ensure your title's service configuration is published in a development sandbox.  You cannot sign-in to Xbox Live in a given development sandbox for a title, unless that title is published to the same sandbox.  Please see the [XDP documentation](https://developer.xboxlive.com/en-us/xdphelp/development/xdpdocs/Pages/setting_up_service_configuration_03_31_16.aspx#PublishServiceConfig) for information on how to do this.

### IDs configured incorrectly

There are several pieces of ID required to configure your game.  You can see more information in [Get started with Visual Studio for UWP games](../../get-started-with-partner/get-started-with-visual-studio-and-uwp.md) and [Getting started with cross-play games](../../get-started-with-partner/get-started-with-cross-play-games.md) depending on what type of title you are creating.

Some things to watch out for are:

* Ensure your  App ID is entered into XDP correctly
* Ensure your PFN is entered into XDP correctly
* Double-check you have created an xboxservices.config in the same directory as your Visual Studio project as described in the [Adding Xbox Live to a new or existing UWP project](../../get-started-with-partner/get-started-with-visual-studio-and-uwp.md) guide.
* Ensure that the "Package Identity" in your appxmanifest is correct.  This is shown in Windows Dev Center as "Package/Identity/Name" on Windows Dev Center in the App Identity section.

### Title ID or SCID not configured correctly

* For UWP titles, your title ID and SCID must be set to the correct value in your xboxservices.config file.  Also ensure that this file is properly formatted as UTF8.  You can see more information in [Get started with Visual Studio for UWP games](../../get-started-with-partner/get-started-with-visual-studio-and-uwp.md). The xboxservices.config file is case sensitive.
* For XDK titles, these values are set in your package.appxmanifest.
* You can see examples for both UWP and XDK title configuration in the Samples directory of the Xbox Live SDK.

## Test using the Xbox App

If you are developing a UWP application, you can debug some issues using the Xbox App:

1. Set your device's sandbox to a development sandbox using the SwitchSandbox.cmd script
2. Open the Xbox App and attempt to sign-in using a test account with access to the same sandbox.

If you are able to successfully sign-in, then this verifies that your development sandbox has been set correctly on your device, and your test account has access to it.

If you are still getting sign-in errors, it is likely that your service configuration is not published to your sandbox, or your xboxservices.config is not setup properly. The xboxservices.config file is case sensitive.

## Debug based on error code

Following are some of the error codes you may see upon sign-in and steps you can take to debug these.  You will see the error code as shown in the below screenshot.

![0x8015DC12 Sign-In Error Screenshot](../../images/troubleshooting/sign_in_error.png)

### 0x80860003 The application is either disabled or incorrectly configured

1. Try deleting your PFX file.

![pfx file in solution explorer](../../images/troubleshooting/pfx_file.png)

If you didn’t sign-in to Visual Studio with the Microsoft Account used for provisioning the app at Windows Dev Center, Visual Studio will auto generate a signing pfx file based on your personal Microsoft Account or your domain account. 
When building the appx package, Visual Studio will use that auto generated pfx to sign the package & alter the “publisher” part of the package identity in the package.appxmanifest. As a result, the produced bits (specifically, the appxmanifest.xml) will have a different package identity than what you intend to use. 

2. Double-check that your package.appxmanifest is set to the same application identity as your title on Dev Center. You can either right click on your project and choose Store -> Associate App With Store... as shown in the below screenshot. Or manually edit your package.appxmanifest. See [Get started with Visual Studio for UWP games](../../get-started-with-partner/get-started-with-visual-studio-and-uwp.md) for more information.

![Associate with store](../../images/troubleshooting/appxmanifest_binding.png)

### 0x8015DC12 Content Isolation Error

In summary, this means that either the device or user doesn't have access to the specified title.

1. This could mean you're not using a test account to attempt sign-in, or your test account doesn't have access to the same sandbox you're signed in as. Please double-check the instructions on creating test accounts at [XDP documentation](https://developer.xboxlive.com/en-us/xdphelp/development/xdpdocs/Pages/creating_development_accounts_03_31_16.aspx) and if necessary create a new test account with access to the appropriate sandbox.

You may need to remove your old account from Windows 10, you can do that by going to Settings from the Start Menu, and then going to Accounts

2. Double-check that your title is published to the sandbox that you are trying to use. Please see the [XDP documentation](https://developer.xboxlive.com/en-us/xdphelp/development/xdpdocs/Pages/setting_up_service_configuration_03_31_16.aspx#PublishServiceConfig) for information on how to do this.

### 0x87DD0005 Unexpected or unknown title

Double-check the Application ID Setup and Dev Center Binding in XDP. You can view the instructions in [Adding Xbox Live Support to a new or existing Visual Studio UWP](../../images/troubleshooting/dev_center_binding.png)

### 0x87DD000E Title not authorized

Double-check that your device is set to the proper development sandbox and that the user has access to the sandbox. See the [Test using the Xbox App](#test-xbox-app) section for more information on how you can verify these with the Xbox App.

If that doesn't resolve the issue, then also check the Dev Center Binding and App ID setup as described above.

If you are getting an error not described here, please refer to the error list in the xbox::services::xbox_live_error_code documentation to get more information about the error codes. You can also refer to errors.h in the XSAPI includes.

After all these steps, if you still cannot sign-in with your title, please post a support thread on the [forums](http://forums.xboxlive.com) or reach out to your DAM.