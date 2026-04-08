---
description: Learn how to send a local app notification from a C++ UWP app and handle the user clicking the notification.
title: Send a local app notification from a C++ UWP app
ms.assetid: E9AB7156-A29E-4ED7-B286-DA4A6E683638
label: Send a local app notification from a C++ UWP app
template: detail.hbs
ms.date: 07/28/2025
ms.topic: how-to
keywords: windows 10, windows app sdk, winappsdk, uwp, send toast notifications, notifications, send notifications, toast notifications, how to, quickstart, getting started, code sample, walkthrough, cpp, c++, uwp
ms.localizationpriority: medium
no-loc: [toast, Toast, app, App]
---
# Send a local app notification from C++ UWP apps

An app notification is a message that your app can construct and deliver to your user while they are not currently inside your app.

<img src="images/toast-notification.png" width="628" alt="Screenshot of an app notification"/>

This quickstart walks you through the steps to create, deliver, and display a Windows 11 app notification using rich content and interactive actions. This quickstart uses local notifications, which are the simplest notification to implement. Many types of Windows apps including WinUI, WPF, WinForms, and console, can send notifications with the [Windows App SDK](/windows/apps/windows-app-sdk/).

> [!NOTE]
> The term "toast notification" is being replaced with "app notification". These terms both refer to the same feature of Windows, but over time we will phase out the use of "toast notification" in the documentation.

> [!IMPORTANT]
> If you're writing a C++ non-UWP app, please see the [C++ WRL](app-notification-cpp-wrl.md) documentation. If you're writing a C# app, please see the [C# documentation](app-notification-csharp-legacy.md).

> [!NOTE]
> For C++ apps using the Windows App SDK, see [Quickstart: App notifications in the Windows App SDK](app-notifications-quickstart.md) which covers the recommended approach using the `Microsoft.Windows.AppNotifications` APIs.

## Step 1: Add namespace declarations

```cpp
using namespace winrt::Windows::UI::Notifications;
using namespace Windows::Data::Xml::Dom;
```

## Step 2: Send an app notification

In Windows 10 and Windows 11, your app notification content is described using an adaptive language that allows great flexibility with how your notification looks. For more information, see the [App notification content](adaptive-interactive-toasts.md) documentation.

We'll start with a simple text-based notification. Construct the notification content and show the notification! You can use the [Windows App SDK](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder) `AppNotificationBuilder` APIs.

<img alt="Simple text notification" src="images/send-toast-01.png" width="364"/>

Construct the XML app notification template, populate it with text and values, construct the notification, and show it.

```cpp
// Construct the XML toast template
XmlDocument doc;
doc.LoadXml(L"\
    <toast>\
        <visual>\
            <binding template=\"ToastGeneric\">\
                <text></text>\
                <text></text>\
            </binding>\
        </visual>\
    </toast>");

// Populate with text and values
doc.DocumentElement().SetAttribute(L"launch", L"action=viewConversation&conversationId=9813");
doc.SelectSingleNode(L"//text[1]").InnerText(L"Andrew sent you a picture");
doc.SelectSingleNode(L"//text[2]").InnerText(L"Check this out, Happy Canyon in Utah!");

// Construct the notification
winrt::Windows::UI::Notifications::ToastNotification notif{ doc };
winrt::Windows::UI::Notifications::ToastNotificationManager toastManager{};
ToastNotifier toastNotifier{ toastManager.CreateToastNotifier() };

// And show it!
toastNotifier.Show(notif);
```

## Step 3: Handle activation

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

> [!IMPORTANT]
> You must initialize your frame and activate your window just like your **OnLaunched** code. **OnLaunched is NOT called if the user clicks on your app notification**, even if your app was closed and is launching for the first time. We often recommend combining **OnLaunched** and **OnActivated** into your own `OnLaunchedOrActivated` method since the same initialization needs to occur in both.

## Activation in depth

