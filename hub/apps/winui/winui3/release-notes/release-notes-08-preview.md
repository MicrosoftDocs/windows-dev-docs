---
title: WinUI 3 Project Reunion 0.8 Preview (April 2021)
description: Overview of WinUI 3 Project Reunion 0.8 Preview.
ms.date: 05/25/2021
ms.topic: article
---

# Overview and release notes: Windows UI Library 3 - Project Reunion 0.8 Preview (May 2021)

Windows UI Library (WinUI) 3 is a native user experience (UX) framework for building modern Windows apps.  It ships independently from the Windows operating system as a part of [Project Reunion](../../../project-reunion/index.md).  The Project Reunion 0.8 Preview release provides [Visual Studio project templates](../winui-project-templates-in-visual-studio.md) to help you start building apps with a WinUI 3-based user interface.

**WinUI 3 - Project Reunion 0.8 Preview** is a pre-release version of WinUI 3 that includes bug fixes, general improvements, and experimental features - some of which will be stabilized for the Project Reunion 0.8 stable release in June 2021. 

> [!Important]
> This preview release is intended for early evaluation and to gather feedback from the developer community. It should **NOT** be used for production apps.
>
> If you're looking to ship a production app using Project Reunion and WinUI 3, see [Overview and release notes: WinUI 3 - Project Reunion 0.5](../index.md).
>
> Please use the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml) to provide feedback and log suggestions and issues.

## Install WinUI 3 - Project Reunion 0.8 Preview

This version of WinUI 3 is available as part of the Project Reunion 0.8 Preview. To install, see:

**[Installation instructions for Project Reunion 0.8 Preview](../../../project-reunion/set-up-your-development-environment.md)**

Since WinUI ships as a part of Project Reunion, you'll download the Project Reunion Visual Studio Extension (VSIX) to get started, which includes a set of developer tools and components. For more on the Project Reunion package, see [Deploy apps that use Project Reunion](../../../project-reunion/deploy-apps-that-use-project-reunion.md). The Project Reunion VSIX includes [WinUI Project Templates](../winui-project-templates-in-visual-studio.md) that you'll use to build your WinUI 3 app. 

> [!NOTE]
> To see WinUI 3 controls and features in action, you can clone and build the [WinUI 3 Controls Gallery](#winui-3-controls-gallery) from GitHub, or download it from the [Microsoft Store](https://www.microsoft.com/en-us/p/winui-3-controls-gallery/9p3jfpwwdzrc).

Once you've set up your development environment, see [WinUI 3 project templates in Visual Studio](../winui-project-templates-in-visual-studio.md) to familiarize yourself with the available Visual Studio Project and Item templates. 

For more information about getting started with building a WinUI 3 app, see the following articles:

- [Get started with WinUI 3 for desktop apps](../get-started-winui3-for-desktop.md)
- [Build a basic WinUI 3 app for desktop](../desktop-build-basic-winui3-app.md)
- [Build a basic WinUI 3 app for UWP](../get-started-winui3-for-uwp.md)

Aside from the [limitations and known issues](#limitations-and-known-issues), building an app using the WinUI projects is  similar to building a UWP app with XAML and WinUI 2.x. Therefore, most of the [guidance documentation](/windows/uwp/design/) for UWP apps and the **Windows.UI** WinRT namespaces in the Windows SDK is applicable.

WinUI 3 API reference documentation is available here: [WinUI 3 API Reference](/windows/winui/api)

### WebView2

To use WebView2 with this WinUI 3 release, please download the Evergreen Bootstrapper or Evergreen Standalone Installer found on [this page](https://developer.microsoft.com/microsoft-edge/webview2/) if you don't already have the WebView2 Runtime installed. 

### Windows Community Toolkit

If you're using the Windows Community Toolkit, [download the latest version](https://aka.ms/wct-winui3).

### Visual Studio Support

In order to take advantage of the latest tooling features added into WinUI 3 like Hot Reload, Live Visual Tree, and Live Property Explorer, you must use a preview version of Visual Studio 2019 16.10. Please note that Visual Studio preview releases are pre-release products, so you may run into bugs and limitations when using preview versions of Visual Studio to build WinUI 3 apps.  

The table below shows the compatibility of Visual Studio 2019 versions with WinUI 3 - Project Reunion 0.5. 

| VS Version  | WinUI 3 - Project Reunion 0.5  |
|---|---|
| 16.8  | No   |
| 16.9  | Yes, but with no Hot Reload, Live Visual Tree, or Live Property Explorer  |
| 16.10 Previews  | Yes, with all WinUI 3 tooling (in preview)  |

## Updating your existing WinUI 3 app

If you created an app with an earlier preview or stable version of WinUI 3, you can update the project to use WinUI 3 - Project Reunion 0.8 Preview. For instructions, see [Update existing projects to the latest release of Project Reunion](../../../project-reunion/update-existing-projects-to-the-latest-release.md).


## Major changes introduced in this release
- Pivot control is now available to use in this release.
- Most of the critical bug fixes from the Project Reunion v0.5.5, v0.5.6, and v0.5.7 servicing releases are included in this release as well. For servicing fixes that didn't make it into this preview, see [Known Issues](#known-issues). 


## List of bugs fixed in WinUI 3 - Project Reunion 0.8 Preview

- x:Bind does not work in a custom MenuFlyoutItem (more generally, x:Bind in ControlTemplate doesn't work if the parent namescope also uses x:Bind)
  - Thanks to @lhak for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/4179)!

- Hide windowed popups when the top-level window (or island) moves

- StandardUICommand page in WinUI 3 Controls Gallery not showing everything 

- Mouse right-click in TextBox crashes the application
  - Thanks to @Herdubreid for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/4804)!

- Context menu displayed at the wrong location for EditableComboBox

- Changing RichTextBlock Selection with touch makes selection indicator go away

- Microsoft C++ exception: winrt::hresult_error at memory location 0x... when NavigationView is being used
  - Thanks to @LeftTwixWand for filing [this issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/4806)!

- WinUI 3 error message needs rewording: "Cannot resolve 'Windows.metadata'. Please install the Windows Software Development Kit. The Windows SDK is installed with Visual Studio."

- VSM Setter quirk for raising exception has backwards logic
  - Thanks to @HppZ for filiing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/1048)!

- Move focus off the WebView2 (if necessary) when browser process crashes

- ProgressBar doesn't show difference between Paused and Error option
  - Thansk to @j0shuams for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3694)!

