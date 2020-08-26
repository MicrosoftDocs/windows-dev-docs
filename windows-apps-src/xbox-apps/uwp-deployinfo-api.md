---
title: Device Portal deploy info API reference
description: Learn how to use the Xbox Device Portal REST API deployinfo to request deployment information for one or more installed packages.
ms.localizationpriority: medium
ms.topic: article
ms.date: 02/08/2017
---
# Requests deployment information for one or more installed packages.

**Request**

Method      | Request URI
:------     | :------
POST | /ext/app/deployinfo
<br />
**URI parameters**

 - None

**Request headers**

- None

**Request body**

A JSON array in the following format:

* DeployInfo
  * PackageFullName - Name of the package that we are requesting information about.
  * OverlayFolder - Optional path to an overlay folder path if using this feature.

###Response

**Response body**

A JSON array in the following format (some fields are optional):

* DeployInfo
  * PackageFullName - Name of the package that we are receiving information about.
  * DeployType - The type of deployment.
  * DeployPathOrSpecifiers - A deploy path for loose deployments or installed specifiers for packaged deployments.
  * DeployDrive - The drive the package is deployed to for applicable deployment types.
  * DeploySizeInBytes - The size in bytes of the package for applicable deployment types.
  * OverlayFolder - The overlay folder for deployments which support this feature.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Success
4XX | Error codes
5XX | Error codes
<br />

**Available device families**

* Windows Xbox