---
ms.assetid: 83b4be37-6613-4d00-a48a-0451a24a30fb
title: Data binding
description: Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Data binding

Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data. Data binding allows you to separate the concern of data from the concern of UI, and that results in a simpler conceptual model as well as better readability, testability, and maintainability of your app. In markup, you can choose to use either the [{x:Bind} markup extension](../xaml-platform/x-bind-markup-extension.md) or the [{Binding} markup extension](../xaml-platform/binding-markup-extension.md). And you can even use a mixture of the two in the same app—even on the same UI element. {x:Bind} is new for Windows 10 and it has better performance.

| Topic | Description |
|-------|-------------|
| [Data binding overview](data-binding-quickstart.md) | This topic shows you how to bind a control (or other UI element) to a single item or bind an items control to a collection of items in a Universal Windows Platform (UWP) app. In addition, we show how to control the rendering of items, implement a details view based on a selection, and convert data for display. For more detailed info, see [Data binding in depth](data-binding-in-depth.md). | 
| [Data binding in depth](data-binding-in-depth.md) | This topic describes data binding features in detail. |
| [Sample data on the design surface, and for prototyping](displaying-data-in-the-designer.md) | In order to have your controls populated with data in the Visual Studio designer (so that you can work on your app's layout, templates, and other visual properties), there are various ways in which you can use design-time sample data. Sample data can also be really useful and time-saving if you're building a sketch (or prototype) app. You can use sample data in your sketch or prototype at run-time to illustrate your ideas without going as far as connecting to real, live data. |
| [Bind hierarchical data and create a master/details view](how-to-bind-to-hierarchical-data-and-create-a-master-details-view.md) | You can make a multi-level master/details (also known as list-details) view of hierarchical data by binding items controls to [<strong>CollectionViewSource</strong>](/uwp/api/Windows.UI.Xaml.Data.CollectionViewSource) instances that are bound together in a chain. |
| [Data binding and MVVM](data-binding-and-mvvm.md) | This topic describes the Model-View-ViewModel (MVVM) UI architectural design pattern. Data binding is at the core of MVVM, and enables loose coupling between UI and non-UI code. |