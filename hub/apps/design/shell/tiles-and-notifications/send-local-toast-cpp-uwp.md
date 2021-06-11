---
description: Learn how to send a local toast notification from a C++ UWP app and handle the user clicking the toast.
title: Send a local toast notification from a C++ UWP app
ms.assetid: E9AB7156-A29E-4ED7-B286-DA4A6E683638
label: Send a local toast notification from a C++ UWP app
template: detail.hbs
ms.date: 11/17/2020
ms.topic: article
keywords: windows 10, uwp, send toast notifications, notifications, send notifications, toast notifications, how to, quickstart, getting started, code sample, walkthrough, cpp, c++, uwp
ms.localizationpriority: medium
---
# Send a local toast notification from C++ UWP apps

[!INCLUDE [intro](includes/send-toast-intro.md)]

> [!IMPORTANT]
> If you're writing a C++ non-UWP app, please see the [C++ WRL](send-local-toast-desktop-cpp-wrl.md) documentation. If you're writing a C# app, please see the [C# documentation](send-local-toast.md).



## Step 1: Install NuGet package

[!INCLUDE [nuget package](includes/nuget-package.md)]

Our code sample will use this package. This package allows you to create toast notifications without using XML.


## Step 2: Add namespace declarations

```cpp
using namespace Microsoft::Toolkit::Uwp::Notifications;
```


## Step 3: Send a toast

[!INCLUDE [basic toast intro](includes/send-toast-basic-toast-intro.md)]

```cpp
// Construct the content and show the toast!
(ref new ToastContentBuilder())
    ->AddArgument("action", "viewConversation")
    ->AddArgument("conversationId", 9813)
    ->AddText("Andrew sent you a picture")
    ->AddText("Check this out, The Enchantments in Washington!")
    ->Show();
```


## Step 4: Handling activation

When the user clicks your notification (or a button on the notification with foreground activation), your app's **App.xaml.cpp** **OnActivated** will be invoked.

**App.xaml.cpp**

```cpp
void App::OnActivated(IActivatedEventArgs^ e)
{
    // Handle notification activation
    if (e->Kind == ActivationKind::ToastNotification)
    {
        ToastNotificationActivatedEventArgs^ toastActivationArgs = (ToastNotificationActivatedEventArgs^)e;

        // Obtain the arguments from the notification
        ToastArguments^ args = ToastArguments::Parse(toastActivationArgs->Argument);

        // Obtain any user input (text boxes, menu selections) from the notification
        auto userInput = toastActivationArgs->UserInput;
 
        // TODO: Show the corresponding content
    }
}
```

[!INCLUDE [OnLaunched warning](includes/onlaunched-warning.md)]


## Activation in depth

