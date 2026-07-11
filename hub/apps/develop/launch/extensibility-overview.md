---
title: Extend your app with services, extensions, and packages
description: Learn the different extensibility technologies for Windows App SDK desktop apps, including app services, app extensions, and package extensions.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 07/08/2026
---

# Extend your app with services, extensions, and packages

Windows provides several technologies that let your app provide functionality to other apps or consume third-party add-ins. This article compares the available extensibility options for Windows App SDK desktop apps.

## Extensibility options overview

| Technology | Description | Package identity required | Minimum OS |
|-----------|-------------|---------------------------|------------|
| [App services](app-services.md) | Request/response communication between apps via `AppServiceConnection` | Yes | Windows 10 1607 |
| [App extensions](app-extensions.md) | Plug-in model — host app discovers content from extension packages | Yes | Windows 10 1607 |
| [Package extensions](package-extensions.md) | Broader package-level extensibility with `uap17:PackageExtension` | Yes | Windows 11 |
| Optional packages | Additional content packages that supplement a main app | Yes | Windows 10 1709 |
| Resource packages | Language, scale, and accessibility assets separated by market | Yes | Windows 10 |

## Choosing the right technology

### Use app services when

- You need two-way communication between separate apps.
- The consumer app sends a request and waits for a response.
- You want to expose an API-like interface to other apps.

Example: A translation service that other apps can call to translate text.

### Use app extensions when

- Your app needs a plug-in model where third parties provide content, themes, or add-ins.
- Extensions are discovered at runtime from installed packages.
- Extensions provide data or configuration, not executable code (code execution should use app services).

Example: An image editor that discovers filter packs from installed extension packages.

### Use package extensions when

- You need broader package-level extensibility on Windows 11.
- Extensions need access to more package content than the `PublicFolder` model allows.

### Use optional packages when

- You have additional content (DLC, premium features) distributed as separate packages.
- Content is authored by the same publisher.

## Architecture patterns

### App service with extension discovery

Combine app extensions with app services for a full plug-in architecture:

1. Your host app uses `AppExtensionCatalog` to discover installed extensions.
2. Each extension declares properties that describe its capabilities.
3. When the user activates an extension, the host app connects to the extension's app service for two-way communication.

```text
┌─────────────────┐      ┌──────────────────┐
│   Host app       │      │  Extension app    │
│                  │      │                   │
│ AppExtension     │◄────►│ AppExtension      │
│   Catalog        │      │   declaration     │
│                  │      │                   │
│ AppService       │◄────►│ AppService        │
│   Connection     │      │   provider        │
└─────────────────┘      └──────────────────┘
```

### Content extension only

For simpler scenarios where extensions provide static content (themes, templates, data files):

1. The host app discovers extensions through `AppExtensionCatalog`.
2. It reads files from the extension's `PublicFolder`.
3. No app service is needed.

## Differences from UWP extensibility

The extensibility technologies described here work the same way in Windows App SDK desktop apps as they do in UWP, with one requirement: **MSIX package identity**. All extensibility features rely on the package manifest for declarations and the package catalog for discovery.

If your desktop app is unpackaged, you cannot use these extensibility technologies. Consider alternative approaches such as:

- COM-based plug-in interfaces
- File-system-based extension discovery
- Named pipes or other IPC mechanisms

## Related content

- [App services](app-services.md)
- [App extensions](app-extensions.md)
- [Package extensions](package-extensions.md)
- [Hosted apps](hosted-apps.md)
