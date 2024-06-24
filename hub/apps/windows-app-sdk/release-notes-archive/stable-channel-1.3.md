---
title: Stable channel release notes for the Windows App SDK 1.3
description: Provides information about the stable release channel for the Windows App SDK 1.3.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Stable channel release notes for the Windows App SDK 1.3

The stable channel provides releases of the Windows App SDK that are supported for use by apps in production environments. Apps that use the stable release of the Windows App SDK can also be published to the Microsoft Store.

**Important links**:

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Downloads for the Windows App SDK

> [!NOTE]
> The Windows App SDK Visual Studio Extensions (VSIX) are no longer distributed as a separate download. They are available in the Visual Studio Marketplace inside Visual Studio.

### Version 1.3.3 (1.3.230724000)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.3 release.

- Fixed an issue where the mouse would sometimes stop working when a dialog box was closed.
- Fixed a deployment issue that prevented apps from installing due to a mismatch of package versions on the system. For more information, see GitHub issue [#3740](https://github.com/microsoft/WindowsAppSDK/issues/3740).
- Fixed an issue affecting context menu positioning in Windows App SDK 1.3.
- Fixed an issue causing some WinUI3 apps, in some situations, to crash when the app was closed because XAML shut itself down too early.
- Fixed an issue where font icons were not mirroring properly in right-to-left languages. For more information, see GitHub issue [#7661](https://github.com/microsoft/microsoft-ui-xaml/issues/7661).
- Fixed an issue causing an app to crash on shutdown when resources were torn down in a bad order. For more information, see GitHub issue [#7924](https://github.com/microsoft/microsoft-ui-xaml/issues/7924).

### Version 1.3.2 (1.3.230602002)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.3 release.

- Fixed a crash when setting a Protected Cursor.
- Fixed a performance issue in XamlMetadataProvider during app startup. For more information, see GitHub issue [#8281](https://github.com/microsoft/microsoft-ui-xaml/issues/8281).
- Fixed an issue with hyperlinks and touch in a RichTextBlock. For more information, see GitHub issue [#6513](https://github.com/microsoft/microsoft-ui-xaml/issues/6513).
- Fixed an issue with scrolling and touchpads in WebView2. For more information, see GitHub issue [#7772](https://github.com/microsoft/microsoft-ui-xaml/issues/7772).
- Fixed an issue where an update of Windows App SDK sometimes required a restart of Visual Studio. For more information, see GitHub issue [#3554](https://github.com/microsoft/microsoft-ui-xaml/issues/3554).
- Fixed a noisy exception on shutdown when running in a debugger.

### Version 1.3.1 (1.3.230502000)

This is a servicing release of the Windows App SDK that includes critical bug fixes for the 1.3 release.

- Fixed issue causing apps to crash when setting the SystemBackdrop if the Content was null. For more information, see GitHub issue [#8416](https://github.com/microsoft/microsoft-ui-xaml/issues/8416).
- Fixed issue causing apps to crash when setting the Window Title in XAML, a new capability added in 1.3.0. For more information, see GitHub issue [#3689](https://github.com/microsoft/microsoft-ui-xaml/issues/3689).
- Fixed issue where a window incorrectly took focus when its content changed.
- Fixed an issue with creating C++ projects with the WinAppSDK 1.3 project templates.
- Updated templates on Visual Studio Marketplace

### New and updated features and known issues for version 1.3

The following sections describe new and updated features and known issues for version 1.3.

In an existing Windows App SDK 1.2 app, you can update your Nuget package to 1.3.230331000 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Latest Windows App SDK downloads](../downloads.md).

### XAML Backdrop APIs

With properties built in to the XAML Window, Mica & Background Acrylic backdrops are now easier to use in your WinUI 3 app.
See the [System Backdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.systembackdrop) and [Mica Backdrop](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.micabackdrop) API docs for more information about the Xaml Backdrop properties.

```csharp
public MainWindow()
{
    this.InitializeComponent();

    this.SystemBackdrop = new MicaBackdrop();
}
```

### Window.AppWindow

By replacing several lines of boilerplate code, you're now able to use AppWindow APIs directly from a **Window** through [`Window.AppWindow`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.window.appwindow).

### New features from across WinAppSDK

- `ApplicationModel.DynamicDependency`: `PackageDependency.PackageGraphRevisionId` that replaces the deprecated MddGetGenerationId.
- Environment Manager: [`EnvironmentManager.AreChangesTracked`](/windows/windows-app-sdk/api/winrt/microsoft.windows.system.environmentmanager.arechangestracked) to inform you whether changes to the environment manager are able to be tracked in your application.
- A new event, DebugSettings.XamlResourceReferenceFailed is now raised when a referenced Static/ThemeResource lookup can't be resolved. This event gives access to a trace that details where the framework searched for that key in order to better enable you to debug Static & ThemeResource lookup failures. For more information, see the [Tracing XAML resource reference lookup failures](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/xaml-resource-references-tracing-spec.md) API spec on GitHub.

### Other updates

- See our [WinAppSDK 1.3 milestone](https://github.com/microsoft/WindowsAppSDK/milestone/14?closed=1) on the [WinAppSDK GitHub](https://github.com/microsoft/WindowsAppSDK) for additional issues addressed in this release.
- See our [WinUI 3 in WinAppSDK 1.3 milestone](https://github.com/microsoft/microsoft-ui-xaml/milestone/18?closed=1) on the [microsoft-ui-xaml GitHub](https://github.com/microsoft/microsoft-ui-xaml) for additional issues addressed in this release.
- With the latest experimental VSIX, you're now able to convert your app between unpackaged and packaged through the Visual Studio menu instead of in your project file.

### Known issue

Due to a recent change to the xaml compiler, an existing project that upgrades to 1.3 may experience a build error like the following within Visual Studio:

```console
> C:\Users\user\\.nuget\packages\microsoft.windowsappsdk\\**1.3.230331000**\buildTransitive\Microsoft.UI.Xaml.Markup.Compiler.interop.targets(537,17): error MSB4064: The "PrecompiledHeaderFile" parameter is not supported by the "CompileXaml" task loaded from assembly: Microsoft.UI.Xaml.Markup.Compiler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=de31ebe4ad15742b from the path: C:\Users\user\\.nuget\packages\microsoft.windowsappsdk\\**1.2.230118.102**\tools\net472\Microsoft.UI.Xaml.Markup.Compiler.dll. Verify that the parameter exists on the task, the <UsingTask> points to the correct assembly, and it is a settable public instance property.
```

This is caused by Visual Studio using a cached xaml compiler task dll from 1.2, but driving it with incompatible MSBuild logic from 1.3, as seen in the error text above.  The workaround is to shut down Visual Studio, restart it, and reload the solution.

## Related topics

- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
