---
title: How to unpackage a WinUI app
description:  How to unpackage a WinUI app
ms.topic: how-to
ms.date: 12/15/2025
keywords: windows app sdk, winappsdk, winui
ms.localizationpriority: medium
content-type: how-to
#Customer intent: As a Windows developer, I want to learn how to create an unpackaged WinUI app.
---

# Unpackage a WinUI app

Packaging is an important part of any Windows App SDK project. For details on your options, see [Advantages and disadvantages of packaging your app](/windows/apps/package-and-deploy/#advantages-and-disadvantages-of-packaging-your-app).

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
