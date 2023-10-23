---
title: Hello Win2D World
description: Display 'Hello world' using Win2D.
ms.date: 05/25/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games
ms.localizationpriority: medium
---

# Hello Win2D World

Launch Visual Studio, and create a new project:
- Go to 'File' -> 'New' -> 'Project...'
- Select "Visual C#", "Visual C++", or "Visual Basic"
- Create a 'Blank App (Universal Windows)' (or WinUI 3)
- Enter a project name of your choosing
- Click 'OK'

Add a `CanvasControl` to your XAML page to host the Win2D view.

* Double-click on MainWindow.xaml in Solution Explorer to open the XAML editor
* Add the **Microsoft.Graphics.Canvas.UI.Xaml** namespace next to the existing xmlns statements:

```XAML
xmlns:canvas="using:Microsoft.Graphics.Canvas.UI.Xaml"
```

Next, add a `CanvasControl` inside the existing Grid control:

```XAML
<Grid>
    <canvas:CanvasControl Draw="CanvasControl_Draw" ClearColor="CornflowerBlue"/>
</Grid>
```

Next, we need to add some drawing code to interact with the `CanvasControl`.

If you created a C# project, edit `MainPage.xaml.cs`:

```cs
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.UI.Xaml;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
    {
        args.DrawingSession.DrawEllipse(155, 115, 80, 30, Colors.Black, 3);
        args.DrawingSession.DrawText("Hello, Win2D world!", 100, 100, Colors.Yellow);
    }
}
```

If you created a C++/WinRT project, edit `MainPage.xaml.h` and add a function declaration to the `MainPage` class:

```cpp
void CanvasControl_Draw(
    Microsoft::Graphics::Canvas::UI::Xaml::CanvasControl const& sender,
    Microsoft::Graphics::Canvas::UI::Xaml::CanvasDrawEventArgs const& args);
```

Then edit `MainPage.xaml.cpp`:

```cpp
#include "pch.h"
#include "MainPage.xaml.h"
#include "MainPage.g.cpp"

using namespace winrt;
using namespace Microsoft::UI;
using namespace Microsoft::Graphics::Canvas::UI::Xaml;

namespace winrt::App1::implementation
{
    void MainPage::CanvasControl_Draw(CanvasControl const& sender, CanvasDrawEventArgs const& args)
    {
        args.DrawingSession().DrawEllipse(155, 115, 80, 30, Colors::Black(), 3);
        args.DrawingSession().DrawText(L"Hello, world!", 100, 100, Colors::Yellow());
    }
}
```

Press **F5** to launch and run the project.

> [!NOTE]
> This sample is using WinUI 3 namespaces (`Microsoft.UI.*`), but the same code can be used on UWP as well, by just changing the namespaces to be `Windows.UI.*` instead). Win2D exposes the same APIs on both frameworks.

## See Also

* Win2D [Quick start](./quick-start.md)
