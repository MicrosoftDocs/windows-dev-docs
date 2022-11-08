---
title: Device Portal Xbox Developer settings API reference
description: Learn how to access Xbox One settings that are useful for development by using the Xbox Device Portal REST API.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 6ab12b99-2944-49c9-92d9-f995efc4f6ce
ms.localizationpriority: medium
---
# Developer settings API reference

You can access Xbox One settings that are useful for development using this API.

## Get all developer settings at once

**Request**

You can use the following request to get all developer settings in a single request.

Method      | Request URI
:------     | :-----
GET | /ext/settings

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**   
The response is a Settings JSON array containing all the settings. Each settings object contains the following fields:

* Name - (String) The name of the setting.
* Value - (String) The value of the setting.
* RequiresReboot - ("Yes" | "No") This field indicates whether the setting requires a reboot to take effect.
* Disabled - ("Yes" | "No") This field indicates whether the setting is disabled and cannot be edited.
* Category - (String) The category of the setting.
* Type - ("Text" | "Number" | "Bool" | "Select") This field indicates what type a setting is: text input, a boolean value ("true" or "false"), a number with a min and max or select with a specific list of values.

If the setting is a number:

* Min - (Number) This field indicates the minimal numerical value of the setting.
* Max - (Number) This field indicates the maximum numerical value of the setting.

If the setting is select:

* OptionsVariable - ("Yes" | "No") This field indicates whether the setting options are variable, if the valid options can change without a reboot.
* Options - JSON array containing the valid select options as strings.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Request was successful
4XX | Error codes
5XX | Error codes

## Get settings one at a time

Settings can also be retrieved individually.

**Request**

You can use the following request to get information about an individual setting.

Method      | Request URI
:------     | :-----
GET | /ext/settings/\<setting name\>

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**   
The response is a JSON object with following fields:

* Name - (String) The name of the setting.
* Value - (String) The value of the setting.
* RequiresReboot - ("Yes" | "No") This field indicates whether the setting requires a reboot to take effect.
* Disabled - ("Yes" | "No") This field indicates whether the setting is disabled and cannot be edited.
* Category - (String) The category of the setting.
* Type - ("Text" | "Number" | "Bool" | "Select") This field indicates what type a setting is: text input, a boolean value ("true" or "false"), a number with a min and max or select with a specific list of values.

If the setting is a number:

* Min - (Number) This field indicates the minimal numerical value of the setting.
* Max - (Number) This field indicates the maximum numerical value of the setting.

If the setting is select:

* OptionsVariable - ("Yes" | "No") This field indicates whether the setting options are variable, if the valid options can change without a reboot.
* Options - JSON array containing the valid select options as strings.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Request was successful
4XX | Error codes
5XX | Error codes

## Set the value of a setting

You can set the value of a setting.

**Request**

You can use the following request to set the value for a setting.

Method      | Request URI
:------     | :-----
PUT | /ext/settings/\<setting name\>

**URI parameters**

- None

**Request headers**

- None

**Request body**   
The request body is JSON object containing the following field:   
Value - (String) The new value for the setting.

**Response**   

- None

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Request was successful
4XX | Error codes
5XX | Error codes

**Available device families**

* Windows Xbox