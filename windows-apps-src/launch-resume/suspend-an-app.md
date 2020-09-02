---
title: Handle app suspend
description: Learn how to save important application data when the system suspends your app.
ms.assetid: F84F1512-24B9-45EC-BF23-A09E0AC985B0
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
# Handle app suspend

**Important APIs**

- [**Suspending**](/uwp/api/windows.ui.xaml.application.suspending)

Learn how to save important application data when the system suspends your app. The example registers an event handler for the [**Suspending**](/uwp/api/windows.ui.xaml.application.suspending) event and saves a string to a file.

## Register the suspending event handler

Register to handle the [**Suspending**](/uwp/api/windows.ui.xaml.application.suspending) event, which indicates that your app should save its application data before the system suspends it.

```csharp
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

partial class MainPage
{
   public MainPage()
   {
      InitializeComponent();
      Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
   }
}
```

```vb
Public NotInheritable Class MainPage

   Public Sub New()
      InitializeComponent()
      AddHandler Application.Current.Suspending, AddressOf App_Suspending
   End Sub
   
End Class
```

```cppwinrt
MainPage::MainPage()
{
    InitializeComponent();
    Windows::UI::Xaml::Application::Current().Suspending({ this, &MainPage::App_Suspending });
}
```

```cpp
using namespace Windows::ApplicationModel;
using namespace Windows::ApplicationModel::Activation;
using namespace Windows::Foundation;
using namespace Windows::UI::Xaml;
using namespace AppName;

MainPage::MainPage()
{
   InitializeComponent();
   Application::Current->Suspending +=
       ref new SuspendingEventHandler(this, &MainPage::App_Suspending);
}
```

## Save application data before suspension

When your app handles the [**Suspending**](/uwp/api/windows.ui.xaml.application.suspending) event, it has the opportunity to save its important application data in the handler function. The app should use the [**LocalSettings**](/uwp/api/windows.storage.applicationdata.localsettings) storage API to save simple application data synchronously.

```csharp
partial class MainPage
{
    async void App_Suspending(
        Object sender,
        Windows.ApplicationModel.SuspendingEventArgs e)
    {
        // TODO: This is the time to save app data in case the process is terminated.
    }
}
```

```vb
Public NonInheritable Class MainPage

    Private Sub App_Suspending(
        sender As Object,
        e As Windows.ApplicationModel.SuspendingEventArgs) Handles OnSuspendEvent.Suspending

        ' TODO: This is the time to save app data in case the process is terminated.
    End Sub

End Class
```

```cppwinrt
void MainPage::App_Suspending(
    Windows::Foundation::IInspectable const& /* sender */,
    Windows::ApplicationModel::SuspendingEventArgs const& /* e */)
{
    // TODO: This is the time to save app data in case the process is terminated.
}
```

```cpp
void MainPage::App_Suspending(Object^ sender, SuspendingEventArgs^ e)
{
    // TODO: This is the time to save app data in case the process is terminated.
}
```

## Release resources

You should release exclusive resources and file handles so that other apps can access them while your app is suspended. Examples of exclusive resources include cameras, I/O devices, external devices, and network resources. Explicitly releasing exclusive resources and file handles helps to ensure that other apps can access them while your app is suspended. When the app is resumed, it should reacquire  its exclusive resources and file handles.

## Remarks

The system suspends your app whenever the user switches to another app or to the desktop or Start screen. The system resumes your app whenever the user switches back to it. When the system resumes your app, the content of your variables and data structures is the same as it was before the system suspended the app. The system restores the app exactly where it left off, so that it appears to the user as if it's been running in the background.

The system attempts to keep your app and its data in memory while it's suspended. However, if the system does not have the resources to keep your app in memory, the system will terminate your app. When the user switches back to a suspended app that has been terminated, the system sends an [**Activated**](/uwp/api/windows.applicationmodel.core.coreapplicationview.activated) event and should restore its application data in its [**OnLaunched**](/uwp/api/windows.ui.xaml.application.onlaunched) method.

The system doesn't notify an app when it's terminated, so your app must save its application data and release exclusive resources and file handles when it's suspended, and restore them when the app is activated after termination.

If you make an asynchronous call within your handler, control returns immediately from that asynchronous call. That means that execution can then return from your event handler and your app will move to the next state even though the asynchronous call hasn't completed yet. Use the [**GetDeferral**](/uwp/api/Windows.ApplicationModel) method on the [**EnteredBackgroundEventArgs**](/uwp/api/Windows.ApplicationModel) object that is passed to your event handler to delay suspension until after you call the [**Complete**](/uwp/api/windows.foundation.deferral.complete) method on the returned [**Windows.Foundation.Deferral**](/uwp/api/windows.foundation.deferral) object.

A deferral doesn't increase the amount you have to run your code before your app is terminated. It only delays termination until either the deferral's *Complete* method is called, or the deadline passes-*whichever comes first*. To extend time in the Suspending state use [**ExtendedExecutionSession**](run-minimized-with-extended-execution.md)

> [!NOTE]
> To improve system responsiveness in Windows 8.1, apps are given low priority access to resources after they are suspended. To support this new priority, the suspend operation timeout is extended so that the app has the equivalent of the 5-second timeout for normal priority on Windows or between 1 and 10 seconds on Windows Phone. You cannot extend or alter this timeout window.

**A note about debugging using Visual Studio:** Visual Studio prevents Windows from suspending an app that is attached to the debugger. This is to allow the user to view the Visual Studio debug UI while the app is running. When you're debugging an app, you can send it a suspend event using Visual Studio. Make sure the **Debug Location** toolbar is being shown, then click the **Suspend** icon.

## Related topics

* [App lifecycle](app-lifecycle.md)
* [Handle app activation](activate-an-app.md)
* [Handle app resume](resume-an-app.md)
* [UX guidelines for launch, suspend, and resume](./index.md)
* [Extended Execution](run-minimized-with-extended-execution.md)

 

 