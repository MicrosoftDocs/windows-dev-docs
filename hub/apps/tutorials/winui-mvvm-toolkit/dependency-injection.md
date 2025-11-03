---
title: Data Binding with WinUI and MVVM Toolkit - Step 3 - Add dependency injection
description: Add dependency injection to manage the lifecycle of your ViewModels and services in your WinUI app.
ms.date: 10/29/2025
ms.topic: tutorial
keywords: windows 11, windows app sdk, winui, windows ui, mvvm, mvvm toolkit, dotnet, dependency injection
ms.localizationpriority: medium
---

# Add dependency injection

Dependency injection (DI) helps you manage the lifecycle of your ViewModels and services. It makes your code more testable and easier to maintain. In this step, you configure DI in your app and update your models to use a file service for file operations.

For more background on the .NET dependency injection framework, see [.NET dependency injection](/dotnet/core/extensions/dependency-injection) and the [Use dependency injection in .NET](/dotnet/core/extensions/dependency-injection-usage) tutorial.

## Install Microsoft.Extensions packages

Add DI support to your projects.

1. Install `Microsoft.Extensions.DependencyInjection` in both **WinUINotes** and **WinUINotes.Bus** projects:

   ```powershell
   dotnet add WinUINotes package Microsoft.Extensions.DependencyInjection
   dotnet add WinUINotes.Bus package Microsoft.Extensions.DependencyInjection
   ```

## Create a file service interface and implementation

1. In the **WinUINotes.Bus** project, create a new folder named **Services**.
1. Add an interface file `IFileService.cs`:

   ```csharp
   using System.Collections.Generic;
   using System.Threading.Tasks;
   using Windows.Storage;

   namespace WinUINotes.Services
   {
       public interface IFileService
       {
           Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync();
           Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync(IStorageFolder storageFolder);
           Task<string> GetTextFromFileAsync(IStorageFile file);
           Task CreateOrUpdateFileAsync(string filename, string contents);
           Task DeleteFileAsync(string filename);
           bool FileExists(string filename);
           IStorageFolder GetLocalFolder();
       }
   }
   ```

   The file service interface defines methods for file operations. It abstracts away the details of file handling from the ViewModels and Models. The parameters and return values are all either basic .NET types or interfaces. This design ensures that the service can be easily mocked or replaced in unit tests, promoting loose coupling and testability.

1. Add the implementation file `WindowsFileService.cs`:

   ```csharp
   using System;
   using System.Collections.Generic;
   using System.Threading.Tasks;
   using Windows.Storage;

   namespace WinUINotes.Services
   {
       public class WindowsFileService : IFileService
       {
            public StorageFolder storageFolder;

            public WindowsFileService(IStorageFolder storageFolder)
            {
                this.storageFolder = (StorageFolder)storageFolder;

                if (this.storageFolder is null)
                {
                    throw new ArgumentException("storageFolder must be of type StorageFolder", nameof(storageFolder));
                }
            }

            public async Task CreateOrUpdateFileAsync(string filename, string contents)
            {
                // Save the note to a file.
                StorageFile storageFile = (StorageFile)await storageFolder.TryGetItemAsync(filename);
                if (storageFile is null)
                {
                    storageFile = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                }
                await FileIO.WriteTextAsync(storageFile, contents);
            }

        public async Task DeleteFileAsync(string filename)
        {
            // Delete the note from the file system.
            StorageFile storageFile = (StorageFile)await storageFolder.TryGetItemAsync(filename);
            if (storageFile is not null)
            {
                await storageFile.DeleteAsync();
            }
        }

        public bool FileExists(string filename)
        {
            StorageFile storageFile = (StorageFile)storageFolder.TryGetItemAsync(filename).AsTask().Result;
            return storageFile is not null;
        }

        public IStorageFolder GetLocalFolder()
        {
            return storageFolder;
        }

        public async Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync()
        {
            return await storageFolder.GetItemsAsync();
        }

        public async Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync(IStorageFolder folder)
        {
            return await folder.GetItemsAsync();
        }

        public async Task<string> GetTextFromFileAsync(IStorageFile file)
        {
            return await FileIO.ReadTextAsync(file);
        }
       }
   }
   ```

