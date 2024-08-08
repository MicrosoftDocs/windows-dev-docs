---
description: Secondary tiles allow users to pin specific content and deep links from your app onto their Start menu, providing easy future access to the content within your app.
title: Secondary tiles
label: Secondary tiles
template: detail.hbs
ms.date: 08/08/2024
ms.topic: article
keywords: windows 10, uwp, secondary tiles
ms.localizationpriority: medium
---
# Secondary tiles

[!INCLUDE [notes](includes/live-tiles-note.md)]

Secondary tiles allow users to pin specific content and deep links from your app onto their Start menu, providing easy future access to the content within your app.

![Screenshot of secondary tiles](images/secondarytiles.png)

For example, users can pin the weather for numerous specific locations on their Start menu, which provides (1) easy live glanceable information about the current weather thanks to Live Tiles, and (2) a quick entry point to the specific city's weather they care about. Users can also pin specific stocks, news articles, and more items that are important to them.

By adding secondary tiles to your app, you help the user re-engage quickly and efficiently with your app, encouraging them to return more often thanks to the easy access that secondary tiles provides.

**Only users can pin a secondary tile; apps cannot pin secondary tiles programmatically without user approval**. The user must explicitly click a "Pin" button within your app, at which point you then use the API to request to create a secondary tile, and then the system displays a dialog box asking the user to confirm whether they would like the tile pinned.

## Quick links

| Article | Description |
| --- | --- |
| [Guidance on secondary tiles](secondary-tiles-guidance.md) | Learn about when and where you should use secondary tiles. |
| [Pin secondary tiles](secondary-tiles-pinning.md) | Learn how to pin a secondary tile. |
| [Pin from desktop apps](/windows/apps/design/shell/tiles-and-notifications/secondary-tiles-desktop-pinning) | Desktop apps can pin secondary tiles thanks to the Desktop Bridge! |

## Secondary tiles in relation to primary tiles

Secondary tiles are associated with a single parent app. They are pinned to the Start menu to provide a user with a consistent and efficient way to launch directly into a frequently used area of the parent app. This can be either a general subsection of the parent app that contains frequently updated content, or a deep link to a specific area in the app.

Examples of secondary tile scenarios include:

* Weather updates for a specific city in a weather app
* A summary of upcoming events in a calendar app
* Status and updates from an important contact in a social app
* Specific feeds in an RSS reader
* A music playlist
* A blog

Any frequently changing content that a user wants to monitor is a good candidate for a secondary tile. After the secondary tile is pinned, users can receive at-a-glance updates through the tile and use it to launch directly into the parent app.

Secondary tiles are similar to primary tiles in many ways:

* They use tile notifications to display rich content.
* They must include a 150 x 150 pixel logo for the default tile content.
* They can optionally include the other logo sizes to enable larger tile sizes.
* They can show notifications and badges.
* They can be rearranged on the Start menu.
* They are automatically deleted when the app is uninstalled.
* Their badge and lock detailed status text can be shown on lock.

However, secondary tiles differ from primary tiles in some noticeable ways:

* Users can delete their secondary tiles at any time without deleting the parent app.
* Secondary tiles can be created at run time. App tiles can be created only during installation.
* A flyout prompts the user for confirmation before adding a secondary tile.
* They cannot be programmatically selected for the lock screen through a request to the user. The user must manually add the secondary tile through the Personalize page in PC Settings.

For sending notifications, specific methods are provided for tile and badge updaters and push notification channels used with secondary tiles. These parallel the versions used with primary tiles. For instance, CreateBadgeUpdaterForApplication vs. CreateBadgeUpdaterForSecondaryTile.

## Guidance on secondary tiles

To learn about when and where you should use secondary tiles, and other usage guidance, please see [Guidance for secondary tiles](secondary-tiles-guidance.md)

## Pinning secondary tiles

To learn how to pin secondary tiles, please see [Pin secondary tiles](secondary-tiles-pinning.md).

## Desktop applications and secondary tiles

To learn how to use secondary tiles from your desktop application via the Desktop Bridge, please see [Pin secondary tiles from desktop apps](/windows/apps/design/shell/tiles-and-notifications/secondary-tiles-desktop-pinning).
