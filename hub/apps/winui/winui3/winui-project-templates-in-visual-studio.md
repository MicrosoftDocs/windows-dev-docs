---
title: WinUI 3 templates in Visual Studio
description: Once you've set up your development computer (see [Install tools for the Windows App SDK](../../windows-app-sdk/set-up-your-development-environment.md)), you're ready to create a WinUI 3 app by starting from one of the WinUI 3 project templates in Visual Studio. This topic describes the available project and item templates.
keywords: windows 10, windows 11, Windows App SDK, Windows app development platform, desktop development, win32, WinRT, uwp, toolkit sdk, winui, Windows UI Library
ms.date: 03/10/2022
ms.topic: article
---

# WinUI 3 templates in Visual Studio

Once you've set up your development computer (see [Install tools for the Windows App SDK](../../windows-app-sdk/set-up-your-development-environment.md)), you're ready to create a WinUI 3 app by starting from one of the WinUI 3 project templates in Visual Studio. This topic describes the available project and item templates. And [Create your first WinUI 3 project](./create-your-first-winui3-app.md) walks you through creating a project.

To access the WinUI 3 project templates, in the **New Project** dialog's drop-down filters, select **C#**/**C++**, **Windows**, and **WinUI**, respectively. Alternatively, you can search for *WinUI*, and select one of the available C# or C++ templates.

![WinUI project templates](images/WinUI3-csharp-newproject-1.0-later.png)

## Project templates for WinUI 3

You can use these WinUI 3 project templates to start creating an app.

### Blank App, Packaged (WinUI 3 in Desktop)

