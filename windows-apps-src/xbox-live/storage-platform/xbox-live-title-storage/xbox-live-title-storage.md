---
title: Xbox Live Title Storage
author: KevinAsgari
description: Learn how to use Xbox Live Title Storage to store game information for a title in the cloud.
ms.assetid: a4182bc8-d232-4e77-93ae-97fe17ac71b1
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Xbox Live Title Storage

The Xbox Live title storage service provides a way to store game information for a title in the cloud. Games running on all platforms can use this service.

<a name="ID4EW"></a>

## Features of Xbox Live title storage

Some of the high-level features of Xbox Live title storage include, but are not limited to:

-   Can be shared across users, titles, and various platforms
-   Supports JSON, binary, and configuration files

The main features of Xbox Live title storage are explained in more detail in the following sections:

-   [Types of storage](#ID4ETB)
-   [Types of data](#ID4ECF)
-   [Title storage URIs](#ID4EBEAC)
-   [Throttle Limit](#ID4ETEAC)

<a name="ID4ETB"></a>

For managed partners and ID@Xbox members:

| Storage Type       | Quota (Managed Partners/ID@Xbox) | Quota (Xbox Live Creators Program) |  Purpose                                                                                                                                                      | Platforms                                                                                           | Users                                       |
|--------------------|--------------------|---------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------|---------------------------------------------|
| Trusted Platform   | 256 MB per user | 64 MB per user    | Per-user data such as saved games or game state for play/pause/resume. More secure, but with platform restrictions. | Any platform may read, but only Xbox One, Xbox 360, or Windows Phone may write.  | Configurable to public or owner only.       |
| Universal Platform | 64 MB per user | 64 MB per user    | Per-user data such as saved games or game state for play/pause/resume. | Any platform may write, but only platforms other than Xbox One, Xbox 360 or Windows Phone may read. | Configurable to public or owner only.       |
| Global             | 256 MB | 256 MB            | Data that everyone can read, such as rosters, maps, challenges, or art resources. | Only writeable via the Xbox Developer Portal, any platform may read.                                | All users may read.

### Deprecated storage Types

The following storage types are deprecated. They are supported only for titles that are currently using them. They are not available for new titles.

| Storage Type       | Quota  |   Purpose                                                                                                                                                      | Platforms                                                                                           | Users                                       |
|--------------------|--------------------|---------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------|---------------------------------------------|
| JSON               | 64 MB per user     | Per-user data such as saved games or game state for play/pause/resume. More secure, no platform restrictions, but with data format restrictions (JSON only). | Any platform may read or write.                                                                     | Configurable to public or owner only.       |
| Device             | 64 MB per device   | Data specific to a device such as settings or device preferences.                                                                                            | Only Xbox One, Xbox 360, or Windows Phone may write. Only the device that wrote the data may read.  | All users may read.                         |
| Session Storage    | 256 MB per session | Data for anyone joined to a particular multiplayer game session.                                                                                             | Any platform that may join the session.                                                             | All users in the session may read or write. |


<a name="ID4ECF"></a>

## Types of data

Games specify the type of data to use in the **{type}** parameter of a GET or PUT method. The following section describes the three supported types:

-   [Binary Information](#ID4ENF)
-   [JSON Information](#ID4EUF)
-   [Configuration information](#ID4ECAAC)

<a name="ID4ENF"></a>

#### Binary Information

Images, sounds, and custom data use the binary type. Because the data must be transmitted over HTTP, binary data must be encoded into characters that HTTP accepts. For example, you can convert the data to hexadecimal strings or use base64 encoding. The title storage system does not process the encoded data, so your game must use the same scheme for encoding and decoding data when reading from and writing to title storage.

<a name="ID4EUF"></a>

#### JSON Information

Structured data can use the JSON type. JSON objects can be directly used in languages, like JavaScript, that support them. When retrieving data from JSON files, the game can supply a *select* parameter to return specific items within the structure. For example, use a JSON formatted file that contains the following information:

    {
    "difficulty" : 1,
    "level" :
        [
            { "number" : "1", "quest" : "swords" },
            { "number" : "2", "quest" : "iron" },
            { "number" : "3", "quest" : "gold" },
            { "number" : "4", "quest" : "queen" }
         ],
    "weapon" :
        {
             "name" : "poison",
             "timeleft" : "2mins"
        }
    }


| Note                                                                                                                                              |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| For security purposes, the first element of the JSON data must not be an array. JSON data submitted with an array at the root will be rejected by the service. |

Games can select portions of this structure with a query like this:

             GET https://titlestorage.xboxlive.com/users/xuid(1234)/storage/titlestorage/titlegroups/
             faa29d21-2b49-4908-96bf-b953157ac4fe/data/save1.dat,json?select=weapon.name
             Content-Type: application/octet-stream
             x-xbl-contract-version: 1
             Authorization: XBL3.0 x=<userHash>;<STSTokenString>
             Connection: Keep-Alive

The response body for this query is:

    {
        "name" : "poison"
    }

The array can be accessed with a query like this:

      GET https://titlestorage.xboxlive.com//users/xuid(1234)/storage/titlestorage/titlegroups/
      faa29d21-2b49-4908-96bf-b953157ac4fe/data/save1.dat,json?select=levels[3].quest
      Content-Type: application/octet-stream
      x-xbl-contract-version: 1
      Authorization: XBL3.0 x=<userHash>;<STSTokenString>
      Connection: Keep-Alive

The response body for this query is:

    {
        "quest" : "queen"
    }

The following length restrictions are enforced for JSON data:

-   Numeric value, maximum length = 32
-   String value, maximum length = 1024
-   Property name, maximum length = 64
-   Hierarchy, maximum depth = 16
-   Array, maximum size = 1024
-   Child properties, maximum in an object = 1024

<a name="ID4ECAAC"></a>

#### Configuration information

The **{type}** can be **config** to indicate that the data is a configuration blob. Configuration blobs are data structures that are stored in global title storage. The format of the blob is similar to a JSON object.

Configuration blobs can include virtual nodes that return a setting from a list of possibilities. Virtual nodes are useful for providing settings for specific situations, such as for a title or locale. The virtual node includes several possible settings along with values and conditions for selecting from the values. In the following example, the **defaultCardDesign** setting can have one of the values in the virtual node.

    {
      "defaultCardDesign":
      {
        "_virtualNode":
       {
          "_selectBy":"titleId",
          "_sourceNodes":
          [
            {"_selector":"123456799", "_data":"RobotUnicornCard.png,binary"},
            {"_selector":"default", "_data":"StandardCard.png,binary"}
          ]
        }
      },
    }

When a game reads this file, the system selects one of the values from the **\_sourceNodes** array. In this case, the item is selected based on the title ID of the game. Users playing the game **12456799** see:

    {
      "defaultCardDesign":"RobotUnicornCard.png,binary",
      "_sourceNodes":["defaultCardDesign:titleID:1234567899"]
    }

The rest of the users see:

    {
      "defaultCardDesign":"StandardCard.png,binary",
      "_sourceNodes":["defaultCardDesign:titleID:default"]
    }

Games can define custom selectors that match a parameter in the request. For example, in this config blob:

    {
        "defaultCardDesign":
        {
            "_virtualNode":
            {
                "_selectBy":"custom:gameMode",
                "_sourceNodes":
                [
                    {"_selector":"silly", "_data":"RobotUnicornCard.png,binary"},
                    {"_selector":"serious", "_data":"SeriousCard.png,binary"},
                    {"_selector":"default", "_data":"StandardCard.png,binary"}
                 ]
            }
        },
        "backgroundColor":"green",
        "dealerHitsOnSoft17":true
    }

Games pass a string the **customSelector** parameter to select which item to return. For example, to get the second option, a game requests:

      GET https://titlestorage.xboxlive.com/media/titlegroups/faa29d21-2b49-4908-96bf-b953157ac4fe
      /storage/data/config.json,config?customSelector=gameMode.serious
      Content-Type: application/octet-stream
      x-xbl-contract-version: 1
      Authorization: XBL3.0 x=<userHash>;<STSTokenString>
      Connection: Keep-Alive

The **\_selectBy** value indicates what type of selection to do and the **\_selector** value indicates the data to use in the selection. The possible values are:

<table>
<thead>
<tr>
<th>_selectBy</th>
<th>Description</th>
</tr>
</thead>
<tbody>
<tr>
<td >titleId</td>
<td ><p>The <strong>_selector</strong> matches the title ID in the provided claim.</p></td>
</tr>
<tr>
<td >locale</td>
<td ><p>The <strong>_selector</strong> matches the locale string from the Accept-Language header.</p></td>
</tr>
<tr>
<td >custom</td>
<td ><p>The <strong>_selector</strong> matches a custom string passed in the <strong>customSelector</strong> query parameter. The <strong>customSelector</strong> contains one or more queries separated by commas. Each query is the name from the <strong>selectBy</strong> element and the value from the <strong>_selector</strong> element.</p></td>
</tr>
</tbody>
</table>

<a name="ID4EBEAC"></a>

## Title storage URIs

Title storage URIs are formatted as follows:

    https://titlestorage.xboxlive.com/{path}

The **{path}** portion of the URI is the type of request being made and must be 245 characters or fewer.

<a name="ID4ETEAC"></a>

## Throttle Limit

There are no fixed limits on how many reads or writes a title can make per minute, but it generally cannot make more than one per minute on average in a one-hour session. For example, a title can make 60 reads or writes at the beginning of a session but no more for the remainder of the hour. Titles should be hardened against more calls later, in case Xbox LIVE Services needs to throttle the requests.

If your title has special partitioning requirements, such as extra reads or writes, contact Microsoft.

<a name="ID4E5EAC"></a>

## Using title storage

To get started with title storage, first determine what kind of data you want to store. Some examples include saved games, game state, daily challenges, game maps, and art resources.

Next determine what titles and platforms will need to access the data. Title storage supports cloud data access from a single title on a single platform, and from multiple titles on multiple platforms.

Finally, use the topics in this section to configure your storage, upload your data, and set access permissions appropriately based on your choices.

<a name="ID4EJFAC"></a>

## In this section

[Reading a Configuration Blob in Xbox Live Title Storage](reading-configuration-blobs.md)  
Demonstrates reading configuration blobs from Xbox Live title storage.

[Storing a Binary Blob in Xbox Live Title Storage](storing-binary-blobs.md)  
Demonstrates storing binary blobs in Xbox Live title storage.

[Reading a Binary Blob in Xbox Live Title Storage](reading-binary-blobs.md)  
Demonstrates reading binary blobs from Xbox Live title storage.

[Storing a JSON Blob in Xbox Live Title Storage](storing-jsonblobs.md)  
Demonstrates storing JSON blobs in Xbox Live title storage.

[Reading a JSON Blob in Xbox Live Title Storage](reading-jsonblobs.md)  
Demonstrates reading JSON blobs from Xbox Live title storage.

<a name="ID4E4FAC"></a>
