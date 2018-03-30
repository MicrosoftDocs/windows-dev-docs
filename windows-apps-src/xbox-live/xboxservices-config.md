---
title: XboxServices.config
author: KevinAsgari
description: Describes the XboxServices.config file for associating your UWP game with an Xbox Live configuration.
ms.author: kevinasg
ms.date: 03/29/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, service configuration, xboxservices.config
ms.localizationpriority: low
---

# XboxServices.config file description

When you develop an Xbox Live enabled UWP game, your project must include an XboxServices.config file.  This file enables the Xbox Live SDK to associate your game with your Dev Center app and your Xbox Live services configuration. This file contains a JSON object that details information such as the service configuration ID, title ID, etc.

If you are using Unity to design an Xbox Live Creators Program game by using the Xbox Live plug-in, this file is automatically created for you by the Xbox Live Association Wizard.

## XboxServices.config fields

>[!NOTE]
> The file created by the Xbox Live Association Wizard may include additional fields beyond the ones described below, but they are not used by the service.

The following fields are defined in the JSON object in the config file:

Field | Description
--- | ---
PrimaryServiceConfigId  |  The Xbox Live service configuration ID (SCID). On the [Dev Center dashboard](https://developer.microsoft.com/en-us/dashboard), you can find this value on the **Xbox Live** page (for Creators Program) or **Xbox Live Setup** page (for full Xbox Live games), under the **Services** section for your app.
TitleId  |  The decimal Title ID for your app. On the [Dev Center dashboard](https://developer.microsoft.com/en-us/dashboard), you can find this value on the **Xbox Live** page (for Creators Program) or **Xbox Live Setup** page (for full Xbox Live games), under the **Services** section for your app.
XboxLiveCreatorsTitle  |  If "true", indicates that the app is an Xbox Live Creators Program app. Otherwise, "false".
Scope  |  **(Optional)** Defines the scope of functionality used by the app. See below for further description.

### Scope field

The **Scope** field is an optional field that you can use to indicate the functionality used by your game.


If the **Scope** field is not specified, then the scope is set to a default value that depends on the value of the **XboxLiveCreatorsTitle** field, as described in the following table:

XboxLiveCreatorsTitle value | Default Scope value
--- | ---
"true"  |  "xbl.signin xbl.friends"
"false"  |  "xboxlive.signin"



The following list describes the valid values for the **Scope** field.

Scope value | Description
--- | ---
xbl.signin  | Includes the sign in functionality for Creators Program games. Required for Creators Program games.
xbl.friends | Includes the friends and social leaderboards functionality for Creators Program games.
xboxlive.signin | Includes the sign in functionality for games that access the full functionality of Xbox Live. Required for non-Creators Program games.

Currently, the only reason to specify the **Scope** field is if you are making an Xbox Live Creators Program game, and your game does not need to access friends lists or social leaderboards (leaderboards which are scoped to your friends). If this is the case, you can add the following line to your XboxServices.config file:

```
  "Scope" : "xbl.signin"
```

Adding this line prevents the UWP app from requesting permission to access friend lists when you start the app for the first time.

## Example XboxServices.config file

```
{
  "PrimaryServiceConfigId": "00000000-0000-0000-0000-000064382e34",
  "TitleId": 9039138423,
  "XboxLiveCreatorsTitle": true,
  "Scope" : "xbl.signin xbl.friends"
}
```
