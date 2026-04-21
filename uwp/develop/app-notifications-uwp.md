---
title: Use app notifications with a UWP app
description: Learn how to send a local app notification from a UWP app and handle the user clicking the notification using the Windows.UI.Notifications APIs.
ms.date: 04/08/2026
ms.topic: how-to
keywords: windows 10, windows 11, uwp, send toast notifications, notifications, send notifications, toast notifications, how to, c#, csharp, c++, cpp
ms.localizationpriority: medium
---

# Use app notifications with a UWP app

An app notification is a UI popup that appears outside of your app's window, delivering timely information or actions to the user. Notifications can be purely informational, can launch your app when clicked, or can trigger a background action without bringing your app to the foreground.

:::image type="content" source="images/toast-notification.png" alt-text="Screenshot of an app notification":::

This article walks you through the steps to create and send an app notification from a UWP app, and then handle activation when the user interacts with it.

For an overview of app notifications and guidance for other frameworks, see [App notifications overview](/windows/apps/develop/notifications/app-notifications/).

This article covers local notifications. For information about delivering notifications from a cloud service, see [Push notifications overview](/windows/apps/develop/notifications/push-notifications/).

## Prerequisites

- A UWP app project in Visual Studio
- The **Universal Windows Platform development** workload installed in Visual Studio

## Send an app notification

UWP apps use the `Windows.UI.Notifications` namespace to construct and send notifications using XML. This section shows how to send notifications using C# and C++.

### [C#](#tab/cs)

```csharp
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

var xml = @"<toast launch=""action=viewConversation&amp;conversationId=9813"">
    <visual>
        <binding template=""ToastGeneric"">
            <text>Andrew sent you a picture</text>
            <text>Check this out, The Enchantments in Washington!</text>
        </binding>
    </visual>
</toast>";

var doc = new XmlDocument();
doc.LoadXml(xml);

var notification = new ToastNotification(doc);
ToastNotificationManager.CreateToastNotifier().Show(notification);
```

### [C++](#tab/cpp)

```cpp
using namespace winrt::Windows::UI::Notifications;
using namespace winrt::Windows::Data::Xml::Dom;

XmlDocument doc;
doc.LoadXml(L"\
    <toast launch=\"action=viewConversation&amp;conversationId=9813\">\
        <visual>\
            <binding template=\"ToastGeneric\">\
                <text>Andrew sent you a picture</text>\
                <text>Check this out, The Enchantments in Washington!</text>\
            </binding>\
        </visual>\
    </toast>");

ToastNotification notif{ doc };
ToastNotificationManager::CreateToastNotifier().Show(notif);
```

---

## Handle activation

When the user clicks your notification (or a button on the notification with foreground activation), your app's `OnActivated` method is invoked. **`OnLaunched` is not called for notification activations**, even if your app was closed and is launching for the first time. We recommend combining `OnLaunched` and `OnActivated` into a shared initialization method.

### [C#](#tab/cs)

**App.xaml.cs**

```csharp
protected override void OnLaunched(LaunchActivatedEventArgs e)
{
    OnLaunchedOrActivated(e.PreviousExecutionState);

    var rootFrame = Window.Current.Content as Frame;
    if (e.PrelaunchActivated == false)
    {
        if (rootFrame?.Content == null)
        {
            rootFrame?.Navigate(typeof(MainPage), e.Arguments);
        }
        Window.Current.Activate();
    }
}

protected override void OnActivated(IActivatedEventArgs e)
{
    OnLaunchedOrActivated(e.PreviousExecutionState);

    if (e is ToastNotificationActivatedEventArgs toastArgs)
    {
        var rootFrame = Window.Current.Content as Frame;
        if (rootFrame?.Content == null)
        {
            rootFrame?.Navigate(typeof(MainPage));
        }
        Window.Current.Activate();

        // Parse the notification arguments
        string argument = toastArgs.Argument;
        // TODO: Navigate to the relevant content based on the arguments
    }
}

private void OnLaunchedOrActivated(ApplicationExecutionState previousState)
{
    if (Window.Current.Content is not Frame)
    {
        var rootFrame = new Frame();
        rootFrame.NavigationFailed += OnNavigationFailed;
        Window.Current.Content = rootFrame;
    }
}
```

### [C++](#tab/cpp)

**App.xaml.cpp**

```cpp
void App::OnLaunched(LaunchActivatedEventArgs const& e)
{
    OnLaunchedOrActivated(e.PreviousExecutionState());

    auto rootFrame = Window::Current().Content().try_as<Frame>();
    if (!e.PrelaunchActivated())
    {
        if (rootFrame.Content() == nullptr)
        {
            rootFrame.Navigate(xaml_typename<MainPage>(), box_value(e.Arguments()));
        }
        Window::Current().Activate();
    }
}

void App::OnActivated(IActivatedEventArgs const& e)
{
    OnLaunchedOrActivated(e.PreviousExecutionState());

    if (e.Kind() == ActivationKind::ToastNotification)
    {
        auto toastArgs = e.as<ToastNotificationActivatedEventArgs>();

        auto rootFrame = Window::Current().Content().try_as<Frame>();
        if (rootFrame.Content() == nullptr)
        {
            rootFrame.Navigate(xaml_typename<MainPage>());
        }
        Window::Current().Activate();

        // Parse the notification arguments
        auto argument = toastArgs.Argument();
        // TODO: Navigate to the relevant content based on the arguments
    }
}

void App::OnLaunchedOrActivated(ApplicationExecutionState previousState)
{
    if (Window::Current().Content() == nullptr)
    {
        auto rootFrame = Frame();
        rootFrame.NavigationFailed({ this, &App::OnNavigationFailed });
        Window::Current().Content(rootFrame);
    }
}
```

---

For information about adding buttons, images, inputs, audio, and other rich content to your notifications, see [App notification content](/windows/apps/develop/notifications/app-notifications/app-notifications-content).

## Related content

- [Quickstart: App notifications in the Windows App SDK](/windows/apps/develop/notifications/app-notifications/app-notifications-quickstart)
- [App notification content](/windows/apps/develop/notifications/app-notifications/app-notifications-content)
- [ToastNotification Class](/uwp/api/Windows.UI.Notifications.ToastNotification)
- [ToastNotificationActivatedEventArgs Class](/uwp/api/Windows.ApplicationModel.Activation.ToastNotificationActivatedEventArgs)




