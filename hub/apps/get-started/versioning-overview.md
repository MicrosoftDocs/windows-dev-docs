---
title: Windows versions and SDK overview
description: Learn how Windows release versions, the Windows SDK, and the Windows App SDK relate to each other, and how to choose the right SDK versions for your project.
ms.topic: article
ms.date: 06/26/2026
ms.localizationpriority: medium
ms.collection: windows11
---

# Windows versions and SDK overview

When you start building Windows apps, you'll notice references to several different version numbers — the Windows OS version, the Windows SDK, and the Windows App SDK. Don't worry: for most new projects, Visual Studio sets sensible defaults and you won't need to think about this at all. But when you do need to target a specific OS version, understand why an API isn't available, or configure your project file, this article explains how the pieces fit together and what choices to make.

## Definitions

| Component | What it is | Version format | How it's updated |
|-----------|-----------|----------------|------------------|
| **Windows OS version** | The operating system installed on the user's device. | `YYHX` (e.g., 24H2, 25H2) | Windows Update (annual feature updates) |
| **Windows SDK** (platform SDK) | Headers, libraries, and metadata for Win32, WinRT, and COM APIs. | `10.0.BBBBB.x` (e.g., 10.0.26100.0) | Installed with Visual Studio or standalone installer |
| **Windows App SDK** | NuGet-distributed framework providing WinUI 3, app lifecycle, windowing, and other modern app features. | `Major.Minor.Patch` (e.g., 1.8.1) | NuGet package update in your project |

## Windows OS version naming

Windows 11 ships major feature updates using the naming pattern **YYHX**:

- **YY** = two-digit calendar year
- **H1** = first half of the year; **H2** = second half of the year

Most major releases have historically shipped in the fall (H2), but H1 releases also occur.

| Windows OS version | Year released | OS build number |
|--------------------|---------------|-----------------|
| Windows 11 22H2 | 2022 | 22621 |
| Windows 11 23H2 | 2023 | 22631 |
| Windows 11 24H2 | 2024 | 26100 |

**Rule:** The OS build number on the user's device determines which platform APIs are available at runtime.

> [!TIP]
> Check the OS build number on any device by pressing **Win + R** and typing `winver`.

## Windows SDK to OS version mapping

Each Windows SDK release family corresponds to a Windows OS version. The SDK's major build number typically matches the OS build number, though some OS feature updates (delivered as enablement packages) share the same SDK family as a prior release.

| Windows SDK family | OS build | Matches Windows version |
|--------------------|----------|------------------------|
| 10.0.22621.x | 22621 | Windows 11 22H2 / 23H2 |
| 10.0.26100.x | 26100 | Windows 11 24H2 |

> [!NOTE]
> Within an SDK family, the last component (e.g., 10.0.26100.**2454**) represent monthly servicing updates that add bug fixes but not new APIs.

**Rule:** Installing a given Windows SDK grants compile-time access to all APIs from that OS version **and all prior versions**. APIs are cumulative.

## Windows App SDK versions

The Windows App SDK is **independent** of both the Windows OS and the Windows SDK. It ships on its own cadence as a NuGet package.

| Windows App SDK | Release date | Minimum supported OS | NuGet package |
|-----------------|--------------|----------------------|---------------|
| 2.2 | 2026-06-09 | Windows 10 1809 (build 17763) | `Microsoft.WindowsAppSDK 2.2.x` |
| 2.1 | 2026-05-21 | Windows 10 1809 (build 17763) | `Microsoft.WindowsAppSDK 2.1.x` |
| 2.0 | 2026-04-29 | Windows 10 1809 (build 17763) | `Microsoft.WindowsAppSDK 2.0.x` |
| 1.8 | 2025-09-09 | Windows 10 1809 (build 17763) | `Microsoft.WindowsAppSDK 1.8.x` |
| 1.7 | 2025-03-18 | Windows 10 1809 (build 17763) | `Microsoft.WindowsAppSDK 1.7.x` |

