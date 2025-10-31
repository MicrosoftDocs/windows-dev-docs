---
title: Data Binding with WinUI and MVVM Toolkit - Step 2 - Implement MVVM with the MVVM Toolkit
description: Implement data binding in your WinUI app using the MVVM Toolkit with ObservableObject, RelayCommand, and source generators.
ms.date: 10/29/2025
ms.topic: tutorial
keywords: windows 11, windows app sdk, winui, windows ui, mvvm, mvvm toolkit, dotnet
ms.localizationpriority: medium
---

# Implement MVVM with the MVVM Toolkit

Now that you have the project structure in place, you can start implementing the MVVM pattern by using the MVVM Toolkit. This step involves creating ViewModels that leverage the MVVM Toolkit's features, such as `ObservableObject` for property change notification and `RelayCommand` for command implementation.

## Install the MVVM Toolkit NuGet package

You need to install the MVVM Toolkit in both the **WinUINotes** and **WinUINotes.Bus** projects.

### Using Visual Studio

1. Right-click on the **WinUINotes.Bus** project in the Solution Explorer.
1. Select **Manage NuGet Packages**.
1. Search for **CommunityToolkit.Mvvm** and install the latest stable version.
1. Repeat these steps for the **WinUINotes** project.

### Using .NET CLI

Alternatively, you can use the .NET CLI to install the package:

```powershell
dotnet add WinUINotes.Bus package CommunityToolkit.Mvvm
dotnet add WinUINotes package CommunityToolkit.Mvvm
```

## Design decisions for the model layer

When you implement MVVM, it's important to decide how to structure your model classes in relation to the ViewModels. In this tutorial, the model classes (`Note` and `AllNotes`) are responsible for data representation, business logic, and updating data storage. The ViewModels handle observable properties, change notification, and commands for UI interaction.

In a simpler implementation, you might use plain old CLR objects (POCOs) for the model classes without any business logic or data access methods. In that case, the ViewModels handle all data operations through the service layer. However, for this tutorial, the model classes include methods for loading, saving, and deleting notes to provide a clearer separation of concerns and keep the ViewModels focused on presentation logic.

### Move the Note model

Move the `Note` class to the **WinUINotes.Bus** project. It remains a simple model class with some logic for data representation and state management but without any MVVM Toolkit features. The ViewModels handle the observable properties and change notification, not the model itself.

1. In the **WinUINotes.Bus** project, create a new folder named **Models**.
1. Move the `Note.cs` file from the **WinUINotes** project to the **WinUINotes.Bus/Models** folder.
1. Update the namespace to match the new location:

   ```csharp
   namespace WinUINotes.Models
   {
       public class Note
       {
           // Existing code remains unchanged
           ...
       }
   }
   ```

The `Note` class is a simple data model. It doesn't need change notification because the ViewModels manage observable properties and notify the UI of changes.

### Move the AllNotes model

Move the `AllNotes` class to the **WinUINotes.Bus** project.

1. Move the `AllNotes.cs` file from the **WinUINotes** project to the **WinUINotes.Bus/Models** folder.
1. Update the namespace to match the new location:

   ```csharp
   namespace WinUINotes.Models
   {
       public class AllNotes
       {
           // Existing code remains unchanged
           ...
       }
   }
   ```

Like the `Note` class, `AllNotes` is a simple model class. The ViewModel handles the observable behavior and manages the collection of notes.

## Create the AllNotesViewModel

1. In the **WinUINotes.Bus** project, create a new folder named **ViewModels**.
1. Add a new class file named `AllNotesViewModel.cs` with the following content:

   ```csharp
   using CommunityToolkit.Mvvm.ComponentModel;
   using CommunityToolkit.Mvvm.Input;
   using System.Collections.ObjectModel;
   using System.Threading.Tasks;
   using WinUINotes.Models;

   namespace WinUINotes.ViewModels
   {
       public partial class AllNotesViewModel : ObservableObject
       {
           private readonly AllNotes allNotes;

           [ObservableProperty]
           private ObservableCollection<Note> notes;

           public AllNotesViewModel()
           {
               allNotes = new AllNotes();
               notes = new ObservableCollection<Note>();
           }

           [RelayCommand]
           public async Task LoadAsync()
           {
               await allNotes.LoadNotes();
               Notes.Clear();
               foreach (var note in allNotes.Notes)
               {
                   Notes.Add(note);
               }
           }
       }
   }
   ```

The `AllNotesViewModel` manages the collection of notes displayed in the UI:

