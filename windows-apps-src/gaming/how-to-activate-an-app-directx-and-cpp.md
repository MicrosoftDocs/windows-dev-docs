---
title: How to activate an app (DirectX and C++)
description: This topic shows how to define the activation experience for a Universal Windows Platform (UWP) DirectX app.
ms.assetid: b07c7da1-8a5e-5b57-6f77-6439bf653a53
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, activation
ms.localizationpriority: medium
---
# How to activate an app (DirectX and C++)



This topic shows how to define the activation experience for a Universal Windows Platform (UWP) DirectX app.

## Register the app activation event handler


First, register to handle the [**CoreApplicationView::Activated**](/uwp/api/windows.applicationmodel.core.coreapplicationview.activated) event, which is raised when your app is started and initialized by the operating system.

Add this code to your implementation of the [**IFrameworkView::Initialize**](/uwp/api/windows.applicationmodel.core.iframeworkview.initialize) method of your view provider (named **MyViewProvider** in the example):

```cpp
void App::Initialize(CoreApplicationView^ applicationView)
{
    // Register event handlers for the app lifecycle. This example includes Activated, so that we
    // can make the CoreWindow active and start rendering on the window.
    applicationView->Activated +=
        ref new TypedEventHandler<CoreApplicationView^, IActivatedEventArgs^>(this, &App::OnActivated);
  
  //...

}
```

## Activate the CoreWindow instance for the app


When your app starts, you must obtain a reference to the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) for your app. **CoreWindow** contains the window event message dispatcher that your app uses to process window events. Obtain this reference in your callback for the app activation event by calling [**CoreWindow::GetForCurrentThread**](/uwp/api/windows.ui.core.corewindow.getforcurrentthread). Once you have obtained this reference, activate the main app window by calling [**CoreWindow::Activate**](/uwp/api/windows.ui.core.corewindow.activate).

```cpp
void App::OnActivated(CoreApplicationView^ applicationView, IActivatedEventArgs^ args)
{
    // Run() won't start until the CoreWindow is activated.
    CoreWindow::GetForCurrentThread()->Activate();
}
```

## Start processing event message for the main app window


Your callbacks occur as event messages are processed by the [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) for the app's [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow). This callback will not be invoked if you do not call [**CoreDispatcher::ProcessEvents**](/uwp/api/windows.ui.core.coredispatcher.processevents) from your app's main loop (implemented in the [**IFrameworkView::Run**](/uwp/api/windows.applicationmodel.core.iframeworkview.run) method of your view provider).

``` syntax
// This method is called after the window becomes active.
void App::Run()
{
    while (!m_windowClosed)
    {
        if (m_windowVisible)
        {
            CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);

            m_main->Update();

            if (m_main->Render())
            {
                m_deviceResources->Present();
            }
        }
        else
        {
            CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessOneAndAllPending);
        }
    }
}
```

## Related topics


* [How to suspend an app (DirectX and C++)](how-to-suspend-an-app-directx-and-cpp.md)
* [How to resume an app (DirectX and C++)](how-to-resume-an-app-directx-and-cpp.md)

 

 