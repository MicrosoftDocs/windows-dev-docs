---
title: Stable channel release notes for the Windows App SDK 1.4
description: Provides information about the stable release channel for the Windows App SDK 1.4.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Stable channel release notes for the Windows App SDK 1.4

The stable channel provides releases of the Windows App SDK that are supported for use by apps in production environments. Apps that use the stable release of the Windows App SDK can also be published to the Microsoft Store.

**Important links:**

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Downloads for the Windows App SDK

> [!NOTE]
> The Windows App SDK Visual Studio Extensions (VSIX) are no longer distributed as a separate download. They are available in the Visual Studio Marketplace inside Visual Studio.

### Version 1.4.6 (1.4.240512000)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.4 release.

- Fixed a potential crash when processing input.
- Fixed an issue where a drag-and-drop operation that started from another app might not allow the correct Copy/Move/Link drop operations.
- Fixed WinUI source server information for debugging to properly point to the microsoft-ui-xaml GitHub repo.
- Fixed an issue with the fix for GitHub issue [#8857](https://github.com/microsoft/microsoft-ui-xaml/issues/8857) to properly merge a library's `resources.pri` into the app's `resources.pri`.

### Version 1.4.5 (1.4.240211001)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.4 release.

- Fixed an issue that could hang applications when clicking a mouse button while scrolling with the mouse wheel. For more info, see GitHub issue [#9233](https://github.com/microsoft/microsoft-ui-xaml/issues/9233).
- Fixed an issue with duplicate assets when referencing a chain of NuGet packages. For more info, see GitHub issue [#8857](https://github.com/microsoft/microsoft-ui-xaml/issues/8857).
- Fixed several `BreadcrumbBar` issues including a memory leak, a crash when the ellipsis menu is empty, and the ellipsis menu being incorrectly constrained within the window.
- Fixed a potential crash on shutdown when releasing graphics resources.

### Version 1.4.4 (1.4.231219000)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.4 release.

- Fixed a WinUI 3 diagnostics security issue.
- Fixed an input issue where the password box didn't show the on-screen keyboard when activated via touch. For more info, see GitHub issue [#8946](https://github.com/microsoft/microsoft-ui-xaml/issues/8946).
- Fixed an issue that caused the `Microsoft.UI.Xaml.Controls.dll` file size to grow unexpectedly.
- Fixed a `CommandBarFlyout` issue that could cause crashes when setting focus.
- Updated Windows App SDK support for .NET 8 RID-specific asset handling.
- Fixed an issue causing some swapchains to be positioned or stretched incorrectly.

### Version 1.4.3 (1.4.231115000)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.4 release.

- Fixed an issue where a menu could appear without a background for a short period of time.
- Fixed a crash that may occur in specific multi-monitor scenarios.
- Fixed an issue where a context menu could appear off-screen.
- Fixed an issue with Window styles and maximizing behavior. For more info, see GitHub issue [#8996](https://github.com/microsoft/microsoft-ui-xaml/issues/8996).
- Fixed an issue with Islands where focus could be unexpectedly grabbed from another control.
- Fixed an issue with tab order on `NavigationView`.
- Fixed a rendering issue where a white bar might be visible at the top of the titlebar. For more info, see GitHub issue [#8947](https://github.com/microsoft/microsoft-ui-xaml/issues/8947).
- Various performance fixes.

### Version 1.4.2 (1.4.231008000)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.4 release.

- Fixed a crashing issue in explorer.exe caused by excessive memory and object allocation.
- Fixed a titlebar interaction issue that prevented the back button from working properly.
- Fixed an issue that caused a warning to be generated for a source file being included multiple times.
- Fixed an issue impacting context menu performance.
- Fixed a .lnk shortcut issue that made the target .exe always point to the same location for packages in the WindowsApps folder.
- Fixed a DWriteCore issue affecting proper rendering of Indic text in certain fonts.
- Fixed an issue in a List View that prevented proper keyboard navigation to and from nested selected items with *Tab/Shift + Tab*.
- Fixed an issue that broke scrolling ComboBox items by touch after expanding the ComboBox a second time. For more info, see GitHub issue [#8831](https://github.com/microsoft/microsoft-ui-xaml/issues/8831).
- Fixed an issue where WinAppSDK packages did not include WinUI's localized resources for some languages.
- Fixed an inconsistency between how File Explorer and XAML display a user's preferred language.
- Fixed a craftsmanship issue in File Explorer causing a thin line to show under the active tab.
- Fixed an issue where some framework-provided keyboard accelerators were not properly localized. For more info, see GitHub issue [#2023](https://github.com/microsoft/microsoft-ui-xaml/issues/2023).
- Fixed an issue with RepeatButton controls that were repeatedly scrolling when tapped.
- Fixed the WinAppSDK installer .exe to have proper resource version info.

### Version 1.4.1 (1.4.230913002)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.4 release.

- Fixed performance issues to improve the time to first frame.
- Fixed an issue where menus didn't respect `RequestedTheme`. For example, it was possible for this issue to lead to white text on a white background. For more info, see GitHub issue [#8756](https://github.com/microsoft/microsoft-ui-xaml/issues/8756).
- Fixed an issue that caused acrylic backgrounds to sometimes become fully transparent in some menus.
- Fixed an issue where XAML sometimes caused Windows to unnecessarily repaint the desktop wallpaper.
- Fixed support for `TabNavigation = Local` and `TabNavigation = Cycle` for `ListView` and `GridView`, which now enables navigating between headers and items with TAB in addition to arrow keys.
- Fixed some noisy exceptions when dismissing a tooltip. For more info, see GitHub issue [#8699](https://github.com/microsoft/microsoft-ui-xaml/issues/8699).

### New and updated features and known issues for version 1.4

The following sections describe new and updated features and known issues for version 1.4.

In an existing Windows App SDK 1.3 app, you can update your Nuget package to 1.4.230822000 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Latest Windows App SDK downloads](../downloads.md).

### Custom titlebar + AppWindow titlebar merger

The WinUI 3 custom titlebar uses the AppWindow titlebar implementation, along with the [NonClientInputPointerSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.input.inputnonclientpointersource) APIs, under the hood in the Windows App SDK 1.4. As a result, both titlebar implementations now behave the same way with the same features and limitations. This is fully backwards compatible in all supported cases - any app with a custom-defined titlebar will behave as before. But, it's now easier for WinUI 3 developers who might be new to custom titlebars to understand and use them by taking advantage of these new features:

- A better default scenario where the developer doesn't define a titlebar element specifically (replacing the fallback titlebar from WinUI 2)
- Distinct drag regions in the titlebar, enabling you to create multiple drag regions and place clickable controls on any part of the non-client area (titlebar area)
- App-wide draggable regions that can be put anywhere in the app or make the whole app draggable
- Better theming support that replaces resource-based theming
  - Since drag regions are transparent, they follow the app theme every time
- More customization: hide the min, max, and close buttons; place system icons in the titlebar; or have different regions act as caption buttons that receive NCHITTEST responses
- More developer freedom that enables you to mix and match with AppWindow titlebar APIs, such as using higher-level WinUI 3 APIs for most scenarios but with AppWindow APIs mixed in for lower-level control

### Widgets updates

Three new interfaces have been added for Widget Providers to implement: `IWidgetProvider2`, `IWidgetProviderAnalytics`, and `IWidgetProviderErrors`. `IWidgetProvider2` allows providers to respond to the *Customize* action invoked by the user, which is identical to what is available for 1st party Widgets. The `IWidgetProviderAnalytics` and `IWidgetProviderErrors` interfaces are used by providers to gather telemetry for their widgets; analytics and failure events about widgets are communicated to the respective widget providers. The `WidgetCustomizationRequestedArgs`, `WidgetAnalyticsInfoReportedArgs`, and `WidgetErrorInfoReportedArgs` classes are used to communicate relevant information to support new functionalities.

### XAML Islands no longer experimental

XAML Islands and the underlying ContentIslands platform are no longer experimental.

- Currently XAML Islands are only tested for use in C++ apps. This release does not include any convenient wrapper elements for use in WPF or WinForms.
- `DesktopWindowXamlSource` and related types have been added in the Microsoft.UI.Xaml.Hosting namespace for XAML Islands. `XamlRoot.ContentIslandEnvironment` was added to help access the underlying Island information for an element.
- Many new types have been introduced in the Microsoft.UI.Content namespace and the Microsoft.UI.Input namespace as the underlying support for XAML Islands or for using this ContentIslands functionality without XAML.
- A new `DragDropManager` (plus related types) has been added in the Microsoft.UI.Input.DragDrop namespace for Island scenarios.

### ItemsView

We're introducing a new list control called the `ItemsView` and a corresponding concrete `ItemContainer` class. `ItemContainer` is a lightweight container with built-in selection states and visuals, which can easily wrap desired content and be used with `ItemsView` for a collection control scenario.

- The new `ItemsView` control displays a data collection. `ItemsView` is similar to the `ListView` and `GridView` controls, but is built using the `ItemsRepeater`, `ScrollView`, `ItemContainer` and `ItemCollectionTransitionProvider` components. It offers the unique ability to plug in custom `Layout` or `ItemCollectionTransitionProvider` implementations. Another key advantage is the ability to switch the layout on the fly while preserving items selection. The inner `ScrollView` control also offers features unavailable in `ListView`/`GridView`'s `ScrollViewer` control such as the ability to control the animation during programmatic scrolls.
  - A new `ItemTransitionProvider` property on `ItemsRepeater` (and the new `ItemsView` control) lets you specify an `ItemCollectionTransitionProvider` object to control transition animations on that control. A `CreateDefaultItemTransitionProvider` method has also been added to `Layout`, which enables a layout object to provide a fallback transition to accompany it if you do not provide one explicitly on the `ItemsView` control.
  - A new `IndexBasedLayoutOrientation` property on `Layout` where the layout orientation, if any, of items is based on their index in the source collection. The default value is `IndexBasedLayoutOrientation.None`. Custom layouts set this property by calling the new (protected) `SetIndexBasedLayoutOrientation` method.
  - A new `VisibleRect` property on `VirtualizingLayoutContext` gets the visible viewport rectangle within the `FrameworkElement` associated with the `Layout`. The protected virtual `VirtualizingLayoutContext.VisibleRectCore` method can be overridden to provide the value that will be returned from the `VisibleRect` property.
- The new `LinedFlowLayout` class is typically used to lay out the items of the `ItemsView` collection control. It is particularly useful for displaying collection of pictures. It does so by laying them out from left to right, and top to bottom, in lines of equal height. The pictures fill a horizontal line and then wrap to a next line. Pictures may be cropped at the left and right edges to fit into a line. They may also be expanded horizontally and cropped at the top and bottom edges to fill a line when the stretching mode is employed.

### New features from across the WinAppSDK

- A new `ThemeSettings` class that allows Win32 WinRT apps to detect when the system's High Contrast setting has changed, similar to UWP's [AccessibilitySettings](/uwp/api/windows.ui.viewmanagement.accessibilitysettings) class. See the [ThemeSettings API spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/themes/ThemeSettings.md) on GitHub for more information.
- `AccessKeyManager.EnterDisplayMode` is a new method to display access keys for the current focused element of a provided root. Access keys are in "display mode" when showing a key tip to invoke a command, such as pressing the Alt key in Paint to show what keys correspond to what controls. This method allows for programmatically entering display mode.
- `Application.ResourceManagerRequested` provides a mechanism to provide a different `IResourceManager` to resolve resource URIs for scenarios when the default `ResourceManager` won't work. For more information, see the [Application.ResourceManagerRequested API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/custom-iresourcemanager-spec.md) on GitHub.
- The version of the WebView2 SDK was updated from 1661.34 to [1823.32](/microsoft-edge/webview2/release-notes?tabs=winrtcsharp#10182332).
- `Popup/FlyoutBase.IsConstrainedToRootBounds = false` is now supported, allowing a popup/flyout to extend outside the bounds of the parent window. A `SystemBackdrop` property has been added to these types to support having acrylic in these unconstrained popups. Menus by default use this to have acrylic.
- `Closed`, `FrameworkClosed`, and `IsClosed` have been added to `DesktopAcrylicController` and `MicaController` to improve handling during object/thread shutdown.
- `DesktopAcrylicController.Kind` can now be set to choose between some standard acrylic appearances.
- `DispatcherQueue` has some new events and helpers to facilitate better organized shutdown and for apps using Islands to easily run a standard supported event loop.
- `InputNonClientPointerSource` in the Microsoft.UI.Input namespace can be used for custom titlebar scenarios to define non-client area regions. Code can register for corresponding events like hover and click events on these regions.
- `AppWindow` has some new helpers to get and associate with a `DispatcherQueue`.
- The new `TreeView.SelectionChanged` event allows developers to respond when the user or code-behind changes the set of selected nodes in the `TreeView` control.
- The new `ScrollView` control provides a new alternative to `ScrollViewer`. This new control is highly aligned in behavior and API with the existing `ScrollViewer` control, but is based on `InteractionTracker`, has new features such as animation-driven view changes, and is also designed to ensure full functionality of `ItemsRepeater`. See [A more flexible ScrollViewer · Issue #108 · microsoft/microsoft-ui-xaml (github.com)](https://github.com/Microsoft/microsoft-ui-xaml/issues/108) for more details. Various new types, including `ScrollPresenter`, are part of the overall `ScrollView` model.
- The new `AnnotatedScrollBar` control extends a regular scrollbar's functionality by providing an easy way to navigate through a large collection of items. This is achieved through a clickable rail with labels that act as markers. It also allows for a more granular understanding of the scrollable content by displaying a tooltip when hovering over the clickable rail.

### Known issues

- When using `ExtendsContentIntoTitleBar = true`, clicks at the top-left corner of the window by default always show the system window menu (Minimize/Close/etc.) rather than letting the pointer input through to the content of the window. This, for example, means that a Back button in that area of the titlebar will not work. A workaround for this issue is to set `AppWindow.TitleBar.IconShowOptions = Microsoft.UI.Windowing.IconShowOptions.HideIconAndSystemMenu` on the Window's AppWindow.
- There are some new continuable exceptions when hiding `ShouldConstrainToRootBounds="False"` popups/flyouts. That includes hiding tooltips, as reported here: [Dismissing a tooltip throws 4 native exceptions · Issue #8699 · microsoft/microsoft-ui-xaml (github.com)](https://github.com/microsoft/microsoft-ui-xaml/issues/8699)
- In 1.4, the min/max/close caption buttons for `ExtendsContentIntoTitleBar = true` are now drawn by AppWindow rather than XAML. This is by design, but it can impact apps that were overriding XAML's internal styles to hide or do extra customization of these buttons, such as in this report: [Cannot hide caption button on titlebar · Issue #8705 · microsoft/microsoft-ui-xaml (github.com)](https://github.com/microsoft/microsoft-ui-xaml/issues/8705)
- There was a breaking change in .NET 8 to how it handles the runtime identifier graph:  [[Breaking change]: Projects targeting .NET 8 and higher will by default use a smaller, portable RID graph. · Issue #36527 · dotnet/docs (github.com)](https://github.com/dotnet/docs/issues/36527). Because of this issue and because .NET 8 has not officially released yet, the Windows App SDK 1.4 does not officially support .NET 8. However, if you would still like to target the pre-release version of .NET 8 with this version of the App SDK, we recommend the following steps:
  - [Setting UseRidGraph to true](/dotnet/core/compatibility/deployment/8.0/rid-asset-list#recommended-action) is recommended. You'll also need to update the `<RuntimeIdentifiers>` property in the `.csproj` file to `<RuntimeIdentifiers>win-x86;win-x64;win-arm64</RuntimeIdentifiers>`, as well as update each `Propeties\*pubxml` file to switch from `win10` to `win` in the `<RuntimeIdentifier>` property (for example, `<RuntimeIdentifier>win-x86</RuntimeIdentifier>`).
- With Windows App SDK 1.4, the target `GenerateDeploymentManagerCS` in `Microsoft.WindowsAppSDK.DeploymentManager.CS.targets` was renamed to `GenerateBootstrapCS`.
- `MenuFlyout` background doesn't use the application's requested theme:
  - [MenuFlyoutItem text doesn't sync with system theme. · Issue #8678 · microsoft/microsoft-ui-xaml (github.com)](https://github.com/microsoft/microsoft-ui-xaml/issues/8678)
  - [1.4 preview: Flyout backdrop theme is derived from system theme and not from its associated element · Issue #8756 · microsoft/microsoft-ui-xaml (github.com)](https://github.com/microsoft/microsoft-ui-xaml/issues/8756)

### Bug fixes

- Fixed an issue where calling the  `Microsoft.Windows.AppLifecycle.AppInstance.Restart("")` API caused unpackaged apps to crash. For more info, see GitHub issue [#2792](https://github.com/microsoft/WindowsAppSDK/issues/2792).
- Fixed an installer crashing issue that was introduced in 1.4-experimental1. For more info, see GitHub issue [#3760](https://github.com/microsoft/WindowsAppSDK/issues/3760).
- Fixed an issue where text strikethrough was not removed properly in a TextBlock. For more info, see GitHub issue [#1093](https://github.com/microsoft/microsoft-ui-xaml/issues/1093).
- Fixed an issue causing incorrect Shift + Tab  navigation in a Panel with *TabFocusNavigation* set to "Once." For more info, see GitHub issue [#1363](https://github.com/microsoft/microsoft-ui-xaml/issues/1363).
- Fixed an issue in C++/WinRT that prevented `{x:Bind}` from working properly with a property of a named XAML control. For more info, see GitHub issue [#2721](https://github.com/microsoft/microsoft-ui-xaml/issues/2721).
- Fixed a runtime AccessViolation issue in WinUI Desktop apps caused by setting `DebugSettings.EnableFrameRateCounter = true`. For more info, see GitHub issue [#2835](https://github.com/microsoft/microsoft-ui-xaml/issues/2835).
- Fixed an issue where `XamlTypeInfo.g.cpp` did not include needed headers. For more info, see GitHub issue [#4907](https://github.com/microsoft/microsoft-ui-xaml/issues/4907).
- Fixed a crashing issue caused by simultaneous multi-touch and mouse input. For more info, see GitHub issue [#7622](https://github.com/microsoft/microsoft-ui-xaml/issues/7622).
- Fixed an issue that prevented an active WinUI 3 app window from scrolling when the system setting to disable scrolling of inactive windows on mouse over was in effect. For more info, see GitHub issue [#8764](https://github.com/microsoft/microsoft-ui-xaml/issues/8764).
- Fixed a crash when trying to subclass `MediaPlayerElement`.
- Fixed some crash and memory leak issues in `TreeView`.
- Fixed an app hang issue that could happen when using keyboard to navigate in `RadioButtons`.
- Fixed a crash when using the keyboard to navigate in a `PipsPager`.
- Fixed WebView2 content to scale with the "Text size" Accessibility setting in the Settings app.
- Fixed a crash that could occur when animations were running when the display turned off.
- Fixed a performance issue introduced in 1.3 that added a ~10% overhead to first layout/render.

## Related topics

- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
