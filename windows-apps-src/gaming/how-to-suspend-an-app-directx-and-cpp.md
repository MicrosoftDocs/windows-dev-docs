---
title: How to suspend an app (DirectX and C++)
description: This topic shows how to save important system state and app data when the system suspends your Universal Windows Platform (UWP) DirectX app.
ms.assetid: 5dd435e5-ec7e-9445-fed4-9c0d872a239e
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, suspend, directx
ms.localizationpriority: medium
---
# How to suspend an app (DirectX and C++)



This topic shows how to save important system state and app data when the system suspends your Universal Windows Platform (UWP) DirectX app.

## Register the suspending event handler


First, register to handle the [**CoreApplication::Suspending**](/uwp/api/windows.applicationmodel.core.coreapplication.suspending) event, which is raised when your app is moved to a suspended state by a user or system action.

Add this code to your implementation of the [**IFrameworkView::Initialize**](/uwp/api/windows.applicationmodel.core.iframeworkview.initialize) method of your view provider:

```cpp
void App::Initialize(CoreApplicationView^ applicationView)
{
  //...
  
    CoreApplication::Suspending +=
        ref new EventHandler<SuspendingEventArgs^>(this, &App::OnSuspending);

  //...
}
```

## Save any app data before suspending


When your app handles the [**CoreApplication::Suspending**](/uwp/api/windows.applicationmodel.core.coreapplication.suspending) event, it has the opportunity to save its important application data in the handler function. The app should use the [**LocalSettings**](/uwp/api/windows.storage.applicationdata.localsettings) storage API to save simple application data synchronously. If you are developing a game, save any critical game state information. Don't forget to suspend the audio processing!

Now, implement the callback. Save the app data in this method.

```cpp
void App::OnSuspending(Platform::Object^ sender, SuspendingEventArgs^ args)
{
    // Save app state asynchronously after requesting a deferral. Holding a deferral
    // indicates that the application is busy performing suspending operations. Be
    // aware that a deferral may not be held indefinitely. After about five seconds,
    // the app will be forced to exit.
    SuspendingDeferral^ deferral = args->SuspendingOperation->GetDeferral();

    create_task([this, deferral]()
    {
        m_deviceResources->Trim();

        // Insert your code here.

        deferral->Complete();
    });
}
```

This callback must complete with 5 seconds. During this callback, you must request a deferral by calling [**SuspendingOperation::GetDeferral**](/uwp/api/windows.applicationmodel.suspendingoperation.getdeferral), which starts the countdown. When your app completes the save operation, call [**SuspendingDeferral::Complete**](/uwp/api/windows.applicationmodel.suspendingdeferral.complete) to tell the system that your app is now ready to be suspended. If you do not request a deferral, or if your app takes longer than 5 seconds to save the data, your app is automatically suspended.

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

## Call Trim()


Starting in Windows 8.1, all DirectX UWP apps must call [**IDXGIDevice3::Trim**](/windows/desktop/api/dxgi1_3/nf-dxgi1_3-idxgidevice3-trim) when suspending. This call tells the graphics driver to release all temporary buffers allocated for the app, which reduces the chance that the app will be terminated to reclaim memory resources while in the suspend state. This is a certification requirement for Windows 8.1.

```cpp
void App::OnSuspending(Platform::Object^ sender, SuspendingEventArgs^ args)
{
    // Save app state asynchronously after requesting a deferral. Holding a deferral
    // indicates that the application is busy performing suspending operations. Be
    // aware that a deferral may not be held indefinitely. After about five seconds,
    // the app will be forced to exit.
    SuspendingDeferral^ deferral = args->SuspendingOperation->GetDeferral();

    create_task([this, deferral]()
    {
        m_deviceResources->Trim();

        // Insert your code here.

        deferral->Complete();
    });
}

// Call this method when the app suspends. It provides a hint to the driver that the app 
// is entering an idle state and that temporary buffers can be reclaimed for use by other apps.
void DX::DeviceResources::Trim()
{
    ComPtr<IDXGIDevice3> dxgiDevice;
    m_d3dDevice.As(&dxgiDevice);

    dxgiDevice->Trim();
}
```

## Release any exclusive resources and file handles


When your app handles the [**CoreApplication::Suspending**](/uwp/api/windows.applicationmodel.core.coreapplication.suspending) event, it also has the opportunity to release exclusive resources and file handles. Explicitly releasing exclusive resources and file handles helps to ensure that other apps can access them while your app isn't using them. When the app is activated after termination, it should open its exclusive resources and file handles.

## Remarks


The system suspends your app whenever the user switches to another app or to the desktop. The system resumes your app whenever the user switches back to it. When the system resumes your app, the content of your variables and data structures is the same as it was before the system suspended the app. The system restores the app exactly where it left off, so that it appears to the user as if it's been running in the background.

The system attempts to keep your app and its data in memory while it's suspended. However, if the system does not have the resources to keep your app in memory, the system will terminate your app. When the user switches back to a suspended app that has been terminated, the system sends an [**Activated**](/uwp/api/windows.applicationmodel.core.coreapplicationview.activated) event and should restore its application data in its handler for the **CoreApplicationView::Activated** event.

The system doesn't notify an app when it's terminated, so your app must save its application data and release exclusive resources and file handles when it's suspended, and restore them when the app is activated after termination.

## Related topics

* [How to resume an app (DirectX and C++)](how-to-resume-an-app-directx-and-cpp.md)
* [How to activate an app (DirectX and C++)](how-to-activate-an-app-directx-and-cpp.md)

 

 