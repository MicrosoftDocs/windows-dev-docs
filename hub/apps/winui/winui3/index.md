---
title: WinUI 3 Preview 4 (February 2021)
description: Overview of the WinUI 3 Preview 4 release.
ms.date: 02/09/2021
ms.topic: article
---

# Windows UI Library 3 Preview 4 (February 2021)

Windows UI Library (WinUI) 3 is a native user experience (UX) framework for both Windows Desktop and UWP apps.

**WinUI 3 Preview 4** is a stability preview release that includes critical bug fixes and other general improvements (see **[Capabilities introduced in Preview 4](#capabilities-introduced-in-preview-4)**).

> [!Important]
> This WinUI 3 preview release is intended for early evaluation and to gather feedback from the developer community. It should **NOT** be used for production apps.
>
> We will continue shipping preview releases of WinUI 3 into 2021, followed by the first official, supported release in March 2021.
>
> Please use the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml) to provide feedback and log suggestions and issues.

## Install WinUI 3 Preview 4

WinUI 3 Preview 4 includes Visual Studio project templates to help get started building apps with a WinUI-based user interface, and a NuGet package that contains the WinUI libraries. To install WinUI 3 Preview 4, follow these steps.

> [!NOTE]
> You can also clone and build the WinUI 3 Preview 4 version of the [XAML Controls Gallery](#xaml-controls-gallery-winui-3-preview-4-branch).

1. Ensure that your development computer has Windows 10, version 1803 (build 17134), or newer installed.

2. Install [Visual Studio 2019 version 16.9 Preview](https://visualstudio.microsoft.com/vs/preview/). Download the **latest preview** to ensure you get all of the necessary updates to your workloads (such as .NET 5).

    You must include the following workload when installing Visual Studio:
    - Universal Windows Platform development

    To build .NET apps, you must also include the following workloads:
    - .NET Desktop Development (this also installs the latest version of .NET 5, which you'll need)

    To build C++ apps, you must also include the following workloads:
    - Desktop development with C++
    - The *C++ (v142) Universal Windows Platform tools* optional component for the Universal Windows Platform workload (see "Installation Details" under the "Universal Windows Platform development" section, on the right pane)

3. Make sure your system has a NuGet package source enabled for **nuget.org**. For more information, see [Common NuGet configurations](/nuget/consume-packages/configuring-nuget-behavior).

4. Download and install the [WinUI 3 Preview 4 VSIX package](https://aka.ms/winui3/preview4-download). This adds both the WinUI 3 project templates and the NuGet package containing the WinUI 3 libraries to Visual Studio 2019.

    For instructions on how to add the VSIX package to Visual Studio, see [Finding and Using Visual Studio Extensions](/visualstudio/ide/finding-and-using-visual-studio-extensions#install-without-using-the-manage-extensions-dialog-box).

5. To use WinUI 3 tooling such as Live Visual Tree, Hot Reload, and Live Property Explorer, you must enable WinUI 3 tooling with Visual Studio Preview Features as described in the [instructions here](https://github.com/microsoft/microsoft-ui-xaml/issues/4140).

#### WebView2
To use WebView2 with WinUI 3 Preview 4, please download the Evergreen Bootstrapper or Evergreen Standalone Installer found on [this page](https://developer.microsoft.com/microsoft-edge/webview2/). 

#### Windows Community Toolkit

If you're using the Windows Community Toolkit, [download the latest version](https://aka.ms/wct-winui3).

## Create WinUI projects

After installing the WinUI 3 Preview 4 VSIX package, you're ready to create a new project using one of the WinUI project templates in Visual Studio. To access the WinUI project templates in the **Create a new project** dialog, filter the language to **C++** or **C#**, the platform to **Windows**, and the project type to **WinUI**. Alternatively, you can search for *WinUI* and select one of the available C# or C++ templates.

![WinUI project templates](images/winui-projects-csharp.png)

For more information about getting started with the WinUI project templates, see the following articles:

- [Get started with WinUI 3 for desktop apps](get-started-winui3-for-desktop.md)
- [Get started with WinUI 3 for UWP apps](get-started-winui3-for-uwp.md)

Aside from the [limitations and known issues](#limitations-and-known-issues), building an app using the WinUI projects is  similar to building a UWP app with XAML and WinUI 2.x. Therefore, most of the [guidance documentation](/windows/uwp/design/) for UWP apps and the **Windows.UI** WinRT namespaces in the Windows SDK is applicable.

API reference documentation for this release is coming soon. The link will be provided here when it is available. In the meantime, feel free to look at the [WinUI 3 API reference documentation for Preview 3](/windows/winui/api/).

If you created a project using WinUI 3 Preview 3, you can upgrade your project to use Preview 4. See the [WinUI GitHub repository](https://aka.ms/winui3/upgrade-instructions) for detailed instructions.

### Project templates for WinUI 3

You can use these WinUI project templates to create apps.

| Template | Language | Description |
|----------|----------|-------------|
| Blank App, Packaged (WinUI in Desktop) | C# and C++ | Creates a desktop .NET 5 (C#) or native Win32 (C++) app with a WinUI-based user interface. The generated project includes a basic window that derives from the **Microsoft.UI.Xaml.Window** class in the WinUI library that you can use to start building your UI. For more information about this project type, see [Get started with WinUI 3 for desktop apps](get-started-winui3-for-desktop.md).<p></p>The solution also includes a [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that is configured to build the app into an [MSIX package](/windows/msix/overview). This provides a modern deployment experience, the ability to integrate with Windows 10 features via package extensions, and much more.  |
| Blank App (WinUI in UWP)  | C# and C++ | Creates a UWP app that with a WinUI-based user interface. The generated project includes a basic page that derives from the **Microsoft.UI.Xaml.Controls.Page** class in the WinUI library that you can use to start building your UI. For more information about this project type, see [Get started with WinUI 3 for UWP apps](get-started-winui3-for-uwp.md). |

You can use these WinUI project templates to build components that can be loaded and used by a WinUI-based app.

| Template | Language | Description |
|----------|----------|-------------|
| Class Library (WinUI in Desktop) | C# only | Creates a .NET 5 managed class library (DLL) in C# that can be used by other .NET 5 desktop apps with a WinUI-based user interface.  |
| Class Library (WinUI in UWP)  | C# only | Creates a managed class library (DLL) in C# that can be used by other UWP apps with a WinUI-based user interface. |
| Windows Runtime Component (WinUI) | C++ | Creates a [Windows Runtime component](/windows/uwp/winrt-components/) written in C++/WinRT that can be consumed by any UWP or Desktop app with a WinUI-based user interface, regardless of the programming language in which the app is written. |
| Windows Runtime Component (UWP) | C# | Creates a [Windows Runtime component](/windows/uwp/winrt-components/) written in C# that can be consumed by any UWP app with a WinUI-based user interface, regardless of the programming language in which the app is written. |

### Item templates for WinUI 3

The following item templates are available for use in a WinUI project. To access these WinUI item templates, right-click the project node in **Solution Explorer**, select **Add** -> **New item**, and click **WinUI** in the **Add New Item** dialog.

![WinUI item templates](images/winui-items-csharp.png)

| Template | Language | Description |
|----------|----------|-------------|
| Blank Page (WinUI) | C# and C++ | Adds a XAML file and code file that defines a new page derived from the **Microsoft.UI.Xaml.Controls.Page** class in the WinUI library. |
| Blank Window (WinUI in Desktop) | C# and C++ | Adds a XAML file and code file that defines a new window derived from the **Microsoft.UI.Xaml.Window** class in the WinUI library. |
| Custom Control (WinUI) | C# and C++ | Adds a code file for creating a templated control with a default style. The templated control is derived from the **Microsoft.UI.Xaml.Controls.Control** class in the WinUI library.<p></p>For a walkthrough that demonstrates how to use this item template, see [Templated XAML controls for UWP and WinUI 3 apps with C++/WinRT](xaml-templated-controls-cppwinrt-winui-3.md) and [Templated XAML controls for UWP and WinUI 3 apps with C#](xaml-templated-controls-csharp-winui-3.md). For more information about templated controls, see [Custom XAML Controls](/archive/msdn-magazine/2019/may/xaml-custom-xaml-controls). |
| Resource Dictionary (WinUI) | C# and C++ | Adds an empty, keyed collection of XAML resources. For more information, see [ResourceDictionary and XAML resource references](/windows/uwp/design/controls-and-patterns/resourcedictionary-and-xaml-resource-references). |
| Resources File (WinUI) | C# and C++ | Adds a file for storing string and conditional resources for your app. You can use this item to help localize your app. For more info, see [Localize strings in your UI and app package manifest](/windows/uwp/app-resources/localize-strings-ui-manifest). |
| User Control (WinUI) | C# and C++ | Adds a XAML file and code file for creating a user control that derives from the **Microsoft.UI.Xaml.Controls.UserControl** class in the WinUI library. Typically, a user control encapsulates related existing controls and provide its own logic.<p></p>For more information about user controls, see [Custom XAML Controls](/archive/msdn-magazine/2019/may/xaml-custom-xaml-controls). |

### Visual Studio Support

In order to take advantage of the latest tooling features added into WinUI 3 Preview 4 like Hot Reload, Live Visual Tree, and Live Property Explorer, you must use the latest preview version of Visual Studio with the latest WinUI 3 preview and be sure to enable WinUI tooling in Visual Studio Preview Features, as described in the [instructions here](https://github.com/microsoft/microsoft-ui-xaml/issues/4140). The table below shows the compatibility of future versions with WinUI 3 Preview 4:

| VS Version  | WinUI 3 Preview 4  |
|---|---|
| 16.8 RTM  | No   |
| 16.9 Previews  | Yes  | 
| 16.9 RTM  | No   |
| 16.10 Previews  | Yes   |

## Capabilities introduced in Preview 4

- Parity with WinUI 2.5 (includes InfoBar control, new features in ProgressRing and NavigationView, and bug fixes)
- Custom titlebar capabilities: new Window.ExtendsContentIntoTitleBar and Window.SetTitleBar APIs that allow developers to create custom title bars in Desktop apps.
- VirtualSurfaceImageSource  support

## List of bugs fixed in Preview 4

Below is a list of user-facing bugs that the team has fixed since Preview 3. There has also been a lot of work going on surrounding stabilization and improving our testing.

- This release has taken on a new version of CS/WinRT and the Windows SDK, which fixed the following bugs:
  - Crash when binding to a URI property using {Binding}
  - C#/WinRT Marshal functions not interoperating correctly with .NET 5

- WinUI 3 crash when running on Windows Insider Builds
  - Thanks to multiple community contributors for reporting this bug on GitHub! 
- WebView2 doesn't apply host app's Language/locale to CoreWebView2Environment
- Windows Community Toolkit DataGrid control crashes app on start/when scrollbars appear
  - Thanks to multiple community contributors for reporting this bug on GitHub!
- Page rendering gets into a bad state when display mode changes
- Crash when using Language ComboBox in CalendarView
- WinUI 3 Desktop: Can't tab out of WebView2
- WinUI 3 Desktop: TreeView with derived TreeViewNodes crashes 
  - Thanks to @eleanorleffler for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/2699)!
- WinUI 3 Desktop: Unable to Enter Text into TextBox inside ContentDialog 
  - Thanks to @eleanorleffler for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/2704)!
- WinUI 3 Desktop: ALT and F6 don't work
- Old removed SwapChainPanel renders on top of new SwapChain
  - Thanks to @dotMorten for filing this [issue on Github](https://github.com/microsoft/microsoft-ui-xaml/issues/2942)!
- WinUI 3 Desktop: Cannot scroll with trackpad
- Crash when using NavigationView control with multiple windows on the same thread
- Accessibility Issue: Show focus rect on WinUI desktop app launch
- Access violation while scrolling in DataGrid
  - Thanks to @TroelsL for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/2946)!
- WinUI 3 Desktop: Tab cycling does not work 
- Drag and Drop on GridView fails in desktop application with WinUI Xaml Islands
  - Thanks to @smk2007 for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3871)!
- Accessibility issue: Unable to scroll with PageUp/PageDown keys on WinUI 3 Desktop
- WebView2 has wrong viewport size
- WebView2 crash on click after opening MenuFlyout
  - Thanks to @sudongg for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3729)!
- WinUI 3 Desktop: Attempting to bring down DropDownButton or SplitButton's flyout causes app crash
- WebView2: Double right click on mouse causes a crash
- Clicking on a ToggleSplitButton causes the application to crash
  - Thanks to @lhak for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3641)!
- WinUI 3 Desktop: Empty DesktopWindowXamlSource window visible on task bar
  - Thanks to @bridgesquared for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3698)!
- WinUI 3 Desktop: DataGrid not displaying
  - Thanks to @eleanorleffler for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/2703)!
- WinUI 3 Desktop: Unable to drop files onto Grid 
  - Thanks to @eleanorleffler for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/2715)!
- WinUI 3 Desktop: ItemsRepeater crash in WinUI 3 Preview 2 
  - Thanks to @hshristov for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3007)!
- AccessViolationException thrown when updating bindings
  - Thanks to @WamWooWam for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3680)!
- WinUI 3 Desktop: app crashes on scroll NavigationView
  - Thanks to @Berkunath for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3598)!
