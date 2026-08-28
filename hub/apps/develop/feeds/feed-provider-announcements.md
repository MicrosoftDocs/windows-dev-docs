---
title: Feed provider announcements
description: Learn how to use feed announcements to display lightweight, glanceable notifications on the Windows taskbar from your feed provider app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 08/28/2026
ms.localizationpriority: medium
---

# Announcements for feed providers

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

Feed announcements let a feed provider request a lightweight, glanceable announcement on the Windows taskbar for related feed content. The announcement surface is intended to help users notice timely updates and continue into the Widgets Board for richer information when they choose to engage. Feed announcements build on the feed provider model in the Windows App SDK. A feed provider registers feed content for the Widgets Board, where feed providers can appear as dashboards in the navigation bar, and users can enable or disable providers from Widgets Board settings.

## What is a feed announcement?

A feed announcement is data that a feed provider requests to display through the provider host. The FeedAnnouncement class provides the announcement data used with FeedManager.TryShowAnnouncement.
The Microsoft.Windows.Widgets.Notifications namespace includes the FeedAnnouncement class, FeedAnnouncementInvokedArgs, and related enums such as AnnouncementActionKind and AnnouncementTextColor.

At a high level, a feed announcement can include:

- A unique announcement ID.
- Primary and secondary text.
- Light and dark mode icon URIs.
- Optional text color, duration, expiration time, custom accessibility text, and badge behavior.

## How feed announcements work

The feed provider app creates a FeedAnnouncement and calls FeedManager.TryShowAnnouncement to request display. The Widgets Board may or may not show the announcement, based on policies. The feed host calls the provider's OnAnnouncementInvoked method when it initially displays the announcement and when the user engages with it.

A typical flow is:

1. The provider determines that it has timely content to announce.
1. The provider creates a FeedAnnouncement that contains the text, icon information, and optional display parameters.
1. The provider calls FeedManager.TryShowAnnouncement, passing the feed provider definition ID, feed definition ID, and the announcement object.
1. The Widgets Board processes the request and decides whether to display the announcement based on its policies.
1. The provider receives OnAnnouncementInvoked when the announcement is shown or the user engages with it.

## Badging for missed announcements

Feed announcement badging allows a feed provider to request persistent visual attention for important updates when a transient taskbar announcement is not sufficient. A badge can remain visible after the announcement has expired, helping users discover content that requires follow-up, such as breaking news, severe weather alerts, sports updates, or other high-value events.

Use badging when:

- The content remains relevant after the announcement is no longer displayed.
- The user is able to discover the update later from the feed provider-backed dashboard.
- The update represents information that benefits from a persistent reminder.

Do not rely on badging for every announcement. The Widgets Board may apply policies that control notification visibility and prioritization. FeedManager.TryShowAnnouncement explicitly states that the Widgets Board may or may not show the requested announcement based on its policies.

## Before you start

To use feed announcements, your app must use Windows App SDK 1.5.2 or later and must have already implemented a feed provider.

You should also review:

- [Dashboards and feed providers](feed-providers.md) for the feed provider model.
- [Microsoft.Windows.Widgets.Notifications namespace](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.notifications) for the announcement-related APIs.

## Create and request an announcement

The following example shows the overall shape of requesting a feed announcement from a feed provider.

```csharp
using Microsoft.Windows.Widgets.Feeds.Providers;
using Microsoft.Windows.Widgets.Notifications;

void RequestFeedAnnouncement(
    string feedProviderDefinitionId,
    string feedDefinitionId,
    string announcementId)
{
    var announcement = new FeedAnnouncement(
        announcementId,
        "Breaking update",
        "Open Widgets to learn more",
        new Uri("ms-appx:///Assets/AnnouncementLight.png"),
        new Uri("ms-appx:///Assets/AnnouncementDark.png"));

    announcement.CustomAccessibilityText = "Breaking update. Open Widgets to learn more.";

    FeedManager.GetDefault().TryShowAnnouncement(
        feedProviderDefinitionId,
        feedDefinitionId,
        announcement);
}
```

FeedManager.TryShowAnnouncement requests that an announcement be shown in the taskbar, and the feed host may or may not show it based on policies.

## Handle announcement invocation

To receive announcement callbacks, implement [IFeedAnnouncementInvokedTarget](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.feeds.providers.ifeedannouncementinvokedtarget) on the same class that implements IFeedProvider. The feed host calls OnAnnouncementInvoked both when it displays an announcement and when the user engages with it. Check the ActionKind property to distinguish these events.