- PointerReplay constantly running

- NavigationView causes crash in UWP, Reunion 0.5 Preview
  - Thanks to @kalin-todorov for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/4499)!

- AutoSuggestBox, ComboBox, and CommandBarFlyout aren't setting ShouldConstrainToRootBounds="false" on their popup

- WinUI 3 - Project Reunion 0.5 generating C++ exceptions for a C# managed app
  - Thanks to @Noemata for filing [this issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/4510)!

- WebView2 initial tab focus lost

- WebView2 crashes when DPI changes after Close()

- Change in Appearance of AppBarButton with Flyout on CommandBar in WinUI3
  - Thanks to @eleanorleffler for filing [this issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/4197)!



## Features and capabilities introduced in past WinUI 3 Previews

The following features and capabilities were introduced in past WinUI 3 preview releases and continue to be supported in WinUI 3 - Project Reunion 0.8 Preview. 

> [!NOTE]
> Some of the following features will continue to be a part of WinUI 3 previews, but may not be a part of the next supported release. These features are marked as experimental and will throw a warning when used in an app. APIs that are a part of the WinUI 2.6 pre-release are also marked as experimental in this release.

- Ability to create desktop apps with WinUI, including [.NET 5](https://github.com/dotnet/core/tree/master/release-notes/5.0) for Win32 apps
- Preview-level support for building UWP apps
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
- Multi-window support in desktop apps
- Input validation

For more information on both the benefits of WinUI 3 and the WinUI roadmap, see the [Windows UI Library Roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md) on GitHub.


### Provide feedback and suggestions

We welcome your feedback in the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose).

### What's coming next?

