---
title: MSIX on Windows 10 and Windows 11
description: Understand which MSIX features are available on Windows 10 vs Windows 11, including Windows 11-only capabilities and known limitations that haven't been backported to Windows 10.
ms.topic: concept-article
ms.date: 04/17/2026
ms.localizationpriority: medium
---

# MSIX on Windows 10 and Windows 11

MSIX works on both Windows 10 and Windows 11, but not all features are available on both. Several capabilities were introduced with Windows 11 and have not been backported, and some Windows 10 bugs remain unresolved. This page helps you understand what works where, so you can make informed packaging decisions when you need to support Windows 10.

> [!IMPORTANT]
> **Windows 10 mainstream support ended October 14, 2025** (all non-LTSC versions). Windows 10 LTSC 2021 is supported until January 12, 2027. If your target audience is still on Windows 10, plan accordingly — feature bugs are unlikely to be fixed on older Windows 10 releases.

## Feature comparison: Windows 10 vs Windows 11

The table below compares MSIX feature availability between the latest Windows 10 release (22H2, which has the same feature support as 21H2) and Windows 11.

| Feature | Windows 10 (22H2) | Windows 11 |
|---|---|---|
| Core MSIX install and uninstall | ✅ | ✅ |
| App Installer file (`.appinstaller`) support | ✅ | ✅ |
| Sideloading (non-Store install) | ✅ Requires `AllowAllTrustedApps` policy or Developer Mode | ✅ Enabled by default |
| Windows services in MSIX | ✅ (requires Windows 10 version 2004+) | ✅ |
| Package with external location (sparse packages) | ✅ (requires Windows 10 version 2004+) | ✅ |
| Hosted Apps | ✅ (requires Windows 10 version 2004+) | ✅ |
| Package Integrity Enforcement (non-Store) | ✅ (requires Windows 10 version 2004+) | ✅ |
| Flexible virtualization | ✅ (requires Windows 10 version 21H1+) | ✅ |
| Start menu groups | ✅ (requires Windows 10 version 21H1+) | ✅ |
| Modification packages | ✅ | ✅ |
| Package Support Framework (PSF) | ✅ | ✅ |
| Shared package containers | ❌ Windows 11 only | ✅ |
| Legacy context menu support (Shell extensions) | ❌ Windows 11 only | ✅ |
| Mutable package directories | ❌ Windows 11 only | ✅ |
| MSIX Persistent Identity (survives reinstall) | ❌ Windows 11 only | ✅ |
| Dynamic dependencies | ✅ Via Windows App SDK dynamic dependency APIs (`Mdd*` / bootstrapper) on supported Windows App SDK versions; ❌ OS-native API | ✅ Via Windows App SDK; OS-native API available on Windows 11, version 22H2+ |

For the full version-by-version breakdown across all Windows 10 releases, see [MSIX features and supported platforms](/windows/msix/supported-platforms).

## Windows 11-only features

If your app uses any of the following features, it will not work on Windows 10:

### Shared package containers

Shared package containers let multiple packaged apps share a common package namespace, allowing them to share data and settings. This feature requires Windows 11.

**Windows 10 alternative:** Use a shared Win32 data location (such as `%ProgramData%` or a named pipe) outside the MSIX container, or restructure the apps as related packages within a single bundle.

### Legacy context menu support

Windows 11 introduced support for registering MSIX-packaged Shell extensions (right-click context menus) that appear in the classic context menu. This is primarily relevant when converting legacy desktop apps to MSIX.

**Windows 10 alternative:** Use the Package Support Framework (PSF) or deliver context menu extensions through a separate Win32 installer on Windows 10.

### Mutable package directories

Mutable package directories allow apps to write to a subdirectory within the install location, rather than the virtualized container. Requires Windows 11.

**Windows 10 alternative:** Write to `ApplicationData.Current.LocalFolder` (AppData\Local\Packages\...\LocalState) or a location outside the package.

### MSIX Persistent Identity

MSIX Persistent Identity ensures that an app's package identity (family name, publisher, etc.) survives uninstall and reinstall. Without it, data in `LocalState` is deleted when the app is uninstalled. Requires Windows 11.

