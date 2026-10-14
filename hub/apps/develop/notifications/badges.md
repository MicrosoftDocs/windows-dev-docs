---
description: Learn how to use badge notifications to show summary or status information on your app's taskbar icon with the Windows App SDK or UWP APIs.
title: Badge notifications for Windows apps
ms.assetid: 48ee4328-7999-40c2-9354-7ea7d488c538
label: Tiles, badges, and notifications
template: detail.hbs
ms.date: 10/13/2026
ms.topic: how-to
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Badge notifications for Windows apps

A notification badge conveys summary or status information specific to your app. They can be numeric (1-99) or one of a set of system-provided glyphs. Examples of information best conveyed through a badge include network connection status in an online game, user status in a messaging app, number of unread mails in a mail app, and number of new posts in a social media app. 

Notification badges appear on your app's taskbar icon and in the lower-right corner of its start tile, regardless of whether the app is running. Badges can be displayed on all tile sizes.  

> [!NOTE]
> You cannot provide your own badge image; only system-provided badge images can be used.


## Numeric badges

Value | Badge | XML
--|--|--
A number from 1 to 99. A value of 0 is equivalent to the glyph value "none" and will clear the badge. | <img src="images/badges/badge-numeric.png" alt="A numeric badge less than 100." /> | `<badge value="1"/>`
Any number greater than 99. | <img src="images/badges/badge-numeric-greater.png" alt="A numeric badge greater than 99." /></td> | `<badge value="100"/>`

## Glyph badges
Instead of a number, a badge can display one of a non-extensible set of status glyphs. 

Status | Glyph | XML
--|--|--
none | (No badge shown.) | `<badge value="none"/>`
activity | :::image source="images/badges/badge-activity.png" alt-text="A glyph badge denoting the 'activity' status."::: | `<badge value="activity"/>`
alarm | :::image source="images/badges/badge-alarm.png" alt-text="A glyph badge denoting the 'alarm' status."::: | `<badge value="alarm"/>`
alert | :::image source="images/badges/badge-alert.png" alt-text="A glyph badge denoting the 'alert' status."::: | `<badge value="alert"/>`
attention | :::image source="images/badges/badge-attention.png" alt-text="A glyph badge denoting the 'attention' status."::: | `<badge value="attention"/>`
available | :::image source="images/badges/badge-available.png" alt-text="A glyph badge denoting the 'available' status."::: | `<badge value="available"/>`
away | :::image source="images/badges/badge-away.png" alt-text="A glyph badge denoting the 'away' status."::: | `<badge value="away"/>`
busy | :::image source="images/badges/badge-busy.png" alt-text="A glyph badge denoting the 'busy' status."::: | `<badge value="busy"/>`
error | :::image source="images/badges/badge-error.png" alt-text="A glyph badge denoting the 'error' status."::: | `<badge value="error"/>`
newMessage | :::image source="images/badges/badge-newMessage.png" alt-text="A glyph badge denoting the 'newMessage' status."::: | `<badge value="newMessage"/>`
paused | :::image source="images/badges/badge-paused.png" alt-text="A glyph badge denoting the 'paused' status."::: | `<badge value="paused"/>`
playing | :::image source="images/badges/badge-playing.png" alt-text="A glyph badge denoting the 'playing' status."::: | `<badge value="playing"/>`
unavailable | :::image source="images/badges/badge-unavailable.png" alt-text="A glyph badge denoting the 'unavailable' status."::: | `<badge value="unavailable"/>`</td>

## Set a badge in a Windows App SDK app

Starting in Windows App SDK 1.7, use the [BadgeNotificationManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.badgenotifications.badgenotificationmanager) class in the `Microsoft.Windows.BadgeNotifications` namespace to set and clear badges. This is the recommended approach for Windows App SDK apps. Instead of building an XML payload, you call a method and pass a number or a [BadgeNotificationGlyph](/windows/windows-app-sdk/api/winrt/microsoft.windows.badgenotifications.badgenotificationglyph) value.

