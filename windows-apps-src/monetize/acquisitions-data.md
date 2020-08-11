---
description: Use this method in the Microsoft Store analytics API to get aggregate acquisition data in JSON format for UWP apps and Xbox One games that were ingested through the Xbox Developer Portal (XDP) and available in the XDP Analytics dashboard. 
title: Get acquisitions data for your games and apps  
ms.date: 03/06/2019
ms.topic: article
keywords: windows 10, uwp, advertising network, app metadata
ms.localizationpriority: medium
---
# Get acquisitions data for your games and apps 
Use this method in the Microsoft Store analytics API to get aggregate acquisition data in JSON format for UWP apps and Xbox One games that were ingested through the Xbox Developer Portal (XDP) and available in the XDP Analytics dashboard. 

> [!NOTE]
> This API does not provide daily aggregate data before Oct 1st 2016. 

## Prerequisites
To use this method, you need to first do the following: 

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md) for the Microsoft Store analytics API. 
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one. 

## Request
### Request syntax

| Method | Request URI |
| --- | --- |
| GET | `https://manage.devcenter.microsoft.com/v1.0/my/analytics/acquisitions` |

### Request header

| Header | Type | Description |
| --- | --- | --- |
| Authorization | string | Required. The Azure AD access token in the form **Bearer** `<token>`. |

### Request parameters

| Parameter | Type | Description | Required |
| --- | --- | --- | --- |
| applicationId | string | The product ID of the Xbox One game for which you are retrieving acquisition data. To get the product ID of your game, navigate to your game in the XDP Analytics Program and retrieve the product ID from the URL. Alternatively, if you download your acquisitions data from the Partner Center analytics report, the product ID is included in the .tsv file.  | Yes |
| startDate | date | The start date in the date range of acquisition data to retrieve. The default is the current date.  | No |
| endDate | date | The end date in the date range of acquisition data to retrieve. The default is the current date.  | No |
| top | integer | The number of rows of data to return. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data.  | No |
| skip | integer | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, *top=10000 and skip=0* retrieves the first 10000 rows of data, *top=10000 and skip=10000* retrieves the next 10000 rows of data, and so on.  | No |
| filter | string | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the **eq** or **ne** operators, and statements can be combined using **and** or **or**. String values must be surrounded by single quotes in the filter parameter. For example, *filter=market eq 'US' and gender eq 'm'*.  <br/> You can specify the following fields from the response body: <ul><li>**acquisitionType**</li><li>**age**</li><li>**storeClient**</li><li>**gender**</li><li>**market**</li><li>**osVersion**</li><li>**deviceType**</li><li>**sandboxId**</li></ul> | No |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: **day**, **week**, or **month**. If unspecified, the default is **day**.  | No |
| orderby | string | A statement that orders the result data values for each acquisition. The syntax is *orderby=field [order],field [order],...* The *field* parameter can be one of the following strings: <ul><li>**date**</li><li>**acquisitionType**</li><li>**age**</li><li>**storeClient**</li><li>**gender**</li><li>**market**</li><li>**osVersion**</li><li>**deviceType**</li><li>**paymentInstrumentType**</li><li>**sandboxId**</li><li>**xboxTitleIdHex**</li></ul> The *order* parameter is optional, and can be **asc** or **desc** to specify ascending or descending order for each field. The default is **asc**. Here is an example *orderby* string: *orderby=date,market*  | No |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields: <ul><li>**date**</li><li>**applicationName**</li><li>**acquisitionType**</li><li>**ageGroup**</li><li>**storeClient**</li><li>**gender**</li><li>**market**</li><li>**osVersion**</li><li>**deviceType**</li><li>**paymentInstrumentType**</li><li>**sandboxId**</li><li>**xboxTitleIdHex**</li></ul> The returned data rows will contain the fields specified in the *groupby* parameter as well as the following: <ul><li>**date**</li><li>**applicationId**</li><li>**acquisitionQuantity**</li></ul> The *groupby* parameter can be used with the aggregationLevel parameter. For example: *&groupby=ageGroup,market&aggregationLevel=week*  | No |

