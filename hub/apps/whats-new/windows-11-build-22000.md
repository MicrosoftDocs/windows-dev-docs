---
title: What's New for Developers in Windows 11
description: Learn what's new for developers in Windows 11
keywords: what's new, Windows 11, Windows, developers, WinUI, sdk
ms.date: 5/24/2024
ms.topic: article
ms.localizationpriority: medium
---

# What's New for developers in Windows 11

Following the announcements at Microsoft Build 2024, here are some of the latest highlights for Windows developers:

* Updated recommendations for app development
* New AI and machine learning capabilities supported by Windows
* Enhanced developer tools


##  Development recommendations

You are strongly encouraged to consider using either the Windows App SDK/WinUI or WPF as your development platform for creating Windows client applications.


## Windows App SDK

Feature | Description
:------ | :------
Windows App SDK | [The Windows App SDK](../windows-app-sdk/index.md) is a set of developer components and tools that represent the next evolution in the Windows app development platform. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on Windows 11 (and downlevel to Windows 10, version 1809).
Windows App SDK release notes | Details on [the latest stable release of the Windows App SDK](../windows-app-sdk/stable-channel.md), which can be used by apps in production environments and by apps published to the Microsoft Store.
Create a new app with the Windows App SDK | The Windows App SDK includes WinUI 3 project templates that enable you to create apps with an entirely WinUI-based user interface. When you create a project using these templates (see [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md)), the entire user interface of your application is implemented using windows, controls, and other UI types provided by WinUI 3.
Use the Windows App SDK in an existing project | If you have an existing project in which you want to use the Windows App SDK, [you can install the latest version of the Windows App SDK NuGet package in your project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md). Unpackaged apps must follow this procedure to use the Windows App SDK, but packaged apps can do this too.
Download the Windows App SDK | There are several packages and release channels for the Windows App SDK. The [Download the Windows App SDK page](../windows-app-sdk/downloads.md) provides guidance on which ones you need, download links, and installation instructions.

## WinUI

Feature | Description
:------ | :------
[WinUI](../winui/winui3/index.md) | WinUI is the native UI platform component that ships with the Windows App SDK (completely decoupled from Windows SDKs). The Windows App SDK provides a unified set of APIs and tools that can be used to create production desktop apps that target Windows 10 and later, and can be published to the Microsoft Store.

## Windows AI

Feature | Description
:------ | :------
[Windows AI](/windows/ai/) | Enhance your Windows apps with AI through local APIs and Machine Learning models.
[Windows Copilot Runtime Overview](/windows/ai/overview) | Windows Copilot Runtime introduces new ways of interacting with the operating system that utilize AI, such as Phi Silica, the Small Language Model (SLM) created by Microsoft Research that is able to offer many of the same capabilities found in Large Language Models (LLMs), but more compact and efficient so that it can run locally on Windows.

## Developer tools
Feature | Description
:------ | :------
[Dev Home Overview](/windows/dev-home/) | Dev Home is a new control center for Windows providing the ability to monitor projects in your dashboard using customizable widgets, set up your dev environment by downloading apps, packages, or repositories, connect to your developer accounts and tools (such as GitHub), and create a Dev Drive for storage all in one place.
[WSL (Windows Subsystem for Linux)](/windows/wsl/) | Windows Subsystem for Linux (WSL) is a feature of Windows that allows you to run a Linux environment on your Windows machine, without the need for a separate virtual machine or dual booting.



## Samples

The [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery) on GitHub is updated regularly to showcase the latest additions and improvements to WinUI in the Windows App SDK. The gallery app can also be downloaded from the [Microsoft Store](https://apps.microsoft.com/detail/9p3jfpwwdzrc).
