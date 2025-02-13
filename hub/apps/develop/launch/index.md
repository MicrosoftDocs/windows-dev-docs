---
title: Launching Windows apps and managing background tasks
description: Learn how to use a Uniform Resource Identifier (URI) or a file type to launch a Windows app or manage background tasks in your apps.
ms.date: 02/11/2025
ms.topic: concept-article
keywords: windows, uwp, winrt, winui, windows 11, windows 10
ms.localizationpriority: medium
# customer-intent: As a Windows developer, I want to learn how to use a URI or a file type to launch a Windows app or manage background tasks in my apps.
---

# Launching Windows apps and managing background tasks

Learn how to use a Uniform Resource Identifier (URI) or a file type to launch a Windows app or manage background tasks in your apps.

## Launch a Windows app with a URI

This section describes how to use a URI to launch your Windows app.

| Topic | Description |
|-------|-------------|
| [Launch the default app for a URI](launch-default-app.md) | Learn how to launch the default app for a Uniform Resource Identifier (URI). URIs allow you to launch another app to perform a specific task. This topic also provides an overview of the many URI schemes built into Windows. |
| [Handle URI activation](handle-uri-activation.md) | Learn how to register an app to become the default handler for a Uniform Resource Identifier (URI) scheme name. |
| [Launch an app for results](/windows/uwp/launch-resume/how-to-launch-an-app-for-results) | Learn how to launch an app from another app and exchange data between the two. This is called launching an app for results. |
| [Launch the Windows Settings app](launch-settings-app.md) | Learn how to launch the Windows Settings app from your app. This topic describes the ms-settings URI scheme. Use this URI scheme to launch the Windows Settings app to specific settings pages. |
| [Launch the Microsoft Store app](launch-store-app.md) | This topic describes the ms-windows-store URI scheme. Your app can use this URI scheme to launch the Microsoft Store app to specific pages in the Store. |
| [Launch the Windows Maps app](launch-maps-app.md) | Learn how to launch the Windows Maps app from your app. |
| [Launch the People app](launch-people-app.md) | This topic describes the ms-people URI scheme. Your app can use this URI scheme to launch the People app for specific actions. |
| [Launch screen snipping](launch-screen-snipping.md) | Learn how to use URI schemes to open a new snip, or to open the Snip & Sketch app. |
| [Enable apps for websites using app URI handlers](web-to-app-linking.md) | Drive user engagement with your app by supporting the Apps for Websites feature. |

## Launch a Windows app through file activation

This section shows how to set up your app to launch when a file of a certain type is opened.

| Topic | Description |
|-------|-------------|
| [Launch the default app for a file](launch-the-default-app-for-a-file.md) | Learn how to launch the default app for a file. |
| [Handle file activation](handle-file-activation.md) | Learn how to register your app to become the default handler for a certain file type. |

## Manage background tasks in your app

This section describes how to manage background tasks in your app.

| Topic | Description |
|-------|-------------|
| [Working with background tasks in Windows apps](create-and-register-a-background-task.md) | Learn how to create and register a background task in your app with the Windows Runtime (WinRT) [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) class. |

## Related content

- [Launch Click to Do (Recall)](/windows/ai/apis/recall#launch-click-to-do)
- [App lifecycle and system services](../app-lifecycle-and-system-services.md)
