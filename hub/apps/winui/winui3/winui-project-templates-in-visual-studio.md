---
title: Visual Studio for Windows app development
description: Visual Studio is the recommended IDE for building Windows apps with WinUI and the Windows App SDK. Learn about key features and available project templates.
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui, visual studio
ms.date: 03/03/2026
ms.topic: article
---

# Visual Studio for Windows app development

[Visual Studio](https://visualstudio.microsoft.com/) is the recommended IDE for building Windows apps with [WinUI](/windows/apps/winui/) and the [Windows App SDK](/windows/apps/windows-app-sdk/). It provides deep integration with the Windows platform and tools purpose-built for XAML-based app development.

Key features for Windows app developers include:

- **XAML Hot Reload** — modify XAML markup while your app is running and see changes applied instantly without restarting
- **XAML Live Visual Tree** — inspect the runtime visual tree of your running app to debug layout issues and understand element hierarchy
- **IntelliSense for XAML and C#/C++** — get code completion, quick info, and error highlighting for both markup and code-behind
- **Integrated debugging** — set breakpoints in XAML and code, inspect data bindings, and diagnose UI rendering issues

To get started, see [Start developing Windows apps](../../get-started/start-here.md).

## WinUI project templates

Visual Studio includes project templates to help you quickly create WinUI apps. To find them, open **File** > **New** > **Project**, then filter by **WinUI** in the project type drop-down or search for *WinUI*.

:::image type="content" source="images/WinUI3-csharp-newproject-1.0-later.png" alt-text="WinUI project templates" border="true":::

### Blank App (Packaged)

Creates a desktop app in C# (.NET) or C++ (Win32) with a WinUI-based UI. The project starts with a single window derived from **Microsoft.UI.Xaml.Window**, ready for you to add your own controls and pages. This is the recommended starting point for most new apps. For a walkthrough, see [Start developing Windows apps](../../get-started/start-here.md).

### Blank App (Packaged with WAP Project)

Same as the Blank App template above, but adds a separate [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) to the solution. This project handles building your app into an [MSIX package](/windows/msix/overview) for distribution. If you prefer a simpler setup, you can use [single-project MSIX](../../windows-app-sdk/single-project-msix.md) instead to avoid maintaining the separate packaging project.

### Component and test templates

These templates create libraries and test projects that work alongside a WinUI app.

| Template | Language | Description |
|----------|----------|-------------|
| **Class Library** | C# | A .NET class library (DLL) for sharing code across WinUI apps. |
| **Windows Runtime Component** | C++ | A [Windows Runtime component](/windows/uwp/winrt-components/) written in C++/WinRT that can be consumed by any app with a WinUI-based UI, regardless of programming language. |
| **Unit Test App** | C# and C++ | An MSTest project for writing and running automated tests against your app. |

## WinUI item templates

Item templates let you add new files to an existing WinUI project. Right-click your project in **Solution Explorer**, select **Add** > **New Item**, and choose the **WinUI** tab.

![WinUI item templates](images/winui3-addnewitem.png)

| Template | Language | Description |
|----------|----------|-------------|
| **Blank Page** | C# and C++ | A new XAML page derived from **Microsoft.UI.Xaml.Controls.Page**. |
| **Blank Window** | C# and C++ | A new window derived from **Microsoft.UI.Xaml.Window**. |
| **Resource Dictionary** | C# and C++ | An empty, keyed collection of XAML resources. See [ResourceDictionary and XAML resource references](../../develop/platform/xaml/xaml-resource-dictionary.md). |
| **Resources File (.resw)** | C# and C++ | A string resource file for localizing your app. See [Localize strings in your UI and app package manifest](/windows/uwp/app-resources/localize-strings-ui-manifest). |
| **Templated Control** | C# and C++ | A lookless control derived from **Microsoft.UI.Xaml.Controls.Control** with a default style. See the walkthroughs for [C++/WinRT](xaml-templated-controls-cppwinrt-winui-3.md) and [C#](xaml-templated-controls-csharp-winui-3.md). |
| **User Control** | C# and C++ | A composite control derived from **Microsoft.UI.Xaml.Controls.UserControl** that groups existing controls with custom logic. See [Custom XAML Controls](/archive/msdn-magazine/2019/may/xaml-custom-xaml-controls). |

## See also

* [Windows App SDK](../../windows-app-sdk/index.md)
* [Start developing Windows apps](../../get-started/start-here.md)
* [Windows App SDK Samples](https://github.com/microsoft/WindowsAppSDK-Samples)
