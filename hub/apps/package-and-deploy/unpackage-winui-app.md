---
title: Distribute an unpackaged WinUI 3 app
description: Learn how to set WindowsPackageType=None for unpackaged WinUI 3 distribution, understand the runtime deployment options, and review the key limitations before you start.
ms.topic: how-to
ms.date: 05/28/2026
keywords: windows app sdk, winappsdk, winui, unpackaged, WindowsPackageType, bootstrapper, self-contained, PublishSingleFile, single-file exe
ms.localizationpriority: medium
content-type: how-to
#Customer intent: As a Windows developer, I want to distribute my WinUI 3 app without MSIX packaging and understand the limitations and runtime requirements.
---

# Distribute an unpackaged WinUI 3 app

Unpackaged distribution lets you ship a WinUI 3 app without MSIX — useful for enterprise scenarios where MSIX deployment isn't available, or for developers who prefer a traditional folder-based install.

> [!IMPORTANT]
> **Review these limitations before you start.** Unpackaged WinUI 3 apps have constraints that affect your distribution strategy:
>
> - **Single-file EXE** — Unpackaged, self-contained WinUI 3 apps support `PublishSingleFile` (Windows App SDK 1.5 and later). This produces a single distributable EXE; dependencies are extracted to a temp directory at first launch. Specific MSBuild properties are required — see [Single-file EXE](#single-file-exe) below. Framework-dependent apps and packaged apps do not support `PublishSingleFile`.
> - **Runtime dependency** — The Windows App SDK runtime must be present on the user's machine. You must either bundle the runtime installer with your app, or use self-contained deployment (which significantly increases output size). See [Deploying the Windows App SDK runtime](#deploying-the-windows-app-sdk-runtime) below.
> - **No package identity** — Without a package manifest, your app cannot use manifest-based Windows features: no automatic updates via App Installer or Store, no background task registration, and no file type associations or Start menu tile customization via package manifest. (Traditional Win32 mechanisms such as installer-written registry entries and shortcuts still work.)
> - **No MSIX/package-identity Store submission** — This distribution model has no package identity; it is not eligible as an MSIX submission to the Microsoft Store. (You can submit a traditional installer to the Store via the [MSI or EXE installer submission path](../publish/publish-your-app/msi/upload-app-packages.md), but that is a separate workflow from what this article describes.)
>
> If these constraints are a concern, consider [packaging your app](index.md) (recommended for most apps) or [packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md) to add package identity without a full MSIX conversion.

For details on all packaging options, see [Advantages and disadvantages of packaging your app](/windows/apps/package-and-deploy/#advantages-and-disadvantages-of-packaging-your-app).

If you choose to unpackage a new or existing WinUI app, follow these steps:

## [C#](#tab/csharp)

In your `.csproj` file, find the first existing **PropertyGroup** element, which also contains `OutputType`, `TargetFramework`, and other properties.

- Add the `WindowsPackageType` project property to this **PropertyGroup** element. Set its value to `None`.

```xml
<Project ...>
  ...
  <PropertyGroup>
    <WindowsPackageType>None</WindowsPackageType><!-- add this -->
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
    ...
  </PropertyGroup> 
  ...
</Project>
```

To start the app from Visual Studio (either **Debugging** or **Without Debugging**), select the _Unpackaged_ launch profile from the **Start** drop-down. If the _Package_ profile is selected, then you'll see a deployment error in Visual Studio. This step isn't necessary if you start the application (`.exe`) from the command line or from Windows File Explorer.
  
:::image type="content" source="images/visual-studio-csharp-launch-profile.png" alt-text="Visual Studio - Start drop-down with C# application unpackaged launch profile highlighted":::

## [C++](#tab/cpp)

In your `.vcxproj` file, find the **PropertyGroup** element with `Label="Globals"`.

- Change the value of the `AppxPackage` property from `true` to `false`.
- Add the `WindowsPackageType` project property. Set its value to `None`.

```xml
<Project ...>
  ...
  <PropertyGroup Label="Globals">
    ...
    <AppxPackage>false</AppxPackage><!-- update this -->
    <WindowsPackageType>None</WindowsPackageType><!-- add this -->
    ...
  </PropertyGroup> 
  ...
</Project>
```

The debug Start drop-down in Visual Studio changes from **Local Machine** to **Local Windows Debugger**.

:::image type="content" source="images/visual-studio-cpp-launch-profile.png" alt-text="Visual Studio - Debug Start drop-down displaying the text local windows debugger.":::

---

### The bootstrapper API

Setting the `<WindowsPackageType>None</WindowsPackageType>` project property causes the *auto-initializer* to locate and load a version of the Windows App SDK that's most appropriate for your app.

If you have advanced needs (such as custom error handling, or to load a specific version of the Windows App SDK), then you can instead call the bootstrapper API explicitly. For more info, see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time), and [Tutorial: Use the bootstrapper API in an app packaged with external location or unpackaged that uses the Windows App SDK](/windows/apps/windows-app-sdk/tutorial-unpackaged-deployment).

For more info about the bootstrapper, see [Deployment architecture and overview for framework-dependent apps](/windows/apps/windows-app-sdk/deployment-architecture#bootstrapper).

## Deploying the Windows App SDK runtime

Unpackaged WinUI 3 apps depend on the Windows App SDK runtime being installed on the user's machine. You have two options for ensuring the runtime is present:

**Option 1: Windows App SDK runtime installer (.exe) (recommended)**

Include the Windows App SDK runtime installer alongside your app. The runtime installer is a redistributable `.exe` that installs the required Windows App SDK runtime packages. Download it from the [Windows App SDK releases page](https://github.com/microsoft/WindowsAppSDK/releases) and bundle it with your own installer or setup script. For full guidance, see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](/windows/apps/windows-app-sdk/use-windows-app-sdk-run-time).

Users must run the runtime installer once. Subsequent app updates don't require reinstallation of the runtime unless the required Windows App SDK version changes.

**Option 2: Self-contained deployment**

Set `<WindowsAppSDKSelfContained>true</WindowsAppSDKSelfContained>` in your project file to bundle the Windows App SDK runtime directly into your app's output folder. This removes the runtime dependency — users don't need to install anything separately.

```xml
<PropertyGroup>
  <WindowsPackageType>None</WindowsPackageType>
  <WindowsAppSDKSelfContained>true</WindowsAppSDKSelfContained>
</PropertyGroup>
```

The trade-off: your output folder is significantly larger (the full runtime is included), and each app update carries the full runtime payload. Use this option for simple distribution scenarios or when you can't control what's installed on the target machine.

→ [Deploy unpackaged apps that use the Windows App SDK](/windows/apps/windows-app-sdk/deploy-unpackaged-apps) for the complete runtime deployment reference.

## Single-file EXE

Starting with Windows App SDK 1.5, **unpackaged, self-contained** WinUI 3 apps support the .NET `PublishSingleFile` deployment model. This produces a single distributable EXE file — all dependencies are bundled into the EXE and extracted to a temp directory at first launch.

> [!IMPORTANT]
> `PublishSingleFile` is **not** supported for packaged apps (MSIX or packaged with external location) or for framework-dependent apps. Both conditions — unpackaged **and** self-contained — are required.

### Required MSBuild properties

The Windows App SDK includes a build-time validation target ([`WindowsAppSDKSingleFileVerifyConfiguration`](project-properties.md)) that checks your project configuration when `PublishSingleFile` is set. The following properties are required:

```xml
<PropertyGroup>
  <WindowsPackageType>None</WindowsPackageType>
  <WindowsAppSDKSelfContained>true</WindowsAppSDKSelfContained>
  <SelfContained>true</SelfContained>
  <EnableMsixTooling>true</EnableMsixTooling>
  <IncludeAllContentForSelfExtract>true</IncludeAllContentForSelfExtract>
  <PublishSingleFile>true</PublishSingleFile>
</PropertyGroup>
```

The build emits **errors** if `EnableMsixTooling`, `WindowsPackageType=None`, or `IncludeAllContentForSelfExtract` are missing, and **warnings** if `WindowsAppSDKSelfContained` or `SelfContained` are absent.

> [!NOTE]
> **Extraction behavior:** `IncludeAllContentForSelfExtract=true` means dependencies are extracted to a temp directory on the user's machine at first launch — the app is not a zero-extraction binary. The single EXE is convenient to distribute, but the extracted files must be present to run. The `WindowsAppSdkUndockedRegFreeWinRTInitialize` auto-initializer handles locating the extracted runtime; if you opt out of it, you must set the `MICROSOFT_WINDOWSAPPRUNTIME_BASE_DIRECTORY` environment variable to `AppContext.BaseDirectory` before program entry.

### Alternatives if single-file extraction is not acceptable

If you need a zero-extraction single binary, or if extraction behavior is not acceptable in your deployment environment, consider:

- **Use MSIX packaging** — users get a single installer experience (App Installer handles all the files), and you get Store eligibility, package identity, and built-in updates
- **Use a traditional installer (WiX, Inno Setup)** — wrap the output folder in a single EXE installer that extracts and installs all required files transparently
- **Use a different framework** — WPF and WinForms apps support `PublishSingleFile` with a broader set of configurations

## Distribution considerations for unpackaged apps

Unpackaged WinUI 3 apps lack package identity, which means they cannot access certain Windows features:

- No automatic update via App Installer or Windows Store
- No background task registration via package manifest
- No file type associations or protocol handlers via package manifest
- No Start menu tile customization via package manifest

If you need these features, consider [packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md) as a middle path that adds package identity without requiring full MSIX conversion.

→ [Publish your first Windows app](publish-first-app.md) for a full overview of distribution options for WinUI 3 and other Windows app frameworks.
