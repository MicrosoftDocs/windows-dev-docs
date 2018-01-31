---
title: Standard HTTP Request and Response Headers
assetID: a5f8fd96-9393-5234-04ad-837e5c117c92
permalink: en-us/docs/xboxlive/rest/httpstandardheaders.html
author: KevinAsgari
description: ' Standard HTTP Request and Response Headers'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# Standard HTTP Request and Response Headers
 
<a id="ID4ES"></a>

 
## Request Headers
 
The following table lists the standard HTTP headers used when making Xbox Live Services requests.
 
| Header| Value| Description| 
| --- | --- | --- | 
| x-xbl-contract-version| 1| API contract version. Required on all Xbox Live Services requests.| 
| Authorization| STSTokenString| STS authentication token. The value for this header is retrieved from the <b>GetTokenAndSignatureResult.Token</b> property. | 
| Content-Type| application/xml, application/json, multipart/form-data or application/x-www-form-urlencoded| Specifies the type of content being submitted with a request.| 
| Content-Length| Integer value| Specifies the length of the data being submitted in a POST request.| 
| Accept-Language | String| Specifies how to localize any strings returned. See <a href="http://msdn.microsoft.com/en-us/library/bb975829.aspx">Advanced Xbox 360 Programming</a> for a list of valid language/locale combinations.| 
  
<a id="ID4E6C"></a>

 
## Response Headers
 
The following table lists the standard HTTP header used in Xbox Live Services responses.
 
| Header| Value| Description| 
| --- | --- | --- | --- | --- | --- | 
| Content-Type| application/xml, application/json| Specifies the type of content being returned.| 
| Content-Length| Integer value| Specifies the length of the data being returned.| 
  
<a id="ID4EEE"></a>

 
## See also
 
<a id="ID4EGE"></a>

 
##### Parent  

[Additional Reference](atoc-xboxlivews-reference-additional.md)

   