---
ms.assetid: b7a8ec88-3013-4e5a-a110-fab3f20ee4bf
title: Windows data binding and MVVM
description: Learn how data binding in the Model-View-ViewModel (MVVM) pattern enables loose coupling between UI and non-UI code for better maintainability.
author: GrantMeStrength
ms.author: jken
ms.date: 07/15/2026
ms.topic: concept-article
keywords: windows 10, windows 11, windows app sdk, winui, windows ui, mvvm
ms.localizationpriority: medium
# customer intent: As a Windows developer, I want to learn how data binding in the Model-View-ViewModel (MVVM) pattern enables loose coupling between UI and non-UI code so that I can improve maintainability.
---

# Windows data binding and MVVM

Model-View-ViewModel (MVVM) is a UI architectural design pattern that decouples UI and non-UI code. Learn how MVVM enables loose coupling using data binding in XAML to synchronize UI and data, improving maintainability and reducing dependencies.

Because it provides loose coupling, the use of data binding reduces hard dependencies between different kinds of code. This approach makes it easier to change individual code units (methods, classes, controls, and so on) without causing unintended side effects in other units. This decoupling is an example of the *separation of concerns*, which is an important concept in many design patterns.

## Benefits of MVVM

Decoupling your code has many benefits, including:

* Enabling an iterative, exploratory coding style. Change that is isolated is less risky and easier to experiment with.
* Simplifying unit testing. You can test code units that are isolated from one another individually and outside of production environments.
* Supporting team collaboration. Separate individuals or teams can develop decoupled code that adheres to well-designed interfaces and integrate it later.
* Improving maintainability. Fixing bugs in decoupled code is less likely to cause regressions in other code.

In contrast with MVVM, an app with a more conventional "code-behind" structure typically uses data binding for display-only data. It responds to user input by directly handling events exposed by controls. The event handlers are implemented in code-behind files (such as MainWindow.xaml.cs) and are often tightly coupled to the controls. They typically contain code that manipulates the UI directly. This structure makes it difficult or impossible to replace a control without having to update the event handling code. With this architecture, code-behind files often accumulate code that isn't directly related to the UI, such as database-access code, which ends up being duplicated and modified for use with other windows.

## App layers

When you use the MVVM pattern, divide your app into the following layers:

* The **model** layer defines the types that represent your business data. This layer includes everything required to model the core app domain and often includes core app logic. This layer is completely independent of the view and view-model layers and often resides partially in the cloud. Given a fully implemented model layer, you can create multiple different client apps if you choose, such as Windows App SDK and web apps that work with the same underlying data.
* The **view** layer defines the UI by using XAML markup. The markup includes data binding expressions (such as [x:Bind](/windows/apps/develop/platform/xaml/x-bind-markup-extension)) that define the connection between specific UI components and various view-model and model members. You can sometimes use code-behind files as part of the view layer to contain additional code needed to customize or manipulate the UI or to extract data from event handler arguments before calling a view-model method that performs the work.
* The **view-model** layer provides data binding targets for the view. In many cases, the view-model exposes the model directly or provides members that wrap specific model members. The view-model can also define members for keeping track of data that is relevant to the UI but not to the model, such as the display order of a list of items. The view-model also serves as an integration point with other services such as data access code. For simple projects, you might not need a separate model layer, but only a view-model that encapsulates all the data you need.

## Basic and advanced MVVM

As with any design pattern, there is more than one way to implement MVVM, and many different techniques are considered part of MVVM. For this reason, there are several different third-party MVVM frameworks supporting the various XAML-based platforms, including Windows App SDK. However, these frameworks generally include multiple services for implementing decoupled architecture, making the exact definition of MVVM somewhat ambiguous.

Although sophisticated MVVM frameworks can be very useful, especially for enterprise-scale projects, there is typically a cost associated with adopting any particular pattern or technique, and the benefits are not always clear, depending on the scale and size of your project. Fortunately, you can adopt only those techniques that provide a clear and tangible benefit, and ignore others until you need them.

In particular, you can get a lot of benefit simply by understanding and applying the full power of data binding and separating your app logic into the layers described earlier. This can be achieved using only the capabilities provided by the Windows App SDK, and without using any external frameworks. In particular, the [{x:Bind} markup extension](/windows/apps/develop/platform/xaml/x-bind-markup-extension) makes data binding easier and higher performing than in previous XAML platforms, eliminating the need for a lot of the boilerplate code required earlier.

For additional ready-to-use MVVM infrastructure in WinUI 3 apps, the [MVVM Toolkit](/dotnet/communitytoolkit/mvvm/) (part of the .NET Community Toolkit) provides source-generated `ObservableProperty`, `RelayCommand`, and messaging primitives that eliminate boilerplate and work with `{x:Bind}`.

For a sample that demonstrates MVVM with WinUI 3, see the [WinUI 3 Gallery](https://github.com/microsoft/WinUI-Gallery) on GitHub, which uses the pattern throughout its source code.

## See also

### Topics

[Data binding in depth](data-binding-in-depth.md)  
[{x:Bind} markup extension](/windows/apps/develop/platform/xaml/x-bind-markup-extension)  
[MVVM performance tips for WinUI apps](../performance/mvvm-performance-tips.md)

### MVVM frameworks and samples

[MVVM Toolkit documentation](/dotnet/communitytoolkit/mvvm/)  
[WinUI 3 Gallery source (GitHub)](https://github.com/microsoft/WinUI-Gallery)  
[Template Studio for WinUI (GitHub)](https://github.com/microsoft/TemplateStudio)
