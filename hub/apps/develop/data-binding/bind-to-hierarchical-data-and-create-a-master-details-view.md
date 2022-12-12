---
ms.assetid: 6c563dd4-3dd0-4175-a1ab-7a1103fc9559
title: Bind hierarchical data and create a master/details view
description: You can make a multi-level master/details (also known as list-details) view of hierarchical data by binding items controls to CollectionViewSource instances that are bound together in a chain.
ms.date: 12/12/2022
ms.topic: article
keywords: windows 10, windows 11, winui, windows app sdk, windows ui
ms.localizationpriority: medium
---

# Bind hierarchical data and create a master/details view

> [!NOTE]
> Also see the [Master/detail sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlMasterDetail).

You can make a multi-level master/details (also known as list-details) view of hierarchical data by binding items controls to [**CollectionViewSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.collectionviewsource) instances that are bound together in a chain. In this topic we use the [{x:Bind} markup extension](/windows/uwp/xaml-platform/x-bind-markup-extension) where possible, and the more flexible (but less performant) [{Binding} markup extension](/windows/uwp/xaml-platform/binding-markup-extension) where necessary.
