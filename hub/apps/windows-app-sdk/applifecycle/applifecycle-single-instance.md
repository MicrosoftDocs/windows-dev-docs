---
description: Describes how to use app instancing features with the app lifecycle API in WinUI with the Windows App SDK.
title: How to create a single-instanced WinUI app
ms.topic: how-to
ms.date: 09/20/2024
keywords: AppLifecycle, Windows, ApplicationModel, instancing, single instance, multi instance
#customer intent: As a Windows developer, I want to learn how to create a single-instanced WinUI 3 app so that I can ensure only one instance of my app is running at a time.
---

# Create a single-instanced WinUI app

Single-instanced apps only allow one instance of the app running at a time. WinUI apps are multi-instanced by default. They allow you to launch multiple instances of the same app simultaneously. That's referred to a multiple instances. However, you may want to implement single-instancing based on the use case of your app. Attempting to launch a second instance of a single-instanced app will only result in the first instance’s main window being activated instead. This tutorial demonstrates how to implement single-instancing in a WinUI app.

In this article, you will learn how to:

> [!div class="checklist"]
> - Turn off XAML's generated `Program` code
> - Define customized `MAIN` method for redirection
> - Test single-instancing after app deployment

## Pre-requisites

This tutorial uses Visual Studio and builds on the WinUI 3 blank app template. To get set up, follow the instructions in [Get started with WinUI](../../get-started/start-here.md). You'll install Visual Studio, configure it for developing apps with WinUI, create the Hello World project, and make sure you have the latest version of WinUI.

When you've done that, come back here to learn how to make Hello World project into a single-instanced app.

## Disable auto-generated Program code
