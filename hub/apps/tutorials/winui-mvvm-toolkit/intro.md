---
title: Data binding with WinUI and MVVM Toolkit tutorial - Introduction
description: Implement data binding, dependency injection, and unit testing in WinUI apps with MVVM Toolkit. Build maintainable Windows applications today.
ms.date: 10/29/2025
ms.topic: tutorial
keywords: windows 11, windows app sdk, winui, windows ui, mvvm, mvvm toolkit, dotnet
ms.localizationpriority: medium
# customer intent: As a Windows developer, I want to learn how to implement data binding, dependency injection, and unit testing in WinUI apps using the MVVM Toolkit so that I can build maintainable and testable applications.
---

# Data binding, dependency injection, and unit testing in WinUI

This tutorial series demonstrates how to implement data binding, dependency injection, and unit testing with the Model-View-ViewModel (MVVM) design pattern and the [MVVM Toolkit](/dotnet/communitytoolkit/mvvm/) in a WinUI app. It builds on the [Create a WinUI app](/windows/apps/tutorials/winui-notes/intro) tutorial and shows you how to update your view models to leverage the MVVM Toolkit and the differences between the MVVM Toolkit and traditional MVVM approaches.

You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes-mvvm-toolkit).

In this tutorial, you learn how to:

> [!div class="checklist"]
>
> - Understand the differences between the MVVM Toolkit and traditional MVVM approaches.
> - Create a separate class library project to hold ViewModels and services for improved testability.
> - Implement data binding in your WinUI app using the MVVM Toolkit.
> - Add `AllNotesViewModel` and `NoteViewModel` view models to leverage the MVVM Toolkit.
> - Integrate dependency injection to manage ViewModels and services.
> - Create a unit test project to test your ViewModels and services.

## Prerequisites

To complete this tutorial, you need the following prerequisites:

- [Visual Studio 2026](https://visualstudio.microsoft.com/vs/) with the **WinUI application development** workload installed.
- A starter project created by following the steps in the [Create a WinUI app](/windows/apps/tutorials/winui-notes/intro) tutorial. If you're already familiar with the tutorial, you can download the code to get started here from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes).

## What is the MVVM Toolkit?

The MVVM Toolkit is a modern, lightweight, and fast library that helps you implement the MVVM design pattern in your .NET applications. It's part of the [.NET Community Toolkit](/dotnet/communitytoolkit/introduction) and provides a set of tools and utilities to simplify the development of MVVM-based applications. The MVVM Toolkit includes features such as:

- **ObservableObject**: A base class that implements the `INotifyPropertyChanged` interface, so you can create view models that notify the view of property changes.
- **RelayCommand**: A command implementation that lets you bind UI actions to methods in your view model.
- **Messenger**: A messaging system that enables communication between different parts of your application without tight coupling.
- **Attributes**: A set of attributes that you can use to generate boilerplate code, such as property change notifications and command implementations.
- **Source Generators**: Compile-time code generation that reduces boilerplate and improves performance.
- **Dependency Injection Support**: Built-in support for dependency injection to manage the lifecycle of view models and services.

The MVVM Toolkit is designed to be easy to use and integrate into your existing projects. It's compatible with various .NET platforms, including WinUI, WPF, and .NET MAUI. You can check out some sample apps on the [GitHub repo](https://github.com/CommunityToolkit/MVVM-Samples) or the [sample app](https://aka.ms/mvvmtoolkit/app) on the Microsoft Store to see how the MVVM Toolkit can be used in different scenarios.

## How does the MVVM Toolkit compare to traditional MVVM approaches?

The MVVM Toolkit reduces the amount of boilerplate ViewModel code and simplifies many aspects of implementing the MVVM design pattern compared to traditional approaches. Here are some key differences:

| Feature | Traditional MVVM approach | MVVM Toolkit approach |
|---------|---------------------------|-----------------------|
| Property Change Notification | Manually implement `INotifyPropertyChanged` in a base class and raise `PropertyChanged` events for each property. | Inherit from `ObservableObject` and use the `SetProperty` method to automatically raise `PropertyChanged` events. |
| Command Implementation | Manually implement `ICommand` for each command. | Use `RelayCommand` to easily create commands with minimal boilerplate. |
| Messaging | Implement custom messaging systems or use third-party libraries. | Use the built-in `Messenger` class for decoupled communication between components. |
| Boilerplate Code | Write repetitive code for property change notifications and command implementations. | Use attributes and source generators to reduce boilerplate code. |
| Performance | Might have performance overhead due to reflection and runtime code generation. | Source generators provide compile-time code generation, improving performance. |
| Dependency Injection Support | Requires manual setup and management of view model lifecycles. | Built-in support for dependency injection to manage view model lifecycles. |
| Learning Curve | Might require a deeper understanding of MVVM concepts and patterns. | Easier to learn and use with a focus on simplicity and productivity. |

For more background on the MVVM design pattern, see [Windows data binding and MVVM](/windows/apps/develop/data-binding/data-binding-and-mvvm), [Model-View-ViewModel (MVVM)](/dotnet/architecture/maui/mvvm), and the reference documentation for the [INotifyPropertyChanged Interface](/dotnet/api/system.componentmodel.inotifypropertychanged).

## The WinUI Notes app

The final application you build in this tutorial is a refactored version of the WinUI Notes app from the [Create a WinUI app](/windows/apps/tutorials/winui-notes/intro) tutorial. The app lets users create, save, and load multiple notes. The user interface of the original app stays the same, but the updated architecture uses the MVVM Toolkit for data binding and view model management.

_AllNotesPage_

:::image type="content" border="false" source="../winui-notes/media/intro/final-all-notes.png" alt-text="Screenshot of WinUI Notes app displaying three saved notes in the AllNotesPage view.":::

_NotePage_

:::image type="content" border="false" source="../winui-notes/media/intro/final-note.png" alt-text="Screenshot of WinUI Notes app showing a blank note page in the NotePage view.":::

> [!TIP]
> When you build Windows apps, you often refer to API reference docs and conceptual docs. In this tutorial, you see links inline in the text, and in groups labeled, "Learn more in the docs:". These links are optional; you don't need to follow them to complete the tutorial. They're provided in case you want to make note of where to find the information you'll need when you start to create your own apps.

> [!div class="nextstepaction"]
> [Continue to step 1 - Create a separate class library project](class-library.md)