The `WindowsFileService` implementation provides concrete file operations by using the Windows Runtime (WinRT) and .NET storage APIs:

- **Constructor injection**: The service accepts an `IStorageFolder` in its constructor. This approach allows you to configure the storage location when you instantiate the service. This approach makes the service flexible and testable.
- **`CreateOrUpdateFileAsync()`**: This method uses `TryGetItemAsync()` to check if a file already exists. If it does, the method updates the existing file. Otherwise, it creates a new file by using `CreateFileAsync()`. This approach handles both create and update scenarios in a single method.
- **`DeleteFileAsync()`**: Before deleting a file, this method verifies that the file exists by using `TryGetItemAsync()`. This check prevents exceptions from being thrown when attempting to delete non-existent files.
- **`FileExists()`**: This synchronous method checks for file existence by calling the async `TryGetItemAsync()` and blocking with `.Result`. While this approach is generally not recommended, it's used here to support the `CanDelete()` validation method in the ViewModel, which must be synchronous.
- **Storage item methods**: The `GetStorageItemsAsync()` and `GetTextFromFileAsync()` methods provide access to files and their contents by using WinRT storage APIs. These methods enable the Models to load and enumerate notes.

By implementing the `IFileService` interface, you can easily replace this class with a mock implementation for testing or a different storage provider if needed.

:::image type="icon" source="../winui-notes/media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Dependency injection in .NET](/dotnet/core/extensions/dependency-injection)
- [StorageFolder class](/uwp/api/windows.storage.storagefolder)
- [FileIO class](/uwp/api/windows.storage.fileio)

## Configure dependency injection in App.xaml.cs

Before updating the models and ViewModels to use the file service, configure dependency injection so the service can be resolved and injected into the constructors.

Update the `App.xaml.cs` file to set up the DI container:

```csharp
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using WinUINotes.ViewModels;

namespace WinUINotes;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Services
        services.AddSingleton<Services.IFileService>(x =>
            ActivatorUtilities.CreateInstance<Services.WindowsFileService>(x,
                            Windows.Storage.ApplicationData.Current.LocalFolder)
        );

        // ViewModels
        services.AddTransient<AllNotesViewModel>();
        services.AddTransient<NoteViewModel>();

        return services.BuildServiceProvider();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();
    }

    public IServiceProvider Services { get; }

    private Window? m_window;

    public new static App Current => (App)Application.Current;
}
```

This configuration sets up the dependency injection container with all the required services:

- **`ConfigureServices()` method**: A static method that creates and configures the service collection. Separating this method makes the configuration more maintainable and easier to test.
- **`Services` property**: An instance property that holds the `IServiceProvider`. The constructor sets this property by calling `ConfigureServices()`.
- **`App.Current` static property**: Provides convenient access to the current `App` instance, which is useful when models or other classes need to access the service provider.
- **`IFileService` registration**: Uses `ActivatorUtilities.CreateInstance` to create a `WindowsFileService` instance with the `ApplicationData.Current.LocalFolder` as a parameter. This approach allows the constructor parameter to be injected at registration time. Register the service as a singleton since file operations are stateless and a single instance can be shared across the application.
- **ViewModels registration**: Register both ViewModels as transient, meaning a new instance is created each time one is requested. This approach ensures each page gets its own ViewModel instance with clean state.

Models and other classes can access the service provider through `App.Current.Services.GetService()` to retrieve registered services when needed.

