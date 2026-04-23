---
title: Grant package identity by packaging with external location
description: Overview of how to grant package identity to an unpackaged Win32 app so that you can use modern Windows features in that app.
ms.date: 05/20/2025
ms.topic: article
keywords: windows 11, windows 10, desktop, sparse, package, identity, external, location, MSIX, Win32, Visual Studio
ms.localizationpriority: medium
---

# Grant package identity by packaging with external location

Many Windows features can be used by a desktop app only if that app has package identity at runtime.
See [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps).
If you have an existing desktop app, with its own installer, then there's very little you need to
change in order to benefit from package identity.

Starting in Windows 10, version 2004, you can grant package identity to an app simply by building
and registering a *package with external location* with your app. Packaging with external location
allows you to register a simple identity package in your existing installer without changing how or
where you install your application. You might be familiar with full MSIX packaging; this is a much
lighter-weight option.

You can [add an identity package to an existing Visual Studio project](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps-visual-studio) with the Windows Application Packaging Project and
[Package with External Location](https://marketplace.visualstudio.com/items?itemName=WapProj-PackageWithExternalLocation.wapprojPackageWithExternalLocation) extension.
This approach is recommended when there is a single application project that needs identity.
The tooling provides a visual manifest editor, visual Resource Designer for localization, graphical
wizard for creating and trusting self-signed certificates, automatic updating of application manifests,
and PowerShell scripts to register and unregister the identity package for local testing.

If you don't build with Visual Studio or want to bundle multiple application executables under a
shared identity, you can
[build an identity package manually](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).
