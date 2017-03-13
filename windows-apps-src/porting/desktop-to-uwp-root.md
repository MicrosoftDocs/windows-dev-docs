---
author: normesta
Description: Get started with the Desktop to UWP Bridge and convert your Windows desktop application (like Win32, WPF, and Windows Forms) to a Universal Windows Platform (UWP) app.
Search.Product: eADQiWindows 10XVcnh
title: Desktop to Universal Windows Platform (UWP) Bridge
ms.author: normesta
ms.date: 03/09/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 74373c24-f948-43bb-aa85-01e2e8e87162
---

# Desktop to Universal Windows Platform (UWP) Bridge

Get started with the Desktop to UWP Bridge and convert your Windows desktop application to a Universal Windows Platform (UWP) app.

The Desktop Bridge is a set of technologies that enable you to convert your Windows desktop application (for example, Win32, Windows Forms, or WPF) or game to a UWP app or game. After conversion, your Windows desktop application is packaged, serviced, and deployed in the form of a UWP app package (an .appx or an .appxbundle) targeting Windows 10 Desktop.

There are two parts to the technology that enables desktop apps to be converted to UWP packages. The first is the conversion process, which takes your existing binaries and repackages them as a UWP package. Your code is still the same, it's just packaged differently. The second piece comprises runtime technologies in the Windows Anniversary update that enable a UWP package to have executables that run as full trust instead of in an app container. This technology also gives a converted app a package identity, which is required to use some UWP APIs.

## Benefits

Here are some of the benefits of converting your Windows desktop application:

**Streamlined deployment**. Apps and games using the bridge have a great deployment experience that ensures users can confidently install and update. If a user chooses to uninstall the app, it's removed completely with no trace left behind. This reduces time authoring setup experiences and keeping users up-to-date.

**Automatic updates and licensing**. Your app can participate in the Windows Store's built-in licensing and automatic update facilities. Automatic update is a highly reliable and efficient mechanism, because only the changed parts of files are downloaded.

**Increased reach and simplified monetization**. Choosing to distribute through the Windows Store gets you increased reach to millions of Windows 10 users, who can acquire apps, games and in-app purchases with local payment options.

**Add UWP features**.  At your own pace, you can add UWP features to your app's package, like a XAML user-interface, live tile updates, UWP background tasks, app services, and many more.

**Broadened use-cases across device**. Using the bridge, you can gradually migrate their code to the Universal Windows Platform to reach every Windows 10 device, including phones, Xbox One and HoloLens.

## Prepare

The Desktop to UWP Bridge is designed for ease of use, and you may not need to do much to get your app ready for the conversion process. However, there are a some caveats and unique situations to be aware of before converting. Consult the article [Prepare your app for the Desktop to UWP Bridge](desktop-to-uwp-prepare.md) and address any issues that apply to your app before proceeding.

## Convert

You have a few different options for converting your app.

**Desktop App Converter (DAC)**. The DAC is a tool that automatically converts and signs your app for you. Using the DAC is convenient and automatic, and it's useful if your app makes lots of system modifications or if there's any uncertainty about what your installer does. To get started, see the article on the [Desktop App Converter](desktop-to-uwp-run-desktop-app-converter.md).

**Manual Conversion**. If your app is installed using xcopy, or you're familiar with the changes that your installer makes to the system, manual conversion might be a more straightforward choice. This involves creating a manifest file, running the MakeAppx.exe tool, and then signing your app package. For details on how to manually convert, see [Manually convert your app to UWP using the Desktop Bridge](desktop-to-uwp-manual-conversion.md).

**Third-Party Installer**. Several popular third-party products and installers now support the Desktop Bridge and can generate MSI installers or converted app packages with only a few clicks. Some options include:

