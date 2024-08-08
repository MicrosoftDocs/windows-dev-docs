---
description: Learn about when and where you should use secondary tiles in your Windows app.
title: Secondary tiles design guidance
label: Secondary tiles
template: detail.hbs
ms.date: 08/08/2024
ms.topic: article
keywords: windows 10, uwp, secondary tiles, guidance, guidelines, best practices
ms.localizationpriority: medium
---
# Secondary tile guidance

[!INCLUDE [notes](includes/live-tiles-note.md)]

A secondary tile provides a consistent, efficient way for users to directly access specific areas within an app from the Start menu. Although a user chooses whether or not to "pin" a secondary tile to the Start menu, the pinnable areas in an app are determined by the developer. For a more detailed summary, see [Secondary tiles overview](secondary-tiles.md). Consider these guidelines when you enable secondary tiles and design the associated UI in your app.

> [!NOTE]
> Only users can pin a secondary tile to the Start menu; apps can't programmatically pin secondary tiles. Users also control tile removal, and can remove a secondary tile from the Start menu or from within the parent app.

## Recommendations

Consider the following recommendations when enabling secondary tiles in your app:

* When the content in focus is pinnable, the app bar should contain a "Pin to Start" button to create a secondary tile for the user.
* When the user clicks "Pin to Start", you should immediately call the API from the UI thread to [pin the secondary tile](secondary-tiles-pinning.md).
* If the content in focus is already pinned, replace the "Pin to Start" button on the app bar with an "Unpin from Start" button. The "Unpin from Start" button should remove the existing secondary tile.
* When the content in focus is not pinnable, don't show a "Pin to Start" button (or show a disabled "Pin to Start" button).
* Use the system-provided glyphs for your "Pin to Start" and "Unpin from Start" buttons (see the pin and unpin members in Windows.UI.Xaml.Controls.Symbol or WinJS.UI.AppBarIcon).
* Use the standard button text: "Pin to Start" and "Unpin from Start". You'll have to override the default text when using the system-provided pin and unpin glyphs.
* Don't use a secondary tile as a virtual command button to interact with the parent app, such as a "skip to next track" tile.

## Additional usage guidance for devs

* When an app launches, it should always enumerate its secondary tiles, in case there were any additions or deletions of which it was unaware. When a secondary tile is deleted through the Start screen app bar, Windows simply removes the tile. The app itself is responsible for releasing any resources that were used by the secondary tile. When secondary tiles are copied through the cloud, current tile or badge notifications on the secondary tile, scheduled notifications, push notification channels, and Uniform Resource Identifiers (URIs) used with periodic notifications are not copied with the secondary tile and must be set up again.
* An app should use meaningful, re-creatable, unique IDs for secondary tiles. Using predictable secondary tile IDs that are meaningful to an app helps the app understand what to do with these tiles when they are seen in a fresh installation on a new computer.
  * At runtime, the app can query whether a specific tile exists.
  * The secondary tile platform can be asked to return the set of all secondary tiles belonging to a specific app. Using meaningful, unique IDs for these tiles can help the app examine the set of secondary tiles and perform appropriate actions. For instance, for a social media app, IDs could identify individual contacts for whom tiles were created.
* Secondary tiles, like all tiles on the Start screen, are dynamic outlets that can be frequently updated with new content. Secondary tiles can surface notifications and updates by using the same mechanisms as any other tile. See [Choose a notification delivery method](/windows/apps/design/shell/tiles-and-notifications/choosing-a-notification-delivery-method) to learn more.

## Related

* [Secondary tiles overview](secondary-tiles.md)
* [Pin secondary tiles](secondary-tiles-pinning.md)
* [Tile assets](/windows/apps/design/style/iconography/overview)
* [Tile content documentation](create-adaptive-tiles.md)
* [Send a local tile notification](sending-a-local-tile-notification.md)
