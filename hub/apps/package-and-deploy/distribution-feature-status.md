---
title: Current status of Windows app distribution features
description: Up-to-date status of Windows app distribution features, including ms-appinstaller protocol status, .appinstaller schema versions, and platform support differences between Windows 10 and Windows 11.
ms.topic: concept-article
ms.date: 04/17/2026
ms.localizationpriority: medium
---

# Current status of Windows app distribution features

This page documents the current status of Windows app distribution features that have changed, are known to have limitations, or behave differently than their documentation may suggest. It is updated as the platform evolves.

**Last reviewed:** April 2026

---

## ms-appinstaller URI protocol

**Status: Disabled by default (since December 2023)**

The `ms-appinstaller:?source=` URI protocol handler allows a web page to trigger a one-click App Installer install without the user downloading the file first. This feature was **disabled by default** in App Installer version 1.21.3421.0, released December 12, 2023, in response to its abuse by the Emotet malware campaign (CVE-2021-43890 exploitation pattern).

| Context | Status |
|---|---|
| Consumer devices (default) | ❌ Disabled |
| Enterprise devices (IT-managed) | ✅ Can be re-enabled via Group Policy |

**Impact:** Tutorial pages on Microsoft Learn that demonstrate `<a href="ms-appinstaller:?source=...">Install</a>` web links no longer work for most users.

**Workarounds:**
- **Link directly to the `.appinstaller` file** — users download and double-click it. This still works and is the recommended approach for non-enterprise scenarios.
- **Publish to the Microsoft Store** — provides a superior one-click install experience with no protocol dependency.
- **Enterprise re-enablement:** Set the Group Policy `EnableMSAppInstallerProtocol` to **Enabled** via the [DesktopAppInstaller CSP](/windows/client-management/mdm/policy-csp-desktopappinstaller#enablemsappinstallerprotocol). Note: the policy value `Disabled` means "the setting is not configured" (double-negative); set to `Enabled` to re-enable the protocol.

**References:** [App Installer security features](/windows/msix/app-installer/app-installer-security-features)

---

## .appinstaller file schema versions

**Status: Visual Studio generates outdated schema by default**

The `.appinstaller` XML file supports multiple schema versions, each with different capabilities. Visual Studio generates files using the **2017/2 schema** by default, which does not support several important update configuration attributes.

| Attribute | 2017/2 schema | 2021 schema |
|---|---|---|
| `ShowPrompt` | ❌ Not supported | ✅ Supported |
| `UpdateBlocksActivation` | ❌ Not supported | ✅ Supported |
| `HoursBetweenUpdateChecks` | ❌ Not supported | ✅ Supported |
| Basic update on launch | ✅ Supported | ✅ Supported |

**Impact:** Developers who rely on Visual Studio to generate `.appinstaller` files and then configure `ShowPrompt` or `UpdateBlocksActivation` will find those settings are silently ignored at runtime.

**Fix:** Manually update the `xmlns` attribute in your `.appinstaller` file:

```xml
<!-- Change this: -->
<AppInstaller xmlns="http://schemas.microsoft.com/appx/appinstaller/2017/2" ...>

<!-- To this: -->
<AppInstaller xmlns="http://schemas.microsoft.com/appx/appinstaller/2021" ...>
```

**References:** [Auto-update and repair apps](/windows/msix/app-installer/auto-update-and-repair--overview) · [WindowsAppSDK Discussion #5125](https://github.com/microsoft/WindowsAppSDK/discussions/5125)

---

## SmartScreen reputation: EV certificates no longer grant instant bypass

**Status: Behavior changed in 2024**

Prior to 2024, Extended Validation (EV) code signing certificates granted immediate SmartScreen reputation — a newly-signed binary would show no download warning. Microsoft updated the Trusted Root Program requirements in 2024, removing EV-specific OIDs. SmartScreen reputation is now exclusively **hash-based and accumulates over time**, regardless of certificate type (OV or EV).

**Impact:** Developers who purchased EV certificates specifically to bypass SmartScreen warnings for new releases will find that EV certificates no longer provide this benefit.

**Current behavior:** All non-Store, non-Microsoft-signed binaries show a SmartScreen prompt on first download until sufficient download history is accumulated for that file hash.

See [SmartScreen reputation for Windows app developers](smartscreen-reputation.md) for full details on expected behavior and recommendations.

---

## MSIX on Windows 10 vs Windows 11

**Status: Several MSIX features are Windows 11-only**

MSIX works on both Windows 10 and Windows 11, but several features — including shared package containers, mutable package directories, and MSIX persistent identity — are Windows 11-only and have not been backported. Dynamic dependencies are also supported on Windows 10 via the Windows App SDK (`Mdd*` APIs / bootstrapper), with Windows 11 additionally providing an OS-native implementation. Additionally, Windows 10 mainstream support ended October 14, 2025.

For a full comparison table, known unbackported limitations, and per-feature workarounds, see [MSIX on Windows 10 and Windows 11](msix-windows10-windows11.md).

---

## MsixPackaging@1 Azure DevOps task

**Status: Uses outdated dependencies**

The `MsixPackaging@1` task in Azure DevOps pipelines uses MSBuild 4.8.4161.0 (instead of MSBuild 16+) and was built against Node 16 (which reached end-of-life in September 2023). This can cause build failures in modern pipeline configurations.

**Workaround:** Use MSBuild directly in your pipeline rather than the `MsixPackaging@1` task, or use GitHub Actions with the `microsoft/setup-msbuild` action.

**References:** [GitHub Issue #518](https://github.com/microsoft/msix-packaging/issues/518) · [GitHub Issue #679](https://github.com/microsoft/msix-packaging/issues/679)

---

## Related content

- [Choose a distribution path for your Windows app](choose-distribution-path.md)
- [SmartScreen reputation for Windows app developers](smartscreen-reputation.md)
- [App Installer file overview](/windows/msix/app-installer/app-installer-file-overview)
- [Auto-update and repair apps](/windows/msix/app-installer/auto-update-and-repair--overview)
- [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/)