### Request example
The following example demonstrates several requests for getting Xbox One game acquisition data. Replace the *applicationId* value with the product ID for your game.  

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/acquisitions?applicationId=9WZDNCRFHXHT&startDate=1/1/2017&endDate=2/1/2019&top=10&skip=0 HTTP/1.1 
Authorization: Bearer <your access token> 

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/acquisitions?applicationId=9WZDNCRFHXHT&startDate=1/1/2017&endDate=2/1/2019&skip=0&filter=market eq 'US' and gender eq 'm' HTTP/1.1 
Authorization: Bearer <your access token> 
```

## Response

### Response body
| Value | Type | Description |
| --- | --- | --- |
| Value | array | An array of objects that contain aggregate acquisition data for the game. For more information about the data in each object, see the [acquisition values](#acquisition-values) section below. |
| @nextLink | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of acquisition data for the query. |
| TotalCount | integer | The total number of rows in the data result for the query. |

### Acquisition values 
Elements in the *Value* array contain the following values. 

| Value | Type | Description |
| --- | --- | --- |
| date | string | The first date in the date range for the acquisition data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| applicationId | string | The product ID of the Xbox One game for which you are retrieving acquisition data. |
| applicationName | string | The display name of the game. |
| acquisitionType | string | One of the following strings that indicates the type of acquisition:  <ul><li>**Free**</li><li>**Trial**</li><li>**Paid**</li><li>**Promotional code**</li><li>**Iap**</li><li>**Subscription Iap**</li><li>**Private Audience**</li><li>**Pre Order**</li><li>**Xbox Game Pass** (or **Game Pass** if querying for data before March 23, 2018)</li><li>**Disk**</li><li>**Prepaid Code**</li><li>**Charged Pre Order**</li><li>**Cancelled Pre Order**</li><li>**Failed Pre Order**</li></ul> |
| age | string | One of the following strings that indicates the age group of the user who made the acquisition: <ul><li>**Less than 13**</li><li>**13-17**</li><li>**18-24**</li><li>**25-34**</li><li>**35-44**</li><li>**44-55**</li><li>**Greater than 55**</li><li>**Unknown**</li></ul> |
| deviceType | string | One of the following strings that specifies the type of device that completed the acquisition: <ul><li>**PC**</li><li>**Phone**</li><li>**Console-Xbox One**</li><li>**Console-Xbox Series X**</li><li>**IoT**</li><li>**Server**</li><li>**Tablet**</li><li>**Holographic**</li><li>**Unknown**</li></ul> |
| gender | string | One of the following strings that specifies the gender of the user who made the acquisition: <ul><li>**m**</li><li>**f**</li><li>**Unknown**</li></ul> |
| market | string | The ISO 3166 country code of the market where the acquisition occurred. |
| osVersion | string | The OS version on which the acquisition occurred. For this method, this value is always **Windows 10**. |
| paymentInstrumentType | string | One of the following strings that indicates the payment instruction used for the acquisition: <ul><li>**Credit Card**</li><li>**Direct Debit Card**</li><li>**Inferred Purchase**</li><li>**MS Balance**</li><li>**Mobile Operator**</li><li>**Online Bank Transfer**</li><li>**PayPal**</li><li>**Split Transaction**</li><li>**Token Redemption**</li><li>**Zero Amount Paid**</li><li>**eWallet**</li><li>**Unknown**</li></ul> |
| sandboxId | string | The sandbox ID created for the game. This can be the value **RETAIL** or a private sandbox ID. |
| storeClient | string | One of the following strings that indicates the version of the Store where the acquisition occurred: <ul><li>**Windows Phone Store (client)**</li><li>**Microsoft Store (client)** (or **Windows Store (client)** if querying for data before March 23, 2018) </li><li>**Microsoft Store (web)** (or **Windows Store (web)** if querying for data before March 23, 2018) </li><li>**Volume purchase by organizations**</li><li>**Other**</li></ul> |
| xboxTitleId | string | The Xbox Live title ID (represented in hexadecimal value) assigned by the Xbox Developer Portal (XDP) for Xbox Live-enabled games. |
| acquisitionQuantity | number | The number of acquisitions that occurred during the specified aggregation level. |
| purchasePriceUSDAmount | number | The amount paid by the customer for the acquisition, converted to USD, using the monthly exchange rate. |
| purchaseTaxUSDAmount | number | The tax amount applied to the acquisition, converted to USD. |
| localCurrencyCode | string | Local Currency code based on the country of the Partner Center account.  |
| xboxProductId | string | Xbox Product ID of the product from XDP, if applicable.  |
| availabilityId | string | Availability ID of the product from XDP, if applicable.  |
| skuId | string | SKU ID of the product from XDP, if applicable.  |
| skuDisplayName  | string | SKU display name of the product from XDP, if applicable.  |
| xboxParentProductId | string | Xbox Parent Product ID of the product from XDP, if applicable.  |
| parentProductName | string | Parent Product Name of the product from XDP, if applicable.  |
| productTypeName | string | Product Type Name of the product from XDP, if applicable.  |
| purchaseTaxType | string | Purchase Tax Type of the product from XDP, if applicable.  |
| purchasePriceLocalAmount | number | Purchase Price Local Amount of the product from XDP, if applicable.  |
| purchaseTaxLocalAmount | number | Purchase Tax Local Amount of the product from XDP, if applicable.  |

### Response example
The following example demonstrates an example JSON response body for this request. 

```JSON
{ 
    "Value": [ 
        { 
            "date": "2019-01-15T01:00:00.0000000Z", 
            "applicationId": "9WZDNCRFHXHT", 
            "applicationName": null, 
            "acquisitionType": "Paid", 
            "age": null, 
            "deviceType": "Phone", 
            "gender": null, 
            "market": "US", 
            "osVersion": "Windows 10", 
            "paymentInstrumentType": null, 
            "sandboxId": "RETAIL", 
            "storeClient": "Microsoft Store (client)", 
            "xboxTitleId": null, 
            "localCurrencyCode": "USD", 
            "xboxProductId": null, 
            "availabilityId": "B42LRTSZ2MCJ", 
            "skuId": "0010", 
            "skuDisplayName": null, 
            "xboxParentProductId": null, 
            "parentProductName": null, 
            "productTypeName": "Game", 
            "purchaseTaxType": "TaxesNotIncluded", 
            "acquisitionQuantity": 1, 
            "purchasePriceUSDAmount": 3.08, 
            "purchasePriceLocalAmount": 3.08, 
            "purchaseTaxUSDAmount": 0.09, 
            "purchaseTaxLocalAmount": 0.09 
        } 
    ], 

    "@nextLink": "acquisitions?applicationId=9WZDNCRFHXHT&aggregationLevel=day&startDate=2017-01-01T08:00:00.0000000Z&endDate=2019-01-16T08:44:15.6045249Z&top=1&skip=1", 
    
    "TotalCount": 12221 
} 
```

 