The first step in making your notifications actionable is to add some launch args to your notification, so that your app can know what to launch when the user clicks the notification (in this case, we're including some information that later tells us we should open a conversation, and we know which specific conversation to open).

```cpp
// Construct the content and show the toast!
(ref new ToastContentBuilder())

    // Arguments returned when user taps body of notification
    ->AddArgument("action", "viewConversation")
    ->AddArgument("conversationId", 9813)

    ->AddText("Andrew sent you a picture")
    ->Show();
```



## Adding images

You can add rich content to notifications. We'll add an inline image and a profile (app logo override) image.

[!INCLUDE [images note](includes/images-note.md)]

<img alt="Toast with images" src="images/send-toast-02.png" width="364"/>

```cpp
// Construct the content and show the toast!
(ref new ToastContentBuilder())
    ...

    // Inline image
    ->AddInlineImage(ref new Uri("https://picsum.photos/360/202?image=883"))

    // Profile (app logo override) image
    ->AddAppLogoOverride(ref new Uri("ms-appdata:///local/Andrew.jpg"), ToastGenericAppLogoCrop::Circle)
    
    ->Show();
```



## Adding buttons and inputs

You can add buttons and inputs to make your notifications interactive. Buttons can launch your foreground app, a protocol, or your background task. We'll add a reply text box, a "Like" button, and a "View" button that opens the image.

<img src="images/toast-notification.png" width="628" alt="Screenshot of a toast notification with inputs and buttons"/>

```cpp
// Construct the content
(ref new ToastContentBuilder())
    ->AddArgument("conversationId", 9813)
    ...

    // Text box for replying
    ->AddInputTextBox("tbReply", "Type a response")

    // Buttons
    ->AddButton((ref new ToastButton())
        ->SetContent("Reply")
        ->AddArgument("action", "reply")
        ->SetBackgroundActivation())

    ->AddButton((ref new ToastButton())
        ->SetContent("Like")
        ->AddArgument("action", "like")
        ->SetBackgroundActivation())

    ->AddButton((ref new ToastButton())
        ->SetContent("View")
        ->AddArgument("action", "view"))
    
    ->Show();
```

The activation of foreground buttons are handled in the same way as the main toast body (your App.xaml.cpp OnActivated will be called).



## Handling background activation

When you specify background activation on your toast (or on a button inside the toast), your background task will be executed instead of activating your foreground app.

For more information on background tasks, please see [Support your app with background tasks](/windows/uwp/launch-resume/support-your-app-with-background-tasks).

If you are targeting build 14393 or higher, you can use in-process background tasks, which greatly simplify things. Note that in-process background tasks will fail to run on older versions of Windows. We'll use an in-process background task in this code sample.

```csharp
const string taskName = "ToastBackgroundTask";

// If background task is already registered, do nothing
if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
    return;

// Otherwise request access
BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();

// Create the background task
BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
{
    Name = taskName
};

// Assign the toast action trigger
builder.SetTrigger(new ToastNotificationActionTrigger());

// And register the task
BackgroundTaskRegistration registration = builder.Register();
```


Then in your App.xaml.cs, override the OnBackgroundActivated method. You can then retrieve the pre-defined arguments and user input, similar to the foreground activation.

**App.xaml.cs**

```csharp
protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
{
    var deferral = args.TaskInstance.GetDeferral();
 
    switch (args.TaskInstance.Task.Name)
    {
        case "ToastBackgroundTask":
            var details = args.TaskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;
            if (details != null)
            {
                string arguments = details.Argument;
                var userInput = details.UserInput;

                // Perform tasks
            }
            break;
    }
 
    deferral.Complete();
}
```


## Set an expiration time

In Windows 10, all toast notifications go in Action Center after they are dismissed or ignored by the user, so users can look at your notification after the popup is gone.

However, if the message in your notification is only relevant for a period of time, you should set an expiration time on the toast notification so the users do not see stale information from your app. For example, if a promotion is only valid for 12 hours, set the expiration time to 12 hours. In the code below, we set the expiration time to be 2 days.

> [!NOTE]
> The default and maximum expiration time for local toast notifications is 3 days.

```cpp
// Create toast content and show the toast!
(ref new ToastContentBuilder())
    ->AddText("Expires in 2 days...")
    ->Show(toast =>
    {
        toast->ExpirationTime = DateTime::Now->AddDays(2);
    });
```


## Provide a primary key for your toast

If you want to programmatically remove or replace the notification you send, you need to use the Tag property (and optionally the Group property) to provide a primary key for your notification. Then, you can use this primary key in the future to remove or replace the notification.

To see more details on replacing/removing already delivered toast notifications, please see [Quickstart: Managing toast notifications in action center (XAML)](/previous-versions/windows/apps/dn631260(v=win.10)).

Tag and Group combined act as a composite primary key. Group is the more generic identifier, where you can assign groups like "wallPosts", "messages", "friendRequests", etc. And then Tag should uniquely identify the notification itself from within the group. By using a generic group, you can then remove all notifications from that group by using the [RemoveGroup API](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory#Windows_UI_Notifications_ToastNotificationHistory_RemoveGroup_System_String_).

```cpp
// Create toast content and show the toast!
(ref new ToastContentBuilder())
    ->AddText("New post on your wall!")
    ->Show(toast =>
    {
        toast.Tag = "18365";
        toast.Group = "wallPosts";
    });
```



## Clear your notifications

Apps are responsible for removing and clearing their own notifications. When your app is launched, we do NOT automatically clear your notifications.

Windows will only automatically remove a notification if the user explicitly clicks the notification.

Here's an example of what a messaging app should do…

1. User receives multiple toasts about new messages in a conversation
2. User taps one of those toasts to open the conversation
3. The app opens the conversation and then clears all toasts for that conversation (by using [RemoveGroup](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory#Windows_UI_Notifications_ToastNotificationHistory_RemoveGroup_System_String_) on the app-supplied group for that conversation)
4. User's Action Center now properly reflects the notification state, since there are no stale notifications for that conversation left in Action Center.

To learn about clearing all notifications or removing specific notifications, see [Quickstart: Managing toast notifications in action center (XAML)](/previous-versions/windows/apps/dn631260(v=win.10)).

```cpp
ToastNotificationManagerCompat::History->Clear();
```



## Resources

* [Full code sample on GitHub](https://github.com/WindowsNotifications/quickstart-sending-local-toast-win10)
* [Toast content documentation](adaptive-interactive-toasts.md)
* [ToastNotification Class](/uwp/api/Windows.UI.Notifications.ToastNotification)
* [ToastNotificationActivatedEventArgs Class](/uwp/api/Windows.ApplicationModel.Activation.ToastNotificationActivatedEventArgs)
