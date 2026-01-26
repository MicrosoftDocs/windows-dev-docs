---
description: Learn how to modernize your UWP app with .NET 10 and Native AOT for improved performance, modern features, and better tooling.
title: Modernize your UWP app with .NET and Native AOT
ms.date: 01/26/2026
ms.topic: how-to
keywords: windows, uwp, .net 10, native aot, dotnet, modernization
ms.localizationpriority: high
---

# Modernize your UWP app with .NET and Native AOT

UWP support for modern .NET is now generally available, providing a path for existing UWP developers to modernize their apps with the latest .NET and Native AOT compilation. Visual Studio 2026 includes built-in project templates to create new UWP applications, class libraries, and Windows Runtime components using .NET 10.

## Why modernize to modern .NET?

Upgrading your UWP app from .NET Native to .NET 10 with Native AOT provides several key benefits:

### Access to Modern .NET Features
- **Latest .NET and C# features**: Use .NET 10 with all modern language features and APIs
- **Active development**: Unlike .NET Native which only receives security updates and is limited to .NET Core 2.0 feature level, .NET 10 is actively developed
- **Better library compatibility**: Reference newer versions of NuGet packages that only support .NET 6 and above

### Improved Developer Experience
- **Faster build times**: Significantly faster compilation compared to .NET Native
- **Better debugging support**: Enhanced debugging tools and diagnostic capabilities for AOT and trimming issues
- **SDK-style project files**: Modern, clean .csproj files without verbose legacy-style configurations
- **XAML Hot Reload**: Full support for XAML and C# Hot Reload during development
- **Better tooling**: IntelliSense, Live Preview, and XAML Diagnostics work seamlessly

### Performance Benefits
- **Native AOT compilation**: Similar or better startup performance compared to .NET Native
- **Optimized runtime**: Performance improvements through inter-procedural optimizations
- **Static validation**: Catch AOT compatibility issues at build time with analyzers and annotations

### Incremental Migration Path to WinUI 3
Rather than migrating both the .NET runtime and UI framework simultaneously, you can now take an incremental approach:
1. First, migrate to .NET 10 and validate Native AOT compatibility
2. Then, separately migrate from UWP XAML to WinUI 3 and the Win32 app model

This two-step approach reduces risk and makes the migration more manageable.

## Prerequisites

To develop UWP apps with .NET 10, you need:

- **Visual Studio 2026**
- **Universal Windows Platform tools** workload
- **Windows SDK 10.0.26100.0 or later**

### Installation Steps

1. Open the Visual Studio Installer
2. Under **Workloads** > **Desktop & Mobile**, select the **Windows application development** workload
3. Under **Optional** (in the right pane), select:
   - **Universal Windows Platform tools** - Contains all tooling for UWP apps
   - **Windows 11 SDK (10.0.26100.0)** - Required to build UWP XAML apps

## Creating a New UWP .NET 10 Project

Visual Studio includes several project templates for UWP with .NET 10:

- **Blank UWP App**: Standard UWP XAML application with single-project MSIX packaging
- **Blank UWP CoreApplication App**: For advanced scenarios with Composition/DirectX content (no XAML)
- **UWP Windows Runtime Component**: Managed WinRT component using latest .NET and CsWinRT
- **UWP Class Library**: Class library with XAML support

### Create a New Project

1. In Visual Studio, select **File** > **New** > **Project**
2. Filter by **C#** and **UWP** in the project type dropdown
3. Select **Blank UWP App** template
4. Enter your project name and select **Create**
5. Choose your target and minimum Windows versions

## Understanding UWP .NET 10 Projects

UWP .NET 10 projects use modern SDK-style .csproj files with key properties:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
    <TargetPlatformMinVersion>10.0.19041.0</TargetPlatformMinVersion>
    <UseUwp>true</UseUwp>
    <UseUwpTools>true</UseUwpTools>
    <EnableMsixTooling>true</EnableMsixTooling>
    <PublishAot>true</PublishAot>
    <DisableRuntimeMarshalling>true</DisableRuntimeMarshalling>
  </PropertyGroup>
