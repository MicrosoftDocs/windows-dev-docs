---
ms.assetid: 40122343-1FE3-4160-BABE-6A2DD9AF1E8E
title: Optimize file access
description: Create Universal Windows Platform (UWP) apps that access the file system efficiently, avoiding performance issues due to disk latency and memory/CPU cycles.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Optimize file access


Create Universal Windows Platform (UWP) apps that access the file system efficiently, avoiding performance issues due to disk latency and memory/CPU cycles.

When you want to access a large collection of files and you want to access property values other than the typical Name, FileType, and Path properties, access them by creating [**QueryOptions**](/uwp/api/Windows.Storage.Search.QueryOptions) and calling [**SetPropertyPrefetch**](/uwp/api/windows.storage.search.queryoptions.setpropertyprefetch). The **SetPropertyPrefetch** method can dramatically improve the performance of apps that display a collection of items obtained from the file system, such as a collection of images. The next set of examples shows a few ways to access multiple files.

The first example uses [**Windows.Storage.StorageFolder.GetFilesAsync**](/uwp/api/windows.storage.storagefolder.getfilesasync) to retrieve the name info for a set of files. This approach provides good performance, because the example accesses only the name property.

> [!div class="tabbedCodeSnippets"]
> ```csharp
> StorageFolder library = Windows.Storage.KnownFolders.PicturesLibrary;
> IReadOnlyList<StorageFile> files = await library.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.OrderByDate);
> 
> for (int i = 0; i < files.Count; i++)
> {
>     // do something with the name of each file
>     string fileName = files[i].Name;
> }
> ```
> ```vb
> Dim library As StorageFolder = Windows.Storage.KnownFolders.PicturesLibrary
> Dim files As IReadOnlyList(Of StorageFile) =
>     Await library.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.OrderByDate)
> 
> For i As Integer = 0 To files.Count - 1
>     ' do something with the name of each file
>     Dim fileName As String = files(i).Name
> Next i
> ```

The second example uses [**Windows.Storage.StorageFolder.GetFilesAsync**](/uwp/api/windows.storage.storagefolder.getfilesasync) and then retrieves the image properties for each file. This approach provides poor performance.

> [!div class="tabbedCodeSnippets"]
> ```csharp
> StorageFolder library = Windows.Storage.KnownFolders.PicturesLibrary;
> IReadOnlyList<StorageFile> files = await library.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.OrderByDate);
> for (int i = 0; i < files.Count; i++)
> {
>     ImageProperties imgProps = await files[i].Properties.GetImagePropertiesAsync();
> 
>     // do something with the date the image was taken
>     DateTimeOffset date = imgProps.DateTaken;
> }
> ```
> ```vb
> Dim library As StorageFolder = Windows.Storage.KnownFolders.PicturesLibrary
> Dim files As IReadOnlyList(Of StorageFile) = Await library.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.OrderByDate)
> For i As Integer = 0 To files.Count - 1
>     Dim imgProps As ImageProperties =
>         Await files(i).Properties.GetImagePropertiesAsync()
> 
>     ' do something with the date the image was taken
>     Dim dateTaken As DateTimeOffset = imgProps.DateTaken
> Next i
> ```

The third example uses [**QueryOptions**](/uwp/api/Windows.Storage.Search.QueryOptions) to get info about a set of files. This approach provides much better performance than the previous example.

