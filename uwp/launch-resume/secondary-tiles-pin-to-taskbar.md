---
description: Learn how to pin secondary tiles to the taskbar, giving your users quick access to content within your app.
title: Pin secondary tiles to taskbar
label: Pin secondary tiles to taskbar
template: detail.hbs
ms.date: 08/08/2024
ms.topic: article
keywords: windows 10, uwp, pin to taskbar, secondary tile, pin secondary tiles to taskbar, shortcut
ms.localizationpriority: medium
---

# Pin secondary tiles to taskbar

[!INCLUDE [notes](includes/live-tiles-note.md)]

Just like pinning secondary tiles to Start, you can pin secondary tiles to the taskbar, giving your users quick access to content within your app.

<img alt="Taskbar pinning" src="images/pin-secondary-ui.png" width="972"/>

> [!IMPORTANT]
> **Limited Access API**: This API is a limited access feature. To use this API, please contact [taskbarsecondarytile@microsoft.com](mailto:taskbarsecondarytile@microsoft.com?Subject=Limited%20Access%20permission%20to%20use%20secondary%20tiles%20on%20taskbar).

> **Requires October 2018 Update**: You must target SDK 17763 and be running build 17763 or later to pin to taskbar.

## Guidance

A secondary tile provides a consistent, efficient way for users to directly access specific areas within an app. Although a user chooses whether or not to "pin" a secondary tile to the taskbar, the pinnable areas in an app are determined by the developer. For more guidance, see [Secondary tile guidance](secondary-tiles-guidance.md).

## 1. Determine if API exists and unlock Limited-Access

Older devices don't have the taskbar pinning APIs (if you're targeting older versions of Windows 10). Therefore, you shouldn't display a pin button on these devices that aren't capable of pinning.

Additionally, this feature is locked under Limited-Access. To gain access, contact Microsoft. API calls to [TaskbarManager.RequestPinSecondaryTileAsync](/uwp/api/windows.ui.shell.taskbarmanager.requestpinsecondarytileasync), [TaskbarManager.IsSecondaryTilePinnedAsync](/uwp/api/windows.ui.shell.taskbarmanager.issecondarytilepinnedasync), and [TaskbarManager.TryUnpinSecondaryTileAsync](/uwp/api/windows.ui.shell.taskbarmanager.tryunpinsecondarytileasync) will fail with an Access Denied exception. Apps are not allowed to use this API without permission, and the API definition may change at any time.

Use the [ApiInformation.IsMethodPresent](/uwp/api/windows.foundation.metadata.apiinformation.ismethodpresent) method to determine if the APIs are present. And then use the [LimitedAccessFeatures](/uwp/api/windows.applicationmodel.limitedaccessfeatures) API to try unlocking the API.

```csharp
if (ApiInformation.IsMethodPresent("Windows.UI.Shell.TaskbarManager", "RequestPinSecondaryTileAsync"))
{
    // API present!
    // Unlock the pin to taskbar feature
    var result = LimitedAccessFeatures.TryUnlockFeature(
        "com.microsoft.windows.secondarytilemanagement",
        "<tokenFromMicrosoft>",
        "<publisher> has registered their use of com.microsoft.windows.secondarytilemanagement with Microsoft and agrees to the terms of use.");

    // If unlock succeeded
    if ((result.Status == LimitedAccessFeatureStatus.Available) ||
        (result.Status == LimitedAccessFeatureStatus.AvailableWithoutToken))
    {
        // Continue
    }
    else
    {
        // Don't show pin to taskbar button or call any of the below APIs
    }
}

else
{
    // Don't show pin to taskbar button or call any of the below APIs
}
```

## 2. Get the TaskbarManager instance

Windows apps can run on a wide variety of devices; not all of them support the taskbar. Right now, only Desktop devices support the taskbar. Additionally, presence of the taskbar might come and go. To check whether taskbar is currently present, call the [TaskbarManager.GetDefault](/uwp/api/windows.ui.shell.taskbarmanager.getdefault) method and check that the instance returned is not null. Don't display a pin button if the taskbar isn't present.

We recommend holding onto the instance for the duration of a single operation, like pinning, and then grabbing a new instance the next time you need to do another operation.

```csharp
TaskbarManager taskbarManager = TaskbarManager.GetDefault();

if (taskbarManager != null)
{
    // Continue
}
else
{
    // Taskbar not present, don't display a pin button
}
```

## 3. Check whether your tile is currently pinned to the taskbar

If your tile is already pinned, you should display an unpin button instead. You can use the [IsSecondaryTilePinnedAsync](/uwp/api/windows.ui.shell.taskbarmanager.issecondarytilepinnedasync) method to check whether your tile is currently pinned (users can unpin it at any time). In this method, you pass the `TileId` of the tile you want to know is pinned.

```csharp
if (await taskbarManager.IsSecondaryTilePinnedAsync("myTileId"))
{
    // The tile is already pinned. Display the unpin button.
}

else 
{
    // The tile is not pinned. Display the pin button.
}
```

## 4. Check whether pinning is allowed

Pinning to the taskbar can be disabled by Group Policy. The [TaskbarManager.IsPinningAllowed](/uwp/api/windows.ui.shell.taskbarmanager.ispinningallowed) property lets you check whether pinning is allowed.

When the user clicks your pin button, you should check this property, and if it's false, you should display a message dialog informing the user that pinning is not allowed on this machine.

