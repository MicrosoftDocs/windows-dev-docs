---
title: Connected Storage Overview
author: aablackm
description: Learn about using Connected Storage to save and load game data across devices.
ms.assetid: a0bacf59-120a-4ffc-85e1-fbeec5db1308
ms.author: aablackm
ms.date: 02/27/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, connected storage
ms.localizationpriority: low
---

# Connected Storage
Connected Storage is designed to allow your title to save gameplay data and other relevant state data that should roam between devices. The Connected Storage API allows titles on Xbox One and Universal Windows Platform(UWP) to save, load, and delete title data that is stored locally and also synced to the cloud whenever the Xbox One or UWP title is connected to the internet. Saved data will be available on any other device which runs your title after synchronization occurs. Developers are encouraged to save title state as accurately as possible to offer the best away from home play experience. Connected Storage is what allows you to progress in a game at home, then pick up your game right where you left off on any other device that supports the same game.

## Connected Storage features

The Connected Storage API provides the following features:

- Apps can quickly save up to 16 MB of data at a time into a memory buffer, which is then cached locally on the HDD by the system and uploaded to the cloud.
- For managed partners and ID@Xbox developers:
    - 256 MB per user/app of cloud storage.
- For Xbox Live Creators Program developers:
    - 64 MB per user/app of cloud storage.
- Robust response to power failures—apps don't have to deal with partial data being saved.
- Data is automatically uploaded to the cloud, even when the app isn't running.
- Data is available across Xbox One or UWP devices that are connected to Xbox Live.
- Xbox Live handles cross-device syncing and conflict management without requiring involvement by the app.

The Connected Storage system makes sure that all saves are made in their entirety or not at all. This means that as a developer you will never have to worry about partially saved data affecting your game state in case of power failure or the user closing the app suddenly, either manually or by opening another app/game on the console. This also ensures a smoother game play experience for users of your title, as Connected Storage is an important part of what allows users to switch between games and apps quickly and freely but still come back to the original game in the state in which they left. In order to implement these features in your own title you will need to have an understanding of the Connected Storage APIs.

## Connected Storage structure

The Connected Storage system allows apps to store data as one or more blobs in containers. At a high level, all data in the Connected Storage system is associated with a user, or a user or machine in the case of Xbox Development Kit developed titles. All data saved by an app for a particular user or machine is stored in a Connected Storage space. Each user of your app gets a Connected Storage space with a limit of 256 or 64 MB total storage. It's important to note that this storage is dedicated to your app alone, it is not shared with other apps.

### Containers and blobs

The Connected Storage container, or container for short, is the basic unit of storage. Each Connected Storage space can contain numerous containers, as shown in the following diagram.

Connected Storage space (per-title/machine or per-title/user)

![connected_storage_space_containers.png](../../images/connected_storage/connected_storage_space_containers.png)

 Data is stored in containers as one or more buffers called blobs. The following diagram illustrates the internal system representation of containers on disk. For each container, there is a container file that contains references to the data file for each blob in the container.

Diagram of a container

![container_storage_blobs.png](../../images/connected_storage/container_storage_blobs.png)

To store data in a container, call the appropriate APIs container method SubmitUpdatesAsync, providing a map of names and blobs (Buffer objects). All changes described in a SubmitUpdatesAsync call are applied atomically, that is, either all the blobs are updated as requested, or the entire operation is terminated and the container remains in its state prior to the call.

Individual save operations that use SubmitUpdatesAsync are limited to 16 MB of data at a time.

## Connected Storage API

Connected Storage has separate APIs for the XDK and UWP apps. Fortunately, these APIs resemble each other very closely. The two APIs differ mainly in their namespace and class names. The functions required to [save](connected-storage-saving.md), [load](connected-storage-loading.md), and [delete](connected-storage-deleting.md) data with the API are named identically.

Further differences between the two Connected Storage APIs are detailed in the Connected Storage section of [Porting Xbox Live code from XDK to UWP](../../using-xbox-live/porting-xbox-live-code-from-xdk-to-uwp.md).

