---
title: Device Portal Fiddler API reference
description: Learn how to enable and disable Fiddler network tracing on your devkit by using the Xbox Device Portal REST API.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: e7d4225e-ac2c-41dc-aca7-9b1a95ec590b
ms.localizationpriority: medium
---
# Fiddler settings API reference   
You can enable and disable Fiddler network tracing on your devkit using this REST API.

## Determine if Fiddler tracing is enabled

**Request**

You can check to see if Fiddler tracing is enabled on the device using the following request.

Method      | Request URI
:------     | :-----
GET | /ext/fiddler


**URI parameters**

- None

**Request headers**

- None

**Request body**   

- None

**Response**   

- JSON bool property IsProxyEnabled which specifiers whether the proxy is enabled or not.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Success
4XX | Error codes
5XX | Error codes

## Enable Fiddler tracing

**Request**

You can enable Fiddler tracing for the devkit using the following request.  Note that the device must be restarted before this takes effect.

Method      | Request URI
:------     | :-----
POST | /ext/fiddler

**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter      | Description     | 
| ------------------ |-----------------|
| proxyAddress       | The IP address or hostname of the device running Fiddler |
| proxyPort          | The port which Fiddler is using for monitoring traffic. Defaults to 8888 |
| updateCert (optional)| A boolean value indicating if the root Fiddler cert is provided. This must be true if Fiddler has never been configured on this devkit or was configured for a different host.  |


**Request headers**

- None

**Request body**

- None if updateCert is false or not provided. Multi-part conforming http body containing the FiddlerRoot.cer file otherwise.

**Response**   

- None  

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
204 | The request to enable Fiddler was accepted. Fiddler will be enabled the next time the device reboots.
4XX | Error codes
5XX | Error codes

## Disable Fiddler tracing on the devkit

**Request**

You can disable Fiddler tracing on the device using the following request. Note that the device must be restarted before this takes effect.

Method      | Request URI
:------     | :-----
DELETE | /ext/fiddler

**URI parameters**

- None

**Request headers**

- None

**Request body**   

- None

**Response**   

- None 

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
204 | The request to disable Fiddler tracing was successful. Tracing will be disabled on the next reboot of the device.
4XX | Error codes
5XX | Error codes


**Available device families**

* Windows Xbox

## See also
- [Configuring Fiddler for UWP on Xbox](uwp-fiddler.md)

