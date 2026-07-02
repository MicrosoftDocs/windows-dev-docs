---
ms.assetid: 27914C0A-2A02-473F-BDD5-C931E3943AA0
title: Create, write, and read a file
description: Learn how to create, write, and read a file using the WinUI objects FileIO, StorageFolder, and StorageFile.
ms.date: 12/19/2018
ms.topic: article
keywords: windows 10, winui
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---
# Create, write, and read a file

**Important APIs**

-   [StorageFolder class](/uwp/api/windows.storage.storagefolder)
-   [StorageFile class](/uwp/api/windows.storage.storagefile)
-   [FileIO class](/uwp/api/windows.storage.fileio)

Read and write a file using a [StorageFile](/uwp/api/windows.storage.storagefile) object.

> [!NOTE]
> For a complete sample, see the [File access sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileAccess).

## Prerequisites

-   **Understand async programming for WinUI apps**

    You can learn how to write asynchronous apps in C#, see [Call asynchronous APIs in C# or Visual Basic](/windows/uwp/threading-async/call-asynchronous-apis-in-csharp-or-visual-basic). To learn how to write asynchronous apps in C++/WinRT, see [Concurrency and asynchronous operations with C++/WinRT](/windows/apps/develop/cpp-winrt/concurrency).

-   **Know how to get the file that you want to read from, write to, or both**

    You can learn how to get a file by using a file picker in [Open files and folders with a picker](/windows/uwp/files/quickstart-using-file-and-folder-pickers).

## Creating a file

Here's how to create a file in the app's local folder. If it already exists, we replace it.

```csharp
// Create sample file; replace if exists.
Windows.Storage.StorageFolder storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
Windows.Storage.StorageFile sampleFile =
    await storageFolder.CreateFileAsync("sample.txt",
        Windows.Storage.CreationCollisionOption.ReplaceExisting);
```

```cppwinrt
// MainPage.h
#include <winrt/Windows.Storage.h>
...
Windows::Foundation::IAsyncAction ExampleCoroutineAsync()
{
    // Create a sample file; replace if exists.
    Windows::Storage::StorageFolder storageFolder{ Windows::Storage::ApplicationData::Current().LocalFolder() };
    co_await storageFolder.CreateFileAsync(L"sample.txt", Windows::Storage::CreationCollisionOption::ReplaceExisting);
}
```


## Writing to a file

Here's how to write to a writable file on disk using the [StorageFile](/uwp/api/windows.storage.storagefile) class. The common first step for each of the ways of writing to a file (unless you're writing to the file immediately after creating it) is to get the file with [StorageFolder.GetFileAsync](/uwp/api/windows.storage.storagefolder.getfileasync).

```csharp
Windows.Storage.StorageFolder storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
Windows.Storage.StorageFile sampleFile =
    await storageFolder.GetFileAsync("sample.txt");
```

```cppwinrt
// MainPage.h
#include <winrt/Windows.Storage.h>
...
Windows::Foundation::IAsyncAction ExampleCoroutineAsync()
{
    Windows::Storage::StorageFolder storageFolder{ Windows::Storage::ApplicationData::Current().LocalFolder() };
    auto sampleFile{ co_await storageFolder.CreateFileAsync(L"sample.txt", Windows::Storage::CreationCollisionOption::ReplaceExisting) };
    // Process sampleFile
}
```


**Writing text to a file**

Write text to your file by calling the [FileIO.WriteTextAsync](/uwp/api/windows.storage.fileio.writetextasync) method.

```csharp
await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "Swift as a shadow");
```

```cppwinrt
// MainPage.h
#include <winrt/Windows.Storage.h>
...
Windows::Foundation::IAsyncAction ExampleCoroutineAsync()
{
    Windows::Storage::StorageFolder storageFolder{ Windows::Storage::ApplicationData::Current().LocalFolder() };
    auto sampleFile{ co_await storageFolder.GetFileAsync(L"sample.txt") };
    // Write text to the file.
    co_await Windows::Storage::FileIO::WriteTextAsync(sampleFile, L"Swift as a shadow");
}
```


**Writing bytes to a file by using a buffer (2 steps)**

