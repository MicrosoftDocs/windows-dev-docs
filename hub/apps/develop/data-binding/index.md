---
ms.assetid: adfa70f3-a4d9-45d1-8957-c26a7703a276
title: Data binding in Windows apps - A guide for developers
description: Learn data binding in Windows apps to connect UI with data dynamically. Explore x:Bind and Binding markup extensions with examples and MVVM best practices.
ms.date: 11/11/2025
ms.topic: concept-article
keywords: windows 10, windows 11, windows app sdk, winui, windows ui
ms.localizationpriority: medium
# customer intent: As a Windows developer, I want to learn about data binding in Windows apps so that I can create a separation of concerns between my data and UI.
---

# Data binding in Windows apps

Data binding connects your app's user interface to its data, creating a dynamic relationship that keeps your UI responsive. In Windows apps, data binding establishes a clear separation between the data layer and presentation layer, improving code organization and making your app easier to maintain and test.

Windows apps support two primary data binding approaches: the [{x:Bind} markup extension](/windows/apps/develop/platform/xaml/x-bind-markup-extension) and the [{Binding} markup extension](/windows/apps/develop/platform/xaml/binding-markup-extension). You can use either approach individually or combine them within the same app. The `{x:Bind}` extension, available in Windows App SDK and UWP apps on Windows 10 and later, offers better performance and compile-time validation.

Whether you're displaying a single data item, binding to collections, or implementing complex architectural patterns like Model-View-ViewModel (MVVM), data binding provides the foundation for creating responsive, maintainable Windows applications.

| Topic | Description |
|-------|-------------|
| [Data binding overview](data-binding-overview.md) | This topic shows you how to bind a control (or other UI element) to a single item or bind an items control to a collection of items in a Windows App SDK app. In addition, it shows how to control the rendering of items, implement a details view based on a selection, and convert data for display. For more detailed info, see [Data binding in depth](data-binding-in-depth.md). |
| [Data binding in depth](data-binding-in-depth.md) | This topic describes data binding features in detail. |
| [Bind hierarchical data and create a master/details view](bind-to-hierarchical-data-and-create-a-master-details-view.md) | You can make a multilevel master/details (also known as list-details) view of hierarchical data by binding items controls to [**CollectionViewSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.collectionviewsource) instances that are bound together in a chain. |
| [Data binding and MVVM](data-binding-and-mvvm.md) | This topic describes the Model-View-ViewModel (MVVM) UI architectural design pattern. Data binding is at the core of MVVM, and enables loose coupling between UI and non-UI code. |
| [How to data bind with the MVVM Toolkit in WinUI apps](../../tutorials/winui-mvvm-toolkit/intro.md) | This tutorial builds on the [Create a WinUI app](/windows/apps/tutorials/winui-notes/intro) tutorial and shows you how to implement data binding with the [MVVM Toolkit](/dotnet/communitytoolkit/mvvm/). It covers updating your view models to leverage the MVVM Toolkit and the differences between the MVVM Toolkit and traditional MVVM approaches. |
| [Functions in x:Bind](function-bindings.md) | In Windows App SDK apps, `{x:Bind}` supports using a function as the leaf step of the binding path. In this topic, learn how properties are bound to functions to do conversions, date formatting, text formatting, text concatenations, and more. |

## Related content

- [Data binding in UWP apps](/windows/uwp/data-binding/)