> [!div class="checklist"]
>
> - **Important APIs**: [BadgeNotificationManager class](/windows/windows-app-sdk/api/winrt/microsoft.windows.badgenotifications.badgenotificationmanager), [BadgeNotificationGlyph enum](/windows/windows-app-sdk/api/winrt/microsoft.windows.badgenotifications.badgenotificationglyph)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the BadgeNotificationManager in action](winui3gallery://item/BadgeNotificationManager)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

> [!IMPORTANT]
> `BadgeNotificationManager` requires package identity. If your app doesn't have package identity, for example because it's unpackaged, calls to `SetBadgeAsCount`, `SetBadgeAsGlyph`, and `ClearBadge` throw an exception. For more info, see [An overview of Package Identity in Windows apps](../../desktop/modernize/package-identity-overview.md).

The badge appears on your app's taskbar icon. If the app is pinned to the taskbar, the badge appears even when the app isn't running. To get the `BadgeNotificationManager` instance for your app, use the [Current](/windows/windows-app-sdk/api/winrt/microsoft.windows.badgenotifications.badgenotificationmanager.current) property.

The following examples are based on the WinUI 3 Gallery sample. To use them, add a `using Microsoft.Windows.BadgeNotifications;` directive to your code file.

### Set a numeric badge

Call [SetBadgeAsCount](/windows/windows-app-sdk/api/winrt/microsoft.windows.badgenotifications.badgenotificationmanager.setbadgeascount) and pass the number to show. If the number is greater than 99, the badge shows "99+".

```csharp
private void SetBadgeCountButton_Click(object sender, RoutedEventArgs e)
{
    // Show the number 5 on the app's taskbar icon.
    BadgeNotificationManager.Current.SetBadgeAsCount(5);
}
```

### Set a glyph badge

Call [SetBadgeAsGlyph](/windows/windows-app-sdk/api/winrt/microsoft.windows.badgenotifications.badgenotificationmanager.setbadgeasglyph) and pass a `BadgeNotificationGlyph` value. The enumeration values correspond to the glyphs in the [Glyph badges](#glyph-badges) table.

```csharp
private void SetBadgeGlyphButton_Click(object sender, RoutedEventArgs e)
{
    // Show the alert glyph on the app's taskbar icon.
    BadgeNotificationManager.Current.SetBadgeAsGlyph(BadgeNotificationGlyph.Alert);
}
```

### Clear the badge

Call [ClearBadge](/windows/windows-app-sdk/api/winrt/microsoft.windows.badgenotifications.badgenotificationmanager.clearbadge) to remove the badge. Setting a count of 0, or the `BadgeNotificationGlyph.None` glyph, also clears the badge.

```csharp
private void ClearBadgeButton_Click(object sender, RoutedEventArgs e)
{
    BadgeNotificationManager.Current.ClearBadge();
}
```

## Create a badge with the UWP BadgeUpdateManager API

These examples use the [BadgeUpdateManager](/uwp/api/windows.ui.notifications.badgeupdatemanager) class in the Windows Runtime `Windows.UI.Notifications` namespace, which is the badge API for UWP apps. You build the badge as an XML payload, using the values in the previous tables. If your app uses Windows App SDK 1.7 or later, use [BadgeNotificationManager](#set-a-badge-in-a-windows-app-sdk-app) instead.

### Create a numeric badge

````csharp
private void setBadgeNumber(int num)
{

    // Get the blank badge XML payload for a badge number
    XmlDocument badgeXml = 
        BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);

    // Set the value of the badge in the XML to our number
    XmlElement badgeElement = badgeXml.SelectSingleNode("/badge") as XmlElement;
    badgeElement.SetAttribute("value", num.ToString());

    // Create the badge notification
    BadgeNotification badge = new BadgeNotification(badgeXml);

    // Create the badge updater for the application
    BadgeUpdater badgeUpdater = 
        BadgeUpdateManager.CreateBadgeUpdaterForApplication();

    // And update the badge
    badgeUpdater.Update(badge);

}
````

### Create a glyph badge
````csharp
private void updateBadgeGlyph()
{
    string badgeGlyphValue = "alert";

    // Get the blank badge XML payload for a badge glyph
    XmlDocument badgeXml = 
        BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeGlyph);

    // Set the value of the badge in the XML to our glyph value
    Windows.Data.Xml.Dom.XmlElement badgeElement = 
        badgeXml.SelectSingleNode("/badge") as Windows.Data.Xml.Dom.XmlElement;
    badgeElement.SetAttribute("value", badgeGlyphValue);

    // Create the badge notification
    BadgeNotification badge = new BadgeNotification(badgeXml);

    // Create the badge updater for the application
    BadgeUpdater badgeUpdater = 
        BadgeUpdateManager.CreateBadgeUpdaterForApplication();

    // And update the badge
    badgeUpdater.Update(badge);

}
````

### Clear a badge

````csharp
private void clearBadge()
{
    BadgeUpdateManager.CreateBadgeUpdaterForApplication().Clear();
}
````

## Get the sample code

* [Windows App SDK notifications sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Notifications)<br/> Shows how to send app notifications and badge updates. 

## Related articles

* [App notification content](app-notifications/app-notifications-content.md)
* [Info badge](../ui/controls/info-badge.md)
* [BadgeNotificationManager class](/windows/windows-app-sdk/api/winrt/microsoft.windows.badgenotifications.badgenotificationmanager)
* [Create tiles](/windows/uwp/launch-resume/creating-tiles)
* [Create adaptive tiles](/windows/uwp/launch-resume/create-adaptive-tiles)
