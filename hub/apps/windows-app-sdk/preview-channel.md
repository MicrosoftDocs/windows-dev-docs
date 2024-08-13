---
title: Preview release channel for the Windows App SDK 
description: Provides info about the preview release channel for the Windows App SDK.
ms.topic: article
ms.date: 08/07/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Latest preview channel release notes for the Windows App SDK

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store.

The preview channel includes releases of the Windows App SDK with [preview channel features](release-channels.md#features-available-by-release-channel) in late stages of development. Preview releases do not include experimental features and APIs but may still be subject to breaking changes before the next stable release.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](update-existing-projects-to-the-latest-release.md).
- For documentation on preview releases, see [Install tools for preview and experimental channels of the Windows App SDK](preview-experimental-install.md).

**Preview channel release note archive:**

- [Preview channel release notes for the Windows App SDK 1.5](release-notes-archive/preview-channel-1.5.md)
- [Preview channel release notes for the Windows App SDK 1.4](release-notes-archive/preview-channel-1.4.md)
- [Preview channel release notes for the Windows App SDK 1.3](release-notes-archive/preview-channel-1.3.md)
- [Preview channel release notes for the Windows App SDK 1.2](release-notes-archive/preview-channel-1.2.md)
- [Preview channel release notes for the Windows App SDK 1.1](release-notes-archive/preview-channel-1.1.md)
- [Preview channel release notes for the Windows App SDK 1.0](release-notes-archive/preview-channel-1.0.md)

## Version 1.6 Preview 1 (1.6.0-preview1)

This is the latest release of the preview channel for version 1.6.

In an existing Windows App SDK 1.5 (from the stable channel) app, you can update your Nuget package to 1.6.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Downloads for the Windows App SDK](./downloads.md).

### New features

> [!NOTE]
> The new TitleBar control we released in 1.6-experimental1 is not yet available in non-experimental builds of 1.6 to allow more time to evaluate and respond to community feedback. We received a lot of great input here and want to make sure we take the time needed to address it.

#### Required C# project changes for 1.6-preview1

