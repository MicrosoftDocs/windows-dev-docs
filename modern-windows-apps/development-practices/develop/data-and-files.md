---
title: Data and files
description: This article provides an index of development features that are related to scenarios involving data and files in Windows apps.
ms.topic: article
ms.date: 05/11/2021
keywords: 
---

# Data and files

This article provides an index of development features that are related to scenarios involving data and files in Windows apps.

## Windows App SDK features

The [Windows App SDK](../windows-app-sdk/index.md) provides the following features related to data and files scenarios for Windows 10 and later OS releases.

| Feature | Description |
|---------|-------------|
| [Manage resources with MRT Core](../windows-app-sdk/mrtcore/mrtcore-overview.md) | Use the *MRT Core* APIs in the [Microsoft.Windows.ApplicationModel.Resources](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources) namespace to manage app resources such as strings and images in multiple languages, scales, and contrast variants independently of your app's logic. MRT Core is a streamlined version of the older resource management APIs in the [Windows.ApplicationModel.Resources](/uwp/api/windows.applicationmodel.resources) of the Windows platform. |

## Windows OS features

Windows 10 and later OS releases provide a wide variety of APIs related to data and files scenarios for apps. These features are available via a combination of WinRT and Win32 (C++ and COM) APIs provided by the [Windows SDK](https://developer.microsoft.com/windows/downloads/windows-sdk).

### WinRT APIs

The following articles provide information about features available via WinRT APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [App resources](/windows/uwp/app-resources/) | Learn how to use the APIs in the [Windows.ApplicationModel.Resources](/uwp/api/windows.applicationmodel.resources) namespace and other related namespaces to manage app resources such as strings and images in multiple languages, scales, and contrast variants independently of your app's logic. |
| [Data binding](/windows/uwp/data-binding/) | Learn how to bind your app's UI to data and keep the UI in sync with changes to the data. |
| [Files, folders, and libraries](/windows/uwp/files/) | Learn how to read and write text and other data formats in files, and to manage files and folders. |

### Win32 (C++ and COM) APIs

The following articles provide information about features available via Win32 (C++ and COM) APIs provided by the Windows SDK.

| Article | Description |
|---------|-------------|
| [Menus and other resources](/windows/win32/menurc/resources) | Learn how to manage app resources including icons, cursors, menus, dialog boxes, bitmaps, and much more. |
| [Data access and storage](/windows/desktop/data-access-and-storage) | Learn about data access and storage scenarios involving file and file system management, database access, and much more. |

## .NET features

The .NET SDK also provides APIs related to data and files scenarios for WPF and Windows Forms apps.

| Article | Description |
|---------|-------------|
| [Resources in .NET apps](/dotnet/framework/resources/) | Learn general strategies for managing app resources in .NET apps. |
| [Application resource, content, and data files (WPF)](/dotnet/framework/wpf/app-development/wpf-application-resource-content-and-data-files) | Learn how to manage app resources in WPF apps. |
| [Application settings (Windows Forms)](/dotnet/framework/winforms/advanced/application-settings-for-windows-forms) | Learn how to create, store, and maintain custom application and user preferences in Windows Forms apps. |
| [LINQ](/dotnet/standard/linq/)  | Learn how to use Language-Integrated Query (LINQ) to perform language-level data queries in .NET apps.  |
| [XML documents and data](/dotnet/standard/data/xml/)  | Learn how to parse and write XML, edit XML data in memory, validate XML data, and perform XSLT transformations in .NET apps.  |
| [Entity Framework Core](/ef/core/)  | Learn how to work with databases using Entity Framework Core in .NET apps.  |
| [Data binding (WPF)](/dotnet/framework/wpf/data/data-binding-wpf) | Learn how to bind your WPF app's UI to data and keep the UI in sync with changes to the data. |
| [Data binding (Windows Forms)](/dotnet/framework/winforms/windows-forms-data-binding) | Learn how to bind your Windows Forms app's UI to data and keep the UI in sync with changes to the data. |
| [File and stream I/O](/dotnet/standard/io/) | Learn how to read and write on data streams and files in .NET apps, both synchronously and asynchronously. |
