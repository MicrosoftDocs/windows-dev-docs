---
title: WinUI Notes part 2 - Note state
description: WinUI Notes part 2 note state.
ms.date: 06/16/2026
ms.topic: tutorial
ms.localizationpriority: medium
---
# Track note state

In the previous step, you fixed the first side effect of navigation caching by implementing `INotifyPropertyChanged` so that edits are reflected in the bound text control. The other side effect of caching the page on navigation is that the notes collection isn't updated when a new note is added or deleted. That's because previously, the note was saved, and then the collection was recreated by re-reading all the saved notes. You'll fix these issues now by tracking the state of a note, then using the state to determine whether the note needs to be added or deleted.

> [!TIP]
> You can download or view the completed code for this tutorial from the [GitHub repo at WinUI Notes part 2](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes-part-2). To see the differences between the start and end points for the project, see this commit: [updates for part 2](https://github.com/MicrosoftDocs/windows-topic-specific-samples/commit/bb4d94785247247fbfbce80173bb3a9097e843d6).

## Update the collection

First, you need to add code to update the collection when a note is added or deleted. In `AllNotes.cs`, add the `AddNote` and `RemoveNote` methods as shown here.

```csharp
    public class AllNotes
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
        // ...

        // ↓ Add this. ↓
        public void AddNote(Note note)
        {
            // Insert the note at the beginning of the collection.
            Notes.Insert(0, note);
        }

        public void RemoveNote(Note note)
        {
            Notes.Remove(note);
        }
    }

```

> [!NOTE]
> `Notes.Add` would add the note at the end of the collection. Instead, `Insert` it at the beginning so new notes are shown first.

:::image type="icon" source="media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Create your first WinUI 3 app, Step 4](../winui-notes/all-notes.md)
- [ObservableCollection](/dotnet/api/system.collections.objectmodel.observablecollection-1)

## Add State to the note

Notes are added or deleted in `NotePage`. But the collection of notes is maintained in `AllNotesPage`, so you still need a way to let `AllNotesPage` know about new notes and deleted notes. For this, you'll add a new `State` property to the `Note` class. Then, in step 3, you'll modify the navigation between the pages to pass new or deleted notes as the navigation parameter.

In `Note.cs`, add a new enum called `NoteState`. (Add it below the `Note` class, but inside of the namespace brackets.)

```csharp
// ↓ Add this. ↓
public enum NoteState
{
    Unset = 0, Unsaved, Saved, Deleted
}
```

Add a new `State` property to the `Note` class, and set it as appropriate:

- `Unset`: New note
- `Unsaved`: Text has been changed, but not saved.
- `Saved`: Text is changed and saved to the file system.
- `Deleted`: Note has been deleted from the file system.

```csharp
// ↓ Add this. ↓
public NoteState State { get; set; } = NoteState.Unset;

// ↓ Update these. ↓
public string Text
{
    get => _text;
    set
    {
        if (_text != value)
        {
            _text = value;
            // ↓ Add this. ↓
            State = NoteState.Unsaved;
            // ↑ Add this. ↑
            OnPropertyChanged();
        }
    }
}

public async Task SaveAsync()
{
    // Save the note to a file.
    StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(Filename);
    if (noteFile is null)
    {
        noteFile = await storageFolder.CreateFileAsync(Filename, CreationCollisionOption.ReplaceExisting);
    }
    await FileIO.WriteTextAsync(noteFile, Text);
    // ↓ Add this. ↓
    State = NoteState.Saved;
    // ↑ Add this. ↑
}

public async Task DeleteAsync()
{
    // Delete the note from the file system.
    StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(Filename);
    if (noteFile is not null)
    {
        await noteFile.DeleteAsync();
    }
    Filename = string.Empty;
    // ↓ Add this. ↓
    State = NoteState.Deleted;
    // ↑ Add this. ↑
}
```

The note `State` also needs to be set when the notes are initially loaded from the file system. By default, the `State` is `Unset` when a new note is created in the editor but hasn't been saved. However, when a previously saved note is loaded from the file system, it should have an initial `State` of `Saved`.

In `AllNotes.cs`, find the `GetFilesInFolderAsync` method. Then update the code to create a new `Note` object with an initial `State` of `Saved`.

```csharp
Note note = new Note()
{
    Filename = file.Name,
    Text = await FileIO.ReadTextAsync(file),
    Date = file.DateCreated.DateTime, // << Add a comma here.
    // ↓ Add this. ↓
    State = NoteState.Saved
    // ↑ Add this. ↑
};
```

> [!div class="nextstepaction"]
> [Continue to step 3 - Pass navigation parameters](3-navigation-parameters.md)