- ItemsControl does not get updated while dynamically adding or removing items in its ItemsSource collection. 
  - Thanks to @VigneshRameshh for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3517)!
- Compile error C2760 in App.xaml.g.h if C++ Conformance Mode enabled 
  - Thanks to @boostafazoo for filing this [issue on GitHub](https://github.com/microsoft/microsoft-ui-xaml/issues/3716)!


## New features and capabilities introduced in past WinUI 3 Previews

The following features and capabilities were introduced in WinUI 3 Preview 1-3 and continue to be supported in WinUI 3 Preview 4.

- Ability to create Desktop apps with WinUI, including [.NET 5](https://github.com/dotnet/core/tree/master/release-notes/5.0) for Win32 apps
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

For more information on both the benefits of WinUI 3 and the WinUI roadmap, see the [Windows UI Library Roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md) on GitHub.

### Provide feedback and suggestions

We welcome your feedback in the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose).

### What's coming next?

Take a look at our detailed [feature roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md#winui-30-feature-roadmap) to see when specific features will be brought into WinUI 3. 

## Limitations and known issues

The Preview 4 release is just that, a preview. The scenarios around Desktop apps are especially new. Please expect bugs, limitations, and other issues.

The following items are some of the known issues with WinUI 3 Preview 4. If you find an issue that isn't listed below, please let us know by contributing to an existing issue or filing a new issue through the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose).

### Platform and OS support

WinUI 3 Preview 4 is compatible with PCs running the Windows 10 April 2018 Update (version 1803 - build 17134) and newer.

### Developer tools

- Only C# and C++/WinRT apps are supported
- Desktop apps support .NET 5 and C# 9, and must be packaged in an MSIX app
- UWP apps support .NET Native and C# 7.3
- Developer tools and Intellisense may not work properly in Visual Studio version 16.8.
- No XAML Designer support
- New C++/CX apps are not supported, however, your existing apps will continue to function (please move to C++/WinRT as soon as possible)
- Support for multiple windows in Desktop apps is in progress, but not yet complete and stable.
  - Please file a bug on our repo if you find new issues or regressions with multi-window behavior.
- Unpackaged desktop deployment is not supported
- When running a Desktop app using F5, make sure that you are running the packaging project. Hitting F5 on the app project will run an unpackaged app, which WinUI 3 does not yet support.

### Missing Platform Features

- Xbox support
- HoloLens support
- Windowed popups
  - More specifically, the `ShouldConstrainToRootBounds` property always acts as if it's set to `true`, regardless of the property value.
- Inking support
- Acrylic
- MediaElement and MediaPlayerElement
- MapControl
- RenderTargetBitmap for SwapChainPanel and non-XAML content
- SwapChainPanel does not support transparency
- Global Reveal uses fallback behavior, a solid brush
- XAML Islands is not supported in this release
- 3rd party ecosystem libraries will not fully function
- IMEs do not work
- CoreWindow, ApplicationView, CoreApplicationView, CoreDispatcher and their dependencies are not supported in Desktop apps (see below)

#### CoreWindow, ApplicationView, CoreApplicationView, and CoreDispatcher in Desktop apps

New in Preview4,
[CoreWindow](https://docs.microsoft.com/uwp/api/Windows.UI.Core.CoreWindow),
[ApplicationView](https://docs.microsoft.com/uwp/api/Windows.UI.ViewManagement.ApplicationView),
[CoreApplicationView](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Core.CoreApplicationView)
[CoreDispatcher](https://docs.microsoft.com/uwp/api/Windows.UI.Core.CoreDispatcher),
and their dependencies are not available in Desktop apps.

For example the
[Window.Dispatcher](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Window.Dispatcher)
property is always null, but the Window.DispatcherQueue property can be used as an alternative.

These APIs only work in UWP apps.
In past previews they've partially worked in Desktop apps as well, but in Preview4 they've been fully disabled.
These APIs are designed for the UWP case where there is only one window per thread,
and one of the features of WinUI3 is to enable multiple.

There are APIs that internally depend on existance of these APIs, which consequently aren't supported
in a Desktop app. These APIs generally have a static `GetForCurrentView` method. For example
[UIViewSettings.GetForCurrentView](https://docs.microsoft.com/uwp/api/Windows.UI.ViewManagement.UIViewSettings.GetForCurrentView).


### Known issues

- Alt+F4 does not close Desktop app windows.

- Due to changes with [CoreWindow](https://docs.microsoft.com/uwp/api/windows.ui.core.corewindow), the following WinRT APIs may no longer work with **Desktop** apps as expected:
  - [`ApplicationView`](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.applicationview) and all related APIs will no longer work.
  - [`CoreApplicationView`](https://docs.microsoft.com/uwp/api/windows.applicationmodel.core.coreapplicationview) and all related APIs will no longer work.
  - All `GetForCurrentView` APIs may not be supported, for example [`CoreInputView.GetForCurrentView`](https://docs.microsoft.com/uwp/api/Windows.UI.ViewManagement.Core.CoreInputView.GetForCurrentView).
  - [`CoreWindow.GetForCurrentThread`](https://docs.microsoft.com/uwp/api/Windows.UI.Core.CoreWindow.GetForCurrentThread) will now return null.

  For more information on using WinRT APIs in your WinUI 3 Desktop app, see [Windows Runtime APIs available to desktop apps](https://docs.microsoft.com/windows/apps/desktop/modernize/desktop-to-uwp-supported-api
).

- The [UISettings.ColorValuesChanged Event](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.uisettings.colorvalueschanged) and [AccessibilitySettings.HighContrastChanged Event](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.accessibilitysettings.highcontrastchanged) are no longer supported in Desktop apps. This may cause issues if you are using it to detect changes in Windows themes. 

- This release includes some experimental APIs. These have not been thoroughly tested by the team and may have unknown issues. Please [file a bug](https://github.com/microsoft/microsoft-ui-xaml/issues/new?assignees=&labels=&template=bug_report.md&title=) on our repo if you encounter any issues. 

- Previously, to get a CompositionCapabilities instance you would call [CompositionCapabilites.GetForCurrentView()](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositioncapabilities.getforcurrentview). However, the capabilities returned from this call were *not* dependent on the view. To address and reflect this, we've deleted the GetForCurrentView() static in this release, so now you can create a [CompositionCapabilties](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositioncapabilities) object directly.

- For C# UWP apps:

  The WinUI 3 framework is a set of WinRT components which can be used from C++ (using C++/WinRT) or C#. When using C#, there are two versions of .NET, depending on the app model: when using WinUI 3 in a UWP app you’re using .NET Native; when using in a Desktop app you’re using .NET 5 (and C#/WinRT).

  When using C# for a WinUI 3 app in UWP, there are a few API namespace differences compared to C# in a WinUI 3 Desktop app or a C# WinUI 2 app: some types are in a `Microsoft` namespace rather than a `System` namespace. For example, rather than the `INotifyPropertyChanged` interface being in the `System.ComponentModel`  namespace, it’s in the `Microsoft.UI.Xaml.Data` namespace. 

  This applies to:
    - `INotifyPropertyChanged` (and related types)
    - `INotifyCollectionChanged`
    - `ICommand`

  The `System` namespace versions still exist, but cannot be used with WinUI 3. This means that `ObservableCollection` doesn't work as-is in WinUI 3 C# UWP apps. For a workaround, see the [CollectionsInterop sample](https://github.com/microsoft/Xaml-Controls-Gallery/blob/winui3preview/XamlControlsGallery/CollectionsInterop.cs) in the [XAML Controls Gallery sample](https://github.com/microsoft/Xaml-Controls-Gallery/tree/winui3preview).

## XAML Controls Gallery (WinUI 3 Preview 4 branch)

See the [WinUI 3 Preview 4 branch of the XAML Controls Gallery](https://github.com/microsoft/Xaml-Controls-Gallery/tree/winui3preview) for a sample app that includes all WinUI 3 Preview 4 controls and features.

![WinUI 3 Preview 4 XAML Controls Gallery app](images/WinUI3XamlControlsGallery.PNG)<br/>
*Example of the WinUI 3 Preview 4 XAML Controls Gallery app*

To download the sample, clone the **winui3preview** branch using the following command:

```
git clone --single-branch --branch winui3preview https://github.com/microsoft/Xaml-Controls-Gallery.git
```

After cloning, ensure that you switch to the **winui3preview** branch in your local Git environment: 

```
git checkout winui3preview
```
