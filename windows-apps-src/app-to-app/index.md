---
ms.assetid: E0728EB0-DFC3-4203-A367-8997B16E2328
description: This section explains how to share data between Universal Windows Platform (UWP) apps, including how to use the Share contract, copy and paste, and drag and drop.
title: App-to-app communication
ms.date: 02/12/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# App-to-app communication

This section explains how to share data between Universal Windows Platform (UWP) apps, including how to use the Share contract, copy and paste, drag and drop, and app services.

The Share contract is one way users can quickly exchange data between apps. For example, a user might want to share a webpage with their friends using a social networking app, or save a link in a notes app to refer to later. Consider using a Share contract if your app receives content in scenarios that a user can quickly complete while in the context of another app.

An app can support the Share feature in two ways. First, it can be a [source app that provides content that the user wants to share](share-data.md). Second, the app can be a [target app that the user selects as the destination for shared content](receive-data.md). An app can also be both a source app and a target app. If you want your app to share content as a source app, you need to decide what data formats your app can provide.

In addition to the Share contract, apps can also integrate classic techniques for transferring data, such as [dragging and dropping](../design/input/drag-and-drop.md) or [copy and pasting](copy-and-paste.md). In addition to communication between UWP apps, these methods also support sharing to and from desktop applications.

UWP apps can also create [app services](../launch-resume/how-to-create-and-consume-an-app-service.md) that provide functionality to other UWP apps. An app service runs as a background task in the host app and can provide its service to other apps. For example, an app service might provide a bar code scanner service that other apps could use.

## In this section

| Topic | Description |
|-------|-------------|
| [Share data](share-data.md) | This article explains how to support the Share contract in a UWP app. The Share contract is an easy way to quickly share data, such as text, links, photos, and videos, between apps. For example, a user might want to share a webpage with their friends using a social networking app, or save a link in a notes app to refer to later. |
| [Receive data](receive-data.md) | This article explains how to receive content in your UWP app shared from another app using Share contract. This Share contract allows your app to be presented as an option when the user invokes Share. |
| [Copy and paste](copy-and-paste.md) | This article explains how to support copy and paste in UWP apps using the clipboard. Copy and paste is the classic way to exchange data either between apps, or within an app, and almost every app can support clipboard operations to some degree. |
| [Drag and drop](../design/input/drag-and-drop.md) | This article explains how to add dragging and dropping in your UWP app. Drag and drop is a classic, natural way of interacting with content such as images and files. Once implemented, drag and drop works seamlessly in all directions, including app-to-app, app-to-desktop, and desktop-to app. |
| [Create and consume an app service](../launch-resume/how-to-create-and-consume-an-app-service.md) | This article explains how to create an app service in a UWP app that provides services to other UWP apps.  |

## See also

- [Develop UWP apps](../develop/index.md)