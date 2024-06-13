---
ms.assetid: adfa70f3-a4d9-45d1-8957-c26a7703a276
title: Data binding in Windows apps
description: Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data.
ms.date: 12/12/2022
ms.topic: article
keywords: windows 10, windows 11, windows app sdk, winui, windows ui
ms.localizationpriority: medium
---

# Data binding in Windows apps

Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data. Data binding allows you to create a separation of concerns between your data and UI, and that results in a simpler conceptual model as well as better readability, testability, and maintainability of your app. In XAML markup, you can choose to use either the [{x:Bind} markup extension](/windows/uwp/xaml-platform/x-bind-markup-extension) or the [{Binding} markup extension](/windows/uwp/xaml-platform/binding-markup-extension). And you can even use a mixture of the two in the same app—even on the same UI element. `{x:Bind}` was new for UWP in Windows 10, is also available in Windows App SDK, and it has better performance.

| Topic | Description |
|-------|-------------|
| [Data binding overview](data-binding-overview.md) | This topic shows you how to bind a control (or other UI element) to a single item or bind an items control to a collection of items in a Windows App SDK app. In addition, we show how to control the rendering of items, implement a details view based on a selection, and convert data for display. For more detailed info, see [Data binding in depth](data-binding-in-depth.md). |
| [Data binding in depth](data-binding-in-depth.md) | This topic describes data binding features in detail. |
| [Bind hierarchical data and create a master/details view](bind-to-hierarchical-data-and-create-a-master-details-view.md) | You can make a multi-level master/details (also known as list-details) view of hierarchical data by binding items controls to [**CollectionViewSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.collectionviewsource) instances that are bound together in a chain. |
| [Data binding and MVVM](data-binding-and-mvvm.md) | This topic describes the Model-View-ViewModel (MVVM) UI architectural design pattern. Data binding is at the core of MVVM, and enables loose coupling between UI and non-UI code. |
| [Functions in x:Bind](function-bindings.md) | In Windows App SDK apps, `{x:Bind}` supports using a function as the leaf step of the binding path. In this topic, learn how properties are bound to functions to do conversions, date formatting, text formatting, text concatenations, etc. |

## See also

* [Data binding in UWP apps](/windows/uwp/data-binding/)
