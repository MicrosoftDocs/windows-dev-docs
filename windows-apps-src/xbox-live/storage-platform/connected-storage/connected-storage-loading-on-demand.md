---
title: Connected Storage loading on demand
author: KevinAsgari
description: Learn how to load Connected Storage data on demand, instead of all at once.
ms.assetid: a0797a14-c972-4017-864c-c6ba0d5a3363
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, connected storage
localizationpriority: medium
---

# Connected Storage loading on demand

*GetSyncOnDemandForUserAsync* allows you to load cloud-backed data from a connected storage space "on demand" rather than all at once. This can improve performance over *GetForUserAsync* for cases where file saves are particularly large.

### To load data from a connected storage space on demand

1.  Call *GetSyncOnDemandForUserAsync*. This triggers a partial sync that downloads a list of containers and their metadata from the cloud, but not their contents. This operation is fast and, under good network conditions, the user should not see any loading UI.

2.  Perform a container query using *GetContainerInfo2Async*. This will return a collection of *ContainerInfo2*, which contains 3 new metadata fields.

    -   *DisplayName*: Any display name you have written using the overload of *SubmitUpdatesAsync* that takes a display name string as a parameter. We suggest always using this field to store a user-friendly name.
    -   *LastModifiedTime*: The last time this container was modified. Note that in the case of conflicting local and remote timestamps, the remote ones is used.
    -   *NeedsSync*: A boolean indicating if this container has data that needs to be downloaded from the cloud.

    Using this additional metadata, you can present the user with core information about their game saves (including name, last time used, and whether selecting one will require a download) without actually performing a full download from the cloud.

3.  Trigger a sync by calling any of the following existing connected storage API.

    -   *BlobInfoQueryResult::GetBlobInfoAsync*
    -   *BlobInfoQueryResult::GetItemCountAsync*
    -   *ConnectedStorageContainer::GetAsync*
    -   *ConnectedStorageContainer::ReadAsync*
    -   *ConnectedStorageSpace::DeleteContainerAsync*

    This will cause the user to see the usual sync UI and progress bar as data from their selected container is downloaded. Note that only data from the selected container is synchronized; data from other containers is not downloaded.

    When calling these API in the context of an on demand sync, these operations can all produce the following new error codes:

    -   *ConnectedStorageErrorStatus::ContainerSyncFailed*: This error indicates that the operation failed and the container could not be synced with the cloud. The most likely cause is the user's network conditions caused the sync to fail. They may want to try again after they've fixed their network or they may choose to use a container they don't have to sync. Your UI should allow either of these options. No retry dialog is required, since they will have already seen the system UI retry dialog.

    -   *ConnectedStorageErrorStatus::ContainerNotInSync*: This error indicates that your title incorrectly tried to write to an unsynced container. Calling *ConnectedStorageContainer::SubmitUpdates* on a container that has the NeedsSync flag set to true is NOT allowed. You must first read a container to trigger a sync before writing to it. If you write to a container without reading from it, it is likely your title has a bug where you could lose user progress.

        This behavior is different from when a user plays offline. While offline, there is no indication of whether containers are synchronized, so it is up to the user to resolve any conflicts at a later time. In this case, however, the system knows the user needs to sync, so it will not allow them to get into a bad state by using an out-of-date container (though if they desire, they can still restart the title and play it offline).

4.  Use the rest of the connected storage API as normal. Its behavior remains unchanged when synchronizing on demand.
