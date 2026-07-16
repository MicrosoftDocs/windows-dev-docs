---
title: MVVM performance tips for WinUI apps
description: This topic discusses WinUI and Windows App SDK performance considerations related to MVVM, bindings, and view composition.
ms.date: 03/16/2026
ms.topic: article
ms.localizationpriority: medium
---
# MVVM performance tips for WinUI apps


This topic discusses some performance considerations for WinUI apps related to MVVM, bindings, and view composition.

## The Model-View-ViewModel (MVVM) pattern

The Model-View-ViewModel (MVVM) pattern is common in many WinUI apps. (MVVM is very similar to Fowler’s description of the Model-View-Presenter pattern, but it is tailored to XAML). The issue with the MVVM pattern is that it can inadvertently lead to apps that have too many layers and too many allocations. The motivations for MVVM are these.

-   **Separation of concerns**. It’s always helpful to divide a problem into smaller pieces, and a pattern like MVVM or MVC is a way to divide an app (or even a single control) into smaller pieces: the actual view, a logical model of the view (view-model), and the view-independent app logic (the model). In particular, it’s a popular workflow to have designers own the view using one tool, developers own the model using another tool, and design integrators own the view-model using both tools.
-   **Unit testing**. You can unit test the view-model (and consequently the model) independent of the view, thereby not relying on creating windows, driving input, and so on. By keeping the view small, you can test a large portion of your app without ever having to create a window.
-   **Agility to user experience changes**. The view tends to see the most frequent changes, and the most late changes, as the user experience is tweaked based on end-user feedback. By keeping the view separate, these changes can be accommodated more quickly and with less churn to the app.

There are multiple concrete definitions of the MVVM pattern, and 3rd party frameworks that help implement it. But strict adherence to any variation of the pattern can lead to apps with a lot more overhead than can be justified.

-   XAML data binding (the {Binding} markup extension) was designed in part to enable model/view patterns. But {Binding} brings with it non-trivial working set and CPU overhead. Creating a {Binding} causes a series of allocations, and updating a binding target can cause reflection and boxing. In WinUI, these problems are addressed with the {x:Bind} markup extension, which compiles bindings at build time and is widely used in WinUI samples and production apps. **Recommendation:** use {x:Bind}.
-   It’s popular in MVVM to connect Button.Click to the view-model using an ICommand, such as the common DelegateCommand or RelayCommand helpers. Those commands are extra allocations, though, including the CanExecuteChanged event listener, adding to the working set, and adding to the startup/navigation time for the page. **Recommendation:** As an alternative to using the convenient ICommand interface, consider putting event handlers in your code-behind, attach them to the view events, and call a command on your view-model when those events are raised. You'll also need to add extra code to disable the Button when the command is unavailable.
-   It’s popular in MVVM to create a Page with all possible configurations of the UI, then collapse parts of the tree by binding the Visibility property to properties in the VM. This adds unnecessarily to startup time and possibly to working set (because some parts of the tree may never become visible). **Recommendations:** Use the [x:Load attribute](/windows/apps/develop/platform/xaml/x-load-attribute) feature to defer unnecessary portions of the tree out of startup. Also, create separate user controls for the different modes of the page and use code-behind to keep only the necessary controls loaded.

## Related content

- [Data binding and MVVM](../data-binding/data-binding-and-mvvm.md)
- [MVVM Toolkit documentation](/dotnet/communitytoolkit/mvvm/)
- [{x:Bind} markup extension](/windows/apps/develop/platform/xaml/x-bind-markup-extension)
- [Keep the UI thread responsive](keep-ui-thread-responsive.md)
