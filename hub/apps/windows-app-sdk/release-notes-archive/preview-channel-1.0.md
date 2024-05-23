---
title: Preview release channel for the Windows App SDK 1.0
description: Provides info about the preview release channel for the Windows App SDK 1.0.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Preview channel release notes for the Windows App SDK 1.0

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

## Version 1.0 Preview 3 (1.0.0-preview3)

Preview 3 is the latest release of the preview channel for version 1.0 of the Windows App SDK. Preview 3 supports all [preview channel features](../release-channels.md#features-available-by-release-channel).

### Download 1.0 Preview 3 Visual Studio extensions (VSIX)

> [!NOTE]
> If you have Windows App SDK Visual Studio extensions (VSIX) already installed, then uninstall them before installing a new version. For directions, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

From the table below you can download the Visual Studio extensions (VSIX) for the 1.0 Preview 3 release. For all versions, see [Latest Windows App SDK downloads](../downloads.md). If you haven't done so already, start by configuring your development environment, using the steps in [Install tools for the Windows App SDK](../set-up-your-development-environment.md?tabs=preview).

The extensions below are tailored for your programming language and version of Visual Studio.

| **1.0 Preview 3 downloads** | **Description** |
| ----------- | ----------- |
| [C# Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/csharp) | Build C# apps with the Windows App SDK Visual Studio 2019 extension. |
| [C++ Visual Studio 2019 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2019/cpp) |  Build C++ apps with the Windows App SDK Visual Studio 2019 extension. |
| [C# Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/csharp) | Build C# apps with the Windows App SDK Visual Studio 2022 extension. |
| [C++ Visual Studio 2022 extension](https://aka.ms/windowsappsdk/1.0-preview3/extension/VS2022/cpp) | Build C++ apps with the Windows App SDK Visual Studio 2022 extension. |
| [The `.exe` installer, and MSIX packages](https://aka.ms/windowsappsdk/1.0-preview3/msix-installer) | Deploy the Windows App SDK with your app using the `.exe` installer, and MSIX packages. |

The following sections describe new and updated features, limitations, and known issues for 1.0 Preview 3.

### WinUI 3 (1.0.0-preview3)

We now support deploying WinUI 3 apps without MSIX packaging. See [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md) to configure your WinUI 3 application to support unpackaged deployment.

**Important limitations:**

- Unpackaged WinUI 3 applications are **supported only on Windows versions 1909 and later**.
- Unpackaged WinUI 3 applications are **supported on x86 and x64**; arm64 support will be added in the next stable release.
- **Single-project MSIX Packaging Tools** for [Visual Studio 2019](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools) or [Visual Studio 2022](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingToolsDev17) is required for unpackaged apps.
- In an unpackaged app, you might receive a prompt to install .NET 3.5; if you do, then you can ignore it.
- Some APIs are not currently supported in unpackaged apps. We're aiming to fix this in the next stable release. A few examples:
  - [ApplicationData](/uwp/api/Windows.Storage.ApplicationData)
  - [StorageFile.GetFileFromApplicationUriAsync](/uwp/api/Windows.Storage.StorageFile.GetFileFromApplicationUriAsync)
  - [ApiInformation](/uwp/api/Windows.Foundation.Metadata.ApiInformation) (not supported on Windows 10)
  - [Package.Current](/uwp/api/windows.applicationmodel.package.current)
- ListView, CalendarView, and GridView controls are using the incorrect styles, and we're aiming to fix this in the next stable release.

For more info, or to get started developing with WinUI 3, see:

- [Windows UI Library (WinUI)](../../winui/index.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)

**Other limitations and known issues:**

- **Unpackaged apps are not supported on Windows 10 version 1809**. We're aiming to fix this in the next release in the stable channel.

- **C# Single-project MSIX app doesn't compile if C++ UWP Tools aren't installed**. If you have a C# Single-project MSIX project, then you'll need to install the **C++ (v14x) Universal Windows Platform Tools** optional component.

- This release introduces the **Blank App, Packaged (WinUI 3 in Desktop)** project templates for C# and C++. These templates enable you to build your app into an MSIX package without the use of a separate packaging project (see [Package your app using single-project MSIX](../single-project-msix.md)). These templates have some known issues in this release:

  - **Missing Publish menu item until you restart Visual Studio**. When creating a new app in both Visual Studio 2019 and Visual Studio 2022 using the **Blank App, Packaged (WinUI 3 in Desktop)** project template, the command to publish the project doesn't appear in the menu until you close and re-open Visual Studio.

  - **Error when adding C++ static/dynamic library project references to C++ apps using Single-project MSIX Packaging**. Visual Studio displays an error that the project can't be added as a reference because the project types are not compatible.
  
  - **Error when referencing a custom user control in a class library project**. The application will crash with the error that the system can't find the path specified.

  - **C# or C++ template for Visual Studio 2019**. When you try to build the project, you'll encounter the error "The project doesn't know how to run the profile *project name*". To resolve this issue, install the [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools).

  - **C# template for Visual Studio 2019 and Visual Studio 2022**. In Visual Studio when you **Start Debugging** or **Start Without Debugging**, if your app doesn't deploy and run (and there's no feedback from Visual Studio), then click on the project node in **Solution Explorer** to select it, and try again.

  - **C# template for Visual Studio 2019 and Visual Studio 2022**. You will encounter the following error when you try to run or debug your project on your development computer: "The project needs to be deployed before we can debug. Please enable Deploy in the Configuration Manager." To resolve this issue, enable deployment for your project in **Configuration Manager**. For detailed instructions, see the [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md).

  - **C++ template for Visual Studio 2022 version 17.0 releases up to Preview 4**. You will encounter the following error the first time you try to run your project: "There were deployment errors". To resolve this issue, run or deploy your project a second time. This issue will be fixed in Visual Studio 2022 version 17.0 Preview 7.

- **No support for Any CPU build configuration**: When [adding the Windows App SDK](../use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

- **C# projects using 1.0 Preview 3 must use the following .NET SDK**: .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

- An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
    3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
    4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.

## Important issue impacting 1.0 Preview 1 and Preview 2

Version 1.0 Preview 1 and Preview 2 of the Windows App SDK includes a mechanism to clean up any environment variable changes made by a packaged app when that app is uninstalled. This feature is in an experimental state, and the first release includes a known bug that can corrupt the system **PATH** environment variable.

Preview 1 and Preview 2 corrupts any **PATH** environment variable that contains the expansion character `%`. This happens whenever any packaged app is uninstalled, regardless of whether that app uses the Windows App SDK.

Also see [PATH environment variable corruption issue](https://github.com/microsoft/WindowsAppSDK/issues/1599).

### Details

The system **PATH** entry is stored in the **Path** value in the following key in the Windows Registry:

`Computer\HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Environment`

If you launch the Registry Editor (`regedit.exe`), then you can copy and paste the path above into the breadcrumb bar (immediately below the menu bar), and press Enter to locate the key.

The **Path** value of that key *should* be of type **REG_EXPAND_SZ**, but the bug changes it to **REG_SZ**. And that makes the system **PATH** environment variable unusable if it contains the variable expansion character `%`.

### Affected releases

- [Windows App SDK 1.0 Preview 1 (1.0.0-preview1)](#version-10-preview-1-100-preview1)
- [Windows App SDK 1.0 Preview 2 (1.0.0-preview2)](#version-10-preview-2-100-preview2)

### Mitigation

To get your machine back into a good state, take the following steps:

1. Check whether the **PATH** in the Registry is corrupt and, if so, reset it by running the script below.

   You can accomplish step 1 with the following Windows PowerShell script (PowerShell Core won't work). Run it elevated.

   ```Powershell
   # This script must be run from an elevated Windows PowerShell
   # window (right-click Windows PowerShell in the Start menu,
   # and select Run as Administrator).

   # If the PATH in the Registry has been set to REG_SZ, then delete
   # it, and recreate it as REG_EXPAND_SZ.
   
   $EnvPath = 'Registry::HKLM\System\CurrentControlSet\Control\Session Manager\Environment'
   $Environment=Get-Item $EnvPath
   $PathKind = $Environment.GetValueKind('Path')

   if ($PathKind -ne 'ExpandString') {
     $Path = $Environment.GetValue('Path')
     Remove-ItemProperty $EnvPath -Name Path
     New-ItemProperty $EnvPath -Name Path -PropertyType ExpandString -Value $Path
   }
   ```

2. Uninstall all apps that use the Windows App SDK 1.0 Preview1 or Preview2 (see the script below).
3. Uninstall the Windows App SDK 1.0 Preview1/Preview2 packages, including the package that contains the bug (see the script below).

   You can accomplish steps 2 and 3 with the following Windows PowerShell script (PowerShell Core won't work). Run it elevated.

   ```Powershell
   # This script must be run from an elevated Windows PowerShell
   # window (right-click Windows PowerShell in the Start menu,
   # and select Run as Administrator).

   # Remove the Windows App SDK 1.0 Preview1/2, and all apps that use it.

   $winappsdk = "Microsoft.WindowsAppRuntime.1.0-preview*"
   Get-AppxPackage | Where-Object { $_.Dependencies -like $winappsdk } | Remove-AppxPackage
   Get-AppxPackage $winappsdk | Remove-AppxPackage
   ```

### Fix in Windows App SDK 1.0 Preview 3

The feature causing the **PATH** environment variable to be corrupted will be removed in the upcoming Windows App SDK 1.0 Preview 3 release. It might be reintroduced at a later date, when all bugs have been fixed and thoroughly tested.

We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3).  

## Version 1.0 Preview 2 (1.0.0-preview2)

> [!IMPORTANT]
> Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you've already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3) instead.

This is the latest release of the preview channel for version 1.0. It supports all [preview channel features](../release-channels.md#features-available-by-release-channel).

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3 (1.0.0-preview2)

**New updates**:

- Controls have been updated to reflect the latest Windows styles from [Windows UI Library 2.6](../../winui/winui2/release-notes/winui-2.6.md#visual-style-updates).
- Single-project MSIX is supported.
- WinUI 3 package can now target build 17763 and later. See [issue #921](https://github.com/microsoft/WindowsAppSDK/issues/921) for more info.
- In-app toolbar is supported. However, the in-app toolbar and existing Hot Reload/Live Visual Tree support require the upcoming Visual Studio 17.0 Preview 5 release, available later in October.

**Bug fixed**: WebView2Runtime text is now localized.

For more info or to get started developing with WinUI 3, see:

- [Windows UI Library (WinUI)](../../winui/index.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)

### Windowing (1.0.0-preview2)

This release introduces updates to the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class. There are no major new features added in this release, but there are changes to method names, properties, and some return values have been removed. See the documentation and samples for detailed updates. If you worked with [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) in the 1.0 Experimental or 1.0 Preview 1 releases, expect some changes to your code.

**New updates**:

- The **AppWindowConfiguration** class has been removed. The properties of this class is now available on the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) itself, or on the **Presenter** classes.
- Most `bool` return values for the WinRT API methods in this space has been removed and are now `void` since these methods would always succeed.
- The C# ImportDll calls are no longer needed for [GetWindowIdFromWindow](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowidfromwindow) and [GetWindowFromWindowId](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowfromwindowid). Use the .NET wrapper methods available in the [**Microsoft.UI.Win32Interop**](/windows/apps/api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop) class instead.

**Important limitations**:

- The Windows App SDK does not currently provide methods for attaching UI framework content to an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow); you're limited to using the HWND interop access methods.
- Window title bar customization works only on Windows 11. Use the [IsCustomizationSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) method to check for title bar customization feature support. We intend to bring this functionality down-level.

For more info, see [Manage app windows (Windows App SDK)](../windowing/windowing-overview.md).

### Input (1.0.0-preview2)

**New updates**:

- Improved support for precision touchpad input.

**Important limitations**:

- All [PointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint) static factory functions have been removed: **GetCurrentPoint**, **GetCurrentPointTransformed**, **GetIntermediatePoints**, and **GetIntermediatePointsTransformed**.
- The Windows App SDK does not support retrieving **PointerPoint** objects with pointer IDs. Instead, you can use the **PointerPoint** member function **GetTransformedPoint** to retrieve a transformed version of an existing **PointerPoint** object. For intermediate points, you can use the **PointerEventArgs** member functions [GetIntermediatePoints](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointereventargs.getintermediatepoints) and **GetTransformedIntermediatePoints**. See the documentation for additional details.

### MRT Core (1.0.0-preview2)

**New updates**:

- App developers can now opt out an image file or a RESW file from being indexed in the PRI file in .NET projects. See [issue 980](https://github.com/microsoft/WindowsAppSDK/issues/980) for more info.  

**Important limitations**:

- In .NET projects, resource files copy-pasted into the project folder aren't indexed on F5 if the app was already built. As a workaround, rebuild the app. See [issue 1503](https://github.com/microsoft/WindowsAppSDK/issues/1503) for more info].
- In .NET projects, existing resource files added from an external folder aren't indexed without manual setting of the Build Action. To work around this issue, set the Build Action in Visual Studio: **Content** for image files and **PRIResource** for RESW files. See issue [1504](https://github.com/microsoft/WindowsAppSDK/issues/1504) for more info.

### Deployment for unpackaged apps

**New features**:

- Windows App SDK 1.0 Preview 2 introduces a .NET wrapper for the bootstrapper API (see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](../use-windows-app-sdk-run-time.md)). The bootstrapper API is a set of native C/C++ functions that unpackaged apps must use to dynamically take a dependency on the Windows App SDK framework package at run time. The .NET wrapper provides an easier way to call the bootstrapper API from .NET apps, including Windows Forms and WPF apps. The .NET wrapper for the bootstrapper API is available in the Microsoft.WindowsAppRuntime.Bootstrap.Net.dll assembly, which is local to your app project. For more info about the .NET wrapper, see [.NET wrapper library](../use-windows-app-sdk-run-time.md#net-wrapper-for-the-bootstrapper-api).
- Packaged apps can now use the deployment API to get the main and singleton MSIX packages installed on the machine. The main and singleton packages are part of the framework package that is installed with the app, but due to a limitation with the Windows application model, packaged apps will need to take this additional step in order to get those packages installed. For more info about how the deployment API works, see [Windows App SDK deployment guide for framework-dependent packaged apps](../deploy-packaged-apps.md).

**Important limitations**:

- The .NET wrapper for the bootstrapper API only is only intended for use by unpackaged .NET applications to simplify access to the Windows App SDK.
- Only MSIX packaged apps that are full trust or have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability have the permission to use the deployment API to install the main and singleton package dependencies. Support for partial-trust packaged apps will be coming in later releases.
- When F5 testing an x86 app which uses the [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) method on an x64 system, ensure that the x64 framework is first installed by running the [WindowsAppRuntimeInstall.exe](https://aka.ms/windowsappsdk/1.0-preview2/msix-installer). Otherwise, you will encounter a **NOT_FOUND** error due to Visual Studio not deploying the x64 framework, which normally occurs through Store deployment or sideloading.

### App lifecycle

Most of the App Lifecycle features already exist in the UWP platform, and have been brought into the Windows App SDK for use by desktop app types, especially unpackaged Console apps, Win32 apps, Windows Forms apps, and WPF apps. The Windows App SDK implementation of these features cannot be used in UWP apps, since there are equivalent features in the UWP platform itself.

Non-UWP apps can also be packaged into MSIX packages. While these apps can use some of the Windows App SDK App Lifecycle features, they must use the manifest approach where this is available. For example, they cannot use the Windows App SDK **RegisterForXXXActivation** APIs and must instead register for rich activation via the manifest.

All the constraints for packaged apps also apply to WinUI 3 apps that are packaged; and there are additional considerations as described below.

**Important considerations**:

- Rich activation: [GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs)
  - *Unpackaged apps*: Fully usable.
  - *Packaged apps*: Usable, but these apps can also use the platform `GetActivatedEventArgs`. Note that the platform defines [Windows.ApplicationModel.AppInstance](/uwp/api/windows.applicationmodel.appinstance) whereas the Windows App SDK defines [Microsoft.Windows.AppLifecycle.AppInstance](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance). And while UWP apps can use the `ActivatedEventArgs` classes, such as `FileActivatedEventArgs` and `LaunchActivatedEventArgs`, apps that use the Windows App SDK AppLifecycle feature must use the interfaces not the classes (e.g, `IFileActivatedEventArgs`, `ILaunchActivatedEventArgs`, and so on).
  - *WinUI 3 apps*: WinUI 3's App.OnLaunched is given a [Microsoft.UI.Xaml.LaunchActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.launchactivatedeventargs), whereas the platform `GetActivatedEventArgs` returns a [Windows.ApplicationModel.IActivatedEventArgs](/uwp/api/Windows.ApplicationModel.Activation.IActivatedEventArgs), and the WindowsAppSDK `GetActivatedEventArgs` returns a [Microsoft.Windows.AppLifecycle.AppActivationArguments](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments) object which can represent a platform `LaunchActivatedEventArgs`.
  - For more info, see [Rich activation with the app lifecycle API](../applifecycle/applifecycle-rich-activation.md).

- Register/Unregister for rich activation
  - *Unpackaged apps*: Fully usable.
  - *Packaged apps*: Not usable use the app's MSIX manifest instead.
  - For more info, see [Rich activation with the app lifecycle API](../applifecycle/applifecycle-rich-activation.md).

- Single/Multi-instancing
  - *Unpackaged apps*: Fully usable.
  - *Packaged apps*: Fully usable.
  - *WinUI 3 apps*: If an app wants to detect other instances and redirect an activation, it must do so as early as possible, and before initializing any windows, etc. To enable this, the app must define DISABLE_XAML_GENERATED_MAIN, and write a custom Main (C#) or WinMain (C++) where it can do the detection and redirection.
  - [RedirectActivationToAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.redirectactivationtoasync) is an async call, and you should not wait on an async call if your app is running in an STA. For Windows Forms and C# WinUI 3 apps, you can declare Main to be async, if necessary. For C++ WinUI 3 and C# WPF apps, you cannot declare Main to be async, so instead you need to move the redirect call to another thread to ensure you don't block the STA.
  - For more info, see [App instancing with the app lifecycle API](../applifecycle/applifecycle-instancing.md).

- Power/State notifications
  - *Unpackaged apps*: Fully usable.
  - *Packaged apps*: Fully usable.
  - For more info, see [Power management with the app lifecycle API](../applifecycle/applifecycle-power.md).

**Known issue:**

File Type associations incorrectly encode %1 to be %251 when setting the Verb handler's command line template, which crashes unpackaged Win32 apps. You can manually edit the Registry value to be %1 instead as a partial workaround. If the target file path has a space in it, then it will still fail and there is no workaround for that scenario.

**Other limitations and known issues:**

- Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you've already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3) instead.

- This release introduces the **Blank App, Packaged (WinUI 3 in Desktop)** templates for C# and C++ projects. These templates enable you to [build your app into an MSIX package without the use of a separate packaging project](../single-project-msix.md). These templates have some known issues in this release:

  - **C# template for Visual Studio 2019.** You will encounter the error when you try to build the project: "The project doesn't know how to run the profile *project name*". To resolve this issue, install the [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools).

  - **C# template for Visual Studio 2019 and Visual Studio 2022.** You will encounter the following error when you try to run or debug your project on your development computer: "The project needs to be deployed before we can debug. Please enable Deploy in the Configuration Manager." To resolve this issue, enable deployment for your project in **Configuration Manager**. For detailed instructions, see the [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md).

  - **C++ template for Visual Studio 2019 and Visual Studio 2022.** In this release, these projects are restricted to calling the subset of Win32 APIs that can be called by UWP apps. The **Blank App, Packaged with WAP (WinUI 3 in Desktop)** template is not affected by this issue.

  - **C++ template for Visual Studio 2022 version 17.0 releases up to Preview 4.** You will encounter the following error the first time you try to run your project: "There were deployment errors". To resolve this issue, run or deploy your project a second time. This issue will be fixed in Visual Studio 2022 version 17.0 Preview 5.

- **Push notifications API (Microsoft.Windows.PushNotifications namespace) incorrectly included in the 1.0 Preview 2 release.** This is still an experimental feature, and to you use it you must install the 1.0 Experimental release instead. This feature will be removed from the upcoming 1.0 release.

- **App lifecycle API (Microsoft.Windows.AppLifecycle namespace) incorrectly includes the Experimental attribute in the 1.0 Preview 2 release.** The Experimental attribute will be removed from this API in the next release.

- **No support for Any CPU build configuration**: When [adding the Windows App SDK](../use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

- **C# projects using 1.0 Preview 2 must use the following .NET SDK**: .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

- An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
    3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
    4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.

## Version 1.0 Preview 1 (1.0.0-preview1)

> [!IMPORTANT]
> Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you've already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3) instead.

This is the first release of the preview channel for version 1.0. It supports all [preview channel features](../release-channels.md#features-available-by-release-channel).

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3 (1.0.0-preview1)

This release of WinUI 3 is focused on building towards 1.0 with bug fixes.

- **New features**: No new features in Preview 1.
- **Fixed issues**: For the full list of issues addressed in this release, see [our GitHub repo](https://aka.ms/winui3/1.0-preview-release-notes).

For more info or to get started developing with WinUI 3, see:

- [Windows UI Library (WinUI)](../../winui/index.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)

### Windowing (1.0.0-preview1)

This release brings the Windowing API we introduced in Experimental 1 to a Preview state. There are no major new features areas in this release as it is focused on bugfixes, stability, and adjustments to the API signature. The noteworthy changes and additions are called out below.

**New features**:

- [DisplayAreaWatcher](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.displayareawatcher) has been added to the Windowing APIs. This allows a developer to observe changes in the display topology and enumerate DisplayAreas currently defined in the system.
- [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) now supports setting the window icon via the [SetIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon) method, and [AppWindowTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) now supports selecting whether to show/hide the window icon along with the system menu via the [IconShowOptions](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions) property.

**Important limitations**:

- This release of [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is currently available only to Win32 apps (both packaged and unpackaged).
- The Windows App SDK does not currently provide methods for attaching UI framework content to an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow); you're limited to using the HWND interop access methods.
- Window title bar customization works only on Windows 11. Use the [IsCustomizationSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) method to check for title bar customization feature support. We intend to bring this functionality down-level.

For more info, see [Manage app windows (Windows App SDK)](../windowing/windowing-overview.md).

### Input  (1.0.0-preview1)

This release brings some new features to the Input API. The noteworthy changes and additions are called out below.

**New features and updates**:

- [PointerPredictor](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpredictor) gives input latency sensitive applications such inking applications the ability to predict input point locations up to 15ms in the future to achieve better latency and smooth animation.  
- [PenDeviceInterop](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.interop.pendeviceinterop) enables you to acquire a reference to the [Windows.Devices.Input.PenDevice](/uwp/api/windows.devices.input.pendevice) by using the [FromPointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.interop.pendeviceinterop.frompointerpoint) method.
- [InputCursor](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputcursor) provides an explicit distinction between preset system cursor types and custom cursor types by removing the "Custom" type present in `CoreCursor`, and splitting the `CoreCursor` object into separate objects.
- Updates to [InputCursor](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputcursor) APIs.
- [GestureRecognizer](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.gesturerecognizer) moved out of experimental to Microsoft.UI.Input.
- [PointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint) moved out of experimental to Microsoft.UI.Input.
- Mouse, touch, and pen input fully supported for WinUI 3 drag and drop.

**Important limitations**:

- This release of Input APIs has known issues with Windows version 1809.  
- MRT Core is not yet supported by any subtype of [InputCursor](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputcursor).
- Direct use of the platform SDK API [Windows.UI.Core.CoreDragOperation](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragoperation) will not work with WinUI 3 applications.
- PointerPoint properties RawPosition and ContactRectRaw were removed because they referred to non-predicted values, which were the same as the normal values in the OS. Use [Position](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint.position) and [ContactRect](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpointproperties.contactrect) instead. Pointer prediction is now handled with the Microsoft.UI.Input.PointerPredictor API object.

### MRT Core (1.0.0-preview1)

Starting in version 1.0 Preview 1, MRT Core APIs have moved from the [Microsoft.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.applicationmodel.resources) namespace to the [Microsoft.Windows.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace.

**Other limitations and known issues:**

- Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you've already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [1.0 Preview 3](#version-10-preview-3-100-preview3) instead.

- Projects created by using the C++ **Blank App, Packaged with WAP (WinUI 3 in Desktop)** project template encounter the following build error by default: `fatal error C1083: Cannot open include file: 'winrt/microsoft.ui.dispatching.co_await.h': No such file or directory`. To resolve this issue, remove the following line of code from the **pch.h** file. This issue will be fixed in the next release.

    ```cpp
    #include <winrt/microsoft.ui.dispatching.co_await.h>
    ```

- An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
    3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
    4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI 3 templates in Visual Studio](../../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](../use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

- **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/ProjectReunion/issues/921).

- **C# projects using 1.0 Preview 1 must use the following .NET SDK**: .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

- **Unpackaged apps not supported on Windows 10 version 1809**: This should be resolved in the next release.

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
