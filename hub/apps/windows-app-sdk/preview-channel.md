---
title: Preview release channel for the Windows App SDK 
description: Provides information about the preview release channel for the Windows App SDK.
ms.topic: article
ms.date: 05/21/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.author: zafaraj
author: zaryaf
ms.localizationpriority: medium
---

# Preview release channel for the Windows App SDK

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store. There are currently no releases available from the preview channel, and we recommend using the [latest stable release](stable-channel.md).

The preview channel provides a preview of the next upcoming stable release. 

There may be breaking API changes between a given preview channel release and the next stable release. Preview channel releases do not include experimental APIs.

If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).

## Version 1.0 Preview 1 (1.0.0-preview1)

This is the latest release of the preview channel. It supports all [preview channel features](release-channels.md#features-available-by-release-channel).

> [!div class="button"]
> [Download](set-up-your-development-environment.md?tabs=preview#4-install-the-windows-app-sdk-extension-for-visual-studio)

The following sections describe new and updated features, limitations, and known issues for this release.

### WinUI 3

This release of WinUI 3 is focused on building towards 1.0 Stable with bug fixes.

- **New features**: No new features in Preview 1.
- **Fixed issues**: For the full list of issues addressed in this release, see [our GitHub repo](https://aka.ms/winui3/1.0-preview-release-notes).

For more information or to get started developing with WinUI, see:

- [Windows UI 3 Library (WinUI)](../winui/index.md)
- [Get started developing apps with WinUI 3](../winui/winui3/get-started-winui3-for-desktop.md)

### Windowing

This release brings the Windowing API we introduced in Experimental 1 to a Preview state. There are no major new features areas in this release as it is focused on bugfixes, stability, and adjustments to the API signature. The noteworthy changes and additions are called out below.

**New features**:

- [DisplayAreaWatcher](/windows/winui/api/microsoft.ui.windowing.displayareawatcher) has been added to the Windowing APIs. This allows a developer to observe changes in the display topology and enumerate DisplayAreas currently defined in the system.
- [AppWindow](/windows/winui/api/microsoft.ui.windowing.appwindow) now supports setting the window icon via the [SetIcon](/windows/winui/api/microsoft.ui.windowing.appwindow.seticon) method, and [AppWindowTitleBar](/windows/winui/api/microsoft.ui.windowing.appwindowtitlebar) now supports selecting whether to show/hide the window icon along with the system menu via the [IconShowOptions](/windows/winui/api/microsoft.ui.windowing.appwindowtitlebar.iconshowoptions) property.

**Important limitations**:

- This release of [AppWindow](/windows/winui/api/microsoft.ui.windowing.appwindow) is currently available only to Win32 apps (both packaged and unpackaged).
- The Windows App SDK does not currently provide methods for attaching UI framework content to an [AppWindow](/windows/winui/api/microsoft.ui.windowing.appwindow); you are limited to using the HWND interop access methods.
- Window title bar customization works only on Windows 11. Use the [IsCustomizationSupported](/windows/winui/api/microsoft.ui.windowing.appwindowtitlebar.iscustomizationssupported) method to check for title bar customization feature support. We intend to bring this functionality down-level.

For more information, see [Manage app windows](windowing/windowing-overview.md).

### Input

This release brings some new features to the Input API. The noteworthy changes and additions are called out below.

**New features and updates**:

- [PointerPredictor](/windows/winui/api/microsoft.ui.input.pointerpredictor) gives input latency sensitive applications such inking applications the ability to predict input point locations up to 15ms in the future to achieve better latency and smooth animation.  
- [PenDeviceInterop](/windows/winui/api/microsoft.ui.input.interop.pendeviceinterop) enables you to acquire a reference to the [Windows.Devices.Input.PenDevice](/uwp/api/windows.devices.input.pendevice) by using the [FromPointerPoint](/windows/winui/api/microsoft.ui.input.interop.pendeviceinterop.frompointerpoint) method.
- [InputCursor](/windows/winui/api/microsoft.ui.input.inputcursor) provides an explicit distinction between preset system cursor types and custom cursor types by removing the "Custom" type present in `CoreCursor`, and splitting the `CoreCursor` object into separate objects.
- Updates to [InputCursor](/windows/winui/api/microsoft.ui.input.inputcursor) APIs.
- [GestureRecognizer](/windows/winui/api/microsoft.ui.input.gesturerecognizer) moved out of experimental to Microsoft.UI.Input.
- [PointerPoint](/windows/winui/api/microsoft.ui.input.pointerpoint) moved out of experimental to Microsoft.UI.Input.
- Mouse, touch, and pen input fully supported for WinUI drag and drop.

**Important limitations**:

- This release of Input APIs has known issues with Windows version 1809.  
- MRT Core is not yet supported by any subtype of [InputCursor](/windows/winui/api/microsoft.ui.input.inputcursor).
- Direct use of the platform SDK API [Windows.UI.Core.CoreDragOperation](/uwp/api/windows.applicationmodel.datatransfer.dragdrop.core.coredragoperation) will not work with WinUI applications.
- PointerPoint properties RawPosition and ContactRectRaw removed. Use [Position](/windows/winui/api/microsoft.ui.input.pointerpoint.position) and [ContactRect](/windows/winui/api/microsoft.ui.input.pointerpointproperties.contactrect) instead.


<!-- This section coming for next preview...
### Deployment for unpackaged apps

**New features**:

Unpackaged apps will now have a .NET 5 wrapper to initialize the Windows App SDK and Dynamic Dependencies for use with your unpackaged application.  Unpackaged .NET apps that reference the Windows App SDK can now call `Initialize()` instead of hand authoring the PInvoke wrapper. 

This functionality lives in Microsoft.WindowsAppRuntime.Bootstrap.NET.dll and will be bin-placed as an app-local binary to the application. This feature replaces the C# DllImport code sample from earlier experimental and preview releases.

**Important limitations**:

This feature is only intended for use by unpackaged .NET applications to simplify access to the Windows App SDK.

For more information, see the following articles:

- [Use MSIX framework packages dynamically from your desktop app](../desktop/modernize/framework-packages/framework-packages-overview.md)
- [Reference the Windows App SDK framework package at run time](reference-framework-package-run-time.md)
-->

### MRT Core

Starting in version 1.0 Preview 1, MRT Core APIs have moved from the [Microsoft.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.applicationmodel.resources) namespace to the [Microsoft.Windows.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace.

### Other limitations and known issues

- Projects created by using the C++ **Blank App, Packaged with WAP (WinUI 3 in Desktop)** project template encounter the following build error by default: `fatal error C1083: Cannot open include file: 'winrt/microsoft.ui.dispatching.co_await.h': No such file or directory`. To resolve this issue, remove the following line of code from the **pch.h** file. This issue will be fixed in the next release.

    ```cpp
    #include <winrt/microsoft.ui.dispatching.co_await.h>
    ```

- If you want to `co_await` on the [DispatcherQueue.TryEnqueue](/windows/winui/api/microsoft.ui.dispatching.dispatcherqueue.tryenqueue) method, use the [resume_foreground](https://github.com/microsoft/wil/blob/master/include/wil/cppwinrt.h#L548-L555) helper function in the [Windows Implementation Library (WIL)](https://github.com/microsoft/wil):

    1. Add a reference to [Microsoft.Windows.ImplementationLibrary](https://www.nuget.org/packages/Microsoft.Windows.ImplementationLibrary/) NuGet package.
    2. Add the `#include <wil/cppwinrt.h>` statement to your code file.
    3. Use `wil::resume_foreground(your_dispatcher);` to `co_await` the result.

- **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI project templates](../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.

- **.NET apps must target build 18362 or higher**: Your TFM must be set to `net5.0-windows10.0.18362` or higher, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or higher. For more info, see the [known issue on GitHub](https://github.com/microsoft/ProjectReunion/issues/921).

- **C# projects using 1.0 Preview 1 must use the following .NET SDK**: .NET 5 SDK version 5.0.400 or later if you're using Visual Studio 2019 version 16.11.

- **Unpackaged apps not supported on Windows 10 version 1809**: This should be resolved in the next release.

## Related topics

- [Stable channel](stable-channel.md)
- [Experimental channel](experimental-channel.md)
- [Set up your development environment](set-up-your-development-environment.md)
- [Create a new project that uses the Windows App SDK](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](deploy-apps-that-use-the-windows-app-sdk.md)