```csharp
TaskbarManager taskbarManager = TaskbarManager.GetDefault();
if (taskbarManager == null)
{
    // Display message dialog informing user that taskbar is no longer present, and then hide the button
}

else if (taskbarManager.IsPinningAllowed == false)
{
    // Display message dialog informing user pinning is not allowed on this machine
}

else
{
    // Continue pinning
}
```

## 5. Construct and pin your tile

The user has clicked your pin button, and you've determined that the APIs are present, taskbar is present, and pinning is allowed... time to pin!

First, construct your secondary tile just like you would when pinning to Start. You can learn more about the secondary tile properties by reading [Pin secondary tiles to Start](secondary-tiles-pinning.md). However, when pinning to taskbar, in addition to the previously required properties, Square44x44Logo (this is the logo used by taskbar) is also required. Otherwise, an exception will be thrown.

Then, pass the tile to the [RequestPinSecondaryTileAsync](/uwp/api/windows.ui.shell.taskbarmanager.requestpinsecondarytileasync) method. Since this is under limited-access, this will not display a confirmation dialog and does not require a UI thread. But in the future when this is opened up beyond limited-access, callers not utilizing limited-access will receive a dialog and be required to use the UI thread.

```csharp
// Initialize the tile (all properties below are required)
SecondaryTile tile = new SecondaryTile("myTileId");
tile.DisplayName = "PowerPoint 2016 (Remote)";
tile.Arguments = "app=powerpoint";
tile.VisualElements.Square44x44Logo = new Uri("ms-appdata:///AppIcons/PowerPoint_Square44x44Logo.png");
tile.VisualElements.Square150x150Logo = new Uri("ms-appdata:///AppIcons/PowerPoint_Square150x150Logo.png");

// Pin it to the taskbar
bool isPinned = await taskbarManager.RequestPinSecondaryTileAsync(tile);
```

This method returns a boolean value that indicates whether your tile is now pinned to the taskbar. If your tile was already pinned, the method updates the existing tile and returns `true`. If pinning wasn't allowed or taskbar isn't supported, the method returns `false`.

## Enumerate tiles

To see all the tiles that you created and are still pinned somewhere (Start, taskbar, or both), use [FindAllAsync](/uwp/api/windows.ui.startscreen.secondarytile.findallasync). You can subsequently check whether these tiles are pinned to the taskbar and/or Start. If the surface isn't supported, these methods return false.

```csharp
var taskbarManager = TaskbarManager.GetDefault();
var startScreenManager = StartScreenManager.GetDefault();

// Look through all tiles
foreach (SecondaryTile tile in await SecondaryTile.FindAllAsync())
{
    if (taskbarManager != null && await taskbarManager.IsSecondaryTilePinnedAsync(tile.TileId))
    {
        // Tile is pinned to the taskbar
    }

    if (startScreenManager != null && await startScreenManager.ContainsSecondaryTileAsync(tile.TileId))
    {
        // Tile is pinned to Start
    }
}
```

## Update a tile

To update an already pinned tile, you can use the [**SecondaryTile.UpdateAsync**](/uwp/api/windows.ui.startscreen.secondarytile.updateasync) method as described in [Updating a secondary tile](secondary-tiles-pinning.md#updating-a-secondary-tile).

## Unpin a tile

Your app should provide an unpin button if the tile is currently pinned. To unpin the tile, simply call [TryUnpinSecondaryTileAsync](/uwp/api/windows.ui.shell.taskbarmanager.tryunpinsecondarytileasync), passing in the `TileId` of the secondary tile you would like unpinned.

This method returns a boolean value that indicates whether your tile is no longer pinned to the taskbar. If your tile wasn't pinned in the first place, this also returns `true`. If unpinning wasn't allowed, this returns `false`.

If your tile was only pinned to taskbar, this will delete the tile since it is no longer pinned anywhere.

```csharp
var taskbarManager = TaskbarManager.GetDefault();
if (taskbarManager != null)
{
    bool isUnpinned = await taskbarManager.TryUnpinSecondaryTileAsync("myTileId");
}
```

## Delete a tile

If you want to unpin a tile from everywhere (Start, taskbar), use the [RequestDeleteAsync](/uwp/api/windows.ui.startscreen.secondarytile.requestdeleteasync) method.

This is appropriate for cases where the content the user pinned is no longer applicable. For example, if your app lets you pin a notebook to Start and taskbar, and then the user deletes the notebook, you should simply delete the tile associated with the notebook.

```csharp
// Initialize a secondary tile with the same tile ID you want removed.
// Or, locate it with FindAllAsync()
SecondaryTile toBeDeleted = new SecondaryTile(tileId);

// And then delete the tile
await toBeDeleted.RequestDeleteAsync();
```

## Unpin only from Start

If you only want to unpin a secondary tile from Start while leaving it on Taskbar, you can call the [StartScreenManager.TryRemoveSecondaryTileAsync](/uwp/api/windows.ui.startscreen.startscreenmanager.tryremovesecondarytileasync) method. This will similarly delete the tile if it is no longer pinned to any other surfaces.

This method returns a boolean value that indicates whether your tile is no longer pinned to Start. If your tile wasn't pinned in the first place, this also returns `true`. If unpinning wasn't allowed or Start isn't supported, this returns `false`.

```csharp
await StartScreenManager.GetDefault().TryRemoveSecondaryTileAsync("myTileId");
```

## Resources

* [TaskbarManager class](/uwp/api/windows.ui.shell.taskbarmanager)
* [Pin secondary tiles to Start](secondary-tiles-pinning.md)
