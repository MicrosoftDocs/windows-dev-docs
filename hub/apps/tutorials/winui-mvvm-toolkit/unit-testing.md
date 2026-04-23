---
title: Data Binding with WinUI and MVVM Toolkit - Step 4 - Add unit tests
description: Create a unit test project to test your ViewModels and services independently of the UI layer.
ms.date: 10/29/2025
ms.topic: tutorial
keywords: windows 11, windows app sdk, winui, windows ui, mvvm, mvvm toolkit, dotnet, unit testing
ms.localizationpriority: medium
---

# Add unit tests

Now that your ViewModels and services are in a separate class library, you can easily create unit tests. Adding unit test projects lets you verify that your ViewModels and services behave as expected without relying on the UI layer or manual testing. You can run unit tests automatically as part of your development workflow, ensuring that your code remains reliable and maintainable.

## Create a unit test project

1. Right-click the solution in the **Solution Explorer**.
1. Select **Add** > **New Project...**.
1. Choose the **WinUI Unit Test App** template and select **Next**.
1. Name the project `WinUINotes.Tests` and select **Create**.

## Add project references

1. Right-click the **WinUINotes.Tests** project and select **Add** > **Project Reference...**.
1. Check the **WinUINotes.Bus** project and select **OK**.

## Create fake implementations for testing

For testing, create fake implementations of the file service and storage classes that don't actually write to disk. Fakes are lightweight implementations that simulate the behavior of real dependencies for testing purposes.

1. In the **WinUINotes.Tests** project, create a new folder named **Fakes**.
1. Add a class file `FakeFileService.cs` in the Fakes folder:

   ```csharp
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading.Tasks;
   using Windows.Storage;
   using WinUINotes.Services;

   namespace WinUINotes.Tests.Fakes
   {
       internal class FakeFileService : IFileService
       {
           private Dictionary<string, string> fileStorage = [];

           public async Task CreateOrUpdateFileAsync(string filename, string contents)
           {
               if (fileStorage.ContainsKey(filename))
               {
                   fileStorage[filename] = contents;
               }
               else
               {
                   fileStorage.Add(filename, contents);
               }

               await Task.Delay(10); // Simulate some async work
           }

           public async Task DeleteFileAsync(string filename)
           {
               if (fileStorage.ContainsKey(filename))
               {
                   fileStorage.Remove(filename);
               }

               await Task.Delay(10); // Simulate some async work
           }

           public bool FileExists(string filename)
           {
               if (string.IsNullOrEmpty(filename))
               {
                   throw new ArgumentException("Filename cannot be null or empty", nameof(filename));
               }

               if (fileStorage.ContainsKey(filename))
               {
                   return true;
               }

               return false;
           }

           public IStorageFolder GetLocalFolder()
           {
               return new FakeStorageFolder(fileStorage);
           }

           public async Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync()
           {
               await Task.Delay(10);
               return GetStorageItemsInternal();
           }

           public async Task<IReadOnlyList<IStorageItem>> GetStorageItemsAsync(IStorageFolder storageFolder)
           {
               await Task.Delay(10);
               return GetStorageItemsInternal();
           }

           private IReadOnlyList<IStorageItem> GetStorageItemsInternal()
           {
               return fileStorage.Keys.Select(filename => CreateFakeStorageItem(filename)).ToList();
           }

           private IStorageItem CreateFakeStorageItem(string filename)
           {
               return new FakeStorageFile(filename);
           }

           public async Task<string> GetTextFromFileAsync(IStorageFile file)
           {
               await Task.Delay(10);

               if (fileStorage.ContainsKey(file.Name))
               {
                   return fileStorage[file.Name];
               }

               return string.Empty;
           }
       }
   }
   ```

   The `FakeFileService` uses an in-memory dictionary (`fileStorage`) to simulate file operations without touching the actual file system. Key features include:
   
   - **Async simulation**: Uses `Task.Delay(10)` to mimic real async file operations
   - **Validation**: Throws exceptions for invalid inputs, just like the real implementation
   - **Integration with fake storage classes**: Returns `FakeStorageFolder` and `FakeStorageFile` instances that work together to simulate the Windows Storage API

