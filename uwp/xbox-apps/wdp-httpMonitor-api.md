---
title: Device Portal HTTP monitor API reference
description: Learn how to access real-time HTTP traffic from the focused app on an Xbox using the /ext/httpmonitor/sessions Xbox Device Portal REST API.
ms.localizationpriority: medium
ms.topic: article
ms.date: 02/08/2017
---
# HTTP Monitor API reference   
You can access real-time HTTP traffic for the focused app using this API if the HTTP monitor has been enabled on the Xbox console by checking the box in Dev Home.

## Get if the HTTP Monitor is enabled

**Request**

You can get whether the HTTP monitor has been enabled in Dev Home.

Method      | Request URI
:------     | :-----
GET | /ext/httpmonitor/sessions

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**   
A JSON object with the following fields:

* Enabled - (Bool) Whether the HTTP monitor has been enabled on the Xbox console by checking the box in Dev Home.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Request was successful
4XX | Error codes
5XX | Error codes

## Get HTTP traffic from the focused app

**Request**

Get HTTP traffic from the focused app on the Xbox, as long as it is not a system app, in real-time, if the HTTP monitor has been enabled from Dev Home.

Method      | Request URI
:------     | :-----
Websocket | /ext/httpmonitor/sessions

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**   
A JSON object with the following fields:

* Sessions
    * RequestHeaders - (JSON Object) The request headers from the HTTP Request.
    * RequestContentHeaders - (JSON Object) The request content headers from the HTTP Request.
    * RequestURL - (String) The request URL.
    * RequestMethod - (String) The request method.
    * RequestMessage - (String) The request message, currently only supporting JSON and text content.
    * ResponseHeaders - (JSON Object) The response headers from the HTTP Response.
    * ResponseContentHeaders - (JSON Object) The response content headers from the HTTP Response.
    * StatusCode - (Number) The response status code.
    * ReasponsePhrase - (String) The response reason phrase.
    * ResponseMessage - (String) The response message, currently only supporting JSON and text content.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Request was successful
4XX | Error codes
403 | HTTP Monitor disabled, must be enabled in Dev Home
5XX | Error codes


**Available device families**

* Windows Xbox