This project template creates a desktop .NET (C#) or native Win32 (C++) app with a WinUI 3-based user interface. The generated project includes a basic window that derives from the **Microsoft.UI.Xaml.Window** class in the WinUI 3 library that you can use to start building your UI. For more information about using this project template, see [Create your first WinUI 3 project](create-your-first-winui3-app.md).

The features of this project template vary between [versions of the Windows App SDK extension](../../windows-app-sdk/downloads.md).

- **Version 1.0 Preview 2:** Starting with this release, this project template generates an application project with the package manifest and other support needed to build the app into an [MSIX package](/windows/msix/overview) without the use of a separate packaging project. To use this project template, you must also install the [single-project MSIX packaging tools extension for Visual Studio](../../windows-app-sdk/single-project-msix.md).

    > [!NOTE]
    > In version 1.0 Preview 2 and later releases, this project template only supports a single executable in the generated MSIX package. If you need to combine multiple executables into a single MSIX package, then you'll need to use the **Blank App, Packaged with Windows Application Packaging Project (WinUI 3 in Desktop)** project template, or add a Windows Application Packaging Project to your solution.

- **Version 1.0 Preview 1 and earlier:** In these releases, this project template generates a solution with a separate [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that is configured to build the app into an [MSIX package](/windows/msix/overview). You can optionally install the [single-project MSIX packaging tools extension for Visual Studio](../../windows-app-sdk/single-project-msix.md) and combine the packaging project settings into your application project so you no longer need to maintain a separate packaging project.

### Blank App, Packaged with Windows Application Packaging Project (WinUI 3 in Desktop)

This project template is available in [version 1.0 Preview 1 and later releases](../../windows-app-sdk/downloads.md). It creates a desktop .NET (C#) or native Win32 (C++) app with a WinUI 3-based user interface. The generated project includes a basic window that derives from the **Microsoft.UI.Xaml.Window** class in the WinUI 3 library that you can use to start building your UI. For more info about using this project template, see [Create your first WinUI 3 project](create-your-first-winui3-app.md).

The solution also includes a separate [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that is configured to build the app into an [MSIX package](/windows/msix/overview). You can optionally install the [single-project MSIX packaging tools extension for Visual Studio](../../windows-app-sdk/single-project-msix.md) and combine the packaging project settings into your application project so you no longer need to maintain a separate packaging project.

### [Experimental] Blank App (WinUI 3 in UWP)

This project template creates a C# or C++ UWP app that with a WinUI 3-based user interface. The generated project includes a basic page that derives from the **Microsoft.UI.Xaml.Controls.Page** class in the WinUI 3 library, which you can use to start building your UI. For more information about this project template, see [Create your first WinUI 3 app](create-your-first-winui3-app.md).

### WinUI project templates for other components

You can use these WinUI 3 project templates to build components that can be loaded and used by a WinUI 3-based app.

| Template | Language | Description |
|----------|----------|-------------|
| Class Library (WinUI 3 in Desktop) | C# only | Creates a .NET managed class library (DLL) in C# that can be used by other .NET desktop apps with a WinUI 3-based user interface.  |
| [Experimental] Class Library (WinUI 3 in UWP)  | C# only | Creates a managed class library (DLL) in C# that can be used by other UWP apps with a WinUI 3-based user interface. |
| Windows Runtime Component (WinUI 3) | C++ | Creates a [Windows Runtime component](/windows/uwp/winrt-components/) written in C++/WinRT that can be consumed by any UWP or desktop app with a WinUI 3-based user interface, regardless of the programming language in which the app is written. |
| [Experimental] Windows Runtime Component (WinUI 3 in UWP) | C# | Creates a [Windows Runtime component](/windows/uwp/winrt-components/) written in C# that can be consumed by any UWP app with a WinUI 3-based user interface, regardless of the programming language in which the app is written. |

## Item templates for WinUI 3

The following item templates are available for use in a WinUI 3 project. To access these WinUI 3 item templates, right-click the project node in **Solution Explorer**, select **Add** -> **New item**, and click **WinUI** in the **Add New Item** dialog.

![WinUI item templates](images/winui3-addnewitem.png)

> [!NOTE]
> If you have the [experimental channel](../../windows-app-sdk/experimental-channel.md) or an older preview release of the Windows App SDK installed, you may see a second set of Item Templates that have the [Experimental] prefix. We recommend that you use those [Experimental] item templates if you're building a non-production/preview app, and use the stable, non-marked item templates if you're building a production desktop app.

| Template | Language | Description |
|----------|----------|-------------|
| Blank Page (WinUI 3) | C# and C++ | Adds a XAML file and code file that defines a new page derived from the **Microsoft.UI.Xaml.Controls.Page** class in the WinUI 3 library. |
| Blank Window (WinUI 3 in Desktop) | C# and C++ | Adds a XAML file and code file that defines a new window derived from the **Microsoft.UI.Xaml.Window** class in the WinUI 3 library. |
| Custom Control (WinUI 3) | C# and C++ | Adds a code file for creating a templated control with a default style. The templated control is derived from the **Microsoft.UI.Xaml.Controls.Control** class in the WinUI 3 library.<p></p>For a walkthrough that demonstrates how to use this item template, see [Templated XAML controls for UWP and WinUI 3 apps with C++/WinRT](xaml-templated-controls-cppwinrt-winui-3.md) and [Templated XAML controls for UWP and WinUI 3 apps with C#](xaml-templated-controls-csharp-winui-3.md). For more information about templated controls, see [Custom XAML Controls](/archive/msdn-magazine/2019/may/xaml-custom-xaml-controls). |
| Resource Dictionary (WinUI 3) | C# and C++ | Adds an empty, keyed collection of XAML resources. For more information, see [ResourceDictionary and XAML resource references](../../design/style/xaml-resource-dictionary.md). |
| Resources File (WinUI 3) | C# and C++ | Adds a file for storing string and conditional resources for your app. You can use this item to help localize your app. For more info, see [Localize strings in your UI and app package manifest](/windows/uwp/app-resources/localize-strings-ui-manifest). |
| User Control (WinUI 3) | C# and C++ | Adds a XAML file and code file for creating a user control that derives from the **Microsoft.UI.Xaml.Controls.UserControl** class in the WinUI 3 library. Typically, a user control encapsulates related existing controls and provide its own logic.<p></p>For more information about user controls, see [Custom XAML Controls](/archive/msdn-magazine/2019/may/xaml-custom-xaml-controls). |

## See also

* [Windows App SDK](../../windows-app-sdk/index.md)
* [Create your first WinUI 3 project](./create-your-first-winui3-app.md)
* [Stable release channel for the Windows App SDK](../../windows-app-sdk/stable-channel.md)
* [Windows App SDK Samples](https://github.com/microsoft/WindowsAppSDK-Samples)
* [Templates source in Windows App SDK Samples](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Templates)