**Rule:** You can use the latest Windows App SDK on older OS versions (back to its stated minimum). The Windows App SDK version does NOT need to match the Windows OS version or Windows SDK version.

For the full support matrix, see [Windows App SDK supported Windows releases](../windows-app-sdk/support.md).

## How the three components relate

```
Your App (.exe / .msix)
  │
  ├── References: Windows App SDK (NuGet package, e.g., 2.2)
  │     • Provides: WinUI 3, app lifecycle, windowing, push notifications
  │     • Updated: independently via NuGet
  │     • Constraint: has a minimum supported OS version
  │
  ├── Compiled against: Windows SDK (e.g., 10.0.26100)
  │     • Provides: Win32, WinRT, COM API declarations
  │     • Updated: with Visual Studio or standalone installer
  │     • Constraint: determines which APIs are visible at compile time
  │
  └── Runs on: Windows OS (e.g., Windows 11 24H2, build 26100)
        • Provides: actual API implementations at runtime
        • Updated: Windows Update
        • Constraint: determines which APIs are callable at runtime
```

**Key relationships:**

1. Windows App SDK **does not replace** the Windows SDK — you need both.
2. The Windows SDK version can be **newer** than the OS your app runs on (use runtime checks for newer APIs).
3. The Windows App SDK version is **completely independent** — update it without changing anything else.
4. You cannot call a platform API at runtime if the user's OS build is older than the build that introduced it, regardless of which SDK you compiled against.

## Project file configuration

MSBuild properties in your project file control platform targeting. The property names differ slightly between C# and C++:

#### C# project file (.csproj)

In SDK-style .NET projects (used by WinUI 3 and Windows App SDK templates), the Windows platform version is embedded in the Target Framework Moniker (TFM):

```xml
<PropertyGroup>
    <!-- The TFM includes the Windows SDK version you compile against -->
    <TargetFramework>net8.0-windows10.0.26100.0</TargetFramework>

    <!-- The minimum OS version your app supports -->
    <SupportedOSPlatformVersion>10.0.19041.0</SupportedOSPlatformVersion>

    <!-- Or equivalently: -->
    <!-- <TargetPlatformMinVersion>10.0.19041.0</TargetPlatformMinVersion> -->
</PropertyGroup>

<ItemGroup>
    <!-- Windows App SDK (independent of the above) -->
    <PackageReference Include="Microsoft.WindowsAppSDK" Version="2.2.0" />
</ItemGroup>
```

> [!NOTE]
> `SupportedOSPlatformVersion` and `TargetPlatformMinVersion` are equivalent in SDK-style projects. The .NET SDK platform compatibility analyzer uses `SupportedOSPlatformVersion` to emit compile-time warnings when you call APIs that may not be available on your minimum OS version.

#### C++ project file (.vcxproj)

```xml
<PropertyGroup>
    <!-- Note: C++ uses "WindowsTargetPlatform" prefix -->
    <WindowsTargetPlatformVersion>10.0.26100.0</WindowsTargetPlatformVersion>
    <WindowsTargetPlatformMinVersion>10.0.17763.0</WindowsTargetPlatformMinVersion>
</PropertyGroup>
```