The first step in making your notifications actionable is to add some launch args to your notification, so that your app can know what to launch when the user clicks the notification (in this case, we're including some information that later tells us we should open a conversation, and we know which specific conversation to open).

```cpp
// Construct the XML toast template
XmlDocument doc;
doc.LoadXml(L"\
    <toast>\
        <visual>\
            <binding template=\"ToastGeneric\">\
                <text></text>\
                <text></text>\
            </binding>\
        </visual>\
    </toast>");

// Populate with text and values
doc.SelectSingleNode(L"//text[1]").InnerText(L"Andrew sent you a picture");
doc.SelectSingleNode(L"//text[2]").InnerText(L"Check this out, Happy Canyon in Utah!");

// Arguments returned when user taps body of notification
doc.DocumentElement().SetAttribute(L"launch", L"action=viewConversation&conversationId=9813");

// Construct the notification
winrt::Windows::UI::Notifications::ToastNotification notif{ doc };
winrt::Windows::UI::Notifications::ToastNotificationManager toastManager{};
ToastNotifier toastNotifier{ toastManager.CreateToastNotifier() };

// And show it!
toastNotifier.Show(notif);
```

## Add images

You can add rich content to notifications. We'll add an inline image and a profile (app logo override) image.

> [!NOTE]
> Images can be used from the app's package, the app's local storage, or from the web. As of the Fall Creators Update, web images can be up to 3 MB on normal connections and 1 MB on metered connections. On devices not yet running the Fall Creators Update, web images must be no larger than 200 KB.

<img alt="App notification with images" src="images/send-toast-02.png" width="364"/>

```cpp
// Construct the XML toast template
XmlDocument doc;
doc.LoadXml(L"\
    <toast>\
        <visual>\
            <binding template=\"ToastGeneric\">\
                <text></text>\
                <text></text>\
                <image/>\
                <image placement=\"appLogoOverride\" hint-crop=\"circle\"/>\
            </binding>\
        </visual>\
    </toast>");

// Populate with text and values
doc.DocumentElement().SetAttribute(L"launch", L"action=viewConversation&conversationId=9813");
doc.SelectSingleNode(L"//text[1]").InnerText(L"Andrew sent you a picture");
doc.SelectSingleNode(L"//text[2]").InnerText(L"Check this out, Happy Canyon in Utah!");

// Inline image
doc.SelectSingleNode(L"//image[1]").as<XmlElement>().SetAttribute(L"src", L"https://picsum.photos/360/202?image=883");

// Profie (app logo override) image
doc.SelectSingleNode(L"//image[2]").as<XmlElement>().SetAttribute(L"src", L"ms-appdata:///local/Andrew.jpg");

// Construct the notification
winrt::Windows::UI::Notifications::ToastNotification notif{ doc };
winrt::Windows::UI::Notifications::ToastNotificationManager toastManager{};
ToastNotifier toastNotifier{ toastManager.CreateToastNotifier() };

// And show it!
toastNotifier.Show(notif);
```

## Add buttons and inputs

You can add buttons and inputs to make your notifications interactive. Buttons can launch your foreground app, a protocol, or your background task. We'll add a reply text box, a "Like" button, and a "View" button that opens the image.

<img src="images/toast-notification.png" width="628" alt="Screenshot of an app notification with inputs and buttons"/>

```cpp
// Construct the XML toast template
XmlDocument doc;
doc.LoadXml(L"\
    <toast>\
        <visual>\
            <binding template=\"ToastGeneric\">\
                <text></text>\
                <text></text>\
                <image/>\
                <image placement=\"appLogoOverride\" hint-crop=\"circle\"/>\
            </binding>\
        </visual>\
        <actions>\
            <input\
                id=\"tbReply\"\
                type=\"text\"\
                placeHolderContent=\"Type a reply\"/>\
            <action\
                content=\"Reply\"\
                activationType=\"background\"/>\
            <action\
                content=\"Like\"\
                activationType=\"background\"/>\
            <action\
                content=\"View\"\
                activationType=\"foreground\"/>\
        </actions>\
    </toast>");

// Populate with text and values
doc.DocumentElement().SetAttribute(L"launch", L"action=viewConversation&conversationId=9813");
...

// Buttons
doc.SelectSingleNode(L"//action[1]").as<XmlElement>().SetAttribute(L"arguments", L"action=reply&conversationId=9813");
doc.SelectSingleNode(L"//action[2]").as<XmlElement>().SetAttribute(L"arguments", L"action=like&conversationId=9813");
doc.SelectSingleNode(L"//action[3]").as<XmlElement>().SetAttribute(L"arguments", L"action=viewImage&imageUrl=https://picsum.photos/364/202?image=883");

// Construct the notification
winrt::Windows::UI::Notifications::ToastNotification notif{ doc };
winrt::Windows::UI::Notifications::ToastNotificationManager toastManager{};
ToastNotifier toastNotifier{ toastManager.CreateToastNotifier() };

// And show it!
toastNotifier.Show(notif);
```

The activation of foreground buttons are handled in the same way as the main notification body (your App.xaml.cpp OnActivated will be called).

## Handle background activation

When you specify background activation on your app notification (or on a button inside the notification), your background task will be executed instead of activating your foreground app.

For more information on background tasks, please see [Support your app with background tasks](/windows/uwp/launch-resume/support-your-app-with-background-tasks).

If you are targeting build 14393 or later, you can use in-process background tasks, which greatly simplify things. Note that in-process background tasks will fail to run on older versions of Windows. We'll use an in-process background task in this code sample.

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

In Windows 10 and 11, all app notifications go in Action Center after they are dismissed or ignored by the user, so users can look at your notification after the popup is gone.

However, if the message in your notification is only relevant for a period of time, you should set an expiration time on the app notification so the users do not see stale information from your app. For example, if a promotion is only valid for 12 hours, set the expiration time to 12 hours. In the code below, we set the expiration time to be 2 days.

> [!NOTE]
> The default and maximum expiration time for local app notifications is 3 days.

```cpp
// Construct the XML toast template
XmlDocument doc;
doc.LoadXml(L"\
    <toast>\
        <visual>\
            <binding template=\"ToastGeneric\">\
                <text>Expires in 2 days...</text>\
            </binding>\
        </visual>\
    </toast>");

// Construct the notification and set expiration time to 2 days
winrt::Windows::UI::Notifications::ToastNotification notif{ doc };
notif.ExpirationTime(winrt::clock::now() + std::chrono::hours{ 48 });

// Show the notification
winrt::Windows::UI::Notifications::ToastNotificationManager toastManager{};
ToastNotifier toastNotifier{ toastManager.CreateToastNotifier() };
toastNotifier.Show(notif);
```

## Provide a primary key for your app notification

If you want to programmatically remove or replace the notification you send, you need to use the Tag property (and optionally the Group property) to provide a primary key for your notification. Then, you can use this primary key in the future to remove or replace the notification.

To see more details on replacing/removing already delivered app notifications, please see [Quickstart: Managing app notifications in action center (XAML)](/previous-versions/windows/apps/dn631260(v=win.10)).

Tag and Group combined act as a composite primary key. Group is the more generic identifier, where you can assign groups like "wallPosts", "messages", "friendRequests", etc. And then Tag should uniquely identify the notification itself from within the group. By using a generic group, you can then remove all notifications from that group by using the [RemoveGroup API](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory#Windows_UI_Notifications_ToastNotificationHistory_RemoveGroup_System_String_).

```cpp
// Construct the XML toast template
XmlDocument doc;
doc.LoadXml(L"\
    <toast>\
        <visual>\
            <binding template=\"ToastGeneric\">\
                <text>New post on your wall!</text>\
            </binding>\
        </visual>\
    </toast>");

// Construct the notification and assign tag and group
winrt::Windows::UI::Notifications::ToastNotification notif{ doc };
notif.Tag(L"18365");
notif.Group(L"wallPosts");

// Show the notification
winrt::Windows::UI::Notifications::ToastNotificationManager toastManager{};
ToastNotifier toastNotifier{ toastManager.CreateToastNotifier() };
toastNotifier.Show(notif);
```



## Clear your notifications

Apps are responsible for removing and clearing their own notifications. When your app is launched, we do NOT automatically clear your notifications.

Windows will only automatically remove a notification if the user explicitly clicks the notification.

Here's an example of what a messaging app should do…

1. User receives multiple app notifications about new messages in a conversation
2. User taps one of those notifications to open the conversation
3. The app opens the conversation and then clears all notifications for that conversation (by using [RemoveGroup](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory) on the app-supplied group for that conversation)
4. User's Action Center now properly reflects the notification state, since there are no stale notifications for that conversation left in Action Center.

To learn about clearing all notifications or removing specific notifications, see [Quickstart: Managing app notifications in action center (XAML)](/previous-versions/windows/apps/dn631260(v=win.10)).

```cpp
winrt::Windows::UI::Notifications::ToastNotificationManager toastManager{};
toastManager.History().Clear();
```

## Resources

* [Full C# code sample on GitHub](https://github.com/WindowsNotifications/quickstart-sending-local-toast-win10)
* [App content documentation](adaptive-interactive-toasts.md)
* [ToastNotification Class](/uwp/api/Windows.UI.Notifications.ToastNotification)
* [ToastNotificationActivatedEventArgs Class](/uwp/api/Windows.ApplicationModel.Activation.ToastNotificationActivatedEventArgs)

