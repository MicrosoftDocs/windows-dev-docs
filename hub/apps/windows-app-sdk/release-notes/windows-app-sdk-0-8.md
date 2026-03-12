---
title: Windows App SDK 0.8 release notes
description: Provides information about what's new in Windows App SDK 0.8.
ms.topic: release-notes
ms.date: 09/22/2025
keywords: windows win32, windows app development, Windows App SDK, release notes
ms.localizationpriority: medium
zone_pivot_groups: wasdk-release-channels
---

# Windows App SDK 0.8 release notes

[!INCLUDE [wasdk-releasenotes](../../../includes/wasdk-release-notes.md)]

:::zone pivot="stable"

## Version 0.8.12

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 0.8.0 release.

<details><summary>Bug fixes</summary>

> - Fixed issue where apps with [SwapChainPanel](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.swapchainpanel) or [WebView2](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.webview2) would unpredictably crash due to an access violation.

</details>

---

## Version 0.8.11

<details><summary>Bug fixes</summary>

> - Fixed regression causing the lost focus event to fire when selecting text using mouse.

</details>

---

## Version 0.8.10

<details><summary>Bug fixes</summary>

> - Fixed issues causing apps to sometimes crash during a drag and drop operation.
>
> [!NOTE]
> Windows App SDK 0.8.9 was not released. The version released directly after 0.8.8 is 0.8.10.

</details>
---
---

## Version 0.8.8

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 0.8.0 release.

> [!NOTE]
> For C# developers, one of the following .NET SDK versions (or later) is required: 6.0.202, 6.0.104, 5.0.407, 5.0.213. To update your .NET SDK version, visit [.NET Downloads](https://dotnet.microsoft.com/download) or update to the latest version of Visual Studio. Without the required .NET SDK version, when updating your NuGet package you will see an error like: *"This version of WindowsAppSDK requires WinRT.Runtime.dll version 1.6 or greater."*.

<details><summary>Bug fixes</summary>

