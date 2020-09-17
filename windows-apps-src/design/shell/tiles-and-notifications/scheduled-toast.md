---
Description: Learn how to schedule a local toast notification to appear at a later time.
title: Schedule a toast notification
label: Schedule a toast notification
template: detail.hbs
ms.date: 04/09/2020
ms.topic: article
keywords: windows 10, uwp, scheduled toast notification, scheduledtoastnotification, how to, quickstart, getting started, code sample, walkthrough
ms.localizationpriority: medium
---

# Schedule a toast notification

Scheduled toast notifications allow you to schedule a notification to appear at a later time, regardless of whether your app is running at that time. This is useful for scenarios like displaying reminders or other follow-up tasks for the user, where the time and content of the notification is known ahead-of-time.

Note that scheduled toast notifications have a delivery window of 5 minutes. If the computer is turned off during the scheduled delivery time, and remains off for longer than 5 minutes, the notification will be "dropped" as no longer relevant to the user. If you need guaranteed delivery of notifications regardless of how long the computer was off, we recommend using a background task with a time trigger, as illustrated in [this code sample](https://github.com/WindowsNotifications/quickstart-snoozable-toasts-even-if-computer-is-off).

> [!IMPORTANT]
> Desktop applications (both MSIX/sparse packages and classic Win32) have slightly different steps for sending notifications and handling activation. Follow along with the instructions below, however replace `ToastNotificationManager` with the `DesktopNotificationManagerCompat` class from the [Desktop apps](toast-desktop-apps.md) documentation.

> **Important APIs**: [ScheduledToastNotification Class](/uwp/api/Windows.UI.Notifications.ScheduledToastNotification)


## Prerequisites

To fully understand this topic, the following will be helpful...

* A working knowledge of toast notification terms and concepts. For more information, see [Toast and action center overview](/archive/blogs/tiles_and_toasts/toast-notification-and-action-center-overview-for-windows-10).
* A familiarity with Windows 10 toast notification content. For more information, see [toast content documentation](adaptive-interactive-toasts.md).
* A Windows 10 UWP app project


## Install NuGet packages

We recommend installing the two following NuGet packages to your project. Our code sample will use these packages.

* [Microsoft.Toolkit.Uwp.Notifications](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/): Generate toast payloads via objects instead of raw XML.
* [QueryString.NET](https://www.nuget.org/packages/QueryString.NET/): Generate and parse query strings with C#


## Add namespace declarations

`Windows.UI.Notifications` includes the toast APIs.

```csharp
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using Microsoft.QueryStringDotNET; // QueryString.NET
```


## Construct the toast content

In Windows 10, your toast notification content is described using an adaptive language that allows great flexibility with how your notification looks. See the [toast content documentation](adaptive-interactive-toasts.md) for more information.

Thanks to the Notifications library, generating the XML content is straightforward. If you don't install the Notifications library from NuGet, you have to construct the XML manually, which leaves room for errors.

You should always set the **Launch** property, so when user taps the body of the toast and your app is launched, your app knows what content it should display.

```csharp
// In a real app, these would be initialized with actual data
string title = "ASTR 170B1";
string content = "You have 3 items due today!";

// Now we can construct the final toast content
ToastContent toastContent = new ToastContent()
{
    Visual = new ToastVisual()
    {
        BindingGeneric = new ToastBindingGeneric()
        {
            Children =
            {
                new AdaptiveText()
                {
                    Text = title
                },
     
                new AdaptiveText()
                {
                    Text = content
                }
            }
        }
    },
 
    // Arguments when the user taps body of toast
    Launch = new QueryString()
    {
        { "action", "viewClass" },
        { "classId", "3910938180" }
 
    }.ToString()
};
```

## Create the scheduled toast

Once you have initialized your toast content, create a new [ScheduledToastNotification](/uwp/api/Windows.UI.Notifications.ScheduledToastNotification) and pass in the content's XML, and the time you want the notification to be delivered.

```csharp
// Create the scheduled notification
var toast = new ScheduledToastNotification(toastContent.GetXml(), DateTime.Now.AddSeconds(5));
```


## Provide a primary key for your toast

If you want to programmatically cancel, remove, or replace the scheduled notification, you need to use the Tag property (and optionally the Group property) to provide a primary key for your notification. Then, you can use this primary key in the future to cancel, remove, or replace the notification.

To see more details on replacing/removing already delivered toast notifications, please see [Quickstart: Managing toast notifications in action center (XAML)](/previous-versions/windows/apps/dn631260(v=win.10)).

Tag and Group combined act as a composite primary key. Group is the more generic identifier, where you can assign groups like "wallPosts", "messages", "friendRequests", etc. And then Tag should uniquely identify the notification itself from within the group. By using a generic group, you can then remove all notifications from that group by using the [RemoveGroup API](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory#Windows_UI_Notifications_ToastNotificationHistory_RemoveGroup_System_String_).

```csharp
toast.Tag = "18365";
toast.Group = "ASTR 170B1";
```


## Schedule the notification

Finally, create a [ToastNotifier](/uwp/api/windows.ui.notifications.toastnotifier) and call AddToSchedule(), passing in your scheduled toast notification.

```csharp
// And your scheduled toast to the schedule
ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
```


## Cancel scheduled notifications

To cancel a scheduled notification, you first have to retrieve the list of all scheduled notifications.

Then, find your scheduled toast matching the tag (and optionally group) you specified earlier, and call RemoveFromSchedule().

```csharp
// Create the toast notifier
ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier();

// Get the list of scheduled toasts that haven't appeared yet
IReadOnlyList<ScheduledToastNotification> scheduledToasts = notifier.GetScheduledToastNotifications();

// Find our scheduled toast we want to cancel
var toRemove = scheduledToasts.FirstOrDefault(i => i.Tag == "18365" && i.Group == "ASTR 170B1");
if (toRemove != null)
{
    // And remove it from the schedule
    notifier.RemoveFromSchedule(toRemove);
}
```


## Activation handling

See the [send a local toast](send-local-toast.md) docs to learn more about handling activation. Activation of a scheduled toast notification is handled the same as activation of a local toast notification.


## Adding actions, inputs, and more

See the [send a local toast](send-local-toast.md) docs to learn more about advanced topics like actions and inputs. Actions and inputs work the same in local toasts as they do in scheduled toasts.


## Resources

* [Toast content documentation](adaptive-interactive-toasts.md)
* [ScheduledToastNotification Class](/uwp/api/Windows.UI.Notifications.ScheduledToastNotification)