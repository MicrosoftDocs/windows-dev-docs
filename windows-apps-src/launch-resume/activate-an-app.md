---
author: TylerMSFT
title: Handle app activation
description: Learn how to handle app activation by overriding the OnLaunched method.
ms.assetid: DA9A6A43-F09D-4512-A2AB-9B6132431007
ms.author: twhitney
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Handle app activation




Learn how to handle app activation by overriding the [**OnLaunched**](https://msdn.microsoft.com/library/windows/apps/br242335) method..

## Override the launch handler

When an app is activated, for any reason, the system sends the [**Activated**](https://msdn.microsoft.com/library/windows/apps/br225018) event. For a list of activation types, see the [**ActivationKind**](https://msdn.microsoft.com/library/windows/apps/br224693) enumeration.

The [**Windows.UI.Xaml.Application**](https://msdn.microsoft.com/library/windows/apps/br242324) class defines methods you can override to handle the various activation types. Several of the activation types have a specific method that you can override. For the other activation types, override the [**OnActivated**](https://msdn.microsoft.com/library/windows/apps/br242330) method.

Define the class for your application.

```xml
<Application xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="AppName.App" >
```

Override the [**OnLaunched**](https://msdn.microsoft.com/library/windows/apps/br242335) method. This method is called whenever the user launches the app. The [**LaunchActivatedEventArgs**](https://msdn.microsoft.com/library/windows/apps/br224731) parameter contains the previous state of your app and the activation arguments.

**Note**  For Windows Phone Store apps, this method is called each time the user launches the app from Start tile or app list, even when the app is currently suspended in memory. On Windows, launching a suspended app from Start tile or app list doesn’t call this method.

> [!div class="tabbedCodeSnippets"]
> ```cs
> using System;
> using Windows.ApplicationModel.Activation;
> using Windows.UI.Xaml;
>
> namespace AppName
> {
>    public partial class App
>    {
>       async protected override void OnLaunched(LaunchActivatedEventArgs args)
>       {
>          EnsurePageCreatedAndActivate();
>       }
>
>       // Creates the MainPage if it isn't already created.  Also activates
>       // the window so it takes foreground and input focus.
>       private MainPage EnsurePageCreatedAndActivate()
>       {
>          if (Window.Current.Content == null)
>          {
>              Window.Current.Content = new MainPage();
>          }
>
>          Window.Current.Activate();
>          return Window.Current.Content as MainPage;
>       }
>    }
> }
> ```
> ```vb
> Class App
>    Protected Overrides Sub OnLaunched(args As LaunchActivatedEventArgs)
>       Window.Current.Content = New MainPage()
>       Window.Current.Activate()
>    End Sub
> End Class
> ```
> ```cpp
> using namespace Windows::ApplicationModel::Activation;
> using namespace Windows::Foundation;
> using namespace Windows::UI::Xaml;
> using namespace AppName;
> void App::OnLaunched(LaunchActivatedEventArgs^ args)
> {
>    EnsurePageCreatedAndActivate();
> }
>
> // Creates the MainPage if it isn't already created.  Also activates
> // the window so it takes foreground and input focus.
> void App::EnsurePageCreatedAndActivate()
> {
>     if (_mainPage == nullptr)
>     {
>         // Save the MainPage for use if we get activated later
>         _mainPage = ref new MainPage();
>     }
>     Window::Current->Content = _mainPage;
>     Window::Current->Activate();
> }
> ```

## Restore application data if app was suspended then terminated


When the user switches to your terminated app, the system sends the [**Activated**](https://msdn.microsoft.com/library/windows/apps/br225018) event, with [**Kind**](https://msdn.microsoft.com/library/windows/apps/br224728) set to **Launch** and [**PreviousExecutionState**](https://msdn.microsoft.com/library/windows/apps/br224729) set to **Terminated** or **ClosedByUser**. The app should load its saved application data and refresh its displayed content.

> [!div class="tabbedCodeSnippets"]
> ```cs
> async protected override void OnLaunched(LaunchActivatedEventArgs args)
> {
>    if (args.PreviousExecutionState == ApplicationExecutionState.Terminated ||
>        args.PreviousExecutionState == ApplicationExecutionState.ClosedByUser)
>    {
>       // TODO: Populate the UI with the previously saved application data
>    }
>    else
>    {
>       // TODO: Populate the UI with defaults
>    }
>
>    EnsurePageCreatedAndActivate();
> }
> ```
> ```vb
> Protected Overrides Sub OnLaunched(args As Windows.ApplicationModel.Activation.LaunchActivatedEventArgs)
>    Dim restoreState As Boolean = False
>
>    Select Case args.PreviousExecutionState
>       Case ApplicationExecutionState.Terminated
>          ' TODO: Populate the UI with the previously saved application data
>          restoreState = True
>       Case ApplicationExecutionState.ClosedByUser
>          ' TODO: Populate the UI with the previously saved application data
>          restoreState = True
>       Case Else
>          ' TODO: Populate the UI with defaults
>    End Select
>
>    Window.Current.Content = New MainPage(restoreState)
>    Window.Current.Activate()
> End Sub
> ```
> ```cpp
> void App::OnLaunched(Windows::ApplicationModel::Activation::LaunchActivatedEventArgs^ args)
> {
>    if (args->PreviousExecutionState == ApplicationExecutionState::Terminated ||
>        args->PreviousExecutionState == ApplicationExecutionState::ClosedByUser)
>    {
>       // TODO: Populate the UI with the previously saved application data
>    }
>    else
>    {
>       // TODO: Populate the UI with defaults
>    }
>
>    EnsurePageCreatedAndActivate();
> }
> ```

If the value of [**PreviousExecutionState**](https://msdn.microsoft.com/library/windows/apps/br224729) is **NotRunning**, the app failed to save its application data successfully and the app should start over as if it were being initially launched.

## Remarks

> **Note**  For Windows Phone Store apps, the [**Resuming**](https://msdn.microsoft.com/library/windows/apps/br242339) event is always followed by [**OnLaunched**](https://msdn.microsoft.com/library/windows/apps/br242335), even when your app is currently suspended and the user re-launches your app from a primary tile or app list. Apps can skip initialization if there is already content set on the current window. You can check the [**LaunchActivatedEventArgs.TileId**](https://msdn.microsoft.com/library/windows/apps/br224736) property to determine if the app was launched from a primary or a secondary tile and, based on that information, decide whether you should present a fresh or resume app experience.

## Related topics

* [Handle app suspend](suspend-an-app.md)
* [Handle app resume](resume-an-app.md)
* [Guidelines for app suspend and resume](https://msdn.microsoft.com/library/windows/apps/hh465088)
* [App lifecycle](app-lifecycle.md)

**Reference**

* [**Windows.ApplicationModel.Activation**](https://msdn.microsoft.com/library/windows/apps/br224766)
* [**Windows.UI.Xaml.Application**](https://msdn.microsoft.com/library/windows/apps/br242324)

 

 