For more information on when specific features are planned, see the [feature roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md#winui-30-feature-roadmap) on GitHub.

## Limitations and known issues

The WinUI 3 - Project Reunion 0.8 Preview is just that, a preview. Please expect bugs, limitations, and other issues.

The following items are some of the known issues with WinUI 3 - Project Reunion 0.8. If you find an issue that isn't listed below, please let us know by contributing to an existing issue or filing a new issue through the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose).

### Platform and OS support

WinUI 3 - Project Reunion 0.8 Preview is compatible with PCs running the Windows 10 October 2018 Update (version 1809 - build 17763) and newer.

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
  - [InkCanvas](/uwp/api/Windows.UI.Xaml.Controls.InkCanvas)
  - [HandwritingView](/uwp/api/Windows.UI.Xaml.Controls.HandwritingView)
  - [InkPresenter](/uwp/api/Windows.UI.Input.Inking.InkPresenter)
- Background acrylic
- MediaElement and MediaPlayerElement
- MapControl
- SwapChainPanel does not support transparency
-	AcrylicBrush and other effects using a CompositionBackdropBrush can’t sample from a SwapChainPanel or WebView2.
- Global Reveal uses fallback behavior, a solid brush
- XAML Islands is not supported in this release
- Using WinUI 3 directly in an existing non-WinUI desktop app has the following limitation: The currently available path for migrating an existing app is to add a **new** WinUI 3 project to your solution, and adjust or refactor your logic as needed.

- Application.Suspending is not called in desktop apps. See API reference documentation on the [Application.Suspending Event](/windows/winui/api/microsoft.ui.xaml.application.suspending) for more details. 

- The [UISettings.ColorValuesChanged Event](/uwp/api/windows.ui.viewmanagement.uisettings.colorvalueschanged) and [AccessibilitySettings.HighContrastChanged Event](/uwp/api/windows.ui.viewmanagement.accessibilitysettings.highcontrastchanged) are no longer supported in desktop apps. This may cause issues if you are using it to detect changes in Windows themes. 

- Previously, to get a CompositionCapabilities instance you would call [CompositionCapabilites.GetForCurrentView()](/uwp/api/windows.ui.composition.compositioncapabilities.getforcurrentview). However, the capabilities returned from this call were *not* dependent on the view. To address and reflect this, we've deleted the GetForCurrentView() static in this release, so now you can create a [CompositionCapabilties](/uwp/api/windows.ui.composition.compositioncapabilities) object directly.

- CoreWindow, ApplicationView, CoreApplicationView, CoreDispatcher and their dependencies are not supported in desktop apps (see below)

### CoreWindow, ApplicationView, CoreApplicationView, and CoreDispatcher in desktop apps

New in WinUI 3 Preview 4 and standard going forward, [CoreWindow](/uwp/api/Windows.UI.Core.CoreWindow), [ApplicationView](/uwp/api/Windows.UI.ViewManagement.ApplicationView), [CoreApplicationView](/uwp/api/Windows.ApplicationModel.Core.CoreApplicationView) [CoreDispatcher](/uwp/api/Windows.UI.Core.CoreDispatcher), and their dependencies are not available in desktop apps. For example, the [Window.Dispatcher](/uwp/api/Windows.UI.Xaml.Window.Dispatcher) property is always **null**, but the **Window.DispatcherQueue** property can be used as an alternative.

These APIs only work in UWP apps. In past previews they've partially worked in desktop apps as well, but since Preview 4 they've been fully disabled. These APIs are designed for the UWP case where there is only one window per thread, and one of the features of WinUI 3 is to enable multiple in the future.

There are APIs that internally depend on existence of these APIs, which consequently aren't supported in a desktop app. These APIs generally have a static `GetForCurrentView` method. For example [UIViewSettings.GetForCurrentView](/uwp/api/Windows.UI.ViewManagement.UIViewSettings.GetForCurrentView).

For more information on affected APIs as well as workarounds and replacements for these APIs, please see [Windows Runtime APIs not supported in desktop apps](../../../desktop/modernize/desktop-to-uwp-supported-api.md).

### Known issues

- You may recieve a build error due to mismatched versions of the .NET SDK and the winrt.runtime.dll. As a workaround, you can try the following:

  Explicitly set your .NET SDK to the correct version. To determine the correct version for your app, locate the `<TargetFramework>` tag in your project file. Using the Windows SDK build number that your app is targeting in the `<TargetFramework>` tag (such as 18362 or 19041), add the following item to your project file, then save your project: 

  ```xml
  <ItemGroup>            
      <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" RuntimeFrameworkVersion="10.0.{Target Windows SDK Build Number}.16" />
      <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" TargetingPackVersion="10.0.{Target Windows SDK Build Number}.16" />
  </ItemGroup>
  ```

  Note this workaround is required for .NET SDK 5.0.203 and earlier, but will not be required for .NET SDK 5.0.204 or 5.0.300.


- When using Visual Studio 2019 16.10 Preview 2, Live Visual Tree may cause a crash. To avoid this, update to the latest [Visual Studio 2019 16.10 Preview](https://visualstudio.microsoft.com/vs/preview/).

- Window caption buttons may be misplaced when SetTitleBar is not set or null

## WinUI 3 Controls Gallery

Check out the WinUI 3 Controls Gallery (previously called _XAML Controls Gallery - WinUI 3 version_) for a sample app that includes all controls and features that are a part of WinUI 3 - Project Reunion 0.8 Preview.

![WinUI 3 Controls Gallery app](../images/WinUI3XamlControlsGallery.png)<br/>
*Example of the WinUI 3 Controls Gallery app*

The WinUI 3 Controls Gallery app is available through the [Microsoft Store](https://www.microsoft.com/en-us/p/winui-3-controls-gallery/9p3jfpwwdzrc).

You can also download the sample by cloning the GitHub repo. To do this, clone the **winui3** branch using the following command:

> [!NOTE]
> There's also a **winui3preview** branch in this GitHub repo that provides a version of the WinUI 3 Controls Gallery that's using WinUI 3 - Project Reunion 0.8 Preview.

```
git clone --single-branch --branch winui3 https://github.com/microsoft/Xaml-Controls-Gallery.git
```

After cloning, ensure that you switch to the **winui3** branch in your local Git environment: 

```
git checkout winui3
```
