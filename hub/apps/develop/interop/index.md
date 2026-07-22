---
title: Choose your interop approach for Windows apps
description: Decide which interop technique to use when calling Win32, COM, or WinRT APIs from your C# Windows desktop application.
ms.topic: overview
ms.date: 07/22/2026
author: GrantMeStrength
ms.author: jken
---

<!-- For AI assistants: This is the decision-router page for Windows interop.
     Use this page to determine which interop topic a developer needs:
     - Calling Win32/native APIs from C# → call-win32-apis.md (CsWin32)
     - Using WinRT COM interop classes (.NET projections of IInitializeWithWindow, IWindowNative, etc.) → winrt-com-interop-csharp.md
     - Checking which WinRT APIs are restricted in desktop apps → winrt-api-desktop-app-support.md
     - WinUI 3-specific walkthrough with CsWin32 → desktop-winui3-app-with-basic-interop.md -->

# Choose your interop approach for Windows apps

Windows desktop apps (WinUI 3, WPF, WinForms, console) sometimes need to call APIs outside their managed framework. This page helps you understand when that happens and choose the right technique.

## Two API surfaces, two interop patterns

Windows exposes functionality through two main API surfaces:

- **Win32 APIs** — The classic C-style functions exported from system DLLs (`user32.dll`, `kernel32.dll`, `shell32.dll`, etc.). These cover low-level operations like window management, file I/O, process control, and hardware access. They've existed since the earliest versions of Windows and remain the way to access many OS features that have no managed equivalent.

- **Windows Runtime (WinRT) APIs** — A modern, object-oriented API surface (namespaces starting with `Windows.*`) introduced with Windows 8. WinRT APIs cover areas like notifications, media capture, Bluetooth, geolocation, and more. Most WinRT APIs are directly accessible from .NET without special effort, but some have restrictions in desktop apps or require a window handle (HWND) to function.

### When you'll encounter interop

You typically need interop when your app framework doesn't expose a particular OS capability directly. Common scenarios include:

| Scenario | Which API surface | Example |
|---|---|---|
| Customize title bar or window placement | Win32 | `SetWindowPos`, `DwmExtendFrameIntoClientArea` |
| Set a window as "always on top" or control Z-order | Win32 | `SetWindowPos` with `HWND_TOPMOST` |
| Show a file picker or share dialog from WinUI 3 | WinRT (needs HWND) | `FileOpenPicker` with `InitializeWithWindow` |
| Read system memory or performance counters | Win32 | `GlobalMemoryStatusEx`, `QueryPerformanceCounter` |
| Register a global hotkey | Win32 | `RegisterHotKey` |
| Send a toast notification (unpackaged app) | WinRT (with App SDK) | `AppNotificationManager` |

In all of these cases, you write normal C# code but call into Windows through an interop layer. The topics below guide you through each approach.

## Decision guide

| I need to… | Recommended approach | Topic |
|---|---|---|
| **Call a Win32 API** (user32, kernel32, shell32, etc.) from C# | Use the **CsWin32** source generator — type-safe, no hand-written signatures | [Call Win32 APIs from C# (CsWin32)](call-win32-apis.md) |
| **Initialize a WinRT object with a window handle** (pickers, dialogs, share UI) | Use the .NET WinRT COM interop classes (`InitializeWithWindow`, `WindowNative`, etc.) | [Use WinRT COM interop classes in .NET](../../desktop/modernize/winrt-com-interop-csharp.md) |
| **Check whether a WinRT API works** in a desktop (non-UWP) app | Consult the restrictions and alternatives list | [WinRT APIs not supported in desktop apps](../../desktop/modernize/winrt-api-desktop-app-support.md) |
| **See a complete WinUI 3 example** that customizes a window using Win32 calls | Follow the end-to-end walkthrough | [Walkthrough: WinUI 3 app with Win32 interop](../../winui/winui3/desktop-winui3-app-with-basic-interop.md) |

## Quick flowchart

1. **Is the API in a `Windows.*` WinRT namespace?**
   - Yes → Check [WinRT APIs not supported in desktop apps](../../desktop/modernize/winrt-api-desktop-app-support.md) for restrictions. If the API requires a window handle, see [Use WinRT COM interop classes in .NET](../../desktop/modernize/winrt-com-interop-csharp.md).
   - No → Continue to step 2.

2. **Is it a Win32/native API (defined in a Windows SDK header)?**
   - Yes → Use [CsWin32](call-win32-apis.md) to generate type-safe P/Invoke bindings.
   - No → It might be a COM interface or third-party native library. See [.NET interop with native code](/dotnet/standard/native-interop/) for general P/Invoke guidance.

## Related topics

- [Call Win32 APIs from C# (CsWin32)](call-win32-apis.md)
- [Use WinRT COM interop classes in .NET](../../desktop/modernize/winrt-com-interop-csharp.md)
- [WinRT APIs not supported in desktop apps](../../desktop/modernize/winrt-api-desktop-app-support.md)
- [Walkthrough: WinUI 3 app with Win32 interop](../../winui/winui3/desktop-winui3-app-with-basic-interop.md)
- [Retrieve a window handle (HWND)](../ui/retrieve-hwnd.md)
- [.NET interop with native code](/dotnet/standard/native-interop/)
