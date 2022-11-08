---
ms.assetid: F46306EC-DFF3-4FF0-91A8-826C1F8C4A52
title: Data binding and MVVM
description: Data binding is at the core of the Model-View-ViewModel (MVVM) UI architectural design pattern, and enables loose coupling between UI and non-UI code.
ms.date: 10/02/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Data binding and MVVM

Model-View-ViewModel (MVVM) is a UI architectural design pattern for decoupling UI and non-UI code. With MVVM, you define your UI declaratively in XAML and use data binding markup to link it to other layers containing data and commands. The data binding infrastructure provides a loose coupling that keeps the UI and the linked data synchronized and routes user input to the appropriate commands. 

Because it provides loose coupling, the use of data binding reduces hard dependencies between different kinds of code. This makes it easier to change individual code units (methods, classes, controls, etc.) without causing unintended side effects in other units. This decoupling is an example of the *separation of concerns*, which is an important concept in many design patterns. 

## Benefits of MVVM

Decoupling your code has many benefits, including:

* Enabling an iterative, exploratory coding style. Change that is isolated is less risky and easier to experiment with.
* Simplifying unit testing. Code units that are isolated from one another can be tested individually and outside of production environments.
* Supporting team collaboration. Decoupled code that adheres to well-designed interfaces can be developed by separate individuals or teams, and integrated later.
* Improving maintainability. Fixing bugs in decoupled code is less likely to cause regressions in other code.

In contrast with MVVM, an app with a more conventional "code-behind" structure typically uses data binding for display-only data, and responds to user input by directly handling events exposed by controls. The event handlers are implemented in code-behind files (such as MainPage.xaml.cs), and are often tightly coupled to the controls, typically containing code that manipulates the UI directly. This makes it difficult or impossible to replace a control without having to update the event handling code. With this architecture, code-behind files often accumulate code that isn't directly related to the UI, such as database-access code, which ends up being duplicated and modified for use with other pages.

## App layers

When using the MVVM pattern, an app is divided into the following layers:

* The **model** layer defines the types that represent your business data. This includes everything required to model the core app domain, and often includes core app logic. This layer is completely independent of the view and view-model layers, and often resides partially in the cloud. Given a fully implemented model layer, you can create multiple different client apps if you so choose, such as UWP and web apps that work with the same underlying data.
* The **view** layer defines the UI using XAML markup. The markup includes data binding expressions (such as [x:Bind](../xaml-platform/x-bind-markup-extension.md)) that define the connection between specific UI components and various view-model and model members. Code-behind files are sometimes used as part of the view layer to contain additional code needed to customize or manipulate the UI, or to extract data from event handler arguments before calling a view-model method that performs the work. 
* The **view-model** layer provides data binding targets for the view. In many cases, the view-model exposes the model directly, or provides members that wrap specific model members. The view-model can also define members for keeping track of data that is relevant to the UI but not to the model, such as the display order of a list of items. The view-model also serves as an integration point with other services such as database-access code. For simple projects, you might not need a separate model layer, but only a view-model that encapsulates all the data you need. 

## Basic and advanced MVVM

As with any design pattern, there is more than one way to implement MVVM, and many different techniques are considered part of MVVM. For this reason, there are several different third-party MVVM frameworks supporting the various XAML-based platforms, including UWP. However, these frameworks generally include multiple services for implementing decoupled architecture, making the exact definition of MVVM somewhat ambiguous. 

Although sophisticated MVVM frameworks can be very useful, especially for enterprise-scale projects, there is typically a cost associated with adopting any particular pattern or technique, and the benefits are not always clear, depending on the scale and size of your project. Fortunately, you can adopt only those techniques that provide a clear and tangible benefit, and ignore others until you need them. 

In particular, you can get a lot of benefit simply by understanding and applying the full power of data binding and separating your app logic into the layers described earlier. This can be achieved using only the capabilities provided by the Windows SDK, and without using any external frameworks. In particular, the [{x:Bind} markup extension](../xaml-platform/x-bind-markup-extension.md) makes data binding easier and higher performing than in previous XAML platforms, eliminating the need for a lot of the boilerplate code required earlier.

For additional guidance on using basic, out-of-the-box MVVM, check out the [Customers Orders Database sample](https://github.com/Microsoft/Windows-appsample-customers-orders-database) on GitHub. Many of the other [UWP app samples](https://github.com/Microsoft?q=windows-appsample
) also use a basic MVVM architecture, and the [Traffic App sample](https://github.com/Microsoft/Windows-appsample-trafficapp) includes both code-behind and MVVM versions, with notes describing the [MVVM conversion](https://github.com/Microsoft/Windows-appsample-trafficapp/blob/MVVM/MVVM.md). 

## See also

### Topics

[Data binding in depth](./data-binding-in-depth.md)  
[{x:Bind} markup extension](../xaml-platform/x-bind-markup-extension.md)  

### Samples

[Customers Orders Database sample](https://github.com/Microsoft/Windows-appsample-customers-orders-database)  
[VanArsdel Inventory sample](https://github.com/Microsoft/InventorySample)  
[Traffic App sample](https://github.com/Microsoft/Windows-appsample-trafficapp)