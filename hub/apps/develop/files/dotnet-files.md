---
ms.assetid: 5931d63c-6b80-4e47-b371-ee299e308b8e
title: Access files and folders with Windows App SDK and .NET
description: Packaged Windows App SDK apps can leverage .NET APIs for reading and writing files, working with folders, and reading drive and volume information.
ms.date: 06/16/2023
ms.topic: article
keywords: windows 10, windows 11, windows, winui, windows app sdk, dotnet
ms.localizationpriority: medium
---
# Access files and folders with Windows App SDK and .NET

Packaged Windows App SDK apps can leverage [.NET APIs](/dotnet/) for reading and writing files, working with folders, and reading drive and volume information. Additionally, any packaged desktop app can utilize both WinRT and Win32 APIs in the Windows SDK, as well as the APIs provided in the .NET SDK. This article provides guidance on how to use the .NET [System.IO](/dotnet/api/system.io) APIs to read and write files, manage drives and folders, and work with memory streams to encode or decode string data.

## Read and write files with .NET APIs

In the following example, `ReadWriteFiles` creates a new file, writes a set of integers to the file, and then reads the integers back from the file. The example uses the [FileStream](/dotnet/api/system.io.filestream) class to create a new file and to open the file for reading or writing. The example uses the [BinaryWriter](/dotnet/api/system.io.binarywriter) class to write the integers to the file and the [BinaryReader](/dotnet/api/system.io.binaryreader) class to read the integers from the file.

```csharp
using System.IO;
...
ReadWriteFiles("test.bin");
...
private void ReadWriteFiles(string fileName)
{
    if (File.Exists(fileName))
    {
        Console.WriteLine($"{fileName} already exists!");
        return;
    }

    using (FileStream fs = new(fileName, FileMode.CreateNew))
    {
        using BinaryWriter writer = new(fs);
        for (int i = 0; i < 11; i++)
        {
            writer.Write(i);
        }
    }

    using (FileStream fs = new(fileName, FileMode.Open, FileAccess.Read))
    {
        using BinaryReader reader = new(fs);
        for (int i = 0; i < 11; i++)
        {
            Console.WriteLine(reader.ReadInt32());
        }
    }
}
```

## Manage drives and folders in .NET

The following example shows how to use the [DirectoryInfo](/dotnet/api/system.io.directoryinfo) and [Directory](/dotnet/api/system.io.directory) classes to create, delete, and manage folders. The example uses the `DirectoryInfo` class to create a new directory, create a subdirectory, and delete the directory. The `DirectoryInfo` class provides methods for creating, moving, and enumerating through directories and subdirectories. The `Directory` class provides *static* methods for creating, moving, and enumerating through directories and subdirectories.

```csharp
using System.IO;
...
private void FolderTest()
{
    FolderManagement(@"c:\MyDir", "Projects");
}
private void FolderManagement(string path, string subfolderName)
{
    DirectoryInfo di = new(path);
    try
    {
        // Create directory if it doesn't exist
        if (di.Exists)
        {
            Console.WriteLine("Path already exists.");
        }
        else
        {
            di.Create();
            Console.WriteLine("The directory was created successfully.");
        }

        // Create subdirectory if it doesn't exist
        string subfolderPath = Path.Combine(path, subfolderName);
        if (Directory.Exists(subfolderPath))
        {
            Console.WriteLine("Subfolder path already exists.");
        }
        else
        {
            di.CreateSubdirectory(subfolderName);
            Console.WriteLine("The subdirectory was created successfully.");
        }

        // Delete directory
        di.Delete(true);
        Console.WriteLine("The directory was deleted successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("The process failed: {0}", ex.ToString());
    }
}
```

This example using the static [GetDrives](/dotnet/api/system.io.driveinfo.getdrives) method to retrieve information about all drives on the system. The [DriveInfo](/dotnet/api/system.io.driveinfo) class provides information about a drive, such as the drive type, label, file system, and available free space.

```csharp
using System.IO;
...
private void DriveManagement()
{
    DriveInfo[] drives = DriveInfo.GetDrives();

    foreach (DriveInfo d in drives)
    {
        Console.WriteLine($"Drive name: {d.Name}");
        Console.WriteLine($"  Drive type: {d.DriveType}");
        if (d.IsReady)
        {
            Console.WriteLine($"  Volume label: {d.VolumeLabel}");
            Console.WriteLine($"  File system type: {d.DriveFormat}");
            Console.WriteLine($"  Space available to user: {d.AvailableFreeSpace, 15} bytes");
            Console.WriteLine($"  Total available space: {d.TotalFreeSpace, 15} bytes");
            Console.WriteLine($"  Total size of drive: {d.TotalSize, 15} bytes ");
        }
    }
}
```

## Encode and decode strings with MemoryStream

This example shows how to use the [MemoryStream](/dotnet/api/system.io.memorystream) class to encode and decode string data. It first creates a `MemoryStream` to asynchronously write a string to a memory stream and then read the string from the memory stream. The [Encoding](/dotnet/api/system.text.encoding) class is used to convert the string to a byte array and then write the byte array to the memory stream. A [StreamReader](/dotnet/api/system.io.streamreader) is then used to asynchronously read the byte array from the memory stream and then convert the byte array back to a string by calling [ReadToEndAsync](/dotnet/api/system.io.streamreader.readtoendasync).

```csharp
using System.IO;
using System.Text;
...
private async Task EncodeDecodeStringAsync(string inputData)
{
    using MemoryStream stream = new();
    var inputBytes = Encoding.UTF8.GetBytes(inputData);
    await stream.WriteAsync(inputBytes, 0, inputBytes.Length);
    stream.Seek(0, SeekOrigin.Begin);

    using StreamReader reader = new(stream);
    string text = await reader.ReadToEndAsync();
    Console.WriteLine(text);
}
```

> [!NOTE]
> For information about converting between .NET streams and WinRT streams, see [How to: Convert between .NET and Windows Runtime streams](/dotnet/standard/io/how-to-convert-between-dotnet-streams-and-winrt-streams).

## See also

[Access files and folders with Windows App SDK and WinRT APIs](winrt-files.md)

[Files, folders, and libraries with Windows App SDK](index.md)