- **`[ObservableProperty]`**: The `notes` field automatically generates a public `Notes` property with change notification. When the `Notes` collection changes, the UI automatically updates.
- **`allNotes` model**: This private field holds an instance of the `AllNotes` model, which handles the actual data operations.
- **`[RelayCommand]`**: This attribute generates a `LoadCommand` property from the `LoadAsync()` method, allowing the UI to trigger the loading operation through data binding.
- **`LoadAsync()` method**: This method loads notes from the model, clears the current observable collection, and populates it with the loaded notes. This pattern ensures the UI-bound collection stays synchronized with the underlying data.

The separation between the `allNotes` model (data operations) and the `Notes` observable collection (UI binding) is a key MVVM pattern that keeps concerns separated and the View in sync with the ViewModel's data.

:::image type="icon" source="../winui-notes/media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [ObservableObject](/dotnet/api/communitytoolkit.mvvm.componentmodel.observableobject)
- [ObservableProperty attribute](/dotnet/communitytoolkit/mvvm/generators/observableproperty)
- [RelayCommand attribute](/dotnet/communitytoolkit/mvvm/generators/relaycommand)

## Create the NoteViewModel

1. In the **ViewModels** folder, add a new class file named `NoteViewModel.cs`:

   ```csharp
   using CommunityToolkit.Mvvm.ComponentModel;
   using CommunityToolkit.Mvvm.Input;
   using System;
   using System.Threading.Tasks;
   using WinUINotes.Models;

   namespace WinUINotes.ViewModels
   {
       public partial class NoteViewModel : ObservableObject
       {
           private Note note;

           [ObservableProperty]
           [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
           [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
           private string filename = string.Empty;

           [ObservableProperty]
           [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
           private string text = string.Empty;

           [ObservableProperty]
           private DateTime date = DateTime.Now;

           public NoteViewModel()
           {
               this.note = new Note();
               this.Filename = note.Filename;
           }

           public void InitializeForExistingNote(Note note)
           {
               this.note = note;
               this.Filename = note.Filename;
               this.Text = note.Text;
               this.Date = note.Date;
           }

           [RelayCommand(CanExecute = nameof(CanSave))]
           private async Task Save()
           {
               note.Filename = this.Filename;
               note.Text = this.Text;
               note.Date = this.Date;
               await note.SaveAsync();

               // Check if the DeleteCommand can now execute
               // (it can if the file now exists)
               DeleteCommand.NotifyCanExecuteChanged();
           }

           private bool CanSave()
           {
               return note is not null
                   && !string.IsNullOrWhiteSpace(this.Text)
                   && !string.IsNullOrWhiteSpace(this.Filename);
           }

           [RelayCommand(CanExecute = nameof(CanDelete))]
           private async Task Delete()
           {
               await note.DeleteAsync();
               note = new Note();
           }

           private bool CanDelete()
           {
               // Note: This is to illustrate how commands can be
               // enabled or disabled.
               // In a real application, you shouldn't perform
               // file operations in your CanExecute logic.
               return note is not null
                   && !string.IsNullOrWhiteSpace(this.Filename)
                   && this.note.NoteFileExists();
           }
       }
   }
   ```

The `NoteViewModel` demonstrates several key MVVM Toolkit features:

- **`[ObservableProperty]`**: The `filename`, `text`, and `date` fields automatically generate public properties (`Filename`, `Text`, `Date`) with change notification support.
- **`[NotifyCanExecuteChangedFor]`**: This attribute ensures that when `Filename` or `Text` changes, the associated commands re-evaluate whether they can execute. For example, when you type text, the Save button automatically enables or disables based on the validation logic.
- **`[RelayCommand(CanExecute = nameof(CanSave))]`**: This attribute generates a `SaveCommand` property that's bound to the validation method `CanSave()`. The command is only enabled when both `Text` and `Filename` have values.
- **`InitializeForExistingNote()`**: This method loads an existing note's data into the ViewModel properties, which then update the UI through data binding.
- **Save logic**: The `Save()` method updates the underlying `Note` model with the current property values and calls `SaveAsync()` on the model. After saving, it notifies the `DeleteCommand` that it should re-evaluate (since a file now exists and can be deleted).
- **Delete logic**: The `Delete()` method calls `DeleteAsync()` on the note model and creates a new empty note.

Later in this tutorial, you integrate the file service to handle the actual file operations and use the MVVM Toolkit's `WeakReferenceMessenger` class to notify other parts of the app when a note is deleted while remaining loosely coupled.

