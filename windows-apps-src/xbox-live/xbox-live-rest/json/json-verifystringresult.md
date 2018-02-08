---
title: VerifyStringResult (JSON)
assetID: 272c688e-179e-c7e9-086b-e76d0d4bcb57
permalink: en-us/docs/xboxlive/rest/json-verifystringresult.html
author: KevinAsgari
description: ' VerifyStringResult (JSON)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# VerifyStringResult (JSON)
Result codes corresponding to each string submitted to [/system/strings/validate](../uri/stringserver/uri-systemstringsvalidate.md).
<a id="ID4ER"></a>


## VerifyStringResult

The VerifyStringResult object has the following specification.

| Member| Type| Description|
| --- | --- | --- |
| resultCode| 32-bit unsigned integer| Required. HResult code corresponding to submitted string.|
| offendingString| string| Required. String value that caused a string to be rejected.|

<a id="ID4EXB"></a>


## Sample JSON syntax


```cpp
{
    "verifyStringResult":
    [
        {"resultCode": "1", "offendingString": "badword"},
        {"resultCode": "0", "offendingString": ""},
        {"resultCode": "0", "offendingString": ""},
        {"resultCode": "0", "offendingString": ""},
    ]
}

```


#### Common HResult Values

| Value| Error Name|
| --- | --- | --- | --- | --- |
| 0| Success|
| 1| Offensive string|
| 2| String too long|
| 3| Unknown error|

<a id="ID4ELD"></a>


## See also

<a id="ID4END"></a>


##### Parent

[JavaScript Object Notation (JSON) Object Reference](atoc-xboxlivews-reference-json.md)


<a id="ID4EXD"></a>


##### Reference

[POST (/system/strings/validate)](../uri/stringserver/uri-systemstringsvalidatepost.md)
