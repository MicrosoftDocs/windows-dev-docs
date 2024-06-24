---
title: Device Portal Loose folder registration API reference
description: Learn how to access the loose folder registration APIs programmatically.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: efdf4214-9738-4df6-bf1f-ed7141696ef6
ms.localizationpriority: medium
---
# Register an app in a loose folder  

**Request**

You can register an app in a loose folder by using the following request format.

Method      | Request URI
:------     | :------
POST | /api/app/packagemanager/register

**URI parameters**

You can specify the following additional parameters on the request URI:

URI Parameter      | Description
:------     | :-----
folder (required) | The destination folder name of the package to be registered. This folder must exist under d:\developmentfiles\LooseApps on the console. This folder name should be base64 encoded as it may contain path separators if the folder is in a subfolder under LooseApps.

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Deploy request accepted and being processed
4XX | Error codes
5XX | Error codes

**Available device families**

* Windows Xbox

**Notes**

There are at least three different ways to get the loose app on the console in the desired folder. The easiest is to simply copy the files via SMB to \\<IP_Address>\DevelopmentFiles\LooseApps. This will require a username and password on UWA kits which can be obtained via [/ext/smb/developerfolder](wdp-smb-api.md). 

The second way is by copying over individual files to the correct location by doing a POST to /api/filesystem/apps/file where knownfolderid is DevelopmentFiles, packagefullname is empty, and filename and path are properly supplied (path should begin with LooseApps).

The third way is to copy an entire folder at a time via [/api/app/packagemanager/upload](wdp-folder-upload.md) where destinationFolder is the name of the folder to be placed under d:\developmentfiles\looseapps and the payload is a multi-part conforming http body of the directory contents.

