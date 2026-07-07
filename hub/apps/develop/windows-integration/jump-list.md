---
title: Add items to the Windows jump list
description: Add tasks and custom groups to your app's Windows jump list — the shortcut menu shown when a user right-clicks your app on the taskbar.
ms.topic: how-to
ms.date: 07/06/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Add items to the Windows jump list

A *jump list* is the shortcut menu that Windows displays when a user right-clicks your app's icon on the taskbar or in the Start menu. You can populate the jump list with *tasks* — quick app-wide actions such as composing a new message or opening settings — and with *custom groups* of items such as recent projects or pinned documents.

> [!div class="nextstepaction"]
> [Open the WinUI Gallery app and see JumpList in action](winui3gallery://item/JumpList)

> [!IMPORTANT]
> The [JumpList](/uwp/api/windows.ui.startscreen.jumplist) API requires package identity. It works in packaged (MSIX) apps and apps packaged with external location, but is not available in unpackaged apps.

## Prerequisites

- Your app must have package identity: either [packaged with MSIX](/windows/msix/overview) or [packaged with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps). Unpackaged apps are not supported.
- Target Windows 10, version 1607 (Anniversary Update, build 14393) or later.

## Load the current jump list

Before you add or remove items, load the app's current jump list by calling [JumpList.LoadCurrentAsync](/uwp/api/windows.ui.startscreen.jumplist.loadcurrentasync):

```csharp
using Windows.UI.StartScreen;

var jumpList = await JumpList.LoadCurrentAsync();
```

## Add tasks

Tasks are items with an empty `GroupName` property. Windows places them in a built-in **Tasks** section at the bottom of the jump list. Use tasks for common actions that are always relevant regardless of context.

Create a task with [JumpListItem.CreateWithArguments](/uwp/api/windows.ui.startscreen.jumplistitem.createwitharguments), add it to the [Items](/uwp/api/windows.ui.startscreen.jumplist.items) collection, and then save by calling [JumpList.SaveAsync](/uwp/api/windows.ui.startscreen.jumplist.saveasync). When the user selects the task, Windows launches your app with the argument string you supplied, which you can read from the activation arguments at startup.

```csharp
var jumpList = await JumpList.LoadCurrentAsync();

var task = JumpListItem.CreateWithArguments("/compose", "New Message");
task.Description = "Compose a new message";
task.Logo = new Uri("ms-appx:///Assets/Tiles/AppList.targetsize-48.png");

jumpList.Items.Add(task);
await jumpList.SaveAsync();
```

## Add items to a custom group

Custom groups let you organize related items under a named heading. Set the [GroupName](/uwp/api/windows.ui.startscreen.jumplistitem.groupname) property to a non-empty string; all items sharing the same `GroupName` appear together under that heading.

```csharp
var jumpList = await JumpList.LoadCurrentAsync();

var item = JumpListItem.CreateWithArguments("/project-alpha", "Project Alpha");
item.GroupName = "Projects";
item.Description = "Open Project Alpha";
item.Logo = new Uri("ms-appx:///Assets/Tiles/AppList.targetsize-48.png");

jumpList.Items.Add(item);
await jumpList.SaveAsync();
```

## Clear all items

To remove all items you have added, clear the [Items](/uwp/api/windows.ui.startscreen.jumplist.items) collection before saving:

```csharp
var jumpList = await JumpList.LoadCurrentAsync();
jumpList.Items.Clear();
await jumpList.SaveAsync();
```

Windows preserves the system-managed sections (such as **Recent** and **Frequent**) regardless of this call; only your custom tasks and groups are cleared.

## Related articles

- [Pin your app to the taskbar](pin-to-taskbar.md)
- [JumpList class (API reference)](/uwp/api/windows.ui.startscreen.jumplist)
- [JumpListItem class (API reference)](/uwp/api/windows.ui.startscreen.jumplistitem)
- [Packaging overview (MSIX)](/windows/msix/overview)
