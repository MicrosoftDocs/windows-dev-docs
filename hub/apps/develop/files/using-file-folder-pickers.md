---
title: Open files and folders with Windows App SDK pickers
description: Use Windows App SDK file and folder pickers in your WinUI app, with FileOpenPicker and FolderPicker examples, options, and results.
ms.date: 07/15/2026
ms.topic: how-to
keywords: windows 11, winui, windows app sdk
dev_langs:
  - csharp
  - cppwinrt
# Customer intent: As a Windows app developer, I want to use the file picker APIs in Windows App SDK to allow users to open files and folders in my WinUI app, so that they can specify the name and location of the file or folder.
---

# Tutorial: Open files and folders with pickers in WinUI

Use Windows App SDK file and folder pickers to let users browse and select files or folders in your WinUI app. The picker APIs provide a familiar Windows experience that helps users navigate their device and cloud storage locations. This article shows you how to implement file open pickers and folder pickers, customize their behavior, and handle the selected results in your app.

The Windows App SDK [FileOpenPicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker) and [FileSavePicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.filesavepicker) classes create a picker dialog that lets users specify the name and location of a file to open or save. The [FolderPicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.folderpicker) class lets you select a folder.

To learn about using a picker to save files, see [Save a file with a Windows App SDK picker](pickers-save-file.md).

## Prerequisites

