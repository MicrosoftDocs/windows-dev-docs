---
description: Developer guidance for integrating your app with Windows system components and platform features — including Connected Experiences for cross-device scenarios, Widgets, Search, and more.
title: Integrate with Windows
ms.topic: concept-article
ms.date: 06/25/2026
ms.localizationpriority: medium
keywords: windows integration, connected experiences, cross-device windows, windows share, windows resume, people on windows
author: GrantMeStrength
ms.author: jken
# customer intent: As a Windows developer, I want to learn how to integrate my app with Windows so that I can provide a seamless experience for my users.
---

# Integrate with Windows

Windows provides a rich set of platform contracts that let your app participate in shell experiences — from sharing content and surfacing contacts to resuming tasks across devices. This section covers how to integrate with those platform features, including cross-device experiences.

## Windows Share

| Article | Description |
|--|--|
| [Windows Share overview](./integrate-sharesheet-overview.md) | Overview of how to integrate your app with the Windows Share Sheet. |
| [Share content from your app](./integrate-sharesheet-send.md) | Implement the Share contract so users can send content from your app. Covers packaged, PWA, and unpackaged apps. |
| [Receive shared content in your app](./integrate-sharesheet-receive.md) | Register as a Share Target and handle incoming shared content. Covers packaged, PWA, and unpackaged apps. |
| [Integrate packaged apps with Windows Share](./integrate-sharesheet-packaged.md) | How to register and activate a packaged app as a share target. |
| [Integrate unpackaged apps with Windows Share](./integrate-sharesheet-unpackaged.md) | How to grant package identity and register an unpackaged app as a share target. |
| [Integrate PWAs with Windows Share](./integrate-sharesheet-pwa.md) | How to integrate a Progressive Web App with the Windows Share Sheet. |

## Windows system components

| Feature | Description |
|--|--|
| [Feed providers](../feeds/feed-providers.md) | Integrate into the Windows feeds experience. |
| [Microsoft Copilot hardware key providers](microsoft-copilot-key-provider.md) | Register as the launch app for the Microsoft Copilot hardware key. |
| [Search providers](../search/search-providers.md) | Integrate into the Windows Search experience. |
| [Widget providers](../widgets/widget-providers.md) | Implement a Windows widget service provider to support your app. |

## Other integration features

| Feature | Description |
|--|--|
| [Connected Experiences](connected-experiences-overview.md) | Developer documentation for Connected Experiences, covering Share, People, and Resume integrations across Windows surfaces. |
| [Smart App Control](../smart-app-control/overview.md) | Protect users from untrusted or potentially dangerous code using Microsoft's app intelligence services and Windows code integrity features. |

## Related content

- [Speech, voice, and conversation in Windows](../speech.md)
- [Audio, video, and camera](../audio-video-camera.md)
