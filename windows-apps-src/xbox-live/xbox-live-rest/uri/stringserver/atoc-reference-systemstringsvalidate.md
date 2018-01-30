---
title: System Strings Validatation URIs
assetID: b9a54456-7b4a-f6d8-16b9-5b6c3bd9813e
permalink: en-us/docs/xboxlive/rest/atoc-reference-systemstringsvalidate.html
author: KevinAsgari
description: ' System Strings Validatation URIs'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# System Strings Validatation URIs
 
This section provides detail about Universal Resource Identifier (URI) addresses and associated Hypertext Transport Protocol (HTTP) methods from Xbox Live Services for *system strings validation*.
 
Before uploading persistent string data to , it should be validated to ensure it doesn't violate the Code of Conduct or Terms of Use. This REST resource takes an array of strings and returns a result code for each one, indicating whether or not it is acceptable on , and a string containing the offending term.
 
The domain for these URIs is client-strings.xboxlive.com.
 
<a id="ID4EQB"></a>

 
## In this section

[/system/strings/validate](uri-systemstringsvalidate.md)

&nbsp;&nbsp;Accesses an array of strings for validation.
 
<a id="ID4EWB"></a>

 
## See also
 
<a id="ID4EYB"></a>

 
##### Parent 

[Universal Resource Identifier (URI) Reference](../atoc-xboxlivews-reference-uris.md)

   