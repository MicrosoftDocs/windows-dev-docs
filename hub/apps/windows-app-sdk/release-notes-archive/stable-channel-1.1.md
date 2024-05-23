---
title: Stable channel release notes for the Windows App SDK 1.1
description: Provides information about the stable release channel for the Windows App SDK 1.1.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Stable channel release notes for the Windows App SDK 1.1

The stable channel provides releases of the Windows App SDK that are supported for use by apps in production environments. Apps that use the stable release of the Windows App SDK can also be published to the Microsoft Store.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Downloads for the Windows App SDK

> [!NOTE]
> The Windows App SDK Visual Studio Extensions (VSIX) are no longer distributed as a separate download. They are available in the Visual Studio Marketplace inside Visual Studio.

## Version 1.1

The latest available release of the 1.1.x lineage of the stable channel of the Windows App SDK is version 1.1.5. 1.1.x supports all stable channel features (see the **Features available by release channel** section in [Windows App SDK release channels](../release-channels.md#features-available-by-release-channel)).

### Version 1.1.5

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.1 release.

#### Bug fixes (1.1.5)

- Fixed issue where Acrylic does not work if Mica is enabled. For more information, see [issue 7200](https://github.com/microsoft/microsoft-ui-xaml/issues/7200) on GitHub.
- Fixed issue causing apps that depend on the WindowsAppRuntime installer (e.g. unpackaged apps) to fail to run on Windows 10 ARM64 machines. For more information, see [issue 2564](https://github.com/microsoft/WindowsAppSDK/issues/2564) on GitHub.

### Version 1.1.4

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.1 release.

#### Bug fixes (1.1.4)

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

#### Bug fixes (1.1.3)

- Fixed related set of issues where XAML crashes when including a ProgressBar, ProgressRing, PipsPager, PersonPicture, or Expander control in the first page of your app. For more information see [issue 7164](https://github.com/microsoft/microsoft-ui-xaml/issues/7164) on GitHub.
- Fixed issue causing the x64 installer to fail to install the Windows App SDK runtime. For more information see [issue 2713](https://github.com/microsoft/WindowsAppSDK/issues/2713) on GitHub.
- Fixed issue causing the WindowsAppRuntime to fail to install if a higher version of the runtime is installed. For more information see [discussion 2708](https://github.com/microsoft/WindowsAppSDK/discussions/2708) on GitHub.

### Version 1.1.2

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.1 release.

#### Bug fixes (1.1.2)

- Fixed issue where XAML crashes when closing a window while a dialog is open. For more information see [issue 1032](https://github.com/microsoft/WinUI-Gallery/issues/1032) on GitHub.
- Added `<auto-generated>` tag in C# files to prevent StyleCop warnings. For more information see [issue 4526](https://github.com/microsoft/TemplateStudio/issues/4526) on GitHub.
- Fixed issue causing an access violation error and crash when calling MddBootstrapInitialize when the matching framework package isn't installed. For more information see [issue 2592](https://github.com/microsoft/WindowsAppSDK/issues/2592) on GitHub.
- Fixed issue where the C# WinUI 3 item templates were missing in Visual Studio. For more information see [issue 7148](https://github.com/microsoft/microsoft-ui-xaml/issues/7148) on GitHub.
- Fixed issue where the WindowsAppRuntime installer fails when run as System user. For more information see [issue 2546](https://github.com/microsoft/WindowsAppSDK/issues/2546#issuecomment-1136746733) on GitHub.

### Version 1.1.1

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.1 release.

#### Bug fixes (1.1.1)

- Fixed issue causing apps to sometimes crash during a drag and drop operation. For more information see [issue 7002](https://github.com/microsoft/microsoft-ui-xaml/issues/7002) on GitHub.
- Fixed issue causing the title bar to disappear when switching AppWindowPresenterKind from FullScreen to Default.
- Fixed issue where Bootstrapper APIs like `ApiInformation.IsPropertyPresent` and `ApiInformation.IsMethodPresent` would cause unhandled exceptions in apps that aren't packaged. For more information see [issue 2382](https://github.com/microsoft/WindowsAppSDK/issues/2382) on GitHub.
- Fixed issue causing app freeze when maximizing application with pen input.

### New and updated features and known issues for 1.1

The following sections describe new and updated features, limitations, and known issues for 1.1.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions (or later) is required: 6.0.202, 6.0.104, 5.0.407, 5.0.213. To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

#### App Lifecycle & Restart

Apps are now able to initiate an explicit restart with specific arguments & state building off of the existing [RegisterApplicationRestart](/windows/win32/api/winbase/nf-winbase-registerapplicationrestart) API to register with the OS to restart in update, hang & reboot scenarios.

**New features:**

- Any packaged or unpackaged desktop app can terminate and restart itself on command, and have access to an arbitrary command-line string for the restarted instance using the `AppInstance.Restart()` API.
  - This is a lifted and synchronous version of the UWP `RequestRestartAsync()` API which enables restarting with arguments and returns an `AppRestartFailureReason` if the restart is unsuccessful.
  - Check out the [Restart API](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppLifecycle/Restart/restartApi.markdown) docs on GitHub for usage & reference information.

#### WinUI 3

WinUI 3 is the native user experience (UX) framework for Windows App SDK. This release includes new features from WinAppSDK 1.0 as well as several stability improvements from 1.0 & 1.1 preview releases.

**New features:**

- Mica and Background Acrylic are now available for WinUI 3 applications.
  - For more information about these materials, check out [Materials in Windows 11](../../design/signature-experiences/materials.md). Check out our sample code for applying Mica in C++ applications at [Apply Mica or Acrylic materials in desktop apps for Windows 11](../system-backdrop-controller.md) and in C# applications [on GitHub](https://github.com/microsoft/WinUI-Gallery/tree/main/WinUIGallery/ControlPagesSampleCode/SystemBackdrops) as part of the [WinUI 3 Gallery](https://www.microsoft.com/store/productId/9P3JFPWWDZRC).
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
- There are additional functional extension categories, UnlockedDEHs, available for packaged apps. Check out the [1.1 Preview 3 release notes](preview-channel-1.1.md#msix-packaging) for more details. These require the Windows App SDK framework package to be installed. See [Latest Windows App SDK downloads](../downloads.md) to install the runtime.
- Self-contained deployment is supported. Check out the [Windows App SDK deployment overview](../../package-and-deploy/deploy-overview.md) for the differences between framework-dependent and self-contained deployment, and how to get started.
- The Bootstrapper API required for apps that don't deploy with MSIX include new options for improved usability and troubleshooting. Please view our documentation for C# apps, [Bootstrapper C# APIs](/windows/apps/api-reference/cs-bootstrapper-apis/) and for C++ apps, [mddbootstrapheader.h header](/windows/windows-app-sdk/api/win32/mddbootstrap). For more details, see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](../use-windows-app-sdk-run-time.md).

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

For more information, see [Manage resources with MRT Core](../mrtcore/mrtcore-overview.md).

#### Notifications

Developers of packaged (including packaged with external location) and unpackaged apps can now send Windows notifications.

**New features:**

- Support for app notifications for packaged and unpackaged apps.
  - Developers can send app notifications, also known as toast notifications, locally or from their own cloud service. See [App notifications overview](../notifications/app-notifications/index.md).
- Support for push notifications for packaged and unpackaged apps.
  - Developers can send raw notifications and app notifications from their own cloud service. See [Push notifications overview](../notifications/push-notifications/index.md).

**Known limitations:**

- Sending notifications from an elevated app is not supported. `PushNotificationManager::IsSupported()` will not perform a check for elevated mode.

#### Windowing

For easier programming access to functionality that's implemented in USER32.dll (see [Windows and messages](/windows/win32/api/_winmsg/)), this release surfaces more of that functionality in [`AppWindow`](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) itself.  

**New features:**

- Apps with existing windows have more control over how a window is shown, by calling `AppWindow.ShowOnceWithRequestedStartupState`—the equivalent of `ShowWindow(SW_SHOWDEFAULT)`.
- Apps can show, minimize, or restore a window while specifying whether the window should be activated or not at the time the call is made.
- Apps can now determine specific dimensions for their window's client area size in Win32 coordinates without needing to calculate the non-client area sizing to get a specific client area size.  
- Additional WinRT APIs are available to support z-order management of windows based off of [SetWindowPos's hWndInsertAfter](/windows/win32/api/winuser/nf-winuser-setwindowpos) functionality.
- Apps drawing custom title bars with [`AppWindowTitleBar.ExtendsContentIntoTitleBar`](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) can set a `PreferredTitleBarHeight` option. You now have a choice between a standard height title bar, or a tall title bar that provides more room for interactive content. See [Title bar](../../design/basics/titlebar-design.md) in the Fluent design guidelines for advice about when to use a tall title bar.

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

C# Windows Runtime Components, including WinUI custom controls, are now supported. This enables component authors to distribute C#-authored runtime components to any WinRT compatible language (e.g., C++/WinRT). See [Walkthrough—Create a C# component with WinUI 3 controls, and consume it from a C++/WinRT app that uses the Windows App SDK](../../develop/platform/csharp-winrt/create-winrt-component-winui-cswinrt.md) and the [sample on GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/CustomControls) to get started.

#### Other limitations and known issues

- Apps that reference a package that depends on WebView2 (like Microsoft.Identity.Client) fail to build. This is caused by conflicting binaries at build time. See [issue 2492](https://github.com/microsoft/WindowsAppSDK/issues/2492) on GitHub for more information.
- Using `dotnet build` with a WinAppSDK C# class library project may see a build error "Microsoft.Build.Packaging.Pri.Tasks.ExpandPriContent task could not be loaded". To resolve this issue set `<EnableMsixTooling>true</EnableMsixTooling>` in your project file.
- The default WinAppSDK templates note that the MaxVersionTested="10.0.19041.0" when it should be "10.0.22000.0". For full support of some features, notably UnlockedDEHs, update the MaxVersionTested to "10.0.22000.0" in your project file.

## Related topics

- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
