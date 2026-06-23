---
description: Reference table mapping Windows Share DataFormats to their send and receive APIs, and a FileType-by-app-category cheat sheet.
title: Windows Share DataFormat and FileType reference
author: GrantMeStrength
ms.author: jken
ms.topic: reference
ms.date: 06/22/2026
ms.localizationpriority: medium
---

# Windows Share DataFormat and FileType reference

Use this reference to choose the correct `DataFormat` declarations and APIs for your Windows Share integration.

## DataFormat reference table

| DataFormat | Send API | Receive API | Typical app types |
|--|--|--|--|
| `Text` | [SetText](/uwp/api/windows.applicationmodel.datatransfer.datapackage.settext) | `GetTextAsync` | Notes apps, clipboard tools, general text |
| `Uri` / `WebLink` | [SetWebLink](/uwp/api/windows.applicationmodel.datatransfer.datapackage.setweblink) | `GetWebLinkAsync` | Browsers, bookmarks, messaging apps |
| `ApplicationLink` | [SetApplicationLink](/uwp/api/windows.applicationmodel.datatransfer.datapackage.setapplicationlink) | `GetApplicationLinkAsync` | Apps that expose deep links |
| `Html` | [SetHtmlFormat](/uwp/api/windows.applicationmodel.datatransfer.datapackage.sethtmlformat) | `GetHtmlFormatAsync` | Email clients, rich-text editors |
| `Bitmap` | [SetBitmap](/uwp/api/windows.applicationmodel.datatransfer.datapackage.setbitmap) | `GetBitmapAsync` | Photo editors, image viewers |
| `StorageItems` | [SetStorageItems](/uwp/api/windows.applicationmodel.datatransfer.datapackage.setstorageitems) | `GetStorageItemsAsync` | File managers, cloud storage apps |
| `Rtf` | [SetRtf](/uwp/api/windows.applicationmodel.datatransfer.datapackage.setrtf) | `GetRtfAsync` | Word processors, rich-text editors |

> [!TIP]
> For URLs, always prefer `SetWebLink` over `SetText`. Target apps can generate rich link previews and handle navigation correctly only when the content arrives typed as a URI.

## FileType declarations by app category

Use this cheat sheet to choose the narrowest set of `<uap:FileType>` declarations for your manifest. Declaring fewer, more specific types reduces noise in the Share Sheet and prevents your app from appearing for content it can't handle.

| App category | Recommended `<uap:FileType>` declarations | Additional data formats |
|--|--|--|
| **Photo viewer / editor** | `.jpg` `.jpeg` `.png` `.gif` `.bmp` `.heic` `.webp` | `Bitmap`, `StorageItems` |
| **Video player** | `.mp4` `.mov` `.avi` `.mkv` `.wmv` | `StorageItems` |
| **Audio player** | `.mp3` `.flac` `.wav` `.aac` `.m4a` | `StorageItems` |
| **Document editor** | `.pdf` `.docx` `.xlsx` `.pptx` `.txt` | `StorageItems` |
| **Link handler / browser** | *(no file types)* | `Uri`, `WebLink` |
| **Messaging app** | *(no file types, or specific attachment types)* | `Uri`, `WebLink`, `StorageItems` |
| **Notes app** | `.txt` `.md` | `Text`, `StorageItems` |
| **Cloud storage / file mover** | `<uap:SupportsAnyFileType />` | `StorageItems` |

> [!IMPORTANT]
> `<uap:SupportsAnyFileType />` is appropriate only for general-purpose file movers. For every other app category, declare specific file extensions. Over-broad declarations are the most common share-target bug.

## Related content

- [Share content from your app](integrate-sharesheet-send.md)
- [Receive content in your app](integrate-sharesheet-receive.md)
- [People on Windows (Cross-device People API)](cross-device-people-api.md)
- [Share on Windows: integrate the Windows Share Sheet](integrate-sharesheet-overview.md)
