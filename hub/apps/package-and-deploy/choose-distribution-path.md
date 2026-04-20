---
title: Choose a distribution path for your Windows app
description: Compare the available Windows app distribution paths — Microsoft Store, PWA, MSIX sideloading, and direct download — to find the right fit for your app and audience.
ms.topic: concept-article
ms.date: 04/20/2026
ms.localizationpriority: medium
---

# Choose a distribution path for your Windows app

How you distribute your Windows app affects code signing costs, update mechanics, enterprise manageability, and how easily customers discover and install it. This article compares the main paths to help you make the right choice.

> [!TIP]
> **For most developers, the Microsoft Store is the recommended path.** It provides free code signing, built-in update delivery, broad discoverability, and a trusted install experience — with no infrastructure to manage.

> [!NOTE]
> **If your app is built on web technologies** (HTML, JavaScript, CSS), a [Progressive Web App (PWA)](#progressive-web-app-pwa) is the fastest path to the Microsoft Store — no native packaging tools required.

## Distribution paths at a glance

| Path | Best for | Code signing cost | Auto-update | Enterprise MDM | Distributed via Store |
|---|---|---|---|---|---|
| **Microsoft Store** | Consumer and business apps, broad reach | ✅ Free (Store signs for you) | ✅ Built-in | ✅ Via Intune with Company Portal | ✅ Yes |
| **PWA (Progressive Web App)** | Web apps and web-based experiences | ✅ Free (Store signs for you) | ✅ Via Store or browser | ✅ Via Intune with Company Portal | ✅ Yes |
| **MSIX sideload (enterprise)** | Internal LOB apps via Intune/ConfigMgr | 💲 Azure Artifact Signing (formerly Trusted Signing) (~$10/mo) or self-signed + Intune cert profile | ✅ Via App Installer file or MDM | ✅ Native | ❌ No |
| **MSIX direct download (ISV)** | Commercial apps sold from your own site | 💲 CA-trusted cert required ([Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/) recommended) | ✅ Via `.appinstaller` file | ⚠️ Limited | ❌ No |
| **Packaging with external location** | Existing apps with own installer needing Windows features | 💲 Same as MSIX direct download | ✅ Your existing mechanism | ⚠️ Limited | ❌ No |
| **Unpackaged WinUI 3** | Niche: enterprise without MSIX capability, or max install simplicity | 💲 Cert recommended for SmartScreen | ❌ Manual only | ⚠️ Limited (via Intune/ConfigMgr Win32 deployment) | ⚠️ Limited (Store-listed installer submission) |

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

## Progressive Web App (PWA)

If your app is a website or built primarily on web technologies, a Progressive Web App is the fastest path to the Microsoft Store — with no native packaging tools or code signing purchase required.

A PWA is a web app that browsers can install as a standalone app. It can run offline, send push notifications, appear in the Start menu and taskbar, and be distributed through the Microsoft Store. Use [PWABuilder](https://www.pwabuilder.com/) to package your site for Store submission in minutes.

**What you get:**
- Store distribution with free code signing (Store signs the package)
- Works across any device with a modern browser
- No manual MSIX, WiX, or installer authoring required — tools like [PWABuilder](https://www.pwabuilder.com/) generate the Store submission package for you
- Built-in update delivery — users always get your latest web content (hosted content updates without a Store re-submission)

**Requirements:**
- App must be served over HTTPS
- A [web app manifest](https://developer.mozilla.org/en-US/docs/Web/Manifest) and [service worker](https://developer.mozilla.org/en-US/docs/Web/API/Service_Worker_API)
- App must pass [Store certification requirements](/windows/apps/publish/publish-your-app/msix/app-package-requirements)

**Limitations:**
- Deep native Windows APIs (file system access, hardware integration beyond Web APIs) are not available without additional bridging
- App logic runs in a web context — not suitable for apps that require native .NET, C++, or WinRT APIs

**When to choose this:**
- Your app is a web app, SaaS tool, or content site you want to make installable
- You want the fastest path to the Store with minimal tooling
- Your feature requirements are satisfied by modern Web APIs

→ [Overview of Progressive Web Apps](/microsoft-edge/progressive-web-apps-chromium/)  
→ [Publish a PWA to the Microsoft Store with PWABuilder](https://docs.pwabuilder.com/#/builder/windows)

## MSIX sideloading — enterprise LOB distribution

For internal line-of-business apps that will be deployed to managed devices via Microsoft Intune or Configuration Manager, MSIX sideloading is the recommended path.

**What you get:**
- Silent install and update via MDM policies
- Integration with enterprise device management (Intune, ConfigMgr)
- Full package identity and access to Windows features (notifications, background tasks, etc.)

**Code signing:**
- Use [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/) (~$10/month) for a CA-trusted certificate, or
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
- [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/) (~$10/month) is Microsoft's recommended option: no hardware token required, integrates with CI/CD pipelines
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

## Unpackaged WinUI 3

Unpackaged distribution removes MSIX from the picture entirely — the app runs directly from a folder without a package manifest. This is a niche option suited to specific scenarios.

**What you get:**
- Simpler build output (a folder of files, no MSIX tooling)
- No MSIX infrastructure required on target machines
- Works on machines where MSIX sideloading isn't enabled

**Limitations:**
- **No single-file EXE** — The Windows App SDK runtime must ship as separate files alongside your executable
- **Runtime deployment** — You must bundle the Windows App SDK runtime installer, or use self-contained deployment (larger output)
- **No package identity** — No automatic updates, no background tasks, no file type associations via manifest
- **No MSIX/package-identity Store submission** — This model has no package identity and cannot be submitted to the Store as an MSIX package. A traditional installer (MSI/EXE) can be submitted separately, but that is outside this distribution path.
- SmartScreen warnings unless signed with a CA-trusted certificate

**When to choose this:**
- Your target environment can't use MSIX (uncommon; most managed enterprise environments support MSIX)
- You're building an internal tool where MSIX overhead isn't justified

**For most WinUI 3 apps, MSIX (via Store or direct download) is the better path.** The limitations above often surprise developers who discover them after investing in unpackaged distribution.

→ [Distribute an unpackaged WinUI 3 app](unpackage-winui-app.md) — step-by-step guide with runtime deployment options



Many Windows apps are distributed using ClickOnce, MSI, WiX, Inno Setup, or similar technologies. These are established and supported options, especially for apps that can't use MSIX or don't need Store distribution. The table below summarizes the common options and their trade-offs.

| Method | Auto-update | Code signing required | Store eligible | Best for |
|---|---|---|---|---|
| **MSIX via Store** | ✅ Built-in | ✅ Free (Store signs) | ✅ Yes | Most apps — recommended starting point |
| **MSIX + .appinstaller** | ✅ Built-in | 💲 CA-trusted cert | ❌ No | ISVs distributing directly from a website |
| **ClickOnce** | ✅ Built-in | 💲 Cert recommended | ❌ No | WPF/WinForms apps; not supported for WinUI 3 |
| **MSI / WiX / Inno Setup** | ⚠️ Manual or custom | 💲 Cert recommended | ❌ No | Apps with complex install requirements or existing installer |
| **Self-contained EXE (xcopy/zip)** | ❌ None | 💲 Cert recommended | ❌ No | Simple utilities; developer/power-user audiences |
| **winget manifest** | ✅ Via winget | 💲 Cert recommended | ❌ No | Any of the above — adds discoverability via `winget install` |

### ClickOnce

ClickOnce is a .NET deployment technology built into Visual Studio. It hosts a manifest on a web server or file share; users install from the manifest URL and ClickOnce handles update checks at launch. It's a good fit for WPF and WinForms apps distributed to a known user base.

ClickOnce is **not supported for WinUI 3 apps**. Use MSIX with `.appinstaller` for WinUI 3 direct distribution.

→ [ClickOnce security and deployment](/visualstudio/deployment/clickonce-security-and-deployment)

### MSI, WiX, Inno Setup, and NSIS

Traditional EXE and MSI installers remain common for Windows apps with complex installation requirements (driver installation, system services, registry configuration). Tools like [WiX Toolset](https://wixtoolset.org/), [Inno Setup](https://jrsoftware.org/isinfo.php), and [NSIS](https://nsis.sourceforge.io/) are community-maintained and widely used. Update support requires your own implementation.

These formats are not Store-eligible as primary distribution packages. However, you can combine them with [packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md) if you need package identity for specific Windows features.

### Self-contained EXE (xcopy deployment)

`dotnet publish --self-contained` produces a folder of files (or a single-file EXE) that users can run without installing .NET. This is the simplest distribution model but requires users to download a new version manually. It suits command-line tools, developer utilities, and power-user apps.

### winget — adding discoverability to any distribution path

Regardless of your packaging format, you can submit a manifest to the [Windows Package Manager Community Repository](https://github.com/microsoft/winget-pkgs) to make your app installable via `winget install <your-app>`. This doesn't replace your existing distribution method — it adds a command-line installation path valued by developer and technical audiences.

## Related content

- [Packaging overview](packaging/index.md)
- [SmartScreen reputation for Windows app developers](smartscreen-reputation.md)
- [Current status of Windows app distribution features](distribution-feature-status.md)
- [Publish to the Microsoft Store](/windows/apps/publish/)
- [Progressive Web Apps overview](/microsoft-edge/progressive-web-apps-chromium/)
- [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/)
