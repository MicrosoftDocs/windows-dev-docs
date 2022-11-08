---
title: Device Portal Xbox info API reference
description: Learn how to access Xbox One device information by using the GET method of the Xbox Device Portal REST API.
ms.date: 04/18/2019
ms.topic: article
keywords: windows 10, uwp, xbox, device portal
ms.localizationpriority: medium
---
# Xbox Info API reference   
You can access Xbox One device information using this API.

## Get Xbox One device information

## Request

You can get device information about your Xbox One.

Method      | Request URI
:------     | :-----
GET | /ext/xbox/info

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

## Response
A JSON object with the following fields:

* OsVersion - (String) The version of the OS.
* OsEdition - (String) The edition of the OS, such as "March 2017" or "March 2017 QFE 1".
* ConsoleId - (String) The console's ID.
* DeviceId - (String) The console's Xbox Live Device Id.
* SerialNumber - (String) The console's serial number.
* DevMode - (String) The console's current developer mode, such as "None" or "Retail".
* ConsoleType - (String) The console's type, such as "Xbox One" or "Xbox One S".
* DevkitCertificateExpirationTime - (Number) The UTC Time in seconds when the console's developer kit certificate will expire.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Request was successful
4XX | Error codes
5XX | Error codes

**Available device families**

* Windows Xbox