> - Fixed touch input issues in TextBox regarding soft keyboard and general interaction. These issues also affected keyboard shortcuts. For more information, see [issue 6291](https://github.com/microsoft/microsoft-ui-xaml/issues/6291) on GitHub.
> - Fixed issue where an app window would sometimes show as inactive when active.
> - Fixed performance issue caused by UIA (UI Automation) running in external processes.
> - Fixed app stability issue with pen input.
> - Fixed issue where the render of png icons in a Menu are dramatically delayed because of UIA.

</details>

---

## Version 0.8.7

This is a servicing release of the Windows App SDK that includes several performance updates for C#/.NET applications. To update to this version, you'll need to reference the latest Windows SDK package version. To do that, add the property `<WindowsSdkPackageVersion>10.0.<sdk_version>.24</WindowsSdkPackageVersion>` to your `.csproj` file with the SDK version your app is targeting from the `TargetFramework` property. For example:

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
        <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
        <WindowsSdkPackageVersion>10.0.19041.24</WindowsSdkPackageVersion>
    </PropertyGroup>
    ...
```

This version of the Windows SDK projection will be available in an upcoming .NET 6 servicing release. After that .NET SDK update is available, you should remove the `<WindowsSdkPackageVersion>` property from your project file.

If you don't set this property, then you'll see an error like: `"Error: This version of Project Reunion requires WinRT.Runtime.dll version 1.6 or greater."`

---

## Version 0.8.6

This is a servicing release of the Windows App SDK that includes several performance improvements for C#/.NET applications for the 0.8.0 release.

To update to this version of Windows App SDK, you will need to have the latest .NET SDK December update installed (see [Download .NET](https://dotnet.microsoft.com/download) and [.NET 5 will reach End of Support on May 10, 2022](https://devblogs.microsoft.com/dotnet/dotnet-5-end-of-support-update/)). If you don't have the minimum required version of the .NET SDK installed, then you'll see an error like `"Error: This version of Project Reunion requires WinRT.Runtime.dll version 1.4 or greater."`

<details><summary>Bug fixes</summary>

> For a detailed list of the performance improvements, see the [C#/WinRT 1.4.1 release notes](https://github.com/microsoft/CsWinRT/releases/tag/1.4.1.211117.1).

</details>

---

## Version 0.8.5

This is a servicing release of the Windows App SDK that includes more critical bug fixes for the 0.8.0 release.

<details><summary>Bug fixes</summary>

> - Fixed issue that was causing WinUI apps using [pointer input](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.input.pointer) to crash.
> - Fixed issue causing the title bar buttons (min, max, close) to not have rounded corners on Windows 11.
> - Fixed issue causing the resizing layout options to not appear when hovering over maximize/restore button on Windows 11.
> - Fixed issue causing a crashing exception where creating a [PointCollection](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.pointcollection) object. For more information, see [issue 971](https://github.com/microsoft/CsWinRT/issues/971) on Github.
>

</details>

---

## Version 0.8.4

This is a servicing release of the Windows App SDK that includes more critical bug fixes for the 0.8.0 release.

<details><summary>Bug fixes</summary>

> - Fixes for custom title bars so that [ContentDialog](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentdialog) doesn't cover it up, and the title bar buttons are rounded.
> - Fix for a crash in image processing when the display scale is changed.
> - Fixes clipping bugs where UI disappear or clipped incorrectly
>

</details>

---

## Version 0.8.3

This is a servicing release of the Windows App SDK that includes more critical bug fixes for the 0.8.0 release.

<details><summary>Bug fixes</summary>

> - Keyboard focus was being lost when a window was minimized and then restored, requiring a mouse click to restore focus.
>

</details>

---

## Version 0.8.2

This is a servicing release of the Windows App SDK that includes more critical bug fixes for the 0.8.0 release.

<details><summary>Bug fixes</summary>

> - Windows App SDK and WinUI 3 are now supported in Visual Studio 2022 Preview 2 and later.
> - For .NET apps, you may receive the following error when passing in an array of enums: `Object contains non-primitive or non-blittable data.`
> - Writing using the HandWriting Panel inside a textbox causes a crash
> - Icons/images always load at their 100% scale value rather than based on the monitor scale value
> - Garbage collection of **EventSource\<T\>** causes subsequent failure to unsubscribe handlers (see [GitHub issue](https://github.com/microsoft/CsWinRT/issues/842) for more details)
> - Security fix – see [CVE-2021-34533](https://msrc.microsoft.com/update-guide/en-US/vulnerability/CVE-2021-34533) for more details.
> - [SwapChainPanel.CompositionScaleChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.swapchainpanel.compositionscalechanged) sometimes returning incorrect CompositionScale values after changing display scale
>

</details>

---

## Version 0.8.1

This is a servicing release of the Windows App SDK that includes a few critical bug fixes for the 0.8.0 release.

<details><summary>Bug fixes</summary>

> - Windows App SDK cannot run on the latest Windows Insider build
> - Crash in EditableComboBox when entering a value that does not appear in dropdown
> - WebView2 doesn't allow user to tab out once focused has been received
> - Fully qualify Windows.Foundation.Metadata.DefaultOverload namespace in WinUI generated code to avoid namespace ambiguity
>   - This fixes bug [#5108](https://github.com/microsoft/microsoft-ui-xaml/issues/5108).
> - Security fix – see [CVE-2021-34489](https://msrc.microsoft.com/update-guide/en-US/vulnerability/CVE-2021-34489) for more details.
>

</details>

---

## Version 0.8.0 Stable

This release supports all [stable channel features](../../windows-app-sdk/release-channels.md#features-available-by-release-channel).

<details><summary>WinUI 3</summary>
>
> This release includes many bug fixes and improved stabilization across WinUI 3. These are all of the new changes in WinUI 3 since the release of WinUI 3 - Project Reunion 0.5:
>
> - The Pivot control has been added back in and can now be used in any WinUI app.
> - All bug fixes from Project Reunion v0.5.5, v0.5.6, and v0.5.7 are included with this release.
> - New bug fixes, including:
>   - Mouse right-click in TextBox crashes the application
>   - NavigationView causes crash in UWP, Reunion 0.5 Preview
>   - ProgressBar doesn't show difference between Paused and Error option
>   - Crash in RichEditBox when copying/pasting/changing text style
>   - Window caption buttons are misplaced when SetTitleBar is not set or null
>
>   **For the full list of bugs addressed in this release, see [our GitHub repo](https://aka.ms/winui3/0.8/bugs-fixed).**
>
> - The `ColorHelper.ToDisplayName` API is no longer available.
> - The following types have been removed:
>   - `Microsoft.Graphics.IGeometrySource2D`
>   - `Microsoft.Graphics.IGeometrySource2DInterop`
>
>   Use `Windows.Graphics.IGeometrySource2D` and `Windows.Graphics.IGeometrySource2DInterop` instead.
>
> - All types in the `Microsoft.System` namespace have been moved to the `Microsoft.UI.Dispatching` namespace, including the [DispatcherQueue class](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue).
>
> - The `AcrylicBrush.BackgroundSource` property has been removed, since `HostBackdrop` is not supported as a `BackgroundSource` in WinUI 3.
>
> For more information on WinUI, see [WinUI](../../winui/index.md).
>
> To see WinUI 3 controls and features in action, you can clone and build the WinUI 3 Gallery app [from GitHub](https://github.com/microsoft/WinUI-Gallery/tree/main), or download the app [from the Microsoft Store](https://apps.microsoft.com/detail/9P3JFPWWDZRC).
>
> To get started developing with WinUI, check out the following articles:
>
> - [WinUI 3 templates in Visual Studio](../../winui/winui3/winui-project-templates-in-visual-studio.md)
> - [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
> - [Build a C# .NET app with WinUI 3 and Win32 interop](../../winui/winui3/desktop-winui3-app-with-basic-interop.md)
> - [WinUI 3 API Reference](/windows/winui/api)

</details>

<details><summary>DWriteCore</summary>
>
> This release of DWriteCore includes the following new and updated features. DWriteCore is introduced and described in the [DWriteCore overview](/windows/win32/directwrite/dwritecore-overview).
>
> - DWriteCore now has support for underline&mdash;see [**IDWriteTextLayout::GetUnderline**](/windows/win32/api/dwrite/nf-dwrite-idwritetextlayout-getunderline) and [**IDWriteTextLayout::SetUnderline**](/windows/win32/api/dwrite/nf-dwrite-idwritetextlayout-setunderline).
> - Support for strikethrough&mdash;see [**IDWriteTextLayout::GetStrikethrough**](/windows/win32/api/dwrite/nf-dwrite-idwritetextlayout-getstrikethrough) and [**IDWriteTextLayout::SetStrikethrough**](/windows/win32/api/dwrite/nf-dwrite-idwritetextlayout-setstrikethrough).
> - Support for vertical text via [**IDWriteTextLayout**](/windows/win32/api/dwrite/nn-dwrite-idwritetextlayout)&mdash;see [Vertical text](/windows/win32/directwrite/vertical-text).
> - All of the methods of the [**IDWriteTextAnalyzer**](/windows/win32/api/dwrite/nn-dwrite-idwritetextanalyzer) and [**IDWriteTextAnalyzer1**](/windows/win32/api/dwrite_1/nn-dwrite_1-idwritetextanalyzer1) interfaces are implemented.
> - The [**DWriteCoreCreateFactory**](/windows/windows-app-sdk/api/win32/dwrite_core/nf-dwrite_core-dwritecorecreatefactory) free function creates a factory object that is used for subsequent creation of individual DWriteCore objects.
>
> [!NOTE]
> [**DWriteCoreCreateFactory**](/windows/windows-app-sdk/api/win32/dwrite_core/nf-dwrite_core-dwritecorecreatefactory) is functionally the same as the [**DWriteCreateFactory**](/windows/win32/api/dwrite/nf-dwrite-dwritecreatefactory) function exported by the system version of DirectWrite. The DWriteCore function has a different name to avoid ambiguity in the event that you link both `DWriteCore.lib` and `DWrite.lib`.
>
> For DWriteCore and DirectWrite API reference, see [DWriteCore API Reference](/windows/windows-app-sdk/api/win32/_dwritecore/) and [DirectWrite API Reference](/windows/win32/directwrite/reference).

</details>

<details><summary>MRTCore</summary>

> - The **Build Action** for resources is automatically set when you add the resource to your project, reducing the need for manual project configuration.

</details>

<details><summary>Limitations</summary>

>
> - This release is not currently supported on the Dev Channel of the [Windows Insider Program](https://insider.windows.com). **This is fixed in version 0.8.1**.
>
> - Desktop apps (C# or C++ desktop): This release is supported for use only in desktop apps (C++ or C#) that are packaged using MSIX. To use the Windows App SDK in unpackaged desktop apps, you must use the experimental release channel.

</details>

<details><summary>Known issues</summary>

>
> - WinUI 3 tooling such as Live Visual Tree, Live Property Explorer, and Hot Reload in version 0.8 and later requires Visual Studio 2019 16.11 Preview 3 and later.
> - Apps currently using WinUI and the Windows App SDK 0.8 cannot use class libraries that use Project Reunion 0.5. Update the class libraries to use the Windows App SDK 0.8.
> - .NET apps must target build 18362 or later: Your TFM must be set to net6.0-windows10.0.18362 or later, and your packaging project's must be set to 18362 or later. For more info, see [GitHub issue #921](https://github.com/microsoft/WindowsAppSDK/issues/921).
> - You may encounter a crash when switching frequently between light and dark mode.
> - For .NET apps, you may receive the following error when passing in an array of enums: `Object contains non-primitive or non-blittable data.` **This is fixed in version 0.8.2**.
> - For .NET apps, there is currently no way  to opt out of an image getting indexed as an app resource using the Visual Studio UI. To work around this, add a Directory.Build.targets (see [Customize your build - Visual Studio](/visualstudio/msbuild/customize-your-build) for instructions) to the project and remove the image(s) as follows:
>
>   - To remove specific images (note that the relative path is needed):
>
>     ```xml
>     <Project>
>     <ItemGroup>
>         <Content Remove="..\Bitmap1.bmp" />
>     </ItemGroup>
>     </Project>
>     ```
>
>     - To remove images based on metadata:
>
>     ```xml
>     <Project>
>     <ItemGroup>
>         <Content Remove="@(None->WithMetadataValue('Pack','true'))" />
>     </ItemGroup>
>     </Project>
>     ```
>
>     A fix for this issue is planned for an upcoming release - at that point, the above workarounds will no longer be needed.

</details>

:::zone-end

:::zone pivot="preview"

## Version 0.8 Preview (0.8.0-preview)

<details><summary>WinUI 3</summary>

> This release introduces critical bug fixes (including those fixed in 0.5 servicing releases) and other changes for WinUI. For a complete list, see the [Overview and release notes: WinUI 3 - Project Reunion 0.8 Preview (May 2021)](../../winui/winui3/release-notes/release-notes-08-preview.md).

</details>

<details><summary>App lifecycle (experimental feature)</summary>

> This release introduces new experimental features related to managing the app lifecycle of your app.
>
> - All apps (packaged and unpackaged) can use **GetActivatedEventArgs** (although packaged apps can already use the implementation of this in the platform).
> - Only unpackaged apps can use the **RegisterForXXXActivation** functions.
> - Packaged desktop apps can use app lifecycle instancing.
>
> For more information, see [App instancing with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-instancing.md) and [Rich activation with the app lifecycle API](../../windows-app-sdk/applifecycle/applifecycle-rich-activation.md).

</details>

<details><summary>Deployment for unpackaged apps (experimental feature)</summary>

> This release introduces new experimental deployment features for unpackaged apps. Unpackaged apps can now dynamically take a dependency on the Windows App SDK runtime packages so you can continue using your existing MSI or setup program for app deployment. This is available through the following features:
>
> - Standalone installer for Windows App SDK.
> - MSIX package bundle that includes dynamic dependencies functionality.
>
> For more info, see [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](../../windows-app-sdk/deploy-unpackaged-apps.md).

</details>

<details><summary>DWriteCore</summary>

> This release adds the following features:  
>
> - Text decorations (underline and strikethrough in the text layout API)
> - Vertical text layout
> - Font face kerning API
> - Experimental support for unpackaged apps
>
> For more information, see [DirectWrite to DWriteCore migration](../../windows-app-sdk/migrate-to-windows-app-sdk/guides/dwritecore.md).

</details>

<details><summary>MRT Core</summary>

> This release adds the following features:
>
> - The build action for resources is now automatically set, reducing the need for manual project configuration.
> - Experimental support for unpackaged apps.
>
> For more information, see [Manage resources with MRT Core](../../windows-app-sdk/mrtcore/mrtcore-overview.md).

</details>

<details><summary>Limitations and known issues</summary>

> - **No support for Any CPU build configuration**: The Windows App SDK is written in native code and thus does not support **Any CPU** build configurations. The [WinUI 3 templates in Visual Studio](../../winui/winui3/winui-project-templates-in-visual-studio.md) only allow architecture-specific builds. When [adding the Windows App SDK](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md) to an existing .NET application or component that supports **Any CPU**, you must specify the desired architecture: `x86`, `x64` or `arm64`.
> - **.NET apps must target build 18362 or later**: Your TFM must be set to `net6.0-windows10.0.18362` or later, and your packaging project's `<TargetPlatformVersion>` must be set to 18362 or later. For more info, see the [known issue on GitHub](https://github.com/microsoft/WindowsAppSDK/issues/921).

</details>

<details><summary>Samples</summary>

> The [Windows App SDK samples](https://github.com/microsoft/Project-Reunion-Samples) do not work with this release.

</details>

:::zone-end

:::zone pivot="experimental"

**There are no experimental releases for this version.**

:::zone-end