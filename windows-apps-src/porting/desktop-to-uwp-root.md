---
author: normesta
Description: Get started with the Desktop to UWP Bridge and convert your Windows desktop application (like Win32, WPF, and Windows Forms) to a Universal Windows Platform (UWP) app.
Search.Product: eADQiWindows 10XVcnh
title: Desktop to Universal Windows Platform (UWP) Bridge
ms.author: normesta
ms.date: 03/30/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 74373c24-f948-43bb-aa85-01e2e8e87162
---

# Desktop to Universal Windows Platform (UWP) Bridge

Get started with the Desktop to UWP Bridge and convert your Windows desktop application to a Universal Windows Platform (UWP) app.
<div style="float: left; padding: 10px">
    ![desktop to UWP bridge image](images/desktop-to-uwp/desktop-bridge-4.png)
</div>
The Desktop Bridge is a set of technologies that help you to convert your Windows desktop application (for example, Win32, Windows Forms, or WPF) or game to a UWP app or game. A converted app is packaged, serviced, and deployed in the form of a UWP app package (an .appx or an .appxbundle) that targets Windows 10 Desktop.

There are two parts to this technology. The first part is the conversion process that takes your existing binaries and repackages them as a UWP package. Your code is still the same, it's just packaged differently.

The second part comprises runtime technologies in the Windows Anniversary update that enable a UWP package to have executables that run as full trust instead of in an app container. This technology also gives a converted app a package identity. Your app will need that identity to use some UWP APIs.

<div></div>

## Benefits

Here are some reasons to convert your Windows desktop application:

**Streamlined deployment**. Apps and games that use the bridge have a great deployment experience. This experience ensures that users can confidently install an app and update it. If a user chooses to uninstall the app, it's removed completely with no trace left behind. This reduces time authoring setup experiences and keeping users up-to-date.

**Automatic updates and licensing**. Your app can participate in the Windows Store's built-in licensing and automatic update facilities. Automatic update is a highly reliable and efficient mechanism, because only the changed parts of files are downloaded.

**Increased reach and simplified monetization**. Choosing to distribute through the Windows Store expands your reach to millions of Windows 10 users, who can acquire apps, games and in-app purchases with local payment options.

**Add UWP features**.  At your own pace, you can add UWP features to your app's package, like a XAML user-interface, live tile updates, UWP background tasks, app services, and many more.

**Broadened use-cases across device**. Using the bridge, you can gradually migrate your code to the Universal Windows Platform to reach every Windows 10 device, including phones, Xbox One and HoloLens.

To view a more complete list of benefits, see [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop).

## Prepare

The Desktop to UWP Bridge is designed for ease of use so you might not have to make many changes to your app before you convert it. However, there are a some caveats and unique situations to be aware of before convert your app.

Consult the article [Prepare your app for the Desktop to UWP Bridge](desktop-to-uwp-prepare.md) and address any of the issues that apply to your app before you convert it.

## Convert

Here are some ways to convert your app.

### Desktop App Converter (DAC)

The DAC is a tool that automatically converts and signs your app for you. The DAC is convenient and automatic, and it's useful if your app makes lots of system modifications or if there's any uncertainty about what your installer does.

See [Convert an app using the Desktop App Converter (Desktop to UWP Bridge)](desktop-to-uwp-run-desktop-app-converter.md)

### Manual conversion

If your app is installed by using **xcopy**, or you're familiar with the changes that your installer makes to the system, manual conversion might be a more straightforward choice. You'll have to create a manifest file, run the **MakeAppx.exe** tool, and then sign your app package.

See [Convert an app manually (Desktop to UWP Bridge)](desktop-to-uwp-manual-conversion.md).

### Visual Studio

This option is similar to the manual option described above except Visual Studio does a few things for you such as generating an app package and the visual assets for your app.

See [Package a .NET app by using Visual Studio (Desktop to UWP Bridge)](desktop-to-uwp-packaging-dot-net.md)

### Third-party installer

 Several popular third-party products and installers now support the Desktop to UWP Bridge. You can use them to generate MSI installers or converted app packages with only a few clicks.

 Here's a few options:

* [Advanced Installer by Caphyon](http://www.advancedinstaller.com/)
* [InstallShield by Flexera](http://www.flexerasoftware.com/producer/products/software-installation/installshield-software-installer)
* [WiX by FireGiant](https://www.firegiant.com/r/appx)
* [RAD Studio by Embarcadero](https://www.embarcadero.com/products/rad-studio/windows-10-store-desktop-bridge)
* [InstallAware](https://www.installaware.com/appx.htm)

## Enhance

You can light up your converted desktop app with features such as live tiles, and push notifications. Use a wide range of UWP APIs. for a complete list, see [UWP APIs available to Window Desktop Bridge apps](desktop-to-uwp-supported-api.md).

Check out these samples to get ideas.
* [Desktop app bridge to UWP Samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples)
* [Universal Windows Platform (UWP) app samples](https://github.com/Microsoft/Windows-universal-samples)

You can use extensions to integrate with the system. No code is required to use an extension. Just add some XML to your app package. You can use extensions to do things like start a process when the user logs on, integrate your app into File Explorer, and add your app a list of print targets that appear in other apps.

See [Desktop Bridge app extensions](desktop-to-uwp-extensions.md).

## Migrate

Using the bridge, you can gradually migrate your older code to UWP while still retaining the ability to run and publish your app on Windows Desktop. Once you’re fully migrated to UWP (and your app no longer contains any WPF/Win32 components), you can reach all Windows devices including phones, Xbox One and HoloLens.

## Debug

You can debug your app by using Visual Studio. See [Debug apps converted with the Desktop Bridge](desktop-to-uwp-debug.md).

If you're interested in the internals of how the Desktop Bridge works under the covers, see [Behind the scenes of the Desktop Bridge](desktop-to-uwp-behind-the-scenes.md).

## Validate

To give your app the best chance of being published on the Windows Store or becoming [Windows Certified](http://go.microsoft.com/fwlink/p/?LinkID=309666), validate and test it locally before you submit it for certification.

If you're using the DAC to convert your app, you can use the new ``-Verify`` flag to validate your package against the Desktop Bridge and Store requirements. See [Desktop app Converter usage](desktop-to-uwp-run-desktop-app-converter.md#desktop-app-converter-usage).

If you're using Visual Studio, you can validate your app from the **Create App Packages** wizard. See [Create an app package](../packaging/packaging-uwp-apps.md#create-an-app-package).

To run the tool manually, see [Windows App Certification Kit](../debug-test-perf/windows-app-certification-kit.md).

To review the list of tests that the Windows App Certification uses to validate your app, see [Windows Desktop Bridge app tests](../debug-test-perf/windows-desktop-bridge-app-tests.md).

## Distribute

You can distribute your app by using the Windows Store or via sideloading.

See [Distribute apps converted with the Desktop Bridge](desktop-to-uwp-distribute.md).

You'll need to sign your app before you can deploy it to users.

See [Sign an app converted with the Desktop Bridge](desktop-to-uwp-signing.md).

## Support and feedback

If you run in to issues converting your app, you can visit the [forums](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/home?forum=wpdevelop) for help.

To give feedback or make feature suggestions, submit or upvote items on [UserVoice](https://wpdev.uservoice.com/forums/110705-universal-windows-platform/category/161895-desktop-bridge-centennial).

## In this section

| Topic | Description |
|-------|-------------|
| [Prepare to convert an app](desktop-to-uwp-prepare.md) | Provides a list of items to review before you convert your app. |
| [Convert an app using the Desktop App Converter ](desktop-to-uwp-run-desktop-app-converter.md) | Shows how to run Desktop App Converter. |
| [Convert an app manually ](desktop-to-uwp-manual-conversion.md) | Learn how to create an app package and manifest to by hand. |
|[Package a .NET app by using Visual Studio (Desktop to UWP Bridge](desktop-to-uwp-packaging-dot-net.md)| Shows you how to convert your app by using Visual Studio |.
| [App extensions for Windows Desktop Bridge apps](desktop-to-uwp-extensions.md) | Enhance your converted app with extensions to enable features like startup tasks and File Explorer integration. |
| [UWP APIs available to Window Desktop Bridge apps](desktop-to-uwp-supported-api.md) | See what UWP APIs are available for your converted desktop app to use. |
| [Debug a Windows Desktop Bridge app](desktop-to-uwp-debug.md) | Explains options for debugging your converted app. |
| [Sign a Windows Desktop Bridge app](desktop-to-uwp-signing.md) | Learn how to sign your converted app package with a certificate. |
| [Distribute a Windows Desktop Bridge app](desktop-to-uwp-distribute.md) | See how you can distribute your converted app to users.  |
| [Behind the scenes of the Desktop to UWP Bridge](desktop-to-uwp-behind-the-scenes.md) | Take a deeper dive on how the Desktop to UWP Bridge works under the covers. |
| [Known Issues (Desktop to UWP Bridge)](desktop-to-uwp-known-issues.md) | Lists known issues with the Desktop to UWP Bridge. |
| [Desktop to UWP Bridge code samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples) | Code samples on GitHub demonstrating features of converted apps. |