The following example handles engagement separately from the initial display notification.

```csharp
using Microsoft.Windows.Widgets.Feeds.Providers;
using Microsoft.Windows.Widgets.Notifications;

public sealed class FeedProvider : IFeedProvider, IFeedAnnouncementInvokedTarget
{
    public void OnAnnouncementInvoked(FeedAnnouncementInvokedArgs args)
    {
        if (args.ActionKind == AnnouncementActionKind.Engaged)
        {
            // Use the provider, feed, and announcement IDs in args to identify
            // the associated content and update the dashboard as needed.
        }
    }

    // Implement the IFeedProvider methods.
}
```

When the user engages with the taskbar announcement, the Widgets Board opens the associated dashboard for that feed provider. Use the identifiers provided by FeedAnnouncementInvokedArgs to highlight or otherwise identify the related content.

## Requesting a badge

The FeedAnnouncement API includes a property named ShowBadgeIfUserNotEngaged. The property indicates whether a badge should be displayed on the taskbar if the user has not engaged with the announcement. The default value is false.

When enabled:

1. A feed provider requests an announcement.
1. The host decides whether to display the announcement.
1. If the user does not engage with the announcement, a badge can remain available to indicate that related feed content is waiting to be viewed.
1. Opening the Widgets Board, users can find additional information related to the notification.

Badging is intended to support scenarios where the user does not engage with the initial announcement. The badge provides an additional opportunity for discovery later. The API description for ShowBadgeIfUserNotEngaged explicitly references user engagement state.

The following example demonstrates enabling badge behavior when constructing a feed announcement.

```csharp
using Microsoft.Windows.Widgets.Feeds.Providers;
using Microsoft.Windows.Widgets.Notifications;

void RequestBadgedAnnouncement(
    string feedProviderDefinitionId,
    string feedDefinitionId)
{
    var announcement = new FeedAnnouncement(
        "breaking-news-001",
        "Breaking News",
        "Major event developing",
        new Uri("ms-appx:///Assets/NewsLight.png"),
        new Uri("ms-appx:///Assets/NewsDark.png"));

    announcement.ShowBadgeIfUserNotEngaged = true;

    FeedManager.GetDefault().TryShowAnnouncement(
        feedProviderDefinitionId,
        feedDefinitionId,
        announcement);
}
```

## Announcement content guidance

Use announcements for content that is timely, useful, and directly connected to feed content that the user can inspect in the Widgets Board.

When building announcement content:

- Keep primary and secondary text concise so the announcement remains glanceable.
- Provide light and dark mode icons so the announcement can display appropriately in the current OS theme. The FeedAnnouncement API includes light and dark mode icon URI properties.
- Provide CustomAccessibilityText when the visual text or iconography needs a clearer screen reader description. The FeedAnnouncement API includes custom accessibility text.
- Set expiration and duration only when those values are meaningful for the announcement. The FeedAnnouncement API includes optional expiration time and duration properties.

## Design recommendations

### Use badges sparingly

Reserve badges for content that remains relevant after the original announcement. Overuse may reduce the effectiveness of notification surfaces.

### Provide meaningful content

Badged announcements should lead to content that provides additional value beyond the announcement headline.

### Support accessibility

Include meaningful primary text, secondary text, and custom accessibility text when appropriate. The FeedAnnouncement API supports custom accessibility text for screen-reader scenarios.

### Provide theme-aware icons

Specify both light-mode and dark-mode icons. The FeedAnnouncement class provides dedicated properties for each theme, and the documentation notes PNG support for announcement icons.

## Widgets Board processing and policy behavior

The Widgets Board controls whether a requested announcement is displayed through announcement queueing, prioritization, and validation, where display is managed by the announcement infrastructure. The API description for TryShowAnnouncement explicitly states that the announcement may or may not show based on policies. Because policy determines final display, apps should not assume that every call to TryShowAnnouncement produces a visible taskbar announcement.

## Limitations and considerations

- The Widgets Board may choose not to show an announcement based on policy.
- Providers should not assume a badge will always be shown.
- Providers should not depend on badge visibility for critical application workflows.
- Applications should continue to provide access to the associated feed provider content through normal dashboard experiences.
- Feed providers are limited to 12 announcements and 7 badges daily.

## Related content

- [Dashboards and feed providers](feed-providers.md)
- [Microsoft.Windows.Widgets.Notifications namespace](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.notifications)
- [FeedAnnouncement class](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.notifications.feedannouncement)
