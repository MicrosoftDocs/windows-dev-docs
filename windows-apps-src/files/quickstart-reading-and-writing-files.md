---
author: laurenhughes
ms.assetid: 27914C0A-2A02-473F-BDD5-C931E3943AA0
title: Create, write, and read a file
description: Read and write a file using a StorageFile object.
ms.author: lahugh
ms.date: 07/05/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Create, write, and read a file




**Important APIs**

-   [**StorageFolder class**](https://msdn.microsoft.com/library/windows/apps/br227230)
-   [**StorageFile class**](https://msdn.microsoft.com/library/windows/apps/br227171)
-   [**FileIO class**](https://msdn.microsoft.com/library/windows/apps/hh701440)

Read and write a file using a [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) object.

> [!NOTE]
> Also see the [File access sample](http://go.microsoft.com/fwlink/p/?linkid=619995).

## Prerequisites

-   **Understand async programming for Universal Windows Platform (UWP) apps**

    You can learn how to write asynchronous apps in C# or Visual Basic, see [Call asynchronous APIs in C# or Visual Basic](https://msdn.microsoft.com/library/windows/apps/mt187337). To learn how to write asynchronous apps in C++, see [Asynchronous programming in C++](https://msdn.microsoft.com/library/windows/apps/mt187334).

-   **Know how to get the file that you want to read from, write to, or both**

    You can learn how to get a file by using a file picker in [Open files and folders with a picker](quickstart-using-file-and-folder-pickers.md).

## Creating a file

Here's how to create a file in the app's local folder. If it already exists, we replace it.

> [!div class="tabbedCodeSnippets"]
```cs  
// Create sample file; replace if exists.
Windows.Storage.StorageFolder storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
Windows.Storage.StorageFile sampleFile =
    await storageFolder.CreateFileAsync("sample.txt",
        Windows.Storage.CreationCollisionOption.ReplaceExisting);
```
```cpp  
// Create a sample file; replace if exists.
StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
concurrency::create_task(storageFolder->CreateFileAsync("sample.txt", CreationCollisionOption::ReplaceExisting));
```
```vb  
' Create sample file; replace if exists.
Dim storageFolder As StorageFolder = Windows.Storage.ApplicationData.Current.LocalFolder
Dim sampleFile As StorageFile = Await storageFolder.CreateFileAsync("sample.txt", CreationCollisionOption.ReplaceExisting)
```

## Writing to a file

Here's how to write to a writable file on disk using the [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) class. The common first step for each of the ways of writing to a file (unless you're writing to the file immediately after creating it) is to get the file with [**StorageFolder.GetFileAsync**](https://msdn.microsoft.com/library/windows/apps/br227272).

> [!div class="tabbedCodeSnippets"]
```cs  
Windows.Storage.StorageFolder storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
Windows.Storage.StorageFile sampleFile =
    await storageFolder.GetFileAsync("sample.txt");
```
```cpp  
StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
create_task(storageFolder->GetFileAsync("sample.txt")).then([](StorageFile^ sampleFile) 
{
    // Process file
});
```
```vb  
Dim storageFolder As StorageFolder = Windows.Storage.ApplicationData.Current.LocalFolder
Dim sampleFile As StorageFile = Await storageFolder.GetFileAsync("sample.txt")
```

**Writing text to a file**

Write text to your file by calling the [**WriteTextAsync**](https://msdn.microsoft.com/library/windows/apps/hh701505) method of the [**FileIO**](https://msdn.microsoft.com/library/windows/apps/hh701440) class.

> [!div class="tabbedCodeSnippets"]
```cs  
await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "Swift as a shadow");
```
```cpp 
StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
create_task(storageFolder->GetFileAsync("sample.txt")).then([](StorageFile^ sampleFile) 
{
    //Write text to a file
    create_task(FileIO::WriteTextAsync(sampleFile, "Swift as a shadow"));
});
```
```vb  
Await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "Swift as a shadow")
```

**Writing bytes to a file by using a buffer (2 steps)**

1.  First, call [**ConvertStringToBinary**](https://msdn.microsoft.com/library/windows/apps/br241385) to get a buffer of the bytes (based on an arbitrary string) that you want to write to your file.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    var buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(
            "What fools these mortals be", Windows.Security.Cryptography.BinaryStringEncoding.Utf8);
    ```
    ```cpp  
    StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
    create_task(storageFolder->GetFileAsync("sample.txt")).then([](StorageFile^ sampleFile)
    {
        // Create the buffer
        IBuffer^ buffer = CryptographicBuffer::ConvertStringToBinary
        ("What fools these mortals be", BinaryStringEncoding::Utf8);
    });
    ```
    ```vb  
    Dim buffer = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(
                        "What fools these mortals be",
                        Windows.Security.Cryptography.BinaryStringEncoding.Utf8)
    ```

2.  Then write the bytes from your buffer to your file by calling the [**WriteBufferAsync**](https://msdn.microsoft.com/library/windows/apps/hh701490) method of the [**FileIO**](https://msdn.microsoft.com/library/windows/apps/hh701440) class.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    await Windows.Storage.FileIO.WriteBufferAsync(sampleFile, buffer);
    ```
    ```cpp  
    StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
    create_task(storageFolder->GetFileAsync("sample.txt")).then([](StorageFile^ sampleFile)
    {
        // Create the buffer
        IBuffer^ buffer = CryptographicBuffer::ConvertStringToBinary
        ("What fools these mortals be", BinaryStringEncoding::Utf8);      
        // Write bytes to a file using a buffer
        create_task(FileIO::WriteBufferAsync(sampleFile, buffer));
    });
    ```
    ```vb  
    Await Windows.Storage.FileIO.WriteBufferAsync(sampleFile, buffer)
    ```

**Writing text to a file by using a stream (4 steps)**

1.  First, open the file by calling the [**StorageFile.OpenAsync**](https://msdn.microsoft.com/library/windows/apps/dn889851) method. It returns a stream of the file's content when the open operation completes.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
    ```
    ```cpp  
    StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
    create_task(storageFolder->GetFileAsync("sample.txt")).then([](StorageFile^ sampleFile)
    {
        create_task(sampleFile->OpenAsync(FileAccessMode::ReadWrite)).then([sampleFile](IRandomAccessStream^ stream)
        {
            // Process stream
        });
    });
    ```
    ```vb  
    Dim stream = Await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite)
    ```

2.  Next, get an output stream by calling the [**GetOutputStreamAt**](https://msdn.microsoft.com/library/windows/apps/br241738) method from the `stream`. Put this in a **using** statement to manage the output stream's lifetime.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    using (var outputStream = stream.GetOutputStreamAt(0))
    {
        // We'll add more code here in the next step.
    }
    stream.Dispose(); // Or use the stream variable (see previous code snippet) with a using statement as well.
    ```
    ```cpp 
    // Add to "Process stream" in part 1
    IOutputStream^ outputStream = stream->GetOutputStreamAt(0);
    ```
    ```vb 
    Using outputStream = stream.GetOutputStreamAt(0)
    ' We'll add more code here in the next step.
    End Using
    ```

3.  Now add this code within the existing **using** statement to write to the output stream by creating a new [**DataWriter**](https://msdn.microsoft.com/library/windows/apps/br208154) object and calling the [**DataWriter.WriteString**](https://msdn.microsoft.com/library/windows/apps/br241642) method.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
    {
        dataWriter.WriteString("DataWriter has methods to write to various types, such as DataTimeOffset.");
    }
    ```
    ```cpp  
    // Added after code from part 2
    DataWriter^ dataWriter = ref new DataWriter(outputStream);
    dataWriter->WriteString("DataWriter has methods to write to various types, such as DataTimeOffset.");
    ```
    ```vb  
    Dim dataWriter As New DataWriter(outputStream)
    dataWriter.WriteString("DataWriter has methods to write to various types, such as DataTimeOffset.")
    ```

4.  Lastly, add this code (within the inner **using** statement) to save the text to your file with [**StoreAsync**](https://msdn.microsoft.com/library/windows/apps/br208171) and close the stream with [**FlushAsync**](https://msdn.microsoft.com/library/windows/apps/br241729).

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    await dataWriter.StoreAsync();
        await outputStream.FlushAsync();
    ```
    ```cpp   
    // Added after code from part 3
    dataWriter->StoreAsync();
    outputStream->FlushAsync();
    ```
    ```vb  
    Await dataWriter.StoreAsync()
        Await outputStream.FlushAsync()
    ```

## Reading from a file

Here's how to read from a file on disk using the [**StorageFile**](https://msdn.microsoft.com/library/windows/apps/br227171) class. The common first step for each of the ways of reading from a file is to get the file with [**StorageFolder.GetFileAsync**](https://msdn.microsoft.com/library/windows/apps/br227272).

> [!div class="tabbedCodeSnippets"]
```cs  
Windows.Storage.StorageFolder storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
Windows.Storage.StorageFile sampleFile =
    await storageFolder.GetFileAsync("sample.txt");
```
```cpp  
StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
create_task(storageFolder->GetFileAsync("sample.txt")).then([](StorageFile^ sampleFile)
{
    // Process file
});
```
```vb  
Dim storageFolder As StorageFolder = Windows.Storage.ApplicationData.Current.LocalFolder
Dim sampleFile As StorageFile = Await storageFolder.GetFileAsync("sample.txt")
```

**Reading text from a file**

Read text from your file by calling the [**ReadTextAsync**](https://msdn.microsoft.com/library/windows/apps/hh701482) method of the [**FileIO**](https://msdn.microsoft.com/library/windows/apps/hh701440) class.

> [!div class="tabbedCodeSnippets"]
```cs  
string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
```
```cpp  
StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
create_task(storageFolder->GetFileAsync("sample.txt")).then([](StorageFile^ sampleFile)
{
    return FileIO::ReadTextAsync(sampleFile);
});
```
```vb  
Dim text As String = Await Windows.Storage.FileIO.ReadTextAsync(sampleFile)
```

**Reading text from a file by using a buffer (2 steps)**

1.  First, call the [**ReadBufferAsync**](https://msdn.microsoft.com/library/windows/apps/hh701468) method of the [**FileIO**](https://msdn.microsoft.com/library/windows/apps/hh701440) class.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    var buffer = await Windows.Storage.FileIO.ReadBufferAsync(sampleFile);
    ```

    ```cpp  
    StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
    create_task(storageFolder->GetFileAsync("sample.txt")).then([](StorageFile^ sampleFile)
    {
        return FileIO::ReadBufferAsync(sampleFile);

    }).then([](Streams::IBuffer^ buffer)
    {
        // Process buffer
    });
    ```

    ```vb  
    Dim buffer = Await Windows.Storage.FileIO.ReadBufferAsync(sampleFile)
    ```

2.  Then use a [**DataReader**](https://msdn.microsoft.com/library/windows/apps/br208119) object to read first the length of the buffer and then its contents.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
    {
        string text = dataReader.ReadString(buffer.Length);
    }
    ```
    ```cpp  
    // Add to "Process buffer" section from part 1
    auto dataReader = DataReader::FromBuffer(buffer);
    String^ bufferText = dataReader->ReadString(buffer->Length);
    ```
    ```vb  
    Dim dataReader As DataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer)
    Dim text As String = dataReader.ReadString(buffer.Length)
    ```

**Reading text from a file by using a stream (4 steps)**

1.  Open a stream for your file by calling the [**StorageFile.OpenAsync**](https://msdn.microsoft.com/library/windows/apps/dn889851) method. It returns a stream of the file's content when the operation completes.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
    ```
    ```cpp  
    StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
    create_task(storageFolder->GetFileAsync("sample.txt")).then([](StorageFile^ sampleFile)
    {
        create_task(sampleFile->OpenAsync(FileAccessMode::Read)).then([sampleFile](IRandomAccessStream^ stream)
        {
            // Process stream
        });
    });
    ```
    ```vb  
    Dim stream = Await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.Read)
    ```

2.  Get the size of the stream to use later.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    ulong size = stream.Size;
    ```
    ```cpp  
    // Add to "Process stream" from part 1
    UINT64 size = stream->Size;
    ```
    ```vb  
    Dim size = stream.Size
    ```

3.  Get an input stream by calling the [**GetInputStreamAt**](https://msdn.microsoft.com/library/windows/apps/br241737) method. Put this in a **using** statement to manage the stream's lifetime. Specify 0 when you call **GetInputStreamAt** to set the position to the beginning of the stream.

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    using (var inputStream = stream.GetInputStreamAt(0))
    {
        // We'll add more code here in the next step.
    }
    ```
    ```cpp  
    // Add after code from part 2
    IInputStream^ inputStream = stream->GetInputStreamAt(0);
    auto dataReader = ref new DataReader(inputStream);
    ```
    ```vb  
    Using inputStream = stream.GetInputStreamAt(0)
        ' We'll add more code here in the next step.
    End Using
    ```

4.  Lastly, add this code within the existing **using** statement to get a [**DataReader**](https://msdn.microsoft.com/library/windows/apps/br208119) object on the stream then read the text by calling [**DataReader.LoadAsync**](https://msdn.microsoft.com/library/windows/apps/br208135) and [**DataReader.ReadString**](https://msdn.microsoft.com/library/windows/apps/br208147).

    > [!div class="tabbedCodeSnippets"]
    ```cs  
    using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
    {
        uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
        string text = dataReader.ReadString(numBytesLoaded);
    }
    ```
    ```cpp 
    // Add after code from part 3
    create_task(dataReader->LoadAsync(size)).then([sampleFile, dataReader](unsigned int numBytesLoaded)
    {
        String^ streamText = dataReader->ReadString(numBytesLoaded);
    });
    ```
    ```vb  
    Dim dataReader As New DataReader(inputStream)
    Dim numBytesLoaded As UInteger = Await dataReader.LoadAsync(CUInt(size))
    Dim text As String = dataReader.ReadString(numBytesLoaded)
    ```