:::image type="icon" source="../winui-notes/media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [IServiceProvider interface](/dotnet/api/system.iserviceprovider)
- [ServiceCollection class](/dotnet/api/microsoft.extensions.dependencyinjection.servicecollection)
- [ActivatorUtilities class](/dotnet/api/microsoft.extensions.dependencyinjection.activatorutilities)

## Update models to use the file service

Now that the file service is available through dependency injection, update the model classes to use it. The models receive the file service and use it for all file operations.

### Update the Note model

Update the `Note` class to accept the file service and use it for save, delete, and file existence operations:

```csharp
using System;
using System.Threading.Tasks;
using WinUINotes.Services;

namespace WinUINotes.Models;

public class Note
{
    private IFileService fileService;
    public string Filename { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;

    public Note(IFileService fileService)
    {
        Filename = "notes" + DateTime.Now.ToBinary().ToString() + ".txt";
        this.fileService = fileService;
    }

    public async Task SaveAsync()
    {
        await fileService.CreateOrUpdateFileAsync(Filename, Text);
    }

    public async Task DeleteAsync()
    {
        await fileService.DeleteFileAsync(Filename);
    }

    public bool NoteFileExists()
    {
        return fileService.FileExists(Filename);
    }
}
```

The `Note` model now receives the file service through constructor injection:

- **Constructor**: Accepts an `IFileService` parameter, making the dependency explicit and required. This design promotes testability and ensures the model always has access to the file service it needs.
- **Filename generation**: The constructor automatically generates a unique filename by using the current timestamp, ensuring each note has a distinct filename.
- **File operations**: The `SaveAsync()`, `DeleteAsync()`, and `NoteFileExists()` methods all delegate to the injected file service, keeping the model focused on coordinating operations rather than implementing file I/O details.

This approach eliminates the need for the model to use the service locator pattern (accessing `App.Services` directly), which improves testability and makes dependencies clear.

### Update the AllNotes model

Update the `AllNotes` class to load notes from storage by using the file service:

```csharp
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using WinUINotes.Services;

namespace WinUINotes.Models;

public class AllNotes
{
        private IFileService fileService;
        public ObservableCollection<Note> Notes { get; set; } = [];

        public AllNotes(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task LoadNotes()
        {
            Notes.Clear();
            await GetFilesInFolderAsync(fileService.GetLocalFolder());
        }

        private async Task GetFilesInFolderAsync(IStorageFolder folder)
        {
            // Each StorageItem can be either a folder or a file.
            IReadOnlyList<IStorageItem> storageItems =
                                        await fileService.GetStorageItemsAsync(folder);
            foreach (IStorageItem item in storageItems)
            {
                if (item.IsOfType(StorageItemTypes.Folder))
                {
                    // Recursively get items from subfolders.
                    await GetFilesInFolderAsync((IStorageFolder)item);
                }
                else if (item.IsOfType(StorageItemTypes.File))
                {
                    IStorageFile file = (IStorageFile)item;
                    Note note = new(fileService)
                    {
                        Filename = file.Name,
                        Text = await fileService.GetTextFromFileAsync(file),
                        Date = file.DateCreated.DateTime
                    };
                    Notes.Add(note);
                }
            }
        }
}
```

The `AllNotes` model receives the file service through constructor injection, just like the `Note` model. Since this class is in the `WinUINotes.Bus` project, it can't access `App.Current.Services` from the `WinUINotes` project (due to project reference constraints). 

The `LoadNotes()` method calls the private `GetFilesInFolderAsync()` method to recursively enumerate all files in the local storage folder and its subfolders. For each storage item:
1. If it's a folder, the method recursively calls itself to process the folder's contents
1. If it's a file, it creates a new `Note` instance with the file service injected
1. The note's `Filename` is set to the file's name
1. The note's `Text` is populated by reading the file's contents by using `GetTextFromFileAsync()`
1. The note's `Date` is set to the file's creation date
1. The note is added to the `Notes` observable collection

This approach ensures all notes loaded from storage have access to the file service they need for future save and delete operations.