</Project>
```

### Key Properties Explained

- **UseUwp**: References WinRT projections for Windows.UI.Xaml types and configures CsWinRT for UWP compatibility
- **UseUwpTools**: Enables UWP-specific tooling including XAML compiler, project capabilities, and MSIX packaging
- **EnableMsixTooling**: Enables single-project MSIX support (no separate packaging project needed)
- **PublishAot**: Enables Native AOT compilation (required for Microsoft Store publication)
- **DisableRuntimeMarshalling**: Optimizes performance for Native AOT scenarios

> [!IMPORTANT]
> Native AOT is the only supported way for publishing UWP apps targeting modern .NET to the Microsoft Store. This ensures fast startup times and meets the Store's native code requirement for UWP apps.

## Migrating Existing UWP Apps to .NET 10

To migrate an existing UWP app from .NET Native to .NET 10:

### Step 1: Update Project File

1. Convert your existing .csproj to SDK-style format
2. Add the required properties (`UseUwp`, `UseUwpTools`, `EnableMsixTooling`, `PublishAot`)
3. Update NuGet package references to versions compatible with .NET 10

### Step 2: Address Native AOT Compatibility

Native AOT requires all code to be AOT-compatible. Common issues include:

- **Reflection usage**: Add appropriate attributes or use source generators
- **Dynamic code generation**: Replace with compile-time alternatives
- **Third-party libraries**: Ensure all dependencies support Native AOT

Enable AOT analyzers to catch issues at build time:

```xml
<PropertyGroup>
  <IsAotCompatible>true</IsAotCompatible>
  <EnableTrimAnalyzer>true</EnableTrimAnalyzer>
  <EnableSingleFileAnalyzer>true</EnableSingleFileAnalyzer>
</PropertyGroup>
```

### Step 3: Test Thoroughly

1. Build your app in Release mode with Native AOT enabled
2. Test all functionality - AOT-compiled code behaves identically to Debug builds when properly annotated
3. Resolve any trim or AOT warnings before publishing

For more information on Native AOT compatibility, see [Introduction to AOT warnings](/dotnet/core/deploying/native-aot/fixing-warnings) and [Prepare .NET libraries for trimming](/dotnet/core/deploying/trimming/prepare-libraries-for-trimming).

## Native AOT vs .NET Native

While both compile your app to native code, Native AOT differs from .NET Native in important ways:

### Static Validation
- **Native AOT**: Uses static analysis with code annotations and analyzers to validate AOT compatibility at build time
- **.NET Native**: Uses heuristics and fallback logic that can lead to runtime crashes difficult to debug

### Performance & Size
- **Startup Performance**: Native AOT provides similar or better startup performance (~5% improvement in benchmarks)
- **Binary Size**: Expect ~4MB increase for smaller apps due to self-contained deployment (no shared framework package)
- **Runtime Performance**: Better performance for backend code through inter-procedural optimizations

### Developer Experience
- **Native AOT**: Problems surface during development with clear error messages and debugging support
- **.NET Native**: Slow build times, differences between Debug/Release, and difficult-to-diagnose runtime issues

## Publishing to Microsoft Store

When publishing UWP apps with modern .NET to the Microsoft Store:

1. Build in Release configuration with `PublishAot` enabled
2. Ensure no AOT or trim warnings are present
3. Create MSIX package as usual
4. Upload to Partner Center

> [!NOTE]
> You can ignore Windows App Certification Kit (WACK) failures related to "unsupported Win32 APIs." Partner Center no longer performs strict Win32 API validation for UWP apps. AppContainer security handles runtime permissions instead.

## Advanced Scenarios

### UWP XAML Islands

With modern .NET, you can host UWP XAML controls inside Win32 apps (WinForms, WPF, WinUI 3) using a single .NET 10 project. This enables:

- Hosting UWP controls like MapControl in full-trust packaged apps
- Single-project solution combining Win32 app and UWP components
- Single native binary without separate build toolchains

### Using Latest NuGet Packages

Modern .NET support enables you to reference modern NuGet packages that require .NET 6+, removing the limitations of .NET Native's .NET Standard 2.0 constraint.

## Additional Resources

- [Introduction to Native AOT deployment](/dotnet/core/deploying/native-aot/)
- [Prepare .NET libraries for trimming](/dotnet/core/deploying/trimming/prepare-libraries-for-trimming)
- [Create a "Hello, World!" UWP app with .NET 10](../get-started/create-a-hello-world-app-xaml-universal.md)
- [Windows App SDK and WinUI 3](/windows/apps/windows-app-sdk/) - Recommended for new Windows apps

## See Also

- [.NET Native (legacy documentation)](index.md)
- [Windows App SDK migration guidance](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy)
- [MSIX packaging](/windows/msix/)
