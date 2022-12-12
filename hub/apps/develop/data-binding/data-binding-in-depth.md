---
ms.assetid: 2a50c798-6244-4fda-9091-a10a9e87fae2
title: Data binding in depth
description: Learn how to use data binding in Windows App SDK applications
ms.date: 12/12/2022
ms.topic: article
keywords: windows 10, windows 11, windows app sdk, winui, windows ui
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---

# Data binding in depth

## Important APIs

- [**{x:Bind} markup extension**](/windows/uwp/xaml-platform/x-bind-markup-extension)
- [**Binding class**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.binding)
- [**DataContext**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.datacontext)
- [**INotifyPropertyChanged**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.inotifypropertychanged)

## Introduction

> [!NOTE]
> This topic describes data binding features in detail. For a short, practical introduction, see [Data binding overview](data-binding-overview.md).

This topic is about data binding for the APIs that reside in the [**Microsoft.UI.Xaml.Data** namespace](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data).

Data binding is a way for your app's UI to display data, and optionally to stay in sync with that data. Data binding allows you to separate the concern of data from the concern of UI, and that results in a simpler conceptual model as well as better readability, testability, and maintainability of your app.

You can use data binding to simply display values from a data source when the UI is first shown, but not to respond to changes in those values. This is a mode of binding called *one-time*, and it works well for a value that doesn't change during run-time. Alternatively, you can choose to "observe" the values and to update the UI when they change. This mode is called *one-way*, and it works well for read-only data. Ultimately, you can choose to both observe and update, so that changes that the user makes to values in the UI are automatically pushed back into the data source. This mode is called *two-way*, and it works well for read-write data. Here are some examples.