## Update ViewModels to use the file service

With the models now using the file service, you need to update the ViewModels. However, since the models handle the file operations directly, the ViewModels primarily focus on orchestrating the models and managing observable properties.

### Update AllNotesViewModel

Update the `AllNotesViewModel` to work with the updated `AllNotes` model:

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WinUINotes.Models;
using WinUINotes.Services;

namespace WinUINotes.ViewModels
{
    public partial class AllNotesViewModel : ObservableObject
    {
        private readonly AllNotes allNotes;

        [ObservableProperty]
        private ObservableCollection<Note> notes;

        public AllNotesViewModel(IFileService fileService)
        {
            allNotes = new AllNotes(fileService);
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

**What changed since Step 2?**

The key change is the addition of the `IFileService` parameter to the constructor. In Step 2, the ViewModel instantiated `AllNotes` with a parameterless constructor (`allNotes = new AllNotes()`). Now that the `AllNotes` model requires the file service to perform its operations, the ViewModel receives the `IFileService` through constructor injection and passes it to the model.

This change maintains proper dependency flow - the file service is injected at the top level (ViewModel) and flows down to the model. The ViewModel continues to focus on coordinating the loading process and keeping the observable `Notes` collection synchronized with the model's data, without needing to know the implementation details of how files are loaded.

### Update NoteViewModel

Update the `NoteViewModel` to inject the file service and use the MVVM Toolkit's messaging system:

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using WinUINotes.Models;
using WinUINotes.Services;

namespace WinUINotes.ViewModels
{
    public partial class NoteViewModel : ObservableObject
    {
        private Note note;
        private IFileService fileService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private string filename = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string text = string.Empty;

        [ObservableProperty]
        private DateTime date = DateTime.Now;

        public NoteViewModel(IFileService fileService)
        {
            this.fileService = fileService;
            this.note = new Note(fileService);
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
            note = new Note(fileService);
            // Send a message from some other module
            WeakReferenceMessenger.Default.Send(new NoteDeletedMessage(note));
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

**What changed since Step 2?**

Several important changes support dependency injection and inter-ViewModel communication:

1. **File service injection**: The constructor now accepts `IFileService` as a parameter and stores it in a field. This service is passed to the `Note` model when creating new instances, ensuring all notes can perform file operations.

1. **WeakReferenceMessenger**: The `Delete()` method now uses the MVVM Toolkit's `WeakReferenceMessenger.Default.Send()` to broadcast a `NoteDeletedMessage` after deleting a note. This approach enables loose coupling between ViewModels - other parts of the application (like `NotePage`) can listen for this message and respond appropriately (for example, by navigating back to the list of notes, which has refreshed) without the `NoteViewModel` needing a direct reference to them.

The `WeakReferenceMessenger` is a key feature of the MVVM Toolkit that prevents memory leaks by using weak references. Components can subscribe to messages without creating strong references that would prevent garbage collection.

:::image type="icon" source="../winui-notes/media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [WeakReferenceMessenger class](/dotnet/communitytoolkit/mvvm/messenger)
- [Messaging overview](/dotnet/communitytoolkit/mvvm/messenger#overview)

### Create the NoteDeletedMessage class

The `WeakReferenceMessenger` needs a message class to send between components. Create a new class to represent the note deletion event:

1. In the **WinUINotes.Bus** project, add a new class file `NoteDeletedMessage.cs`:

   ```csharp
   using CommunityToolkit.Mvvm.Messaging.Messages;
   using WinUINotes.Models;
    
   namespace WinUINotes
   {
       public class NoteDeletedMessage : ValueChangedMessage<Note>
       {
           public NoteDeletedMessage(Note note) : base(note)
           {
           }
       }
   }
   ```

This message class inherits from `ValueChangedMessage<Note>`, which is a specialized message type provided by the MVVM Toolkit for carrying value change notifications. The constructor accepts a `Note` and passes it to the base class, making it available to message recipients through the `Value` property. When `NoteViewModel` sends this message, any component that subscribes to `NoteDeletedMessage` receives it and can access the deleted note through the `Value` property.

**How messaging works in the MVVM Toolkit:**

1. **Sender**: The `NoteViewModel.Delete()` method sends the message by using `WeakReferenceMessenger.Default.Send(new NoteDeletedMessage(note))`.
1. **Receiver**: Pages (like `NotePage`) can register to receive messages by implementing `IRecipient<NoteDeletedMessage>` and registering with the messenger. When the message is received, the page can navigate back to the all notes list.
1. **Loose coupling**: The sender doesn't need to know who (if anyone) is listening. The receiver doesn't need a direct reference to the sender. This setup keeps your components independent and testable.

The weak reference approach means that if a component is garbage collected, its message subscription is automatically cleaned up without causing memory leaks.

## Update pages to use dependency injection

Update your page constructors to receive the ViewModels through DI.

### Update AllNotesPage.xaml.cs

```csharp
using Microsoft.Extensions.DependencyInjection;
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
            viewModel = App.Current.Services.GetService<AllNotesViewModel>();
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

**What changed since Step 2?**

The app now gets the `AllNotesViewModel` from the dependency injection container by using `App.Current.Services.GetService<AllNotesViewModel>()` instead of creating it directly with `new AllNotesViewModel()`. This approach has several benefits:

1. **Automatic dependency resolution**: The DI container automatically provides the `IFileService` dependency that `AllNotesViewModel` requires in its constructor.
1. **Lifecycle management**: The DI container manages the ViewModel's lifecycle according to how it was registered (as a transient in this case, providing a fresh instance).
1. **Testability**: This pattern makes it easier to swap implementations or mock dependencies in tests.
1. **Maintainability**: If the ViewModel's dependencies change in the future, you only need to update the DI configuration, not every place where the ViewModel is created.

The rest of the code stays the same. The `OnNavigatedTo()` method still calls `LoadAsync()` to refresh the notes list when the user navigates to this page.

### Update NotePage.xaml.cs

```csharp
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
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

        public void RegisterForDeleteMessages()
        {
            WeakReferenceMessenger.Default.Register<NoteDeletedMessage>(this, (r, m) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            noteVm = App.Current.Services.GetService<NoteViewModel>();
            RegisterForDeleteMessages();

            if (e.Parameter is Note note && noteVm is not null)
            {
                noteVm.InitializeForExistingNote(note);
            }
        }
    }
}
```

**What changed since Step 2?**

Several important changes integrate dependency injection and messaging features:

1. **ViewModel from DI container**: The `NoteViewModel` is now retrieved from the dependency injection container by using `App.Current.Services.GetService<NoteViewModel>()` in the `OnNavigatedTo()` method instead of being instantiated directly. This approach ensures the ViewModel automatically receives its required `IFileService` dependency.
1. **Message registration**: The new `RegisterForDeleteMessages()` method subscribes to `NoteDeletedMessage` by using the `WeakReferenceMessenger`. When a note is deleted (from the `NoteViewModel.Delete()` method), this page receives the message and navigates back to the all notes list by using `Frame.GoBack()`.
1. **Messaging pattern**: This pattern demonstrates the loose coupling enabled by the MVVM Toolkit's messaging system. The `NoteViewModel` doesn't need to know about navigation or the page structure - it simply sends a message when a note is deleted, and the page handles the navigation response independently.
1. **Lifecycle timing**: The ViewModel is instantiated and message registration occurs in `OnNavigatedTo()`, ensuring everything is properly initialized when the page becomes active.

This pattern separates concerns effectively: the ViewModel focuses on business logic and data operations, while the page handles UI-specific concerns like navigation.

> [!div class="nextstepaction"]
> [Continue to step 4 - Add unit testing](unit-testing.md)
