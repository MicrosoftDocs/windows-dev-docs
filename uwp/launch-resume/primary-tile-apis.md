---
description: You can programmatically pin your own app's primary tile to Start, just like you can pin secondary tiles. And you can check whether it's currently pinned.
title: Primary tile API's
label: Primary tile API's
template: detail.hbs
ms.date: 08/08/2024
ms.topic: article
keywords: windows 10, uwp, StartScreenManager, pin primary tile, primary tile apis, check if tile pinned, live tile
ms.localizationpriority: medium
---
# Primary tile APIs

[!INCLUDE [notes](includes/live-tiles-note.md)]

Primary tile APIs let you check whether your app is currently pinned to Start, and request to pin your app's primary tile.

> [!IMPORTANT]
> **Requires Creators Update**: You must target SDK 15063 and be running Windows 10 build 15063 or later to use the primary tile APIs.

> **Important APIs**: [**StartScreenManager class**](/uwp/api/windows.ui.startscreen.startscreenmanager), [ContainsAppListEntryAsync](/uwp/api/windows.ui.startscreen.startscreenmanager#Windows_UI_StartScreen_StartScreenManager_ContainsAppListEntryAsync_Windows_ApplicationModel_Core_AppListEntry_), [RequestAddAppListEntryAsync](/uwp/api/windows.ui.startscreen.startscreenmanager#Windows_UI_StartScreen_StartScreenManager_RequestAddAppListEntryAsync_Windows_ApplicationModel_Core_AppListEntry_)

## When to use primary tile APIs

You put a lot of effort into designing a great experience for your app's primary tile, and now you have the opportunity to ask the user to pin it to Start. But before we dive into the code, here are some things to keep in mind as you're designing your experience:

* **Do** craft a non-disruptive and easily dismissible UX in your app with a clear "Pin Live Tile" call to action.
* **Do** clearly explain the value of your app's Live Tile before asking the user to pin it.
* **Don't** ask a user to pin your app's tile if the tile is already pinned or the device doesn't support it (more info follows).
* **Don't** repeatedly ask the user to pin your app's tile (they will probably get annoyed).
* **Don't** call the pin API without explicit user interaction or when your app is minimized/not open.

## Checking whether the API's exist

If your app supports older versions of Windows 10, you need to check whether these primary tile APIs are available. You do this by using ApiInformation. If the primary tile APIs aren't available, avoid executing any calls to the APIs.

```csharp
if (ApiInformation.IsTypePresent("Windows.UI.StartScreen.StartScreenManager"))
{
    // Primary tile API's supported!
}
else
{
    // Older version of Windows, no primary tile API's
}
```

## Check if Start supports your app

Depending on the current Start menu, and your type of app, pinning your app to the current Start screen might not be supported. For example, IoT or xbox devices do not support pinning to Start. Therefore, before showing any pin UI or executing any pin code, you first need to check if your app is even supported for the current Start screen. If it's not supported, don't prompt the user to pin the tile.

```csharp
// Get your own app list entry
// (which is always the first app list entry assuming you are not a multi-app package)
AppListEntry entry = (await Package.Current.GetAppListEntriesAsync())[0];

// Check if Start supports your app
bool isSupported = StartScreenManager.GetDefault().SupportsAppListEntry(entry);
```

## Check whether you're currently pinned

To find out if your primary tile is currently pinned to Start, use the [ContainsAppListEntryAsync](/uwp/api/windows.ui.startscreen.startscreenmanager.containsapplistentryasync) method.

```csharp
// Get your own app list entry
AppListEntry entry = (await Package.Current.GetAppListEntriesAsync())[0];

// Check if your app is currently pinned
bool isPinned = await StartScreenManager.GetDefault().ContainsAppListEntryAsync(entry);
```

## Pin your primary tile

If your primary tile currently isn't pinned, and your tile is supported by Start, you might want to show a tip to users that they can pin your primary tile.

> [!NOTE]
> You must call this API from a UI thread while your app is in the foreground, and you should only call this API after the user has intentionally requested the primary tile be pinned (for example, after the user clicked yes to your tip about pinning the tile).

If the user clicks your button to pin the primary tile, you call the [RequestAddAppListEntryAsync](/uwp/api/windows.ui.startscreen.startscreenmanager.requestaddapplistentryasync) method to request that your tile be pinned to Start. This will display a dialog asking the user to confirm that they want your tile pinned to Start.

This will return a boolean representing whether your tile is now pinned to Start. If your tile was already pinned, this will immediately return `true` without showing the dialog to the user. If the user clicks "No" on the dialog, or pinning your tile to Start isn't supported, this will return `false`. Otherwise, the user clicked "Yes" and the tile was pinned, and the API will return `true`.

```csharp
// Get your own app list entry
AppListEntry entry = (await Package.Current.GetAppListEntriesAsync())[0];

// And pin it to Start
bool isPinned = await StartScreenManager.GetDefault().RequestAddAppListEntryAsync(entry);
```

## Resources

* [Full code sample on GitHub](https://github.com/WindowsNotifications/quickstart-pin-primary-tile)
* [Pin to taskbar](/windows/apps/design/shell/pin-to-taskbar)
