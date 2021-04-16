---
title: WinUI 3 Project Reunion 0.5 (March 2021)
description: Overview of WinUI 3 Project Reunion 0.5.
ms.date: 03/19/2021
ms.topic: article
---

# Windows UI Library 3 - Project Reunion 0.5 (March 2021)

Windows UI Library (WinUI) 3 is a native user experience (UX) framework for building modern Windows apps.  It ships independently from the Windows operating system as a part of [Project Reunion](../../project-reunion/index.md).  The Project Reunion 0.5 release provides [Visual Studio project templates](https://aka.ms/projectreunion/vsixdownload) to help you start building apps with a WinUI 3-based user interface.

**WinUI 3 - Project Reunion 0.5** is the first stable, supported version of WinUI 3 that can be used to create production apps that can be published to the Microsoft Store. This release consists of the stability updates and general improvements that allow WinUI 3 to be forward-compatible and production-ready.


## Install WinUI 3 - Project Reunion 0.5

This new version of WinUI 3 is available as part of Project Reunion 0.5. To install, see:

**[Installation instructions for Project Reunion 0.5](../../project-reunion/get-started-with-project-reunion.md#set-up-your-development-environment)**

Now that WinUI ships as a part of Project Reunion, you'll download the Project Reunion Visual Studio Extension (VSIX) to get started, which includes a set of developer tools and components. For more on the Project Reunion package, see [Deploy apps that use Project Reunion](../../project-reunion/deploy-apps-that-use-project-reunion.md). The Project Reunion VSIX includes [WinUI Project Templates](winui-project-templates-in-visual-studio.md) that you'll use to build your WinUI 3 app. 

> [!NOTE]
> To see WinUI 3 controls and features in action, you can clone and build the WinUI 3 version of the [XAML Controls Gallery](#winui-3-controls-gallery) from GitHub.

> [!NOTE]
> To use WinUI 3 tooling such as Live Visual Tree, Hot Reload, and Live Property Explorer, you must enable WinUI 3 tooling with Visual Studio Preview Features as described in the [instructions here](https://github.com/microsoft/microsoft-ui-xaml/issues/4140).

Once you've set up your development environment, see [WinUI 3 project templates in Visual Studio](winui-project-templates-in-visual-studio.md) to familiarize yourself with the available Visual Studio Project and Item templates.

For more information about getting started with building a WinUI 3 app, see the following articles:

- [Get started with WinUI 3 for desktop apps](get-started-winui3-for-desktop.md)
- [Build a basic WinUI 3 app for desktop](desktop-build-basic-winui3-app.md)

Aside from the [limitations and known issues](#limitations-and-known-issues), building an app using the WinUI projects is  similar to building a UWP app with XAML and WinUI 2.x. Therefore, most of the [guidance documentation](/windows/uwp/design/) for UWP apps and the **Windows.UI** WinRT namespaces in the Windows SDK is applicable.

WinUI 3 API reference documentation is available here: [WinUI 3 API Reference](/windows/winui/api)

### WebView2

To use WebView2 with this WinUI 3 release, please download the Evergreen Bootstrapper or Evergreen Standalone Installer found on [this page](https://developer.microsoft.com/microsoft-edge/webview2/) if you don't already have the WebView2 Runtime installed. 

### Windows Community Toolkit

If you're using the Windows Community Toolkit, [download the latest version](https://aka.ms/wct-winui3).

### Visual Studio Support

In order to take advantage of the latest tooling features added into WinUI 3 like Hot Reload, Live Visual Tree, and Live Property Explorer, you must use the latest preview version of Visual Studio and be sure to enable WinUI tooling in Visual Studio Preview Features, as described in the [instructions here](https://github.com/microsoft/microsoft-ui-xaml/issues/4140). The table below shows the compatibility of Visual Studio 2019 versions with WinUI 3 - Project Reunion 0.5:

| VS Version  | WinUI 3 - Project Reunion 0.5  |
|---|---|
| 16.8  | No   |
| 16.9  | Yes, but with no Hot Reload, Live Visual Tree, or Live Property Explorer  |
| 16.10 Previews  | Yes, with all WinUI 3 tooling   |

## Update your existing WinUI 3 app

If you created an app with an earlier preview or release version of WinUI 3, you can update the project to use the latest release of WinUI 3 - Project Reunion 0.5. For instructions, see [Update existing projects to the latest release of Project Reunion](../../project-reunion/update-existing-projects-to-the-latest-release.md).

## Major changes introduced in this release

### Stable features

This release provides the stability and support to make WinUI 3 suitable for production apps that can ship to the Microsoft Store. It includes support and forward compatibility for most features introduced in past previews:

- Ability to create desktop apps with WinUI, including [.NET 5](https://github.com/dotnet/core/tree/master/release-notes/5.0) for Win32 apps
- [RadialGradientBrush](/windows/uwp/design/style/brushes#radial-gradient-brushes)
- [TabView updates](/windows/uwp/design/controls-and-patterns/tab-view)
- Dark theme updates
- Improvements and updates to [WebView2](/microsoft-edge/hosting/webview2)
  - Support for High DPI
  - Support for window resizing and moving
  - Updated to target more recent version of Edge
  - No longer necessary to reference a WebView2-specific Nuget package
- SwapChainPanel
- MRT Core Support
  - This makes apps faster and lighter on startup and provides quicker resource lookup.
- ARM64 Support
- Drag and drop inside and outside of apps
- RenderTargetBitmap (currently only XAML content - no SwapChainPanel content)
- Custom cursor support
- Off-thread input
- Improvements to our tooling/developer experience:
  - Live Visual Tree, Hot Reload, Live Property Explorer and similar tools
  - Intellisense for WinUI 3
- Improvements required for open source migration
- Custom titlebar capabilities: new [Window.ExtendsContentIntoTitleBar](/windows/winui/api/microsoft.ui.xaml.window.extendscontentintotitlebar) and [Window.SetTitleBar](/windows/winui/api/microsoft.ui.xaml.window.settitlebar) APIs that allow developers to create custom title bars in desktop apps.
- VirtualSurfaceImageSource support
- In-app acrylic

### Preview features

As this is a stable release, preview features have been removed from this version of WinUI 3. You can still access these features by using the current [preview version of WinUI 3](release-notes/winui3-project-reunion-0.5-preview.md). Please note the following key features are still in preview, and work to stabilize them is ongoing:

- UWP support
  - This means you cannot build or run a UWP app using the WinUI 3 - Project Reunion 0.5 VSIX. You'll need to use the [WinUI 3 - Project Reunion 0.5 Preview VSIX](https://aka.ms/projectreunion/previewdownload), and follow the rest of the instructions for setting up your development environment in [Get started with Project Reunion](../../project-reunion/get-started-with-project-reunion.md). See  [Windows UI Library 3 - Project Reunion 0.5 Preview (March 2021) release notes](release-notes/winui3-project-reunion-0.5-preview.md) for more information.

- Multi-window support in desktop apps

- Input validation

### Provide feedback and suggestions

We welcome your feedback in the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose).

### What's coming next?

For more information on when specific features are planned, see the [feature roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md#winui-30-feature-roadmap) on GitHub.

## Limitations and known issues

The following items are some of the known issues with WinUI 3 - Project Reunion 0.5. If you find an issue that isn't listed below, please let us know by contributing to an existing issue or filing a new issue through the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose).

### Platform and OS support

WinUI 3 - Project Reunion 0.5 is compatible with PCs running the Windows 10 October 2018 Update (version 1809 - build 17763) and newer.

### Developer tools

- Only C# and C++/WinRT apps are supported
- Desktop apps support .NET 5 and C# 9, and must be packaged in an MSIX app
- No XAML Designer support
- New C++/CX apps are not supported, however, your existing apps will continue to function (please move to C++/WinRT as soon as possible)
- Unpackaged desktop deployment is not supported
- When running a desktop app using F5, make sure that you are running the packaging project. Hitting F5 on the app project will run an unpackaged app, which WinUI 3 does not yet support.

### Missing Platform Features

- Xbox support
- HoloLens support
- Windowed popups
  - More specifically, the `ShouldConstrainToRootBounds` property always acts as if it's set to `true`, regardless of the property value.
- Inking support, including:
  - [InkCanvas](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.InkCanvas)
  - [HandwritingView](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.HandwritingView)
  - [InkPresenter](https://docs.microsoft.com/uwp/api/Windows.UI.Input.Inking.InkPresenter)
- Background acrylic
- MediaElement and MediaPlayerElement
- MapControl
- SwapChainPanel does not support transparency
- Global Reveal uses fallback behavior, a solid brush
- XAML Islands is not supported in this release
- Using WinUI 3 directly in an existing non-WinUI desktop app has the following limitation: The currently available path for migrating an existing app is to add a **new** WinUI 3 project to your solution, and adjust or refactor your logic as needed.

- Application.Suspending is not called in desktop apps. See API reference documentation on the [Application.Suspending Event](https://docs.microsoft.com/windows/winui/api/microsoft.ui.xaml.application.suspending?view=winui-3.0-preview&preserve-view=true ) for more details. 

- CoreWindow, ApplicationView, CoreApplicationView, CoreDispatcher and their dependencies are not supported in desktop apps (see below)

### CoreWindow, ApplicationView, CoreApplicationView, and CoreDispatcher in desktop apps

New in WinUI 3 Preview 4 and standard going forward, [CoreWindow](/uwp/api/Windows.UI.Core.CoreWindow), [ApplicationView](/uwp/api/Windows.UI.ViewManagement.ApplicationView), [CoreApplicationView](/uwp/api/Windows.ApplicationModel.Core.CoreApplicationView) [CoreDispatcher](/uwp/api/Windows.UI.Core.CoreDispatcher), and their dependencies are not available in desktop apps. For example, the [Window.Dispatcher](/uwp/api/Windows.UI.Xaml.Window.Dispatcher) property is always **null**, but the **Window.DispatcherQueue** property can be used as an alternative.

These APIs only work in UWP apps. In past previews they've partially worked in desktop apps as well, but since Preview 4 they've been fully disabled. These APIs are designed for the UWP case where there is only one window per thread, and one of the features of WinUI 3 is to enable multiple in the future.

There are APIs that internally depend on existence of these APIs, which consequently aren't supported in a desktop app. These APIs generally have a static `GetForCurrentView` method. For example [UIViewSettings.GetForCurrentView](/uwp/api/Windows.UI.ViewManagement.UIViewSettings.GetForCurrentView).

For more information on affected APIs as well as workarounds and replacements for these APIs, please see [WinRT API changes for desktop apps](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/winrt-apis-for-desktop.md)

### Known issues

- The [UISettings.ColorValuesChanged Event](/uwp/api/windows.ui.viewmanagement.uisettings.colorvalueschanged) and [AccessibilitySettings.HighContrastChanged Event](/uwp/api/windows.ui.viewmanagement.accessibilitysettings.highcontrastchanged) are no longer supported in desktop apps. This may cause issues if you are using it to detect changes in Windows themes. 

- Previously, to get a CompositionCapabilities instance you would call [CompositionCapabilites.GetForCurrentView()](/uwp/api/windows.ui.composition.compositioncapabilities.getforcurrentview). However, the capabilities returned from this call were *not* dependent on the view. To address and reflect this, we've deleted the GetForCurrentView() static in this release, so now you can create a [CompositionCapabilties](/uwp/api/windows.ui.composition.compositioncapabilities) object directly.

- You may recieve a build error due to mismatched versions of the .NET SDK and the winrt.runtime.dll. As a workaround, you can try the following:

    - Explicitly set your .NET SDK to the latest version. To do this, add the following item group to your .csproj file, then save your project:

      ```xml
      <ItemGroup>            
          <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" RuntimeFrameworkVersion="10.0.18362.16" />
          <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" TargetingPackVersion="10.0.18362.16" />
      </ItemGroup>
      ```

    Note that once .NET 5.0.6 is available in May, these lines can be removed. 


## WinUI 3 Controls Gallery

Check out the WinUI 3 Controls Gallery (previously called _XAML Controls Gallery - WinUI 3 version_) for a sample app that includes all controls and features that are a part of WinUI 3 - Project Reunion 0.5.

![WinUI 3 Controls Gallery app](images/WinUI3XamlControlsGallery.png)<br/>
*Example of the WinUI 3 Controls Gallery app*

You can download the sample by cloning the GitHub repo. To do this, clone the **winui3** branch using the following command:

```
git clone --single-branch --branch winui3 https://github.com/microsoft/Xaml-Controls-Gallery.git
```

After cloning, ensure that you switch to the **winui3** branch in your local Git environment: 

```
git checkout winui3
```
