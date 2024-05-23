---
title: Preview release channel for the Windows App SDK 1.3
description: Provides info about the preview release channel for the Windows App SDK 1.3.
ms.topic: article
ms.date: 04/25/2024
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Preview channel release notes for the Windows App SDK 1.3

> [!IMPORTANT]
> The preview channel is **not supported** for use in production environments, and apps that use the preview releases cannot be published to the Microsoft Store.

The preview channel includes releases of the Windows App SDK with [preview channel features](../release-channels.md#features-available-by-release-channel) in late stages of development. Preview releases do not include experimental features and APIs but may still be subject to breaking changes before the next stable release.

**Important links:**

- If you'd like to upgrade an existing app from an older version of the Windows App SDK to a newer version, see [Update existing projects to the latest release of the Windows App SDK](../update-existing-projects-to-the-latest-release.md).
- For documentation on preview releases, see [Install tools for preview and experimental channels of the Windows App SDK](../preview-experimental-install.md).

**Latest preview channel release:**

- [Latest preview channel release notes for the Windows App SDK](../preview-channel.md)

**Latest stable channel release:**

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)

## Version 1.3 Preview 1 (1.3.0-preview1)

This is the latest release of the preview channel for version 1.3. This release includes previews for new features across WinAppSDK and several performance, security, accessibility and reliability bug fixes.

In an existing Windows App SDK 1.2 (from the stable channel) app, you can update your Nuget package to 1.3.0-preview1 (see the **Update a package** section in [Install and manage packages in Visual Studio using the NuGet Package Manager](/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).

For the updated runtime and MSIX, see [Latest Windows App SDK downloads](../downloads.md).

### XAML Backdrop APIs

With properties built in to the XAML Window, Mica & Background Acrylic backdrops are now easier to use in your WinUI 3 app.

See the [Xaml Backdrop API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/33541da536673fa360212e94e4a6ac896b8b49fb/specs/xaml-backdrop-api.md?plain=1#L39) on GitHub for more information about the **Window.SystemBackdrop** property.

```csharp
public MainWindow()
{
    this.InitializeComponent();

    this.SystemBackdrop = new MicaBackdrop();
}
```

### Window.AppWindow

Replacing several lines of boilerplate code, you're now able to use AppWindow APIs directly from an **Window** through `Window.AppWindow`. See the [Window.AppWindow API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/appwindow-spec.md) on GitHub for additional background and usage information.

### New features from across WinAppSDK

- `ApplicationModel.DynamicDependency`: `PackageDependency.PackageGraphRevisionId` that replaces the deprecated MddGetGenerationId.
- Environment Manager: `EnvironmentManager.AreChangesTracked` to inform you whether changes to the environment manager are able to be tracked in your application. See the [Environment Manager API spec](https://github.com/microsoft/WindowsAppSDK/blob/main/specs/AppLifecycle/EnvironmentVariables/AppLifecycle%20-%20Environment%20Variables%20(EV).md) on GitHub for more information.
- MRT Core: A new event, `Application.ResourceManagerInitializing` allows your app to provide its own implementation of the `IResourceManager` interface, and gives you access to the ResourceManager that WinUI uses to resolve resource URIs. See the [IResourceManager API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/33541da536673fa360212e94e4a6ac896b8b49fb/specs/custom-iresourcemanager-spec.md) on GitHub for more information.
- With the latest experimental VSIX, you're now able to convert your app between unpackaged and packaged through the Visual Studio menu instead of in your project file.
- A new event, `DebugSettings.XamlResourceReferenceFailed` is now raised when a referenced Static/ThemeResource lookup can't be resolved. This event gives access to a trace that details where the framework searched for that key in order to better enable you to debug Static & ThemeResource lookup failures. For more information, see the [API spec](https://github.com/microsoft/microsoft-ui-xaml/blob/main/specs/xaml-resource-references-tracing-spec.md) and issues [4972](https://github.com/microsoft/microsoft-ui-xaml/issues/4972), [2350](https://github.com/microsoft/microsoft-ui-xaml/issues/2350), and [6073](https://github.com/microsoft/microsoft-ui-xaml/issues/6073) on GitHub.
- Deployment: To manage and repair the Windows App Runtime, `DeploymentRepairOptions` is now available as part of the `DeploymentManager`. For more information, see the Repair section of the [Deployment API Spec](https://github.com/microsoft/WindowsAppSDK/blob/user/sachinta/DeploymentRepairAPISpec/specs/Deployment/DeploymentAPI.md#repair) on GitHub.

### Known issues

- The Pivot control causes a runtime crash with a XAML parsing error. See issue [#8160](https://github.com/microsoft/microsoft-ui-xaml/issues/8160) on GitHub for more info.
- When the DatePicker or TimePicker flyout is opened, the app crashes.
- The `WindowsAppRuntime.ReleaseInfo` and `WindowsAppRuntime.RuntimeInfo` APIs introduced in 1.3 releases are not yet supported as they contain a critical bug.

## Related topics

- [Latest stable channel release notes for the Windows App SDK](../stable-channel.md)
- [Latest experimental channel release notes for the Windows App SDK](../experimental-channel.md)
- [Install tools for the Windows App SDK](../set-up-your-development-environment.md)
- [Create your first WinUI 3 (Windows App SDK) project](../../winui/winui3/create-your-first-winui3-app.md)
- [Use the Windows App SDK in an existing project](../use-windows-app-sdk-in-existing-project.md)
- [Deployment overview](../../package-and-deploy/index.md#use-the-windows-app-sdk)