1.  First, call [CryptographicBuffer.ConvertStringToBinary](/uwp/api/windows.security.cryptography.cryptographicbuffer.convertstringtobinary) to get a buffer of the bytes (based on a string) that you want to write to your file.

    ```csharp
    var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(
        "What fools these mortals be", Windows.Security.Cryptography.BinaryStringEncoding.Utf8);
    ```

    ```cppwinrt
    // MainPage.h
    #include <winrt/Windows.Security.Cryptography.h>
    #include <winrt/Windows.Storage.h>
    #include <winrt/Windows.Storage.Streams.h>
    ...
    Windows::Foundation::IAsyncAction ExampleCoroutineAsync()
    {
        Windows::Storage::StorageFolder storageFolder{ Windows::Storage::ApplicationData::Current().LocalFolder() };
        auto sampleFile{ co_await storageFolder.GetFileAsync(L"sample.txt") };
        // Create the buffer.
        Windows::Storage::Streams::IBuffer buffer{
            Windows::Security::Cryptography::CryptographicBuffer::ConvertStringToBinary(
                L"What fools these mortals be", Windows::Security::Cryptography::BinaryStringEncoding::Utf8)};
        // The code in step 2 goes here.
    }
    ```



2.  Then write the bytes from your buffer to your file by calling the [FileIO.WriteBufferAsync](/uwp/api/windows.storage.fileio.writebufferasync) method.

    ```csharp
    await Windows.Storage.FileIO.WriteBufferAsync(sampleFile, buffer);
    ```

    ```cppwinrt
    co_await Windows::Storage::FileIO::WriteBufferAsync(sampleFile, buffer);
    ```
    
    

**Writing text to a file by using a stream (4 steps)**

1.  First, open the file by calling the [StorageFile.OpenAsync](/uwp/api/windows.storage.storagefile.openasync) method. It returns a stream of the file's content when the open operation completes.

    ```csharp
    var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
    ```
    
    ```cppwinrt
    // MainPage.h
    #include <winrt/Windows.Storage.h>
    #include <winrt/Windows.Storage.Streams.h>
    ...
    Windows::Foundation::IAsyncAction ExampleCoroutineAsync()
    {
        Windows::Storage::StorageFolder storageFolder{ Windows::Storage::ApplicationData::Current().LocalFolder() };
        auto sampleFile{ co_await storageFolder.GetFileAsync(L"sample.txt") };
        Windows::Storage::Streams::IRandomAccessStream stream{ co_await sampleFile.OpenAsync(Windows::Storage::FileAccessMode::ReadWrite) };
        // The code in step 2 goes here.
    }
    ```
    
    

2.  Next, get an output stream by calling the [IRandomAccessStream.GetOutputStreamAt](/uwp/api/windows.storage.streams.irandomaccessstream.getoutputstreamat) method from the `stream`. If you're using C#, then enclose this in a using statement to manage the output stream's lifetime. If you're using [C++/WinRT](/windows/apps/develop/cpp-winrt/intro-to-using-cpp-with-winrt), then you can control its lifetime by enclosing it in a block, or setting it to `nullptr` when you're done with it.

    ```csharp
    using (var outputStream = stream.GetOutputStreamAt(0))
    {
        // We'll add more code here in the next step.
    }
    stream.Dispose(); // Or use the stream variable (see previous code snippet) with a using statement as well.
    ```
    
    ```cppwinrt
    Windows::Storage::Streams::IOutputStream outputStream{ stream.GetOutputStreamAt(0) };
    // The code in step 3 goes here.
    ```
    
    

