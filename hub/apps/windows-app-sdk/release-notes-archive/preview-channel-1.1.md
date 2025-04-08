---
title: Preview release channel for the Windows App SDK 1.1
description: Provides info about the preview release channel for the Windows App SDK 1.1.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Preview channel release notes for the Windows App SDK 1.1

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store.

The preview channel includes releases of the Windows App SDK with [preview channel features](../release-channels.md#features-available-by-release-channel) in late stages of development. Preview releases do not include experimental features and APIs but may still be subject to breaking changes before the next stable release.

**Important links:**

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).
- For documentation on preview releases, see [Install tools for preview and experimental channels of the Windows App SDK](../preview-experimental-install.md).

**Latest preview channel release:**

- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Version 1.1 Preview 3 (1.1.0-preview3)

This is the latest release of the preview channel for version 1.1. It supports all preview channel features (see [Features available by release channel](../release-channels.md#features-available-by-release-channel)).

In an existing app using Windows App SDK 1.0, you can update your Nuget package to 1.1.0-preview3 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)). Additionally, see [Latest Windows App SDK downloads](../downloads.md) for the updated runtime and MSIX.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions (or later) is required: 6.0.202, 6.0.104, 5.0.407, 5.0.213. To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: "*This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater.*".

In addition to all of the [Preview 2](#version-11-preview-2-110-preview2) features, the following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3 (1.1.0-preview3)

Mica and Background Acrylic are now available for WinUI 3 applications.

For more information about these materials, check out [Materials in Windows 11](../../design/signature-experiences/materials.md). Check out our sample code for applying Mica in C++ applications at [Apply Mica or Acrylic materials in desktop apps for Windows 11](../system-backdrop-controller.md) and in C# applications [on GitHub](https://github.com/microsoft/WinUI-Gallery/tree/main/WinUIGallery/ControlPagesSampleCode/SystemBackdrops) as part of the WinUI Controls Gallery.

### Notifications (1.1.0-preview3)

**Fixed issues:**

- In 1.1.0-preview1 and 1.1.0-preview2, some unpackaged apps will have seen their app icons incorrectly copied to AppData\LocalMicrosoftWindowsAppSDK. For this release, they will be copied to AppData\Local\Microsoft\WindowsAppSDK instead. To avoid leaking icons, you should manually delete the app icon at the incorrect path after updating to 1.1.0-preview3.
- App icon and app display name retrieval for app notifications via Shortcuts is now supported. This app icon will be prioritized over any icon specified in resource files.
- Support for push notifications for unpackaged apps has been restored (see **Limitations** for noted exception). We've introduced the PushNotificationManager::IsSupported API to check if your app supports push notifications.

**Limitations:**

- Notifications for an *elevated* unpackaged app is not supported. PushNotificationManager::IsSupported will not perform a check for elevated mode. However, we are working on supporting this in a future release.

### MSIX packaging

We've enhanced MSIX adding new and extending existing functionality via the extension categories:

- windows.appExecutionAlias
- windows.customDesktopEventLog
- windows.dataShortcuts
- windows.fileTypeAssociation
- windows.fileTypeAssociation.iconHandler
- windows.folder
- windows.shortcut

These require the Windows App SDK framework package to be installed. See [Latest Windows App SDK downloads](../downloads.md) to install the runtime.

### Environment manager (1.1.0-preview3)

API set that allows developers to add, remove, and modify environment variables without having to directly use the registry API.

Clarification from 1.1 Preview 1: Automatic removal of any environment variable changes when an app that used environment manager is uninstalled is only available for packaged apps. Additionally, reverting environment variable changes requires installation of the Windows App SDK framework package, see [Latest Windows App SDK downloads](../downloads.md) for the runtime.

### Other known limitations

Regressions from 1.1 Preview 2:

- For .NET apps using MRT Core APIs and WinUI apps that *don't* deploy with single-project MSIX:
  - RESW and image files that were added to the project as Existing Items and  previously automatically included to the PRIResource and Content ItemGroups, respectively, won't be included in those ItemGroups. As a result, these resources won't be indexed during PRI generation, so they won't be available during runtime.
    - Workaround: Manually include the resources in the project file and remove them from the None ItemGroup.
    - Alternative workaround: When available, upgrade your apps' .NET SDK to 6.0.300. See [Version requirements for .NET SDK](/dotnet/core/compatibility/sdk/6.0/vs-msbuild-version) for additional information.
- For .NET apps that *don't* deploy with single-project MSIX:
  - If a file is added to the Content ItemGroup twice or more, then there will be a build error.
    - Workaround: Delete the duplicate inclusion/s or set EnableDefaultContentItems to false in the project file.

Both regressions will be restored in the next stable release.

## Version 1.1 Preview 2 (1.1.0-preview2)

This is the second release of the preview channel for version 1.1. It supports all preview channel features (see [Features available by release channel](../release-channels.md#features-available-by-release-channel)).

In an existing app using Windows App SDK 1.0, you can update your Nuget package to 1.1.0-preview2 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)). Additionally, see [Latest Windows App SDK downloads](../downloads.md) for the updated runtime and MSIX.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions (or later) is required: 6.0.202, 6.0.104, 5.0.407, 5.0.213. To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

In addition to all of the [Preview 1](#version-11-preview-1-110-preview1) features, the following sections describe new and updated features, limitations, and known issues for this release.

### Notifications (1.1.0-preview2)

**Fixed issues:**

- An app without package identity sending notifications will now see its app icon in the notification if the icon is a part of the app's resource. If the app resource has no icon, the Windows default app icon is used.
- A WinUI 3 app that's not running can now be background-activated via a notification.

**Regression from 1.1 Preview 1:** Push notifications support for unpackaged apps. Expected to be restored in the next release.

**Known limitations:**

- We've introduced the PushNotificationManager::IsSupported API to check if self-contained apps support push notifications. However, this API is not yet working as intended, so keep an eye out in the next preview release for full support of the IsSupported API.
- Some unpackaged apps will see their app icons incorrectly copied to AppData\LocalMicrosoftWindowsAppSDK. For the next release, they will be copied to AppData\Local\Microsoft\WindowsAppSDK instead. To avoid leaking icons, the developer should manually delete their app icon at the incorrect path after upgrading to the next release.
- App icon and app display name retrieval for notifications via Shortcuts is not supported. But we're working on supporting that in a future release.

### Deployment

**New features:**

- Packaged apps can now force deploy the Windows App SDK runtime packages using the **DeploymentManager.Initialize** API.
- The Bootstrapper API now includes new options for improved usability and troubleshooting. For more details, see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](../use-windows-app-sdk-run-time.md) and [Rich information on Bootstrap initialization failure](https://github.com/microsoft/WindowsAppSDK/pull/2316).

**Known limitations:**

- Self-contained deployment is supported only on Windows 10, 1903 and later.

### Windowing

For easier programming access to functionality that's implemented in `USER32.dll` (see [Windows and messages](/windows/win32/api/_winmsg/)), this release surfaces more of that functionality in [**AppWindow**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) itself.

**New features:**

- Apps with existing windows have more control over how a window is shown, by calling **AppWindow.ShowOnceWithRequestedStartupState**&mdash;the equivalent of `ShowWindow(SW_SHOWDEFAULT)`.
- Apps can show, minimize, or restore a window and specify whether the window should be activated or not at the time the call is made.
- Apps can now set a window's client area size in Win32 coordinates.
- We've added APIs to support z-order management of windows.
- Apps drawing custom titlebars with [**AppWindowTitleBar.ExtendsContentIntoTitleBar**](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.extendscontentintotitlebar) can set a *PreferredTitleBarHeight* option. You have a choice of a *standard height* titlebar, or a *tall* titlebar that provides more room for interactive content. See [Title bar](../../design/basics/titlebar-design.md) in the Fluent design guidelines for advice about when to use a tall titlebar.

**Known limitations:**

- Tall titlebar support is available only on Windows 11. We are working to bring this downlevel along with other custom titlebar APIs.

### WinUI 3 (1.1.0-preview2)

**Fixed issues:**

- Fixed issue causing C# apps with WebView2 to crash on launch when the C/C++ Runtime (CRT) isn't installed by upgrading the WebView2 SDK from 1020.46 to 1185.39.
- Fixed issue causing some rounded corners to show a gradient when they should be a solid color. For more information see [issue 6076](https://github.com/microsoft/microsoft-ui-xaml/issues/6076) & [issue 6194](https://github.com/microsoft/microsoft-ui-xaml/issues/6194) on GitHub.
- Fixed issue where updated styles were missing from generic.xaml.
- Fixed layout cycle issue causing an app to crash when scrolling to the end of a ListView. For more information see [issue 6218](https://github.com/microsoft/microsoft-ui-xaml/issues/6218) on GitHub.

### Performance

C# applications have several performance improvements. For more details, see the [C#/WinRT 1.6.1 release notes](https://github.com/microsoft/CsWinRT/releases/tag/1.6.1.220314.1).

## Version 1.1 Preview 1 (1.1.0-preview1)

This is the first release of the preview channel for version 1.1. It supports all preview channel features (see [Features available by release channel](../release-channels.md#features-available-by-release-channel)).

In an existing app using Windows App SDK 1.0, you can update your Nuget package to 1.1.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)). Additionally, see [Latest Windows App SDK downloads](../downloads.md) for the updated runtime and MSIX.

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3 (1.1.0-preview1)

**Known issue**: Users are unable to drop an element when drag-and-drop is enabled.

### Elevated (admin) support

Using Windows App SDK 1.1 Preview 1, apps (including WinUI 3) will be able to run with elevated privilege.

**Important limitations:**

- Currently available only on Windows 11. But we're evaluating bringing this support downlevel in a later release.

**Known issues:**

- WinUI 3 apps crash when dragging an element during a drag-and-drop interaction.

### Self-contained deployment

Windows App SDK 1.1 will introduce support for self-contained deployment. Our [Windows App SDK deployment overview](../../package-and-deploy/deploy-overview.md) details the differences between framework-dependent and self-contained deployment, and how to get started.

**Known issues:**

- A C++ app that's packaged needs to add the below to the bottom of its project file to work around a bug in the self-contained `.targets` file that removes framework references to VCLibs:

    ```xml
    <PropertyGroup>
        <IncludeGetResolvedSDKReferences>true</IncludeGetResolvedSDKReferences>
    </PropertyGroup>

    <Target Name="_RemoveFrameworkReferences"
        BeforeTargets="_ConvertItems;_CalculateInputsForGenerateCurrentProjectAppxManifest">
        <ItemGroup>
            <FrameworkSdkReference Remove="@(FrameworkSdkReference)" Condition="'%(FrameworkSdkReference.SDKName)' == 'Microsoft.WindowsAppRuntime.1.1-preview1'" />
        </ItemGroup>
    </Target>
     ```

- Supported only on Windows 10, 1903 and later.

### Notifications (1.1.0-preview1)

Developers of packaged (including packaged with external location) and unpackaged apps can now send Windows notifications.

**New features:**

- Support for app notifications for packaged and unpackaged apps. Full details on [GitHub](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppNotifications/AppNotifications-spec.md)
  - Developers can send app notifications, also known as toast notifications, locally or from their own cloud service.
- Support for push notification for packaged and unpackaged apps. Full details on [GitHub](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/PushNotifications/PushNotifications-spec.md)
  - Developers can send raw notifications or app notifications from their own cloud service.

**Limitations:**

- Apps published as self-contained may not have push notifications support. Keep an eye out in the next preview release for an **IsSupported** API to check for push notifications support.
- Apps that are unpackaged sending app notifications will not see their app icon in the app notification unless they are console applications. Console apps that are unpackaged should follow the patterns shown in the [ToastNotificationsDemoApp](https://github.com/microsoft/WindowsAppSDK/blob/main/test/TestApps/ToastNotificationsDemoApp/main.cpp) sample.
- Windows App SDK runtime must be installed to support push notifications, see [Latest Windows App SDK downloads](../downloads.md) for the installer.
- A WinUI 3 app that's not running can't be background-activated via a notification. But we're working on supporting that in a future release.

### Environment manager (1.1.0-preview1)

API set that allows developers to add, remove, and modify environment variables without having to directly use the registry API.

**New features:**

- Provides automatic removal of any environment variables changes when an app that used environment manager is uninstalled.

**Limitations:**

- Currently unavailable in C# apps. But we're evaluating bringing this feature to C# apps in a later release.

### Other limitations and known issues

- If you're using C# with 1.1.0 Preview 1, then you must use one of the following .NET SDK versions at a minimum: .NET SDK 6.0.201, 6.0.103, 5.0.212, or 5.0.406. To upgrade your .NET SDK, you can update to the latest version of Visual Studio, or visit [Download .NET](https://dotnet.microsoft.com/download).

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