1. Add `FakeStorageFolder.cs`:

   ```csharp
   using System;
   using System.Collections.Generic;
   using System.Runtime.InteropServices.WindowsRuntime;
   using Windows.Foundation;
   using Windows.Storage;
   using Windows.Storage.FileProperties;
   using Windows.Storage.Search;

   namespace WinUINotes.Tests.Fakes
   {
       internal class FakeStorageFolder : IStorageFolder
       {
           private string name;
           private Dictionary<string, string> fileStorage = [];

           public FakeStorageFolder(Dictionary<string, string> files)
           {
               fileStorage = files;
           }

           public FileAttributes Attributes => throw new NotImplementedException();
           public DateTimeOffset DateCreated => throw new NotImplementedException();
           public string Name => name;
           public string Path => throw new NotImplementedException();

           public IAsyncOperation<StorageFile> CreateFileAsync(string desiredName)
           {
               throw new NotImplementedException();
           }

           public IAsyncOperation<StorageFile> CreateFileAsync(string desiredName, CreationCollisionOption options)
           {
               throw new NotImplementedException();
           }

           public IAsyncOperation<StorageFolder> CreateFolderAsync(string desiredName)
           {
               throw new NotImplementedException();
           }

           // Only partial implementation shown for brevity
           ...
       }
   }
   ```

   The `FakeStorageFolder` takes the file storage dictionary in its constructor, allowing it to work with the same in-memory file system as `FakeFileService`. Most interface members throw `NotImplementedException` since only the properties and methods actually used by the tests need to be implemented.

   You can view the complete implementation of `FakeStorageFolder` in the [GitHub code repository](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes-mvvm-toolkit) for this tutorial.

1. Add `FakeStorageFile.cs`:

   ```csharp
   using System;
   using System.IO;
   using System.Runtime.InteropServices.WindowsRuntime;
   using Windows.Foundation;
   using Windows.Storage;
   using Windows.Storage.FileProperties;
   using Windows.Storage.Streams;

   namespace WinUINotes.Tests.Fakes
   {
       public class FakeStorageFile : IStorageFile
       {
           private string name;

           public FakeStorageFile(string name)
           {
               this.name = name;
           }

           public string ContentType => throw new NotImplementedException();
           public string FileType => throw new NotImplementedException();
           public FileAttributes Attributes => throw new NotImplementedException();
           public DateTimeOffset DateCreated => throw new NotImplementedException();
           public string Name => name;
           public string Path => throw new NotImplementedException();

           public IAsyncOperation<StorageFile> CopyAsync(IStorageFolder destinationFolder)
           {
               throw new NotImplementedException();
           }

           public IAsyncOperation<StorageFile> CopyAsync(IStorageFolder destinationFolder, string desiredNewName)
           {
               throw new NotImplementedException();
           }

           public IAsyncOperation<StorageFile> CopyAsync(IStorageFolder destinationFolder, string desiredNewName, NameCollisionOption option)
           {
               throw new NotImplementedException();
           }

           public IAsyncAction CopyAndReplaceAsync(IStorageFile fileToReplace)
           {
               throw new NotImplementedException();
           }

           // Only partial implementation shown for brevity
           ...
       }
   }
   ```

   The `FakeStorageFile` represents individual files in the fake storage system. It stores the filename and provides the minimal implementation needed for the tests. Like `FakeStorageFolder`, it only implements the members that are actually used by the code being tested.

   You can view the complete implementation of `FakeStorageFolder` in the [GitHub code repository](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes-mvvm-toolkit) for this tutorial.

