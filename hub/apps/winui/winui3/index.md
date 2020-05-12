---
title: WinUI 3.0 Preview 1 (May 2020)
description: Overview of the WinUI 3.0 Preview.
ms.date: 05/11/2020
ms.topic: article
---

# Windows UI Library (WinUI) 3.0 Preview 1 (May 2020)

WinUI 3.0 is a major update to the Windows UI Library that transforms WinUI into a full UX framework for all types of Windows apps—from Win32 to UWP.

## New features in WinUI 3.0 Preview 1

- Ability to create Win32 Apps with WinUI, including .NET 5 for Win32 apps
- All the new features of WinUI 2.4
  - RadialGradientBrush
  - Hierarchical Navigation View
  - ProgressRing
  - TabView updates
  - Dark Theme updates
- Improvements and updates to the WebView2
- SwapChainPanel
- Improvements required for open source migration

For more information on the roadmap and benefits of WinUI 3.0, see the [Windows UI Library Roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md) on GitHub.

We welcome your feedback in the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml).

> [!Important]
> This WinUI 3.0 preview release is intended for early evaluation and to gather feedback from the developer community. It should **NOT** be used for production apps. The forward-compatible official release is scheduled for later this year.
>
> **See [Preview 1 limitations and known issues](#preview-1-limitations-and-known-issues)**.

## Try WinUI 3.0 Preview 1

### ALERT: Insert big download link here, but remind them to look below for detailed instructions

To try out WinUI 3.0 Preview 1, install the latest Visual Studio Preview then either start with a blank [Visual Studio project template](#visual-studio-project-templates), or clone and build the [Xaml Controls Gallery (WinUI 3.0 Preview 1 branch)](#xaml-controls-gallery-winui-30-preview-1-branch).

### Set up Visual Studio

> [!IMPORTANT]
> Visual Studio 2019 Preview version 16.4 or newer is required for building apps using WinUI 3.0 Preview 1.

See [Visual Studio Preview](https://visualstudio.microsoft.com/vs/preview) to download the latest Visual Studio Preview.

You must include the following workloads when installing the Visual Studio Preview:

- .NET Desktop Development
- Universal Windows Platform development

To build C++ apps you must also include the following workloads:

- Desktop development with C++
- The *C++ (v142) Universal Windows Platform tools* optional component for the Universal Windows Platform workload

> [!NOTE]
> If you are running the April 2018 Update (1803), you may need to manually install the Visual C redistributable for Visual Studio 2019. For more details, see [The latest supported Visual C++ downloads](https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads).

### Visual Studio project templates

To access the WinUI 3.0 Preview 1 and project templates, go to **https://ourmarketplacelink**

After completing the form, you must download a Visual Studio Extension (`.vsix`) to add the WinUI project templates to Visual Studio 2019.

For directions on how to add the `.vsix` to Visual Studio, see [Finding and Using Visual Studio Extensions](https://docs.microsoft.com/visualstudio/ide/finding-and-using-visual-studio-extensions?view=vs-2019#install-without-using-the-manage-extensions-dialog-box).

After installing the `.vsix` extension you can create a new WinUI 3.0 project by searching for "WinUI" and selecting one of the available C# or C++ templates.

![WinUI 3.0 Visual Studio Templates](images/WinUI3Templates.png)<br/>
*Example of the WinUI 3.0 Visual Studio Templates*

### Visual Studio project configuration

To use WinUI 3.0 Preview 1, the Visual Studio project **Target version** must be set to **18362 or higher**, and the **Min version** must be set to **17134 or higher**.

Right click on the Visual Studio project and select Properties to change these values.

#### VCLibs deployment error

If your app fails to deploy with an error similar to the following:

> `DEP0700: Registration of the app failed. [0x80073CF3] Windows cannot install package <app package name> because this package depends on a framework that could not be found. Provide the framework "Microsoft.VCLibs.140.00.UWPDesktop" published by "CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US", with neutral or x86 processor architecture and minimum version 14.0.24217.0, along with this package to install. The frameworks with name "Microsoft.VCLibs.140.00.UWPDesktop" currently installed are: {}`

Then perform the following steps:

1. In the `Packages.appxmanifest` file, delete this `PackageDependency` declaration:

> `<PackageDependency Name="Microsoft.VCLibs.140.00.UWPDesktop" MinVersion="14.0.24217.0" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" />`

2. In the project file (`.csproj` or `.vcxproj`), add this `ItemGroup` declaration to the end of the `<Project>` section:

    ```xaml
    <ItemGroup>
        <SDKReference Include="Microsoft.VCLibs.Desktop, Version=14.0">
            <Name>Visual C++ 2015 UWP Desktop Runtime for native apps</Name>
        </SDKReference>
    </ItemGroup>
    ```

## Build apps

Aside from the limitations and known issues outlined below, building an app using WinUI 3.0 Preview 1 is very similar to building a UWP app with Xaml and WinUI 2.x, so most of the guidance and documentation for UWP apps and `Windows.UI` APIs is applicable.

For more information on how to create a UWP app, see [Start coding](https://docs.microsoft.com/windows/uwp/get-started/create-uwp-apps).

The main difference is that WinUI 3 APIs are in the `Microsoft.UI` namespace instead of the `Windows.UI` namespace, so you might need to update the namespaces when copying and pasting sample code. Similarly, libraries and components using UI controls from `Windows.UI.Xaml` are not compatible with WinUI 3.0 and must be updated to `Microsoft.UI.Xaml`.

## Xaml Controls Gallery (WinUI 3.0 Preview 1 branch)

See the [WinUI 3.0 Preview 1 branch of the Xaml Controls Gallery](https://github.com/microsoft/Xaml-Controls-Gallery/tree/winui3preview1) for a sample app that includes all WinUI 3.0 Preview 1 controls and features.

![WinUI 3.0 Preview 1 Xaml Controls Gallery app](images/WinUI3XamlControlsGallery.png)<br/>
*Example of the WinUI 3.0 Preview 1 Xaml Controls Gallery app*

To download the sample, clone the **winui3preview1** branch using the following command:

> `git clone --single-branch --branch winui3preview1 https://github.com/microsoft/Xaml-Controls-Gallery.git`

After cloning, ensure that you switch to the **winui3preview1** branch in your local Git environment:

> `git checkout winui3preview1`

## Preview 1 limitations and known issues

The Preview 1 build is incomplete, so we expect some bugs, limitations, and issues.

The following items are some of the known issues with WinUI 3.0 Preview 1. If you find an issue that isn't listed below, please let us know by contributing to an existing issue or filing a new issue on the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues).

### Project types

**At this time, C# 8 and .NET Core are not supported**. Future updates are planned that include support for other project types and languages, including .NET Core, and C# 8.

### Platform and OS support

WinUI 3.0 Preview 1 is compatible with PCs running the Windows 10 April 2018 Update (1803) and newer.

Future updates will include support for a wider range of Windows versions and devices.

### Missing Platform Features

- Background Acrylic
- MediaElement and MediaPlayerElement
- RenderTargetBitmap
- MapControl
- WinUI 3 content can only be in one window per process or one ApplicationView per app
- Intellisense is not currently supported
- Tooling limitations:
  - No visual designer
  - No hot reload
  - No live visual tree
- Unpackaged deployment is not supported
- New C++/CX apps are not supported, however, your existing apps will continue to function (please move to C++/WinRT as soon as possible)
- No ARM64 support
- SwapChainPanel does not support transparency
- In C# you need to use `WinRT.WeakReference<T>` rather than `System.WeakReference<T>`.
- Global Reveal uses fallback behavior, a solid brush
- Development with VS Code is not yet supported
- XAML Islands is not supported in this release
- 3rd party ecosystem libraries will not function
- No inking support
- Win10 desktop only (no support for xbox, hololens, etc.)
- No support for windowed popups
