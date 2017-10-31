---
title: Managing local Connected Storage
author: KevinAsgari
description: Learn how to manage local Connected Storage data in a development environment.
ms.assetid: 630cb5fc-5d48-4026-8d6c-3aa617d75b2e
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, connected storage
localizationpriority: medium
---

# Managing local Connected Storage

*xbStorage* is a development tool that allows managing the local data for Connected Storage on an Xbox One dev kit from a development PC.

It allows clearing, from the hard drive, the local data from Connected Storage Spaces, as well as importing and exporting of data for users or machines from Connected Storage Spaces by using XML files.

When an operation is performed on a local data from Connected Storage Space, the system will behave as if that operation had been performed by the app itself, so the act of reading the data from a Connected Storage Space to a local file will cause synchronization with the cloud prior to copying.

Similarly, a copy of data from an XML file on the development PC to a Connected Storage Container on the Xbox One dev kit will cause the console to start uploading that data to the cloud. However, there are conditions in which this will not occurr: if the dev kit cannot acquire the lock, or if there is a data conflict between the Containers on the console and those in the cloud, the console will behave as if the user had decided not to resolve the conflict by picking one version of the Container to keep, and the console will behave is if the user is playing offline until the next time the title is started.

The one exception to these commands is **reset** **/f** which clears the local storage of saved data for all SCIDs and users but does not alter the data stored in the cloud. This is useful for putting a console into the state it would be in if a user was roaming to a console and downloading data from the cloud upon playing a title.

For more information about xbStorage, see *Manage Connected Storage (xbstorage.exe)* in the XDK documentation.
