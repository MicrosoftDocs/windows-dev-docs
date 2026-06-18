---
title: WinUI Notes -Step 5 - Navigation
description: WinUI Notes - Step 5 - Add navigation between pages
author: jwmsft
ms.author: jimwalk
ms.date: 09/02/2025
ms.topic: tutorial
---
# Navigate between pages

This portion of the tutorial adds the final piece to the app, navigation between the _all notes_ page and the individual _note_ page.

Before writing any code to handle in-app navigation, let's describe the expected navigation behavior.

In `AllNotesPage`, there's the collection of existing notes and a **New note** button.

- Clicking an existing note should navigate to the note page and load the note that was clicked.
- Clicking the **New note** button should navigate to the note page and load an empty, unsaved note.

On the note page, you'll add a _back_ button, and there are **Save** and **Delete** buttons .

- Clicking the back button should navigate back to the all notes page, discarding any changes made to the note.
- Clicking the **Delete** button should delete the note and then navigate back.

## New note

First, you'll handle navigation for a new note.

> [!TIP]
> You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes). To see the code as it is in this step, see this commit: [navigation - new note](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/25c23e5976c6b791355b109c7a7a0430ab16a3f9/WinUINotes).

1. In **AllNotesPage.xaml**, find the `AppBarButton` for a new note.
1. Add a `Click` event handler and rename it to `NewNoteButton_Click`. (See _Add event handlers_ in the note page step if you need a reminder how to do this.)

    ```xaml
    <AppBarButton Icon="Add" Label="New note"
                  Click="NewNoteButton_Click"/>
    ```

1. In the `NewNoteButton_Click` method (in **AllNotesPage.xaml.cs**), add this code:

    ```csharp
    private void NewNoteButton_Click(object sender, RoutedEventArgs e)
    {
        // ↓ Add this. ↓
        Frame.Navigate(typeof(NotePage));
    }
    ```

Each [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page) has a [Frame](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page.frame) property that provides a reference to the [Frame](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame) instance that the `Page` belongs to (if any). That's the `Frame` that you call the [Navigate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.navigate) method on here. The `Navigate` method takes the [Type](/dotnet/api/system.type) of the page that you want to navigate to. You get that `Type` in C# by using the `typeof` operator.

If you run the app now, you can click the **New note** button to navigate to the note page, and you can type into the note editor. However, if you try to save the note, nothing will happen. This is because an instance of the `Note` model hasn't been created in the note page yet. You'll do that now.

1. Open **NotePage.xaml.cs**.
1. Add code to override the page's [OnNavigatedTo](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page.onnavigatedto) method.

    ```csharp
    public NotePage()
    {
        this.InitializeComponent();
    }
    //  ↓ Add this. ↓
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        noteModel = new Note();
    }
    ```

Now, when you navigate to `NotePage`, a new instance of the `Note` model is created.

## Existing notes

Now you'll add navigation for existing notes. Currently, when you click the note in the `ItemsView`, the note is selected, but nothing happens. The default behavior for most collection controls is _single selection_, which means one item is selected at a time. You'll update the `ItemsView` so that instead of selecting it, you can click an item like a button.

> [!TIP]
> You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes). To see the code as it is in this step, see this commit: [navigation - final app](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/3e17b8d7aae7eda6fed3a56ff10e63504c651a96/WinUINotes).

1. Open **AllNotesPage.xaml**.
1. Update the XAML for the `ItemsView` with [SelectionMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selectionmode) = [None](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsviewselectionmode) and [IsItemInvokedEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.isiteminvokedenabled) = `True`.
1. Add a handler for the [ItemInvoked](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.iteminvoked) event.

    ```xaml
    <ItemsView ItemsSource="{x:Bind notesModel.Notes}"
               Grid.Row="1" Margin="24" 
               ItemTemplate="{StaticResource NoteItemTemplate}"
               <!-- ↓ Add this. ↓ -->
               SelectionMode="None"
               IsItemInvokedEnabled="True"
               ItemInvoked="ItemsView_ItemInvoked">
    ```