- **Windows App SDK 1.8 or later.** The `Microsoft.Windows.Storage.Pickers` APIs used in this article were introduced in Windows App SDK 1.8. If your project targets an earlier version, see [Use WinRT pickers with HWND interop](#use-winrt-pickers-with-hwnd-interop) for the legacy approach.

## Important APIs

This article uses the following APIs:

- [FileOpenPicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker)
- [FolderPicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.folderpicker)
- [PickFileResult](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.pickfileresult)
- [PickFolderResult](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.pickfolderresult)

## File picker UI

A file picker displays information to orient users and provide a consistent experience when opening or saving files.

That information includes:

- The current location
- The item or items that the user picks
- A tree of locations that the user can browse to. These locations include file system locations—such as the Music or Downloads folder—as well as apps that implement the file picker contract (such as Camera, Photos, and Microsoft OneDrive).

You might have an app that lets users open or save files. When the user initiates that action, your app calls the file picker, which displays the file picker UI:

:::image type="content" source="images/picker-open-file.png" alt-text="Screenshot of an open file picker with a filter selected to show .txt, .pdf, .doc, and .docx files.":::

## How pickers work with your app

With a picker, your app can access, browse, and save files and folders on the user's system. Your app receives those picks as lightweight [PickFileResult](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.pickfileresult) and [PickFolderResult](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.pickfolderresult) objects, which provide the path to the selected file or folder.

The picker uses a single, unified interface to let the user pick files and folders from the file system or from other apps. Files picked from other apps are like files from the file system. In general, your app can operate on them in the same ways as other objects. Other apps make files available by participating in file picker contracts.

For example, you might call the file picker in your app so that your user can open a file. This action makes your app the calling app. The file picker interacts with the system and other apps to let the user navigate and pick the file. When your user chooses a file, the file picker returns that file's path to your app.

## Pick a file to open example

The following code shows how to use the [FileOpenPicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker) class to let the user pick a single file, such as a photo. The code sets properties on the picker to customize its appearance and behavior, and then shows the picker to the user by using the [PickSingleFileAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker.picksinglefileasync) method. If the user picks a file, the app reads the file's content and stores it in a variable.

```csharp
using Microsoft.Windows.Storage.Pickers;

var openPicker = new FileOpenPicker(this.AppWindow.Id)
{
    // (Optional) Specify the initial location for the picker. 
    //     If the specified location doesn't exist on the user's machine, it falls back to the DocumentsLibrary.
    //     If not set, it defaults to PickerLocationId.Unspecified, and the system will use its default location.
    SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
    
    // (Optional) specify the text displayed on the commit button. 
    //     If not specified, the system uses a default label of "Open" (suitably translated).
    CommitButtonText = "Choose selected files",

    // (Optional) specify file extension filters. If not specified, defaults to all files (*.*).
    FileTypeFilter = { ".txt", ".pdf", ".doc", ".docx" },

    // (Optional) specify the view mode of the picker dialog. If not specified, defaults to List.
    ViewMode = PickerViewMode.List,
};

var result = await openPicker.PickSingleFileAsync();
if (result is not null)
{
    var content = System.IO.File.ReadAllText(result.Path);
}
else
{
    // Add your error handling here.
}
```

```cppwinrt
#include <winrt/Microsoft.Windows.Storage.Pickers.h>
#include <fstream>
#include <string>

using namespace winrt::Microsoft::Windows::Storage::Pickers;

FileOpenPicker openPicker(this->AppWindow().Id());

// (Optional) Specify the initial location for the picker. 
//     If the specified location doesn't exist on the user's machine, it falls back to the DocumentsLibrary.
//     If not set, it defaults to PickerLocationId.Unspecified, and the system will use its default location.
openPicker.SuggestedStartLocation(PickerLocationId::DocumentsLibrary);

// (Optional) specify the text displayed on the commit button. 
//     If not specified, the system uses a default label of "Open" (suitably translated).
openPicker.CommitButtonText(L"Choose selected files");

// (Optional) specify file extension filters. If not specified, defaults to all files (*.*).
openPicker.FileTypeFilter().ReplaceAll({ L".txt", L".pdf", L".doc", L".docx" });

// (Optional) specify the view mode of the picker dialog. If not specified, defaults to List.
openPicker.ViewMode(PickerViewMode::List);

auto result{ co_await openPicker.PickSingleFileAsync() };
if (result)
{
    std::ifstream fileReader(result.Path().c_str());
    std::string text((std::istreambuf_iterator<char>(fileReader)), std::istreambuf_iterator<char>());
    winrt::hstring hText = winrt::to_hstring(text);
}
else
{
    // Add your error handling here.
}
```

## Pick multiple files to open example

You can also let the user pick multiple files. The following code shows how to use the [FileOpenPicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker) class to let the user pick multiple files, such as photos. The process is the same but the [PickMultipleFilesAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.fileopenpicker.pickmultiplefilesasync) method returns a collection of [PickFileResult](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.pickfileresult) objects. Each result exposes a `Path` property when you need the raw file path.

```csharp
using Microsoft.Windows.Storage.Pickers;

var openPicker = new FileOpenPicker(this.AppWindow.Id);

var results = await openPicker.PickMultipleFilesAsync();
if (results.Count > 0)
{
    var pickedFilePaths = results.Select(f => f.Path);
    foreach (var path in pickedFilePaths)
    {
        var content = System.IO.File.ReadAllText(path);
    }
}
else
{
    // Add your error handling here.
}
```

```cppwinrt
#include <winrt/Microsoft.Windows.Storage.Pickers.h>
#include <fstream>
#include <string>

using namespace winrt::Microsoft::Windows::Storage::Pickers;

FileOpenPicker openPicker(this->AppWindow().Id());
auto results{ co_await openPicker.PickMultipleFilesAsync() };
if (results.Size() > 0)
{
    for (auto const& result : results)
    {
        std::ifstream fileReader(result.Path().c_str());
        std::string text((std::istreambuf_iterator<char>(fileReader)), std::istreambuf_iterator<char>());
        winrt::hstring hText = winrt::to_hstring(text);
    }
}
else
{
    // Add your error handling here.
}
```

## Pick a folder example

To pick a folder by using the [FolderPicker](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.folderpicker) class, use the following code. This code creates a folder picker, shows it to the user by using the [PickSingleFolderAsync](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.folderpicker.picksinglefolderasync) method, and retrieves the selected folder's path in a [PickFolderResult](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers.pickfolderresult) object. If the user picks a folder, the app retrieves the folder's path and stores it in a variable for later use.

```csharp
using Microsoft.Windows.Storage.Pickers;

var folderPicker = new FolderPicker(this.AppWindow.Id)
{
    // (Optional) Specify the initial location for the picker. 
    //     If the specified location doesn't exist on the user's machine, it falls back to the DocumentsLibrary.
    //     If not set, it defaults to PickerLocationId.Unspecified, and the system will use its default location.
    SuggestedStartLocation = PickerLocationId.DocumentsLibrary,

    // (Optional) specify the text displayed on the commit button. 
    //     If not specified, the system uses a default label of "Open" (suitably translated).
    CommitButtonText = "Select Folder",

    // (Optional) specify the view mode of the picker dialog. If not specified, default to List.
    ViewMode = PickerViewMode.List,
};

var result = await folderPicker.PickSingleFolderAsync();

if (result is not null)
{
    var path = result.Path;
}
else
{
    // Add your error handling here.
}
```

```cppwinrt
#include <winrt/Microsoft.Windows.Storage.Pickers.h>

using namespace winrt::Microsoft::Windows::Storage::Pickers;

FolderPicker folderPicker(this->AppWindow().Id());

// (Optional) Specify the initial location for the picker. 
//     If the specified location doesn't exist on the user's machine, it falls back to the DocumentsLibrary.
//     If not set, it defaults to PickerLocationId.Unspecified, and the system will use its default location.
folderPicker.SuggestedStartLocation(PickerLocationId::DocumentsLibrary);

// (Optional) specify the text displayed on the commit button. 
//     If not specified, the system uses a default label of "Open" (suitably translated).
folderPicker.CommitButtonText(L"Select Folder");

// (Optional) specify the view mode of the picker dialog. If not specified, default to List.
folderPicker.ViewMode(PickerViewMode::List);

auto result{ co_await folderPicker.PickSingleFolderAsync() };
if (result)
{
    auto path{ result.Path() };
}
else
{
    // Add your error handling here.
}
```

> [!TIP]
> Whenever your app accesses a file or folder through a picker, add it to your app's [FutureAccessList](/uwp/api/windows.storage.accesscache.storageapplicationpermissions.futureaccesslist) or [MostRecentlyUsedList](/uwp/api/windows.storage.accesscache.storageapplicationpermissions.mostrecentlyusedlist) to keep track of it by using the Windows Runtime (WinRT) APIs. For more information, see [Track recently used files and folders](track-recently-used-files-folders.md).

The folder picker UI looks like this:

:::image type="content" source="images/picker-folder.png" alt-text="Screenshot of a folder picker viewing the C drive.":::

## Use WinRT pickers with HWND interop

If your project targets Windows App SDK 1.7 or earlier, or if you use the legacy [Windows.Storage.Pickers](/uwp/api/windows.storage.pickers) APIs (for example, when migrating from UWP), you must initialize the picker with a window handle (HWND) before showing it. Without this step, the picker throws an exception or fails silently in desktop (WinUI 3) apps.

> [!IMPORTANT]
> WinUI 3 desktop apps don't have a `CoreWindow`. You must associate the picker with your app window by calling `WinRT.Interop.InitializeWithWindow.Initialize` (C#) or `IInitializeWithWindow::Initialize` (C++/WinRT) before any `Pick*Async` call.

### C# example (legacy WinRT pickers)

```csharp
// This example assumes you have a reference to your app's main window.
// In a Window class, use 'this'. In a Page, use a stored reference like App.MainWindow.

var picker = new Windows.Storage.Pickers.FileOpenPicker();
picker.FileTypeFilter.Add(".png");
picker.FileTypeFilter.Add(".jpg");

// Get the window handle (HWND) for the current WinUI 3 window.
var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);

// Associate the picker with the window.
WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

var file = await picker.PickSingleFileAsync();
if (file is null)
{
    // The user cancelled the picker.
}
```

### C++/WinRT example (legacy WinRT pickers)

```cppwinrt
// pch.h
#include <winrt/Windows.Storage.Pickers.h>
#include <microsoft.ui.xaml.window.h>
#include <shobjidl_core.h>

// MainWindow.xaml.cpp — inside a MainWindow member function
Windows::Storage::Pickers::FileOpenPicker picker;
picker.FileTypeFilter().Append(L".png");
picker.FileTypeFilter().Append(L".jpg");

// Get the window handle (HWND) of the current WinUI 3 window.
auto windowNative{ this->m_inner.as<::IWindowNative>() };
HWND hwnd{ 0 };
windowNative->get_WindowHandle(&hwnd);

// Associate the picker with the window.
auto initializeWithWindow{ picker.as<::IInitializeWithWindow>() };
initializeWithWindow->Initialize(hwnd);

auto file{ co_await picker.PickSingleFileAsync() };
if (!file)
{
    // The user cancelled the picker.
}
```

> [!TIP]
> The recommended approach for new apps is to use the Windows App SDK pickers (`Microsoft.Windows.Storage.Pickers`), which accept a `WindowId` in the constructor and don't require the `InitializeWithWindow` pattern. See the examples earlier in this article.

For more information about retrieving a window handle, see [Retrieve a window handle (HWND)](../ui/retrieve-hwnd.md).

## Related content

- [Microsoft.Windows.Storage.Pickers namespace](/windows/windows-app-sdk/api/winrt/microsoft.windows.storage.pickers)
- [Save a file with a Windows App SDK picker](pickers-save-file.md)
- [Files, folders, and libraries with Windows App SDK](index.md)
- [Retrieve a window handle (HWND)](../ui/retrieve-hwnd.md)
- [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md)
- [Display WinRT UI objects that depend on CoreWindow](../ui/display-ui-objects.md)
- [WinUI Gallery file picker sample](https://github.com/microsoft/WinUI-Gallery)
