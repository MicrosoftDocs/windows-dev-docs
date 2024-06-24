---
title: Device Portal network credentials API reference
description: Learn how to add, remove, or update network credentials programmatically.
ms.localizationpriority: medium
ms.topic: article
ms.date: 02/08/2017
---

# Network Credentials API reference

You can add, remove, or update stored network credentials on your devkit using this REST API.

## Get existing credentials

**Request**

You can get a list of the stored shares along with the username of the user who has credentials for that network share.

| Method | Request URI |
|--------|-------------|
| GET | /ext/networkcredential |

**URI parameters**

- None

**Request headers**

- None

**Request body**   

- None

**Response**   

JSON array in the following format:

* Credentials
  * NetworkPath - The path to the network share.
  * Username - The username which has stored credentials.

**Status code**

This API has the following expected status codes.

| HTTP status code | Description |
|------------------|-------------|
| 200 | Success |
| 4XX | Error codes |
| 5XX | Error codes |

## Add or update stored credentials for a user

**Request**

| Method | Request URI |
|--------|-------------|
| POST | /ext/networkcredential |

**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter      | Description     |
| ------------------ |-----------------|
| NetworkPath        | The network path to the share you are adding credentials to access. |

**Request headers**

- None

**Request body**

The following JSON elements:
* NetworkPath - The path to the network share.
* Username - The username to store the credentials under.
* Password - The new or updated password for this user.

**Response**   

- None  

**Status code**

This API has the following expected status codes.

| HTTP status code | Description |
|------------------|-------------|
| 204 | Success |
| 4XX | Error codes |
| 5XX | Error codes |

## Remove stored credentials for a share.

**Request**

| Method | Request URI |
|--------|-------------|
| DELETE | /ext/networkcredential |

**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter      | Description     |
| ------------------ |-----------------|
| NetworkPath        | The network path to the share from which you are removing stored credentials. |

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
| 204 | The request to the credentials was successful. |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Xbox