---
title: Publish your first Windows app
description: An end-to-end guide for indie developers — from a built app to users' hands. Covers choosing a distribution path, code signing, packaging, and getting your first users.
ms.topic: concept-article
ms.date: 04/20/2026
ms.localizationpriority: high
---

# Publish your first Windows app

You built a WPF, WinForms, or WinUI 3 app — now you need to get it into users' hands. This guide walks through the full path from a finished build to a published app, covering the two most common distribution scenarios: publishing to the **Microsoft Store** (recommended) and setting up **direct download** distribution.

> [!TIP]
> **The Microsoft Store is the recommended path for most developers.** It handles code signing, update delivery, and discovery — and it's the lowest-friction way to reach Windows users. Direct download is the right choice when you have specific commercial, enterprise, or distribution requirements that the Store doesn't fit.

## Step 1: Choose your distribution path

Your distribution path determines your code signing costs, update mechanics, how users discover your app, and how enterprises can deploy it.

→ [Choose a distribution path for your Windows app](choose-distribution-path.md) has a full comparison. In short:

- **Microsoft Store** — recommended for most apps. Free signing, built-in updates, broad discoverability, and a trusted install experience. Requires a one-time $19 [Partner Center](https://partner.microsoft.com/dashboard) developer account fee.
- **Direct download** — appropriate for commercial ISVs with their own storefront, enterprise LOB apps, or apps with content the Store doesn't permit. You are responsible for signing, hosting, and updates.

Most new indie apps are a good fit for the Store. If you're unsure, start there.

## Step 2: Set up code signing

**If you're publishing to the Microsoft Store:** skip this step. Microsoft signs your package automatically as part of the certification process.

**If you're distributing directly:** you need a trusted code signing certificate. Unsigned apps and self-signed apps trigger strong SmartScreen warnings, and some enterprise environments will block them entirely.

→ [Code signing options for Windows app developers](code-signing-options.md) has a full comparison. Quick guidance:

- **Organizations in USA, Canada, EU, or UK / Individuals in USA or Canada:** [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/) — approximately $9.99/month, no hardware token required, integrates with CI/CD pipelines. This is Microsoft's recommended option for non-Store distribution.
- **Individual developers outside USA/Canada, or anyone not eligible for Azure Artifact Signing:** An OV certificate from a Certificate Authority such as DigiCert or Sectigo — typically $150–300/year.

> [!NOTE]
> Signing your app is required for a good first-run experience. A new signed app will still show a SmartScreen warning until it builds reputation — but the warning is much less severe than for unsigned apps. See [SmartScreen reputation for developers](smartscreen-reputation.md) to understand what your early users will see.

## Step 3: Package your app

How you package your app depends on the app framework you used.

### WinUI 3

WinUI 3 apps created with the Windows App SDK project templates are **already packaged as MSIX by default**. When you build your solution in Visual Studio, the output is an `.msix` or `.msixbundle` file ready for Store submission or direct distribution.

If you want to distribute an unpackaged WinUI 3 app (without MSIX packaging), see [Distribute an unpackaged WinUI 3 app](unpackage-winui-app.md). Note that unpackaged WinUI 3 apps cannot produce a single-file EXE and require the Windows App SDK runtime on the user's machine (either installed separately via the runtime installer, or bundled using self-contained deployment).

### WPF and WinForms

WPF and WinForms projects do not produce MSIX by default. You have a few options:

**Option A: Windows Application Packaging Project (MSIX)**  
Add a Windows Application Packaging Project to your solution in Visual Studio. This wraps your app in an MSIX package, giving you package identity, Store eligibility, and App Installer–based updates. This is the recommended approach for Store submission.

→ [Package your desktop app using single-project MSIX](../windows-app-sdk/single-project-msix.md)

**Option B: `dotnet publish` (self-contained EXE)**  
For direct download distribution, `dotnet publish` with `--self-contained` produces a standalone EXE that includes the .NET runtime — users don't need to install .NET separately.

```console
dotnet publish -c Release -r win-x64 --self-contained true
```

This produces a folder of files suitable for zipping and distributing, or wrapping in an installer. It is not directly Store-eligible.

**Option C: Framework-dependent publish**  
Omit `--self-contained` if you're comfortable requiring users to have the correct .NET runtime installed. The output is smaller but has a runtime dependency.

## Step 4a: Submit to the Microsoft Store (recommended path)

The Store submission process runs through [Partner Center](https://partner.microsoft.com/dashboard).

**Steps at a glance:**

1. **Create a developer account** — One-time $19 fee at [Partner Center](https://partner.microsoft.com/dashboard). The account is permanent.
2. **Reserve your app name** — Claim your app's name in Partner Center before submission. The name is held for you during development.
3. **Build and package your app** — Create an MSIX package that meets Store requirements. For Store submissions, MSIX/AppX packages don't need a CA-trusted signature — Microsoft re-signs the package with a Microsoft certificate after certification. If you distribute outside the Store using an MSI or EXE installer, Authenticode signing is recommended and expected by Windows security features.
4. **Create your submission** — Upload your package, provide store listing details (description, screenshots, categories, age rating), and set pricing.
5. **Certification** — Microsoft reviews your app for policy compliance. Certification typically takes a few business days for new apps.
6. **Publish** — Once certified, your app appears in the Store and is available to users.

→ [Create your app submission](/windows/apps/publish/publish-your-app/msix/create-app-submission)  
→ [App package requirements](/windows/apps/publish/publish-your-app/msix/app-package-requirements)

## Step 4b: Distribute directly (alternative path)

If you're distributing your app outside the Store, you have several packaging and hosting options.

### MSIX with App Installer (.appinstaller)

MSIX packages distributed with a companion `.appinstaller` file support automatic update checks. When users install the app from the `.appinstaller` file, Windows periodically checks the URL you specify for a newer version and offers to update.

> [!IMPORTANT]
> The `ms-appinstaller` URI protocol (which allows installing directly from a web link) is **disabled by default** since December 2023 due to security concerns. Users must download the `.appinstaller` or `.msix` file and open it manually.  
> → [Current status of distribution features](distribution-feature-status.md)

### ClickOnce (WPF and WinForms)

ClickOnce is a .NET deployment technology built into Visual Studio that supports automatic updates for WPF and WinForms apps. Users install from a hosted manifest, and ClickOnce handles update checks transparently.

ClickOnce is **not supported for WinUI 3 apps** — use MSIX with `.appinstaller` instead.

→ [ClickOnce security and deployment](/visualstudio/deployment/clickonce-security-and-deployment)

### EXE installer

A traditional EXE installer created with a tool such as [WiX Toolset](https://wixtoolset.org/) or [Inno Setup](https://jrsoftware.org/isinfo.php) is familiar to users and straightforward to produce. These are well-established community tools used by many Windows applications. Note that EXE installers require you to implement your own update mechanism.

### Hosting your download

Common hosting options for direct download:

- **GitHub Releases** — free, versioned, and integrates well with automated build pipelines. Suitable for open-source and small commercial apps.
- **Your own website** — full control over the download page, analytics, and payment flow. You'll need reliable storage and CDN capacity if your app grows.
- **Third-party stores** — some developers distribute through storefronts such as itch.io for gaming-adjacent apps.

### winget for discoverability

Submitting your app to the [Windows Package Manager Community Repository](https://github.com/microsoft/winget-pkgs) makes your app installable via `winget install`. This is especially valued by developer and power-user audiences who prefer command-line tooling. Submission is a pull request against the community manifest repository and is free.

## Step 5: Set up auto-update

Keeping users on the latest version reduces support burden and ensures security fixes reach them promptly.

| Distribution path | Update mechanism |
|---|---|
| Microsoft Store | Automatic — Store delivers updates in the background |
| MSIX + `.appinstaller` | Built-in — Windows checks the URL you specify on a schedule |
| ClickOnce | Built-in — checks for updates at launch |
| EXE installer or self-contained EXE | Manual implementation required |

For EXE or self-contained deployments without a built-in update mechanism, [Velopack](https://velopack.io) is a community tool that adds auto-update and installer capabilities to .NET apps. It is not a Microsoft product — evaluate it based on your own requirements.

## Step 6: Handle SmartScreen for new apps

Every new app — regardless of how well it's signed — will trigger a SmartScreen warning on first download until it accumulates enough download history. This is expected and normal. Here's how to handle it well:

- **Sign every release.** Unsigned apps show a more severe "Windows protected your PC" block. Signed apps show a softer "unrecognized app" warning that users can bypass more easily.
- **Set expectations with early users.** Tell beta testers and early adopters what to expect. A brief note in your release announcement ("You may see a SmartScreen prompt on first run — this is normal for new apps; click 'More info' then 'Run anyway'") prevents confusion and abandoned installs.
- **Be patient.** Reputation builds organically with download volume. There is no manual submission process to accelerate it for consumer endpoints.

→ [SmartScreen reputation for developers](smartscreen-reputation.md) explains the full reputation model, what users see at each stage, and enterprise policy considerations.

## What's next

Once your app is published, you can:

- **Monitor app health and ratings** — Partner Center provides crash analytics, user ratings, and review management for Store apps
- **Manage releases with staged rollouts** — the Store supports rolling out a new version to a percentage of users before a full release
- **Set up telemetry** — consider integrating [Windows App SDK diagnostics](/windows/apps/windows-app-sdk/applifecycle/app-lifecycle) or a third-party analytics service to understand how your app is used
- **Respond to user feedback** — Partner Center surfaces Store reviews; for direct download apps, consider a feedback channel (GitHub Issues, a dedicated email address, or a community forum)

## Related content

- [Choose a distribution path for your Windows app](choose-distribution-path.md)
- [Code signing options for Windows app developers](code-signing-options.md)
- [SmartScreen reputation for developers](smartscreen-reputation.md)
- [Current status of distribution features](distribution-feature-status.md)
- [Create your app submission](/windows/apps/publish/publish-your-app/msix/create-app-submission)
- [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/)
- [Windows Package Manager (winget)](https://github.com/microsoft/winget-pkgs)
