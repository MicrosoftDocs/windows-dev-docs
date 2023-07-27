---
title: Stable channel release notes for the Windows App SDK 
description: Provides information about the stable release channel for the Windows App SDK.
ms.topic: article
ms.date: 03/31/2022
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Stable channel release notes for the Windows App SDK

The stable channel provides releases of the Windows App SDK that are supported for use by apps in production environments. Apps that use the stable release of the Windows App SDK can also be published to the Microsoft Store.

The following releases of the stable channel are currently available:

- [Version 1.3](#version-13)
- [Version 1.2](#version-12)
- [Version 1.1](#version-11)
- [Version 1.0](#version-10)
- [Version 0.8](#version-08)
- [Version 0.5](#version-05)

If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md). 

## Downloads for Windows App SDK

The Windows App SDK VSIX and runtime (installer and MSIX packages) are available at [Downloads for the Windows App SDK](downloads.md). The SDK downloads include the Visual Studio extensions to create and build new projects using the Windows App SDK. The runtime downloads include the installer and MSIX packages used to deploy apps. If you haven't done so already, [Install tools for the Windows App SDK](set-up-your-development-environment.md?tabs=preview). 

> [!NOTE]
> If you have Windows App SDK Visual Studio extensions (VSIX) already installed, then uninstall them before installing a new version. For directions, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions). 

## Version 1.3

### Version 1.3.3 (1.3.230724000)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.3 release.

- Fixed an issue where the mouse would sometimes stop working when a dialog box was closed.
- Fixed a deployment issue that prevented apps from installing due to a mismatch of package versions on the system. For more information, see GitHub issue [#3740](https://github.com/microsoft/WindowsAppSDK/issues/3740).
- Fixed an issue affecting context menu positioning in Windows App SDK 1.3.
- Fixed an issue causing some WinUI3 apps, in some situations, to crash when the app was closed because XAML shut itself down too early.
- Fixed an issue where font icons were not mirroring properly in right-to-left languages. For more information, see GitHub issue [#7661](https://github.com/microsoft/microsoft-ui-xaml/issues/7661).
- Fixed an issue causing an app to crash on shutdown when resources were torn down in a bad order. For more information, see GitHub issue [#7924](https://github.com/microsoft/microsoft-ui-xaml/issues/7924).

### Version 1.3.2 (1.3.230602002)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.3 release.

- Fixed a crash when setting a Protected Cursor.
- Fixed a performance issue in XamlMetadataProvider during app startup. For more information, see GitHub issue [#8281](https://github.com/microsoft/microsoft-ui-xaml/issues/8281).
- Fixed an issue with hyperlinks and touch in a RichTextBlock. For more information, see GitHub issue [#6513](https://github.com/microsoft/microsoft-ui-xaml/issues/6513).
- Fixed an issue with scrolling and touchpads in WebView2. For more information, see GitHub issue [#7772](https://github.com/microsoft/microsoft-ui-xaml/issues/7772).
- Fixed an issue where an update of Windows App SDK sometimes required a restart of Visual Studio. For more information, see GitHub issue [#3554](https://github.com/microsoft/microsoft-ui-xaml/issues/3554).
- Fixed a noisy exception on shutdown when running in a debugger.

### Version 1.3.1 (1.3.230502000)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.3 release.
- Fixed issue causing apps to crash when setting the SystemBackdrop if the Content was null. For more information, see GitHub issue [#8416](https://github.com/microsoft/microsoft-ui-xaml/issues/8416).
- Fixed issue causing apps to crash when setting the Window Title in XAML, a new capability added in 1.3.0. For more information, see GitHub issue [#3689](https://github.com/microsoft/microsoft-ui-xaml/issues/3689).
- Fixed issue where a window incorrectly took focus when its content changed.
- Fixed an issue with creating C++ projects with the WinAppSDK 1.3 project templates.
- Updated templates on Visual Studio Marketplace

### Version 1.3

The following sections describe new and updated features and known issues for version 1.3.

In an existing Windows App SDK 1.2 app, you can update your Nuget package to 1.3.230331000 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

### XAML Backdrop APIs
With properties built in to the XAML Window, Mica & Background Acrylic backdrops are now easier to use in your WinUI 3 app.
See the [System Backdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.systembackdrop) and [Mica Backdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.micabackdrop) API docs for more information about the Xaml Backdrop properties.

```csharp
public MainWindow()
{
    this.InitializeComponent();

    this.SystemBackdrop = new MicaBackdrop();
}
```

### Window.AppWindow
Replacing several lines of boilerplate code, you're now able to use AppWindow APIs directly from an **Window** through [`Window.AppWindow`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow).

### New features from across WinAppSDK
- `ApplicationModel.DynamicDependency`: `PackageDependency.PackageGraphRevisionId` that replaces the deprecated MddGetGenerationId.
- Environment Manager: [`EnvironmentManager.AreChangesTracked`](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.environmentmanager.arechangestracked) to inform you whether changes to the environment manager are able to be tracked in your application.
- A new event, DebugSettings.XamlResourceReferenceFailed is now raised when a referenced Static/ThemeResource lookup can't be resolved. This event gives access to a trace that details where the framework searched for that key in order to better enable you to debug Static & ThemeResource lookup failures. For more information, see the [Tracing XAML resource reference lookup failures](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/xaml-resource-references-tracing-spec.md) API spec on GitHub.

### Other updates
- See our [WinAppSDK 1.3 milestone](https://github.com/microsoft/WindowsAppSDK/milestone/14?closed=1) on the [WinAppSDK GitHub](https://github.com/microsoft/WindowsAppSDK) for additional issues addressed in this release.
- See our [WinUI 3 in WinAppSDK 1.3 milestone](https://github.com/microsoft/microsoft-ui-xaml/milestone/18?closed=1) on the [microsoft-ui-xaml GitHub](https://github.com/microsoft/microsoft-ui-xaml) for additional issues addressed in this release.
- With the latest experimental VSIX, you're now able to convert your app between unpackaged and packaged through the Visual Studio menu instead of in your project file.

### Known issue
Due to a recent change to the xaml compiler, an existing project that upgrades to 1.3 may experience a build error like the following within Visual Studio:

```console
> C:\Users\user\\.nuget\packages\microsoft.windowsappsdk\\**1.3.230331000**\buildTransitive\Microsoft.UI.Xaml.Markup.Compiler.interop.targets(537,17): error MSB4064: The "PrecompiledHeaderFile" parameter is not supported by the "CompileXaml" task loaded from assembly: Microsoft.UI.Xaml.Markup.Compiler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=de31ebe4ad15742b from the path: C:\Users\user\\.nuget\packages\microsoft.windowsappsdk\\**1.2.230118.102**\tools\net472\Microsoft.UI.Xaml.Markup.Compiler.dll. Verify that the parameter exists on the task, the <UsingTask> points to the correct assembly, and it is a settable public instance property.
```

This is caused by Visual Studio using a cached xaml compiler task dll from 1.2, but driving it with incompatible MSBuild logic from 1.3, as seen in the error text above.  The workaround is to shut down Visual Studio, restart it, and reload the solution.

## Version 1.2

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

### Version 1.2

The following sections describe new and updated features, limitations, and known issues for 1.2.

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

**Known limitations when developing Widgets**

- Third-party Widgets can only be tested locally on devices enrolled in WIP for this preview release.
- Widgets can only be created for packaged, Win32 apps. Widgets for Progressive Web Apps (PWA) are planned to be supported as part of [Microsoft Edge 108](/deployedge/microsoft-edge-release-schedule).

#### DisplayInformation
Windows desktop apps  can now support High Dynamic Range (HDR) and [Auto Color Management](https://devblogs.microsoft.com/directx/auto-color-management/) (ACM) through the DisplayInformation class in WinAppSDK. The DisplayInformation class enables you to monitor display-related information for an application view. This includes events to allow clients to monitor for changes in the application view affecting which display(s) the view resides on, as well as changes in displays that can affect the application view.

#### WinUI 3
WinUI 3 apps can play audio and video with the [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement) and [**MediaTransportControls**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediatransportcontrols) media playback controls. For more info on how and when to use media controls, see [Media players](/windows/apps/design/controls/media-playback).

WinUI 3 has been updated with the latest controls, styles, and behaviors from WinUI 2.8. These updates include the addition of the [**InfoBadge**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.infobadge) control, improvements to accessibility and high contrast mode, as well as bug fixes across controls. For more details, see the release notes for [WinUI 2.7](/windows/apps/winui/winui2/release-notes/winui-2.7) and [WinUI 2.8](/windows/apps/winui/winui2/release-notes/winui-2.8).

**Fixed issues**
- Acrylic backdrop material with [DesktopAcrylicController](/windows/windows-app-sdk/api/winrt/Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController) is now supported in Windows 10 apps. For more information, check out issue [7112](https://github.com/microsoft/microsoft-ui-xaml/issues/7112) on GitHub.
- Fixed various issues that caused routing of App.UnhandledException to fail. For more information, check out issue [5221](https://github.com/microsoft/microsoft-ui-xaml/issues/5221) on GitHub. Regarding the remaining issues, workarounds are documented at the following GitHub issues and will be resolved in a future 1.2 release:
  - [App_UnhandledException's UnhandledExceptionEventArgs.Exception can only be fetched once - should be cached](https://github.com/microsoft/CsWinRT/issues/1258)
  - [Do_Abi_* event/callback handlers should wrap invocations with RoReportUnhandledError](https://github.com/microsoft/CsWinRT/issues/1259)
- Fixed issue causing ListView styles to regress and change from WinAppSDK 1.1. For more information, check out issue [7666](https://github.com/microsoft/microsoft-ui-xaml/issues/7666) on GitHub.
- Fixed issue causing the incorrect Mica fallback background color to appear when the app is inactive. For more information, check out issue [7801](https://github.com/microsoft/microsoft-ui-xaml/issues/7801) on GitHub.

**Known limitations**
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

**Breaking change**

For push notifications, when making a channel request call, apps will need to use the Azure Object ID instead of the Azure App ID. See [Quickstart: Push notification in the Windows App SDK](/windows/apps/windows-app-sdk/notifications/push-notifications/push-quickstart) for details on finding your Azure Object ID.

**Fixed issue**

[**PushNotificationManager.IsSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.pushnotifications.pushnotificationmanager.issupported) will perform a check for elevated mode. It will return `false` if the app is elevated.

**Known limitations (Notifications)**

- In [**AppNotificationScenario**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationscenario), `Urgent` is only supported for Windows builds 19041 and later. You can use [**AppNotificationBuilder.IsUrgentScenarioSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbuilder.isurgentscenariosupported) to check whether the feature is available at runtime.
- In [**AppNotificationButton**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton), `hint-toolTip` and `hint-buttonStyle` are only supported for builds 19041 and later. You can use [**IsButtonStyleSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton.isbuttonstylesupported) and [**IsToolTipSupported**](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder.appnotificationbutton.istooltipsupported) to check whether the feature is available at runtime.
- In [**MediaPlayerElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement), when used in XAML markup for an unpackaged app, the [**Source**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.mediaplayerelement.source) property cannot be set with an ms-appx or ms-resource URI. As an alternative, set the Source using a file URI, or set from code.


#### Windowing

Full title bar customization is now available on Windows 10, version 1809 and later through the [**AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) class. You can set [**AppWindowTitleBar.ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) to `true` to extend content into the title bar area, and [**SetDragRectangles**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.setdragrectangles#microsoft-ui-windowing-appwindowtitlebar-setdragrectangles(windows-graphics-rectint32())) to define drag regions (in addition to other customization options).

If you've been using the [**AppWindowTitleBar.IsCustomizationSupported**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) property to check whether you can call the [**AppWindowTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) APIs, it now returns `true` on supported Windows App SDK Windows 10 versions (1809 and later).

**Known limitations (Windowing)**

Basic title bar customizations are not supported on Windows 10. These include [**BackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.backgroundcolor), [**InactiveBackgroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactivebackgroundcolor), [**ForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.foregroundcolor), [**InactiveForegroundColor**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.inactiveforegroundcolor) and [**IconShowOptions**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions). If you call these properties, they will be ignored silently. All other **AppWindowTitleBar** APIs work in Windows 10, version 1809 and later. For the caption button color APIs (among others) and [**Height**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.height), [**ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) must be set to `true`, otherwise they will also be ignored silently.

#### Access control

Introduced [security.accesscontrol.h](/windows/windows-app-sdk/api/win32/security.accesscontrol/) with the [**GetSecurityDescriptorForAppContainerNames**](/windows/windows-app-sdk/api/win32/security.accesscontrol/nf-security-accesscontrol-getsecuritydescriptorforappcontainernames?branch=main) function to ease and streamline named object sharing between packaged processes and general Win32 APIs. This method takes a list of Package Family Names (PFNs) and access masks, and returns a security descriptor. For more information, see the [GetSecurityDescriptorForAppContainerNames spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/GetSecurityDescriptorForAppContainerNames/GetSecurityDescriptorForAppContainerNames.md) on GitHub.

#### Other limitations and known issues

> [!Important]
> When you reference WinAppSDK 1.2 from a project you might see an error similar to: "_Detected package downgrade: Microsoft.Windows.SDK.BuildTools from 10.0.22621.1 to 10.0.22000.194._", which is caused by incompatible references to the package from the app project and the WinAppSDK package. To resolve this you can update the reference in the project to a more recent and compatible version of Microsoft.Windows.SDK.BuildTools.

- Unit tests may fail with a `REGDB_E_CLASSNOTREG` error in the Tests output pane in Visual Studio. As a workaround, you can add `<WindowsAppContainer>true</WindowsAppContainer>` to your project file.
- .NET PublishSingleFile isn't supported.
- Bootstrapper and Undocked RegFree WinRT auto-initializer defaults is (now) only set for projects that produce an executable (OutputType=Exe or WinExe). This prevents adding auto-initializers into class library DLLs and other non-executables by default.
  - If you need an auto-initializer in a non-executable (e.g. a test DLL loaded by a generic executable that doesn't initialize the Bootstrapper) you can explicitly enable an auto-initializer in your project via `<WindowsAppSdkBootstrapInitialize>true</WindowsAppSdkBootstrapInitialize>` or `<WindowsAppSdkUndockedRegFreeWinRTInitialize>true</WindowsAppSdkUndockedRegFreeWinRTInitialize>`.
- Microsoft.WindowsAppRuntime.Release.Net.dll is always the Arm64 binary and does not work for x86 and x64 apps. When explicitly calling the Bootstrap API do not use the Microsoft.WindowsAppRuntime.Release.Net.dll assembly. As a workaround you can include version constants in this source file distributed with the NuGet package: '..\include\WindowsAppSDK-VersionInfo.cs' or use the auto-initializer.

## Version 1.1

The latest available release of the 1.1.x lineage of the stable channel of the Windows App SDK is version 1.1.5. 1.1.x supports all stable channel features (see the **Features available by release channel** section in [Windows App SDK release channels](./release-channels.md#features-available-by-release-channel)).

### Version 1.1.5

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.1 release.

#### Bug fixes
- Fixed issue where Acrylic does not work if Mica is enabled. For more information, see [issue 7200](https://github.com/microsoft/microsoft-ui-xaml/issues/7200) on GitHub.
- Fixed issue causing apps that depend on the WindowsAppRuntime installer (e.g. unpackaged apps) to fail to run on Windows 10 ARM64 machines. For more information, see [issue 2564](https://github.com/microsoft/WindowsAppSDK/issues/2564) on GitHub.

### Version 1.1.4

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.1 release.

#### Bug fixes
- Fixed regression from 1.0.x causing ListView, TreeView, and other 'List' controls to crash when scrolling with many items. For more information, see [issue 7230](https://github.com/microsoft/microsoft-ui-xaml/issues/7230) on GitHub.
- Fixed issue with DispatcherQueue causing enqueued callbacks to no longer be invoked.
- Fixed issue causing an app crash when calling `DeploymentManager.Initialize` multiple times in same app session.
- Fixed issue causing C# apps to fail to build on Arm64 Visual Studio. For more information, see [issue 7140](https://github.com/microsoft/microsoft-ui-xaml/issues/7140) on GitHub.
- Fixed intermittent crash in XAML imaging code due to incorrect failure handling.
- Fixed memory leak issue when attaching an event handler in ItemsRepeater with a parent UserControl. For more information, see [issue 6123](https://github.com/microsoft/microsoft-ui-xaml/issues/6123) on GitHub.
- Fixed issue causing a build failure in Visual Studio 17.3 when an app project is configured to enable automatic updates of its package when it's sideloaded (i.e. .appinstaller). For more information, see [issue 2773](https://github.com/microsoft/WindowsAppSDK/issues/2773).
- Fixed issue causing Store-distributed packaged apps that call Initialize (necessary e.g. for Push) to call it redundantly as DeploymentManager::GetStatus returned `Package Install Needed` when main and singleton packages are already installed. This caused a perf degradation on app launch.
- Fixed issue causing an exception in single instance apps when the cleanup event was intended to be ignored if it couldn't be opened. For more information, see the [PR](https://github.com/microsoft/WindowsAppSDK/pull/2658) on GitHub.

### Version 1.1.3

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.1 release.

#### Bug fixes
- Fixed related set of issues where XAML crashes when including a ProgressBar, ProgressRing, PipsPager, PersonPicture, or Expander control in the first page of your app. For more information see [issue 7164](https://github.com/microsoft/microsoft-ui-xaml/issues/7164) on GitHub.
- Fixed issue causing the x64 installer to fail to install the Windows App SDK runtime. For more information see [issue 2713](https://github.com/microsoft/WindowsAppSDK/issues/2713) on GitHub.
- Fixed issue causing the WindowsAppRuntime to fail to install if a higher version of the runtime is installed. For more information see [discussion 2708](https://github.com/microsoft/WindowsAppSDK/discussions/2708) on GitHub.

### Version 1.1.2

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.1 release.

#### Bug fixes
- Fixed issue where XAML crashes when closing a window while a dialog is open. For more information see [issue 1032](https://github.com/microsoft/WinUI-Gallery/issues/1032) on GitHub.
- Added `<auto-generated>` tag in C# files to prevent StyleCop warnings. For more information see [issue 4526](https://github.com/microsoft/TemplateStudio/issues/4526) on GitHub.
- Fixed issue causing an access violation error and crash when calling MddBootstrapInitialize when the matching framework package isn't installed. For more information see [issue 2592](https://github.com/microsoft/WindowsAppSDK/issues/2592) on GitHub.
- Fixed issue where the C# WinUI 3 item templates were missing in Visual Studio. For more information see [issue 7148](https://github.com/microsoft/microsoft-ui-xaml/issues/7148) on GitHub.
- Fixed issue where the WindowsAppRuntime installer fails when run as System user. For more information see [issue 2546](https://github.com/microsoft/WindowsAppSDK/issues/2546#issuecomment-1136746733) on GitHub.

### Version 1.1.1

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.1 release.

#### Bug fixes

- Fixed issue causing apps to sometimes crash during a drag and drop operation. For more information see [issue 7002](https://github.com/microsoft/microsoft-ui-xaml/issues/7002) on GitHub.
- Fixed issue causing the title bar to disappear when switching AppWindowPresenterKind from FullScreen to Default.
- Fixed issue where Bootstrapper APIs like `ApiInformation.IsPropertyPresent` and `ApiInformation.IsMethodPresent` would cause unhandled exceptions in apps that aren't packaged. For more information see [issue 2382](https://github.com/microsoft/WindowsAppSDK/issues/2382) on GitHub.
- Fixed issue causing app freeze when maximizing application with pen input.

### Version 1.1

The following sections describe new and updated features, limitations, and known issues for 1.1.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions (or later) is required: 6.0.202, 6.0.104, 5.0.407, 5.0.213. To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

#### App Lifecycle & Restart
Apps are now able to initiate an explicit restart with specific arguments & state building off of the existing [RegisterApplicationRestart](/windows/win32/api/winbase/nf-winbase-registerapplicationrestart) API to register with the OS to restart in update, hang & reboot scenarios.

**New features:**
- Any packaged or unpackaged desktop app can terminate and restart itself on command, and have access to an arbitrary command-line string for the restarted instance using the `AppInstance.Restart()` API.
  -  This is a lifted and synchronous version of the UWP `RequestRestartAsync()` API which enables restarting with arguments and returns an `AppRestartFailureReason` if the restart is unsuccessful.
  - Check out the [Restart API](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppLifecycle/Restart/restartApi.markdown) docs on GitHub for usage & reference information.

#### WinUI 3
WinUI 3 is the native user experience (UX) framework for Windows App SDK. This release includes new features from WinAppSDK 1.0 as well as several stability improvements from 1.0 & 1.1 preview releases.

**New features:**
- Mica and Background Acrylic are now available for WinUI 3 applications.
  - For more information about these materials, check out [Materials in Windows 11](../design/signature-experiences/materials.md). Check out our sample code for applying Mica in C++ applications at [Using a SystemBackdropController with WinUI 3 XAML](system-backdrop-controller.md) and in C# applications [on GitHub](https://github.com/microsoft/WinUI-Gallery/tree/main/WinUIGallery/ControlPagesSampleCode/SystemBackdrops) as part of the [WinUI 3 Gallery](https://www.microsoft.com/store/productId/9P3JFPWWDZRC).
- First introduced in 1.0.1, we have stabilized and enabled the creation of **multiple windows on the same thread** in WinUI 3 applications. See [issue 5918](https://github.com/microsoft/microsoft-ui-xaml/issues/5918) for more information.

**Fixed bugs:**
- Fixed issue when using Mica where the app would crash when a window is divided equally by two screens. See [issue 7079](https://github.com/microsoft/microsoft-ui-xaml/issues/7079) on GitHub for more info.
- Fixed issue causing C# apps with WebView2 to crash on launch when the C/C++ Runtime (CRT) isn't installed by upgrading the WebView2 SDK from 1020.46 to 1185.39.
- Fixed issue causing some rounded corners to show a gradient when they should be a solid color. For more information see [issue 6076](https://github.com/microsoft/microsoft-ui-xaml/issues/6076) & [issue 6194](https://github.com/microsoft/microsoft-ui-xaml/issues/6194) on GitHub.
- Fixed issue where updated styles were missing from generic.xaml.
- Fixed layout cycle issue causing an app to crash when scrolling to the end of a ListView. For more information see [issue 6218](https://github.com/microsoft/microsoft-ui-xaml/issues/6218) on GitHub.
- Fixed issue where users are unable to drop an element when drag-and-drop is enabled. For more information see [issue 7008](https://github.com/microsoft/microsoft-ui-xaml/issues/7008) on GitHub.

**Known limitations:**
- When using a custom title bar, the caption controls do not change color on theme change.
- XAML crashes when a user closes a window while a dialog is open.
#### Deployment
**New features:**
- Packaged apps can now force deploy the Windows App SDK runtime packages using the [**DeploymentManager.Initialize(DeploymentInitializeOptions) API**](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize#microsoft-windows-applicationmodel-windowsappruntime-deploymentmanager-initialize(microsoft-windows-applicationmodel-windowsappruntime-deploymentinitializeoptions)) or using the --force option with the Windows App Runtime installer.
- There are additional functional extension categories, UnlockedDEHs, available for packaged apps. Check out the [1.1 Preview 3 release notes](preview-channel.md#msix-packaging) for more details. These require the Windows App SDK framework package to be installed. See [Downloads for the Windows App SDK](./downloads.md) to install the runtime.
- Self-contained deployment is supported. Check out the [Windows App SDK deployment overview](../package-and-deploy/deploy-overview.md) for the differences between framework-dependent and self-contained deployment, and how to get started.
- The Bootstrapper API required for apps that don't deploy with MSIX include new options for improved usability and troubleshooting. Please view our documentation for C# apps, [Bootstrapper C# APIs](../api-reference/cs-bootstrapper-apis/index.md?branch=release-appsdk-1.1-stable) and for C++ apps, [mddbootstrapheader.h header](/windows/windows-app-sdk/api/win32/mddbootstrap). For more details, see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](use-windows-app-sdk-run-time.md).

**Known limitations:**
- Running the Windows App Runtime installer (WindowsAppRuntimeInstall.exe) requires sideloading to be enabled. See [issue 2469](https://github.com/microsoft/WindowsAppSDK/issues/2469) on GitHub for more information.
- Creating an MSIX package through the Visual Studio Project menus can crash Visual Studio in some scenarios. This issue will be fixed in Visual Studio version 17.3 Preview 2 and serviced to 17.2. If you encounter this issue, you can work around it by generating an MSIX from the command line, switching to an unpackaged project, or reverting back to Windows App SDK 1.0.
- Self-contained applications packaged with MSIX are unsupported on 1809 causing app crash on launch.

#### Elevation
Apps are now able to run with elevated privilege.

**Known limitations:**
- Elevated support requires the following OS servicing update:
  - Win11 - [May 10, 2022—KB5013943 (OS Build 22000.675)](https://support.microsoft.com/topic/may-10-2022-kb5013943-os-build-22000-675-14aa767a-aa87-414e-8491-b6e845541755)
  - Win10 - [May 10, 2022—KB5013942 (OS Builds 19042.1706, 19043.1706, and 19044.1706)](https://support.microsoft.com/topic/may-10-2022-kb5013942-os-builds-19042-1706-19043-1706-and-19044-1706-60b51119-85be-4a34-9e21-8954f6749504)
- App and Push Notifications for an elevated unpackaged app is not supported.
- Elevated WinUI 3 apps crash when dragging an element during a drag-and-drop interaction.

#### Environment Variable Manager 
Environment Variable Manager is a new API introduced in Windows App SDK 1.1.  Environment Variable Manager allows developers to access, and modify environment variables  in the process, user, and machine scope from one API surface.

If Environment Variable Manager is used from a packaged application, all environment variable operations are recorded.  When the package is removed all environment variable operations are reverted.

**New features:**
- Get and set environment variables in the process, user, and machine scope. 
- Automatic environment variable revert when a package that uses environment variable manager is removed.
- Includes specific APIs for PATH and PATHEXT.

**Known limitations:**
- Only available on Windows 11

#### MRT Core
MRT Core is a streamlined version of the modern Windows [Resource Management System](/windows/uwp/app-resources/resource-management-system) that is distributed as part of the Windows App SDK.

**Fixed issues:**  
- An issue causing resources not to be indexed by default when a resource file is added using the VS UI is fixed in .NET SDK 6.0.300. If using an older .NET SDK version, please continue to use the workaround documented in 1.0's release notes. See [issue 1786](https://github.com/microsoft/WindowsAppSDK/issues/1786) on GitHub for additional information.
- An issue causing the resource URI to not be built correctly in unpackaged C++ WinUI 3 apps was fixed in Visual Studio 2022 17.2. If using an older Visual Studio version, please update Visual Studio to 17.2 to receive this fix.

**Known limitations:**
- In .NET projects, resource files copy-pasted into the project folder aren't indexed on F5 if the app was already built. As a workaround, rebuild the app. See [issue 1503](https://github.com/microsoft/WindowsAppSDK/issues/1503) on GitHub for more info. 

For more information, see [Manage resources with MRT Core](./mrtcore/mrtcore-overview.md).

#### Notifications
Developers of packaged (including packaged with external location) and unpackaged apps can now send Windows notifications.

**New features:**
- Support for app notifications for packaged and unpackaged apps.
  - Developers can send app notifications, also known as toast notifications, locally or from their own cloud service. See [App notifications overview](./notifications/app-notifications/index.md).
- Support for push notifications for packaged and unpackaged apps.
  - Developers can send raw notifications and app notifications from their own cloud service. See [Push notifications overview](./notifications/push-notifications/index.md).

**Known limitations:**
- Sending notifications from an elevated app is not supported. `PushNotificationManager::IsSupported()` will not perform a check for elevated mode.

#### Windowing
For easier programming access to functionality that's implemented in USER32.dll (see [Windows and messages](/windows/win32/api/_winmsg/)), this release surfaces more of that functionality in [`AppWindow`](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) itself.  

**New features:**
- Apps with existing windows have more control over how a window is shown, by calling `AppWindow.ShowOnceWithRequestedStartupState`—the equivalent of `ShowWindow(SW_SHOWDEFAULT)`.
- Apps can show, minimize, or restore a window while specifying whether the window should be activated or not at the time the call is made.
- Apps can now determine specific dimensions for their window's client area size in Win32 coordinates without needing to calculate the non-client area sizing to get a specific client area size.  
- Additional WinRT APIs are available to support z-order management of windows based off of [SetWindowPos’s hWndInsertAfter](/windows/win32/api/winuser/nf-winuser-setwindowpos) functionality.
- Apps drawing custom title bars with [`AppWindowTitleBar.ExtendsContentIntoTitleBar`](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) can set a `PreferredTitleBarHeight` option. You now have a choice between a standard height title bar, or a tall title bar that provides more room for interactive content. See [Title bar](../design/basics/titlebar-design.md) in the Fluent design guidelines for advice about when to use a tall title bar.

**Fixed issues:**
- When full screen presenter is invoked the first time, the window now fits on the entire screen correctly. See [issue 1853](https://github.com/microsoft/WindowsAppSDK/issues/1853) on GitHub for more info.
- Windows created with `AppWindow::GetFromWindowId` have the `OverlappedPresenter` as the default presenter but does not have restrictions in terms of changes to window styles coming from other APIs.  Windows created with AppWindow::Create will have the default Presenter guardrails in place from the start. See [issue 2049](https://github.com/microsoft/WindowsAppSDK/issues/2049) on GitHub for more info.
- Using the `OverlappedPresenter.SetBorderAndTitlebar` API to hide caption buttons and borders would result in a 1px top border when maximized. This has been resolved. See [issue 1693](https://github.com/microsoft/WindowsAppSDK/issues/1693) on GitHub for more info.

**Known limitations:**
- When using the **AppWindowTitlebar** API to customize the colors of the standard title bar, the icon and text is misaligned compared to the standard title bar. For more info, see GitHub [issue 2459](https://github.com/microsoft/WindowsAppSDK/issues/2459).
- When solving GitHub [issue 2049](https://github.com/microsoft/WindowsAppSDK/issues/2049) (seen above), we introduced the following bug: if you apply any **AppWindowPresenter** to an **AppWindow** that you've retrieved from **GetFromWindowId**, then change a window style that's being tracked by that Presenter through calling USER32 APIs, and then try to revert back to the window's previous state by re-applying the default Presenter, the result is a window that has no title bar. If you rely on any Presenter in your app, and use calls to USER32 for changing window styles at the time that a non-default Presenter is applied, then you might need to add a workaround to ensure correct window behavior until this bug is serviced. You can use the following code snippet as a template for how to work around the issue:

    ```csharp
    AppWindow m_appWindow;
    OverlappedPresenter m_defaultPresenter;

    private void EnterFullScreen_Click(object sender, RoutedEventArgs e)
    {
        // Capture the default presenter.
        m_defaultPresenter = m_appWindow.Presenter as OverlappedPresenter;

        // Opt in the default overlapped presenter so it can control various aspects of the AppWindow.
        m_defaultPresenter.IsAlwaysOnTop = m_defaultPresenter.IsAlwaysOnTop;
        m_defaultPresenter.IsResizable = m_defaultPresenter.IsResizable;
        m_defaultPresenter.IsMinimizable = m_defaultPresenter.IsMinimizable;
        m_defaultPresenter.IsMaximizable = m_defaultPresenter.IsMaximizable;
        m_defaultPresenter.SetBorderAndTitleBar(m_defaultPresenter.HasBorder, m_defaultPresenter.HasTitleBar);

        m_appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
    }

    private void ExitFullScreen_Click(object sender, RoutedEventArgs e)
    {
        m_appWindow.SetPresenter(AppWindowPresenterKind.Default);
    }
    ```

#### C#/WinRT
C# Windows Runtime Components, including WinUI custom controls, are now supported. This enables component authors to distribute C#-authored runtime components to any WinRT compatible language (e.g., C++/WinRT). See [Walkthrough—Create a C# component with WinUI 3 controls, and consume it from a C++/WinRT app that uses the Windows App SDK](../develop/platform/csharp-winrt/create-winrt-component-winui-cswinrt.md) and the [sample on GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/CustomControls) to get started.

#### Other limitations and known issues

- Apps that reference a package that depends on WebView2 (like Microsoft.Identity.Client) fail to build. This is caused by conflicting binaries at build time. See [issue 2492](https://github.com/microsoft/WindowsAppSDK/issues/2492) on GitHub for more information.
- Using `dotnet build` with a WinAppSDK C# class library project may see a build error "Microsoft.Build.Packaging.Pri.Tasks.ExpandPriContent task could not be loaded". To resolve this issue set `<EnableMsixTooling>true</EnableMsixTooling>` in your project file.
- The default WinAppSDK templates note that the MaxVersionTested="10.0.19041.0" when it should be "10.0.22000.0". For full support of some features, notably UnlockedDEHs, update the MaxVersionTested to "10.0.22000.0" in your project file.

## Version 1.0

The latest available release of the 1.0.x lineage of the stable channel of the Windows App SDK is version 1.0.4. 1.0.x supports all stable channel features (see the **Features available by release channel** section in [Windows App SDK release channels](./release-channels.md#features-available-by-release-channel)).

### Version 1.0.4

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.0 release.

#### Bug fixes

- Fixed issue causing AppBars, when used as Page.TopAppBar or Page.BottomAppBar to not render on screen.
- Fixed issue where apps with a package name of 12 characters or less that use a WinUI control from MUXControls.dll will immediately crash. For more information, see [issue 6360](https://github.com/microsoft/microsoft-ui-xaml/issues/6360) on GitHub.
- Fixed touch input issues causing problems with keyboard shortcuts and other scenarios. For more information, see [issue 6291](https://github.com/microsoft/microsoft-ui-xaml/issues/6291) on GitHub.
- Fixed issue causing apps packaged with MSIX or deployed as self-contained to fail to deploy.
- Fixed issue causing apps to sometimes crash during a drag and drop operation. For more information see [issue 7002](https://github.com/microsoft/microsoft-ui-xaml/issues/7002) on GitHub.

### Version 1.0.3

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.0 release.

#### Bug fixes

- Fixed issue causing C# apps with WebView2 to crash on launch when the C/C++ Runtime (CRT) isn't installed.
- Fixed touch input issues causing problems with keyboard shortcuts and other scenarios. For more information, see [issue 6291](https://github.com/microsoft/microsoft-ui-xaml/issues/6291) on GitHub.

**Note**: We don't usually add functionality in a servicing release, but this release's WebView2 fix required us to update to the latest version of the WebView2 SDK (1020.46 to 1185.39). See [Release Notes for the WebView2 SDK](/microsoft-edge/webview2/release-notes#10118539) for additional information on WebView2 1.0.1185.39 and [Distribute your app and the WebView2 Runtime](/microsoft-edge/webview2/concepts/distribution) for additional information on the WebView2 Runtime.

### Version 1.0.2

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.0 release.

#### Bug fixes

- Fixed layout cycle issue causing an app to crash when scrolling to the end of a ListView. For more information see [issue 6218](https://github.com/microsoft/microsoft-ui-xaml/issues/6218) on GitHub.
- Fixed issue causing C# apps to crash on launch when the C/C++ Runtime (CRT) isn't installed. However, the CRT is still required for C# apps using WebView2. For more information see [issue 2117](https://github.com/microsoft/WindowsAppSDK/issues/2117) on GitHub.
- Fixed issue where applications with Single-project MSIX did not generate a .appinstaller file. For more information see [issue 1821](https://github.com/microsoft/WindowsAppSDK/issues/1821) on GitHub.
- Fixed issue where WinUI applications did not support .NET 6 `dotnet build`.

### Version 1.0.1

This is a servicing release of the Windows App SDK that includes critical bug fixes and multi-window support for the 1.0 release.

#### Bug fixes

- Fixed issue causing the MddBootstrapAutoinitializer to not compile with enabled ImplicitUsings. For more information see [issue 1686](https://github.com/microsoft/WindowsAppSDK/issues/1686) on GitHub.
- Fixed issue where focus in WebView2 would be unexpectedly lost causing input and selection issues. For more information, see [issue 5615](https://github.com/microsoft/microsoft-ui-xaml/issues/5615) & [issue 5570](https://github.com/microsoft/microsoft-ui-xaml/issues/5570) on GitHub.
- Fixed issue causing the in-app toolbar in Visual Studio to be unclickable when using a custom title bar in a WinUI 3 app.
- Fixed issue causing Snap Layout to not appear when using a custom title bar in a WinUI 3 app. For more information, see [issue 6333](https://github.com/microsoft/microsoft-ui-xaml/issues/6333) & [issue 6246](https://github.com/microsoft/microsoft-ui-xaml/issues/6246) on GitHub.
- Fixed issue causing an exception when setting Window.ExtendsContentIntoTitleBar property when Window.SetTitlebar has been called with a still-loading UIElement.
- Fixed issue where Single-project MSIX apps did not support `dotnet build`.
- Fixed issue causing unpackaged apps to not install after installing a packaged app. For more information, see [issue 1871](https://github.com/microsoft/WindowsAppSDK/issues/1871) on GitHub.
- Fixed issue reducing performance during mouse drag operations.
- Fixed crash when calling GetWindowIdFromWindow() in unpackaged apps. For more information, see [discussion 1891](https://github.com/microsoft/WindowsAppSDK/discussions/1891) on GitHub.

The [limitations and known issues](#other-limitations-and-known-issues) for version 1.0 also apply to version 1.0.1.

Additionally, for apps with custom title bars, we have made changes in this release (and fixed numerous issues) that include fixes to the glass window used for drag&drop operations. 
The recommendation is to use the default values and behaviors (give them a try!). 
If your title bar used margins so that the default caption buttons were interactive, we recommend visualizing your drag region by setting the background of your title bar to red and then adjusting the margins to extend the drag region to the caption controls.

#### New features
We have stabilized and enabled the creation of **multiple windows on the same thread** in WinUI 3 applications. See [issue 5918](https://github.com/microsoft/microsoft-ui-xaml/issues/5918) for more information.

### Version 1.0

The following sections describe new and updated features, limitations, and known issues for 1.0.

#### WinUI 3

WinUI 3 is the native user experience (UX) framework for Windows App SDK. In this release we've added multiple new features from Windows App SDK 0.8 and stabilized issues from 1.0 Preview releases.

**New features and updates**:
 - We've added new controls (PipsPager, Expander, BreadcrumbBar) and updated existing controls to reflect the latest Windows styles from [WinUI 2.6](../winui/winui2/release-notes/winui-2.6.md#visual-style-updates).
 - Single-project MSIX packaging is supported in WinUI by creating a new application using the “Blank App, Packaged…” template. 
 - We now support deploying WinUI 3 apps that aren't packaged on Windows versions 1809 and above. Please view [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md) for additional information.
 - WinUI 3 projects can now set their target version down to Windows 10, version 1809. Previously, they could only be set as low as version 1903.
 - In-app toolbar, Hot Reload, & Live Visual Tree for WinUI packaged apps are supported in Visual Studio 2022 Preview 5 and GA.


**Important limitations**:

- Known issues for **both packaged and unpackaged WinUI applications**:
  - *Run-time error in C++ or C# apps that reference a C++ Windows Runtime Component:* 
    - To resolve, add the below target to the end of the Windows Runtime Component's .vcxproj:

      ```xml
      <Target Name="GetPriIndexName">
      <PropertyGroup>
          <!-- Winmd library targets use the default root namespace of the project for the App package name -->
          <PriIndexName Condition="'$(RootNamespace)' != ''">$(RootNamespace)</PriIndexName>
          <!-- If RootNamespace is empty fall back to TargetName -->
          <PriIndexName Condition="$(PriIndexName) == ''">$(TargetName)</PriIndexName>
      </PropertyGroup>
      </Target>
      ```

     - The expected error will be similar to *WinRT originate error - 0x80004005 : 'Cannot locate resource from 'ms-appx:///BlankPage.xaml'.'.*

- Known issues for **WinUI applications with Single-project MSIX** (Blank App, Packaged template):
  - *Missing Package & Publish menu item until you restart Visual Studio:* When creating a new app with Single-project MSIX in 
  both Visual Studio 2019 and Visual Studio 2022 using the Blank App, Packaged (WinUI 3 in Desktop) project template, 
  the command to publish the project doesn't appear in the menu until you close and re-open Visual Studio.
  - A C# app with Single-project MSIX will not compile without the "C++ (v14x) Universal Windows Platform Tools" 
  optional component installed. See [Install tools for the Windows App SDK](set-up-your-development-environment.md) for additional information.
  - *Potential run-time error in an app with Single-project MSIX that consumes types defined in a referenced Windows Runtime Component:* 
  To resolve, manually add [activatable class entries](/uwp/schemas/appxpackage/how-to-specify-extension-points-in-a-package-manifest) to the appxmanifest.xml.
    - The expected error in C# applications is “COMException: Class not registered (0x80040154 (REGDB_E_CLASSNOTREG)). 
    - The expected error in C++/WinRT applications is “winrt::hresult_class_not_registered”. 

- Known issues for **WinUI 3 apps that aren't packaged** (unpackaged apps):
  - Some APIs require package identity, and aren't supported in unpackaged apps, such as:
    - [ApplicationData](/uwp/api/Windows.Storage.ApplicationData)
    - [StorageFile.GetFileFromApplicationUriAsync](/uwp/api/Windows.Storage.StorageFile.GetFileFromApplicationUriAsync)
    - [StorageFile.CreateStreamedFileFromUriAsync](/uwp/api/windows.storage.storagefile.createstreamedfilefromuriasync)
    - [ApiInformation](/uwp/api/Windows.Foundation.Metadata.ApiInformation) (not supported on Windows 10)
    - [Package.Current](/uwp/api/windows.applicationmodel.package.current)
    - Any API in the [Windows.ApplicationModel.Resources](/uwp/api/windows.applicationmodel.resources) namespace
    - Any API in the [Microsoft.Windows.ApplicationModel.Resources](/uwp/api/windows.applicationmodel.resources.core) namespace

- Known issues for **packaging and deploying WinUI applications**:
  - The `Package` command is not supported in WinUI apps with Single-project MSIX (Blank App, Packaged template). Instead, use the `Package & Publish` command to create an MSIX package.
  - To create a NuGet package from a C# Class Library with the `Pack` command, ensure the active `Configuration` is `Release`.
  - The `Pack` command is not supported in C++ Windows Runtime Components to create a NuGet package.

For more info, or to get started developing with WinUI, see:

- [Windows UI 3 Library (WinUI)](../winui/index.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)

#### Windowing

The Windows App SDK provides an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class that evolves the previous easy-to-use Windows.UI.WindowManagement.AppWindow preview class and makes it available to all Windows apps, including Win32, WPF, and WinForms. 

**New Features**
- [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is a high-level windowing API that allows for easy-to-use windowing scenarios that integrates well with the Windows user experience and with other apps. Represents a high-level abstraction of a system-managed container of the content of an app. This is the container in which your content is hosted, and represents the entity that users interact with when they resize and move your app on screen. For developers familiar with Win32, the AppWindow can be seen as a high-level abstraction of the HWND. 
- [DisplayArea](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.displayarea) represents a high-level abstraction of a HMONITOR, follows the same principles as AppWindow. 
- [DisplayAreaWatcher](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.displayareawatcher) allows a developer to observe changes in the display topology and enumerate DisplayAreas currently defined in the system. 

For more info, see [Manage app windows](windowing/windowing-overview.md).

#### Input

These are the input APIs that support WinUI and provide a lower level API surface for developers to achieve more advanced input interactions.

**New Features**
- Pointer APIs: [PointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint), [PointerPointProperties](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpointproperties), and [PointerEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointereventargs) to support retrieving pointer event information with XAML input APIs.
- [InputPointerSource API](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputpointersource): Represents an object that is registered to report pointer input, and provides pointer cursor and input event handling for XAML's SwapChainPanel API.
- [Cursor API](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputcursor): Allows developers to change the cursor bitmap.
- [GestureRecognizer API](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.gesturerecognizer): Allows developers to recognize certain gestures such as drag, hold, and click when given pointer information.

**Important limitations**
- All [PointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint) static factory functions have been removed: **GetCurrentPoint**, **GetCurrentPointTransformed**, **GetIntermediatePoints**, and **GetIntermediatePointsTransformed**.
- The Windows App SDK doesn't support retrieving **PointerPoint** objects with pointer IDs. Instead, you can use the **PointerPoint** member function **GetTransformedPoint** to retrieve a transformed version of an existing **PointerPoint** object. For intermediate points, you can use the **PointerEventArgs** member functions [**GetIntermediatePoints**](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointereventargs.getintermediatepoints) and **GetTransformedIntermediatePoints**. 
- Direct use of the platform SDK API [**Windows.UI.Core.CoreDragOperation**](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragoperation) will not work with WinUI applications.
- **PointerPoint** properties **RawPosition** and **ContactRectRaw** were removed because they referred to non-predicted values, which were the same as the normal values in the OS. Use [**Position**](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint.position) and [**ContactRect**](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpointproperties.contactrect) instead. Pointer prediction is now handled with the **Microsoft.UI.Input.PointerPredictor** API object.

#### App Lifecycle

Most of the App Lifecycle features already exist in the UWP platform, and have been brought into the Windows App SDK for use by desktop app types, especially unpackaged Console apps, Win32 apps, Windows Forms apps, and WPF apps. The Windows App SDK implementation of these features cannot be used in UWP apps, since there are equivalent features in the UWP platform itself.

[!INCLUDE [UWP migration guidance](./includes/uwp-app-sdk-migration-pointer.md)]

Non-UWP apps can also be packaged into MSIX packages. While these apps can use some of the Windows App SDK App Lifecycle features, they must use the manifest approach where this is available. For example, they cannot use the Windows App SDK **RegisterForXXXActivation** APIs and must instead register for rich activation via the manifest.

All the constraints for packaged apps also apply to WinUI apps, which are packaged, and there are additional considerations as described below.

**Important considerations**:

- Rich activation: [GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs)
  - _Unpackaged apps_: Fully usable.
  - _Packaged apps_: Usable, but these apps can also use the platform `GetActivatedEventArgs`. Note that the platform defines [Windows.ApplicationModel.AppInstance](/uwp/api/windows.applicationmodel.appinstance) whereas the Windows App SDK defines [Microsoft.Windows.AppLifecycle.AppInstance](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance). And while UWP apps can use the `ActivatedEventArgs` classes, such as `FileActivatedEventArgs` and `LaunchActivatedEventArgs`, apps that use the Windows App SDK AppLifecycle feature must use the interfaces not the classes (e.g, `IFileActivatedEventArgs`, `ILaunchActivatedEventArgs`, and so on).
  - _WinUi apps_: WinUI's App.OnLaunched is given a [Microsoft.UI.Xaml.LaunchActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.launchactivatedeventargs), whereas the platform `GetActivatedEventArgs` returns a [Windows.ApplicationModel.IActivatedEventArgs](/uwp/api/Windows.ApplicationModel.Activation.IActivatedEventArgs), and the WindowsAppSDK `GetActivatedEventArgs` returns a [Microsoft.Windows.AppLifecycle.AppActivationArguments](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments) object which can represent a platform `LaunchActivatedEventArgs`.
  - For more info, see [Rich activation](applifecycle/applifecycle-rich-activation.md).

- Register/Unregister for rich activation
  - _Unpackaged apps_: Fully usable.
  - _Packaged apps_: Not usable use the app's MSIX manifest instead.
  - For more info, see [Rich activation](applifecycle/applifecycle-rich-activation.md).

- Single/Multi-instancing
  - _Unpackaged apps_: Fully usable.
  - _Packaged apps_: Fully usable.
  - _WinUI apps_: If an app wants to detect other instances and redirect an activation, it must do so as early as possible, and before initializing any windows, etc. To enable this, the app must define DISABLE_XAML_GENERATED_MAIN, and write a custom Main (C#) or WinMain (C++) where it can do the detection and redirection.
  - [RedirectActivationToAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.redirectactivationtoasync) is an async call, and you should not wait on an async call if your app is running in an STA. For Windows Forms and C# WinUI apps, you can declare Main to be async, if necessary. For C++ WinUI and C# WPF apps, you cannot declare Main to be async, so instead you need to move the redirect call to another thread to ensure you don't block the STA. 
  - For more info, see [App instancing](applifecycle/applifecycle-instancing.md).

- Power/State notifications
  - _Unpackaged apps_: Fully usable.
  - _Packaged apps_: Fully usable.
  - For more info, see [Power management](applifecycle/applifecycle-power.md).

**Known issue**:

- File Type associations incorrectly encode %1 to be %251 when setting the Verb handler's command line template, which crashes unpackaged Win32 apps. You can manually edit the Registry value to be %1 instead as a partial workaround. If the target file path has a space in it, then it will still fail and there is no workaround for that scenario.
- These Single/Multi-instancing bugs will be fixed in an upcoming servicing patch: 
    - AppInstance redirection doesn't work when compiled for x86
    - Registering a key, unregistering it, and re-registering it causes the app to crash


#### DWriteCore

DWriteCore is the Windows App SDK implementation of [DirectWrite](/windows/win32/directwrite/direct-write-portal), which is the DirectX API for high-quality text rendering, resolution-independent outline fonts, and full Unicode text and layout support. DWriteCore is a form of DirectWrite that runs on versions of Windows down to Windows 10, version 1809 (10.0; Build 17763), and opens the door for you to use it cross-platform. 

**Features**
DWriteCore contains all of the features of DirectWrite, with a few exceptions.

**Important limitations**
- DWriteCore does not contain the following DirectWrite features:  
    - Per-session fonts 
    - End-user defined character (EUDC) fonts 
    - Font-streaming APIs 
- Low-level rendering API support is partial. 
- DWriteCore doesn't interoperate with Direct2D, but you can use [IDWriteGlyphRunAnalysis](/windows/win32/api/dwrite/nn-dwrite-idwriteglyphrunanalysis) and [IDWriteBitmapRenderTarget](/windows/win32/api/dwrite/nn-dwrite-idwritebitmaprendertarget).

For more information, see [DWriteCore overview](/windows/win32/directwrite/dwritecore-overview).

#### MRT Core

MRT Core is a streamlined version of the modern Windows [Resource Management System](/windows/uwp/app-resources/resource-management-system) that is distributed as part of the Windows App SDK.

**Important limitations**
- In .NET projects, resource files copy-pasted into the project folder aren't indexed on F5 if the app was already built. As a workaround, rebuild the app. See [issue 1503](https://github.com/microsoft/WindowsAppSDK/issues/1503) for more info.
- In .NET projects, when a resource file is added to the project using the Visual Studio UI, the files may not be indexed by default. See [issue 1786](https://github.com/microsoft/WindowsAppSDK/issues/1786) for more info. To work around this issue, please remove the entries below in the CSPROJ file:
    ```xml 
    <ItemGroup>
        <Content Remove="<image file name>" />
    </ItemGroup>
    <ItemGroup>
        <PRIResource Remove="<resw file name>" />
    </ItemGroup>
    ```
- For unpackaged C++ WinUI apps, the resource URI is not built correctly. To work around this issue, add the following in the vcxproj:
    ```xml 
    <!-- Add the following after <Import Project="$(VCTargetsPath)\Microsoft.Cpp.props" /> -->
    
    <PropertyGroup>
        <AppxPriInitialPath></AppxPriInitialPath>   
    </PropertyGroup>
    ```
For more information, see [Manage resources with MRT Core](mrtcore/mrtcore-overview.md).

#### Deployment

**New Features and updates**
-  You can auto-initialize the Windows App SDK through the `WindowsPackageType project` property to load the Windows App SDK runtime and call the Windows App SDK APIs. See [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md) for instructions.
- Unpackaged apps can deploy Windows App SDK by integrating in the standalone Windows App SDK `.exe` installer into your existing MSI or setup program. For more info, see [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md). 
- Unpackaged .NET apps can also use .NET wrapper for the [bootstrapper API](use-windows-app-sdk-run-time.md) to dynamically take a dependency on the Windows App SDK framework package at run time. For more info about the .NET wrapper, see [.NET wrapper library](use-windows-app-sdk-run-time.md#net-wrapper-for-the-bootstrapper-api). 
- Packaged apps can use the deployment API to verify and ensure that all required packages are installed on the machine. For more info about how the deployment API works, see [Windows App SDK deployment guide for framework-dependent packaged apps](deploy-packaged-apps.md).

**Important limitations**
- The .NET wrapper for the bootstrapper API is only intended for use by unpackaged .NET applications to simplify access to the Windows App SDK.
- Only MSIX packaged apps that are full trust or have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability have the permission to use the deployment API to install the main and singleton package dependencies. Support for partial-trust packaged apps will be coming in later releases. 
- When F5 testing an x86 app which uses the [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) method on an x64 system, ensure that the x64 framework is first installed by running the [WindowsAppRuntimeInstall.exe](https://aka.ms/windowsappsdk/1.0-preview2/msix-installer). Otherwise, you will encounter a **NOT_FOUND** error due to Visual Studio not deploying the x64 framework, which normally occurs through Store deployment or sideloading.


#### Other limitations and known issues

- **No support for Any CPU build configuration**: When [adding the Windows App SDK](use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
- **Upgrading from .NET 5 to .NET 6**: When upgrading in the Visual Studio UI, you might run into build errors. As a workaround, manually update your project file's `TargetFrameworkPackage` to the below:

    ```xml 
        <TargetFramework>net6.0-windows10.0.19041.0</TargetFramework> 
    ```

- **C# Single-project MSIX app doesn't compile if C++ UWP Tools aren't installed.** If you have a C# Single-project MSIX project, then you'll need to install the **C++ (v14x) Universal Windows Platform Tools** optional component.

-  **Subsequent language VSIX fails to install into Visual Studio 2019 when multiple versions of Visual Studio 2019 are installed.** If you have multiple versions of Visual Studio 2019 installed (e.g. Release and Preview) and then install the Windows App SDK VSIX for both C++ *and* C#, the second installation will fail. To resolve, uninstall the Single-project MSIX Packaging Tools for Visual Studio 2019 after the first language VSIX. View [this feedback](https://developercommunity.visualstudio.com/t/Installation-of-a-VSIX-into-both-Release/1582487?entry=myfeedback) for additional information about this issue.

- An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
    3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
    4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.

## Version 0.8

The latest available release of the 0.8.x lineage of the stable channel of the Windows App SDK is version 0.8.12.

> [!NOTE]
> The Windows App SDK was previously known by the code name **Project Reunion**. Some SDK assets in version 0.8 and earlier still use the code name. Some areas of the documentation still use **Project Reunion** when referring to an existing asset or a specified earlier release.

### Version 0.8.12
This is a servicing release of the Windows App SDK that includes critical bug fixes for the 0.8.0 release.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions is required: 5.0.213, 5.0.407, 6.0.104, 6.0.202 (or later). To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

#### Bug fixes:
- Fixed issue where apps with [SwapChainPanel](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.swapchainpanel) or [WebView2](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.webview2) would unpredictably crash due to an access violation.

### Version 0.8.11
This is a servicing release of the Windows App SDK that includes critical bug fixes for the 0.8.0 release.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions is required: 5.0.213, 5.0.407, 6.0.104, 6.0.202 (or later). To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

#### Bug fixes:
- Fixed regression causing the lost focus event to fire when selecting text using mouse.

### Version 0.8.10
This is a servicing release of the Windows App SDK that includes critical bug fixes for the 0.8.0 release.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions is required: 5.0.213, 5.0.407, 6.0.104, 6.0.202 (or later). To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

#### Bug fixes:
- Fixed issues causing apps to sometimes crash during a drag and drop operation.

> [!NOTE]
> Windows App SDK 0.8.9 was not released. The version released directly after 0.8.8 is 0.8.10.

### Version 0.8.8
This is a servicing release of the Windows App SDK that includes critical bug fixes for the 0.8.0 release.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions (or later) is required: 6.0.202, 6.0.104, 5.0.407, 5.0.213. To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

#### Bug fixes:
- Fixed touch input issues in TextBox regarding soft keyboard and general interaction. These issues also affected keyboard shortcuts. For more information, see [issue 6291](https://github.com/microsoft/microsoft-ui-xaml/issues/6291) on GitHub. 
- Fixed issue where an app window would sometimes show as inactive when active.
- Fixed performance issue caused by UIA (UI Automation) running in external processes.
- Fixed app stability issue with pen input.
- Fixed issue where the render of png icons in a Menu are dramatically delayed because of UIA.

### Version 0.8.7

This is a servicing release of the Windows App SDK that includes several performance updates for C#/.NET applications. To update to this version, you'll need to reference the latest Windows SDK package version. To do that, add the property `<WindowsSdkPackageVersion>10.0.<sdk_version>.24</WindowsSdkPackageVersion>` to your `.csproj` file with the SDK version your app is targeting from the `TargetFramework` property. For example:

 ```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net6.0-windows10.0.19041.0</TargetFramework>
        <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
        <WindowsSdkPackageVersion>10.0.19041.24</WindowsSdkPackageVersion>
    <PropertyGroup>
    ...
 ```

This version of the Windows SDK projection will be available in an upcoming .NET 6 servicing release. After that .NET SDK update is available, you should remove the `<WindowsSdkPackageVersion>` property from your project file. 

If you don't set this property, then you'll see an error like: `"Error: This version of Project Reunion requires WinRT.Runtime.dll version 1.6 or greater."`

### Version 0.8.6

This is a servicing release of the Windows App SDK that includes several performance improvements for C#/.NET applications for the 0.8.0 release. 

To update to this version of Windows App SDK, you will need to have the latest .NET SDK December update installed (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)). If you don't have the minimum required version of the .NET SDK installed, then you''ll see an error like `"Error: This version of Project Reunion requires WinRT.Runtime.dll version 1.4 or greater."`

#### Bug Fixes
For a detailed list of the performance improvements, see the [C#/WinRT 1.4.1 release notes](https://github.com/microsoft/CsWinRT/releases/tag/1.4.1.211117.1). 

### Version 0.8.5

This is a servicing release of the Windows App SDK that includes more critical bug fixes for the 0.8.0 release. 

#### Bug fixes

- Fixed issue that was causing WinUI apps using [pointer input](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.input.pointer) to crash. 
- Fixed issue causing the title bar buttons (min, max, close) to not have rounded corners on Windows 11. 
- Fixed issue causing the resizing layout options to not appear when hovering over maximize/restore button on Windows 11. 
- Fixed issue causing a crashing exception where creating a [PointCollection](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.pointcollection) object. For more information, see [issue 971](https://github.com/microsoft/CsWinRT/issues/971) on Github. 

The limitations and known issues for version 0.8 also apply to version 0.8.5, unless marked otherwise in the [section below](#limitations).

### Version 0.8.4

This is a servicing release of the Windows App SDK that includes more critical bug fixes for the 0.8.0 release. 

#### Bug fixes

- Fixes for custom title bars so that [ContentDialog](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentdialog) doesn't cover it up, and the title bar buttons are rounded.
- Fix for a crash in image processing when the display scale is changed.
- Fixes clipping bugs where UI disappear or clipped incorrectly

The limitations and known issues for version 0.8 also apply to version 0.8.4, unless marked otherwise in the [section below](#limitations).

### Version 0.8.3

This is a servicing release of the Windows App SDK that includes more critical bug fixes for the 0.8.0 release. 

#### Bug fixes

Keyboard focus was being lost when a window was minimized and then restored, requiring a mouse click to restore focus.

The limitations and known issues for version 0.8 also apply to version 0.8.3, unless marked otherwise in the [section below](#limitations).

### Version 0.8.2

This is a servicing release of the Windows App SDK that includes more critical bug fixes for the 0.8.0 release. 

#### Bug fixes

- Windows App SDK and WinUI 3 are now supported in Visual Studio 2022 Preview 2 and later.
- For .NET apps, you may receive the following error when passing in an array of enums: `Object contains non-primitive or non-blittable data.`
- Writing using the HandWriting Panel inside a textbox causes a crash
- Icons/images always load at their 100% scale value rather than based on the monitor scale value
- Garbage collection of **EventSource\<T\>** causes subsequent failure to unsubscribe handlers (see [GitHub issue](https://github.com/microsoft/CsWinRT/issues/842) for more details)
- Security fix – see [CVE-2021-34533](https://msrc.microsoft.com/update-guide/en-US/vulnerability/CVE-2021-34533) for more details.
- [SwapChainPanel.CompositionScaleChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.swapchainpanel.compositionscalechanged) sometimes returning incorrect CompositionScale values after changing display scale

The limitations and known issues for version 0.8 also apply to version 0.8.2, unless marked otherwise in the [section below](#limitations).

### Version 0.8.1

This is a servicing release of the Windows App SDK that includes a few critical bug fixes for the 0.8.0 release. 

#### Bug fixes

- Windows App SDK cannot run on the latest Windows Insider build
- Crash in EditableComboBox when entering a value that does not appear in dropdown
- WebView2 doesn't allow user to tab out once focused has been received
- Fully qualify Windows.Foundation.Metadata.DefaultOverload namespace in WinUI generated code to avoid namespace ambiguity 
    - This fixes bug [#5108](https://github.com/microsoft/microsoft-ui-xaml/issues/5108).
- Security fix – see [CVE-2021-34489](https://msrc.microsoft.com/update-guide/en-US/vulnerability/CVE-2021-34489) for more details.

The limitations and known issues for version 0.8 also apply to version 0.8.1, unless marked otherwise in the [section below](#limitations).

### Version 0.8.0 Stable 

#### New features and updates

This release supports all [stable channel features](release-channels.md#features-available-by-release-channel).

**WinUI 3**

This release includes many bug fixes and improved stabilization across WinUI 3. These are all of the new changes in WinUI 3 since the release of WinUI 3 - Project Reunion 0.5:

- The Pivot control has been added back in and can now be used in any WinUI 3 app.
- All bug fixes from Project Reunion v0.5.5, v0.5.6, and v0.5.7 are included with this release.
- New bug fixes, including:
    - Mouse right-click in TextBox crashes the application
    - NavigationView causes crash in UWP, Reunion 0.5 Preview
    - ProgressBar doesn't show difference between Paused and Error option
    - Crash in RichEditBox when copying/pasting/changing text style
    - Window caption buttons are misplaced when SetTitleBar is not set or null

    **For the full list of bugs addressed in this release, see [our GitHub repo](https://aka.ms/winui3/0.8/bugs-fixed).** 

- The `ColorHelper.ToDisplayName` API is no longer available. 
- The following types have been removed:
    - `Microsoft.Graphics.IGeometrySource2D`
    - `Microsoft.Graphics.IGeometrySource2DInterop`

    Use `Windows.Graphics.IGeometrySource2D` and `Windows.Graphics.IGeometrySource2DInterop` instead.

- All types in the `Microsoft.System` namespace have been moved to the `Microsoft.UI.Dispatching` namespace, including the [DispatcherQueue class](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue).

- The `AcrylicBrush.BackgroundSource` property has been removed, since `HostBackdrop` is not supported as a `BackgroundSource` in WinUI 3.

For more information on WinUI, see [Windows UI 3 Library (WinUI)](../winui/index.md).

To see WinUI 3 controls and features in action, you can clone and build the WinUI 3 Gallery app [from GitHub](https://github.com/microsoft/WinUI-Gallery/tree/main), or download the app [from the Microsoft Store](https://www.microsoft.com/store/productId/9P3JFPWWDZRC).

To get started developing with WinUI, check out the following articles:
- [WinUI 3 templates in Visual Studio](../winui/winui3/winui-project-templates-in-visual-studio.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Build a C# .NET app with WinUI 3 and Win32 interop](../winui/winui3/desktop-winui3-app-with-basic-interop.md)
- [WinUI 3 API Reference](/windows/winui/api)

**DWriteCore**

This release of DWriteCore includes the following new and updated features. DWriteCore is introduced and described in the [DWriteCore overview](/windows/win32/directwrite/dwritecore-overview).

- DWriteCore now has support for underline&mdash;see [**IDWriteTextLayout::GetUnderline**](/windows/win32/api/dwrite/nf-dwrite-idwritetextlayout-getunderline) and [**IDWriteTextLayout::SetUnderline**](/windows/win32/api/dwrite/nf-dwrite-idwritetextlayout-setunderline).
- Support for strikethrough&mdash;see [**IDWriteTextLayout::GetStrikethrough**](/windows/win32/api/dwrite/nf-dwrite-idwritetextlayout-getstrikethrough) and [**IDWriteTextLayout::SetStrikethrough**](/windows/win32/api/dwrite/nf-dwrite-idwritetextlayout-setstrikethrough).
- Support for vertical text via [**IDWriteTextLayout**](/windows/win32/api/dwrite/nn-dwrite-idwritetextlayout)&mdash;see [Vertical text](/windows/win32/directwrite/vertical-text).
- All of the methods of the [**IDWriteTextAnalyzer**](/windows/win32/api/dwrite/nn-dwrite-idwritetextanalyzer) and [**IDWriteTextAnalyzer1**](/windows/win32/api/dwrite_1/nn-dwrite_1-idwritetextanalyzer1) interfaces are implemented.
- The [**DWriteCoreCreateFactory**](/windows/windows-app-sdk/api/win32/dwrite_core/nf-dwrite_core-dwritecorecreatefactory) free function creates a factory object that is used for subsequent creation of individual DWriteCore objects.

> [!NOTE]
> [**DWriteCoreCreateFactory**](/windows/windows-app-sdk/api/win32/dwrite_core/nf-dwrite_core-dwritecorecreatefactory) is functionally the same as the [**DWriteCreateFactory**](/windows/win32/api/dwrite/nf-dwrite-dwritecreatefactory) function exported by the system version of DirectWrite. The DWriteCore function has a different name to avoid ambiguity in the event that you link both `DWriteCore.lib` and `DWrite.lib`.

For DWriteCore and DirectWrite API reference, see [DWriteCore API Reference](/windows/windows-app-sdk/api/win32/_dwritecore/) and [DirectWrite API Reference](/windows/win32/directwrite/reference).

**MRTCore**

- The **Build Action** for resources is automatically set when you add the resource to your project, reducing the need for manual project configuration.

#### Limitations

- This release is not currently supported on the Dev Channel of the [Windows Insider Program](https://insider.windows.com). **This is fixed in version 0.8.1**.

- Desktop apps (C# or C++ desktop): This release is supported for use only in desktop apps (C++ or C#) that are packaged using MSIX. To use the Windows App SDK in unpackaged desktop apps, you must use the [experimental release channel](experimental-channel.md).

[!INCLUDE [UWP migration guidance](./includes/uwp-app-sdk-migration-pointer.md)]

#### Known issues 

- WinUI 3 tooling such as Live Visual Tree, Live Property Explorer, and Hot Reload in version 0.8 and later requires Visual Studio 2019 16.11 Preview 3 and later.

- Apps currently using WinUI 3 and the Windows App SDK 0.8 cannot use class libraries that use Project Reunion 0.5. Update the class libraries to use the Windows App SDK 0.8.

- .NET apps must target build 18362 or later: Your TFM must be set to net6.0-windows10.0.18362 or later, and your packaging project's must be set to 18362 or later. For more info, see [GitHub issue #921](https://github.com/microsoft/WindowsAppSDK/issues/921).

- You may encounter a crash when switching frequently between light and dark mode.

- For .NET apps, you may receive the following error when passing in an array of enums: `Object contains non-primitive or non-blittable data.` **This is fixed in version 0.8.2**.
    
- For .NET apps, there is currently no way  to opt out of an image getting indexed as an app resource using the Visual Studio UI. To work around this, add a Directory.Build.targets (see [Customize your build - Visual Studio](/visualstudio/msbuild/customize-your-build) for instructions) to the project and remove the image(s) as follows:
 
    - To remove specific images (note that the relative path is needed):
        ```xml
        <Project> 
        <ItemGroup> 
            <Content Remove="..\Bitmap1.bmp" />
        </ItemGroup>
        </Project>
        ```
 
    - To remove images based on metadata: 
        ```xml
        <Project>
        <ItemGroup>
            <Content Remove="@(None->WithMetadataValue('Pack','true'))" />
        </ItemGroup>
        </Project>
        ```

    A fix for this issue is planned for an upcoming release - at that point, the above workarounds will no longer be needed.

## Version 0.5

The latest available release of the 0.5.x lineage of the stable channel of the Windows App SDK is version [0.5.9](https://github.com/microsoft/WindowsAppSDK/discussions/1214).

> [!div class="button"]
> [Download](https://aka.ms/projectreunion/vsixdownload)

### New features and updates

This release supports all [stable channel features](release-channels.md#features-available-by-release-channel).

### Known issues and limitations

This release has the following limitations and known issues:

- **Desktop apps (C# or C++ desktop)**: This release is supported for use only in desktop apps (C++ or C#) that are packaged using MSIX. To use the Windows App SDK in unpackaged desktop apps, you must use the [experimental release channel](experimental-channel.md).
- **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/WindowsAppSDK/issues/921).

[!INCLUDE [UWP migration guidance](./includes/uwp-app-sdk-migration-pointer.md)]

## Related topics

- [Preview channel](preview-channel.md)
- [Experimental channel](experimental-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)
