---
author: WilliamsJason
title: Device Portal SSH pins API reference
description: Learn how to remove all trusted SSH pins programatically.
ms.localizationpriority: medium
---

# SSH Pins API reference
You can remove all trusted SSH pins on your devkit using this REST API.

## Remove trusted SSH pins

**Request**

Method      | Request URI
:------     | :-----
DELETE | /ext/app/sshpins
<br />
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
204 | The request to clear the pins was successful.
4XX | Error codes
5XX | Error codes

<br />
**Available device families**

* Windows Xbox

