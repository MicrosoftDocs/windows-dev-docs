---
title: Media Capture API reference
description: Learn how to capture a PNG representation of the current screen by using the Xbox Device Portal REST API.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 3f92c8fd-4096-4972-97da-01ae5db6423c
ms.localizationpriority: medium
---
# Media Capture API reference #

## Request

You can capture a PNG representation of the current screen by using the following request format.

| Method        | Request URI     | 
| ------------- |-----------------|
| GET           | /ext/screenshot |


**URI parameters**

You can specify the following additional parameters on the request URI:


| URI parameter      | Description     | 
| ------------------ |-----------------|
| download (optional)| A boolean value indicating if HTTP response headers should be set indicating that the host browser should download the screenshot as an attachment rather than rendering it in the browser.  |

**Request headers**

* None

**Request body**

* None

## Response

**Status code**

This API has the following expected status codes.

| HTTP status code   | Description     | 
| ------------------ |-----------------|
| 200                | Screenshot request successful and capture returned |
| 5XX                | Error codes for unexpected failures |
<br>

**Available device families**

* Windows Xbox

