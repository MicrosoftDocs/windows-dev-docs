---
author: normesta
description: This guide helps you to create Fluent-based UWP UIs directly in your WPF and Windows Forms applications
title: Host UWP controls in WPF and Windows Forms applications
ms.author: normesta
ms.date: 05/03/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp, windows forms, wpf
keywords: windows 10, uwp, windows forms, wpf
ms.localizationpriority: medium
---

# Host UWP controls in WPF and Windows Forms applications

> [!NOTE]
> Some information relates to pre-released product which may be substantially modified before it’s commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.

We're bringing UWP controls to the desktop so that you can enhance the look, feel, and functionality of your existing WPF or Windows applications with Fluent Design features. There's two ways to do this.

First, you can add controls directly to the design surface of your WPF or Windows Forms project, and then use them like any other control in your designer.  Try this out today with the new **WebView** control. This control uses the Microsoft Edge rendering engine, and until now, this control was available only to UWP applications. You can find the **WebView** in the latest release of the [Windows Community Toolkit](https://docs.microsoft.com/windows/uwpcommunitytoolkit/).

Soon, you'll have access to even more Fluent Design features: we'll be providing a control that lets you host a variety of UWP controls. Look for this control and many other controls in future releases of the Windows Community Toolkit.

Here's a quick look at how these controls are organized architecturally. The names used in this diagram are subject to change.  

![Host control Architecture](images/host-controls.png)

The APIs that appear at the bottom of this diagram ship with the Windows SDK.  The controls that you'll add to your designer ship as Nuget packages in the Windows Community Toolkit.

These new controls have limitations so before you use them, please take a moment to review what's not yet supported, and what's functional only with workarounds.

### What's supported

For the most part, everything is supported unless explicitly called out in the list below.

### What's supported only with workarounds

:heavy_check_mark: Hosting multiple inbox controls inside of multiple windows. You'll have to place each window in its own thread.

:heavy_check_mark: Using ``x:Bind`` with hosted controls. You'll have to declare the data model in a .NET Standard library.

:heavy_check_mark: C#-based third-party controls. If you have the source code to a third-party control, you can compile against it.

### What's not yet supported

:no_entry_sign: Accessibility tools that work seamlessly across the application and hosted controls.

:no_entry_sign: Localized content in controls that you add to applications which don't contain a Windows app package.

:no_entry_sign: Asset references made in XAML within applications that don't contain a Windows app package.

:no_entry_sign: Controls properly responding to changes in DPI and scale.

:no_entry_sign: Adding a **WebView** control to a custom user control, (Either on-thread, off-thread, or out of proc).

:no_entry_sign: The [Reveal highlight](https://docs.microsoft.com/windows/uwp/design/style/reveal) Fluent effect.

:no_entry_sign: Inline inking, @Places, and @People for input controls.

:no_entry_sign: Assigning accelerator keys.

:no_entry_sign: C++-based third-party controls.

:no_entry_sign: Hosting custom user controls.

The items in this list will likely change as we continue to improve the experience of bringing Fluent to the desktop.  
