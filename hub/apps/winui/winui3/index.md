---
title: WinUI 3.0 Alpha (February 2020)
description: Overview of the WinUI 3.0 Alpha.
ms.date: 04/15/2020
ms.topic: article
---

# WinUI 3.0 Alpha (February 2020)

WinUI 3.0 is a major update to the Windows 10 UI platform planned for release in 2020.

For more information on the roadmap and benefits of WinUI 3.0, see the [Windows UI Library Roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md) on GitHub.

The WinUI 3.0 Alpha is a pre-release build of WinUI 3.0, and we welcome your feedback in the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml).

> [!NOTE]
> The WinUI 3.0 Alpha is intended for early evaluation and to gather feedback from the developer community. It should **NOT** be used for production apps.

## Try the WinUI 3.0 Alpha

To try the WinUI 3.0 Alpha, install the latest Visual Studio Preview then either start with a blank [Visual Studio project template](#visual-studio-project-templates), or clone and build the [XAML Controls Gallery (WinUI 3.0 Alpha) app](#xaml-controls-gallery-winui-30-alpha-branch).

### Set up Visual Studio

> [!IMPORTANT]
> Visual Studio 2019 Preview version 16.4 or newer is required for building apps using the WinUI 3.0 Alpha.

See [Visual Studio Preview](https://visualstudio.microsoft.com/vs/preview) to download the latest Visual Studio Preview.

You must include the following workloads when installing the Visual Studio Preview:

* .NET Desktop Development
* Universal Windows Platform development

To build C++ apps you must also include the following workloads:

* Desktop development with C++
* The *C++ (v142) Universal Windows Platform tools* optional component for the Universal Windows Platform workload

#### Additional steps for April 2018 update

If you are running the April 2018 Update (1803), you may need to manually install the Visual C redistributable for Visual Studio 2019.

> [!NOTE]
> This step is not required for newer versions of Windows 10.

https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads

### Visual Studio project templates

To access the WinUI 3.0 Alpha and project templates, fill out the following form:

**https://aka.ms/WinUI3AlphaAccess**

After completing the form, you must download a Visual Studio Extension (`.vsix`) to add the WinUI project templates to Visual Studio 2019.

For directions on how to add the `.vsix` to Visual Studio, see [Finding and Using Visual Studio Extensions](https://docs.microsoft.com/visualstudio/ide/finding-and-using-visual-studio-extensions?view=vs-2019#install-without-using-the-manage-extensions-dialog-box).

After installing the `.vsix` extension you can create a new WinUI 3.0 project by searching for "WinUI" and selecting one of the available C# or C++ templates.

![WinUI 3.0 Visual Studio Templates](images/WinUI3Templates.png)

*Example of the WinUI 3.0 Visual Studio Templates*

### Visual Studio project configuration

To use the WinUI 3.0 Alpha, the Visual Studio project **Target version** must be **18362 or higher**, and the **Min version** must be **17134 or higher** (right click on the Visual Studio project and select Properties to change these values).

#### VCLibs deployment error

If your app fails to deploy with an error similar to the following:

```
DEP0700: Registration of the app failed. [0x80073CF3] Windows cannot install package <app package name> because this package depends on a framework that could not be found. Provide the framework "Microsoft.VCLibs.140.00.UWPDesktop" published by "CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US", with neutral or x86 processor architecture and minimum version 14.0.24217.0, along with this package to install. The frameworks with name "Microsoft.VCLibs.140.00.UWPDesktop" currently installed are: {}
```

Then perform the following steps:

1. In the `Packages.appxmanifest` file, delete this `PackageDependency` declaration:

    ```
    <PackageDependency Name="Microsoft.VCLibs.140.00.UWPDesktop" MinVersion="14.0.24217.0" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" />
    ```

2. In the project file (`.csproj` or `.vcxproj`), add this `ItemGroup` declaration to the end of the `<Project>` section:

    ```
    <ItemGroup>
        <SDKReference Include="Microsoft.VCLibs.Desktop, Version=14.0">
            <Name>Visual C++ 2015 UWP Desktop Runtime for native apps</Name>
        </SDKReference>
    </ItemGroup>
    ```

### Xaml Controls Gallery (WinUI 3.0 Alpha branch)

See the [WinUI 3.0 Alpha branch of the Xaml Controls Gallery](https://github.com/microsoft/Xaml-Controls-Gallery/tree/winui3alpha) for a sample app that includes all WinUI 3.0 Alpha controls and features.

![WinUI 3.0 Alpha Xaml Controls Gallery app](images/WinUI3XamlControlsGallery.png)

*Example of the WinUI 3.0 Alpha Xaml Controls Gallery app*

To download the sample, clone the **winui3alpha** branch using the following command:

> `git clone --single-branch --branch winui3alpha https://github.com/microsoft/Xaml-Controls-Gallery.git`

After cloning, ensure that you switch to the **winui3alpha** branch in your local Git environment:

> `git checkout winui3alpha`

## Build apps

Aside from the limitations and known issues outlined below, building an app using the WinUI 3.0 Alpha is very similar to building a UWP app with Xaml and WinUI 2.x, so most of the guidance and documentation for UWP apps and `Windows.UI` APIs is applicable.

For more information on how to create a UWP app, see [Start coding](https://docs.microsoft.com/windows/uwp/get-started/create-uwp-apps).

The main difference is that WinUI 3 APIs are in the `Microsoft.UI` namespace instead of the `Windows.UI` namespace, so you might need to update the namespaces when copying and pasting sample code. Similarly, libraries and components using UI controls from `Windows.UI.Xaml` are not compatible with WinUI 3.0 and must be updated to `Microsoft.UI.Xaml`.

## Alpha limitations and known issues

The Alpha build is incomplete, so you should expect some bugs and limitations.

The following items are some of the known issues with the WinUI 3.0 Alpha. If you find an issue that isn't listed below, please let us know by contributing to an existing issue or filing a new issue on the [WinUI GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues).

### Project types

The WinUI 3.0 Alpha project templates for Visual Studio only support building C# and C++/WinRT components and apps using the UWP app model.

**At this time, C# 8 and .NET Core are not supported**. Future updates are planned that include support for other project types and languages, including .NET Core, C# 8, and the desktop (win32) app model.

### Platform and OS support

The WinUI 3.0 Alpha is compatible with PCs running the Windows 10 April 2018 Update (1803) and newer.

Future updates will include support for a wider range of Windows versions and devices.

### Controls and features

The latest (February 2020) update to the WinUI 3.0 Alpha adds a Chromium-based Microsoft Edge [WebView2](https://github.com/microsoft/microsoft-ui-xaml/issues/1658) control. This release of the WebView2 control has the following known issues:

> [!IMPORTANT]
> You must add a reference to the [Microsoft Edge WebView2 NuGet package](https://www.nuget.org/packages/Microsoft.Web.WebView2) to your app. [Version 0.8.355](https://www.nuget.org/packages/Microsoft.Web.WebView2/) is the latest NuGet package known to work.

* A limited API/feature set.
  - See [API draft](https://github.com/microsoft/microsoft-ui-xaml-specs/blob/master/active/WebView2/WebView2_spec.md) for more details.
* Current Beta version of Microsoft Edge is required.
  - Future updates to Microsoft Edge might cause the WebView2 control to stop working.
* Incomplete accessibility support.
* Z-order issue.
  - Alpha WebView2 is always rendered on top of other content.
* Limited support for Keyboard input.
  - Basic keyboard input works as expected. However, other text input scenarios are incomplete.
* WebViewBrush is not yet functional with WebView2.
* Touch input does not work correctly when using DPI scaling.
  - DPI should be set to 100%.
* IntelliSense still shows for the deprecated WebView control which is no longer included. For correct IntelliSense support for WebView2 you must use an xmlns namespace, such as:

```xaml
<Page
...
    xmlns:controls="using:Microsoft.UI.Xaml.Controls">

    <Grid>
        <controls:WebView2 UriSource="http://www.bing.com" Width="800" Height="600"/>
    </Grid>
</Page>
```

The following list shows the UI controls and features that are not included (or not fully functional) in the current Alpha build.

* [AcrylicBrush](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.acrylicbrush)
* [AppWindow](https://docs.microsoft.com/windows/uwp/design/layout/app-window) functionality including [ElementCompositionPreview.GetAppWindowContent](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview.getappwindowcontent) and [ElementCompositionPreview.SetAppWindowContent](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview.setappwindowcontent) 
* [ContentLink](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents.contentlink)
* [HandWritingView](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.handwritingview)
* [InkCanvas](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.inkcanvas), [InkToolbar](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.InkToolbar) and related APIs 
* [MapControl](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.Maps.MapControl)
* [MediaPlayerElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediaplayerelement), [MediaElement](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.MediaElement) and related APIs
* [RevealBrush](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealbrush)
* [SwapChainPanel](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel) and [SwapChainBackgroundPanel](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.swapchainbackgroundpanel)
* [System.ComponentModel.INotifyPropertyChanged](https://docs.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanged?view=dotnet-uwp-10.0) and [System.Collections.Specialized.INotifyCollectionChanged](https://docs.microsoft.com/dotnet/api/system.collections.specialized.inotifycollectionchanged?view=dotnet-uwp-10.0) for C# apps. Use the `Microsoft.UI.Xaml.Interop` versions of these interfaces instead.
* [ObservableCollection\<T\>](https://docs.microsoft.com/dotnet/api/system.collections.objectmodel.observablecollection-1?view=netframework-4.8) implements [System.ComponentModel.INotifyPropertyChanged](https://docs.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanged?view=dotnet-uwp-10.0) and [System.Collections.Specialized.INotifyCollectionChanged](https://docs.microsoft.com/dotnet/api/system.collections.specialized.inotifycollectionchanged?view=dotnet-uwp-10.0). See the Xaml Controls Gallery app for an example of an [alternative observable collection implementation](https://github.com/microsoft/Xaml-Controls-Gallery/blob/winui3alpha/XamlControlsGallery/CollectionsInterop.cs).
* [ThemeShadow](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.themeshadow)
* [Window.SetTitleBar](https://docs.microsoft.com/uwp/api/windows.ui.xaml.window.settitlebar) and custom window title bars
* [Xaml Islands](https://docs.microsoft.com/windows/apps/desktop/modernize/xaml-islands) functionality including [DesktopWindowXamlSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource)
* [XamlLight](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.xamllight)

### INotifyPropertyChanged and INotifyCollectionChanged

The .NET interfaces [System.ComponentModel.INotifyPropertyChanged](https://docs.microsoft.com/dotnet/api/system.componentmodel.inotifypropertychanged?view=dotnet-uwp-10.0) and [System.Collections.Specialized.INotifyCollectionChanged](https://docs.microsoft.com/dotnet/api/system.collections.specialized.inotifycollectionchanged?view=dotnet-uwp-10.0) are projected into the WinRT Windows.UI.Xaml namespace as [Windows.UI.Xaml.Data.INotifyPropertyChanged](https://docs.microsoft.com/uwp/api/windows.ui.xaml.data.inotifypropertychanged) and [Windows.UI.Xaml.Interop.INotifyCollectionChanged](https://docs.microsoft.com/uwp/api/windows.ui.xaml.interop.inotifycollectionchanged), respectively. However, WinUI 3.0 depends on interfaces in the Microsoft.UI.Xaml namespace, so types that implement these .NET interfaces do not function well with WinUI 3.0. To work around this issue, your type should implement the corresponding Microsoft.UI.Xaml interfaces.

As a result of this issue, [ObservableCollection\<T\>](https://docs.microsoft.com/dotnet/api/system.collections.objectmodel.observablecollection-1) does not work well in conjunction with WinUI 3.0. When used as an [ItemsSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.itemscontrol.itemssource#Windows_UI_Xaml_Controls_ItemsControl_ItemsSource), changes to the collection can result in the following exception:

`System.Runtime.InteropServices.COMException: 'The application called an interface that was marshalled for a different thread. (Exception from HRESULT: 0x8001010E (RPC_E_WRONG_THREAD))'`

For an example of how to work around this issue, see the Xaml Controls Gallery, which uses a [custom implementation of a collection that implements INotifyCollectionChanged](https://github.com/microsoft/Xaml-Controls-Gallery/blob/winui3alpha/XamlControlsGallery/CollectionsInterop.cs) instead of ObservableCollection\<T\>.

### Developer tools

* The Design view for Xaml files in Visual Studio is currently disabled for WinUI 3.0 apps.
* Live Visual Tree and other debugging tools may not function correctly for WinUI 3.0 apps.
* When Visual Studio automatically generates a code-behind event handler from Xaml markup (for example, events such as `Button.Click`), the namespace of the generated event args incorrectly start with `Windows.UI.Xaml` instead of `Microsoft.UI.Xaml`. You can fix this by manually changing the namespaces from `Windows.UI.Xaml` to  `Microsoft.UI.Xaml`.
* For Intellisense to work with named elements in code-behind, you must build your WinUI 3.0 project (the g.cs and g.i.cs files are not generated on the fly).

### Interactivity

* Multi-touch interactions on a touchscreen or precision touchpad can make scrolling and touch responsiveness unpredictable within the app and should be avoided at this time.
