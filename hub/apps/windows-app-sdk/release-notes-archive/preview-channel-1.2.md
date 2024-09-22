---
title: Preview release channel for the Windows App SDK 1.2
description: Provides info about the preview release channel for the Windows App SDK 1.2.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Preview channel release notes for the Windows App SDK 1.2

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store.

The preview channel includes releases of the Windows App SDK with [preview channel features](../release-channels.md#features-available-by-release-channel) in late stages of development. Preview releases do not include experimental features and APIs but may still be subject to breaking changes before the next stable release.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).
- For documentation on preview releases, see [Install tools for preview and experimental channels of the Windows App SDK](../preview-experimental-install.md).

**Latest preview channel release:**

- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Version 1.2 Preview 2 (1.2.0-preview2)

This is the latest release of the preview channel for version 1.2.

In an existing Windows App SDK 1.1 (from the stable channel) app, you can update your Nuget package to 1.2.0-preview2 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Latest Windows App SDK downloads](../downloads.md).

> [!Important]
> Visual Studio 2019 and .NET 5 is no longer supported for building C# apps (see [Windows App SDK 1.2 moving to C# WinRT 2.0](https://github.com/microsoft/WindowsAppSDK/discussions/2879)). You will need Visual Studio 2022 and one of the following .NET SDK versions: 6.0.401 (or later), 6.0.304, 6.0.109.
>
> To update your .NET SDK version, install the latest version of Visual Studio 2022 or visit [.NET Downloads](https://dotnet.microsoft.com/download). When updating your NuGet package without the required .NET SDK version, you will see an error like: *"This version of WindowsAppSDK requires .NET 6+ and WinRT.Runtime.dll version 2.0 or greater."*. To update the project from .NET 5.0 to .NET 6.0, open the project file and change "TargetFramework" to `net6.0` and "Target OS version" to the appropriate value (such as `net6.0-windows10.0.19041.0`).

### Third-party Widgets in Windows

The Widgets Board was first introduced in Windows 11 and was limited to displaying first party Widgets. Widgets are small UI containers that display text and graphics on the Widgets Board, and are associated with an app installed on the device. With Windows App SDK, as third party developers you can now create Widgets for your packaged Win32 apps and test them locally on the Windows 11 Widgets Board.

For more information about Widgets, check out [Widgets overview](../../design/widgets/index.md).

To get started developing Widgets for your app, check out the [Widget providers](../../develop/widgets/widget-providers.md) development docs and [Widgets design fundamentals](../../design/widgets/widgets-design-fundamentals.md) for prerequisites, guidance and best practices.

Prerequisites for this release include:

- Developer mode enabled on the development machine.
- The development machine is running a version of Windows from the Dev Channel of the Windows Insider Program (WIP) with Widgets Board version 521.20060.1205.0 or above.

#### Known limitations when developing Widgets

- Third-party Widgets can only be tested locally on devices enrolled in WIP for this preview release. In Windows App SDK 1.2.0, users on retail versions of Windows can begin acquiring 3P Widgets via Microsoft Store shipped versions of your app.
- Widgets can only be created for packaged, Win32 apps. Widgets for Progressive Web Apps (PWA) are planned to be supported as part of [Microsoft Edge 108](/deployedge/microsoft-edge-release-schedule).

### Trimming for apps developed with .NET

.NET developers are now able to publish their WinAppSDK apps trimmed. With CsWinRT 2.0, the C#/WinRT projections distributed in WinAppSDK are now trimmable. Publishing your app trimmed can reduce the disk footprint of your app by removing any unused code from trimmable binaries.  Apps may also see a startup performance improvement. With a basic Hello World app, we have seen a ~80% disk footprint improvement and a ~7% startup performance improvement when published trimmed. With WinUI gallery, we have seen a ~45% disk footprint improvement.

For more details on how to enable trimming, trimming limitations (such as reflection against trimmable types), and trim warnings, see [Trim self-contained deployments and executables](/dotnet/core/deploying/trimming/trim-self-contained). Developers should thoroughly test their apps after trimming to ensure everything works as expected. For more information, check out issue [2478](https://github.com/microsoft/WindowsAppSDK/issues/2478) on GitHub.

### DisplayInformation

Win32 apps can now support High Dynamic Range (HDR) through the DisplayInformation class in WinAppSDK. The DisplayInformation class enables you to monitor display-related information for an application view. This includes events to allow clients to monitor for changes in the application view affecting which display(s) the view resides on, as well as changes in displays that can affect the application view.

### Fixed issues in WinUI 3

- Acrylic backdrop material via [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController) is now supported in Windows 10 apps. For more information, check out issue [7112](https://github.com/microsoft/microsoft-ui-xaml/issues/7112) on GitHub.
- Fixed issue causing App.UnhandledException to fail to be routed to the application. For more information, check out issue [5221](https://github.com/microsoft/microsoft-ui-xaml/issues/5221) on GitHub.
- Fixed issue causing ListView styles to regress and change from WinAppSDK 1.1. For more information, check out issue [7666](https://github.com/microsoft/microsoft-ui-xaml/issues/7666) on GitHub.

### Other limitations and known issues (1.2.0-preview2)

> [!Important]
> When you reference WinAppSDK 1.2 from a project you might see an error similar to: "*Detected package downgrade: Microsoft.Windows.SDK.BuildTools from 10.0.22621.1 to 10.0.22000.194.*", which is caused by incompatible references to the package from the app project and the WinAppSDK package. To resolve this you can update the reference in the project to a more recent and compatible version of Microsoft.Windows.SDK.BuildTools, or simply remove the reference from your project. If you remove it from your project, a compatible version will be implicitly referenced by the WinAppSDK package.

- Building with [Arm64 Visual Studio](https://devblogs.microsoft.com/visualstudio/arm64-visual-studio/) is not currently supported.
- Bootstrapper and Undocked RegFree WinRT auto-initializer defaults is (now) only set for projects that produce an executable (OutputType=Exe or WinExe). This prevents adding auto-initializers into class library DLLs and other non-executables by default.
  - If you need an auto-initializer in a non-executable (e.g. a test DLL loaded by a generic executable that doesn't initialize the Bootstrapper) you can explicitly enable an auto-initializer in your project via `<WindowsAppSdkBootstrapInitialize>true</WindowsAppSdkBootstrapInitialize>` or `<WindowsAppSdkUndockedRegFreeWinRTInitialize>true</WindowsAppSdkUndockedRegFreeWinRTInitialize>`.
- The version information APIs (ReleaseInfo and RuntimeInfo) can be called but return version 0 (not the actual version information).

## Version 1.2 Preview 1 (1.2.0-preview1)

In an existing Windows App SDK 1.1 (from the stable channel) app, you can update your Nuget package to 1.2.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Latest Windows App SDK downloads](../downloads.md).

### WinUI 3

WinUI 3 apps can play audio and video with the [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) and [**MediaTransportControls**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediatransportcontrols) media playback controls. For more info on how and when to use media controls, see [Media players](../../design/controls/media-playback.md).

WinUI 3 has been updated with the latest controls, styles, and behaviors from WinUI 2.8. These updates include the addition of the [**InfoBadge**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.infobadge) control, improvements to accessibility and high contrast mode, as well as bug fixes across controls. For more details, see the release notes for [WinUI 2.7](../../winui/winui2/release-notes/winui-2.7.md) and [WinUI 2.8](../../winui/winui2/release-notes/winui-2.8.md).

#### Known issue

[**ListView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) styles regressed and changed from WinAppSDK 1.1.

### Notifications

[**AppNotificationBuilder**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder) introduced as an alternative to XML payload for creating and defining App Notifications.

For usage information, see the [AppNotificationBuilder spec](https://github.com/microsoft/WindowsAppSDK/blob/release/1.2-preview1/specs/AppNotifications/AppNotificationContentSpec/AppNotificationBuilder-spec.md) on GitHub.

Also see [Quickstart: App notifications in the Windows App SDK](../notifications/app-notifications/app-notifications-quickstart.md) for an example of how to create a desktop Windows application that sends and receives local app notifications.

#### Breaking change

For push notifications, when making a channel request call, apps will need to use the Azure Object ID instead of the Azure App ID. See [Quickstart: Push notification in the Windows App SDK](../notifications/push-notifications/push-quickstart.md) for details on finding your Azure Object ID.

#### Fixed issue

[**PushNotificationManager.IsSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationmanager.issupported) will perform a check for elevated mode. It will return `false` if the app is elevated.

#### Known limitations (Notifications)

- In [**AppNotificationScenario**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationscenario), `Urgent` is only supported for Windows builds 19041 and later. You can use [**AppNotificationBuilder.IsUrgentScenarioSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.isurgentscenariosupported) to check whether the feature is available at runtime.
- In [**AppNotificationButton**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton), `hint-toolTip` and `hint-buttonStyle` are only supported for builds 19041 and later. You can use [**IsButtonStyleSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton.isbuttonstylesupported) and [**IsToolTipSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton.istooltipsupported) to check whether the feature is available at runtime.
- In [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement), when used in XAML markup for an unpackaged app, the [**Source**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement.source) property cannot be set with an ms-appx or ms-resource URI. As an alternative, set the Source using a file URI, or set from code.

### Windowing

Full title bar customization is now available on Windows 10, version 1809 and later through the [**AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) class. You can set [**AppWindowTitleBar.ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) to `true` to extend content into the title bar area, and [**SetDragRectangles**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.setdragrectangles#microsoft-ui-windowing-appwindowtitlebar-setdragrectangles(windows-graphics-rectint32())) to define drag regions (in addition to other customization options).

If you've been using the [**AppWindowTitleBar.IsCustomizationSupported**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) property to check whether you can call the [**AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) APIs, it now returns `true` on supported Windows App SDK Windows 10 versions (1809 and later).

#### Known limitations (Windowing)

Simple title bar customizations are not supported on Windows 10. These include [**BackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.backgroundcolor), [**InactiveBackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactivebackgroundcolor), [**ForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.foregroundcolor), [**InactiveForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactiveforegroundcolor) and [**IconShowOptions**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions). If you call these properties, they will be ignored silently. All other **AppWindowTitleBar** APIs work in Windows 10, version 1809 and later. For the caption button color APIs (among others) and [**Height**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.height), [**ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) must be set to `true`, otherwise they will also be ignored silently.

### Access control

Introduced [security.accesscontrol.h](/windows/windows-app-sdk/api/win32/security.accesscontrol/) with the [**GetSecurityDescriptorForAppContainerNames**](/windows/windows-app-sdk/api/win32/security.accesscontrol/nf-security-accesscontrol-getsecuritydescriptorforappcontainernames?branch=main) function to ease and streamline named object sharing between packaged processes and general Win32 APIs. This method takes a list of Package Family Names (PFNs) and access masks, and returns a security descriptor. For more information, see the [GetSecurityDescriptorForAppContainerNames spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/GetSecurityDescriptorForAppContainerNames/GetSecurityDescriptorForAppContainerNames.md) on GitHub.

### Other limitations and known issues (1.2.0-preview1)

- .NET PublishSingleFile isn't supported.

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
