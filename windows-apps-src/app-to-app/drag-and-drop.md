---
description: This article explains how to add dragging and dropping in your Universal Windows Platform (UWP) app.
title: Drag and drop
ms.assetid: A15ED2F5-1649-4601-A761-0F6C707A8B7E
author: msatranjr
ms.author: misatran
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Drag and drop



This article explains how to add dragging and dropping in your Universal Windows Platform (UWP) app. Drag and drop is a classic, natural way of interacting with content such as images and files. Once implemented, drag and drop works seamlessly in all directions, including app-to-app, app-to-desktop, and desktop-to app.

## Set valid areas

Use the [**AllowDrop**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.UIElement.AllowDrop) and [**CanDrag**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.UIElement.CanDrag) properties to designate the areas of your app valid for dragging and dropping.

The following markup shows how to set a specific area of the app as valid for dropping by using the [**AllowDrop**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.UIElement.AllowDrop) in XAML. If a user tries to drop somewhere else, the system won't let them. If you want users to be able to drop items anywhere on your app, set the entire background as a drop target.

[!code-xml[Main](./code/drag_drop/cs/MainPage.xaml#SnippetDropArea)]

With dragging, you'll usually want to be specific about what's draggable. Users will want to drag certain items, such as pictures, not everything in your app. Here's how to set [**CanDrag**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.UIElement.CanDrag) by using XAML.

[!code-xml[Main](./code/drag_drop/cs/MainPage.xaml#SnippetDragArea)]

You don't need to do any other work to allow dragging, unless you want to customize the UI (which is covered later in this article). Dropping requires a few more steps.

## Handle the DragOver event

The [**DragOver**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.UIElement.DragOver) event fires when a user has dragged an item over your app, but not yet dropped it. In this handler, you need to specify what kind of operations your app supports by using the [**AcceptedOperation**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.DragEventArgs.AcceptedOperation) property. Copy is the most common.

[!code-cs[Main](./code/drag_drop/cs/MainPage.xaml.cs#SnippetGrid_DragOver)]

## Process the Drop event

The [**Drop**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.UIElement.Drop) event occurs when the user releases items in a valid drop area. Process them by using the [**DataView**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.DragEventArgs.DataView) property.

For simplicity in the example below, we'll assume the user dropped a single photo and access it directly. In reality, users can drop multiple items of varying formats simultaneously. Your app should handle this possibility by checking what types of files were dropped and how many there are, and process each accordingly. You should also consider notifying the user if they're trying to do something your app doesn't support.

[!code-cs[Main](./code/drag_drop/cs/MainPage.xaml.cs#SnippetGrid_Drop)]

## Customize the UI

The system provides a default UI for dragging and dropping. However, you can also choose to customize various parts of the UI by setting custom captions and glyphs, or by opting not to show a UI at all. To customize the UI, use the [**DragEventArgs.DragUIOverride**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.DragEventArgs.DragUIOverride) property.

[!code-cs[Main](./code/drag_drop/cs/MainPage.xaml.cs#SnippetGrid_DragOverCustom)]

## Open a context menu on an item you can drag with touch

When using touch, dragging a [**UIElement**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.UIElement) and opening its context menu share similar touch gestures; each begins with a press and hold. Here's how the system disambiguates between the two actions for elements in your app that support both: 

* If a user presses and holds an item and begins dragging it within 500 milliseconds, the item is dragged and the context menu is not shown. 
* If the user presses and holds but does not drag within 500 milliseconds, the context menu is opened. 
* After the context menu is open, if the user tries to drag the item (without lifting their finger), the context menu is dismissed and the drag will start.

## Designate an item in a ListView or GridView as a folder

You can specify a [**ListViewItem**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.ListViewItem) or [**GridViewItem**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.GridViewItem) as a folder. This is particularly useful for TreeView and File Explorer scenarios. To do so, explicitly set the [**AllowDrop**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.UIElement.AllowDrop) property to **True** on that item. 

The system will automatically show the appropriate animations for dropping into a folder versus a non-folder item. Your app code must continue to handle the [**Drop**](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.UIElement.Drop) event on the folder item (as well as on the non-folder item) in order to update the data source and add the dropped item to the target folder.

## See also

* [App-to-app communication](index.md)
* [AllowDrop](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.allowdrop.aspx)
* [CanDrag](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.candrag.aspx)
* [DragOver](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.dragover.aspx)
* [AcceptedOperation](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.drageventargs.acceptedoperation.aspx)
* [DataView](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.drageventargs.dataview.aspx)
* [DragUIOverride](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.drageventargs.draguioverride.aspx)
* [Drop](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.drop.aspx)
* [IsDragSource](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.listviewbase.isdragsource.aspx)