:::image type="icon" source="../winui-notes/media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [Unit testing best practices](/dotnet/core/testing/unit-testing-best-practices)
- [Test doubles (fakes, mocks, stubs)](/dotnet/core/testing/unit-testing-best-practices#test-doubles)

## Write a simple unit test

1. Rename `UnitTest1.cs` to `NoteTests.cs` and update it:

   ```csharp
   using Microsoft.VisualStudio.TestTools.UnitTesting;
   using System;
   using WinUINotes.Tests.Fakes;
    
   namespace WinUINotes.Tests
   {
       [TestClass]
       public partial class NoteTests
       {
           [TestMethod]
           public void TestCreateUnsavedNote()
           {
               var noteVm = new ViewModels.NoteViewModel(new FakeFileService());
               Assert.IsNotNull(noteVm);
               Assert.IsTrue(noteVm.Date > DateTime.Now.AddHours(-1));
               Assert.IsTrue(noteVm.Filename.EndsWith(".txt"));
               Assert.IsTrue(noteVm.Filename.StartsWith("notes"));
               noteVm.Text = "Sample Note";
               Assert.AreEqual("Sample Note", noteVm.Text);
               noteVm.SaveCommand.Execute(null);
               Assert.AreEqual("Sample Note", noteVm.Text);
           }
       }
   }
   ```

   This test shows how to unit test the `NoteViewModel` by using the `FakeFileService`. The test creates a new `NoteViewModel`, checks its initial state (date is recent, filename follows the expected pattern), sets text on the note, runs the save command, and confirms the text persists. Because the fake file service is used instead of the real implementation, the test runs quickly without any actual file I/O and can run repeatedly without side effects.

:::image type="icon" source="../winui-notes/media/doc-icon-sm.png" border="false"::: Learn more in the docs:

- [MSTest overview](/dotnet/core/testing/unit-testing-with-mstest)
- [Assert class](/dotnet/api/microsoft.visualstudio.testtools.unittesting.assert)

## Run the tests

1. Open the **Test Explorer** window in Visual Studio (**Test** > **Test Explorer**).
1. Select **Run All Tests** to execute your unit test.
1. Verify that the test passes.

You now have a testable architecture where you can test your ViewModels and services independently of the UI!

## Summary

In this tutorial series, you learned how to:

- Create a separate class library project (Bus project) to hold your ViewModels and services, enabling unit testing separate from the UI layer.
- Implement the MVVM pattern using the MVVM Toolkit, leveraging `ObservableObject`, `[ObservableProperty]` attributes, and `[RelayCommand]` to reduce boilerplate code.
- Use source generators to automatically create property change notifications and command implementations.
- Use `[NotifyCanExecuteChangedFor]` to automatically update command availability when property values change.
- Integrate dependency injection using `Microsoft.Extensions.DependencyInjection` to manage the lifecycle of ViewModels and services.
- Create an `IFileService` interface and implementation to handle file operations in a testable way.
- Configure the DI container in `App.xaml.cs` and retrieve ViewModels from the service provider in your pages.
- Implement the `WeakReferenceMessenger` to enable loose coupling between components, allowing pages to respond to ViewModel events without direct references.
- Create message classes that inherit from `ValueChangedMessage<T>` to carry data between components.
- Create fake implementations of dependencies for testing without touching the actual file system.
- Write unit tests using MSTest to verify ViewModel behavior independently of the UI layer.

This architecture provides a solid foundation for building maintainable, testable WinUI applications with clear separation of concerns between the UI, business logic, and data access layers. You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes-mvvm-toolkit).

## Next steps

Now that you understand how to implement MVVM with the MVVM Toolkit and dependency injection, you can explore more advanced topics:

- **Advanced Messaging**: Explore additional messaging patterns, including request/response messages and message tokens for selective message handling.
- **Validation**: Add input validation to your ViewModels using data annotations and the MVVM Toolkit's validation features.
- **Async Commands**: Learn more about asynchronous command execution, cancellation support, and progress reporting with `AsyncRelayCommand`.
- **Advanced Testing**: Explore more advanced testing scenarios, including testing message handling, async command execution, and property change notifications.
- **Observable Collections**: Use `ObservableCollection<T>` effectively and explore `ObservableRangeCollection<T>` for bulk operations.

## Related content

- [MVVM Toolkit documentation](/dotnet/communitytoolkit/mvvm/)
- [.NET Community Toolkit](/dotnet/communitytoolkit/introduction)
- [Create a WinUI app tutorial](/windows/apps/tutorials/winui-notes/intro)
- [Data binding overview](/windows/apps/develop/data-binding/data-binding-overview)
- [Dependency injection in .NET](/dotnet/core/extensions/dependency-injection)
- [Unit testing in .NET](/dotnet/core/testing/)
- [MVVM Toolkit samples on GitHub](https://github.com/CommunityToolkit/MVVM-Samples)
