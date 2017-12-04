---
author: mtoepke
title: How to activate an app (DirectX and C++)
description: This topic shows how to define the activation experience for a Universal Windows Platform (UWP) DirectX app.
ms.assetid: b07c7da1-8a5e-5b57-6f77-6439bf653a53
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, directx, activation
ms.localizationpriority: medium
---

# How to activate an app (DirectX and C++)



This topic shows how to define the activation experience for a Universal Windows Platform (UWP) DirectX app.

## Register the app activation event handler


First, register to handle the [**CoreApplicationView::Activated**](https://msdn.microsoft.com/library/windows/apps/br225018) event, which is raised when your app is started and initialized by the operating system.

Add this code to your implementation of the [**IFrameworkView::Initialize**](https://msdn.microsoft.com/library/windows/apps/hh700495) method of your view provider (named **MyViewProvider** in the example):

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


When your app starts, you must obtain a reference to the [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225) for your app. **CoreWindow** contains the window event message dispatcher that your app uses to process window events. Obtain this reference in your callback for the app activation event by calling [**CoreWindow::GetForCurrentThread**](https://msdn.microsoft.com/library/windows/apps/hh701589). Once you have obtained this reference, activate the main app window by calling [**CoreWindow::Activate**](https://msdn.microsoft.com/library/windows/apps/br208254).

```cpp
void App::OnActivated(CoreApplicationView^ applicationView, IActivatedEventArgs^ args)
{
    // Run() won't start until the CoreWindow is activated.
    CoreWindow::GetForCurrentThread()->Activate();
}
```

## Start processing event message for the main app window


Your callbacks occur as event messages are processed by the [**CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211) for the app's [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225). This callback will not be invoked if you do not call [**CoreDispatcher::ProcessEvents**](https://msdn.microsoft.com/library/windows/apps/br208215) from your app's main loop (implemented in the [**IFrameworkView::Run**](https://msdn.microsoft.com/library/windows/apps/hh700505) method of your view provider).

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

 

 




