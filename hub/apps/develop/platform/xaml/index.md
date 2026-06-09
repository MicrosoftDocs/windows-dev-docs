---
ms.assetid: b632a6cc-3503-4ab8-bfd1-dde731bd89ab
description: This section includes topics that explain the XAML framework for WinUI apps.
title: XAML platform
ms.date: 09/08/2025
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# XAML platform

This section includes topics that explain programming concepts that are generally applicable to any app that you write using the Windows Runtime and XAML for your UI definition.

| Topic | Description |
|-------|-------------|
| [XAML overview](xaml-overview.md) | Introduces the XAML language and concepts to the Windows Runtime app developer audience, and describes the different ways to declare objects and set attributes in XAML as it is used for creating a Windows Runtime app. |
| [Dependency properties overview](dependency-properties-overview.md) | Explains the dependency property system that is available when you write a Windows Runtime app with XAML definitions for UI. |
| [Custom dependency properties](custom-dependency-properties.md) | Explains how to define and implement custom dependency properties for a Windows Runtime app. |
| [Attached properties overview](attached-properties-overview.md) | Explains the concept of an attached property in XAML and provides some examples. |
| [Custom attached properties](custom-attached-properties.md) | Explains how to implement a XAML attached property as a dependency property and how to define the accessor convention that is necessary for your attached property to be usable in XAML. |
| [Events and routed events overview](events-and-routed-events-overview.md) | Describes the programming concept of events in a Windows Runtime app when using C#, Visual Basic, or C++/CX as your programming language and XAML for your UI definition. You can assign handlers for events as part of the declarations for UI elements in XAML, or you can add the handlers in code. Windows Runtime supports *routed events*: certain input events and data events can be handled by objects beyond the object that fired the event. Routed events are useful when you define control templates or use pages or layout containers. |
