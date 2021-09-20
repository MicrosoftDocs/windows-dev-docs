---
title: Best practices for writing to files
description: Learn best practices for using various file writing methods of the FileIO and PathIO classes.
ms.date: 02/06/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Best practices for writing to files

**Important APIs**

* [**FileIO class**](/uwp/api/Windows.Storage.FileIO)
* [**PathIO class**](/uwp/api/windows.storage.pathio)

Developers sometimes run into a set of common problems when using the **Write** methods of the [**FileIO**](/uwp/api/Windows.Storage.FileIO) and [**PathIO**](/uwp/api/windows.storage.pathio) classes to perform file system I/O operations. For example, common problems include:

* A file is partially written.
* The app receives an exception when calling one of the methods.
* The operations leave behind .TMP files  with a file name similar to the target file name.

The **Write** methods of the [**FileIO**](/uwp/api/Windows.Storage.FileIO) and [**PathIO**](/uwp/api/windows.storage.pathio) classes include the following:

* **WriteBufferAsync**
* **WriteBytesAsync**
* **WriteLinesAsync**
* **WriteTextAsync**

 This article provides details about how these methods work so developers understand better when and how to use them. This article provides guidelines and does not attempt to provide a solution for all possible file I/O problems. 

> [!NOTE]
> This article focuses on the **FileIO** methods in examples and discussions. However, the **PathIO** methods follow a similar pattern and most of the guidance in this article applies to those methods too. 

## Convenience vs. control

A [**StorageFile**](/uwp/api/windows.storage.storagefile) object is not a file handle like the native Win32 programming model. Instead, a [**StorageFile**](/uwp/api/windows.storage.storagefile) is a representation of a file with methods to manipulate its contents.

