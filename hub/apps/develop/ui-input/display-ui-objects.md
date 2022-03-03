---
title: Display Windows.\*-namespace UI objects
description: In your desktop app, you can display many Windows Runtime (WinRT) user-interface (UI) objects that are in **Windows.\*** namespaces. For some of those types (pickers, popups, dialogs, and other UI objects), you just need to add a little bit of interoperation code. This topic shows you how.
ms.topic: article
ms.date: 03/02/2022
keywords: Windows, App, SDK, desktop, C#, C++, cpp, window, handle, HWND, Windows UI Library, WinUI, interop
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---

# Display Windows.\*-namespace UI objects

In your desktop app, you can display many Windows Runtime (WinRT) user-interface (UI) objects that are in **Windows.\*** namespaces. For some of those types (pickers, popups, dialogs, and other UI objects), you just need to add a little bit of interoperation code. This topic shows you how.

## Parent a UI object to a window handle (HWND)

The following UI objects apply to this section:

* [**DataTransferManager.ShowShareUI**](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.showshareui#Windows_ApplicationModel_DataTransfer_DataTransferManager_ShowShareUI) method
* [**Windows.Storage.Pickers.FileOpenPicker**](/uwp/api/windows.storage.pickers.fileopenpicker) class
* [**Windows.Storage.Pickers.FileSavePicker**](/uwp/api/windows.storage.pickers.filesavepicker) class
* [**Windows.Storage.Pickers.FolderPicker**](/uwp/api/windows.storage.pickers.folderpicker) class
* [**Windows.UI.Popups.PopupMenu**](/uwp/api/windows.ui.popups.popupmenu) class

The interop code involves simply parenting the UI object to a window in your app. It's a two-step process.

1. Decide which window will be the parent of the UI object that you want to display, and retrieve that window's HWND. For more details and code examples for this step, see [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd).
2. Then call the appropriate interoperatability API (for C# or C++/WinRT) to *parent* the UI object to the window handle (HWND).

The next sections contain code examples to display a [**FolderPicker**](/uwp/api/windows.storage.pickers.fileopenpicker). But it's the same technique to display any of the UI objects listed above.

## Windows UI Library (WinUI) 3, by using C#

The C# code below expects that you've already used the pattern documented in [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd#windows-ui-library-winui-3-by-using-c). Then, to *parent* the UI object that you want to display to a window, the code calls the **Initialize** method on the **WinRT.Interop.InitializeWithWindow** C# interop class. For more info about the C# interop classes, see [Call WinRT COM interop interfaces from a .NET 5+ app](/windows/apps/desktop/modernize/winrt-com-interop-csharp).

```csharp
// MainWindow.xaml.cs
private async void ShowFolderPickerAsync(IntPtr hWnd)
{
    // Create a folder picker.
    var folderPicker = new Windows.Storage.Pickers.FolderPicker();

    // Initialize the folder picker with the window handle (HWND).
    WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hWnd);

    // Use the folder picker as usual.
    folderPicker.FileTypeFilter.Add("*");
    var folder = await folderPicker.PickSingleFolderAsync();
}
```

## Windows UI Library (WinUI) 3, by using C++

The C++/WinRT code below expects that you've already used the pattern documented in [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd#windows-ui-library-winui-3-by-using-c-1). Then, to *parent* the UI object that you want to display to a window, the code calls the interoperatability method [**IInitializeWithWindow::Initialize**](/windows/win32/api/shobjidl_core/nf-shobjidl_core-iinitializewithwindow-initialize).

```cppwinrt
// pch.h
...
#include <microsoft.ui.xaml.window.h>
#include <Shobjidl.h>
#include <winrt/Windows.Storage.Pickers.h>

// MainWindow.xaml.cpp
winrt::fire_and_forget ShowFolderPickerAsync(HWND hWnd)
{
    // Create a folder picker.
    Windows::Storage::Pickers::FolderPicker folderPicker;

    // Initialize the folder picker with the window handle (HWND).
    auto initializeWithWindow{ folderPicker.as<::IInitializeWithWindow>() };
    initializeWithWindow->Initialize(hWnd);

    // Use the folder picker as usual.
    folderPicker.FileTypeFilter().Append(L"*");
    auto folder{ co_await folderPicker.PickSingleFolderAsync() };
}
```

## Related topics

* [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd)
* [DataTransferManager.ShowShareUI method](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.showshareui#Windows_ApplicationModel_DataTransfer_DataTransferManager_ShowShareUI)
* [Windows.Storage.Pickers.FileOpenPicker class](/uwp/api/windows.storage.pickers.fileopenpicker)
* [Windows.Storage.Pickers.FileSavePicker class](/uwp/api/windows.storage.pickers.filesavepicker)
* [Windows.Storage.Pickers.FolderPicker class](/uwp/api/windows.storage.pickers.folderpicker)
* [Windows.UI.Popups.PopupMenu class](/uwp/api/windows.ui.popups.popupmenu)
