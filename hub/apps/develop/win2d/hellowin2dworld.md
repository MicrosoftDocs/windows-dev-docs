---
title: Win2D "Hello, World!" quickstart
description: "This topic shows you how to create a very simple \"Hello, World!\" project for Win2D."
ms.date: 10/25/2023
ms.topic: article
keywords: windows 11, windows 10, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, win2d
ms.localizationpriority: medium
---

# Win2D "Hello, World!" quickstart

In this topic you'll create a very simple "Hello, World!" project for Win2D.

In Visual Studio, create a new project from one of the following project templates:

* **WinUI 3 (Windows App SDK)**. To create a new WinUI 3 project, use the **Blank App, Packaged (WinUI 3 in Desktop)** project template. You can find that project template by choosing language: either *C#* or *C++*; platform: *Windows*; project type: *Desktop*.
* **Universal Windows Platform (UWP)**. To create a new UWP project, use the **Blank App (Universal Windows)** or **Blank App (C++/WinRT)** or **Blank App (Universal Windows - C++/CX)** project template. For language, choose: either *C#* or *C++*; platform: *Windows*; project type: *UWP*.

> [!IMPORTANT]
> For info about how to set up your project to use Win2D, see [Overview of Win2D](./index.md).

To host Win2D content, you'll need to add a Win2D **CanvasControl** to your project's `MainWindow.xaml` (or `MainPage.xaml`, for a UWP project).

First, add the following xml namespace declaration:

```xaml
xmlns:win2dcanvas="using:Microsoft.Graphics.Canvas.UI.Xaml"
```

And then add a **CanvasControl**, prefixed with that xml namespace. For example, you could add a **Grid** as your layout root, like this:

```xaml
<Grid>
    <win2dcanvas:CanvasControl Draw="CanvasControl_Draw" ClearColor="CornflowerBlue"/>
</Grid>
```

The project won't build at the moment, due to the referenced-but-not-implemented **Draw** event handler. So we'll remedy that next, while we add some drawing code to interact with the **CanvasControl**.

## For a WinUI 3 (Windows App SDK) project

For a C# project, add the following event handler to `MainWindow.xaml.cs`:

```csharp
// MainWindow.xaml.cs
...
public sealed partial class MainWindow : Window
{
    ...
    void CanvasControl_Draw(
        Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender,
        Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
    {
        args.DrawingSession.DrawEllipse(155, 115, 80, 30, Microsoft.UI.Colors.Black, 3);
        args.DrawingSession.DrawText("Hello, Win2D World!", 100, 100, Microsoft.UI.Colors.Yellow);
    }
}
...
```

For a C++/WinRT project, add the following event handler to `MainWindow.xaml.h` and `MainWindow.xaml.cpp`:

```cppwinrt
// MainWindow.xaml.h
...
void CanvasControl_Draw(
    winrt::Microsoft::Graphics::Canvas::UI::Xaml::CanvasControl const& sender,
    winrt::Microsoft::Graphics::Canvas::UI::Xaml::CanvasDrawEventArgs const& args);
...

// MainWindow.xaml.cpp
...
namespace winrt::MYPROJECT::implementation
{
    ...
    void MainWindow::CanvasControl_Draw(
        winrt::Microsoft::Graphics::Canvas::UI::Xaml::CanvasControl const& sender,
        winrt::Microsoft::Graphics::Canvas::UI::Xaml::CanvasDrawEventArgs const& args)
    {
        args.DrawingSession().DrawEllipse(155, 115, 80, 30, winrt::Microsoft::UI::Colors::Black(), 3);
        args.DrawingSession().DrawText(L"Hello, Win2D World!", 100, 100, winrt::Microsoft::UI::Colors::Yellow());
    }
}
```

You can now build and run the project. You'll see some Win2D content&mdash;a black ellipse with a yellow "Hello, World!" message in front of it.

## For a UWP project

For a C# project, you can use the same C# code as for a WinUI 3 project (see the [For a WinUI 3 project](#for-a-winui-3-windows-app-sdk-project) section above). The only differences are that you'll be editing `MainPage.xaml.cs` instead of `MainWindow.xaml.cs`. And you'll need to change `Microsoft.UI.Colors` to `Windows.UI.Colors`.

For a C++/WinRT project, add the following code to `pch.h`, `MainPage.h`, and `MainPage.cpp`:

```cppwinrt
// pch.h
...
#include <winrt/Microsoft.Graphics.Canvas.UI.Xaml.h>

// MainPage.h
...
void CanvasControl_Draw(
    winrt::Microsoft::Graphics::Canvas::UI::Xaml::CanvasControl const& sender,
    winrt::Microsoft::Graphics::Canvas::UI::Xaml::CanvasDrawEventArgs const& args);
...

// MainPage.cpp
...
namespace winrt::MYPROJECT::implementation
{
    ...
    void MainPage::CanvasControl_Draw(
        winrt::Microsoft::Graphics::Canvas::UI::Xaml::CanvasControl const& sender,
        winrt::Microsoft::Graphics::Canvas::UI::Xaml::CanvasDrawEventArgs const& args)
    {
        args.DrawingSession().DrawEllipse(155, 115, 80, 30, winrt::Windows::UI::Colors::Black(), 3);
        args.DrawingSession().DrawText(L"Hello, Win2D World!", 100, 100, winrt::Windows::UI::Colors::Yellow());
    }
}
```

For a C++/CX project, add the following event handler to `MainPage.xaml.h` and `MainPage.xaml.cpp`:

```cppcx
// MainPage.xaml.h
...
void CanvasControl_Draw(
	Microsoft::Graphics::Canvas::UI::Xaml::CanvasControl^ sender,
	Microsoft::Graphics::Canvas::UI::Xaml::CanvasDrawEventArgs^ args);
...

// MainWindow.xaml.cpp
...
void MainPage::CanvasControl_Draw(
    Microsoft::Graphics::Canvas::UI::Xaml::CanvasControl^ sender,
    Microsoft::Graphics::Canvas::UI::Xaml::CanvasDrawEventArgs^ args)
{
    args->DrawingSession->DrawEllipse(155, 115, 80, 30, Windows::UI::Colors::Black, 3);
    args->DrawingSession->DrawText("Hello, Win2D World!", 100, 100, Windows::UI::Colors::Yellow);
}
```

## See Also

* [Overview of Win2D](./index.md)
* [Win2D quickstart](./quick-start.md)