Understanding this concept is useful when performing I/O with a **StorageFile**. For example, the [Writing to a file](quickstart-reading-and-writing-files.md#writing-to-a-file) section presents three ways to write to a file:

* Using the [**FileIO.WriteTextAsync**](/uwp/api/windows.storage.fileio.writetextasync) method.
* By creating a buffer and then calling the [**FileIO.WriteBufferAsync**](/uwp/api/windows.storage.fileio.writebufferasync) method.
* The four-step model using a stream:
  1. [Open](/uwp/api/windows.storage.storagefile.openasync) the file to get a stream.
  2. [Get](/uwp/api/windows.storage.streams.irandomaccessstream.getoutputstreamat) an output stream.
  3. Create a [**DataWriter**](/uwp/api/windows.storage.streams.datawriter) object and call the corresponding **Write** method.
  4. [Commit](/uwp/api/windows.storage.streams.datawriter.storeasync) the data in the data writer and [flush](/uwp/api/windows.storage.streams.ioutputstream.flushasync) the output stream.

The first two scenarios are the ones most commonly used by apps. Writing to the file in a single operation is easier to code and maintain, and it also removes the responsibility of the app from dealing with many of the complexities of file I/O. However, this convenience comes at a cost: the loss of control over the entire operation and the ability to catch errors at specific points.

## The transactional model

The **Write** methods of the [**FileIO**](/uwp/api/Windows.Storage.FileIO) and [**PathIO**](/uwp/api/windows.storage.pathio) classes wrap the steps on the third write model described above, with an added layer. This layer is encapsulated in a storage transaction.

To protect the integrity of the original file in case something goes wrong while writing the data, the **Write** methods use a transactional model by opening the file using [**OpenTransactedWriteAsync**](/uwp/api/windows.storage.storagefile.opentransactedwriteasync). This process creates a [**StorageStreamTransaction**](/uwp/api/windows.storage.storagestreamtransaction) object. After this transaction object is created, the APIs write the data following a similar fashion to the [File Access](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/FileAccess) sample or the code example in the [**StorageStreamTransaction**](/uwp/api/windows.storage.storagestreamtransaction) article.

The following diagram illustrates the underlying tasks performed by the the **WriteTextAsync** method in a successful write operation. This illustration provides a simplified view of the operation. For example, it skips steps such as text encoding and async completion on different threads.

![UWP API call sequence diagram for writing to a file](images/file-write-call-sequence.svg)

The advantages of using the **Write** methods of the [**FileIO**](/uwp/api/Windows.Storage.FileIO) and [**PathIO**](/uwp/api/windows.storage.pathio) classes instead of the more complex four-step model using a stream are:

* One API call to handle all the intermediate steps, including errors.
* The original file is kept if something goes wrong.
* The system state will try to be kept as clean as possible.

However, with so many possible intermediate points of failure, there’s an increased chance of failure. When an error occurs it may be difficult to understand where the process failed. The following sections present some of the failures you might encounter when using the **Write** methods and provide possible solutions.

## Common error codes for Write methods of the FileIO and PathIO classes

This table presents common error codes that app developers encounter when using the **Write** methods. The steps in the table correspond to steps in the previous diagram.

|  Error name (value)  |  Steps  |  Causes  |  Solutions  |
|----------------------|---------|----------|-------------|
|  ERROR_ACCESS_DENIED (0X80070005)  |  5  |  The original file might be marked for deletion, possibly from a previous operation.  |  Retry the operation.</br>Ensure access to the file is synchronized.  |
|  ERROR_SHARING_VIOLATION (0x80070020)  |  5  |  The original file is opened by another exclusive write.   |  Retry the operation.</br>Ensure access to the file is synchronized.  |
|  ERROR_UNABLE_TO_REMOVE_REPLACED (0x80070497)  |  19-20  |  The original file (file.txt) could not be replaced because it is in use. Another process or operation gained access to the file before it could be replaced.  |  Retry the operation.</br>Ensure access to the file is synchronized.  |
|  ERROR_DISK_FULL (0x80070070)  |  7, 14, 16, 20  |  The transacted model creates an extra file, and this consumes extra storage.  |    |
|  ERROR_OUTOFMEMORY (0x8007000E)  |  14, 16  |  This can happen due to multiple outstanding I/O operations or large file sizes.  |  A more granular approach by controlling the stream might resolve the error.  |
|  E_FAIL (0x80004005) |  Any  |  Miscellaneous  |  Retry the operation. If it still fails, it might be a platform error and the app should terminate because it's in an inconsistent state. |

## Other considerations for file states that might lead to errors

Apart from errors returned by the **Write** methods, here are some guidelines on what an app can expect when writing to a file.

### Data was written to the file if and only if operation completed

Your app should not make any assumption about data in the file while a write operation is in progress. Trying to access the file before an operation completes might lead to inconsistent data. Your app should be responsible of tracking outstanding I/Os.

### Readers

If the file that being written to is also being used by a polite reader (that is, opened with [**FileAccessMode.Read**](/uwp/api/Windows.Storage.FileAccessMode), subsequent reads will fail with an error ERROR_OPLOCK_HANDLE_CLOSED (0x80070323). Sometimes apps retry opening the file for read again while the **Write** operation is ongoing. This might result in a race condition on which the **Write** ultimately fails when trying to overwrite the original file because it cannot be replaced.

### Files from KnownFolders

Your app might not be the only app that is trying to access a file that resides on any of the [**KnownFolders**](/uwp/api/Windows.Storage.KnownFolders). There’s no guarantee that if the operation is successful, the contents an app wrote to the file will remain constant the next time it tries to read the file. Also, sharing or access denied errors become more common under this scenario.

### Conflicting I/O

The chances of concurrency errors can be lowered if our app uses the **Write** methods for files in its local data, but some caution is still required. If multiple **Write** operations are being sent concurrently to the file, there’s no guarantee about what data ends up in the file. To mitigate this, we recommend that your app serializes **Write** operations to the file.

### ~TMP files

Occasionally, if the operation is forcefully cancelled (for example, if the app was suspended or terminated by the OS), the transaction is not committed or closed appropriately. This can leave behind files with a (.~TMP) extension. Consider deleting these temporary files (if they exist in the app's local data) when handling the app activation.

## Considerations based on file types

Some errors can become more prevalent depending on the type of files, the frequency on which they’re accessed, and their file size. Generally, there are three categories of files your app can access:

* Files created and edited by the user in your app's local data folder. These are created and edited only while using your app, and they exist only within the app.
* App metadata. Your app uses these files to keep track of its own state.
* Other files in locations of the file system where your app has declared capabilities to access. These are most commonly located in one of the [**KnownFolders**](/uwp/api/Windows.Storage.KnownFolders).

Your app has full control on the first two categories of files, because they’re part of your app's package files and are accessed by your app exclusively. For files in the last category, your app must be aware that other apps and OS services may be accessing the files concurrently.

Depending on the app, access to the files can vary on frequency:

* Very low. These are usually files that are opened once when the app launches and are saved when the app is suspended.
* Low. These are files that the user is specifically taking an action on (such as save or load).
* Medium or high. These are files in which the app must constantly update data (for example, autosave features or constant metadata tracking).

For file size, consider the performance data in the following chart for the **WriteBytesAsync** method. This chart compares the time to complete an operation vs file size, over an average performance of 10000 operations per file size in a controlled environment.

![WriteBytesAsync performance](images/writebytesasync-performance.png)

The time values on the y-axis are omitted intentionally from this chart because different hardware and configurations will yield different absolute time values. However, we have consistently observed these trends in our tests:

* For very small files (<= 1 MB): The time to complete the operations is consistently fast.
* For larger files (> 1 MB): The time to complete the operations starts to increase exponentially.

## I/O during app suspension

Your app must designed to handle suspension if you want to keep state information or metadata for use in later sessions. For background information about app suspension, see [App lifecycle](../launch-resume/app-lifecycle.md) and [this blog post](https://blogs.windows.com/buildingapps/2016/04/28/the-lifecycle-of-a-uwp-app/#qLwdmV5zfkAPMEco.97).

Unless the OS grants extended execution to your app, when your app is suspended it has 5 seconds to release all its resources and save its data. For the best reliability and user experience, always assume the time you have to handle suspension tasks is limited. Keep in mind the following guidelines during the 5 second time period for handling suspension tasks:

* Try to keep I/O to a minimum to avoid race conditions caused by flushing and release operations.
* Avoid writing files that require hundreds of milliseconds or more to write.
* If your app uses the **Write** methods, keep in mind all the intermediate steps that these methods require.

If your app operates on a small amount of state data during suspension, in most cases you can use the **Write** methods to flush the data. However, if your app uses a large amount of state data, consider using streams to directly store your data. This can help reduce the delay introduced by the transactional model of the **Write** methods. 

For an example, see the [BasicSuspension](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicSuspension) sample.

## Other examples and resources

Here are several examples and other resources for specific scenarios.

### Code example for retrying file I/O example

The following is a pseudo-code example on how to retry a write (C#), assuming the write is to be done after the user picks a file for saving:

```csharp
Windows.Storage.Pickers.FileSavePicker savePicker = new Windows.Storage.Pickers.FileSavePicker();
savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();

Int32 retryAttempts = 5;

const Int32 ERROR_ACCESS_DENIED = unchecked((Int32)0x80070005);
const Int32 ERROR_SHARING_VIOLATION = unchecked((Int32)0x80070020);

if (file != null)
{
    // Application now has read/write access to the picked file.
    while (retryAttempts > 0)
    {
        try
        {
            retryAttempts--;
            await Windows.Storage.FileIO.WriteTextAsync(file, "Text to write to file");
            break;
        }
        catch (Exception ex) when ((ex.HResult == ERROR_ACCESS_DENIED) ||
                                   (ex.HResult == ERROR_SHARING_VIOLATION))
        {
            // This might be recovered by retrying, otherwise let the exception be raised.
            // The app can decide to wait before retrying.
        }
    }
}
else
{
    // The operation was cancelled in the picker dialog.
}
```

### Synchronize access to the file

The [Parallel Programming with .NET blog](https://devblogs.microsoft.com/pfxteam/) is a great resource for guidance about parallel programming. In particular, the [post about AsyncReaderWriterLock](https://devblogs.microsoft.com/pfxteam/building-async-coordination-primitives-part-7-asyncreaderwriterlock/) describes how to maintain exclusive access to a file for writes while allowing concurrent read access. Keep in mind that serializing I/O will impact performance.

## See also

* [Create, write, and read a file](quickstart-reading-and-writing-files.md)