:::image type="icon" source="../winui-notes/media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [NotifyCanExecuteChangedFor attribute](/dotnet/communitytoolkit/mvvm/generators/observableproperty#notifycanexecutechangedfor)
- [Asynchronous commands](/dotnet/communitytoolkit/mvvm/relaycommand#asynchronous-commands)

## Update the views to use the ViewModels

Now you need to update your XAML pages to bind to the new ViewModels.

### Update AllNotesPage view

1. In `AllNotesPage.xaml`, update the `ItemsSource` binding of the `ItemsView` to use the ViewModel's `Notes` property:

   ```xml
   <ItemsView ItemsSource="{x:Bind viewModel.Notes}"
   ...
   ```

1. Update the `AllNotesPage.xaml.cs` file to look like this:

   ```csharp
   using Microsoft.UI.Xaml;
   using Microsoft.UI.Xaml.Controls;
   using Microsoft.UI.Xaml.Navigation;
   using WinUINotes.ViewModels;

   namespace WinUINotes.Views
   {
       public sealed partial class AllNotesPage : Page
       {
           private AllNotesViewModel? viewModel;

           public AllNotesPage()
           {
               this.InitializeComponent();
               viewModel = new AllNotesViewModel();
           }

           private void NewNoteButton_Click(object sender, RoutedEventArgs e)
           {
               Frame.Navigate(typeof(NotePage));
           }

           private void ItemsView_ItemInvoked(ItemsView sender, ItemsViewItemInvokedEventArgs args)
           {
               Frame.Navigate(typeof(NotePage), args.InvokedItem);
           }

           protected override async void OnNavigatedTo(NavigationEventArgs e)
           {
               base.OnNavigatedTo(e);

               if (viewModel is not null)
               {
                   await viewModel.LoadAsync();
               }
           }
       }
   }
   ```

In this code-behind file, the constructor instantiates the `AllNotesViewModel` directly. The `OnNavigatedTo()` method calls the `LoadAsync()` method on the ViewModel when the page is navigated to. This method loads the notes from storage and updates the observable collection. This pattern ensures the data is always refreshed when the user navigates to the all notes page.

Later in this tutorial, you refactor this code to use dependency injection, which allows the ViewModel to be injected into the page constructor instead of being created directly. This approach improves testability and makes it easier to manage ViewModel lifecycles.

### Update the NotePage view

1. In `NotePage.xaml`, update the `TextBox` bindings for `Text` and `Header` to use the ViewModel's properties. Update the `StackPanel` buttons to bind to the commands instead of using the `Click` events:

   ```xml
   ...
   <TextBox x:Name="NoteEditor"
            Text="{x:Bind noteVm.Text, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
            AcceptsReturn="True"
            TextWrapping="Wrap"
            PlaceholderText="Enter your note"
            Header="{x:Bind noteVm.Date.ToString()}"
            ScrollViewer.VerticalScrollBarVisibility="Auto"
            MaxWidth="400"
            Grid.Column="1"/>

   <StackPanel Orientation="Horizontal"
               HorizontalAlignment="Right"
               Spacing="4"
               Grid.Row="1" Grid.Column="1">
       <Button Content="Save" Command="{x:Bind noteVm.SaveCommand}"/>
       <Button Content="Delete" Command="{x:Bind noteVm.DeleteCommand}"/>
   </StackPanel>
   ...
   ```

   You also set `UpdateSourceTrigger` on the `TextBox.Text` binding to ensure that changes are sent to the ViewModel as the user types. This setting allows the `Save` button to enable or disable in real-time based on the input.

1. In `NotePage.xaml.cs`, update the code to use the `NoteViewModel`:

   ```csharp
   using Microsoft.UI.Xaml.Controls;
   using Microsoft.UI.Xaml.Navigation;
   using WinUINotes.Models;
   using WinUINotes.ViewModels;

   namespace WinUINotes.Views
   {
       public sealed partial class NotePage : Page
       {
           private NoteViewModel? noteVm;
    
           public NotePage()
           {
               this.InitializeComponent();
           }

           protected override void OnNavigatedTo(NavigationEventArgs e)
           {
               base.OnNavigatedTo(e);
               noteVm = new NoteViewModel();
    
               if (e.Parameter is Note note && noteVm is not null)
               {
                   noteVm.InitializeForExistingNote(note);
               }
           }
       }
   }
   ```

   The `Click` events for `Save` and `Delete` are removed since the buttons now bind directly to the commands in the ViewModel. The `NoteViewModel` is instantiated in the `OnNavigatedTo()` method. If a `Note` parameter is passed, it initializes the ViewModel with the existing note data.

:::image type="icon" source="../winui-notes/media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Data binding overview](/windows/apps/develop/data-binding/data-binding-overview)
- [x:Bind markup extension](/windows/apps/develop/platform/xaml/x-bind-markup-extension)
- [UpdateSourceTrigger property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.binding.updatesourcetrigger)

> [!div class="nextstepaction"]
> [Continue to step 3 - Add dependency injection](dependency-injection.md)
