---
title: Handle app resume
description: Learn how to refresh displayed content when the system resumes your app.
ms.assetid: DACCC556-B814-4600-A10A-90B82664EA15
ms.date: 07/06/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs:
- csharp
- vb
- cppwinrt
- cpp
---
# Handle app resume

**Important APIs**

- [**Resuming**](/uwp/api/windows.ui.xaml.application.resuming)

Learn where to refresh your UI when the system resumes your app. The example in this topic registers an event handler for the [**Resuming**](/uwp/api/windows.ui.xaml.application.resuming) event.

## Register the resuming event handler

Register to handle the [**Resuming**](/uwp/api/windows.ui.xaml.application.resuming) event, which indicates that the user switched away from your app and then back to it.

```csharp
partial class MainPage
{
   public MainPage()
   {
      InitializeComponent();
      Application.Current.Resuming += new EventHandler<Object>(App_Resuming);
   }
}
```

```vb
Public NonInheritable Class MainPage

   Public Sub New()
      InitializeComponent()
      AddHandler Application.Current.Resuming, AddressOf App_Resuming
   End Sub

End Class
```

```cppwinrt
MainPage::MainPage()
{
    InitializeComponent();
    Windows::UI::Xaml::Application::Current().Resuming({ this, &MainPage::App_Resuming });
}
```

```cpp
MainPage::MainPage()
{
    InitializeComponent();
    Application::Current->Resuming +=
        ref new EventHandler<Platform::Object^>(this, &MainPage::App_Resuming);
}
```

## Refresh displayed content and reacquire resources

The system suspends your app a few seconds after the user switches to another app or to the desktop. The system resumes your app when the user switches back to it. When the system resumes your app, the content of your variables and data structures are the same as they were before the system suspended the app. The system restores the app where it left off. To the user, it appears as if the app has been running in the background.

When your app handles the [**Resuming**](/uwp/api/windows.ui.xaml.application.resuming) event, your app may be been suspended for hours or days. It should refresh any content that might have become stale while the app was suspended, such as news feeds or the user's location.

This is also a good time to restore any exclusive resources that you released when your app was suspended such as file handles, cameras, I/O devices, external devices, and network resources.

```csharp
partial class MainPage
{
    private void App_Resuming(Object sender, Object e)
    {
        // TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.
    }
}
```

```vb
Public NonInheritable Class MainPage

    Private Sub App_Resuming(sender As Object, e As Object)
 
        ' TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.

    End Sub
>
End Class
```

```cppwinrt
void MainPage::App_Resuming(
    Windows::Foundation::IInspectable const& /* sender */,
    Windows::Foundation::IInspectable const& /* e */)
{
    // TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.
}
```

```cpp
void MainPage::App_Resuming(Object^ sender, Object^ e)
{
    // TODO: Refresh network data, perform UI updates, and reacquire resources like cameras, I/O devices, etc.
}
```

> [!NOTE]
> Because the [**Resuming**](/uwp/api/windows.ui.xaml.application.resuming) event is not raised from the UI thread, a dispatcher must be used in your handler to dispatch any calls to your UI.

## Remarks

When your app is attached to the Visual Studio debugger, it will not be suspended. You can suspend it from the debugger, however, and then send it a **Resume** event so that you can debug your code. Make sure the **Debug Location toolbar** is visible and click the drop-down next to the **Suspend** icon. Then choose **Resume**.

For Windows Phone Store apps, the [**Resuming**](/uwp/api/windows.ui.xaml.application.resuming) event is always followed by [**OnLaunched**](/uwp/api/windows.ui.xaml.application.onlaunched), even when your app is currently suspended and the user re-launches your app from a primary tile or app list. Apps can skip initialization if there is already content set on the current window. You can check the [**LaunchActivatedEventArgs.TileId**](/uwp/api/windows.applicationmodel.activation.launchactivatedeventargs.tileid) property to determine if the app was launched from a primary or a secondary tile and, based on that information, decide whether you should present a fresh or resume app experience.

## Related topics

* [App lifecycle](app-lifecycle.md)
* [Handle app activation](activate-an-app.md)
* [Handle app suspend](suspend-an-app.md)