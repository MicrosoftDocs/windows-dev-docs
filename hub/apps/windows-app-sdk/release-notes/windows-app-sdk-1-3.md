---
title: Windows App SDK 1.3 release notes
description: Provides information about what's new in Windows App SDK 1.3.
ms.topic: release-notes
ms.date: 09/22/2025
keywords: windows win32, windows app development, Windows App SDK, release notes
ms.localizationpriority: medium
zone_pivot_groups: wasdk-release-channels
---

# Windows App SDK 1.3 release notes

[!INCLUDE [wasdk-releasenotes](../../../includes/wasdk-release-notes.md)]

:::zone pivot="stable"

## Version 1.3.3 (1.3.230724000)

<details><summary>Bug fixes</summary>

> - Fixed an issue where the mouse would sometimes stop working when a dialog box was closed.
> - Fixed a deployment issue that prevented apps from installing due to a mismatch of package versions on the system. For more information, see GitHub issue [#3740](https://github.com/microsoft/WindowsAppSDK/issues/3740).
> - Fixed an issue affecting context menu positioning in Windows App SDK 1.3.
> - Fixed an issue causing some WinUI3 apps, in some situations, to crash when the app was closed because XAML shut itself down too early.
> - Fixed an issue where font icons were not mirroring properly in right-to-left languages. For more information, see GitHub issue [#7661](https://github.com/microsoft/microsoft-ui-xaml/issues/7661).
> - Fixed an issue causing an app to crash on shutdown when resources were torn down in a bad order. For more information, see GitHub issue [#7924](https://github.com/microsoft/microsoft-ui-xaml/issues/7924).

</details>

---

## Version 1.3.2 (1.3.230602002)

<details><summary>Bug fixes</summary>

> - Fixed a crash when setting a Protected Cursor.
> - Fixed a performance issue in XamlMetadataProvider during app startup. For more information, see GitHub issue [#8281](https://github.com/microsoft/microsoft-ui-xaml/issues/8281).
> - Fixed an issue with hyperlinks and touch in a RichTextBlock. For more information, see GitHub issue [#6513](https://github.com/microsoft/microsoft-ui-xaml/issues/6513).
> - Fixed an issue with scrolling and touchpads in WebView2. For more information, see GitHub issue [#7772](https://github.com/microsoft/microsoft-ui-xaml/issues/7772).
> - Fixed an issue where an update of Windows App SDK sometimes required a restart of Visual Studio. For more information, see GitHub issue [#3554](https://github.com/microsoft/microsoft-ui-xaml/issues/3554).
> - Fixed a noisy exception on shutdown when running in a debugger.

</details>

---

## Version 1.3.1 (1.3.230502000)

<details><summary>Bug fixes</summary>

> - Fixed issue causing apps to crash when setting the SystemBackdrop if the Content was null. For more information, see GitHub issue [#8416](https://github.com/microsoft/microsoft-ui-xaml/issues/8416).
> - Fixed issue causing apps to crash when setting the Window Title in XAML, a new capability added in 1.3.0. For more information, see GitHub issue [#3689](https://github.com/microsoft/microsoft-ui-xaml/issues/3689).
> - Fixed issue where a window incorrectly took focus when its content changed.
> - Fixed an issue with creating C++ projects with the Windows App SDK 1.3 project templates.
> - Updated templates on Visual Studio Marketplace

</details>

---

## Version 1.3

<details><summary>XAML Backdrop APIs</summary>

>
> With properties built in to the XAML Window, Mica & Background Acrylic backdrops are now easier to use in your WinUI app.
> See the [System Backdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.systembackdrop) and [Mica Backdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.micabackdrop) API docs for more information about the Xaml Backdrop properties.
>
> ```csharp
> public MainWindow()
> {
>     this.InitializeComponent();
>
>     this.SystemBackdrop = new MicaBackdrop();
> }
> ```
>

</details>

<details><summary>Window.AppWindow</summary>

>
> By replacing several lines of boilerplate code, you're now able to use AppWindow APIs directly from a **Window** through [`Window.AppWindow`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow).
>

</details>

<details><summary>New features from across Windows App SDK</summary>

>
> - `ApplicationModel.DynamicDependency`: `PackageDependency.PackageGraphRevisionId` that replaces the deprecated MddGetGenerationId.
> - Environment Manager: [`EnvironmentManager.AreChangesTracked`](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.environmentmanager.arechangestracked) to inform you whether changes to the environment manager are able to be tracked in your application.
> - A new event, DebugSettings.XamlResourceReferenceFailed is now raised when a referenced Static/ThemeResource lookup can't be resolved. This event gives access to a trace that details where the framework searched for that key in order to better enable you to debug Static & ThemeResource lookup failures. For more information, see the [Tracing XAML resource reference lookup failures](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/xaml-resource-references-tracing-spec.md) API spec on GitHub.
>

</details>

<details><summary>Other updates</summary>

>
> - See our [Windows App SDK 1.3 milestone](https://github.com/microsoft/WindowsAppSDK/milestone/14?closed=1) on the [Windows App SDK GitHub](https://github.com/microsoft/WindowsAppSDK) for additional issues addressed in this release.
> - See our [WinUI 3 in Windows App SDK 1.3 milestone](https://github.com/microsoft/microsoft-ui-xaml/milestone/18?closed=1) on the [microsoft-ui-xaml GitHub](https://github.com/microsoft/microsoft-ui-xaml) for additional issues addressed in this release.
> - With the latest experimental VSIX, you're now able to convert your app between unpackaged and packaged through the Visual Studio menu instead of in your project file.
>

</details>

<details><summary>Known issue</summary>

>
> Due to a recent change to the xaml compiler, an existing project that upgrades to 1.3 may experience a build error like the following within Visual Studio:
>
> ```console
> C:\Users\user\\.nuget\packages\microsoft.windowsappsdk\\**1.3.230331000**\buildTransitive\Microsoft.UI.Xaml.Markup.Compiler.interop.targets(537,17): error MSB4064: The "PrecompiledHeaderFile" parameter is not supported by the "CompileXaml" task loaded from assembly: Microsoft.UI.Xaml.Markup.Compiler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=de31ebe4ad15742b from the path: C:\Users\user\\.nuget\packages\microsoft.windowsappsdk\\**1.2.230118.102**\tools\net472\Microsoft.UI.Xaml.Markup.Compiler.dll. Verify that the parameter exists on the task, the <UsingTask> points to the correct assembly, and it is a settable public instance property.
> ```
>
> This is caused by Visual Studio using a cached xaml compiler task dll from 1.2, but driving it with incompatible MSBuild logic from 1.3, as seen in the error text above.  The workaround is to shut down Visual Studio, restart it, and reload the solution.

</details>

:::zone-end

:::zone pivot="preview"

## Version 1.3 Preview 1 (1.3.0-preview1)

<details><summary>XAML Backdrop APIs</summary>

>
> With properties built in to the XAML Window, Mica & Background Acrylic backdrops are now easier to use in your WinUI app.
>
> See the [Xaml Backdrop API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/33541da536673fa360212e94e4a6ac896b8b49fb/specs/xaml-backdrop-api.md?plain=1#L39) on GitHub for more information about the **Window.SystemBackdrop** property.
>
> ```csharp
> public MainWindow()
> {
>     this.InitializeComponent();
>
>     this.SystemBackdrop = new MicaBackdrop();
> }
> ```
>

</details>

<details><summary>Window.AppWindow</summary>

>
> Replacing several lines of boilerplate code, you're now able to use AppWindow APIs directly from a **Window** through `Window.AppWindow`. See the [Window.AppWindow API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/appwindow-spec.md) on GitHub for additional background and usage information.
>

</details>

<details><summary>New features from across Windows App SDK</summary>

>
> - `ApplicationModel.DynamicDependency`: `PackageDependency.PackageGraphRevisionId` that replaces the deprecated MddGetGenerationId.
> - Environment Manager: `EnvironmentManager.AreChangesTracked` to inform you whether changes to the environment manager are able to be tracked in your application. See the [Environment Manager API spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppLifecycle/EnvironmentVariables/AppLifecycle%20-%20Environment%20Variables%20(EV).md) on GitHub for more information.
> - MRT Core: A new event, `Application.ResourceManagerInitializing` allows your app to provide its own implementation of the `IResourceManager` interface, and gives you access to the ResourceManager that WinUI uses to resolve resource URIs. See the [IResourceManager API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/33541da536673fa360212e94e4a6ac896b8b49fb/specs/custom-iresourcemanager-spec.md) on GitHub for more information.
> - With the latest experimental VSIX, you're now able to convert your app between unpackaged and packaged through the Visual Studio menu instead of in your project file.
> - A new event, `DebugSettings.XamlResourceReferenceFailed` is now raised when a referenced Static/ThemeResource lookup can't be resolved. This event gives access to a trace that details where the framework searched for that key in order to better enable you to debug Static & ThemeResource lookup failures. For more information, see the [API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/xaml-resource-references-tracing-spec.md) and issues [4972](https://github.com/microsoft/microsoft-ui-xaml/issues/4972), [2350](https://github.com/microsoft/microsoft-ui-xaml/issues/2350), and [6073](https://github.com/microsoft/microsoft-ui-xaml/issues/6073) on GitHub.
> - Deployment: To manage and repair the Windows App Runtime, `DeploymentRepairOptions` is now available as part of the `DeploymentManager`. For more information, see the Repair section of the [Deployment API Spec](https://github.com/microsoft/WindowsAppSDK/blob/user/sachinta/DeploymentRepairAPISpec/specs/Deployment/DeploymentAPI.md#repair) on GitHub.
>

</details>

<details><summary>Known issues</summary>

>
> - The Pivot control causes a runtime crash with a XAML parsing error. See issue [#8160](https://github.com/microsoft/microsoft-ui-xaml/issues/8160) on GitHub for more info.
> - When the DatePicker or TimePicker flyout is opened, the app crashes.
> - The `WindowsAppRuntime.ReleaseInfo` and `WindowsAppRuntime.RuntimeInfo` APIs introduced in 1.3 releases are not yet supported as they contain a critical bug.
>

</details>

:::zone-end

:::zone pivot="experimental"

## Version 1.3 Experimental (1.3.0-experimental1)

<details>
<summary>XAML Backdrop APIs</summary>

>
> With properties built in to the XAML Window, Mica & Background Acrylic backdrops are now easier to use in your WinUI app.
>
> See the [Xaml Backdrop API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/33541da536673fa360212e94e4a6ac896b8b49fb/specs/xaml-backdrop-api.md?plain=1#L39) on GitHub for more information about the **Window.SystemBackdrop** property.
>
> Of note in this release, you're able to set the backdrop only in code-behind, as below. Setting `<Window.SystemBackdrop>` in markup results in a compile error.
>
> Additionally, the Xaml Backdrop APIs are currently missing an 'experimental' tag as they are under active development.
>
> ```csharp
> public MainWindow()
> {
>     this.InitializeComponent();
>
>     this.SystemBackdrop = new MicaBackdrop();
> }
> ```
>

</details>

<details>
<summary>Window.AppWindow</summary>

>
> Replacing several lines of boilerplate code, you're now able to use AppWindow APIs directly from a **Window** through `Window.AppWindow`. See the [Window.AppWindow API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/appwindow-spec.md) on GitHub for additional background and usage information.
>

</details>

<details>
<summary>New features from across Windows App SDK</summary>

>
> - `ApplicationModel.DynamicDependency`: `PackageDependency.PackageGraphRevisionId` that replaces the deprecated MddGetGenerationId.
> - Environment Manager: `EnvironmentManager.AreChangesTracked` to inform you whether changes to the environment manager are able to be tracked in your application. See the [Environment Manager API spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppLifecycle/EnvironmentVariables/AppLifecycle%20-%20Environment%20Variables%20(EV).md) on GitHub for more information.
> - MRT Core: A new event, `Application.ResourceManagerInitializing` allows your app to provide its own implementation of the `IResourceManager` interface, and gives you access to the ResourceManager that WinUI uses to resolve resource URIs.
> - With the latest experimental VSIX, you're now able to convert your app between unpackaged and packaged through the Visual Studio menu instead of in your project file.
> - A new event, `DebugSettings.XamlResourceReferenceFailed` is now raised when a referenced Static/ThemeResource lookup can't be resolved. This event gives access to a trace that details where the framework searched for that key in order to better enable you to debug Static & ThemeResource lookup failures. For more information, see issues [4972](https://github.com/microsoft/microsoft-ui-xaml/issues/4972), [2350](https://github.com/microsoft/microsoft-ui-xaml/issues/2350), and [6073](https://github.com/microsoft/microsoft-ui-xaml/issues/6073) on GitHub.
>

</details>

<details>
<summary>Bug fixes</summary>

>
> - Fixed issues with touch input causing the soft keyboard to not appear on text boxes. For more information, see issue [6291](https://github.com/microsoft/microsoft-ui-xaml/issues/6291) on GitHub.
> - Fixed issue causing an ItemsRepeater with an IElementFactory as its ItemTemplate to throw an ArgumentException. For more info, see issue [4705](https://github.com/microsoft/microsoft-ui-xaml/issues/4705) on GitHub.
>

</details>

<details>
<summary>Additional Experimental APIs</summary>

>
> This release also includes several APIs that are in early development.
>
> The list below details the APIs introduced in this experimental release that we don't plan to ship in the 1.3.0 stable release.
>
> ```csharp
> **Microsoft.UI.Content**
>
>     DesktopSiteBridge
>         GetInputEnabledToRoot
>         GetVisibleToRoot
>         InputEnabled
> ```
>
> ```csharp
> **Microsoft.UI.Dispatching**
>
>     DispatcherQueue
>         FrameworkShutdownStarting
> ```
>
> ```csharp
> **Microsoft.UI.Input**
>
>     InputLightDismissAction
>         GetForIsland
>
>     InputNonClientPointerSource
>     InputPointerActivationBehavior
>     InputPointerSource
>         ActivationBehavior
>
>     NonClientRegionCaptionTappedEventArgs
>     NonClientRegionHoverEventArgs
>     NonClientRegionKind
> ```
>
> ```csharp
> **Microsoft.UI.Input.DragDrop**
>
>     DragDropManager
>     DragDropModifiers
>     DragInfo
>     DragOperation
>     DragUIContentMode
>     DragUIOverride
>     DropOperationTargetRequestedEventArgs
>     IDropOperationTarget
> ```
>
> ```csharp
> **Microsoft.UI.Xaml.Automation.Peers**
>
>     ItemContainerAutomationPeer
>     ItemsViewAutomationPeer
> ```
>
> ```csharp
> **Microsoft.UI.Xaml.Controls**
>
>     AnnotatedScrollBar
>     AnnotatedScrollBarLabel
>     AnnotatedScrollBarScrollEventArgs
>     AnnotatedScrollBarScrollEventType
>     AnnotatedScrollBarScrollOffsetRequestedEventArgs
>     AnnotatedScrollBarSubLabelRequestedEventArgs
>     AnnotatedScrollBarValueRequestedEventArgs
>     ElementFactory
>         GetElement
>         GetElementCore
>         RecycleElement
>         RecycleElementCore
>
>     IndexBasedLayoutOrientation
>     ItemContainer
>     ItemContainerInteractionTrigger
>     ItemContainerInvokedEventArgs
>     ItemContainerMultiSelectMode
>     ItemContainerUserInvokeMode
>     ItemContainerUserSelectMode
>     ItemsView
>     ItemsViewItemInvokedEventArgs
>     ItemsViewItemInvokeMode
>     ItemsViewSelectionMode
>     Layout
>         IndexBasedLayoutOrientation
>
>     NonVirtualizingLayout
>         IndexBasedLayoutOrientationCore
>
>     RiverFlowLayout
>     RiverFlowLayoutItemsInfoRequestedEventArgs
>     RiverFlowLayoutItemsJustification
>     RiverFlowLayoutItemsStretch
>     VirtualizingLayout
>         IndexBasedLayoutOrientationCore
>
>     VirtualizingLayoutContext
>         VisibleRect
>         VisibleRectCore
> ```
>
> ```csharp
> **Microsoft.Graphics.Display**
>
>     DisplayInformation
>         AngularOffsetFromNativeOrientation
>         DpiChanged
>         OrientationChanged
>         RawDpi
>         RawPixelsPerViewPixel
>
>     DisplayOrientation
> ```
>
> ```csharp
> **Microsoft.UI.Xaml.Hosting**
>
>     DesktopWindowXamlSource
>         CreateSiteBridge
>         SiteBridge
>         SystemBackdrop
> ```
>

</details>

:::zone-end