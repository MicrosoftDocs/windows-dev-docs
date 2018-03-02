---
title: Connected Storage Best Practices
author: aablackm
description: Recommendations on how to get the best performance and experience from Connected Storage
ms.author: aablackm
ms.date: 02/27/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, Connected Storage
ms.localizationpriority: low
---
# Connected Storage best practices

Developers should divide save data into logical groupings which are independently updateable rather than writing monolithic saves. This allows titles to reduce the amount of data they write in various situations, reducing both local resource consumption and upload bandwidth usage. The API also allows titles to update more than one data item in an atomic operation which is guaranteed to succeed entirely or not take effect at all (for instance in the case of catastrophic failure mid-save).

Because Xbox One allows users to quickly switch among titles, developers should design their title to keep their current state ready to save on short notice in anticipation of receiving a Suspend event, which can happen virtually at any time. The Connected Storage API uses RAM outside of the title reservation as the first point of storage in order to maximize title write speed during the short suspend time window. The system then persists the data to durable storage, reconciles it with any other data writes since the last upload, and schedules data uploads. Once stored and queued for upload, the system is robust to various failures such as network connectivity loss or power failure.

## When to load a user's Connected Storage space data

The execution time for loading a user's Connected Storage data space can vary. Apps should take this action during main execution, rather than in response to a user's starting to sign out or in response to receiving a suspend notification from the system.
Generally, apps should load a Connected Storage space as a user signs in and indicates desire to play, unless the game is in a mode in which it's certain no save functionality is needed. You should also consider aligning the loading of a Connected Storage space with long sequences of data loading, so that the operations can execute in parallel.
Once your app has loaded a user's Connected Storage space data, it should retain this value for future saves. Retaining a Connected Storage space over time does not have negative effects on performance or robustness. Because loading a Connected Storage space causes a synchronization check with the cloud if the system is online, releasing and re-loading a user's Connected Storage space during slow or unreliable network conditions might cause the user to see a synchronization UI until the system times out.
Connected Storage spaces do not need to be freed explicitly to cause cloud synchronization. Once the completion handler specified in a `SubmitUpdatesAsync` call has returned with `AsyncStatus::Completed`, the system takes care of synchronization with the cloud whether or not the app frees the `ConnectedStorageSpace` object.

## When to save

Whenever an app receives a suspend notification, the app should at least save relevant data, enabling the system to return to a contextually appropriate state for the user.

If your game design uses periodic, automatic, or user-initiated saves, Connected Storage can be called more often than when receiving a suspend notification; doing so is a good way to reduce the risk of data loss due to power loss or a crash.
If you're developing your game with the XDK, when a user signs out, the User object for that user remains valid, and at that time the app may perform final save operations by using Connected Storage.

## Robustness

Because saved data is always synchronized to the cloud, a bug in saved data and app code that causes the app to crash could be backed up in the cloud and distributed across devices. To prevent users from having an app that crashes on every launch, design the app to ensure that:

-	Users can reach a point in the app at which they can manage saved state, even if some saved data is malformed.
-	The app can handle corrupt data automatically, recovering as much data as it can and reinitializing everything else to a safe state.

## Use cases for save-game designs

The design of a game saving system that makes the best use of containers in Connected Storage space depends on the type of app:

### Single save

For apps that use a single, campaign-style save system, like a first person shooter:

-	Put all of the data into a single container and always write to the same container, identified by name.
-	Consider exposing an option to reset all data that will clear all of a user's saved data, in case he or she wants to start playing the app from the beginning, with no previous progress being retained.

### Multiple saves

For apps that have a fixed number of save slots, let's say five, there are two ways to use containers to save game data:

-	Store all 5 slots within one fixed-name container by using 1 blob for each save slot. Using this method all 5 slots will be fully synchronized and available, or, in the case that synchronization fails at any point, none of the slots will be synchronized and will remain in their previous state. If a user plays the app offline on two different consoles, saving progress in slot 1 on the first console and in slot 2 on the second console, the user must choose which data to retain on connecting both consoles to Xbox Live; the merge logic for containers will produce a conflict.
-	Store each slot in a container with its own name. This allows independent progress in each slot, even on multiple machines that might be offline. However, if a user cancels partway through a synchronization, it's possible that only some of the slots will be available during that session; some of the containers might not have completed downloading. In such a case, the user is notified that the synchronization was incomplete, and that some of the cloud data isn't on the local console.

Whichever approach is used, the app should provide the user with UI to delete individual saves from slots.

### Warning

**Do not store dependent data across containers.** Do not store data with dependencies across more than one container. Containers can be separated due to incomplete synchronization, power loss, or other error conditions. Data stored in a single container must be self-sufficient and self-consistent.

### Tips

**Do not discourage users from turning off the console or navigating away.** Your title should not discourage users from turning off the console or navigating away from your app when saving. On Xbox 360, if a user turns off the system while your title is saving, the user's data is not saved. On Xbox One, your title receives a suspend event and has 1 second to use the Connected Storage API to save state. The system ensures that data is properly committed to the hard drive before it shuts down completely or enters its low-power state. The same suspension process occurs if the user ejects your title's disc to play another one.

**Retain Connected Storage spaces.** Retain ConnectedStorageSpace objects rather than try to load them every time a read or write event occurs. There are no negative effects on performance or robustness caused by retaining a ConnectedStorageSpace object for an extended time.

**Keep data sizes small.** Keep the size of saved data small. All user data in Connected Storage is uploaded to the cloud when the console is online. Optimize your data formats to ensure minimal delays and bandwidth usage.

**Verify that users don't mind not saving.** Check for OutOfLocalStorage errors returned from GetForUserAsync and SubmitUpdatesAsync, and query users to see if they really want to play without saving. If a user indicates wanting to save games, retry the operation.

**Check the user's quota and prompt to clear space.** Check for a QuotaExceeded error returned by SubmitUpdatesAsync. If your app receives this message, notify users that they cannot save any more data until they have freed up some space and present them with UI that enables them to do so. Each user gets 256 or 64 MB of data per app, and each XDK title gets 64 MB of per-machine storage that is local to the console.

**Save the state of menus for restoration later.** Save menu state and other app settings in addition to saving core game data. If the user plays another app and then comes back to yours, restore them to a contextually appropriate menu state.
Respond to signed-in user changes. Users can sign out while your app is suspended. When the app is resumed, it should determine if the set of signed-in users has changed. Consider navigating to an appropriate location within the app, such as a menu, when this occurs.

**Provide UI for managing saved data.** Your app should provide UI that allows users to manage their saved data within the app. For apps with an automatic save system, the app should offer an option to reset saved data to enable users to reset to a default play state.

**Ensure that users can always reach UI to manage saved games.** Ensure that your app can always reach its management UI for saved games, even in the presence of buggy saved data. If a user's saved data becomes unreadable due to an app bug or data corruption, the app should allow users to recover to a state that doesn't crash or prevent them from playing.