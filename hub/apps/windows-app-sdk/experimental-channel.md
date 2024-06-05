---
title: Latest experimental channel release notes for the Windows App SDK
description: Learn about the latest experimental releases of the Windows App SDK.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, project reunion, experimental, windows app sdk
ms.localizationpriority: medium
---

# Latest experimental channel release notes for the Windows App SDK

> [!IMPORTANT]
> The experimental channel is **not supported** for use in production environments, and apps that use the experimental releases cannot be published to the Microsoft Store.

The experimental channel includes releases of the Windows App SDK with [experimental channel features](release-channels.md#features-available-by-release-channel) in early stages of development. APIs for experimental features have the [Experimental](/uwp/api/Windows.Foundation.Metadata.ExperimentalAttribute) attribute. If you call an experimental API in your code, you will receive a build-time warning. All APIs in the experimental channel are subject to extensive revisions and breaking changes. Experimental features and APIs may be removed from subsequent releases at any time.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).
- For documentation on experimental releases, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md).

**Experimental channel release note archive:**

- [Experimental channel release notes for the Windows App SDK 1.5](release-notes-archive/experimental-channel-1.5.md)
- [Experimental channel release notes for the Windows App SDK 1.4](release-notes-archive/experimental-channel-1.4.md)
- [Experimental channel release notes for the Windows App SDK 1.3](release-notes-archive/experimental-channel-1.3.md)
- [Experimental channel release notes for the Windows App SDK 1.2](release-notes-archive/experimental-channel-1.2.md)
- [Experimental channel release notes for the Windows App SDK 1.0](release-notes-archive/experimental-channel-1.0.md)
- [Experimental channel release notes for the Windows App SDK 0.8](release-notes-archive/experimental-channel-0.8.md)

## Version 1.6 Experimental (1.6.0-experimental1)

This is the latest release of the experimental channel.

To download, retarget your WinAppSDK NuGet version to `1.6.240531000-experimental1`.

### Required C# project changes for 1.6-experimental1

