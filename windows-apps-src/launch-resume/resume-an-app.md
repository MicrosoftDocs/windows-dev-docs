---
author: TylerMSFT
title: Handle app resume
description: Learn how to refresh displayed content when the system resumes your app.
ms.assetid: DACCC556-B814-4600-A10A-90B82664EA15
ms.author: twhitney
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Handle app resume


**Important APIs**

-   [**Resuming**](https://msdn.microsoft.com/library/windows/apps/br242339)

Learn where to refresh your UI when the system resumes your app. The example in this topic registers an event handler for the [**Resuming**](https://msdn.microsoft.com/library/windows/apps/br242339) event.

## Register the resuming event handler

Register to handle the [**Resuming**](https://msdn.microsoft.com/library/windows/apps/br242339) event, which indicates that the user switched away from your app and then back to it.

> [!div class="tabbedCodeSnippets"]
> ```cs
> partial class MainPage
> {
>    public MainPage()
>    {
>       InitializeComponent();
>       Application.Current.Resuming += new EventHandler<Object>(App_Resuming);
>    }
> }
> ```
> ```vb
> Public NonInheritable Class MainPage
>
>    Public Sub New()
>       InitializeComponent()
>       AddHandler Application.Current.Resuming, AddressOf App_Resuming
>    End Sub
>
> End Class
> ```
> ```cpp
> MainPage::MainPage()
> {
>     InitializeComponent();
>     Application::Current->Resuming +=
>         ref new EventHandler<Platform::Object^>(this, &MainPage::App_Resuming);
> }
> ```

## Refresh displayed content and reacquire resources

The system suspends your app a few seconds after the user switches to another app or to the desktop. The system resumes your app when the user switches back to it. When the system resumes your app, the content of your variables and data structures are the same as they were before the system suspended the app. The system restores the app where it left off. To the user, it appears as if the app has been running in the background.

When your app handles the [**Resuming**](https://msdn.microsoft.com/library/windows/apps/br242339) event, your app may be been suspended for hours or days. It should refresh any content that might have become stale while the app was suspended, such as news feeds or the user's location.

This is also a good time to restore any exclusive resources that you released when your app was suspended such as file handles, cameras, I/O devices, external devices, and network resources.

> [!div class="tabbedCodeSnippets"]
> ```cs
> partial class MainPage
> {
>     private void App_Resuming(Object sender, Object e)
>     {
>         // TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.
>     }
> }
> ```
> ```vb
> Public NonInheritable Class MainPage
>
>     Private Sub App_Resuming(sender As Object, e As Object)
>  
>         ' TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.
>
>     End Sub
>
> End Class
> ```
> ```cpp
> void MainPage::App_Resuming(Object^ sender, Object^ e)
> {
>     // TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.
> }
> ```

> **Note**  Because the [**Resuming**](https://msdn.microsoft.com/library/windows/apps/br242339) event is not raised from the UI thread, a dispatcher must be used in your handler to dispatch any calls to your UI.

## Remarks

When your app is attached to the Visual Studio debugger, it will not be suspended. You can suspend it from the debugger, however, and then send it a **Resume** event so that you can debug your code. Make sure the **Debug Location toolbar** is visible and click the drop-down next to the **Suspend** icon. Then choose **Resume**.

For Windows Phone Store apps, the [**Resuming**](https://msdn.microsoft.com/library/windows/apps/br242339) event is always followed by [**OnLaunched**](https://msdn.microsoft.com/library/windows/apps/br242335), even when your app is currently suspended and the user re-launches your app from a primary tile or app list. Apps can skip initialization if there is already content set on the current window. You can check the [**LaunchActivatedEventArgs.TileId**](https://msdn.microsoft.com/library/windows/apps/br224736) property to determine if the app was launched from a primary or a secondary tile and, based on that information, decide whether you should present a fresh or resume app experience.

## Related topics

* [App lifecycle](app-lifecycle.md)
* [Handle app activation](activate-an-app.md)
* [Handle app suspend](suspend-an-app.md)
