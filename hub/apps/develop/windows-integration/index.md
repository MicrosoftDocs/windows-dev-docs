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
| [Integrate packaged apps with Windows Share](./integrate-sharesheet-overview.md) | How to register and activate a packaged app as a share target. |
| [Integrate unpackaged apps with Windows Share](./integrate-sharesheet-receive.md#2-handle-the-share-activation) | How to grant package identity and register an unpackaged app as a share target. |
| [Integrate PWAs with Windows Share](./integrate-sharesheet-overview.md) | How to integrate a Progressive Web App with the Windows Share Sheet. |

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

## Shell and desktop integration

| Feature | Description |
|--|--|
| [Jump lists](jump-list.md) | Customize your app's Jump List on the taskbar to surface recently used items, tasks, and pinned destinations. |
| [Pin your app to the taskbar](pin-to-taskbar.md) | Programmatically request users to pin your Win32 or WinUI app to the taskbar. |
| [Default apps and file type associations](default-apps-platform.md) | Register your app as a handler for file types, URI schemes, or protocols. |
| [Drag and drop](../data/drag-and-drop.md) | Transfer data between or within apps using the XAML drag-and-drop APIs. |
| [App notifications](../notifications/app-notifications/app-notifications-quickstart.md) | Send toast notifications to engage users with timely and relevant content. |

## Related content

- [Speech, voice, and conversation in Windows](/windows/uwp/ui-input/speech-interactions)
- [Audio, video, and camera](../audio-video-camera.md)