You can find the XDK Connected Storage APIs documented in the XDK .chm file under the path:
**Xbox ONE XDK >> API Reference >> Platform API Reference >> System API Reference >> Windows.Xbox.Storage**.
The XDK APIs are also documented on the [developer.microsoft.com site](https://developer.microsoft.com/en-us/games/xbox/docs/xdk/storage-xbox-microsoft-n).
The link to XDK APIs requires that you have a Microsoft Account(MSA) that has been enabled for Xbox Developer Kit(XDK) access.
Windows.Xbox.Storage is the name of the Connected Storage namespace for Xbox One consoles.

You can find the UWP Connected Storage APIs documented in the Xbox Live SDK .chm file under the path:
**Xbox Live APIs >> Xbox Live Platform Extensions SDK API Reference >> Windows.Gaming.XboxLive.Storage**.
The UWP Connected Storage APIs are also documented online under the [Windows.Gaming.XboxLive.Storage namespace reference](https://docs.microsoft.com/en-us/uwp/api/windows.gaming.xboxlive.storage).
Windows.Gaming.XboxLive.Storage is the name of the Connected Storage namespace for UWP apps.

To begin using Connected Storage you will need to acquire a Connected Storage *space*. A Connected Storage space is associated with a user or machine and holds all of the Connected Storage data associated with that user or machine in the form of *containers* and *blobs*. Acquiring a Connected Storage space for a machine or user will give you access to read and write to that entities stored data. To acquire a Connected Storage space, both XDK and UWP titles will call the `GetForUserAsync` method, XDK titles may also call the `GetForMachineAsync` method, UWP titles will be unable to call `GetForMachineAsync`. `GetForUserAsync` and `GetForMachineAsync` are contained in the `ConnectedStorageSpace` class in the XDK. `GetForUserAsync` is contained in the `GameSaveProvider` class in the UWP API. These methods are potentially long-running operations, especially if the user has saved data on one device and is resuming gameplay for the first time on another device. `GetForUserAsync` retrieves a Connected Storage Space for a user which you can then use to create, access, and delete containers.

To create a container, or access a previously created container, call the `CreateContainer` function of your `ConnectedStorageSpace` or `GameSaveProvider` class, this will give you access to a named container for the user or machine associated with the `ConnectedStorageSpace` or `GameSaveProvider` used to create the container. The `ConnectedStorageSpace` and `GameSaveProvider` classes also include the `DeleteContainerAsync` function which allows you to delete a Container provided you give the name of the container to be deleted.

To update blobs in your container call `SubmitUpdatesAsync` in the `ConnectedStorageContainer` class in the XDK or in the `GameSaveContainer` class of the UWP API. `SubmitUpdatesAsync` allows you to provide a list of names and buffers as data to be written to the blob, a list of names of blobs to be deleted, and a display name for the calling save container. This is the function that you will ultimately need to call to update data in your Connected Storage space.

To see examples of the Connected Storage APIs in use read the following Connected Storage articles:
[Save Data](connected-storage-saving.md)
[Load Data](connected-storage-loading.md)
[Delete Data](connected-storage-deleting.md)

> [!NOTE]
> A Note on Security:
>
> Universal Windows Platform (UWP) apps running on PCs save local data in a location that is accessible to the local user, and is not inherently protected from user tampering.
>You should consider applying your own encryption and validity checks to game save data in order to help prevent users from modifying the game saves outside of the game.
>For this same reason you should decide if you want to allow PC and Xbox versions of your game to share saves or keep them separated.

## Managing local storage

When testing Connected Storage on your Xbox Development Kit or UWP app, you may need to manipulate the data stored locally on your development device.

The tool xbstorage ships with the XDK and is a command line tool that will allow you to manipulate local storage on your development console.
For UWP developers there is an identical tool for PC called gamesaveutil.exe which ships with the Windows 10 SDK for Fall Creators Update(10.0.16299.91) and later.

Both of these tools allow you to manipulate local storage on your device with these commands:

|Command  |Description  |
|---------|---------|
|reset    | Performs a factory reset on Connected Storage. |
|import   | Imports data from the specified XML file to a Connected Storage space. |
|export   | Exports data from a Connected Storage space to the specified XML file. |
|delete   | Deletes data from a Connected Storage space. |
|generate | Generates dummy data and saves to the specified XML file. |
|simulate | Simulates out of Storage Space conditions. |

To learn more about the functions available in the xbstorage tool and gamesaveutils.exe read [Managing local Connected Storage](connected-storage-xb-storage.md).

## Technical overview

To take a more in depth look at how Connected Storage works on the XDK read the [Connected Storage Technical Overview](connected-storage-technical-overview.md). The technical overview was written specifically for XDK developers but the concepts contained within apply to Connected Storage in general.