> [!div class="tabbedCodeSnippets"]
> ```csharp
> // Set QueryOptions to prefetch our specific properties
> var queryOptions = new Windows.Storage.Search.QueryOptions(CommonFileQuery.OrderByDate, null);
> queryOptions.SetThumbnailPrefetch(ThumbnailMode.PicturesView, 100,
>         ThumbnailOptions.ReturnOnlyIfCached);
> queryOptions.SetPropertyPrefetch(PropertyPrefetchOptions.ImageProperties, 
>        new string[] {"System.Size"});
> 
> StorageFileQueryResult queryResults = KnownFolders.PicturesLibrary.CreateFileQueryWithOptions(queryOptions);
> IReadOnlyList<StorageFile> files = await queryResults.GetFilesAsync();
> 
> foreach (var file in files)
> {
>     ImageProperties imageProperties = await file.Properties.GetImagePropertiesAsync();
> 
>     // Do something with the date the image was taken.
>     DateTimeOffset dateTaken = imageProperties.DateTaken;
> 
>     // Performance gains increase with the number of properties that are accessed.
>     IDictionary<String, object> propertyResults =
>         await file.Properties.RetrievePropertiesAsync(
>               new string[] {"System.Size" });
> 
>     // Get/Set extra properties here
>     var systemSize = propertyResults["System.Size"];
> }
> ```
> ```vb
> ' Set QueryOptions to prefetch our specific properties
> Dim queryOptions = New Windows.Storage.Search.QueryOptions(CommonFileQuery.OrderByDate, Nothing)
> queryOptions.SetThumbnailPrefetch(ThumbnailMode.PicturesView,
>             100, Windows.Storage.FileProperties.ThumbnailOptions.ReturnOnlyIfCached)
> queryOptions.SetPropertyPrefetch(PropertyPrefetchOptions.ImageProperties,
>                                  New String() {"System.Size"})
> 
> Dim queryResults As StorageFileQueryResult = KnownFolders.PicturesLibrary.CreateFileQueryWithOptions(queryOptions)
> Dim files As IReadOnlyList(Of StorageFile) = Await queryResults.GetFilesAsync()
> 
> 
> For Each file In files
>     Dim imageProperties As ImageProperties = Await file.Properties.GetImagePropertiesAsync()
> 
>     ' Do something with the date the image was taken.
>     Dim dateTaken As DateTimeOffset = imageProperties.DateTaken
> 
>     ' Performance gains increase with the number of properties that are accessed.
>     Dim propertyResults As IDictionary(Of String, Object) =
>         Await file.Properties.RetrievePropertiesAsync(New String() {"System.Size"})
> 
>     ' Get/Set extra properties here
>     Dim systemSize = propertyResults("System.Size")
> 
> Next file
> ```
If you're performing multiple operations on Windows.Storage objects such as `Windows.Storage.ApplicationData.Current.LocalFolder`, create a local variable to reference that storage source so that you don't recreate intermediate objects each time you access it.

## Stream performance in C# and Visual Basic

### Buffering between UWP and .NET streams

There are many scenarios when you might want to convert a UWP stream (such as a [**Windows.Storage.Streams.IInputStream**](/uwp/api/Windows.Storage.Streams.IInputStream) or [**IOutputStream**](/uwp/api/Windows.Storage.Streams.IOutputStream)) to a .NET stream ([**System.IO.Stream**](/dotnet/api/system.io.stream?view=dotnet-uwp-10.0&preserve-view=true)). For example, this is useful when you are writing a UWP app and want to use existing .NET code that works on streams with the UWP file system. In order to enable this, .NET APIs for UWP apps provides extension methods that allow you to convert between .NET and UWP stream types. For more info, see [**WindowsRuntimeStreamExtensions**](/dotnet/api/system.io.windowsruntimestreamextensions?view=dotnet-uwp-10.0&preserve-view=true).

When you convert a UWP stream to a .NET stream, you effectively create an adapter for the underlying UWP stream. Under some circumstances, there is a runtime cost associated with invoking methods on UWP streams. This may affect the speed of your app, especially in scenarios where you perform many small, frequent read or write operations.

In order to speed up apps, the UWP stream adapters contain a data buffer. The following code sample demonstrates small consecutive reads using a UWP stream adapter with a default buffer size.

> [!div class="tabbedCodeSnippets"]
> ```csharp
> StorageFile file = await Windows.Storage.ApplicationData.Current
>     .LocalFolder.GetFileAsync("example.txt");
> Windows.Storage.Streams.IInputStream windowsRuntimeStream = 
>     await file.OpenReadAsync();
> 
> byte[] destinationArray = new byte[8];
> 
> // Create an adapter with the default buffer size.
> using (var managedStream = windowsRuntimeStream.AsStreamForRead())
> {
> 
>     // Read 8 bytes into destinationArray.
>     // A larger block is actually read from the underlying 
>     // windowsRuntimeStream and buffered within the adapter.
>     await managedStream.ReadAsync(destinationArray, 0, 8);
> 
>     // Read 8 more bytes into destinationArray.
>     // This call may complete much faster than the first call
>     // because the data is buffered and no call to the 
>     // underlying windowsRuntimeStream needs to be made.
>     await managedStream.ReadAsync(destinationArray, 0, 8);
> }
> ```
> ```vb
> Dim file As StorageFile = Await Windows.Storage.ApplicationData.Current -
> .LocalFolder.GetFileAsync("example.txt")
> Dim windowsRuntimeStream As Windows.Storage.Streams.IInputStream =
>     Await file.OpenReadAsync()
> 
> Dim destinationArray() As Byte = New Byte(8) {}
> 
> ' Create an adapter with the default buffer size.
> Dim managedStream As Stream = windowsRuntimeStream.AsStreamForRead()
> Using (managedStream)
> 
>     ' Read 8 bytes into destinationArray.
>     ' A larger block is actually read from the underlying 
>     ' windowsRuntimeStream and buffered within the adapter.
>     Await managedStream.ReadAsync(destinationArray, 0, 8)
> 
>     ' Read 8 more bytes into destinationArray.
>     ' This call may complete much faster than the first call
>     ' because the data is buffered and no call to the 
>     ' underlying windowsRuntimeStream needs to be made.
>     Await managedStream.ReadAsync(destinationArray, 0, 8)
> 
> End Using
> ```

