---
title: Rich Presence updating strings
author: KevinAsgari
description: Learn how to update an Xbox Live Rich Presence string.
ms.assetid: eb2bb82e-8730-4d74-9b33-95d133360e44
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, rich presence
ms.localizationpriority: low
---
# Rich Presence updating strings

To update the Rich Presence string in your title, you can call the Write Title URI with the appropriate parameters in a JSON object. This restful call is also wrapped by Xbox Service APIs. See **Microsoft.Xbox.Services.Presence Namespace** for information on the related API.

The URI looks like this:

          POST /users/xuid({xuid})/devices/current/titles/current

Below are only the fields for setting Rich Presence strings. There are other optional fields related to the writing presence for a title not listed here.

## TitleRequest Object

Property | Type | Req'd | Description
---|---|---|---
Activity|ActivityRequest|N|Record that describes in-title information (Rich Presence and media info, if available)

## ActivityRequest Object

Property | Type | Req'd | Description
---|---|---|---
richPresence|RichPresenceRequest|N|The friendlyName of the Rich Presence string that should be used.

## RichPresenceRequest Object

Property | Type | Req'd | Description
---|---|---|---
Id|String|Y|The friendlyName of the Rich Presence string that should be used
Scid|String|Y|Scid that tells us where the Rich Presence strings are defined.

For example, if I wanted to update the Rich Presence for user whose xuid is 12345, my call would look as follows:

          POST /users/xuid(12345)/devices/current/titles/current


With the following JSON body:

```json
          {
            activity:
            {
              richPresence:
              {
                id:"playingMap",
                scid:"0000-0000-0000-0000-01010101"
              }
            }
          }
```

Using the wrapper API, this would be a call to **PresenceService.SetPresenceAsync Method**

If you're keeping the data platform up-to-date, then you don't have to reset the Rich Presence String every time the data to fill in the blank changes. In the example above we know that you want to use the current map. Presence will look up the data in the data platform when a user tries to read the string to fill in the current value. So even if the player is switching from map to map to map, you don't have to reset the Rich Presence string in your game as long as you're sending the appropriate events to the data platform. Keep in mind that it may take a few seconds for the data to find its way through the Data Platform.

Then, when someone attempts to read user 12345's Rich Presence, the service will look at what locale is being requested and format the string appropriately before returning.

In this case, let's say that a user wants to read the en-US string. Reading rich presence would work as follows (for more information about this call, see **GET (/users/xuid({xuid}))**

          GET /users/xuid(12345)?level=all

The wrapper API for this is **PresenceService.GetPresenceAsync Method**

What's happening here is that you're asking for the PresenceRecord of the user, whose xuid is 12345. And you're requesting that the level of detail be "all". If "all" wasn't specified, Rich Presence would not be returned.

And it would return the following in the JSON response:

```json
          {
            xuid:"12345",
            state:"online",
            devices:
            [
              {
                type:"D",
                titles:
                [
                  {
                    id:"12345",
                    name:"Buckets are Awesome",
                    lastModified:"2012-09-17T07:15:23.4930000",
                    placement: "full",
                    state:"active",
                    activity:
                    {
                      richPresence:"Playing on map:Mountains"
                    }
                  }
                ]
              }
            ]
          }
```
