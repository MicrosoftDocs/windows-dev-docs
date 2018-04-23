---
title: Title Storage configuration on Dev Center
author: aablackm
description: Learn how to configure Title Storage on Windows Dev Center
ms.author: aablackm
ms.date: 04/24/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: low
keywords: Xbox Live, Xbox, games, uwp, windows 10, Xbox one, Title Storage, Windows Dev Center
---
# Configure storage for you title on Windows Dev Center

Xbox Live allows you to save data associated with your game in the cloud through the Title Storage service. The Title Storage configuration page allows you to determine what types of cloud storage services your game will allow, as well as upload files to be used for Global Storage.

You can find the Xbox Live Title Storage configuration page by going to your [Windows Development Center Dashboard](https://developer.microsoft.com/en-us/dashboard/windows/overview), choosing your app from **Overview** or **Products**, opening the **Services** drop down, and selecting **Xbox Live**. Developers in the Creators Program will need to click **show options** in the **Cloud saves and storage** section of their configuration page to see the Title Storage configuration options. Those with the full set of Xbox Live features available will need to find the **Title Storage** link to navigate to the Title Storage configuration page.

Title Storage configuration has two main sections. The Title Storage settings section and the global storage file management section.

## Section 1: Title Storage settings

![Title Storage settings screenshot](../../images/dev-center/title-storage/title-storage-settings.JPG)

In this section you can enable any of the four storage types by checking the box in the **Active** column. After choosing you title's storage types you can choose whether reading the data will be restricted to the player who owns it, by clicking the storage type row's **Owner** Only radio button, or shared publicly, by clicking on the **Everyone** radio button. If you select **Owner Only** for a given **Storage Type** then the Title Storage data of that type will only be readable by the player who generated that data. If you select **Everyone** for a given **Storage Type** then the Title Storage data of that type will be readable to all players. Writing or modifying saved data is only available to the user who generated it in all cases.

## Storage types

There are four storage types which can be activated on the Title Storage configuration page. You can find a description of each storage type by hovering over the info icon next to each storage type's name, or by reading the table below.

|Storage Type |Description |Example Usage  |
|---------|---------|---------|
|Global             |Data Uploaded to Windows Dev Center that can be read by any device, and is accessible to every user. Can only be written to by the developer uploads to Windows Dev Center. | Advertise updates to all users via in-game news feed.     |
|Connected Storage  |Allows background syncing of game data on XboxOne and Windows 10 Games. A robust fault tolerant game save service. Can be read by any device, can be written to by Xbox One and Windows 10 devices    | Save files for an individual user to allow play on a separate console.         |
|Universal          |Network accessible blob storage that gives read/write access to any device that is not an Xbox 360 or Windows Phone. Can be read by Android and IOS devices.      | save playtime or other stats to be accessible from multiple Windows devices.        |
|Trusted            |Network accessible blob storage that can only be written by Xbox One, Xbox 360 and Windows Phone. Can be read by any device. Can be read by Android and IOS.     | store a player's ranking in multiplayer.        |

## Section 2: Global Storage file management

![global storage file management screenshot](../../images/dev-center/title-storage/global-storage-file-management.JPG)

In order to see the full Global Storage file management options you must click the **show options** drop down. In this section you can add files and folders that will be accessible if the **Global** storage type is set to active in the Title Storage settings. Your game will need to be published for testing in order to add files in this section. You may see a warning at the top of the Title Storage configuration page if your game is not adequately published for testing.

## Manage Global Storage files

Global storage file management allows you to upload and download files to be used for global storage. These files can potentially be accessed by anyone who owns your title, and are meant to be shared across all players who play your game. In order to see the global storage file options you must click the **show options** drop down next to the sections title. To add your first file click the **+ New ...** link. You will then be given the option to add a new file or folder to your global storage files.

### New folders

When adding a new folder to your global storage files you simply need to name the folder and click the **Create** button. Your new folder will appear in the file explorer table.

![add folder dialogue](../../images/dev-center/title-storage/add-folder-global-storage-filled.JPG)

In order to add files to your folder you must upload them to the folder directly by pushing the folders **Actions** button: "**...**", and selecting **upload files**. You cannot drag and drop files into folders within the files explorer table. Using the **create folder** action in the **Actions** menu of a folder will create a child folder. Choosing the **delete** action in the **Actions** menu of a folder will delete the folder and all of it's contents.

### New files

When adding a new file to global storage you will be prompted to upload a file from your computer's file explorer and then asked to choose from one of the three file types, Binary, Config, and JSON. In addition to being able to upload a file with the **+ New ...** button you can also drag and drop files from you computer to the file explorer table.

> [!WARNING]
> You cannot drag folders into the file explorer table, attempting this action will result in the folder being treated like a file and will not work as expected.

File management actions:
![file management gif](../../images/dev-center/title-storage/global-storage-management.gif)

#### File types

* Binary - The binary type should be used for images, sounds and custom data. This data must be HTTP friendly.
* Config - Config files hold information about your game and can have dynamic query return values based on some input.
* JSON - .json files. These files hold some information about an aspect of your game, similar to a Config file.

## Further reading

To learn more about the Xbox Live storage platform, read the [Title Storage documentation](../../storage-platform/xbox-live-title-storage/xbox-live-title-storage.md) which will give more details on Universal, Trusted, Global Storage and storage file types, and the [Connected Storage documentation](../../storage-platform/connected-storage/connected-storage-overview.md), which will teach you more about saving game progress in the cloud so that you can continue your game between devices.