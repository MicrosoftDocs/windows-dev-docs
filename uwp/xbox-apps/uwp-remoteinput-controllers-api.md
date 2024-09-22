---
title: Device Portal controllers API reference
description: Learn how to get the number of attached physical controllers and turn them off programmatically.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Controller API reference

You can get the number of attached physical controllers and turn them off using this REST API.

## Determine the number of attached physical controllers

**Request**

You can check the number of attached physical controllers on the device using the following request.

Method | Request URI |
-------|-------------|
| GET | /ext/remoteinput/controllers |

**URI parameters**

- None

**Request headers**

- None

**Request body**   

- None

**Response**   

- JSON number property ConnectedControllerCount which specifies the number of attached physical controllers.

**Status code**

This API has the following expected status codes.

| HTTP status code | Description |
|------------------|-------------|
| 200 | Success |
| 4XX | Error codes |
| 5XX | Error codes |

## Disconnect all physical controllers on the devkit

**Request**

You can disconnect all physical controllers on the device using the following request.

| Method | Request URI |
|--------|-------------|
| DELETE | /ext/remoteinput/controllers |

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

| HTTP status code | Description |
|------------------|-------------|
| 204 | The request to disconnect controllers was successful. |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Xbox
