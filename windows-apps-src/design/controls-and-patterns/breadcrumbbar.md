---
description: The BreadcrumbBar control provides the direct path of pages or folders to the current location. It adapts to a variety of screen sizes. 
title: BreadcrumbBar
template: detail.hbs
ms.date: 3/9/2021
ms.topic: article
keywords: windows 10, winui, uwp
pm-contact: kayang
design-contact: shurd
dev-contact: ranjeshj
ms.custom: 21H1
ms.localizationpriority: medium
---

# Breadcrumb Bar
A BreadcrumbBar provides the direct path of pages or folders to the current location. It is often used for situations where the user's navigation trail (in a file system or menu system) needs to be persistently visible and the user may need to go back to a previous location. 

![Breadcrumb with nodes: Home, Documents, Design, Northwind, Images, Folder1, Folder2, Folder3. The app is resized so that the Breadcrumb crumbles and an ellipsis replaces the leftmost nodes. Then, clicking the ellipsis opens a flyout with the crumbled nodes](images/breadcrumbbar-default.gif)

**Get the Windows UI Library**

:::row:::
   :::column:::
      ![WinUI logo](images/winui-logo-64x64.png)
   :::column-end:::
   :::column span="3":::
      The **BreadcrumbBar** control requires the Windows UI Library, a NuGet package that contains new controls and UI features for Windows apps. For more info, including installation instructions, see [Windows UI Library](/uwp/toolkits/winui/).
   :::column-end:::
   :::column:::

   :::column-end:::
:::row-end:::

## Is this the right control?

Use a BreadcrumbBar when the path taken to the current position is relevant. This UI is commonly used in folder managers and when a user may navigate many levels deep into an app. 

## Examples

### Create a Breadcrumb
The XAML below describes a BreadcrumbBar with the default styling. A BreadcrumbBar can be placed like any other element. 

```xaml
<muxc:BreadcrumbBar x:Name="BreadcrumbBar1"/>
```

```csharp
BreadcrumbBar1.ItemsSource = new string[] { "Home", "Documents", "Design", "Northwind", "Images", "Folder1", "Folder2", "Folder3" };
```

![Breadcrumb with nodes: Home, Documents, Design, Northwind, Images, Folder1, Folder2, Folder3. The app is resized so that the Breadcrumb crumbles and an ellipsis replaces the leftmost nodes. Then, clicking the ellipsis opens a flyout with the crumbled nodes](images/breadcrumbbar-default.gif)

### Lightweight styling

You can modify the default Style and ControlTemplate to give the control a unique appearance. See the Control Style and Template section of the BreadcrumbBar API docs for a list of the available theme resources. For more info, see the [Light-weight styling section](https://docs.microsoft.com/windows/uwp/design/controls-and-patterns/xaml-styles#lightweight-styling) of the [Styling controls](https://docs.microsoft.com/windows/uwp/design/controls-and-patterns/xaml-styles) article. 

## Recommendations

* Common UI patterns where a Breadcrumb is used is when you have many levels of navigation and  expect users to be able to return to any previous level.