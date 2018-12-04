---
ms.assetid: 159681E4-BF9E-4A57-9FEE-EC7ED0BEFFAD
title: MVVM and language performance tips
description: This topic discusses some performance considerations related to your choice of software design patterns, and programming language.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# MVVM and language performance tips


This topic discusses some performance considerations related to your choice of software design patterns, and programming language.

## The Model-View-ViewModel (MVVM) pattern

The Model-View-ViewModel (MVVM) pattern is common in a lot of XAML apps. (MVVM is very similar to Fowler’s description of the Model-View-Presenter pattern, but it is tailored to XAML). The issue with the MVVM pattern is that it can inadvertently lead to apps that have too many layers and too many allocations. The motivations for MVVM are these.

-   **Separation of concerns**. It’s always helpful to divide a problem into smaller pieces, and a pattern like MVVM or MVC is a way to divide an app (or even a single control) into smaller pieces: the actual view, a logical model of the view (view-model), and the view-independent app logic (the model). In particular, it’s a popular workflow to have designers own the view using one tool, developers own the model using another tool, and design integrators own the view-model using both tools.
-   **Unit testing**. You can unit test the view-model (and consequently the model) independent of the view, thereby not relying on creating windows, driving input, and so on. By keeping the view small, you can test a large portion of your app without ever having to create a window.
-   **Agility to user experience changes**. The view tends to see the most frequent changes, and the most late changes, as the user experience is tweaked based on end-user feedback. By keeping the view separate, these changes can be accommodated more quickly and with less churn to the app.

There are multiple concrete definitions of the MVVM pattern, and 3rd party frameworks that help implement it. But strict adherence to any variation of the pattern can lead to apps with a lot more overhead than can be justified.

-   XAML data binding (the {Binding} markup extension) was designed in part to enable model/view patterns. But {Binding} brings with it non-trivial working set and CPU overhead. Creating a {Binding} causes a series of allocations, and updating a binding target can cause reflection and boxing. These problems are being addressed with the {x:Bind} markup extension, which compiles the bindings at build time. **Recommendation:** use {x:Bind}.
-   It’s popular in MVVM to connect Button.Click to the view-model using an ICommand, such as the common DelegateCommand or RelayCommand helpers. Those commands are extra allocations, though, including the CanExecuteChanged event listener, adding to the working set, and adding to the startup/navigation time for the page. **Recommendation:** As an alternative to using the convenient ICommand interface, consider putting event handlers in your code-behind and attaching them to the view events and call a command on your view-model when those events are raised. You'll also need to add extra code to disable the Button when the command is unavailable.
-   It’s popular in MVVM to create a Page with all possible configurations of the UI, then collapse parts of the tree by binding the Visibility property to properties in the VM. This adds unnecessarily to startup time and possibly to working set (because some parts of the tree may never become visible). **Recommendations:** Use the [x:Load attribute](../xaml-platform/x-load-attribute.md) or [x:DeferLoadStrategy attribute](../xaml-platform/x-deferloadstrategy-attribute.md) feature to defer unnecessary portions of the tree out of startup. Also, create separate user controls for the different modes of the page and use code-behind to keep only the necessary controls loaded.

## C++/CX recommendations

-   **Use the latest version**. There are continual performance improvements made to the C++/CX compiler. Ensure your app is building using the latest toolset.
-   **Disable RTTI (/GR-)**. RTTI is on by default in the compiler so, unless your build environment switches it off, you’re probably using it. RTTI has significant overhead, and unless your code has a deep dependency on it, you should turn it off. The XAML framework has no requirement that your code use RTTI.
-   **Avoid heavy use of ppltasks**. Ppltasks are very convenient when calling async WinRT APIs, but they come with significant code size overhead. The C++/CX team is working on a language feature – await – that will provide much better performance. In the meantime, balance your use of ppltasks in the hot paths of your code.
-   **Avoid use of C++/CX in the “business logic” of your app**. C++/CX is designed to be a convenient way to access WinRT APIs from C++ apps. It makes use of wrappers that have overhead. You should avoid C++/CX inside the business logic/model of your class, and reserve it for use at the boundaries between your code and WinRT.