---
title: Device Portal Xbox Live sandbox API reference
description: Learn how to get and set the value for the device's Xbox Live sandbox by using the Xbox Device Portal REST API.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 72c7459c-420a-4da9-8afa-191a846185a5
ms.localizationpriority: medium
---
# Xbox Live Sandbox API reference   
You can get and set your Xbox Live sandbox using this REST API.

## Get the Xbox Live sandbox

**Request**

You can read the current value for the device's Xbox Live sandbox using the following request:

Method      | Request URI
:------     | :-----
GET | /ext/xboxlive/sandbox

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**   
Sandbox - (String) The current Sandbox the device is in.   

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | The request to access the credentials for the file share was granted.
4XX | Error codes
5XX | Error codes

## Set the Xbox Live sandbox
You can change the Xbox Live sandbox for the device using the following request. Note that on Xbox One, the device needs to be restarted before the setting takes effect.

**Request**

You can set the current value for the device's Xbox Live sandbox using the following request:

Method      | Request URI
:------     | :-----
PUT | /ext/xboxlive/sandbox

**URI parameters**

- None

**Request headers**

- None

**Request body**   
The request body is a JSON object containing the following field:   
Sandbox - (String) The new value to set the device's sandbox to.

**Response**   
Sandbox - (String) The current Sandbox the device is in.   

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | The request to access the credentials for the file share was granted.
4XX | Error codes
5XX | Error codes

**Available device families**

* Windows Xbox

