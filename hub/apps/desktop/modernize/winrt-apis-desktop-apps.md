---
description: Learn how to configure your WPF, Windows Forms, or Win32 desktop app to call Windows Runtime APIs and add modern Windows experiences.
title: Call Windows Runtime APIs in desktop apps
ms.date: 04/28/2026
ms.topic: how-to
keywords: windows 11, windows app sdk, winrt, desktop app
ms.localizationpriority: medium
zone_pivot_groups: desktop-framework
---

# Call Windows Runtime APIs in desktop apps

This article describes how to configure your desktop app projects to call Windows Runtime (WinRT) APIs — the APIs that power modern Windows features such as notifications, file pickers, sharing, and more.

> [!NOTE]
> Some WinRT APIs are not supported in desktop apps. For more information, see [Support for WinRT APIs in desktop apps](winrt-api-desktop-app-support.md).

:::zone pivot="dotnet"

## Configure a .NET project

<a id="net-6-and-later-use-the-target-framework-moniker-option"></a>
### .NET 6 and later: Use the Target Framework Moniker option

Specify a Windows OS version-specific [Target Framework Moniker](/dotnet/standard/frameworks) (TFM) in your project file. This adds a reference to the appropriate [Windows SDK targeting package](https://www.nuget.org/packages/Microsoft.Windows.SDK.NET.Ref) at build time.

1. In Visual Studio, select **Project > [*ProjectName*] Properties**.

1. On the properties page, find **Application > General** by selecting it in the left side navigation or scrolling to it.

1. Choose settings for:
    1. **Target framework** - This is initially set to the .NET version you selected when the project was created, but you can change it here.
    1. **Target OS** - Leave this set to Windows.
    1. **Target OS version** - Select the version for the API set you want to use, typically the latest.
    1. **Supported OS version** (Optional) - See the next section for more information.

:::image type="content" source="images/target-framework-settings-vs.png" alt-text="Target framework settings in Visual Studio.":::

In the Target Framework Moniker, these values translate like this:

- **Target framework** - `net10.0`
- **Target OS** - `-windows`
- **Target OS version** - `10.0.22621.0`

```xml
<TargetFramework>net10.0-windows10.0.22621.0</TargetFramework>
<!-- If set... -->
<SupportedOSPlatformVersion>10.0.19041.0</SupportedOSPlatformVersion>
```

You can also set the values by manually editing the project file.

1. In Visual Studio, right-click your project in **Solution Explorer** and choose **Edit Project File**.

2. Replace the **TargetFramework** value with a Windows-version-specific TFM.

    | Target | TFM |
    |--------|-----|
    | Windows 11, version 24H2 | `net10.0-windows10.0.26100.0` |
    | Windows 11, version 22H2 | `net10.0-windows10.0.22621.0` |
    | Windows 11 (initial release) | `net10.0-windows10.0.22000.0` |
    | Windows 10, version 2004 | `net10.0-windows10.0.19041.0` |
    | Windows 10, version 1903 | `net10.0-windows10.0.18362.0` |
    | Windows 10, version 1809 | `net10.0-windows10.0.17763.0` |

    > [!NOTE]
    > The values shown are for .NET 10. Update as needed for other versions of .NET: `net8.0`, `net9.0`.

    Example:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
      <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
      </PropertyGroup>
    </Project>
    ```
3. Save and close the project file.

> [!TIP]
> The version specified by the TFM indicates which APIs are available to your app. It doesn't control the OS version that your app supports at runtime. It's used to select the reference assemblies that your project compiles against, and to select assets from NuGet packages. Think of this version as the "platform version" or "OS API version" to disambiguate it from the runtime OS version. For more info, see [Target frameworks: OS version in TFMs](/dotnet/standard/frameworks#os-version-in-tfms).

#### Supporting a minimum Windows version

To allow your app to run on a Windows version older than your TFM target, set the **SupportedOSVersion** (`SupportedOSPlatformVersion`), as shown in the previous section. For more info, see [Target frameworks: Support older OS versions](/dotnet/standard/frameworks#support-older-os-versions).

When you target a range of OS versions, guard calls to APIs that aren't available on all versions using [ApiInformation](/uwp/api/windows.foundation.metadata.apiinformation) checks. For more information, see [Version adaptive apps](/windows/uwp/debug-test-perf/version-adaptive-apps).

When **SupportedOSVersion** is set, Visual Studio will give a warning for APIs that need a runtime check. For example, if the target version is 19041, and the minimum version is 17763, you will see a warning like this when you call [AppInfo.Current](/uwp/api/windows.applicationmodel.appinfo.current).

```console
CA1416	Using platform dependent API on a component makes the code no longer work across all platforms.

This call site is reachable on: 'Windows' 10.0.17763.0 and later. 'AppInfo.Current' is only supported on: 'Windows' 10.0.19041.0 and later.
```


```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
    <SupportedOSPlatformVersion>10.0.17763.0</SupportedOSPlatformVersion>
    <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
  </PropertyGroup>
</Project>
```
> [!NOTE]
> You can also manually add [TargetPlatformMinVersion](/dotnet/api/microsoft.build.utilities.sdkmanifest.targetplatformminversion), but this does not provide compile-time warnings in Visual Studio.

#### WinRT APIs not supported in .NET 6 and later

In .NET 6 and later, several WinRT APIs in the **Windows.UI** namespace are not supported. Use the equivalent APIs in the **Microsoft.UI** namespace (provided by the Windows App SDK) instead:

| Unsupported | Use instead |
|---|---|
| `Windows.UI.Colors` | [`Microsoft.UI.Colors`](/windows/windows-app-sdk/api/winrt/microsoft.ui.colors) |
| `Windows.UI.ColorHelper` | [`Microsoft.UI.ColorHelper`](/windows/windows-app-sdk/api/winrt/microsoft.ui.colorhelper) |
| `Windows.UI.Text` (most types) | [`Microsoft.UI.Text`](/windows/windows-app-sdk/api/winrt/microsoft.ui.text) |
| `Windows.UI.Xaml` (all types) | [`Microsoft.UI.Xaml`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml) |

### .NET Core 3.x or .NET Framework: Install the NuGet package

If your app targets .NET Core 3.x or .NET Framework, install the [`Microsoft.Windows.SDK.Contracts`](https://www.nuget.org/packages/Microsoft.Windows.SDK.Contracts) NuGet package:

1. In Visual Studio, right-click your project and choose **Manage NuGet Packages**.
2. Search for `Microsoft.Windows.SDK.Contracts`.
3. Select the package version matching your minimum Windows target:

    | Package version | Windows target |
    |---|---|
    | 10.0.19041.xxxx | Windows 10, version 2004 |
    | 10.0.18362.xxxx | Windows 10, version 1903 |
    | 10.0.17763.xxxx | Windows 10, version 1809 |
    | 10.0.17134.xxxx | Windows 10, version 1803 |

4. Click **Install**.

### Multi-targeting .NET 6+ and earlier .NET versions

Configure the project file to use the TFM approach for .NET 6+ and the NuGet package for earlier versions:

```xml
<Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFrameworks>netcoreapp3.1;net8.0-windows10.0.19041.0</TargetFrameworks>
    <UseWPF>true</UseWPF>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Condition="'$(TargetFramework)' == 'netcoreapp3.1'"
                     Include="Microsoft.Windows.SDK.Contracts"
                     Version="10.0.19041.0"/>
  </ItemGroup>
</Project>
```
#### Conditional compilation

<a id="conditional-compilation"></a>

When multi-targeting across .NET 6+ and earlier versions, use conditional compilation to write version-specific code in a single project. For more information, see [Target frameworks: Preprocessor symbols](/dotnet/standard/frameworks#preprocessor-symbols).

```csharp
#if NET6_0_OR_GREATER
    // Code that uses .NET 6+ APIs or TFM-available WinRT APIs
#else
    // Fallback code for .NET Core 3.x / .NET Framework
#endif
```
:::zone-end

:::zone pivot="win32"


## Configure a C++ (Win32) project

Use [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) to consume WinRT APIs from C++ desktop apps.

- Install the [Microsoft.Windows.CppWinRT](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) NuGet package.
- Because C++/WinRT uses features from the C++17 standard, ensure the project property **C/C++ > Language > C++ Language Standard** is set to **ISO C++17 Standard (/std:c++17)** or later in Visual Studio.

For more details, see [Visual Studio support for C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

:::zone-end

## Next steps

You can now call [Windows Runtime (WinRT) APIs](/uwp/api) from the _Windows SDK_.

To also call WinRT APIs from the _Windows App SDK_, see [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).

Some WinRT APIs require package identity. For more info see:

- [Packaging overview](/windows/apps/package-and-deploy/packaging/)
- [Features that require package identity](modernize-packaged-apps.md)
- [Package your app using single-project MSIX](../../windows-app-sdk/single-project-msix.md)
- [Grant identity to a non-packaged app](grant-identity-to-nonpackaged-apps-overview.md)

## Related content

- [Windows App SDK supported Windows releases](../../windows-app-sdk/support.md)
- [Support for WinRT APIs in desktop apps](winrt-api-desktop-app-support.md)
- [Modernize your desktop apps](index.md)
