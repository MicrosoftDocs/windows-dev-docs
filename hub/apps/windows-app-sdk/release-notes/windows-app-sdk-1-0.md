---
title: Windows App SDK 1.0 release notes
description: Provides information about what's new in Windows App SDK 1.0.
ms.topic: release-notes
ms.date: 09/22/2025
keywords: windows win32, windows app development, Windows App SDK, release notes
ms.localizationpriority: medium
zone_pivot_groups: wasdk-release-channels
---

# Windows App SDK 1.0 release notes

[!INCLUDE [wasdk-releasenotes](../../../includes/wasdk-release-notes.md)]

:::zone pivot="stable"

## Version 1.0.4

<details><summary>Bug fixes</summary>

> - Fixed issue causing AppBars, when used as Page.TopAppBar or Page.BottomAppBar to not render on screen.
> - Fixed issue where apps with a package name of 12 characters or less that use a WinUI control from MUXControls.dll will immediately crash. For more information, see [issue 6360](https://github.com/microsoft/microsoft-ui-xaml/issues/6360) on GitHub.
> - Fixed touch input issues causing problems with keyboard shortcuts and other scenarios. For more information, see [issue 6291](https://github.com/microsoft/microsoft-ui-xaml/issues/6291) on GitHub.
> - Fixed issue causing apps packaged with MSIX or deployed as self-contained to fail to deploy.
> - Fixed issue causing apps to sometimes crash during a drag and drop operation. For more information see [issue 7002](https://github.com/microsoft/microsoft-ui-xaml/issues/7002) on GitHub.

</details>

---

## Version 1.0.3

<details><summary>Bug fixes</summary>

> - Fixed issue causing C# apps with WebView2 to crash on launch when the C/C++ Runtime (CRT) isn't installed.
> - Fixed touch input issues causing problems with keyboard shortcuts and other scenarios. For more information, see [issue 6291](https://github.com/microsoft/microsoft-ui-xaml/issues/6291) on GitHub.
>
> **Note**: We don't usually add functionality in a servicing release, but this release's WebView2 fix required us to update to the latest version of the WebView2 SDK (1020.46 to 1185.39). See [Release Notes for the WebView2 SDK](/microsoft-edge/webview2/release-notes#10118539) for additional information on WebView2 1.0.1185.39 and [Distribute your app and the WebView2 Runtime](/microsoft-edge/webview2/concepts/distribution) for additional information on the WebView2 Runtime.

</details>

---

## Version 1.0.2

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.0 release.

<details><summary>Bug fixes</summary>

> - Fixed layout cycle issue causing an app to crash when scrolling to the end of a ListView. For more information see [issue 6218](https://github.com/microsoft/microsoft-ui-xaml/issues/6218) on GitHub.
> - Fixed issue causing C# apps to crash on launch when the C/C++ Runtime (CRT) isn't installed. However, the CRT is still required for C# apps using WebView2. For more information see [issue 2117](https://github.com/microsoft/WindowsAppSDK/issues/2117) on GitHub.
> - Fixed issue where applications with Single-project MSIX did not generate a .appinstaller file. For more information see [issue 1821](https://github.com/microsoft/WindowsAppSDK/issues/1821) on GitHub.
> - Fixed issue where WinUI applications did not support .NET 6 `dotnet build`.

</details>

---

## Version 1.0.1

This is a servicing release of the Windows App SDK that includes critical bug fixes and multi-window support for the 1.0 release.

<details><summary>Bug fixes</summary>

> - Fixed issue causing the MddBootstrapAutoinitializer to not compile with enabled ImplicitUsings. For more information see [issue 1686](https://github.com/microsoft/WindowsAppSDK/issues/1686) on GitHub.
> - Fixed issue where focus in WebView2 would be unexpectedly lost causing input and selection issues. For more information, see [issue 5615](https://github.com/microsoft/microsoft-ui-xaml/issues/5615) & [issue 5570](https://github.com/microsoft/microsoft-ui-xaml/issues/5570) on GitHub.
> - Fixed issue causing the in-app toolbar in Visual Studio to be unclickable when using a custom title bar in a WinUI app.
> - Fixed issue causing Snap Layout to not appear when using a custom title bar in a WinUI app. For more information, see [issue 6333](https://github.com/microsoft/microsoft-ui-xaml/issues/6333) & [issue 6246](https://github.com/microsoft/microsoft-ui-xaml/issues/6246) on GitHub.
> - Fixed issue causing an exception when setting Window.ExtendsContentIntoTitleBar property when Window.SetTitlebar has been called with a still-loading UIElement.
> - Fixed issue where Single-project MSIX apps did not support `dotnet build`.
> - Fixed issue causing unpackaged apps to not install after installing a packaged app. For more information, see [issue 1871](https://github.com/microsoft/WindowsAppSDK/issues/1871) on GitHub.
> - Fixed issue reducing performance during mouse drag operations.
> - Fixed crash when calling GetWindowIdFromWindow() in unpackaged apps. For more information, see [discussion 1891](https://github.com/microsoft/WindowsAppSDK/discussions/1891) on GitHub.
>
> The limitations and known issues for version 1.0 also apply to version 1.0.1.
>
> Additionally, for apps with custom title bars, we have made changes in this release (and fixed numerous issues) that include fixes to the glass window used for drag&drop operations.
> The recommendation is to use the default values and behaviors (give them a try!).
> If your title bar used margins so that the default caption buttons were interactive, we recommend visualizing your drag region by setting the background of your title bar to red and then adjusting the margins to extend the drag region to the caption controls.

</details>

<details><summary>New features</summary>

> We have stabilized and enabled the creation of **multiple windows on the same thread** in WinUI applications. See [issue 5918](https://github.com/microsoft/microsoft-ui-xaml/issues/5918) for more information.

</details>

---

## Version 1.0

<details><summary>WinUI 3</summary>

>
> WinUI 3 is the native user experience (UX) framework for Windows App SDK. In this release we've added multiple new features from Windows App SDK 0.8 and stabilized issues from 1.0 Preview releases.
>
> **New features and updates:**
>
> - We've added new controls (PipsPager, Expander, BreadcrumbBar) and updated existing controls to reflect the latest Windows styles from [WinUI for UWP 2.6](/windows/uwp/get-started/winui2/release-notes/winui-2.6#visual-style-updates).
> - Single-project MSIX packaging is supported in WinUI by creating a new application using the "Blank App, Packaged…" template.
> - We now support deploying WinUI apps that aren't packaged on Windows versions 1809 and above. Please view [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md) for additional information.
> - WinUI 3 projects can now set their target version down to Windows 10, version 1809. Previously, they could only be set as low as version 1903.
> - In-app toolbar, Hot Reload, & Live Visual Tree for WinUI packaged apps are supported in Visual Studio 2022 Preview 5 and GA.
>
> **Important limitations:**
>
> - Known issues for **both packaged and unpackaged WinUI applications**:
>   - *Run-time error in C++ or C# apps that reference a C++ Windows Runtime Component:*
>     - To resolve, add the below target to the end of the Windows Runtime Component's .vcxproj:
>
>     ```xml
>     <Target Name="GetPriIndexName">
>     <PropertyGroup>
>         <!-- Winmd library targets use the default root namespace of the project for the App package name -->
>         <PriIndexName Condition="'$(RootNamespace)' != ''">$(RootNamespace)</PriIndexName>
>         <!-- If RootNamespace is empty fall back to TargetName -->
>         <PriIndexName Condition="$(PriIndexName) == ''">$(TargetName)</PriIndexName>
>     </PropertyGroup>
>     </Target>
>     ```
>
>     - The expected error will be similar to *WinRT originate error - 0x80004005 : 'Cannot locate resource from 'ms-appx:///BlankPage.xaml'.'.*
>
> - Known issues for **WinUI applications with Single-project MSIX** (Blank App, Packaged template):
>   - *Missing Package & Publish menu item until you restart Visual Studio:* When creating a new app with Single-project MSIX in both Visual Studio 2019 and Visual Studio 2022 using the Blank App, Packaged (WinUI 3 in Desktop) project template, the command to publish the project doesn't appear in the menu until you close and re-open Visual Studio.
>   - A C# app with Single-project MSIX will not compile without the "C++ (v14x) Universal Windows Platform Tools"   optional component installed. See [Install tools for the Windows App SDK](../../get-started/start-here.md) for additional information.
>   - *Potential run-time error in an app with Single-project MSIX that consumes types defined in a referenced Windows Runtime Component:* To resolve, manually add [activatable class entries](/uwp/schemas/appxpackage/how-to-specify-extension-points-in-a-package-manifest) to the appxmanifest.xml.
>     - The expected error in C# applications is "COMException: Class not registered (0x80040154 (REGDB_E_CLASSNOTREG)).
>     - The expected error in C++/WinRT applications is "winrt::hresult_class_not_registered".
>
> - Known issues for **WinUI apps that aren't packaged** (unpackaged apps):
>   - Some APIs require package identity, and aren't supported in unpackaged apps, such as:
>     - [ApplicationData](/uwp/api/Windows.Storage.ApplicationData)
>     - [StorageFile.GetFileFromApplicationUriAsync](/uwp/api/Windows.Storage.StorageFile.GetFileFromApplicationUriAsync)
>     - [StorageFile.CreateStreamedFileFromUriAsync](/uwp/api/windows.storage.storagefile.createstreamedfilefromuriasync)
>     - [ApiInformation](/uwp/api/Windows.Foundation.Metadata.ApiInformation) (not supported on Windows 10)
>     - [Package.Current](/uwp/api/windows.applicationmodel.package.current)
>     - Any API in the [Windows.ApplicationModel.Resources](/uwp/api/windows.applicationmodel.resources) namespace
>     - Any API in the [Microsoft.Windows.ApplicationModel.Resources](/uwp/api/windows.applicationmodel.resources.core) namespace
>
> - Known issues for **packaging and deploying WinUI applications**:
>   - The `Package` command is not supported in WinUI apps with Single-project MSIX (Blank App, Packaged template). Instead, use the `Package & Publish` command to create an MSIX package.
>   - To create a NuGet package from a C# Class Library with the `Pack` command, ensure the active `Configuration` is `Release`.
>   - The `Pack` command is not supported in C++ Windows Runtime Components to create a NuGet package.
>
> For more info, or to get started developing with WinUI, see:
>
> - [WinUI](../../winui/winui3/index.md)
> - [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md)

</details>

<details><summary>Windowing</summary>

>
> The Windows App SDK provides an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class that evolves the previous easy-to-use Windows.UI.WindowManagement.AppWindow preview class and makes it available to all Windows apps, including Win32, WPF, and WinForms.
>
> **New Features:**
>
> - [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is a high-level windowing API that allows for easy-to-use windowing scenarios that integrates well with the Windows user experience and with other apps. Represents a high-level abstraction of a system-managed container of the content of an app. This is the container in which your content is hosted, and represents the entity that users interact with when they resize and move your app on screen. For developers familiar with Win32, the AppWindow can be seen as a high-level abstraction of the HWND.
> - [DisplayArea](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.displayarea) represents a high-level abstraction of a HMONITOR, follows the same principles as AppWindow.
> - [DisplayAreaWatcher](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.displayareawatcher) allows a developer to observe changes in the display topology and enumerate DisplayAreas currently defined in the system.
>
> For more info, see [Manage app windows (Windows App SDK)](../../develop/ui/manage-app-windows.md).

</details>

<details><summary>Input</summary>

>
> These are the input APIs that support WinUI and provide a lower level API surface for developers to achieve more advanced input interactions.
>
> **New Features:**
>
> - Pointer APIs: [PointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint), [PointerPointProperties](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpointproperties), and [PointerEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointereventargs) to support retrieving pointer event information with XAML input APIs.
> - [InputPointerSource API](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputpointersource): Represents an object that is registered to report pointer input, and provides pointer cursor and input event handling for XAML's SwapChainPanel API.
> - [Cursor API](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputcursor): Allows developers to change the cursor bitmap.
> - [GestureRecognizer API](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.gesturerecognizer): Allows developers to recognize certain gestures such as drag, hold, and click when given pointer information.
>
> **Important limitations:**
>
> - All [PointerPoint](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint) static factory functions have been removed: **GetCurrentPoint**, **GetCurrentPointTransformed**, **GetIntermediatePoints**, and **GetIntermediatePointsTransformed**.
> - The Windows App SDK doesn't support retrieving **PointerPoint** objects with pointer IDs. Instead, you can use the **PointerPoint** member function **GetTransformedPoint** to retrieve a transformed version of an existing **PointerPoint** object. For intermediate points, you can use the **PointerEventArgs** member functions [**GetIntermediatePoints**](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointereventargs.getintermediatepoints) and **GetTransformedIntermediatePoints**.
> - Direct use of the platform SDK API [**Windows.UI.Core.CoreDragOperation**](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragoperation) will not work with WinUI applications.
> - **PointerPoint** properties **RawPosition** and **ContactRectRaw** were removed because they referred to non-predicted values, which were the same as the normal values in the OS. Use [**Position**](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint.position) and [**ContactRect**](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpointproperties.contactrect) instead. Pointer prediction is now handled with the **Microsoft.UI.Input.PointerPredictor** API object.

</details>

<details><summary>App Lifecycle</summary>

>
> Most of the App Lifecycle features already exist in the UWP platform, and have been brought into the Windows App SDK for use by desktop app types, especially unpackaged Console apps, Win32 apps, Windows Forms apps, and WPF apps. The Windows App SDK implementation of these features cannot be used in UWP apps, since there are equivalent features in the UWP platform itself.
>
> Non-UWP apps can also be packaged into MSIX packages. While these apps can use some of the Windows App SDK App Lifecycle features, they must use the manifest approach where this is available. For example, they cannot use the Windows App SDK **RegisterForXXXActivation** APIs and must instead register for rich activation via the manifest.
>
> All the constraints for packaged apps also apply to WinUI apps, which are packaged, and there are additional considerations as described below.
>
> **Important considerations:**
>
> - Rich activation: [GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs)
>   - *Unpackaged apps*: Fully usable.
>   - *Packaged apps*: Usable, but these apps can also use the platform `GetActivatedEventArgs`. Note that the platform defines [Windows.ApplicationModel.AppInstance](/uwp/api/windows.applicationmodel.appinstance) whereas the Windows App SDK defines [Microsoft.Windows.AppLifecycle.AppInstance](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance). And while UWP apps can use the `ActivatedEventArgs` classes, such as `FileActivatedEventArgs` and `LaunchActivatedEventArgs`, apps that use the Windows App SDK AppLifecycle feature must use the interfaces not the classes (e.g, `IFileActivatedEventArgs`, `ILaunchActivatedEventArgs`, and so on).
>   - *WinUi apps*: WinUI's App.OnLaunched is given a [Microsoft.UI.Xaml.LaunchActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.launchactivatedeventargs), whereas the platform `GetActivatedEventArgs` returns a [Windows.ApplicationModel.IActivatedEventArgs](/uwp/api/Windows.ApplicationModel.Activation.IActivatedEventArgs), and the WindowsAppSDK `GetActivatedEventArgs` returns a [Microsoft.Windows.AppLifecycle.AppActivationArguments](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments) object which can represent a platform `LaunchActivatedEventArgs`.
>   - For more info, see [Rich activation with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md).
>
> - Register/Unregister for rich activation
>   - *Unpackaged apps*: Fully usable.
>   - *Packaged apps*: Not usable use the app's MSIX manifest instead.
>   - For more info, see [Rich activation with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md).
>
> - Single/Multi-instancing
>   - *Unpackaged apps*: Fully usable.
>   - *Packaged apps*: Fully usable.
>   - *WinUI apps*: If an app wants to detect other instances and redirect an activation, it must do so as early as possible, and before initializing any windows, etc. To enable this, the app must define DISABLE_XAML_GENERATED_MAIN, and write a custom Main (C#) or WinMain (C++) where it can do the detection and redirection.
>   - [RedirectActivationToAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.redirectactivationtoasync) is an async call, and you should not wait on an async call if your app is running in an STA. For Windows Forms and C# WinUI apps, you can declare Main to be async, if necessary. For C++ WinUI and C# WPF apps, you cannot declare Main to be async, so instead you need to move the redirect call to another thread to ensure you don't block the STA.
>   - For more info, see [App instancing with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-instancing.md).
>
> - Power/State notifications
>   - *Unpackaged apps*: Fully usable.
>   - *Packaged apps*: Fully usable.
>   - For more info, see [Power management with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-power.md).
>
> **Known issue**:
>
> - File Type associations incorrectly encode %1 to be %251 when setting the Verb handler's command line template, which crashes unpackaged Win32 apps. You can manually edit the Registry value to be %1 instead as a partial workaround. If the target file path has a space in it, then it will still fail and there is no workaround for that scenario.
> - These Single/Multi-instancing bugs will be fixed in an upcoming servicing patch:
>   - AppInstance redirection doesn't work when compiled for x86
>   - Registering a key, unregistering it, and re-registering it causes the app to crash

</details>

<details><summary>DWriteCore</summary>

>
> DWriteCore is the Windows App SDK implementation of [DirectWrite](/windows/win32/directwrite/direct-write-portal), which is the DirectX API for high-quality text rendering, resolution-independent outline fonts, and full Unicode text and layout support. DWriteCore is a form of DirectWrite that runs on versions of Windows down to Windows 10, version 1809 (10.0; Build 17763), and opens the door for you to use it cross-platform.
>
> **Features:**
>
> DWriteCore contains all of the features of DirectWrite, with a few exceptions.
>
> **Important limitations:**
>
> - DWriteCore does not contain the following DirectWrite features:  
>   - Per-session fonts
>   - End-user defined character (EUDC) fonts
>   - Font-streaming APIs
> - Low-level rendering API support is partial.
> - DWriteCore doesn't interoperate with Direct2D, but you can use [IDWriteGlyphRunAnalysis](/windows/win32/api/dwrite/nn-dwrite-idwriteglyphrunanalysis) and [IDWriteBitmapRenderTarget](/windows/win32/api/dwrite/nn-dwrite-idwritebitmaprendertarget).
>
> For more information, see [DWriteCore overview](/windows/win32/directwrite/dwritecore-overview).

</details>

<details><summary>MRT Core</summary>

>
> MRT Core is a streamlined version of the modern Windows [Resource Management System](/windows/uwp/app-resources/resource-management-system) that is distributed as part of the Windows App SDK.
>
> **Important limitations:**
>
> - In .NET projects, resource files copy-pasted into the project folder aren't indexed on F5 if the app was already built. As a workaround, rebuild the app. See [issue 1503](https://github.com/microsoft/WindowsAppSDK/issues/1503) for more info.
> - In .NET projects, when a resource file is added to the project using the Visual Studio UI, the files may not be indexed by default. See [issue 1786](https://github.com/microsoft/WindowsAppSDK/issues/1786) for more info. To work around this issue, please remove the entries below in the CSPROJ file:
>
>   ```xml
>   <ItemGroup>
>       <Content Remove="<image file name>" />
>   </ItemGroup>
>   <ItemGroup>
>       <PRIResource Remove="<resw file name>" />
>   </ItemGroup>
>   ```
>
> - For unpackaged C++ WinUI apps, the resource URI is not built correctly. To work around this issue, add the following in the vcxproj:
>
>   ```xml
>   <!-- Add the following after <Import Project="$(VCTargetsPath)\Microsoft.Cpp.props" /> -->
>   
>   <PropertyGroup>
>       <AppxPriInitialPath></AppxPriInitialPath>   
>   </PropertyGroup>
>   ```
>
> For more information, see [Manage resources with MRT Core](../../windows-app-sdk/mrtcore/mrtcore-overview.md).

</details>

<details><summary>Deployment</summary>

>
> **New Features and updates:**
>
> - You can auto-initialize the Windows App SDK through the `WindowsPackageType project` property to load the Windows App SDK runtime and call the Windows App SDK APIs. See [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md) for instructions.
> - Unpackaged apps can deploy Windows App SDK by integrating in the standalone Windows App SDK `.exe` installer into your existing MSI or setup program. For more info, see [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](../../windows-app-sdk/deploy-unpackaged-apps.md).
> - Unpackaged .NET apps can also use .NET wrapper for the [bootstrapper API](../../windows-app-sdk/use-windows-app-sdk-run-time.md) to dynamically take a dependency on the Windows App SDK framework package at run time. For more info about the .NET wrapper, see [.NET wrapper library](../../windows-app-sdk/use-windows-app-sdk-run-time.md#net-wrapper-for-the-bootstrapper-api).
> - Packaged apps can use the deployment API to verify and ensure that all required packages are installed on the machine. For more info about how the deployment API works, see [Windows App SDK deployment guide for framework-dependent packaged apps](../../windows-app-sdk/deploy-packaged-apps.md).
>
> **Important limitations:**
>
> - The .NET wrapper for the bootstrapper API is only intended for use by unpackaged .NET applications to simplify access to the Windows App SDK.
> - Only MSIX packaged apps that are full trust or have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability have the permission to use the deployment API to install the main and singleton package dependencies. Support for partial-trust packaged apps will be coming in later releases.
> - When F5 testing an x86 app which uses the [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) method on an x64 system, ensure that the x64 framework is first installed by running the [WindowsAppRuntimeInstall.exe](https://aka.ms/windowsappsdk/1.0-preview2/msix-installer). Otherwise, you will encounter a **NOT_FOUND** error due to Visual Studio not deploying the x64 framework, which normally occurs through Store deployment or sideloading.

</details>

<details><summary>Other limitations and known issues</summary>

>
> - **No support for Any CPU build configuration**: When [adding the Windows App SDK](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
> - **Upgrading from .NET 5 to .NET 6**: When upgrading in the Visual Studio UI, you might run into build errors. As a workaround, manually update your project file's `TargetFrameworkPackage` to the below:
>
>   ```xml
>     <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework> 
>   ```
>
> - **C# Single-project MSIX app doesn't compile if C++ UWP Tools aren't installed.** If you have a C# Single-project MSIX project, then you'll need to install the **C++ (v14x) Universal Windows Platform Tools** optional component.
>
> - **Subsequent language VSIX fails to install into Visual Studio 2019 when multiple versions of Visual Studio 2019 are installed.** If you have multiple versions of Visual Studio 2019 installed (e.g. Release and Preview) and then install the Windows App SDK VSIX for both C++ *and* C#, the second installation will fail. To resolve, uninstall the Single-project MSIX Packaging Tools for Visual Studio 2019 after the first language VSIX. View [this feedback](https://developercommunity.visualstudio.com/t/Installation-of-a-VSIX-into-both-Release/1582487?entry=myfeedback) for additional information about this issue.
>
> - An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):
>
>     1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
>     2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
>     3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
>     4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.

</details>

:::zone-end

:::zone pivot="preview"

### Download 1.0 Preview 3 Visual Studio extensions (VSIX)

> [!NOTE]
> If you have Windows App SDK Visual Studio extensions (VSIX) already installed, then uninstall them before installing a new version. For directions, see [Manage extensions for Visual Studio](/visualstudio/ide/finding-and-using-visual-studio-extensions).

From the table below you can download the Visual Studio extensions (VSIX) for the 1.0 Preview 3 release. For all versions, see [Latest Windows App SDK downloads](../../windows-app-sdk/downloads.md). If you haven't done so already, start by configuring your development environment, using the steps in [Install tools for the Windows App SDK](../../windows-app-sdk/set-up-your-development-environment.md?tabs=preview).

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

We now support deploying WinUI apps without MSIX packaging. See [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md) to configure your WinUI application to support unpackaged deployment.

**Important limitations:**

- Unpackaged WinUI applications are **supported only on Windows versions 1909 and later**.
- Unpackaged WinUI applications are **supported on x86 and x64**; arm64 support will be added in the next stable release.
- **Single-project MSIX Packaging Tools** for [Visual Studio 2019](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools) or [Visual Studio 2022](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingToolsDev17) is required for unpackaged apps.
- In an unpackaged app, you might receive a prompt to install .NET 3.5; if you do, then you can ignore it.
- Some APIs are not currently supported in unpackaged apps. We're aiming to fix this in the next stable release. A few examples:
  - [ApplicationData](/uwp/api/Windows.Storage.ApplicationData)
  - [StorageFile.GetFileFromApplicationUriAsync](/uwp/api/Windows.Storage.StorageFile.GetFileFromApplicationUriAsync)
  - [ApiInformation](/uwp/api/Windows.Foundation.Metadata.ApiInformation) (not supported on Windows 10)
  - [Package.Current](/uwp/api/windows.applicationmodel.package.current)
- ListView, CalendarView, and GridView controls are using the incorrect styles, and we're aiming to fix this in the next stable release.

For more info, or to get started developing with WinUI 3, see:

- [WinUI](../../winui/winui3/index.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md)

**Other limitations and known issues:**

- **Unpackaged apps are not supported on Windows 10 version 1809**. We're aiming to fix this in the next release in the stable channel.

- **C# Single-project MSIX app doesn't compile if C++ UWP Tools aren't installed**. If you have a C# Single-project MSIX project, then you'll need to install the **C++ (v14x) Universal Windows Platform Tools** optional component.

- This release introduces the **Blank App, Packaged (WinUI 3 in Desktop)** project templates for C# and C++. These templates enable you to build your app into an MSIX package without the use of a separate packaging project (see [Package your app using single-project MSIX](../../windows-app-sdk/single-project-msix.md)). These templates have some known issues in this release:

  - **Missing Publish menu item until you restart Visual Studio**. When creating a new app in both Visual Studio 2019 and Visual Studio 2022 using the **Blank App, Packaged (WinUI 3 in Desktop)** project template, the command to publish the project doesn't appear in the menu until you close and re-open Visual Studio.

  - **Error when adding C++ static/dynamic library project references to C++ apps using Single-project MSIX Packaging**. Visual Studio displays an error that the project can't be added as a reference because the project types are not compatible.
  
  - **Error when referencing a custom user control in a class library project**. The application will crash with the error that the system can't find the path specified.

  - **C# or C++ template for Visual Studio 2019**. When you try to build the project, you'll encounter the error "The project doesn't know how to run the profile *project name*". To resolve this issue, install the [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools).

  - **C# template for Visual Studio 2019 and Visual Studio 2022**. In Visual Studio when you **Start Debugging** or **Start Without Debugging**, if your app doesn't deploy and run (and there's no feedback from Visual Studio), then click on the project node in **Solution Explorer** to select it, and try again.

  - **C# template for Visual Studio 2019 and Visual Studio 2022**. You will encounter the following error when you try to run or debug your project on your development computer: "The project needs to be deployed before we can debug. Please enable Deploy in the Configuration Manager." To resolve this issue, enable deployment for your project in **Configuration Manager**. For detailed instructions, see the [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md).

  - **C++ template for Visual Studio 2022 version 17.0 releases up to Preview 4**. You will encounter the following error the first time you try to run your project: "There were deployment errors". To resolve this issue, run or deploy your project a second time. This issue will be fixed in Visual Studio 2022 version 17.0 Preview 7.

- **No support for Any CPU build configuration**: When [adding the Windows App SDK](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

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

---

## Version 1.0 Preview 2 (1.0.0-preview2)

> [!IMPORTANT]
> Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you've already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2).

This is the latest release of the preview channel for version 1.0. It supports all [preview channel features](../../windows-app-sdk/release-channels.md#features-available-by-release-channel).

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3 (1.0.0-preview2)

**New updates**:

- Controls have been updated to reflect the latest Windows styles from [WinUI for UWP 2.6](/windows/uwp/get-started/winui2/release-notes/winui-2.6#visual-style-updates).
- Single-project MSIX is supported.
- WinUI 3 package can now target build 17763 and later. See [issue #921](https://github.com/microsoft/WindowsAppSDK/issues/921) for more info.
- In-app toolbar is supported. However, the in-app toolbar and existing Hot Reload/Live Visual Tree support require the upcoming Visual Studio 17.0 Preview 5 release, available later in October.

**Bug fixed**: WebView2Runtime text is now localized.

For more info or to get started developing with WinUI 3, see:

- [WinUI](../../winui/winui3/index.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md)

### Windowing (1.0.0-preview2)

This release introduces updates to the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) class. There are no major new features added in this release, but there are changes to method names, properties, and some return values have been removed. See the documentation and samples for detailed updates. If you worked with [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) in the 1.0 Experimental or 1.0 Preview 1 releases, expect some changes to your code.

**New updates**:

- The **AppWindowConfiguration** class has been removed. The properties of this class is now available on the [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) itself, or on the **Presenter** classes.
- Most `bool` return values for the WinRT API methods in this space has been removed and are now `void` since these methods would always succeed.
- The C# ImportDll calls are no longer needed for [GetWindowIdFromWindow](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowidfromwindow) and [GetWindowFromWindowId](/windows/windows-app-sdk/api/win32/microsoft.ui.interop/nf-microsoft-ui-interop-getwindowfromwindowid). Use the .NET wrapper methods available in the [**Microsoft.UI.Win32Interop**](/windows/apps/api-reference/cs-interop-apis/microsoft.ui/microsoft.ui.win32interop) class instead.

**Important limitations**:

- The Windows App SDK does not currently provide methods for attaching UI framework content to an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow); you're limited to using the HWND interop access methods.
- Window title bar customization works only on Windows 11. Use the [IsCustomizationSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) method to check for title bar customization feature support. We intend to bring this functionality down-level.

For more info, see [Manage app windows (Windows App SDK)](../../develop/ui/manage-app-windows.md).

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

- Windows App SDK 1.0 Preview 2 introduces a .NET wrapper for the bootstrapper API (see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](../../windows-app-sdk/use-windows-app-sdk-run-time.md)). The bootstrapper API is a set of native C/C++ functions that unpackaged apps must use to dynamically take a dependency on the Windows App SDK framework package at run time. The .NET wrapper provides an easier way to call the bootstrapper API from .NET apps, including Windows Forms and WPF apps. The .NET wrapper for the bootstrapper API is available in the Microsoft.WindowsAppRuntime.Bootstrap.Net.dll assembly, which is local to your app project. For more info about the .NET wrapper, see [.NET wrapper library](../../windows-app-sdk/use-windows-app-sdk-run-time.md#net-wrapper-for-the-bootstrapper-api).
- Packaged apps can now use the deployment API to get the main and singleton MSIX packages installed on the machine. The main and singleton packages are part of the framework package that is installed with the app, but due to a limitation with the Windows application model, packaged apps will need to take this additional step in order to get those packages installed. For more info about how the deployment API works, see [Windows App SDK deployment guide for framework-dependent packaged apps](../../windows-app-sdk/deploy-packaged-apps.md).

**Important limitations**:

- The .NET wrapper for the bootstrapper API only is only intended for use by unpackaged .NET applications to simplify access to the Windows App SDK.
- Only MSIX packaged apps that are full trust or have the [packageManagement](/windows/uwp/packaging/app-capability-declarations) restricted capability have the permission to use the deployment API to install the main and singleton package dependencies. Support for partial-trust packaged apps will be coming in later releases.
- When F5 testing an x86 app which uses the [DeploymentManager.Initialize](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.windowsappruntime.deploymentmanager.initialize) method on an x64 system, ensure that the x64 framework is first installed by running the [WindowsAppRuntimeInstall.exe](https://aka.ms/windowsappsdk/1.0-preview2/msix-installer). Otherwise, you will encounter a **NOT_FOUND** error due to Visual Studio not deploying the x64 framework, which normally occurs through Store deployment or sideloading.

### App lifecycle

Most of the App Lifecycle features already exist in the UWP platform, and have been brought into the Windows App SDK for use by desktop app types, especially unpackaged Console apps, Win32 apps, Windows Forms apps, and WPF apps. The Windows App SDK implementation of these features cannot be used in UWP apps, since there are equivalent features in the UWP platform itself.

Non-UWP apps can also be packaged into MSIX packages. While these apps can use some of the Windows App SDK App Lifecycle features, they must use the manifest approach where this is available. For example, they cannot use the Windows App SDK **RegisterForXXXActivation** APIs and must instead register for rich activation via the manifest.

All the constraints for packaged apps also apply to WinUI apps that are packaged; and there are additional considerations as described below.

**Important considerations**:

- Rich activation: [GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs)
  - *Unpackaged apps*: Fully usable.
  - *Packaged apps*: Usable, but these apps can also use the platform `GetActivatedEventArgs`. Note that the platform defines [Windows.ApplicationModel.AppInstance](/uwp/api/windows.applicationmodel.appinstance) whereas the Windows App SDK defines [Microsoft.Windows.AppLifecycle.AppInstance](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance). And while UWP apps can use the `ActivatedEventArgs` classes, such as `FileActivatedEventArgs` and `LaunchActivatedEventArgs`, apps that use the Windows App SDK AppLifecycle feature must use the interfaces not the classes (e.g, `IFileActivatedEventArgs`, `ILaunchActivatedEventArgs`, and so on).
  - *WinUI apps*: WinUI 3's App.OnLaunched is given a [Microsoft.UI.Xaml.LaunchActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.launchactivatedeventargs), whereas the platform `GetActivatedEventArgs` returns a [Windows.ApplicationModel.IActivatedEventArgs](/uwp/api/Windows.ApplicationModel.Activation.IActivatedEventArgs), and the WindowsAppSDK `GetActivatedEventArgs` returns a [Microsoft.Windows.AppLifecycle.AppActivationArguments](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments) object which can represent a platform `LaunchActivatedEventArgs`.
  - For more info, see [Rich activation with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md).

- Register/Unregister for rich activation
  - *Unpackaged apps*: Fully usable.
  - *Packaged apps*: Not usable use the app's MSIX manifest instead.
  - For more info, see [Rich activation with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md).

- Single/Multi-instancing
  - *Unpackaged apps*: Fully usable.
  - *Packaged apps*: Fully usable.
  - *WinUI apps*: If an app wants to detect other instances and redirect an activation, it must do so as early as possible, and before initializing any windows, etc. To enable this, the app must define DISABLE_XAML_GENERATED_MAIN, and write a custom Main (C#) or WinMain (C++) where it can do the detection and redirection.
  - [RedirectActivationToAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.redirectactivationtoasync) is an async call, and you should not wait on an async call if your app is running in an STA. For Windows Forms and C# WinUI apps, you can declare Main to be async, if necessary. For C++ WinUI 3 and C# WPF apps, you cannot declare Main to be async, so instead you need to move the redirect call to another thread to ensure you don't block the STA.
  - For more info, see [App instancing with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-instancing.md).

- Power/State notifications
  - *Unpackaged apps*: Fully usable.
  - *Packaged apps*: Fully usable.
  - For more info, see [Power management with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-power.md).

**Known issue:**

File Type associations incorrectly encode %1 to be %251 when setting the Verb handler's command line template, which crashes unpackaged Win32 apps. You can manually edit the Registry value to be %1 instead as a partial workaround. If the target file path has a space in it, then it will still fail and there is no workaround for that scenario.

**Other limitations and known issues:**

- Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you've already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2).

- This release introduces the **Blank App, Packaged (WinUI 3 in Desktop)** templates for C# and C++ projects. These templates enable you to [build your app into an MSIX package without the use of a separate packaging project](../../windows-app-sdk/single-project-msix.md). These templates have some known issues in this release:

  - **C# template for Visual Studio 2019.** You will encounter the error when you try to build the project: "The project doesn't know how to run the profile *project name*". To resolve this issue, install the [Single-project MSIX Packaging Tools extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftSingleProjectMSIXPackagingTools).

  - **C# template for Visual Studio 2019 and Visual Studio 2022.** You will encounter the following error when you try to run or debug your project on your development computer: "The project needs to be deployed before we can debug. Please enable Deploy in the Configuration Manager." To resolve this issue, enable deployment for your project in **Configuration Manager**. For detailed instructions, see the [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md).

  - **C++ template for Visual Studio 2019 and Visual Studio 2022.** In this release, these projects are restricted to calling the subset of Win32 APIs that can be called by UWP apps. The **Blank App, Packaged with WAP (WinUI 3 in Desktop)** template is not affected by this issue.

  - **C++ template for Visual Studio 2022 version 17.0 releases up to Preview 4.** You will encounter the following error the first time you try to run your project: "There were deployment errors". To resolve this issue, run or deploy your project a second time. This issue will be fixed in Visual Studio 2022 version 17.0 Preview 5.

- **Push notifications API (Microsoft.Windows.PushNotifications namespace) incorrectly included in the 1.0 Preview 2 release.** This is still an experimental feature, and to you use it you must install the 1.0 Experimental release instead. This feature will be removed from the upcoming 1.0 release.

- **App lifecycle API (Microsoft.Windows.AppLifecycle namespace) incorrectly includes the Experimental attribute in the 1.0 Preview 2 release.** The Experimental attribute will be removed from this API in the next release.

- **No support for Any CPU build configuration**: When [adding the Windows App SDK](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

- **C# projects using 1.0 Preview 2 must use the following .NET SDK**: .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

- An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
    3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
    4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.

---

## Version 1.0 Preview 1 (1.0.0-preview1)

> [!IMPORTANT]
> Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you've already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2).

This is the first release of the preview channel for version 1.0. It supports all [preview channel features](../../windows-app-sdk/release-channels.md#features-available-by-release-channel).

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3 (1.0.0-preview1)

This release of WinUI 3 is focused on building towards 1.0 with bug fixes.

- **New features**: No new features in Preview 1.
- **Fixed issues**: For the full list of issues addressed in this release, see [our GitHub repo](https://aka.ms/winui3/1.0-preview-release-notes).

For more info or to get started developing with WinUI 3, see:

- [WinUI](../../winui/winui3/index.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md)

### Windowing (1.0.0-preview1)

This release brings the Windowing API we introduced in Experimental 1 to a Preview state. There are no major new features areas in this release as it is focused on bugfixes, stability, and adjustments to the API signature. The noteworthy changes and additions are called out below.

**New features**:

- [DisplayAreaWatcher](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.displayareawatcher) has been added to the Windowing APIs. This allows a developer to observe changes in the display topology and enumerate DisplayAreas currently defined in the system.
- [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) now supports setting the window icon via the [SetIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow.seticon) method, and [AppWindowTitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar) now supports selecting whether to show/hide the window icon along with the system menu via the [IconShowOptions](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions) property.

**Important limitations**:

- This release of [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow) is currently available only to Win32 apps (both packaged and unpackaged).
- The Windows App SDK does not currently provide methods for attaching UI framework content to an [AppWindow](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindow); you're limited to using the HWND interop access methods.
- Window title bar customization works only on Windows 11. Use the [IsCustomizationSupported](/windows/windows-app-sdk/api/winrt/microsoft.ui.windowing.appwindowtitlebar.iscustomizationsupported) method to check for title bar customization feature support. We intend to bring this functionality down-level.

For more info, see [Manage app windows (Windows App SDK)](../../develop/ui/manage-app-windows.md).

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
- Direct use of the platform SDK API [Windows.UI.Core.CoreDragOperation](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragoperation) will not work with WinUI applications.
- PointerPoint properties RawPosition and ContactRectRaw were removed because they referred to non-predicted values, which were the same as the normal values in the OS. Use [Position](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpoint.position) and [ContactRect](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.pointerpointproperties.contactrect) instead. Pointer prediction is now handled with the Microsoft.UI.Input.PointerPredictor API object.

### MRT Core (1.0.0-preview1)

Starting in version 1.0 Preview 1, MRT Core APIs have moved from the [Microsoft.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.applicationmodel.resources) namespace to the [Microsoft.Windows.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace.

**Other limitations and known issues:**

- Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you've already installed one of these previews, see [how to resolve the issue](#important-issue-impacting-10-preview-1-and-preview-2).

- Projects created by using the C++ **Blank App, Packaged with WAP (WinUI 3 in Desktop)** project template encounter the following build error by default: `fatal error C1083: Cannot open include file: 'winrt/microsoft.ui.dispatching.co_await.h': No such file or directory`. To resolve this issue, remove the following line of code from the **pch.h** file. This issue will be fixed in the next release.

    ```cpp
    #include <winrt/microsoft.ui.dispatching.co_await.h>
    ```

- An alternative to [**DispatcherQueue.TryEnqueue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) (for resuming execution on the dispatcher queue thread) is to use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to your project to the [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add `#include <wil/cppwinrt_helpers.h>` to your `pch.h`.
    3. Add `#include <winrt/Microsoft.UI.Dispatching.h>` to your `pch.h`.
    4. Now `co_await wil::resume_foreground(your_dispatcherqueue);`.

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI 3 templates in Visual Studio](../../dev-tools/visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

- **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/ProjectReunion/issues/921).

- **C# projects using 1.0 Preview 1 must use the following .NET SDK**: .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).

- **Unpackaged apps not supported on Windows 10 version 1809**: This should be resolved in the next release.

:::zone-end

:::zone pivot="experimental"


## Version 1.0 Experimental (1.0.0-experimental1)

<details>
<summary>WinUI 3</summary>

>
> This release of WinUI 3 is focused on building towards new features for 1.0 stable and fixing bugs.
>
> - **New features**: Support for showing a ContentDialog per window rather than per thread.
> - **Bugs**: For the full list of bugs addressed in this release, see [our GitHub repo](https://aka.ms/winui3/1.0-exp-announcement).
> - **Samples**: To see WinUI 3 controls and features in action, you can clone and build the WinUI 3 Gallery app [from GitHub](https://github.com/microsoft/WinUI-Gallery/tree/main), or download the app [from the Microsoft Store](https://apps.microsoft.com/detail/9P3JFPWWDZRC).
>
> For more information or to get started developing with WinUI, see:
>
> - [WinUI](../../winui/winui3/index.md)
> - [Create your first WinUI 3 (Windows App SDK) project](../../get-started/start-here.md)
>

</details>

<details>
<summary>Push notifications (experimental feature)</summary>

>
> This release introduces a push notifications API that can be used by packaged desktop apps with Azure app registration-based identities. To use this feature, you must [sign up for our private preview](https://aka.ms/windowsappsdk/push-private-preview).
>
> Important limitations:
>
> - Push notifications are only supported in MSIX packaged apps that are running on Windows 10 version 2004 (build 19041) or later releases.
> - Microsoft reserves the right to disable or revoke apps from push notifications during the private preview.
> - Microsoft does not guarantee the reliability or latency of push notifications.
> - During the private preview, push notification volume is limited to 1 million per month.
>
> For more information, see [Push notifications overview](../../develop/notifications/push-notifications/index.md).
>

</details>

<details>
<summary>Windowing</summary>

>
> This release includes updates to the windowing APIs. These are a set of high-level windowing APIs, centered around the AppWindow class, which allows for easy-to-use windowing scenarios that integrates well with the Windows user experience and other apps. This is similar to, but not the same as, the UWP AppWindow.
>
> Important limitations:
>
> - This release of `AppWindow` is currently available only to Win32 apps (both packaged and unpackaged).
> - The Windows App SDK does not currently provide methods for attaching UI framework content to an `AppWindow`; you're limited to using the `HWND` interop access methods.
> - The Windowing API's will currently not work on Windows version 1809 and 1903 for AMD64.
>
> For more information, see [Manage app windows (Windows App SDK)](../../develop/ui/manage-app-windows.md).
>

</details>

<details>
<summary>Deployment for unpackaged apps</summary>

>
> This release introduces updates to the *dynamic dependencies* feature, including the *bootstrapper API*.
>
> Important limitations:
>
> - The dynamic dependencies feature is only supported for unpackaged apps.
> - Elevated callers aren't supported.
>
> For more information, see the following articles:
>
> - [MSIX framework packages and dynamic dependencies](../../desktop/modernize/framework-packages/framework-packages-overview.md)
> - [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](../../windows-app-sdk/use-windows-app-sdk-run-time.md)
>

</details>

<details>
<summary>Other limitations and known issues</summary>

>
> - **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI 3 templates in Visual Studio](../../dev-tools/visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
> - **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/WindowsAppSDK/issues/921).
> - **C# apps using 1.0 Experimental must use one of the following .NET SDKs**:
>   - .NET 6 SDK or later (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)).
>

</details>

:::zone-end