**Windows 10 alternative:** Store persistent data outside the package container — for example, in `%APPDATA%`, `%LOCALAPPDATA%` (outside the Packages folder), or a database in `%ProgramData%`.

### Dynamic dependencies

Dynamic dependencies allow packaged apps to take runtime dependencies on framework packages that aren't declared at packaging time. This is the mechanism underlying the Windows App SDK's support for unpackaged apps. The Windows App SDK dynamic dependency APIs (including the `Mdd*` APIs and bootstrapper flow) are supported on both Windows 10 and Windows 11 where the Windows App SDK is supported. A separate OS-native dynamic dependency implementation is also available on Windows 11, version 22H2 and later.

**Windows 10 guidance:** Use the Windows App SDK dynamic dependency APIs or bootstrapper to handle dependency resolution on Windows 10 — no workaround is needed.

## Windows 10-specific considerations

### Sideloading policy

On Windows 10 version 2004 and later, signed non-Store MSIX packages can generally be installed by double-click without separately enabling sideloading. On older Windows 10 versions (pre-2004), sideloading must be enabled via Developer Mode or the `AllowAllTrustedApps` Group Policy.

Exceptions apply regardless of Windows version: unsigned packages require additional trust configuration, and enterprise-managed devices can restrict or disable non-Store app installation through policy.

On Windows 11, sideloading is also enabled by default for standard signed packages, subject to the same policy-based restrictions.

### LTSC 2021 feature limitations

Windows 10 LTSC 2021 (build 19044, equivalent to 20H2) is missing features that were added in Windows 10 21H1 and later:

| Feature | LTSC 2021 | Windows 10 21H1+ |
|---|---|---|
| Flexible virtualization | ❌ | ✅ |
| Start menu groups | ❌ | ✅ |

If your enterprise deployment targets LTSC 2021, don't rely on flexible virtualization or packaged start menu groups.

### Unresolved bugs on Windows 10

Some MSIX issues reported on Windows 10 have not been backported and are unlikely to be fixed given Windows 10's end-of-support status. If you encounter MSIX behavior on Windows 10 that differs from Windows 11, check the open issues in the [microsoft/msix-packaging](https://github.com/microsoft/msix-packaging/issues) repository.

Common categories of known Windows 10-specific issues include:
- Registry virtualization edge cases
- App Installer reliability with certain network configurations
- Packaging Tool compatibility with some installer types

## Packaging tools for Windows 10 targets

If you're packaging apps for Windows 10 and encountering tool compatibility issues, several community packaging tools provide additional compatibility workarounds:

- **[MSIX Packaging Tool](/windows/msix/packaging-tool/tool-overview)** (Microsoft) — the official tool; actively maintained
- **[Advanced Installer](https://www.advancedinstaller.com/)** — commercial tool with strong Windows 10 MSIX support and a well-maintained compatibility matrix
- **[Conveyor](https://conveyor.hydraulic.dev/)** — community tool that automates packaging for multiple targets including MSIX; useful if you also ship on macOS or Linux

These tools can smooth over some Windows 10 edge cases in the build process, though runtime OS limitations (such as missing Win11-only APIs) cannot be worked around in packaging.

## Recommendation

If you need to support Windows 10:

1. **Avoid Windows 11-only features** — don't use shared package containers, mutable package directories, persistent identity, or dynamic dependencies if your MinVersion targets Windows 10.
2. **Set an accurate `MinVersion`** in your MSIX manifest's `TargetDeviceFamily` element to the oldest Windows 10 build you intend to support.
3. **Test on Windows 10 specifically** — behavior differences between Windows 10 and Windows 11 are rarely caught in Windows 11-only CI pipelines.
4. **Plan your Win10 EOL strategy** — with mainstream Windows 10 support ended, consider setting a roadmap to require Windows 11 in a future app version.

## Related content

- [MSIX features and supported platforms](/windows/msix/supported-platforms) — full version-by-version compatibility table
- [Choose a distribution path for your Windows app](choose-distribution-path.md)
- [Current status of Windows app distribution features](distribution-feature-status.md)
- [Package Support Framework overview](/windows/msix/psf/package-support-framework-overview)
