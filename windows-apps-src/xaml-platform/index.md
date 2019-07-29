---
ms.assetid: b632a6cc-3503-4ab8-bfd1-dde731bd89ab
description: This section includes topics that explain the XAML framework for Universal Windows Platform (UWP) apps.
title: XAML platform
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# XAML platform

This section includes topics that explain programming concepts that are generally applicable to any app that you write using C#, Visual Basic, or Visual C++ component extensions (C++/CX) and XAML for your UI definition. The programming concepts include how to use properties and events and how they apply to Universal Windows Platform (UWP) app programming. The Universal Windows Platform extends C#, Visual Basic, or C++/CX concepts of properties and their values by adding the dependency property system. Topics in this section also document the XAML language as it's used by the UWP and basic to advanced scenarios about how to use XAML to define the UI for your UWP app.

| Topic | Description |
|-------|-------------|
| [XAML overview](xaml-overview.md) | Introduces the XAML language and concepts to the Windows Runtime app developer audience, and describes the different ways to declare objects and set attributes in XAML as it is used for creating a Windows Runtime app. |
| [Dependency properties overview](dependency-properties-overview.md) | Explains the dependency property system that is available when you write a Windows Runtime app using C++, C#, or Visual Basic along with XAML definitions for UI. |
| [Custom dependency properties](custom-dependency-properties.md) | Explains how to define and implement custom dependency properties for a Windows Runtime app using C++, C#, or Visual Basic. |
| [Attached properties overview](attached-properties-overview.md) | Explains the concept of an attached property in XAML and provides some examples. |
| [Custom attached properties](custom-attached-properties.md) | Explains how to implement a XAML attached property as a dependency property and how to define the accessor convention that is necessary for your attached property to be usable in XAML. |
| [Events and routed events overview](events-and-routed-events-overview.md) | Describes the programming concept of events in a Windows Runtime app when using C#, Visual Basic, or C++/CX as your programming language and XAML for your UI definition. You can assign handlers for events as part of the declarations for UI elements in XAML, or you can add the handlers in code. Windows Runtime supports *routed events*: certain input events and data events can be handled by objects beyond the object that fired the event. Routed events are useful when you define control templates or use pages or layout containers. |
|[UWP controls in desktop apps (XAML Islands)](/windows/apps/desktop/modernize/xaml-islands)| Explains how to use UWP XAML controls to enhance the UI of a Windows Forms, WPF, or Win32 desktop application.|