| Property (C#) | Purpose | Effect if set too high | Effect if set too low |
|----------|---------|------------------------|-----------------------|
| Windows version in TFM (`TargetFramework`) | Compile-time API surface | Enables platform analyzer warnings (a benefit, not a problem) | Newer APIs are invisible; compiler errors if you reference them |
| `SupportedOSPlatformVersion` / `TargetPlatformMinVersion` | Minimum OS for installation | Fewer users can install your app | You must add runtime checks before calling newer APIs |
| `Microsoft.WindowsAppSDK` version | WinUI 3 and framework features | N/A (use latest stable) | Missing newer WinUI controls or framework features |

## Decision guide: choosing versions

Use the following rules to select versions for a new project:

1. **Set the Windows platform version in your TFM to the latest stable Windows SDK** (e.g., `net8.0-windows10.0.26100.0`). This gives you the broadest compile-time API surface and enables platform compatibility analyzer warnings for APIs unavailable on your minimum OS.
2. **Set `SupportedOSPlatformVersion` (or `TargetPlatformMinVersion`) based on your audience:**
   - Enterprise apps with managed devices: match your organization's minimum supported OS.
   - Consumer apps (broadest reach): `10.0.19041.0` (Windows 10 2004) or `10.0.17763.0` (Windows 10 1809).
   - Apps using features from a specific OS version: set to that version's build number.
3. **Always use the latest stable Windows App SDK.** There is no compatibility penalty; it supports back to Windows 10 1809.
4. **Never use preview/experimental SDK releases in production.**

## Runtime API availability checks

When the Windows platform version in your TFM is higher than `SupportedOSPlatformVersion`, some APIs visible at compile time may not exist on the user's device. The .NET platform compatibility analyzer warns you about these at compile time, but you **must** also check availability at runtime before calling them.

### Pattern: Check API contract (WinRT APIs)

```csharp
using Windows.Foundation.Metadata;

if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 15))
{
    // Safe to call APIs introduced in this contract version.
    // For example, use a newer composition API:
}
else
{
    // Fallback for older OS versions
}
```

### Pattern: Check for a specific type

```csharp
if (ApiInformation.IsTypePresent("Windows.UI.Composition.SceneVisual"))
{
    // Safe to use SceneVisual
}
```

### Pattern: Check for a specific method

```csharp
if (ApiInformation.IsMethodPresent("Windows.Storage.StorageFile", "GetFileFromPathForUserAsync"))
{
    // Safe to call this method
}
```

**Rule:** If you skip these checks and call an unavailable API, the app will fail at runtime. The exact failure depends on the API type — common exceptions include `TypeLoadException`, `MissingMethodException`, or a platform-specific error.

## Common questions

### "Which Windows SDK should I use?"

Use the latest Windows SDK available (currently 10.0.26100). You can always compile against a newer SDK than your target minimum — it only affects what's visible at compile time.

### "Do I need to update the Windows SDK when I update the Windows App SDK?"

No. They are independent. You can update `Microsoft.WindowsAppSDK` from 1.7 to 1.8 without changing your Windows SDK version.

### "My app targets Windows 10. Can I use the Windows App SDK?"

Yes. The Windows App SDK supports Windows 10 version 1809 (build 17763) and later. Set your `SupportedOSPlatformVersion` accordingly.

### "What happens if I compile against SDK 10.0.26100 but the user runs Windows 11 22H2 (build 22621)?"

APIs introduced between build 22621 and 26100 will be visible in your code but will fail at runtime unless you add API availability checks. APIs from 22621 and earlier will work normally.

### "How do I know which build number introduced a specific API?"

Check the API's reference page on Microsoft Learn. The **Requirements** section lists the minimum OS build (as an API contract version or Windows version).

## Summary of rules

| Rule | Description |
|------|-------------|
| OS version = runtime capability | The user's OS build determines which APIs actually work at runtime. |
| Windows SDK = compile-time surface | The SDK you install determines which APIs are visible in your code. |
| Windows App SDK = app framework | Updated via NuGet, independent of OS and platform SDK versions. |
| SDK ≥ OS is safe | You can compile against a newer SDK than your minimum OS target. |
| Runtime checks are required | If your TFM targets a higher Windows version than `SupportedOSPlatformVersion`, guard newer API calls with runtime checks. |
| Windows App SDK back-compat | The Windows App SDK supports back to Windows 10 1809 regardless of its release version. |

## Related content

- [Windows App SDK supported Windows releases](../windows-app-sdk/support.md)
- [Windows App SDK release notes](../windows-app-sdk/release-notes/windows-app-sdk-2-0.md)
- [Windows SDK archive](/windows/apps/windows-sdk/)
- [Version-adaptive apps](/windows/uwp/debug-test-perf/version-adaptive-apps)
- [Set up your development environment](start-here.md)