This default buffering behavior is desirable in most scenarios where you convert a UWP stream to a .NET stream. However, in some scenarios you may want to tweak the buffering behavior in order to increase performance.

### Working with large data sets

When reading or writing larger sets of data you may be able to increase your read or write throughput by providing a large buffer size to the [**AsStreamForRead**](/dotnet/api/system.io.windowsruntimestreamextensions.asstreamforread?view=dotnet-uwp-10.0&preserve-view=true), [**AsStreamForWrite**](/dotnet/api/system.io.windowsruntimestreamextensions.asstreamforwrite?view=dotnet-uwp-10.0&preserve-view=true), and [**AsStream**](/dotnet/api/system.io.windowsruntimestreamextensions.asstream?view=dotnet-uwp-10.0&preserve-view=true) extension methods. This gives the stream adapter a larger internal buffer size. For instance, when passing a stream that comes from a large file to an XML parser, the parser can make many sequential small reads from the stream. A large buffer can reduce the number of calls to the underlying UWP stream and boost performance.

> **Note**   You should be careful when setting a buffer size that is larger than approximately 80 KB, as this may cause fragmentation on the garbage collector heap (see [Improve garbage collection performance](improve-garbage-collection-performance.md)). The following code example creates a managed stream adapter with an 81,920 byte buffer.

> [!div class="tabbedCodeSnippets"]
```csharp
// Create a stream adapter with an 80 KB buffer.
Stream managedStream = nativeStream.AsStreamForRead(bufferSize: 81920);
```
```vb
' Create a stream adapter with an 80 KB buffer.
Dim managedStream As Stream = nativeStream.AsStreamForRead(bufferSize:=81920)
```

The [**Stream.CopyTo**](/dotnet/api/system.io.stream.copyto?view=dotnet-uwp-10.0&preserve-view=true) and [**CopyToAsync**](/dotnet/api/system.io.stream.copytoasync?view=dotnet-uwp-10.0&preserve-view=true) methods also allocate a local buffer for copying between streams. As with the [**AsStreamForRead**](/dotnet/api/system.io.windowsruntimestreamextensions.asstreamforread?view=dotnet-uwp-10.0&preserve-view=true) extension method, you may be able to get better performance for large stream copies by overriding the default buffer size. The following code example demonstrates changing the default buffer size of a **CopyToAsync** call.

> [!div class="tabbedCodeSnippets"]
> ```csharp
> MemoryStream destination = new MemoryStream();
> // copies the buffer into memory using the default copy buffer
> await managedStream.CopyToAsync(destination);
> 
> // Copy the buffer into memory using a 1 MB copy buffer.
> await managedStream.CopyToAsync(destination, bufferSize: 1024 * 1024);
> ```
> ```vb
> Dim destination As MemoryStream = New MemoryStream()
> ' copies the buffer into memory using the default copy buffer
> Await managedStream.CopyToAsync(destination)
> 
> ' Copy the buffer into memory using a 1 MB copy buffer.
> Await managedStream.CopyToAsync(destination, bufferSize:=1024 * 1024)
> ```

This example uses a buffer size of 1 MB, which is greater than the 80 KB previously recommended. Using such a large buffer can improve throughput of the copy operation for very large data sets (that is, several hundred megabytes). However, this buffer is allocated on the large object heap and could potentially degrade garbage collection performance. You should only use large buffer sizes if it will noticeably improve the performance of your app.

When you are working with a large number of streams simultaneously, you might want to reduce or eliminate the memory overhead of the buffer. You can specify a smaller buffer, or set the *bufferSize* parameter to 0 to turn off buffering entirely for that stream adapter. You can still achieve good throughput performance without buffering if you perform large reads and writes to the managed stream.

### Performing latency-sensitive operations

You might also want to avoid buffering if you want low-latency reads and writes and do not want to read in large blocks out of the underlying UWP stream. For example, you might want low-latency reads and writes if you are using the stream for network communications.

In a chat app you might use a stream over a network interface to send messages back and forth. In this case you want to send messages as soon as they are ready and not wait for the buffer to fill up. If you set the buffer size to 0 when calling the [**AsStreamForRead**](/dotnet/api/system.io.windowsruntimestreamextensions.asstreamforread?view=dotnet-uwp-10.0&preserve-view=true), [**AsStreamForWrite**](/dotnet/api/system.io.windowsruntimestreamextensions.asstreamforwrite?view=dotnet-uwp-10.0&preserve-view=true), and [**AsStream**](/dotnet/api/system.io.windowsruntimestreamextensions.asstream?view=dotnet-uwp-10.0&preserve-view=true) extension methods, then the resulting adapter will not allocate a buffer, and all calls will manipulate the underlying UWP stream directly.