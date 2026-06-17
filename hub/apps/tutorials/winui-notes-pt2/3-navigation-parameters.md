---
title: WinUI Notes part 2 - Navigation parameters
description: WinUI Notes part 2 navigation parameters.
ms.date: 06/16/2026
ms.topic: tutorial
ms.localizationpriority: medium
---
# Pass the note as a navigation parameter

Now that the `Note` has a `State`, update the navigation to pass it back to `AllNotesPage`, where you can handle it as appropriate based on it's `State`. This also gives you the opportunity to improve the user experience related to navigation.

> [!TIP]
> If needed, review [Create your first WinUI 3 app, Step 1 - Navigation](/windows/apps/tutorials/winui-notes/navigation). It's helpful to understand how the navigation is set up before making these changes.

Currently, all navigation from `NotePage` back to `AllNotesPage` is done with a simple call to [Frame.GoBack](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.goback). However, the `GoBack` method doesn't allow you to pass a navigation parameter. In order to pass a `Note` as a parameter, you'll need to replace the back navigation with a forward navigation ([Frame.Navigate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.navigate)). `GoBack` also doesn't add an entry to the navigation stack like a forward navigation does, so you'll need to manage the backstack to prevent this forward navigation from being added.

> [!TIP]
> You can download or view the completed code for this tutorial from the [GitHub repo at WinUI Notes part 2](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes-part-2). To see the differences between the start and end points for the project, see this commit: [updates for part 2](https://github.com/MicrosoftDocs/windows-topic-specific-samples/commit/bb4d94785247247fbfbce80173bb3a9097e843d6).

## OnNavigatingFrom

In `NotePage.cs`, override the [OnNavigatingFrom](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page.onnavigatingfrom) method. This is called after the user presses the back button and [GoBack](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame.goback) is called. It lets you intercept the navigation, check the note `State`, and cancel the navigation if needed.

Here, if the note is not saved, you cancel the back navigation and show a dialog that asks whether the user wants to save the note.

- If the user saves the note, call `SaveAsync`, then replace the back navigation with a call to `Navigate` that passes the note to `AllNotesPage`.
- If the user doesn't save their changes, use [TextBox.Undo](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox.undo) to undo any edits, reset the note state, and restart the back navigation.

```csharp
// ↓ Add this. ↓
protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
{
    if (noteModel?.State == NoteState.Unsaved)
    {
        e.Cancel = true;
        ContentDialog dialog = new ContentDialog();

        // XamlRoot must be set for the ContentDialog.
        dialog.XamlRoot = this.XamlRoot;
        dialog.Title = "Save your work?";
        dialog.PrimaryButtonText = "Save";
        dialog.SecondaryButtonText = "Don't Save";
        dialog.CloseButtonText = "Cancel";
        dialog.DefaultButton = ContentDialogButton.Primary;

        ContentDialogResult result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            await noteModel.SaveAsync();
            Frame.Navigate(typeof(AllNotesPage), noteModel);
        }
        else if (result == ContentDialogResult.Secondary)
        {
            while (NoteEditor.CanUndo)
            {
                NoteEditor.Undo();
            }
            NoteEditor.Focus(FocusState.Programmatic);
            noteModel.State = NoteState.Saved;
            Frame.Navigate(typeof(AllNotesPage), noteModel);
        }
    }
}
```

## Delete

Next, modify the code for the delete button `Click` event in `NotePage.xaml.cs`. Instead of simply deleting the note and navigating back, you'll check the note state.

- If the state is `Unset` – which means the note is newly created, doesn't have any edits, and hasn't been saved – you just navigate back. You don't need to pass the note as a parameter.
- Otherwise, delete the note file from the file system and pass the `Note` object back as the navigation parameter. Then `AllNotesPage` will receive the `Note` with it's `Deleted` state and know to delete it from the `Notes` collection.

```csharp
private async void DeleteButton_Click(object sender, RoutedEventArgs e)
{
    if (noteModel is not null)
    {
        if (noteModel.State == NoteState.Unset)
        {
            // If the note is new, doesn't have any edits,
            // and hasn't been saved, just call GoBack.
            // There's no need to pass back the noteModel.
            if (Frame.CanGoBack == true)
            {
                Frame.GoBack();
            }
        }
        else
        {
            // If the note has been saved before, then delete it
            // and navigate back to the AllNotesPage passing the
            // noteModel with its Deleted state.
            await noteModel.DeleteAsync();
            Frame.Navigate(typeof(AllNotesPage), noteModel);
        }
    }
}
```

## Save and close

Currently, the user has to click to save a note, and then click the back arrow to close the note page and go back to the notes collection. This experience can be improved by letting the user save and close the note with one click. To do this, you'll replace the "Save" button with a "Save and close" [SplitButton](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.splitbutton) with a drop-down option to just save.

```xaml
<!-- ↓ Delete this. ↓ -->
<!--<Button Content="Save" Click="SaveButton_Click"/>-->

<!-- ↓ Add this. ↓ -->
<SplitButton Content="Save &amp; close" Click="SaveCloseButton_Click"
             Height="32">
    <SplitButton.Flyout>
        <MenuFlyout>
            <MenuFlyoutItem Text="Save" Click="SaveButton_Click"/>
        </MenuFlyout>
    </SplitButton.Flyout>
</SplitButton>
```

Add a new event handler for the "Save and close" button `Click` event. Here, you save the note and then pass it back to `AllNotesPage` as the navigation parameter.

```csharp
// ↓ Add this. ↓
private async void SaveCloseButton_Click(SplitButton sender, SplitButtonClickEventArgs args)
{
    if (noteModel is not null)
    {
        await noteModel.SaveAsync();
        Frame.Navigate(typeof(AllNotesPage), noteModel);
    }
}
```

## Handle the navigation parameter in AllNotesPage

In `AllNotesPage`, you need to handle the incoming navigation parameter (`Note`) and add it to or remove it from the `Notes` collection as needed.

To handle the incoming navigation parameter, override the [OnNavigatedTo](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page.onnavigatedto) method, as shown here.

```csharp
// ↓ Add this. ↓
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    if (e.Parameter is Note note)
    {
        if (note.State == NoteState.Deleted)
        {
            notesModel.RemoveNote(note);
        }
        else if (!notesModel.Notes.Contains(note))
        {
            notesModel.AddNote(note);
        }
        // This navigation should be treated like a
        // back navigation, so clear the backstack.
        Frame.BackStack.Clear();
    }
}
```

Now you can run your app to see how these changes work. Try adding new notes, navigating back and forth between notes, and deleting notes.

:::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Implement navigation between two pages](../../design/basics/navigate-between-two-pages.md)
- [Navigation history and backwards navigation](../../design/basics/navigation-history-and-backwards-navigation.md)
- [Frame class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.frame), [Page class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.page)

## Next steps

Congratulations! You've completed the _WinUI Notes part 2_ tutorial!

The following links provide more information about creating apps with WinUI and the Windows App SDK:

- [Samples and resources](/windows/apps/get-started/samples)
- [Design for Windows apps](/windows/apps/design/)
- [Develop Windows desktop apps](/windows/apps/develop/)
- [Controls for Windows apps](/windows/apps/design/controls/)