3.  Now add this code (if you're using C#, within the existing using statement) to write to the output stream by creating a new [DataWriter](/uwp/api/windows.storage.streams.datawriter) object and calling the [DataWriter.WriteString](/uwp/api/windows.storage.streams.datawriter.writestring) method.

    ```csharp
    using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
    {
        dataWriter.WriteString("DataWriter has methods to write to various types, such as DataTimeOffset.");
    }
    ```
    
    ```cppwinrt
    Windows::Storage::Streams::DataWriter dataWriter{ outputStream };
    dataWriter.WriteString(L"DataWriter has methods to write to various types, such as DataTimeOffset.");
    // The code in step 4 goes here.
    ```
    
    

4.  Lastly, add this code (if you're using C#, within the inner using statement) to save the text to your file with [DataWriter.StoreAsync](/uwp/api/windows.storage.streams.datawriter.storeasync) and close the stream with [IOutputStream.FlushAsync](/uwp/api/windows.storage.streams.ioutputstream.flushasync).

    ```csharp
    await dataWriter.StoreAsync();
    await outputStream.FlushAsync();
    ```
    
    ```cppwinrt
    co_await dataWriter.StoreAsync();
    co_await outputStream.FlushAsync();
    ```
    
    

**Best practices for writing to a file**

For additional details and best practice guidance, see [Best practices for writing to files](best-practices-writing-files.md).
    
## Reading from a file

Here's how to read from a file on disk using the [StorageFile](/uwp/api/Windows.Storage.StorageFile) class. The common first step for each of the ways of reading from a file is to get the file with [StorageFolder.GetFileAsync](/uwp/api/windows.storage.storagefolder.getfileasync).

```csharp
Windows.Storage.StorageFolder storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
Windows.Storage.StorageFile sampleFile =
    await storageFolder.GetFileAsync("sample.txt");
```

```cppwinrt
Windows::Storage::StorageFolder storageFolder{ Windows::Storage::ApplicationData::Current().LocalFolder() };
auto sampleFile{ co_await storageFolder.GetFileAsync(L"sample.txt") };
// Process file
```


**Reading text from a file**

Read text from your file by calling the [FileIO.ReadTextAsync](/uwp/api/windows.storage.fileio.readtextasync) method.

```csharp
string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
```

```cppwinrt
Windows::Foundation::IAsyncOperation<winrt::hstring> ExampleCoroutineAsync()
{
    Windows::Storage::StorageFolder storageFolder{ Windows::Storage::ApplicationData::Current().LocalFolder() };
    auto sampleFile{ co_await storageFolder.GetFileAsync(L"sample.txt") };
    co_return co_await Windows::Storage::FileIO::ReadTextAsync(sampleFile);
}
```


**Reading text from a file by using a buffer (2 steps)**

1.  First, call the [FileIO.ReadBufferAsync](/uwp/api/windows.storage.fileio.readbufferasync) method.

    ```csharp
    var buffer = await Windows.Storage.FileIO.ReadBufferAsync(sampleFile);
    ```
    
    ```cppwinrt
    Windows::Storage::StorageFolder storageFolder{ Windows::Storage::ApplicationData::Current().LocalFolder() };
    auto sampleFile{ co_await storageFolder.GetFileAsync(L"sample.txt") };
    Windows::Storage::Streams::IBuffer buffer{ co_await Windows::Storage::FileIO::ReadBufferAsync(sampleFile) };
    // The code in step 2 goes here.
    ```
    
    

2.  Then use a [DataReader](/uwp/api/windows.storage.streams.datareader) object to read first the length of the buffer and then its contents.

    ```csharp
    using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
    {
        string text = dataReader.ReadString(buffer.Length);
    }
    ```
    
    ```cppwinrt
    auto dataReader{ Windows::Storage::Streams::DataReader::FromBuffer(buffer) };
    winrt::hstring bufferText{ dataReader.ReadString(buffer.Length()) };
    ```
    
    

**Reading text from a file by using a stream (4 steps)**

1.  Open a stream for your file by calling the [StorageFile.OpenAsync](/uwp/api/windows.storage.storagefile.openasync) method. It returns a stream of the file's content when the operation completes.

    ```csharp
    var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
    ```
    
    ```cppwinrt
    Windows::Storage::StorageFolder storageFolder{ Windows::Storage::ApplicationData::Current().LocalFolder() };
    auto sampleFile{ co_await storageFolder.GetFileAsync(L"sample.txt") };
    Windows::Storage::Streams::IRandomAccessStream stream{ co_await sampleFile.OpenAsync(Windows::Storage::FileAccessMode::Read) };
    // The code in step 2 goes here.
    ```
    
    

2.  Get the size of the stream to use later.

    ```csharp
    ulong size = stream.Size;
    ```
    
    ```cppwinrt
    uint64_t size{ stream.Size() };
    // The code in step 3 goes here.
    ```
    
    

3.  Get an input stream by calling the [IRandomAccessStream.GetInputStreamAt](/uwp/api/windows.storage.streams.irandomaccessstream.getinputstreamat) method. Put this in a using statement to manage the stream's lifetime. Specify 0 when you call GetInputStreamAt to set the position to the beginning of the stream.

    ```csharp
    using (var inputStream = stream.GetInputStreamAt(0))
    {
        // We'll add more code here in the next step.
    }
    ```
    
    ```cppwinrt
    Windows::Storage::Streams::IInputStream inputStream{ stream.GetInputStreamAt(0) };
    Windows::Storage::Streams::DataReader dataReader{ inputStream };
    // The code in step 4 goes here.
    ```
    
    

4.  Lastly, add this code within the existing using statement to get a [DataReader](/uwp/api/windows.storage.streams.datareader) object on the stream then read the text by calling [DataReader.LoadAsync](/uwp/api/windows.storage.streams.datareader.loadasync) and [DataReader.ReadString](/uwp/api/windows.storage.streams.datareader.readstring).

    ```csharp
    using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
    {
        uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
        string text = dataReader.ReadString(numBytesLoaded);
    }
    ```
    
    ```cppwinrt
    unsigned int cBytesLoaded{ co_await dataReader.LoadAsync(size) };
    winrt::hstring streamText{ dataReader.ReadString(cBytesLoaded) };
    ```
    
    

## See also

- [Best practices for writing to files](best-practices-writing-files.md)