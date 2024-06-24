---
title: Overall migration strategy
description: Considerations and strategies for approaching the migration process, and how to set up your development environment for migrating.
ms.topic: article
ms.date: 10/01/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# Overall migration strategy

## Introduction

The Windows App SDK provides a broad set of Windows APIs&mdash;with implementations that are decoupled from the OS, and released to developers via [NuGet](https://www.nuget.org/) packages. As a developer with a Universal Windows Platform (UWP) application, you can make great use of your existing skill set, and your source code, by moving your app to the Windows App SDK.

With the Windows App SDK you can incorporate the latest runtime, language, and platform features into your app. Since each application is different, and so are your requirements and preferences, there are many different ways to approach the process of migrating your app's source code. But the high-level approach, and code changes needed for various feature areas, are similar. So in this topic we'll review strategies on how you can approach migrating your app, as well as more info (and limitations) about certain feature areas. So also see [What's supported when porting from UWP to WinUI 3](what-is-supported.md).

Most [Windows Runtime (WinRT)](/uwp/api/) APIs can be used by Windows App SDK apps. But there are some that aren't supported in desktop apps, or have restrictions.

* APIs that have dependencies on UI features that were designed for use only in UWP apps.
* APIs that require package identity. These APIs are supported only in desktop apps that are packaged using MSIX.

For those APIs, we'll show you what alternatives to use. Most of those alternatives are available in [WinUI](../../winui/index.md), or via WinRT COM interfaces that are available in the Windows App SDK.

For example, we'll see certain UI scenarios where you'll need to track your main window object, and use various **HWND**-based APIs and interoperation patterns, such as [**IInitializeWithWindow::Initialize**](/windows/win32/api/shobjidl_core/nf-shobjidl_core-iinitializewithwindow-initialize).

> [!NOTE]
> Also see [Windows Runtime APIs not supported in desktop apps](../../desktop/modernize/desktop-to-uwp-supported-api.md). Windows App SDK apps are *one* kind of desktop app. Other kinds of desktop app include .NET desktop apps, and C/C++ Win32 desktop apps. The audience of that topic is developers wishing to migrate to anything in the union of those different kinds of desktop app, including (but not limited to) Windows App SDK apps.

We'd love to hear your feedback about this migration guide, and about your own migration experience. Use the **Feedback** section right at the foot of this page like this:
* For questions and feedback about the Windows App SDK, or just to start a discussion, use the **This product** button. You can also start a discussion on the [Discussions tab](https://github.com/microsoft/WindowsAppSDK/discussions) of the **WindowsAppSDK** GitHub repo. Using those channels, you could also tell us what problem you're trying to solve, how you've tried to solve it so far, and what would be the ideal solution for your app.
* For feedback about missing or incorrect information in this migration guide, use the **This page** button.

## Why migrate to the Windows App SDK?

The Windows App SDK offers you an opportunity to enhance your app with new platform features, as well as with the modern [Windows UI 3 Library (WinUI 3)](../../winui/index.md), which is developed and designed to delight your users.

In addition, the Windows App SDK is backward compatible; it supports apps down to Windows 10, version 1809 (10.0; Build 17763)&mdash;also known as the Windows 10 October 2018 Update.

The value proposition of moving the Windows App SDK is manifold. Here are some considerations:

* Latest user interface (UI) platform and controls such as [WinUI](../../winui/index.md) 3 and [WebView2](/microsoft-edge/webview2/).
* A single API surface across desktop app platforms.
* More frequent release cadence that releases separately from Windows.
* A consistent experience across Windows versions.
* .NET compatibility.
* Backward-compatible down to Windows 10, version 1809.
* Improved runtime environment. See [MSIX container](/windows/msix/msix-container).

For more info, see [Windows App SDK](../index.md).

## Overview of the migration process

> [!NOTE]
> You can think of the UWP version of the app that you want to migrate as the *source* solution/project (it's the *source* of the migration process). The Windows App SDK version is the *target* solution (it's the *target* of the migration process).

## Install the Windows App SDK VSIX

Download the Windows App SDK Visual Studio extension (VSIX) installer from [Stable release channel for the Windows App SDK](../stable-channel.md), and run to install it.

## Create a new project

In Visual Studio, [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md). For example, use the **Blank App, Packaged (WinUI 3 in Desktop)** project template. You can find that project template in the **Create a new project** dialog by choosing language: *C#* or *C++*; platform: *Windows App SDK*; project type: *WinUI* or *Desktop*.

You'll see two projects in **Solution Explorer**&mdash;one is qualified as **(Desktop)**, and the other as **(Package)**.

## Migrate code with the least dependencies first

To illustrate this recommendation, let's take the [PhotoLab case study](case-study-1.md) as an example. For the PhotoLab sample app, one perhaps obvious approach might be to begin by migrating **MainPage**&mdash;since that's such an important and prominent piece of the app. But if we were to do that, then we'd soon realize that **MainPage** has a dependency on the **DetailPage** view; and then that **DetailPage** has a dependency on the **Photo** model. If we were to follow that path, then we might be constantly interrupting ourselves (switching over to work on each newly discovered dependency). Certainly we wouldn't expect to get a clean *build* until we'd chased down every dependency, and essentially ported the whole project in one giant leap.

If on the other hand we were to begin with the piece of the project that doesn't depend on anything else, and work outward from there (from least- to most-dependent piece), then we'd be able to focus on each piece one at a time. And we'd also be able to achieve a clean build after migrating each piece, and in that way confirm that the migration process is staying on track.

So when you're migrating your own apps, we recommend that you migrate code with the least dependencies first.

## Copy files, or copy file contents?

When you migrate, you will of course be copying over asset *files* (and not asset file *contents*). But what about source code files? As an example let's again take the **MainPage** class from the [PhotoLab case study](case-study-1.md) and the [Photo Editor case study](case-study-2.md).

**C#**. In the C# version (PhotoLab), **MainPage** is made up of the source code files `MainPage.xaml` and `MainPage.xaml.cs`.

**C++/WinRT**. In the C++/WinRT version (Photo Editor), **MainPage** is made up of the source code files `MainPage.xaml`, `MainPage.idl`, `MainPage.h`, and `MainPage.cpp`.

So you have the choice between these two options:

* (Recommended) you can copy over *the files themselves* (using **File Explorer**), and then add the copies to the target project. Exceptions to this recommendation are files such as `App.xaml` and `App.xaml.cs`, since those files already exist in the target project, and they contain source code that's specific to the Windows App SDK. For those you'll need to *merge* the source code that's already there with source code from the source project.
* Or you can use Visual Studio to create new **Page** files (such as `MainPage.xaml` and `MainPage.xaml.cs`) in the target project, and then copy the *contents* of those source code files over from the source project to the target project. For a C++/WinRT project, this option involves a lot more steps.

Also see the section [MainPage and MainWindow](guides/windowing.md#mainpage-and-mainwindow).

## Folder and file name differences (C++/WinRT)

In a C++/WinRT UWP project, code-behind files for XAML views are named of the form `MainPage.h` and `MainPage.cpp`. But in a C++/WinRT Windows App SDK, you'll need to rename those to `MainPage.xaml.h` and `MainPage.xaml.cpp`.

In a C++/WinRT UWP project, when migrating a hypothetical XAML view (in the sense of models, views, and viewmodels) named **MyPage**, in `MyPage.xaml.cpp` you'll need to add `#include "MyPage.g.cpp"` immediately after `#include "DetailPage.xaml.h"`. And for a hypothetical model named **MyModel**, in `MyModel.cpp` add `#include "MyModel.g.cpp"` immediately after #include "MyModel.h". For an example, see [Migrate DetailPage source code](case-study-2.md#migrate-detailpage-source-code).

## If you change the name of your migrated project

While migrating, you might choose to give your target project a different name from that of the source project. If you do, then that will affect the default namespace within the source project. As you copy source code over from the source project to the target project, you might need to change namespace names that are mentioned in the source code.

Changing project name (and consequently default namespace name) is something that we do, for example, in the case study [A Windows App SDK migration of the UWP PhotoLab sample app (C#)](case-study-1.md). In that case study, instead of copying over file *contents* from the source to the target project, we copy entire files using File Explorer. The source project/namespace name is *PhotoLab*, and the target project/namespace name is *PhotoLabWinUI3*. So we need to search for *PhotoLab* in the contents of any source code files we copied over, and change that to *PhotoLabWinUI3*

In that particular case study, we made those changes in `App.xaml`, `App.xaml.cs`, `MainPage.xaml`, `MainPage.xaml.cs`, `DetailPage.xaml`, `DetailPage.xaml.cs`, `ImageFileInfo.cs`, and `LoadedImageBrush.cs`.

## Install the same NuGet packages that were installed in the source project

One task during the migration process is to identify any NuGet packages that the source project has dependencies on. And then install those same NuGet packages in the target project.

For example, in the case study [A Windows App SDK migration of the UWP PhotoLab sample app (C#)](case-study-1.md), the source project contains references to the **Microsoft.Graphics.Win2D** NuGet package. So in that case study we add a references to the same NuGet package to the target project.

## Related topics

* [Windows Runtime (WinRT)](/uwp/api/) APIs
* [WinUI](../../winui/index.md)
* [Stable release channel for the Windows App SDK](../stable-channel.md)
* [PhotoLab case study](case-study-1.md)
* [Photo Editor case study](case-study-2.md)
* [Windows Runtime APIs not supported in desktop apps](../../desktop/modernize/desktop-to-uwp-supported-api.md)
