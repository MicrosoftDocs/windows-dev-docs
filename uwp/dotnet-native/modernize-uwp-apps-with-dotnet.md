---
description: Learn how to modernize your UWP app with the latest .NET and Native AOT for improved performance, modern features, and better tooling.
title: Modernize your UWP app with .NET and Native AOT
ms.date: 01/26/2026
ms.topic: how-to
keywords: windows, uwp, .net, native aot, dotnet, modernization
ms.localizationpriority: high
---

# Modernize your UWP app with .NET and Native AOT

UWP support for modern .NET is now generally available and is the **default project type for C# UWP apps in Visual Studio 2026**. Visual Studio 2026 includes built-in project templates to create new UWP applications, class libraries, and Windows Runtime components using the [latest .NET](https://dotnet.microsoft.com).

> [!NOTE]
> This article refers to [.NET Native](index.md), the legacy precompilation technology for UWP apps. .NET Native will continue to receive security and reliability fixes but will not receive new feature updates. If you're currently using .NET Native, this guide will help you understand the benefits of migrating to modern .NET with Native AOT.

## Why modernize to modern .NET?

Upgrading your UWP app from [.NET Native](index.md) to the [latest .NET](https://dotnet.microsoft.com) with Native AOT provides several key benefits:

### Access to Modern .NET Features
- **Latest .NET and C# features**: Use the [latest .NET](https://dotnet.microsoft.com) with all modern language features and APIs
- **Active development**: [.NET Native](index.md) will continue to receive security and reliability fixes but will not receive new feature updates. Modern .NET is actively developed with regular feature releases
- **Better library compatibility**: Reference newer versions of NuGet packages that only support .NET 6 and above

### Improved Developer Experience
- **Faster build times**: Significantly faster compilation compared to [.NET Native](index.md)
- **Better debugging support**: Enhanced debugging tools and diagnostic capabilities for AOT and trimming issues
- **SDK-style project files**: Modern, clean .csproj files without verbose legacy-style configurations
- **XAML Hot Reload**: Full support for XAML and C# Hot Reload during development
- **Better tooling**: IntelliSense, Live Preview, and XAML Diagnostics work seamlessly

### Performance Benefits
- **Native AOT compilation**: Similar or better startup performance compared to [.NET Native](index.md)
- **Optimized runtime**: Performance improvements through inter-procedural optimizations
- **Static validation**: Catch AOT compatibility issues at build time with analyzers and annotations

### Incremental Migration Path to WinUI 3
Rather than migrating both the .NET runtime and UI framework simultaneously, you can now take an incremental approach:
1. First, migrate to the latest .NET and validate Native AOT compatibility
2. Then, separately migrate from UWP XAML to WinUI 3 and the Win32 app model

This two-step approach reduces risk and makes the migration more manageable.

## Prerequisites

To develop UWP apps with modern .NET, you need:

- **Visual Studio 2026**
- **Universal Windows Platform tools** workload
- **Windows SDK 10.0.26100.0 or later**

### Installation Steps

1. Open the Visual Studio Installer
2. Under **Workloads** > **Desktop & Mobile**, select the **WinUI application development** workload
3. Under **Optional** (in the right pane), select:
   - **Universal Windows Platform tools** - Contains all tooling for UWP apps
   - **Windows 11 SDK (10.0.26100.0)** - Required to build UWP XAML apps

## Creating a New UWP Project with Modern .NET

Visual Studio 2026 includes several project templates for UWP with the latest .NET. **The default C# UWP project templates now target modern .NET** instead of .NET Native:

- **Blank UWP App**: Standard UWP XAML application with single-project MSIX packaging
- **Blank UWP CoreApplication App**: For advanced scenarios with Composition/DirectX content (no XAML)
- **UWP Windows Runtime Component**: Managed WinRT component using latest .NET and CsWinRT
- **UWP Class Library**: Class library with XAML support

> [!TIP]
> Legacy .NET Native templates (marked as ".NET Native") are still available for compatibility, but the modern .NET templates are recommended for all new development.

### Create a New Project

1. In Visual Studio, select **File** > **New** > **Project**
2. Filter by **C#** and **UWP** in the project type dropdown
3. Select **Blank UWP App** template
4. Enter your project name and select **Create**
5. Choose your target and minimum Windows versions

## Understanding UWP Modern .NET Projects

UWP modern .NET projects use SDK-style .csproj files with key properties:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows10.0.26100.0</TargetFramework> <!-- Use the latest supported .NET version -->
    <TargetPlatformMinVersion>10.0.19041.0</TargetPlatformMinVersion>
    <UseUwp>true</UseUwp>
    <EnableMsixTooling>true</EnableMsixTooling>
    <PublishAot>true</PublishAot>
    <DisableRuntimeMarshalling>true</DisableRuntimeMarshalling>
  </PropertyGroup>
</Project>
```

### Key Properties Explained

- **UseUwp**: References WinRT projections for Windows.UI.Xaml types and configures CsWinRT for UWP compatibility
- **UseUwpTools**: Enables UWP-specific tooling including XAML compiler, project capabilities, and MSIX packaging.  Note this property is enabled by default when `UseUwp` is enabled.
- **EnableMsixTooling**: Enables single-project MSIX support (no separate packaging project needed)
- **PublishAot**: Enables Native AOT compilation
- **DisableRuntimeMarshalling**: Optimizes performance for Native AOT scenarios

## Migrating Existing UWP Apps to Modern .NET

To migrate an existing UWP app from [.NET Native](index.md) to modern .NET:

### Step 1: Update Project File

1. Convert your existing .csproj to SDK-style format
2. Add the required properties - `UseUwp`, `EnableMsixTooling`, then `PublishAot` or `SelfContained`
3. Update NuGet package references to versions compatible with the latest .NET

### Step 2: Address Native AOT Compatibility

Native AOT requires all code to be AOT-compatible. Common issues include:

- **Reflection usage**: Add appropriate attributes or use source generators
- **Dynamic code generation**: Replace with compile-time alternatives
- **Third-party libraries**: Ensure all dependencies support Native AOT

For more information on AOT compatibility:
- [Fixing trim warnings](/dotnet/core/deploying/trimming/fixing-warnings)
- [Known trimming incompatibilities](/dotnet/core/deploying/trimming/incompatibilities)
- [Intrinsic APIs marked RequiresDynamicCode](/dotnet/core/deploying/native-aot/intrinsic-requiresdynamiccode-apis)

Application code projects:

```xml
<PropertyGroup>
  <PublishAot>true</PublishAot>
</PropertyGroup>
```

The following can be set in library projects:

```xml
<PropertyGroup>
  <IsAotCompatible>true</IsAotCompatible>
</PropertyGroup>
```

If your existing app uses a [runtime directives (rd.xml) file](runtime-directives-rd-xml-configuration-file-reference.md) for .NET Native, you'll need to address reflection and trimming requirements differently with Native AOT using attributes and analyzers instead.

> [!TIP]
> Use `[GeneratedBindableCustomProperty]` on classes that need `{Binding}` in XAML. These classes should be marked as `partial`.

### Step 3: Test Thoroughly

1. Build your app in Release mode with Native AOT enabled
2. Test all functionality - AOT-compiled code behaves identically to Debug builds when properly annotated
3. Resolve any trim or AOT warnings before publishing

For more information on Native AOT compatibility, see [Introduction to AOT warnings](/dotnet/core/deploying/native-aot/fixing-warnings) and [Prepare .NET libraries for trimming](/dotnet/core/deploying/trimming/prepare-libraries-for-trimming).

## Native AOT vs .NET Native

While both compile your app to native code, Native AOT differs from [.NET Native](index.md) in important ways:

### Static Validation
- **Native AOT**: Uses static analysis with code annotations and analyzers to validate AOT compatibility at build time
- **[.NET Native](index.md)**: Uses heuristics and fallback logic that can lead to runtime crashes difficult to debug

### Performance & Size
- **Startup Performance**: Native AOT provides similar or better startup performance (~5% improvement in benchmarks)
- **Binary Size**: Expect ~4MB increase for smaller apps due to self-contained deployment (no shared framework package)
- **Runtime Performance**: Better performance for backend code through inter-procedural optimizations

### Developer Experience
- **Native AOT**: Problems surface during development with clear error messages and debugging support
- **[.NET Native](index.md)**: Slow build times, differences between Debug/Release, and difficult-to-diagnose runtime issues

### Reflection and Metadata
- **Native AOT**: Uses compile-time attributes like `[DynamicallyAccessedMembers]` and source generators to handle reflection
- **[.NET Native](index.md)**: Uses [runtime directives (rd.xml) files](runtime-directives-rd-xml-configuration-file-reference.md) to specify metadata requirements at build time

For more information on .NET Native limitations, see [Getting Started with .NET Native](getting-started-with-net-native.md) and [.NET Native and Compilation](net-native-and-compilation.md).

## Publishing to Microsoft Store

When publishing UWP apps with modern .NET to the Microsoft Store:

1. Build in Release configuration with either `PublishAot` or `SelfContained` enabled
2. Ensure no AOT or trim warnings are present
3. Create MSIX package as usual
4. Upload to Partner Center

> [!NOTE]
> You can ignore Windows App Certification Kit (WACK) failures related to "unsupported Win32 APIs." Partner Center no longer performs strict Win32 API validation for UWP apps. AppContainer security handles runtime permissions instead.

## Advanced Scenarios

### UWP XAML Islands

With modern .NET, you can host UWP XAML controls inside Win32 apps (WinForms, WPF, WinUI 3). This enables:

- Hosting UWP controls like MapControl in full-trust packaged apps
- Single-project solution combining Win32 app and UWP components
- Single native binary without separate build toolchains

### Using Latest NuGet Packages

Modern .NET support enables you to reference modern NuGet packages that require .NET 6+, removing the limitations of [.NET Native's .NET Standard 2.0 constraint](net-native-and-compilation.md).

## Additional Resources

- [Introduction to Native AOT deployment](/dotnet/core/deploying/native-aot/)
- [Prepare .NET libraries for trimming](/dotnet/core/deploying/trimming/prepare-libraries-for-trimming)
- [Create a "Hello, World!" UWP app](../get-started/create-a-hello-world-app-xaml-universal.md)
- [Windows App SDK and WinUI 3](/windows/apps/windows-app-sdk/) - Recommended for new Windows apps

## See Also

### .NET Native Documentation (Legacy)
- [Compiling Apps with .NET Native](index.md) - Overview of .NET Native technology
- [Getting Started with .NET Native](getting-started-with-net-native.md) - How to use .NET Native
- [.NET Native and Compilation](net-native-and-compilation.md) - Understanding .NET Native compilation
- [Runtime Directives (rd.xml) Configuration File Reference](runtime-directives-rd-xml-configuration-file-reference.md) - .NET Native metadata configuration

### Migration Resources
- [Windows App SDK migration guidance](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy)
- [MSIX packaging](/windows/msix/)
