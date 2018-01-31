---
title: POST (/titles/{titleId}/variants)
assetID: 84303448-5a11-d96f-907d-77f57f859741
permalink: en-us/docs/xboxlive/rest/uri-titlestitleidvariants-post.html
author: KevinAsgari
description: ' POST (/titles/{titleId}/variants)'
ms.author: kevinasg
ms.date: 20-12-2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
---


# POST (/titles/{titleId}/variants)
URI called by a client that retrieves a list of game variants for the specified title Id. 
The domains for these URIs are `gameserverds.xboxlive.com` and `gameserverms.xboxlive.com`.
 
  * [URI Parameters](#ID4EZ)
  * [Required Request Headers](#ID4EIB)
  * [Optional Request Headers](#ID4EED)
  * [Authorization](#ID4E3D)
  * [Request Body](#ID4EEE)
  * [Required Response Headers](#ID4ELF)
  * [Optional Response Headers](#ID4EMG)
  * [Response Body](#ID4EEH)
 
<a id="ID4EZ"></a>

 
## URI Parameters
 
| Parameter| Description| 
| --- | --- | 
| titleid| ID of the title that the request should operate on.| 
  
<a id="ID5EG"></a>

 
## Host Name

gameserverds.xboxlive.com
 
<a id="ID4EIB"></a>

 
## Required Request Headers
 
When making a request, the headers shown in the following table are required.
 
| Header| Value| Description| 
| --- | --- | --- | --- | --- | 
| Content-Type| application/json| Type of data being submitted.| 
| Host| gameserverds.xboxlive.com|  | 
| Content-Length|  | Length of the request object.| 
| x-xbl-contract-version| 1| API contract version.| 
| Authorization| XBL3.0 x=[hash];[token]| Authentication token.| 
  
<a id="ID4EED"></a>

 
## Optional Request Headers
 
When making a request, the headers shown in the following table are optional.
 
| Header| Value| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | 
| X-XblCorrelationId|  | The mime type of the body of the request.| 
  
<a id="ID4E3D"></a>

 
## Authorization

The request must include a valid Xbox Live authorization header. If the caller is not allowed to access this resource, the service returns 403 Forbidden in response. If the header is invalid or missing, the service returns 401 Unauthorized in response.
 
<a id="ID4EEE"></a>

 
## Request Body
 
The request must contain a JSON object with the following members.
 
| Member| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| locale| The local of variants to return.| 
| maxVariants| The maximum number of variants to return.| 
| publisherOnly|  | 
| restriction|  | 
 
<a id="ID4EDF"></a>

 
### Sample Request
 

```cpp
{
  "locale": "en-us",
  "maxVariants": "100",
  "publisherOnly": "false",
  "restriction": null
}

```

   
<a id="ID4ELF"></a>

 
## Required Response Headers
 
A response will always include the headers shown in the following table.
 
| Header| Value| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| Content-Type| application/json| Type of data in the response body.| 
| Content-Length|  | Length of the response body.| 
  
<a id="ID4EMG"></a>

 
## Optional Response Headers
 
A response may inlcude the headers shown in the following.
 
| Header| Value| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| X-XblCorrelationId|  | The mime type of the response body.| 
  
<a id="ID4EEH"></a>

 
## Response Body
 
If the call is successful, the service will return a JSON object with the following members.
 
| Member| Description| 
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | 
| variants| An array of variants.| 
| variantId| The Id of a variant.| 
| name| The name of a variant.| 
| isPublisher|  | 
| rank|  | 
| gameVariantSchemaId|  | 
| variantSchemas| An array of variant schemas.| 
| variantSchemaId| The Id of the schema.| 
| schemaContent| The schema contents| 
| name| Name of the schema| 
| gsiSets| An array of GSI sets.| 
| minRequiredPlayers| The minimum number of players for the variant.| 
| maxAllowedPlayers| The maximum number of players for the variant.| 
| gsiSetId| The Id of the GSI set.| 
| gsiSetName| The name of the GSI set.| 
| selectionOrder|  | 
| variantSchemaId| Id of the varaint schema used in the GSI set.| 
 
<a id="ID4EYBAC"></a>

 
### Sample Response
 

```cpp
{
 "variants": [
     { 
       "variantId": "8B6EF8A0-7807-42C4-9CB0-1D9B8B8CE742", 
       "name": "tankWarsV2.0",
       "isPublisher": "true",
       "rank": "1",
       "gameVariantSchemaId": "9742DBA5-23FD-4760-9D74-6CFA211B9CFB"
     }],
  "variantSchemas": [
     {
        "variantSchemaId": "9742DBA5-23FD-4760-9D74-6CFA211B9CFB",
        "schemaContent": "&lt;?xml version=\"1.0\" encoding=\"UTF-8\" ?>&lt;xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">&lt;xs:element name=\"root\">&lt;/xs:element>&lt;/xs:schema>"
        "name": "tanksSchema"
     }],
     "gsiSets":
     [{ 
          "minRequiredPlayers": "5", 
          "maxAllowedPlayers": "10", 
          "gsiSetId": "B28047F5-B52F-477E-97C2-4C1C39E31D42",
          "gsiSetName": "TanksGSISet",
          "selectionOrder": "1",
          "variantSchemaId": "9742DBA5-23FD-4760-9D74-6CFA211B9CFB"
     }]
 }

  

```

   
<a id="ID4ERCAC"></a>

 
## See also
 [/titles/{titleId}/variants](uri-titlestitleidvariants.md)

  