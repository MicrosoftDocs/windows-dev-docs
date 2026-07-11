---
title: Create user activities in Windows App SDK apps
description: Learn how to create user activities in your Windows App SDK desktop app to help users resume tasks and track activity history.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Create user activities in Windows App SDK apps

User activities represent tasks that a user performs in your app. You create activities to enable users to pick up where they left off. Activities appear in the local activity history and can be surfaced by Windows features that help users return to previous tasks.

> [!NOTE]
> Timeline cloud sync was deprecated in July 2021. User activities created by your app are stored locally and no longer sync across devices through Microsoft Graph Timeline. Local activity history on the device still works.

## Prerequisites

- Your app must be packaged (MSIX) or have package identity.
- No special capability declaration is required — the UserActivity API is available to all packaged apps.

## Create a user activity

Use the [UserActivityChannel](/uwp/api/windows.applicationmodel.useractivities.useractivitychannel) and [UserActivity](/uwp/api/windows.applicationmodel.useractivities.useractivity) classes:

```csharp
using Windows.ApplicationModel.UserActivities;

private UserActivitySession? _currentSession;

private async Task CreateActivityAsync()
{
    var channel = UserActivityChannel.GetDefault();
    var activity = await channel.GetOrCreateUserActivityAsync("document-123");

    activity.ActivationUri = new Uri("myapp://open?doc=123");
    activity.VisualElements.DisplayText = "Quarterly Report";
    activity.VisualElements.Description = "Working on Q4 financial summary";

    await activity.SaveAsync();
    _currentSession = activity.CreateSession();
}
```

The activity session signals that the user is currently engaged with this task. Dispose it when the user switches to a different task.

## Set rich visual details

Use the properties on [UserActivityVisualElements](/uwp/api/windows.applicationmodel.useractivities.useractivityvisualelements) to describe the activity to the user:

```csharp
UserActivity activity = new UserActivity("quarterly-report");

activity.VisualElements.DisplayText = "Quarterly Report";
activity.VisualElements.Description = "Last edited: Section 3 - Revenue Analysis";
activity.VisualElements.Attribution = new UserActivityAttribution(
    new Uri("ms-appx:///Assets/AppIcon.png"));
```

> [!NOTE]
> `AdaptiveCardBuilder` (`Windows.UI.Shell`) let you render a full Adaptive Card as an activity's visual, but that surface was part of Windows Timeline, which Microsoft retired. Don't use `AdaptiveCardBuilder` in new code — use the `VisualElements` properties shown above instead.

## Handle activation from an activity

When the user selects an activity to resume, your app is activated with a protocol URI. Handle it in your activation logic:

```csharp
var activatedArgs = AppInstance.GetCurrent().GetActivatedEventArgs();

if (activatedArgs.Kind == ExtendedActivationKind.Protocol)
{
    var protocolArgs = activatedArgs.Data as Windows.ApplicationModel.Activation.IProtocolActivatedEventArgs;
    if (protocolArgs?.Uri.Scheme == "myapp")
    {
        // Parse the query string manually; System.Web.HttpUtility isn't
        // available to apps that target .NET (as opposed to .NET Framework).
        string? docId = protocolArgs.Uri.Query
            .TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(pair => pair.Split('=', 2))
            .FirstOrDefault(pair => pair[0] == "doc")
            ?.ElementAtOrDefault(1);
        // Navigate to the document
    }
}
```

## End the session

When the user stops working on the activity, dispose the session:

```csharp
UserActivitySession? _currentSession = null;

_currentSession?.Dispose();
_currentSession = null;
```

## Best practices

- **Use meaningful activity IDs** — The ID should uniquely identify the task (for example, a document path or project name).
- **Update activities** — Call `SaveAsync()` when the user makes progress to keep the description current.
- **Set an activation URI** — Always provide a URI so the activity can relaunch the app to the correct state.
- **Create one session at a time** — Dispose the previous session before creating a new one.

For detailed guidance, see [User activities best practices](user-activities-best-practices.md).

## Related content

- [User activities best practices](user-activities-best-practices.md)
- [UserActivity class](/uwp/api/windows.applicationmodel.useractivities.useractivity)
- [UserActivityChannel class](/uwp/api/windows.applicationmodel.useractivities.useractivitychannel)
- [Adaptive Cards](https://adaptivecards.io/)
