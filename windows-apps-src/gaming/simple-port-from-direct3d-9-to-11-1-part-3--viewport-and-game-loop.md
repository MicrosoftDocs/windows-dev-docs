---
title: Port the game loop
description: Shows how to implement a window for a Universal Windows Platform (UWP) game and how to bring over the game loop, including how to build an IFrameworkView to control a full-screen CoreWindow.
ms.assetid: 070dd802-cb27-4672-12ba-a7f036ff495c
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, porting, game loop, direct3d 9, directx 11
ms.localizationpriority: medium
---
# Port the game loop



**Summary**

-   [Part 1: Initialize Direct3D 11](simple-port-from-direct3d-9-to-11-1-part-1--initializing-direct3d.md)
-   [Part 2: Convert the rendering framework](simple-port-from-direct3d-9-to-11-1-part-2--rendering.md)
-   Part 3: Port the game loop


Shows how to implement a window for a Universal Windows Platform (UWP) game and how to bring over the game loop, including how to build an [**IFrameworkView**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkView) to control a full-screen [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow). Part 3 of the [Port a simple Direct3D 9 app to DirectX 11 and UWP](walkthrough--simple-port-from-direct3d-9-to-11-1.md) walkthrough.

## Create a window


To set up a desktop window with a Direct3D 9 viewport, we had to implement the traditional windowing framework for desktop apps. We had to create an HWND, set the window size, provide a window processing callback, make it visible, and so on.

The UWP environment has a much simpler system. Instead of setting up a traditional window, a Microsoft Store game using DirectX implements [**IFrameworkView**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkView). This interface exists for DirectX apps and games to run directly in a [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) inside the app container.

> **Note**   Windows supplies managed pointers to resources such as the source application object and the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow). See [**Handle to Object Operator (^)**](/cpp/extensions/handle-to-object-operator-hat-cpp-component-extensions).

 

Your "main" class needs to inherit from [**IFrameworkView**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkView) and implement the five **IFrameworkView** methods: [**Initialize**](/uwp/api/windows.applicationmodel.core.iframeworkview.initialize), [**SetWindow**](/uwp/api/windows.applicationmodel.core.iframeworkview.setwindow), [**Load**](/uwp/api/windows.applicationmodel.core.iframeworkview.load), [**Run**](/uwp/api/windows.applicationmodel.core.iframeworkview.run), and [**Uninitialize**](/uwp/api/windows.applicationmodel.core.iframeworkview.uninitialize). In addition to creating the **IFrameworkView**, which is (essentially) where your game will reside, you need to implement a factory class that creates an instance of your **IFrameworkView**. Your game still has an executable with a method called **main()**, but all main can do is use the factory to create the **IFrameworkView** instance.

Main function

```cpp
//-----------------------------------------------------------------------------
// Required method for a DirectX-only app.
// The main function is only used to initialize the app's IFrameworkView class.
//-----------------------------------------------------------------------------
[Platform::MTAThread]
int main(Platform::Array<Platform::String^>^)
{
    auto direct3DApplicationSource = ref new Direct3DApplicationSource();
    CoreApplication::Run(direct3DApplicationSource);
    return 0;
}
```

IFrameworkView factory

```cpp
//-----------------------------------------------------------------------------
// This class creates our IFrameworkView.
//-----------------------------------------------------------------------------
ref class Direct3DApplicationSource sealed : 
    Windows::ApplicationModel::Core::IFrameworkViewSource
{
public:
    virtual Windows::ApplicationModel::Core::IFrameworkView^ CreateView()
    {
        return ref new Cube11();
    };
};
```

## Port the game loop


Let's look at the game loop from our Direct3D 9 implementation. This code exists in the app's main function. Each iteration of this loop processes a window message or renders a frame.

Game loop in Direct3D 9 desktop game

```cpp
while(WM_QUIT != msg.message)
{
    // Process window events.
    // Use PeekMessage() so we can use idle time to render the scene. 
    bGotMsg = (PeekMessage(&msg, NULL, 0U, 0U, PM_REMOVE) != 0);

    if(bGotMsg)
    {
        // Translate and dispatch the message
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }
    else
    {
        // Render a new frame.
        // Render frames during idle time (when no messages are waiting).
        RenderFrame();
    }
}
```

The game loop is similar - but easier - in the UWP version of our game:

The game loop goes in the [**IFrameworkView::Run**](/uwp/api/windows.applicationmodel.core.iframeworkview.run) method (instead of **main()**) because our game functions within the [**IFrameworkView**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkView) class.

Instead of implementing a message handling framework and calling [**PeekMessage**](/windows/desktop/api/winuser/nf-winuser-peekmessagea), we can call the [**ProcessEvents**](/uwp/api/windows.ui.core.coredispatcher.processevents) method built in to our app window's [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher). There's no need for the game loop to branch and handle messages - just call **ProcessEvents** and proceed.

Game loop in Direct3D 11 Microsoft Store game

```cpp
// UWP apps should not exit. Use app lifecycle events instead.
while (true)
{
    // Process window events.
    auto dispatcher = CoreWindow::GetForCurrentThread()->Dispatcher;
    dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);

    // Render a new frame.
    RenderFrame();
}
```

Now we have a UWP app that sets up the same basic graphics infrastructure, and renders the same colorful cube, as our DirectX 9 example.

## Where do I go from here?


Bookmark the [DirectX 11 porting FAQ](directx-porting-faq.md).

The DirectX UWP templates include a robust Direct3D device infrastructure that's ready for use with your game. See [Create a DirectX game project from a template](user-interface.md) for guidance on picking the right template.

Visit the following in-depth Microsoft Store game development articles:

-   [Walkthrough: a simple UWP game with DirectX](tutorial--create-your-first-uwp-directx-game.md)
-   [Audio for games](working-with-audio-in-your-directx-game.md)
-   [Move-look controls for games](tutorial--adding-move-look-controls-to-your-directx-game.md)

 

 