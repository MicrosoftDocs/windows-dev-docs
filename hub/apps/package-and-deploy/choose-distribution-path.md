---
title: Choose a distribution path for your Windows app
description: Compare the available Windows app distribution paths — Microsoft Store, MSIX sideloading, and direct download — to find the right fit for your app and audience.
ms.topic: concept-article
ms.date: 04/17/2026
ms.localizationpriority: medium
---

# Choose a distribution path for your Windows app

How you distribute your Windows app affects code signing costs, update mechanics, enterprise manageability, and how easily customers discover and install it. This article compares the main paths to help you make the right choice.

> [!TIP]
> **For most developers, the Microsoft Store is the recommended path.** It provides free code signing, built-in update delivery, broad discoverability, and a trusted install experience — with no infrastructure to manage.

## Distribution paths at a glance

| Path | Best for | Code signing cost | Auto-update | Enterprise MDM | Distributed via Store |
|---|---|---|---|---|---|
| **Microsoft Store** | Consumer and business apps, broad reach | ✅ Free (Store signs for you) | ✅ Built-in | ✅ Via Intune with Company Portal | ✅ Yes |
| **MSIX sideload (enterprise)** | Internal LOB apps via Intune/ConfigMgr | 💲 Azure Trusted Signing (~$10/mo) or self-signed + Intune cert profile | ✅ Via App Installer file or MDM | ✅ Native | ❌ No |
| **MSIX direct download (ISV)** | Commercial apps sold from your own site | 💲 CA-trusted cert required ([Azure Trusted Signing](/azure/trusted-signing/) recommended) | ✅ Via `.appinstaller` file | ⚠️ Limited | ❌ No |
| **Packaging with external location** | Existing apps with own installer needing Windows features | 💲 Same as MSIX direct download | ✅ Your existing mechanism | ⚠️ Limited | ❌ No |

## Microsoft Store (recommended)

Publishing to the Microsoft Store is the most complete distribution solution for Windows apps.

**What you get:**
- Code signing handled automatically — no certificate purchase needed
- Built-in update delivery with staged rollout support
- Discovery through the Store's search and curated collections
- Trusted install UX with no SmartScreen warnings
- Revenue processing, refunds, and analytics included

**Requirements:**
- App must be packaged as MSIX (WinUI 3 apps are packaged by default)
- App must pass [Store certification requirements](/windows/apps/publish/publish-your-app/msix/app-package-requirements)
- Developer account required ([Partner Center](https://partner.microsoft.com/dashboard))

**When to choose this:**
- Your app targets consumers or business users broadly
- You want the simplest possible distribution infrastructure
- You're building a new WinUI 3 app (you're already packaged — just submit)

→ [Publish to the Microsoft Store](/windows/apps/publish/publish-your-app/msix/create-app-submission)

## MSIX sideloading — enterprise LOB distribution

For internal line-of-business apps that will be deployed to managed devices via Microsoft Intune or Configuration Manager, MSIX sideloading is the recommended path.

**What you get:**
- Silent install and update via MDM policies
- Integration with enterprise device management (Intune, ConfigMgr)
- Full package identity and access to Windows features (notifications, background tasks, etc.)

**Code signing:**
- Use [Azure Trusted Signing](/azure/trusted-signing/) (~$10/month) for a CA-trusted certificate, or
- Use a self-signed certificate deployed to endpoints via Intune Trusted Certificate profiles

**Requirements:**
- Target devices must trust the signing certificate (either via MDM or Group Policy)
- Sideloading must be allowed on target devices (enabled by default on Windows 10 version 2004+ and all Windows 11 devices)

**When to choose this:**
- Distributing an internal app to company-managed devices
- You have an IT team who can configure certificate trust via Intune or Group Policy

→ [Deploy MSIX apps with Intune](/windows/msix/desktop/managing-your-msix-deployment-intune)  
→ [Deploy MSIX apps with Configuration Manager](/windows/msix/desktop/managing-your-msix-deployment-configmgr)

## MSIX direct download — ISV and commercial apps

For commercial apps sold directly from your website (not through the Store), you can distribute MSIX packages with an `.appinstaller` file for auto-update support.

**What you get:**
- Familiar install experience via App Installer
- Auto-update support via `.appinstaller` file (hosted on your server)
- Full package identity and Windows feature access
- Control over your own distribution channel and pricing

**Code signing:**
- A CA-trusted code signing certificate is required — users cannot install unsigned or self-signed MSIX packages without trusting the cert manually
- [Azure Trusted Signing](/azure/trusted-signing/) (~$10/month) is Microsoft's recommended option: no hardware token required, integrates with CI/CD pipelines
- Traditional OV certificates are also accepted (typically $150–300/year from a CA)

**SmartScreen:** New certificates accumulate SmartScreen reputation over time based on download volume. Expect some SmartScreen prompts for new releases. See [SmartScreen reputation for Windows app developers](smartscreen-reputation.md).

> [!IMPORTANT]
> The `ms-appinstaller:` URI protocol (one-click browser install) is disabled by default since December 2023. Link to the `.appinstaller` file directly for download, or consider publishing to the Store for broader reach. See [Current status of Windows app distribution features](distribution-feature-status.md).

**When to choose this:**
- You're an ISV selling software directly from your website
- You need control over installer UX, pricing, or licensing that the Store doesn't support
- Your customers are businesses that procure software outside the Store

→ [App Installer file overview](/windows/msix/app-installer/app-installer-file-overview)  
→ [Auto-update and repair apps](/windows/msix/app-installer/auto-update-and-repair--overview)

## Packaging with external location (sparse package)

If you have an existing app with its own installer (WiX, NSIS, InstallShield) and want to add Windows features that require package identity - without replacing your installer with MSIX - use packaging with external location.

**What you get:**
- Package identity without changing your installer or binary locations
- Access to Windows features: notifications, background tasks, file type associations, protocol handlers
- Your existing install and update mechanism stays in place

**What you don't get:**
- Store eligibility
- The clean install/uninstall model of full MSIX

**When to choose this:**
- You have an existing Win32/WPF/WinForms app with an established installer
- You want specific Windows API features that require package identity
- Migrating fully to MSIX is not feasible right now

→ [Grant package identity by packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md)

## What about other installer formats?

Many Windows apps are distributed using MSI, WiX, Inno Setup, ClickOnce, or similar technologies. These are established and supported options, especially for existing apps. However, they may not provide MSIX's package identity, clean uninstall model, or Store eligibility.

## Related content

- [Packaging overview](packaging/index.md)
- [SmartScreen reputation for Windows app developers](smartscreen-reputation.md)
- [Current status of Windows app distribution features](distribution-feature-status.md)
- [Publish to the Microsoft Store](/windows/apps/publish/)
- [Azure Trusted Signing](/azure/trusted-signing/)