In 1.6-experimental1, Windows App SDK managed apps require [Microsoft.Windows.SDK.NET.Ref](https://www.nuget.org/packages/Microsoft.Windows.SDK.NET.Ref) `*.*.*.35-preview` (or later), which can be specified via [WindowsSdkPackageVersion](/dotnet/core/compatibility/sdk/5.0/override-windows-sdk-package-version) in your `csproj` file. For example:

```XML
<Project Sdk="Microsoft.NET.Sdk">
   <PropertyGroup>
       <OutputType>WinExe</OutputType>
       <TargetFramework>net8.0-windows10.0.22621.0</TargetFramework>
       <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
       <WindowsSdkPackageVersion>10.0.22621.35-preview</WindowsSdkPackageVersion>
   <PropertyGroup>
   ...
```

In addition, Windows App SDK managed apps using C#/WinRT should update to [Microsoft.Windows.CsWinRT](https://www.nuget.org/packages/Microsoft.Windows.CsWinRT) `2.1.0-prerelease.240602.1` (or later).

### Native AOT support

The .NET `PublishAOT` project property is now supported for native Ahead-Of-Time compilation. For details, see [Native AOT Deployment](/dotnet/core/deploying/native-aot/). Because AOT builds on Trimming support, much of the following trimming-related guidance applies to AOT as well.

For `PublishAOT` support, in addition to the C# project changes described in the previous section you'll also need a package reference to [Microsoft.Windows.CsWinRT](https://www.nuget.org/packages/Microsoft.Windows.CsWinRT) `2.1.0-prerelease.240602.1` (or later) to enable the source generator from that package. 

To enable the `PublishAOT` MSBuild targets, the NuGet restore operation must explicitly set the `PublishAOT` property to **true**. For example:

`dotnet restore -p:publishAot=true ...`

or 

`msbuild /t:restore /p:publishAot=true ...`

Or you can add this to your `csproj` file:

```XML
<PublishAot Condition="'$(ExcludeRestorePackageImports)'=='true'">true</PublishAot>
```

#### Resolving AOT Issues

In this release, the developer is responsible for ensuring that all types are properly rooted to avoid trimming (such as with reflection-based `{Binding}` targets). Later releases will enhance both C#/WinRT and the XAML Compiler to automate rooting where possible, alert developers to trimming risks, and provide mechanisms to resolve.

##### Partial Classes

C#/WinRT also includes `PublishAOT` support in version 2.1.0-prerelease.240602.1. To enable a class for AOT publishing with C#/WinRT, it must first be marked `partial`. This allows the C#/WinRT AOT source analyzer to attribute the classes for static analysis. Only classes (which contain methods, the targets of trimming) require this attribute.

##### Reflection-Free Techniques

To enable AOT compatibility, reflection-based techniques should be replaced with statically typed serialization, AppContext.BaseDirectory, typeof(), etc. For details, see [Introduction to trim warnings](/dotnet/core/deploying/trimming/fixing-warnings).

##### Rooting Types

Until full support for `{Binding}` is implemented, types may be preserved from trimming as follows:
Given project `P` consuming assembly `A` with type `T` in namespace `N`, which is only dynamically referenced (so normally trimmed), `T` can be preserved via:

`P.csproj`:

```xml
<ItemGroup>
    <TrimmerRootDescriptor Include="ILLink.Descriptors.xml" />
</ItemGroup>
```

`ILLink.Descriptors.xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<linker>
    <assembly fullname="A">
        <type fullname="N.T" preserve="all" />
    </assembly>
</linker>
```

For complete root descriptor XML expression syntax, see [Root Descriptors](/dotnet/core/deploying/trimming/trimming-options?pivots=dotnet-8-0#root-descriptors).

> [!NOTE]
> Dependency packages that have not yet adopted AOT support may exhibit runtime issues.

### Improved TabView tab tear-out

`TabView` supports a new `CanTearOutTabs` mode which provides an enhanced experience for dragging tabs and dragging out to a new window. When this new option is enabled, tab dragging is very much like the tab drag experience in Edge and Chrome, where a new window is immediately created during the drag, allowing the user to drag it to the edge of the screen to maximize or snap the window in one smooth motion. This implementation also doesn't use drag-and-drop APIs, so it isn't impacted by any limitations in those APIs. Notably, tab tear-out is supported in processes running elevated as Administrator.

**Known issue:** In this release, pointer input behavior for `CanTearOutTabs` is incorrect on monitors with scale factor different than 100%. This will be fixed in the next 1.6 release.

### New TitleBar control

A new `TitleBar` control makes it easy to create a great, customizable titlebar for your app with the following features:

- Configurable Icon, Title, and Subtitle properties
- An integrated back button
- The ability to add a custom control like a search box
- Automatic hiding and showing of elements based on window width
- Affordances for showing active or deactive window state
- Support for default titlebar features including draggable regions in empty areas, theme responsiveness, default caption (min/max/close) buttons, and built-in accessibility support

The `TitleBar` control is designed to support various combinations of titlebars, making it flexible to create the experience you want without having to write a lot of custom code. We took feedback from the [community toolkit titlebar prototype](https://github.com/CommunityToolkit/Labs-Windows/discussions/454) and look forward to additional feedback!

**Known issue:** In this release, the `TitleBar` only shows the Icon and Title due to an issue where some elements don't show up on load. To work around this, use the following code to load the other elements (Subtitle, Header, Content, and Footer):

```C#
public MainWindow()
  {
      this.InitializeComponent();
      this.ExtendsContentIntoTitleBar = true;
      this.SetTitleBar(MyTitleBar);

      MyTitleBar.Loaded += MyTitleBar_Loaded;
  }

  private void MyTitleBar_Loaded(object sender, RoutedEventArgs e)
  {
      // Parts get delay loaded. If you have the parts, make them visible.
      VisualStateManager.GoToState(MyTitleBar, "SubtitleTextVisible", false);
      VisualStateManager.GoToState(MyTitleBar, "HeaderVisible", false);
      VisualStateManager.GoToState(MyTitleBar, "ContentVisible", false);
      VisualStateManager.GoToState(MyTitleBar, "FooterVisible", false);

      // Run layout so we re-calculate the drag regions.
      MyTitleBar.InvalidateMeasure();
  }
```

This issue will be fixed in the next 1.6 release.

### Other notable changes

- Unsealed `ItemsWrapGrid`. This should be a backward-compatible change.
- `PipsPager` supports a new mode where it can wrap between the first and list items.
- `RatingControl` is now more customizable, by moving some hard-coded style properties to theme resources. This allows apps to override these values to better customize the appearance of RatingControl.

### New APIs for 1.6-experimental1

1.6-experimental1 includes the following new APIs. These APIs are not experimental, but are not yet included in a stable release version of the WinAppSDK.

```C#
Microsoft.UI.Xaml.Controls

    PipsPager
        WrapMode
        WrapModeProperty

    PipsPagerWrapMode
        None
        Wrap
```

## Additional 1.6-experimental1 APIs

This release includes the following new and modified experimental APIs:

```C#
Microsoft.UI.Content

    ChildContentLink
    ContentExternalOutputLink
        IsAboveContent

    ContentIsland
        Children
        Create
        FindAllForCompositor
        GetByVisual
        Offset
        RotationAngleInDegrees

    ContentSite
        Offset
        RotationAngleInDegrees

    ContentSiteView
        Offset
        RotationAngleInDegrees

    IContentLink
    IContentSiteBridge2
    ReadOnlyDesktopSiteBridge

Microsoft.UI.Input

    EnteredMoveSizeEventArgs
    EnteringMoveSizeEventArgs
    ExitedMoveSizeEventArgs
    InputNonClientPointerSource
        EnteredMoveSize
        EnteringMoveSize
        ExitedMoveSize
        WindowRectChanged
        WindowRectChanging

    MoveSizeOperation
    WindowRectChangedEventArgs
    WindowRectChangingEventArgs

Microsoft.UI.Windowing

    AppWindow
        DefaultTitleBarShouldMatchAppModeTheme

Microsoft.UI.Xaml

    XamlRoot
        CoordinateConverter
        TryGetContentIsland

Microsoft.UI.Xaml.Controls

    ScrollingViewChangingEventArgs
    ScrollView
        ViewChanging

    StackLayout
        IsVirtualizationEnabled
        IsVirtualizationEnabledProperty

    TabView
        CanTearOutTabs
        CanTearOutTabsProperty
        ExternalTornOutTabsDropped
        ExternalTornOutTabsDropping
        TabTearOutRequested
        TabTearOutWindowRequested

    TabViewExternalTornOutTabsDroppedEventArgs
    TabViewExternalTornOutTabsDroppingEventArgs
    TabViewTabTearOutRequestedEventArgs
    TabViewTabTearOutWindowRequestedEventArgs
    TitleBar
    TitleBarAutomationPeer
    TitleBarTemplateSettings

Microsoft.UI.Xaml.Controls.Primitives

    ScrollPresenter
        ViewChanging
```

### Other known issues

- Non-XAML applications that use `Microsoft.UI.Content.ContentIslands` and do not handle the *ContentIsland.AutomationProviderRequested* event (or return *nullptr* as the automation provider) will crash if any accessibility or UI automation tool is enabled such as Voice Access, Narrator, Accessibility Insights, Inspect.exe, etc.

### Bug fixes

This release includes the following bug fixes:

- Fixed an issue where clicking in an empty area of a `ScrollViewer` would always move focus to the first focusable control in the `ScrollViewer` and scroll that control into view. For more info, see GitHub issue [#597](https://github.com/microsoft/microsoft-ui-xaml/issues/597).
- Fixed an issue where the `Window.Activated` event sometimes fired multiple times. For more info, see GitHub issue [#7343](https://github.com/microsoft/microsoft-ui-xaml/issues/7343).
- Fixed an issue setting the `NavigationViewItem.IsSelected` property to `true` prevents its children from showing when expanded. For more info, see GitHub issue [#7930](https://github.com/microsoft/microsoft-ui-xaml/issues/7930).
- Fixed an issue where `MediaPlayerElement` would not properly display captions with `None` or `DropShadow` edge effects. For more info, see GitHub issue [#7981](https://github.com/microsoft/microsoft-ui-xaml/issues/7981).
- Fixed an issue where the `Flyout.ShowMode` property was not used when showing the flyout. For more info, see GitHub issue [#7987](https://github.com/microsoft/microsoft-ui-xaml/issues/7987).
- Fixed an issue where `NumberBox` would sometimes have rounding errors. For more info, see GitHub issue [#8780](https://github.com/microsoft/microsoft-ui-xaml/issues/8780).
- Fixed an issue where using a library compiled against an older version of WinAppSDK can hit a trying to find a type or property. 
For more info, see GitHub issue [#8810](https://github.com/microsoft/microsoft-ui-xaml/issues/8810).
- Fixed an issue where initial keyboard focus is not set when launching a window. For more info, see GitHub issue [#8816](https://github.com/microsoft/microsoft-ui-xaml/issues/8816).
- Fixed an issue where `FlyoutShowMode.TransientWithDismissOnPointerMoveAway` didn't work after the first time it is shown. 
For more info, see GitHub issue [#8896](https://github.com/microsoft/microsoft-ui-xaml/issues/8896).
- Fixed an issue where some controls did not correctly template bind `Foreground` and `Background` properties. For more info, see GitHub issue [#7070](https://github.com/microsoft/microsoft-ui-xaml/issues/7070), [#9020](https://github.com/microsoft/microsoft-ui-xaml/issues/9020), [#9029](https://github.com/microsoft/microsoft-ui-xaml/issues/9029), [#9083](https://github.com/microsoft/microsoft-ui-xaml/issues/9083) and [#9102](https://github.com/microsoft/microsoft-ui-xaml/issues/9102).
- Fixed an issue where `ThemeResource`s used in `VisualStateManager` setters wouldn't update on theme change. This commonly affected controls in flyouts. For more info, see GitHub issue [#9198](https://github.com/microsoft/microsoft-ui-xaml/issues/9198).
- Fixed an issue where `WebView` would lose key focus, resulting in extra blur/focus events and other issues. 
For more info, see GitHub issue [#9288](https://github.com/microsoft/microsoft-ui-xaml/issues/9288).
- Fixed an issue where `NavigationView` can show a binding error in debug output. For more info, see GitHub issue [#9384](https://github.com/microsoft/microsoft-ui-xaml/issues/9384).
- Fixed an issue where SVG files defining a negative viewbox no longer rendered. For more info, see GitHub issue [#9415](https://github.com/microsoft/microsoft-ui-xaml/issues/9415).
- Fixed an issue where changing `ItemsView.Layout` orientation caused an item to be removed. For more info, see GitHub issue [#9422](https://github.com/microsoft/microsoft-ui-xaml/issues/9422).
- Fixed an issue where scrolling a `ScrollView` generated a lot of debug output. For more info, see GitHub issue [#9434](https://github.com/microsoft/microsoft-ui-xaml/issues/9434).
- Fixed an issue where `MapContorl.InteractiveControlsVisible` does not work properly. For more info, see GitHub issue [#9486](https://github.com/microsoft/microsoft-ui-xaml/issues/9486).
- Fixed an issue where `MapControl.MapElementClick` event doesn't properly fire. For more info, see GitHub issue [#9487](https://github.com/microsoft/microsoft-ui-xaml/issues/9487).
- Fixed an issue where x:Bind doesn't check for null before using a weak reference, which can result in a crash. For more info, see GitHub issue [#9551](https://github.com/microsoft/microsoft-ui-xaml/issues/9551).
- Fixed an issue where changing the `TeachingTip.Target` property doesn't correctly update its position. For more info, see GitHub issue [#9553](https://github.com/microsoft/microsoft-ui-xaml/issues/9553).
- Fixed an issue where dropdowns did not respond in WebView2. For more info, see GitHub issue [#9566](https://github.com/microsoft/microsoft-ui-xaml/issues/9566).
- Fixed a memory leak when using `GeometryGroup`. For more info, see GitHub issue [#9578](https://github.com/microsoft/microsoft-ui-xaml/issues/9578).
- Fixed an issue where scrolling through a very large number of items from an `ItemRepeater` in a `ScrollView` can cause blank render frames. For more info, see GitHub issue [#9643](https://github.com/microsoft/microsoft-ui-xaml/issues/9643).
- Fixed an issue where `SceneVisual` wasn't working.

## Related topics

- [Stable channel](stable-channel.md)
- [Preview channel](preview-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)
