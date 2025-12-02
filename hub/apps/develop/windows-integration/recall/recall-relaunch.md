---
title: Enable relaunching your content from Recall
description: Learn how to enable your users to relaunch back to your content from Windows Recall.
ms.date: 05/21/2025
ms.topic: how-to
no-loc: [Recall, useractivity]
---

# Enable relaunching your content from Recall

Recall automatically saves snapshots of your application, however, users won't be able to relaunch back to your content unless you provide a [UserActivity](/uwp/api/windows.applicationmodel.useractivities.useractivity) at the time of the snapshot. This enables Recall to launch the user back into what was being seen at that time.

A **UserActivity** refers to something specific the user was working on within your app. For example, when a user is writing a document, a `UserActivity` could refer to the specific place in the document where the user left off writing. When listening to a music app, the `UserActivity` could be the playlist that the user last listened to. When drawing on a canvas, the `UserActivity` could be where the user last made a mark. In summary, a `UserActivity` represents a destination within your Windows app that a user can return to so that they can resume what they were doing.

## Pushing user activities

Whenever the main content in your app changes (like the user opening a different email, opening a different webpage, etc), your app should log a new `UserActivitySession` so that the system knows what content is currently open. Recall will then associate the most recent `UserActivity` with the snapshot it saves, and will use the `ActivationUri` within the activity to allow the user to relaunch back to that content.

We recommend that you push user activities on all PCs, even those not running Recall.

```csharp
UserActivitySession _previousSession;

private async Task OnContentChangedAsync()
{
    // Dispose of any previous session (which automatically logs the end of the interaction with that content)
    _previousSession?.Dispose();

    // Generate an identifier that uniquely maps to your new content.
    string id = "doc135.txt";

    // Create a new user activity that represents your new content
    var activity = await UserActivityChannel.GetDefault().GetOrCreateUserActivityAsync(id);

    // Populate the required properties
    activity.DisplayText = "doc135.txt";
    activity.ActivationUri = new Uri("my-app://docs/doc135.txt");

    // Save the activity
    await activity.SaveAsync();

    // And start a new session tracking the engagement with this new activity
    _previousSession = activity.CreateSession();
}
```

> [!NOTE]
> The [**GetOrCreateUserActivityAsync**](/uwp/api/windows.applicationmodel.useractivities.useractivitychannel.getorcreateuseractivityasync) method will always return a new activity on the latest versions of Windows. The ability to get your previously saved activities has been removed, and Windows no longer stores your app's previous activities in a way that your app can retrieve them.

## Optional: Handling the Requested event

In addition to pushing activities, your app can choose to implement the [`UserActivityRequested`](/uwp/api/windows.applicationmodel.useractivities.useractivityrequestmanager.useractivityrequested) event, which Windows may fire to ensure it has the latest activity from your app.

```csharp
public void OnLaunched()
{
    UserActivityRequestManager.GetForCurrentView().UserActivityRequested += UserActivityRequested;
}

private async void UserActivityRequested(
    Windows.ApplicationModel.UserActivities.UserActivityRequestManager sender,
    Windows.ApplicationModel.UserActivities.UserActivityRequestedEventArgs args)
{
    // Start a deferral so you can use async code
    var deferral = args.GetDeferral();

    try
    {
        // Generate an identifier that uniquely maps to your current content.
        string id = "doc135.txt";

        // Create a user activity that represents your current content
        var activity = await UserActivityChannel.GetDefault().GetOrCreateUserActivityAsync(id);

        // Populate the required properties
        activity.DisplayText = "doc135.txt";
        activity.ActivationUri = new Uri("my-app://docs/doc135.txt");

        // And return the activity to the event handler
        args.Request.SetUserActivity(activity);
    }

    finally
    {
        // And complete the deferral
        deferral.Complete();
    }
}
```

## Related content

- [UserActivity class](/uwp/api/windows.applicationmodel.useractivities.useractivity)
- [Provide sensitivity labels to Recall with UserActivity ContentInfo](recall-contentinfo-labels.md) - Learn how to supply sensitivity label metadata through `UserActivity.ContentInfo` for enterprise DLP policy enforcement
- [Guidance for developers of web browsers](./recall-web-browsers.md)