In 1.6-preview1, Windows App SDK managed apps require [Microsoft.Windows.SDK.NET.Ref](https://www.nuget.org/packages/Microsoft.Windows.SDK.NET.Ref) `*.*.*.38`, which can be specified via [WindowsSdkPackageVersion](/dotnet/core/compatibility/sdk/5.0/override-windows-sdk-package-version) in your `csproj` file. For example:

```XML
<Project Sdk="Microsoft.NET.Sdk">
   <PropertyGroup>
       <OutputType>WinExe</OutputType>
       <TargetFramework>net8.0-windows10.0.22621.0</TargetFramework>
       <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
       <WindowsSdkPackageVersion>10.0.22621.38</WindowsSdkPackageVersion>
   <PropertyGroup>
   ...
```

In addition, Windows App SDK managed apps should update to [Microsoft.Windows.CsWinRT](https://www.nuget.org/packages/Microsoft.Windows.CsWinRT) `2.1.1` (or later).

#### Native AOT support

The .NET `PublishAot` project property is now supported for native Ahead-Of-Time compilation. For details on Native AOT, see [Native AOT Deployment](/dotnet/core/deploying/native-aot/). Because AOT builds on Trimming support, much of the Trimming-related guidance previously described in the 1.6-experimental1 release applies as well. See [Native AOT support](/windows/apps/windows-app-sdk/experimental-channel#native-aot-support) for more info.

As noted above, C# projects should have a package reference to [Microsoft.Windows.CsWinRT](https://www.nuget.org/packages/Microsoft.Windows.CsWinRT) 2.1.1 (or later).This version includes an AOT-safe `ICustomPropertyProvider` implementation. Types used with this support should be marked with the `WinRT.GeneratedBindableCustomProperty` attribute along with being made `partial`.

#### Changed Edge WebView2 SDK Integration

The Windows App SDK now consumes the Edge WebView2 SDK as a NuGet reference rather than embedding a hardcoded version of the Edge WebView2 SDK. The new model allows apps to choose a newer version of the `Microsoft.Web.WebView2` package instead of being limited to the version with which the Windows App SDK was built. The new model also allows apps to reference NuGet packages which also reference the Edge WebView2 SDK. For more info, see GitHub issue [#5689](https://github.com/microsoft/microsoft-ui-xaml/issues/5689).

#### New Package Deployment APIs

The Package Management API has received several enhancements including Is\*ReadyOrNewerAvailable\*(), EnsureReadyOptions.RegisterNewerIfAvailable, Is\*Provisioned\*(), IsPackageRegistrationPending(), and several bug fixes. See [PackageManagement.md](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/packagemanager/PackageManagement.md) and [Pull Request #4453](https://github.com/microsoft/WindowsAppSDK/pull/4453) for more details. 

#### Improved TabView tab tear-out

`TabView` supports a new `CanTearOutTabs` mode which provides an enhanced experience for dragging tabs and dragging out to a new window. When this new option is enabled, tab dragging is very much like the tab drag experience in Edge and Chrome where a new window is immediately created during the drag, allowing the user to drag it to the edge of the screen to maximize or snap the window in one smooth motion. This implementation also doesn't use drag-and-drop APIs, so it isn't impacted by any limitations in those APIs. Notably, tab tear-out is supported in processes running elevated as Administrator.

#### Other notable changes

- We added a new `ColorHelper.ToDisplayName()` API, filling that gap from UWP.
- Added a new `Microsoft.Windows.Globalization.ApplicationLanguages` class, which notably includes a new `PrimaryLanguageOverride` feature. For more info, see GitHub issue [#4523](https://github.com/microsoft/WindowsAppSDK/pull/4523).
- Unsealed `ItemsWrapGrid`. This should be a backward-compatible change.
- `PipsPager` supports a new mode where it can wrap between the first and list items.
- `RatingControl` is now more customizable, by moving some hard-coded style properties to theme resources. This allows apps to override these values to better customize the appearance of RatingControl.

### Known Issues

- If the debugger is set to break on all C++ exceptions, it will break on a pair of noisy exceptions on start-up in the BCP47 (Windows Globalization) code.

### Bug Fixes

- Fixed a few issues around handling of custom titlebar scenarios. For more info, see GitHub issues [#7629](https://github.com/microsoft/microsoft-ui-xaml/issues/7629), [#9670](https://github.com/microsoft/microsoft-ui-xaml/issues/9670), [#9709](https://github.com/microsoft/microsoft-ui-xaml/issues/9709) and [#8431](https://github.com/microsoft/microsoft-ui-xaml/issues/8431).
- Fixed an issue where `InfoBadge` icon was not visible. For more info, see GitHub issue [#8176](https://github.com/microsoft/microsoft-ui-xaml/issues/8176).
- Fixed an issue with icons sometimes showing in the wrong position in `CommandBarFlyout`. For more info, see GitHub issue [#9409](https://github.com/microsoft/microsoft-ui-xaml/issues/9409).
- Fixed an issue with keyboard focus in menus when opening or closing a sub menu. For more info, see GitHub issue [#9519](https://github.com/microsoft/microsoft-ui-xaml/issues/9519).
- Fixed an issue with `TreeView` using the incorrect `IsExpanded` state when recycling items. For more info, see GitHub issue [#9549](https://github.com/microsoft/microsoft-ui-xaml/issues/9549).
- Fixed an issue when using an ElementName binding in an `ItemsRepeater.ItemTemplate`. For more info, see GitHub issue [#9715](https://github.com/microsoft/microsoft-ui-xaml/issues/9715).
- Fixed an issue with the first item in an `ItemsRepeater` sometimes having an incorrect position. For more info, see GitHub issue [#9743](https://github.com/microsoft/microsoft-ui-xaml/issues/9743).
- Fixed an issue with `InputNonClientPointerSource` sometimes breaking input to the min/max/close buttons. For more info, see GitHub issue [#9749](https://github.com/microsoft/microsoft-ui-xaml/issues/9749).
- Fixed a compile error when using Microsoft.UI.Interop.h with clang-cl. For more info, see GitHub issue [#9771](https://github.com/microsoft/microsoft-ui-xaml/issues/9771).
- Fixed an issue where the `CharacterReceived` event was not working in `ComboBox`/`TextBox`. For more info, see GitHub issue [#9786](https://github.com/microsoft/microsoft-ui-xaml/issues/9786).
- Fixed the issue in the 1.6-experimental builds where pointer input behavior for `CanTearOutTabs` was incorrect on monitors with scale factor different than 100%. For more info, see GitHub issue [#9791](https://github.com/microsoft/microsoft-ui-xaml/issues/9791).
- Fixed the issue in the 1.6-experimental2 build where some language translations had character encoding issues for `ColorHelper.ToDisplayName()`.
- Fixed an issue from 1.6-experimental1 where `NumberBox` wasn't using the correct foreground and background colors. For more info, see GitHub issue [#9714](https://github.com/microsoft/microsoft-ui-xaml/issues/9714).
- Fixed an issue where duplicate `KeyUp` events were raised for arrow and tab keys. For more info, see GitHub issue [#9399](https://github.com/microsoft/microsoft-ui-xaml/issues/9399).
- Fixed an issue where the `PowerManager.SystemSuspendStatusChanged` event was unusable to get the `SystemSuspendStatus`. For more info, see GitHub issue [#2833](https://github.com/microsoft/WindowsAppSDK/issues/2833).
- Fixed an issue where initial keyboard focus was not correctly given to a `WebView2` when that was the only control in the window.
- Fixed an issue when using `ExtendsContentIntoTitleBar=true` where the Min/Max/Close buttons did not correctly appear in the UI Automation, which prevented Voice Access from showing numbers for those buttons.
- Fixed an issue where an app might crash in a lock check due to unexpected reentrancy.
- Fixed an issue where `Hyperlink` colors did not correctly update when switching into a high contrast theme.
- Fixed an issue where changing the collection of a `ListView` in a background window may incorrectly move that window to the foreground and take focus.
- Fixed an issue from 1.6-experimental1 where setting `AcrylicBrush.TintLuminosityOpacity` in .xaml in a class library project would crash with a type conversion error.
- Fixed an issue where calling `ItemsRepeater.StartBringIntoView` could sometimes cause items to disappear.
- Fixed an issue where touching and dragging on a `Button` in a `ScrollViewer` would leave it in a pressed state.
- Updated IntelliSense, which was missing information for many newer types and members.
- Fixed an issue where clicking in an empty area of a `ScrollViewer` would always move focus to the first focusable control in the `ScrollViewer` and scroll that control into view. For more info, see GitHub issue [#597](https://github.com/microsoft/microsoft-ui-xaml/issues/597).
- Fixed an issue where the `Window.Activated` event sometimes fired multiple times. For more info, see GitHub issue [#7343](https://github.com/microsoft/microsoft-ui-xaml/issues/7343).
- Fixed an issue where setting the `NavigationViewItem.IsSelected` property to `true` prevented its children from showing when expanded. For more info, see GitHub issue [#7930](https://github.com/microsoft/microsoft-ui-xaml/issues/7930).
- Fixed an issue where `MediaPlayerElement` would not properly display captions with `None` or `DropShadow` edge effects. For more info, see GitHub issue [#7981](https://github.com/microsoft/microsoft-ui-xaml/issues/7981).
- Fixed an issue where the `Flyout.ShowMode` property was not used when showing the flyout. For more info, see GitHub issue [#7987](https://github.com/microsoft/microsoft-ui-xaml/issues/7987).
- Fixed an issue where `NumberBox` would sometimes have rounding errors. For more info, see GitHub issue [#8780](https://github.com/microsoft/microsoft-ui-xaml/issues/8780).
- Fixed an issue where using a library compiled against an older version of WinAppSDK could hit an error trying to find a type or property. 
For more info, see GitHub issue [#8810](https://github.com/microsoft/microsoft-ui-xaml/issues/8810).
- Fixed an issue where initial keyboard focus was not set when launching a window. For more info, see GitHub issue [#8816](https://github.com/microsoft/microsoft-ui-xaml/issues/8816).
- Fixed an issue where `FlyoutShowMode.TransientWithDismissOnPointerMoveAway` didn't work after the first time it was shown. 
For more info, see GitHub issue [#8896](https://github.com/microsoft/microsoft-ui-xaml/issues/8896).
- Fixed an issue where some controls did not correctly template bind `Foreground` and `Background` properties. For more info, see GitHub issue [#7070](https://github.com/microsoft/microsoft-ui-xaml/issues/7070), [#9020](https://github.com/microsoft/microsoft-ui-xaml/issues/9020), [#9029](https://github.com/microsoft/microsoft-ui-xaml/issues/9029), [#9083](https://github.com/microsoft/microsoft-ui-xaml/issues/9083) and [#9102](https://github.com/microsoft/microsoft-ui-xaml/issues/9102).
- Fixed an issue where `ThemeResource`s used in `VisualStateManager` setters wouldn't update on theme change. This commonly affected controls in flyouts. For more info, see GitHub issue [#9198](https://github.com/microsoft/microsoft-ui-xaml/issues/9198).
- Fixed an issue where `WebView` would lose key focus, resulting in extra blur/focus events and other issues. 
For more info, see GitHub issue [#9288](https://github.com/microsoft/microsoft-ui-xaml/issues/9288).
- Fixed an issue where `NavigationView` could show a binding error in debug output. For more info, see GitHub issue [#9384](https://github.com/microsoft/microsoft-ui-xaml/issues/9384).
- Fixed an issue where SVG files defining a negative viewbox no longer rendered. For more info, see GitHub issue [#9415](https://github.com/microsoft/microsoft-ui-xaml/issues/9415).
- Fixed an issue where changing `ItemsView.Layout` orientation caused an item to be removed. For more info, see GitHub issue [#9422](https://github.com/microsoft/microsoft-ui-xaml/issues/9422).
- Fixed an issue where scrolling a `ScrollView` generated a lot of debug output. For more info, see GitHub issue [#9434](https://github.com/microsoft/microsoft-ui-xaml/issues/9434).
- Fixed an issue where `MapContorl.InteractiveControlsVisible` did not work properly. For more info, see GitHub issue [#9486](https://github.com/microsoft/microsoft-ui-xaml/issues/9486).
- Fixed an issue where `MapControl.MapElementClick` event didn't properly fire. For more info, see GitHub issue [#9487](https://github.com/microsoft/microsoft-ui-xaml/issues/9487).
- Fixed an issue where x:Bind didn't check for null before using a weak reference, which could result in a crash. For more info, see GitHub issue [#9551](https://github.com/microsoft/microsoft-ui-xaml/issues/9551).
- Fixed an issue where changing the `TeachingTip.Target` property didn't correctly update its position. For more info, see GitHub issue [#9553](https://github.com/microsoft/microsoft-ui-xaml/issues/9553).
- Fixed an issue where dropdowns did not respond in WebView2. For more info, see GitHub issue [#9566](https://github.com/microsoft/microsoft-ui-xaml/issues/9566).
- Fixed a memory leak when using `GeometryGroup`. For more info, see GitHub issue [#9578](https://github.com/microsoft/microsoft-ui-xaml/issues/9578).
- Fixed an issue where scrolling through a very large number of items from an `ItemRepeater` in a `ScrollView` could cause blank render frames. For more info, see GitHub issue [#9643](https://github.com/microsoft/microsoft-ui-xaml/issues/9643).
- Fixed an issue where `SceneVisual` wasn't working.

### New APIs in 1.6.0-preview1

Version 1.6-preview1 includes the following new APIs compared to the stable 1.5 release:

```C#
Microsoft.UI

    ColorHelper
        ToDisplayName
```

```C#
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
```

```C#
Microsoft.UI.Xaml

    XamlRoot
        CoordinateConverter
```

```C#
Microsoft.UI.Xaml.Automation.Peers

    ScrollPresenterAutomationPeer
```

```C#
Microsoft.UI.Xaml.Controls

    PipsPager
        WrapMode
        WrapModeProperty

    PipsPagerWrapMode
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
```

```C#
Microsoft.Windows.Globalization

    ApplicationLanguages
```

```C#
Microsoft.Windows.Management.Deployment

    EnsureReadyOptions
        RegisterNewerIfAvailable

    PackageDeploymentFeature
    PackageDeploymentManager
        IsPackageDeploymentFeatureSupported
        IsPackageProvisioned
        IsPackageProvisionedByUri
        IsPackageReadyOrNewerAvailable
        IsPackageReadyOrNewerAvailableByUri
        IsPackageSetProvisioned
        IsPackageSetReadyOrNewerAvailable

    PackageReadyOrNewerAvailableStatus
```

```C#
Microsoft.Windows.Storage

    ApplicationData
    ApplicationDataContainer
    ApplicationDataContract
    ApplicationDataCreateDisposition
    ApplicationDataLocality
```

## Related topics

- [Stable channel](stable-channel.md)
- [Experimental channel](experimental-channel.md)
- [Install tools for the Windows App SDK](set-up-your-development-environment.md)
- [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](use-windows-app-sdk-in-existing-project.md)
- [Deploy apps that use the Windows App SDK](../package-and-deploy/index.md#use-the-windows-app-sdk)