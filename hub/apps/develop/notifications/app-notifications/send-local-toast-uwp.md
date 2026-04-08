---
title: Send a local app notification from a UWP app
description: Learn how to send a local app notification from a UWP app and handle the user clicking the notification using the Windows.UI.Notifications APIs.
ms.date: 04/08/2026
ms.topic: how-to
keywords: windows 10, windows 11, uwp, send toast notifications, notifications, send notifications, toast notifications, how to, c#, csharp, c++, cpp
ms.localizationpriority: medium
---

# Send a local app notification from a UWP app

An app notification is a message that your app can construct and deliver to your user while they are not currently inside your app.

:::image type="content" source="images/toast-notification.png" alt-text="Screenshot of an app notification" width="628":::

This article walks you through the steps to send and handle app notifications from a UWP app. UWP apps use the `Windows.UI.Notifications` APIs that are built into the Windows SDK.

> [!TIP]
> For new desktop apps, we recommend using the [Windows App SDK](/windows/apps/windows-app-sdk/) instead. See [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md) for the recommended approach using the `Microsoft.Windows.AppNotifications` APIs.

> [!NOTE]
> For other app types, see [WPF](send-local-toast-wpf.md), [WinForms](send-local-toast-winforms.md), or [Console](send-local-toast-console.md).

## Prerequisites

- A UWP app project in Visual Studio
- The **Universal Windows Platform development** workload installed in Visual Studio

## Step 1: Send an app notification

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

## Step 2: Handle activation

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

## Adding buttons and inputs

You can add buttons and inputs to make your notifications interactive.

### [C#](#tab/cs)

```csharp
var xml = @"<toast launch=""action=viewConversation&amp;conversationId=9813"">
    <visual>
        <binding template=""ToastGeneric"">
            <text>Andrew sent you a picture</text>
            <text>Check this out, The Enchantments in Washington!</text>
        </binding>
    </visual>
    <actions>
        <input id=""tbReply"" type=""text"" placeHolderContent=""Type a reply""/>
        <action content=""Reply"" arguments=""action=reply&amp;conversationId=9813"" 
                activationType=""background""/>
        <action content=""Like"" arguments=""action=like&amp;conversationId=9813"" 
                activationType=""background""/>
        <action content=""View"" arguments=""action=viewImage"" 
                activationType=""foreground""/>
    </actions>
</toast>";

var doc = new XmlDocument();
doc.LoadXml(xml);

var notification = new ToastNotification(doc);
ToastNotificationManager.CreateToastNotifier().Show(notification);
```

### [C++](#tab/cpp)

```cpp
XmlDocument doc;
doc.LoadXml(LR"(
    <toast launch="action=viewConversation&amp;conversationId=9813">
        <visual>
            <binding template="ToastGeneric">
                <text>Andrew sent you a picture</text>
                <text>Check this out, The Enchantments in Washington!</text>
            </binding>
        </visual>
        <actions>
            <input id="tbReply" type="text" placeHolderContent="Type a reply"/>
            <action content="Reply" arguments="action=reply&amp;conversationId=9813" 
                    activationType="background"/>
            <action content="Like" arguments="action=like&amp;conversationId=9813" 
                    activationType="background"/>
            <action content="View" arguments="action=viewImage" 
                    activationType="foreground"/>
        </actions>
    </toast>)");

ToastNotification notif{ doc };
ToastNotificationManager::CreateToastNotifier().Show(notif);
```

---

Foreground buttons are handled in `OnActivated`, the same as clicking the notification body. For background button activation in UWP, see [Handle background activation](#handle-background-activation).

## Adding images

You can add rich content to notifications. For more information, see [App notification content](adaptive-interactive-toasts.md).

> [!NOTE]
> Images can be used from the app's package, the app's local storage, or from the web. Web images can be up to 3 MB on normal connections and 1 MB on metered connections.

## Handle background activation

UWP apps can handle notification button clicks in the background using a background task. This allows your app to perform actions without coming to the foreground.

### [C#](#tab/cs)

Register the background task:

```csharp
const string taskName = "ToastBackgroundTask";

// If background task is already registered, do nothing
if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
    return;

// Request access
BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();

// Create and register the background task
var builder = new BackgroundTaskBuilder { Name = taskName };
builder.SetTrigger(new ToastNotificationActionTrigger());
BackgroundTaskRegistration registration = builder.Register();
```

Then handle it in `App.xaml.cs`:

```csharp
protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
{
    var deferral = args.TaskInstance.GetDeferral();

    if (args.TaskInstance.Task.Name == "ToastBackgroundTask")
    {
        var details = (ToastNotificationActionTriggerDetail)args.TaskInstance.TriggerDetails;
        string arguments = details.Argument;
        var userInput = details.UserInput;

        // Perform background work here
    }

    deferral.Complete();
}
```

### [C++](#tab/cpp)

Background task registration and handling in C++ follows the same pattern. For details, see [Support your app with background tasks](/windows/uwp/launch-resume/support-your-app-with-background-tasks).

---

## Related content

- [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md)
- [App notification content](adaptive-interactive-toasts.md)
- [ToastNotification Class](/uwp/api/Windows.UI.Notifications.ToastNotification)
- [ToastNotificationActivatedEventArgs Class](/uwp/api/Windows.ApplicationModel.Activation.ToastNotificationActivatedEventArgs)