* [InstallShield by Flexera](http://www.flexerasoftware.com/producer/products/software-installation/installshield-software-installer)
* [WiX by FireGiant](https://www.firegiant.com/r/appx)
* [Advanced Installer by Caphyon](http://www.advancedinstaller.com/uwp-app-package)
* [RAD Studio by Embarcadero](https://www.embarcadero.com/products/rad-studio/windows-10-store-desktop-bridge)
* [InstallAware](https://www.installaware.com/appx.htm)

For more information, visit the respective website for each installer.

## Enhance

You can light up your converted desktop app with a wide range of UWP APIs to add features like live tiles, push notifications, and more. For complete code samples, see the [Desktop app bridge to UWP Samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples) and [Universal Windows Platform (UWP) app samples](https://github.com/Microsoft/Windows-universal-samples) repos on GitHub. To view a complete list of supported API, review [Supported UWP APIs for apps converted with the Desktop Bridge](desktop-to-uwp-supported-api.md).

In addition to calling UWP APIs, you can extend your app with features accessible only to converted apps. These include scenarios like launching a process when the user logs on and File Explorer integration, and are designed to smooth the transition between the original desktop app and a full UWP app package. For details, see [Desktop Bridge app extensions](desktop-to-uwp-extensions.md).

## Migrate

Using the bridge, you can gradually migrate your older code to UWP while still retaining the ability to run and publish your app on Windows Desktop. Once you’re fully migrated to UWP (and your app no longer contains any WPF/Win32 components), you can reach all Windows devices including phones, Xbox One and HoloLens.

## Debug

You can debug your app using Visual Studio. Consult [Debug apps converted with the Desktop Bridge](desktop-to-uwp-debug.md) for detailed help.

If you're interested in the internals of how the Desktop Bridge works under the covers, check out [Behind the scenes of the Desktop Bridge](desktop-to-uwp-behind-the-scenes.md).

## Distribute

You can distribute your app using the Windows Store or via sideloading. For full details, see [Distribute apps converted with the Desktop Bridge](desktop-to-uwp-distribute.md). Be aware that you'll need to sign your app before you can deploy it to users. See [Sign an app converted with the Desktop Bridge](desktop-to-uwp-signing.md) for step-by-step directions.

## Support and feedback

If you run in to issues converting your app, you can visit the [forums](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/home?forum=wpdevelop) for help.

To give feedback or make feature suggestions, submit or upvote items on [UserVoice](https://wpdev.uservoice.com/forums/110705-universal-windows-platform/category/161895-desktop-bridge-centennial).

## In this section

| Topic | Description |
|-------|-------------|
| [Desktop App Converter](desktop-to-uwp-run-desktop-app-converter.md) | Shows how to run Desktop App Converter. |
| [Manually convert your app to UWP using the Desktop Bridge](desktop-to-uwp-manual-conversion.md) | Learn how to create an app package and manifest to by hand. |
| [Desktop Bridge app extensions](desktop-to-uwp-extensions.md) | Enhance your converted app with extensions to enable features like startup tasks and File Explorer integration. |
| [Supported UWP APIs for apps converted with the Desktop Bridge](desktop-to-uwp-supported-api.md) | See what UWP APIs are available for your converted desktop app to use. |
| [Desktop Bridge Packaging Guide for .NET Desktop apps with Visual Studio](desktop-to-uwp-packaging-dot-net.md) | Configure your Visual Studio Solution so you can edit, debug, and package your .NET app. |
| [Debug apps converted with the Desktop Bridge](desktop-to-uwp-debug.md) | Explains options for debugging your converted app. |
| [Sign an app converted with the Desktop Bridge](desktop-to-uwp-signing.md) | Learn how to sign your converted app package with a certificate. |
| [Distribute apps converted with the Desktop Bridge](desktop-to-uwp-distribute.md) | See how you can distribute your converted app to users.  |
| [Behind the scenes of the Desktop Bridge](desktop-to-uwp-behind-the-scenes.md) | Take a deeper dive on how the Desktop to UWP Bridge works under the covers. |
| [Known issues with the Desktop Bridge](desktop-to-uwp-known-issues.md) | Lists known issues with the Desktop to UWP Bridge. |
| [Desktop app bridge to UWP code samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples) | Code samples on GitHub demonstrating features of converted apps. |
