---
title: How to resume an app (DirectX and C++)
description: This topic shows how to restore important application data when the system resumes your Universal Windows Platform (UWP) DirectX app.
ms.assetid: 5e6bb673-6874-ace5-05eb-f88c045f2178
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, resuming, directx
ms.localizationpriority: medium
---
# How to resume an app (DirectX and C++)



This topic shows how to restore important application data when the system resumes your Universal Windows Platform (UWP) DirectX app.

## Register the resuming event handler


Register to handle the [**CoreApplication::Resuming**](/uwp/api/windows.applicationmodel.core.coreapplication.resuming) event, which indicates that the user switched away from your app and then back to it.

Add this code to your implementation of the [**IFrameworkView::Initialize**](/uwp/api/windows.applicationmodel.core.iframeworkview.initialize) method of your view provider:

```cpp
// The first method is called when the IFrameworkView is being created.
void App::Initialize(CoreApplicationView^ applicationView)
{
  //...
  
    CoreApplication::Resuming +=
        ref new EventHandler<Platform::Object^>(this, &App::OnResuming);
    
  //...

}
```

## Refresh displayed content after suspension


When your app handles the Resuming event, it has the opportunity to refresh its displayed content. Restore any app you have saved with your handler for [**CoreApplication::Suspending**](/uwp/api/windows.applicationmodel.core.coreapplication.suspending), and restart processing. Game devs: if you've suspended your audio engine, now's the time to restart it.

```cpp
void App::OnResuming(Platform::Object^ sender, Platform::Object^ args)
{
    // Restore any data or state that was unloaded on suspend. By default, data
    // and state are persisted when resuming from suspend. Note that this event
    // does not occur if the app was previously terminated.

    // Insert your code here.
}
```

This callback occurs as an event message processed by the [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) for the app's [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow). This callback will not be invoked if you do not call [**CoreDispatcher::ProcessEvents**](/uwp/api/windows.ui.core.coredispatcher.processevents) from your app's main loop (implemented in the [**IFrameworkView::Run**](/uwp/api/windows.applicationmodel.core.iframeworkview.run) method of your view provider).

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

## Remarks


The system suspends your app whenever the user switches to another app or to the desktop. The system resumes your app whenever the user switches back to it. When the system resumes your app, the content of your variables and data structures is the same as it was before the system suspended the app. The system restores the app exactly where it left off, so that it appears to the user as if it's been running in the background. However, the app may have been suspended for a significant amount of time, so it should refresh any displayed content that might have changed while the app was suspended, and restart any rendering or audio processing threads. If you've saved any game state data during a previous suspend event, restore it now.

## Related topics

* [How to suspend an app (DirectX and C++)](how-to-suspend-an-app-directx-and-cpp.md)
* [How to activate an app (DirectX and C++)](how-to-activate-an-app-directx-and-cpp.md)

 

 