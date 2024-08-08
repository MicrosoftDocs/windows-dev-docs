---
description: Learn how to use badge notification to conveys summary or status information specific to your app.
title: Badge notifications for Windows apps
ms.assetid: 48ee4328-7999-40c2-9354-7ea7d488c538
label: Tiles, badges, and notifications
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
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
activity | :::image type="icon" source="images/badges/badge-activity.png"::: | `<badge value="activity"/>`
alarm | :::image type="icon" source="images/badges/badge-alarm.png"::: | `<badge value="alarm"/>`
alert | :::image type="icon" source="images/badges/badge-alert.png"::: | `<badge value="alert"/>`
attention | :::image type="icon" source="images/badges/badge-attention.png"::: | `<badge value="attention"/>`
available | :::image type="icon" source="images/badges/badge-available.png"::: | `<badge value="available"/>`
away | :::image type="icon" source="images/badges/badge-away.png"::: | `<badge value="away"/>`
busy | :::image type="icon" source="images/badges/badge-busy.png"::: | `<badge value="busy"/>`
error | :::image type="icon" source="images/badges/badge-error.png"::: | `<badge value="error"/>`
newMessage | :::image type="icon" source="images/badges/badge-newMessage.png"::: | `<badge value="newMessage"/>`
paused | :::image type="icon" source="images/badges/badge-paused.png"::: | `<badge value="paused"/>`
playing | :::image type="icon" source="images/badges/badge-playing.png"::: | `<badge value="playing"/>`
unavailable | :::image type="icon" source="images/badges/badge-unavailable.png"::: | `<badge value="unavailable"/>`</td>

## Create a badge

These examples show you how to create a badge update.

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

* [Notifications sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Notifications)<br/> Shows how to create live tiles, send badge updates, and display toast notifications. 

## Related articles

* [Adaptive and interactive toast notifications](adaptive-interactive-toasts.md)
* [Create tiles](/windows/uwp/launch-resume/creating-tiles)
* [Create adaptive tiles](/windows/uwp/launch-resume/create-adaptive-tiles)