1. In **AllNotesPage.xaml.cs**, find the `ItemsView_ItemInvoked` method. (If Visual Studio didn't create it for you, which could happen if you copy and paste the code, add it in the next step.)
1. In the `ItemsView_ItemInvoked` method, add code to navigate to `NotePage`. This time, you'll use an overload of the [Navigate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.navigate) method that lets you pass an object to the other page. Pass the invoked `Note` as the second navigation parameter.

    ```csharp
    private void ItemsView_ItemInvoked(ItemsView sender, ItemsViewItemInvokedEventArgs args)
    {
        Frame.Navigate(typeof(NotePage), args.InvokedItem);
    }
    ```

1. Open **NotePage.xaml.cs**.
1. Update the `OnNavigatedTo` method override to handle the `Note` that's passed by the call to `Navigate`.

    ```csharp
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        //  ↓ Update this. ↓
        if (e.Parameter is Note note)
        {
            noteModel = note;
        }
        else
        {
            noteModel = new Note();
        }
       // ↑ Update this. ↑
    }
    ```

    In this code, you first check to see if the passed _parameter_ is a `Note` object. If it is, you assign it as the `Note` model for the page. If it's `null` or not a `Note`, create a new `Note` as before.

## Back navigation

Lastly, you need to update the app so that you can navigate back from an individual note to the all notes page.

The WinUI 3 [TitleBar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.titlebar) control includes a back button that meets all the Fluent Design guidelines for placement and appearance. You'll use this built-in button for back navigation.

1. Open **MainWindow.xaml**.
1. Update the XAML for the `TitleBar` with [IsBackButtonVisible](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.titlebar.isbackbuttonvisible) = `True` and [IsBackButtonEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.titlebar.isbackbuttonenabled) bound to the [Frame.CanGoBack](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.cangoback) property.
1. Add a handler for the [BackRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.titlebar.backrequested) event. Your XAML should look like this:

    ```xaml
    <TitleBar x:Name="AppTitleBar"
              Title="WinUI Notes"
              IsBackButtonVisible="True"
              IsBackButtonEnabled="{x:Bind rootFrame.CanGoBack, Mode=OneWay}"
              BackRequested="AppTitleBar_BackRequested">
    ```

    Here, the [IsBackButtonVisible](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.titlebar.isbackbuttonvisible) property is set to `true`. This makes the back button appear in the upper-left corner of the app window.

    Then, the [IsBackButtonEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.titlebar.isbackbuttonenabled) property is bound to the [Frame.CanGoBack](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.cangoback) property, so the back button is enabled only if the frame can navigate back.

    Finally, the [BackRequested](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.titlebar.backrequested) event handler is added. This is where you put the code to navigate back.

1. In **MainWindow.xaml.cs**, add this code to the `AppTitleBar_BackRequested` method:

    ```csharp
    private void AppTitleBar_BackRequested(TitleBar sender, object args)
    {
        // ↓ Add this. ↓
        if (rootFrame.CanGoBack == true)
        {
            rootFrame.GoBack();
        }
        // ↑ Add this. ↑
    }
    ```

    The `Frame` class keeps track of navigation in its [BackStack](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.backstack), so you can navigate back to previous pages simply by calling the [GoBack](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.goback) method. It's a best practice to always check the [CanGoBack](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.cangoback) property before calling `GoBack`.

Next, you need to update the code in `NotePage` to navigate back after the note is deleted.

1. Open **NotePage.xaml.cs**.
1. Update the `DeleteButton_Click` event handler method with a call to the `Frame.CanGoBack` method after the note is deleted:

    ```csharp
    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (noteModel is not null)
        {
            await noteModel.DeleteAsync();
        }
        // ↓ Add this. ↓
        if (Frame.CanGoBack == true)
        {
            Frame.GoBack();
        }
        // ↑ Add this. ↑
    }
    ```

> [!TIP]
> You might have noticed that in `NotePage`, you call `Frame.GoBack`, while in `MainWindow` you called `rootFrame.GoBack`. This is because the [Page](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page) class has a [Frame](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page.frame) property that gets the `Frame` that is hosting the `Page`, if any. In this case, it gets a reference to `rootFrame`.

Now you can run your app. Try adding new notes, navigating back and forth between notes, and deleting notes.

:::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Implement navigation between two pages](../../design/basics/navigate-between-two-pages.md)
- [Navigation history and backwards navigation](../../design/basics/navigation-history-and-backwards-navigation.md)
- [Frame class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame), [Page class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page)

## Next steps

Congratulations! You've completed the _Create a WinUI app_ tutorial!

In order to keep things simple and introduce some foundational concepts, this tutorial focused on simplicity over efficiency, and introduced features at the most basic level. So while the app works, there are some things that can be improved.

We recommend that you continue with one of these follow-on tutorials to learn more:

- [WinUI Notes part 2](../winui-notes-pt2/0-intro.md) – Explains more features related to navigation and data binding. You'll update the app to work more efficiently while keeping the same basic architecture.

    **OR**

- [Data binding, dependency injection, and unit testing in WinUI](../winui-mvvm-toolkit/intro.md) – Shows how to implement data binding, dependency injection, and unit testing with the Model-View-ViewModel (MVVM) design pattern. You'll change the app and view models to leverage the MVVM Toolkit.
