---
title: Stable channel release notes for the Windows App SDK 1.2
description: Provides information about the stable release channel for the Windows App SDK 1.2.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Stable channel release notes for the Windows App SDK 1.2

The stable channel provides releases of the Windows App SDK that are supported for use by apps in production environments. Apps that use the stable release of the Windows App SDK can also be published to the Microsoft Store.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Downloads for the Windows App SDK

> [!NOTE]
> The Windows App SDK Visual Studio Extensions (VSIX) are no longer distributed as a separate download. They are available in the Visual Studio Marketplace inside Visual Studio.

### Version 1.2.5 (1.2.230313.1)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.2 release.

- Fixed issue causing apps to crash during Composition shutdown.
- Fixed issue causing apps to continue running animations even when the screen is off.
- Fixed issue causing mouse and touch input to fail in WebView2 when mouse and keyboard input occurred simultaneously. For more information, see GitHub issue [#3266](https://github.com/microsoft/WindowsAppSDK/issues/3266).

### Version 1.2.4 (1.2.230217.4)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.2 release.

- Fixed issue causing self-contained apps to not be able to set UAC Settings. For more information, see GitHub issue [#3376](https://github.com/microsoft/WindowsAppSDK/issues/3376).
- Fixed issue causing push notifications to return an inaccurate Expiration time with `PushNotificationChannel::ExpirationTime`. For more information, see GitHub issue [#3300](https://github.com/microsoft/WindowsAppSDK/issues/3330).
- Fixed issue causing negative numbers to be considered "invalid" when passing a double as a parameter into an x:Bind function.
- Several fixes to update the WinUI VSIX. These updates included simplifying the project template dipAwareness in app.manifest, removing the UWP templates, updating localized resource files, adding the phone id to unblock store submission, and removing the copyright notice and license. For more info see GitHub issues [#5659](https://github.com/microsoft/microsoft-ui-xaml/issues/5659), [#3205](https://github.com/microsoft/WindowsAppSDK/issues/3205), [#3323](https://github.com/microsoft/WindowsAppSDK/issues/3323), [#3322](https://github.com/microsoft/WindowsAppSDK/issues/3322), [#3143](https://github.com/microsoft/WindowsAppSDK/issues/3143).

### Version 1.2.3 (1.2.230118.102)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.2 release.

- Fixed issue causing WinUI 3 apps to crash when multiple windows are closed.
- Fixed issue causing a crash on app close when two or more references to the ThreadPoolTimer interface are called. For more information, see GitHub issues [#7260](https://github.com/microsoft/microsoft-ui-xaml/issues/7260) and [#7239](https://github.com/microsoft/microsoft-ui-xaml/issues/7239).
- Fixed issue causing all Single-project MSIX apps to run as full trust. For more information, see GitHub issue [#7766](https://github.com/microsoft/microsoft-ui-xaml/issues/7766).

### Version 1.2.2 (1.2.221209.1)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.2 release.

- Fixed issue that caused the Store and side-load packages (e.g. from installer, NuGet, and bootstrapper) to fail to install if the other is already installed. For more information, see GitHub issue [#3168](https://github.com/microsoft/WindowsAppSDK/issues/3168).
- Fixed issue causing missing elasticity effects and animation curves when scrolling with a touchpad. For more information, see GitHub issue [#7874](https://github.com/microsoft/microsoft-ui-xaml/issues/7874).
- Fixed issue in ListView causing memory leaks.
- Fixed issue causing the Button template to not respect the Foreground property after mouse hover. For more information, see GitHub issue [#7208](https://github.com/microsoft/microsoft-ui-xaml/issues/7208).
- Fixed issue causing an unneeded exception when there is no MediaPlaybackItem in a MediaElement.
- Fixed issue causing a white frame to appear in MediaPlayerElement on content transitions.
- Fixed additional issues causing App.UnhandledException to not catch exceptions from other threads. For more information, see GitHub issues [#1259](https://github.com/microsoft/CsWinRT/issues/1259) and [#5221](https://github.com/microsoft/microsoft-ui-xaml/issues/5221).

### Version 1.2.1 (1.2.221116.1)

This is a servicing release of the Windows App SDK that includes a critical bug fix for the 1.2 release.

Fixed issue that caused a crash on startup in C++ WinUI 3 apps when adding a WebView2 or TextBox control. For more information see GitHub issues [#7911](https://github.com/microsoft/microsoft-ui-xaml/issues/7911) & [#3117](https://github.com/microsoft/WindowsAppSDK/issues/3117).

### new and updated features and known issues for version 1.2

The following sections describe new and updated features and known issues for 1.2.

> [!NOTE]
> Visual Studio 2019 and .NET 5 is no longer supported for building C# apps (see [Windows App SDK 1.2 moving to C# WinRT 2.0](https://github.com/microsoft/WindowsAppSDK/discussions/2879)). You will need Visual Studio 2022 and one of the following .NET SDK versions: 6.0.401 (or later), 6.0.304, 6.0.109. When released, WinAppSDK 1.2 will support .NET 7 as well.
>
> To update your .NET SDK version, install the latest version of Visual Studio 2022 or visit [.NET Downloads](https://dotnet.microsoft.com/download). When updating your NuGet package without the required .NET SDK version, you will see an error like: *"This version of WindowsAppSDK requires .NET 6+ and WinRT.Runtime.dll version 2.0 or greater."*. To update the project from .NET 5.0 to .NET 6.0, open the project file and change "TargetFramework" to `net6.0` and "Target OS version" to the appropriate value (such as `net6.0-windows10.0.19041.0`).

#### Third-party Widgets in Windows

The widgets board was first introduced in Windows 11 and was limited to displaying built-in widgets. Widgets are small UI containers that display text and graphics on the widgets board, and are associated with an app installed on the device. With Windows App SDK, as third party developers you can now create widgets for your packaged Win32 apps and test them locally on the Windows 11 widgets board.

For more information about widgets, check out [Widgets Overview](/windows/apps/design/widgets/).

To get started developing widgets for your app, check out the [Widget service providers](/windows/apps/develop/widgets/widget-service-providers) development docs and [Widgets design fundamentals](/windows/apps/design/widgets/widgets-design-fundamentals) for prerequisites, guidance and best practices.

Prerequisites for this release include:

- Developer mode enabled on the development machine.
- The development machine is running a version of Windows from the Dev Channel of Windows Insider Preview (WIP) that is greater than or equal to 25217 with widgets board version 521.20060.1205.0 or above.

**Known limitations when developing Widgets:**

- Third-party Widgets can only be tested locally on devices enrolled in WIP for this preview release.
- Widgets can only be created for packaged, Win32 apps. Widgets for Progressive Web Apps (PWA) are planned to be supported as part of [Microsoft Edge 108](/deployedge/microsoft-edge-release-schedule).

#### DisplayInformation

Windows desktop apps  can now support High Dynamic Range (HDR) and [Auto Color Management](https://devblogs.microsoft.com/directx/auto-color-management/) (ACM) through the DisplayInformation class in WinAppSDK. The DisplayInformation class enables you to monitor display-related information for an application view. This includes events to allow clients to monitor for changes in the application view affecting which display(s) the view resides on, as well as changes in displays that can affect the application view.

#### WinUI 3

WinUI 3 apps can play audio and video with the [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) and [**MediaTransportControls**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediatransportcontrols) media playback controls. For more info on how and when to use media controls, see [Media players](/windows/apps/design/controls/media-playback).

WinUI 3 has been updated with the latest controls, styles, and behaviors from WinUI 2.8. These updates include the addition of the [**InfoBadge**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.infobadge) control, improvements to accessibility and high contrast mode, as well as bug fixes across controls. For more details, see the release notes for [WinUI 2.7](/windows/apps/winui/winui2/release-notes/winui-2.7) and [WinUI 2.8](/windows/apps/winui/winui2/release-notes/winui-2.8).

**Fixed issues:**

- Acrylic backdrop material with [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController) is now supported in Windows 10 apps. For more information, check out issue [7112](https://github.com/microsoft/microsoft-ui-xaml/issues/7112) on GitHub.
- Fixed various issues that caused routing of App.UnhandledException to fail. For more information, check out issue [5221](https://github.com/microsoft/microsoft-ui-xaml/issues/5221) on GitHub. Regarding the remaining issues, workarounds are documented at the following GitHub issues and will be resolved in a future 1.2 release:
  - [App_UnhandledException's UnhandledExceptionEventArgs.Exception can only be fetched once - should be cached](https://github.com/microsoft/CsWinRT/issues/1258)
  - [Do_Abi_* event/callback handlers should wrap invocations with RoReportUnhandledError](https://github.com/microsoft/CsWinRT/issues/1259)
- Fixed issue causing ListView styles to regress and change from WinAppSDK 1.1. For more information, check out issue [7666](https://github.com/microsoft/microsoft-ui-xaml/issues/7666) on GitHub.
- Fixed issue causing the incorrect Mica fallback background color to appear when the app is inactive. For more information, check out issue [7801](https://github.com/microsoft/microsoft-ui-xaml/issues/7801) on GitHub.

**Known limitations:**

- When creating a new WinUI 3 project with Visual Studio 2022 17.4.0, it will reference a preview version of the WinAppSDK. Use NuGet Package Manager to update the reference to this release.
- Setting MediaPlayerElement.Source to relative URI (ms-appx/ms-resource) fails in unpackaged apps. The recommended workaround is to convert the relative ms-appx:/// URI to a fully resolved file:/// URI.

#### Trimming for apps developed with .NET

.NET developers can now publish trimmed WinAppSDK apps. With CsWinRT 2.0, the C#/WinRT projections distributed in WinAppSDK are now trimmable. Publishing your app trimmed can reduce the disk footprint of your app by removing any unused code from trimmable binaries.  Apps may also see a startup performance improvement. With a basic Hello World app, we have seen a ~80% disk footprint improvement and a ~7% startup performance improvement when published trimmed. With WinUI gallery, we have seen a ~45% disk footprint improvement.

For more details on how to enable trimming, trimming limitations (such as reflection against trimmable types), and trim warnings, see [Trim self-contained deployments and executables](/dotnet/core/deploying/trimming/trim-self-contained). Developers should thoroughly test their apps after trimming to ensure everything works as expected. For more information, check out issue [2478](https://github.com/microsoft/WindowsAppSDK/issues/2478) on GitHub.

#### Support for Visual Studio Arm64

As early as Project Reunion (now WinAppSDK) 0.5, apps developed with WinAppSDK were able to run on Arm64. Starting with Visual Studio 17.3 Preview 2, you can develop native applications with WinAppSDK on Arm64 devices.

To get started developing on an Arm64 device, see [Windows on Arm](/windows/arm/overview) and [Arm64 Visual Studio](https://devblogs.microsoft.com/visualstudio/arm64-visual-studio/).

#### Notifications

[**AppNotificationBuilder**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) introduced as an alternative to XML payload for creating and defining App Notifications.

For usage information, see the [AppNotificationBuilder spec](https://github.com/microsoft/WindowsAppSDK/blob/release/1.2-preview1/specs/AppNotifications/AppNotificationContentSpec/AppNotificationBuilder-spec.md) on GitHub.

Also see [Quickstart: App notifications in the Windows App SDK](/windows/apps/windows-app-sdk/notifications/app-notifications/app-notifications-quickstart) for an example of how to create a desktop Windows application that sends and receives local app notifications.

**Breaking change:**

For push notifications, when making a channel request call, apps will need to use the Azure Object ID instead of the Azure App ID. See [Quickstart: Push notification in the Windows App SDK](/windows/apps/windows-app-sdk/notifications/push-notifications/push-quickstart) for details on finding your Azure Object ID.

**Fixed issue:**

[**PushNotificationManager.IsSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationmanager.issupported) will perform a check for elevated mode. It will return `false` if the app is elevated.

**Known limitations (Notifications):**

- In [**AppNotificationScenario**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationscenario), `Urgent` is only supported for Windows builds 19041 and later. You can use [**AppNotificationBuilder.IsUrgentScenarioSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.isurgentscenariosupported) to check whether the feature is available at runtime.
- In [**AppNotificationButton**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton), `hint-toolTip` and `hint-buttonStyle` are only supported for builds 19041 and later. You can use [**IsButtonStyleSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton.isbuttonstylesupported) and [**IsToolTipSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton.istooltipsupported) to check whether the feature is available at runtime.
- In [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement), when used in XAML markup for an unpackaged app, the [**Source**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement.source) property cannot be set with an ms-appx or ms-resource URI. As an alternative, set the Source using a file URI, or set from code.

#### Windowing

Full title bar customization is now available on Windows 10, version 1809 and later through the [**AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) class. You can set [**AppWindowTitleBar.ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) to `true` to extend content into the title bar area, and [**SetDragRectangles**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.setdragrectangles#microsoft-ui-windowing-appwindowtitlebar-setdragrectangles(windows-graphics-rectint32())) to define drag regions (in addition to other customization options).

If you've been using the [**AppWindowTitleBar.IsCustomizationSupported**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) property to check whether you can call the [**AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) APIs, it now returns `true` on supported Windows App SDK Windows 10 versions (1809 and later).

**Known limitations (Windowing):**

Basic title bar customizations are not supported on Windows 10. These include [**BackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.backgroundcolor), [**InactiveBackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactivebackgroundcolor), [**ForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.foregroundcolor), [**InactiveForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactiveforegroundcolor) and [**IconShowOptions**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions). If you call these properties, they will be ignored silently. All other **AppWindowTitleBar** APIs work in Windows 10, version 1809 and later. For the caption button color APIs (among others) and [**Height**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.height), [**ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) must be set to `true`, otherwise they will also be ignored silently.

#### Access control

Introduced [security.accesscontrol.h](/windows/windows-app-sdk/api/win32/security.accesscontrol/) with the [**GetSecurityDescriptorForAppContainerNames**](/windows/windows-app-sdk/api/win32/security.accesscontrol/nf-security-accesscontrol-getsecuritydescriptorforappcontainernames?branch=main) function to ease and streamline named object sharing between packaged processes and general Win32 APIs. This method takes a list of Package Family Names (PFNs) and access masks, and returns a security descriptor. For more information, see the [GetSecurityDescriptorForAppContainerNames spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/GetSecurityDescriptorForAppContainerNames/GetSecurityDescriptorForAppContainerNames.md) on GitHub.

#### Other limitations and known issues

> [!Important]
> When you reference WinAppSDK 1.2 from a project you might see an error similar to: "*Detected package downgrade: Microsoft.Windows.SDK.BuildTools from 10.0.22621.1 to 10.0.22000.194.*", which is caused by incompatible references to the package from the app project and the WinAppSDK package. To resolve this you can update the reference in the project to a more recent and compatible version of Microsoft.Windows.SDK.BuildTools.

- Unit tests may fail with a `REGDB_E_CLASSNOTREG` error in the Tests output pane in Visual Studio. As a workaround, you can add `<WindowsAppContainer>true</WindowsAppContainer>` to your project file.
- .NET PublishSingleFile isn't supported.
- Bootstrapper and Undocked RegFree WinRT auto-initializer defaults is (now) only set for projects that produce an executable (OutputType=Exe or WinExe). This prevents adding auto-initializers into class library DLLs and other non-executables by default.
  - If you need an auto-initializer in a non-executable (e.g. a test DLL loaded by a generic executable that doesn't initialize the Bootstrapper) you can explicitly enable an auto-initializer in your project via `<WindowsAppSdkBootstrapInitialize>true</WindowsAppSdkBootstrapInitialize>` or `<WindowsAppSdkUndockedRegFreeWinRTInitialize>true</WindowsAppSdkUndockedRegFreeWinRTInitialize>`.
- Microsoft.WindowsAppRuntime.Release.Net.dll is always the Arm64 binary and does not work for x86 and x64 apps. When explicitly calling the Bootstrap API do not use the Microsoft.WindowsAppRuntime.Release.Net.dll assembly. As a workaround you can include version constants in this source file distributed with the NuGet package: '..\include\WindowsAppSDK-VersionInfo.cs' or use the auto-initializer.

## Related topics

- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
