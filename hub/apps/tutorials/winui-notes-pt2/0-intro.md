---
title: WinUI Notes part 2 - Introduction
description: WinUI Notes part 2 introduction.
ms.date: 06/16/2026
ms.topic: tutorial
ms.localizationpriority: medium
---
# WinUI Notes part 2 - navigation and data binding

This tutorial demonstrates how to use navigation and data binding in a WinUI 3 app using XAML and C#.

In this tutorial, you learn how to:

> [!div class="checklist"]
>
> - Enable `NavigationCacheMode` to keep the same instance of a `Page` when navigating.
> - Implement the `INotifyPropertyChanged` interface to let a data binding know that data has been updated.
> - Pass objects between pages when navigating.

This tutorial improves on the *WinUI Notes* sample app from the [Create your first WinUI 3 app](../winui-notes/intro.md) tutorial. If you've completed that tutorial, you can continue working with the same code. Or, you can download the completed code for that tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes) and use it as a starting point for this tutorial.

In either case, you should be familiar with concepts presented and the code created in the [Create your first WinUI 3 app](../winui-notes/intro.md) tutorial.

> [!TIP]
> You can also download or view the completed code for this tutorial from the [GitHub repo at WinUI Notes part 2](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes-part-2). To see the differences between the start and end points for the project, see this commit: [updates for part 2](https://github.com/MicrosoftDocs/windows-topic-specific-samples/commit/bb4d94785247247fbfbce80173bb3a9097e843d6).

## Background

In order to keep things simple and introduce some foundational concepts, the [Create your first WinUI 3 app](../winui-notes/intro.md) tutorial focused on simplicity over efficiency. So while the app works, there are some things that can be improved.

The key issue has to do with navigating between pages in the app. By default, [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page) instances are not saved when navigating, so each time you navigate to a `Page`, a new instance is created. In the WinUI Notes app, the `notesModel` object is created in the `AllNotesPage` constructor, and is populated by reading all the notes from the file system.

```csharp
public sealed partial class AllNotesPage : Page
{
    private AllNotes notesModel = new AllNotes();
    ...
}
```

Each time you navigate from `NotePage` back to `AllNotesPage`, the `notesModel` is recreated and all the notes are re-read from the file system. This inefficiency typically isn't noticeable in a small sample app without much data, but it would be unacceptable in, for example, a photos app that was reading thousands of large images from the file system.

To fix this issue, this tutorial will cover these steps:

- First, ensure that the `Page` instance is cached so that on navigation it's re-used and not re-created.
- Update the `Note` class so that bound properties are notified when there are any changes to the note text.
- Ensure that the `notesModel` is properly updated with saved or deleted notes, since it's not being re-created with each navigation.

> [!TIP]
> You'll frequently refer to API reference docs and conceptual docs while building Windows apps. In this tutorial, you'll see links inline in the text, and in groups labeled, "Learn more in the docs:". These links are optional; you don't need to follow them to complete the tutorial. They're provided in case you want to make note of where to find the information you'll need when you start to create your own apps.

> [!div class="nextstepaction"]
> [Continue to step 1 - Navigation cache and change notification](1-navigation-cache-change-notification.md)
