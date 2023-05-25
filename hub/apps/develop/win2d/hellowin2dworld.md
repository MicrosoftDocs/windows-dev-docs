---
title: Hello Win2D World
description: Display 'Hello world' using Win2D.
ms.date: 05/25/2023
ms.topic: article
keywords: windows 10, windows 11, windows app sdk, winui, windows ui, graphics, games
ms.localizationpriority: medium
---

# Hello Win2D World

Launch Visual Studio, and create a new project.

Add a CanvasControl to your XAML page to host the Win2D view.

* Double-click on MainWindow.xaml in Solution Explorer to open the XAML editor
* Add the **Microsoft.Graphics.Canvas.UI.Xaml** namespace next to the existing xmlns statements:

```xmls
xmlns:canvas="using:Microsoft.Graphics.Canvas.UI.Xaml"
```

Next, add a CanvasControl inside the existing Grid control:

```xmls
<Grid>
    <canvas:CanvasControl Draw="CanvasControl_Draw" ClearColor="CornflowerBlue"/>
</Grid>
```

Now add some Win2D drawing code. Edit **MainWindow.xaml.cs**, and add:


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

Press **F5** to launch and run the project.

## See Also

* Win2D [Quick start](https://microsoft.github.io/Win2D/WinUI3/html/QuickStart.htm)
