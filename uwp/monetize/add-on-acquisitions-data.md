---
description: Use this method in the Microsoft Store analytics API to get aggregate add-on acquisition data in JSON format for UWP apps and Xbox One games that were ingested through the Xbox Developer Portal (XDP) and available in the XDP Analytics Partner Center dashboard.  
title: Get add-on acquisitions data for your games and apps 
ms.date: 03/06/2019
ms.topic: article
keywords: windows 10, uwp, advertising network, app metadata
ms.localizationpriority: medium
---

# Get add-on acquisitions data for your games and apps 
Use this method in the Microsoft Store analytics API to get aggregate add-on acquisition data in JSON format for UWP apps and Xbox One games that were ingested through the Xbox Developer Portal (XDP) and available in the XDP Analytics Partner Center dashboard. 

## Prerequisites
To use this method, you need to first do the following: 

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md) for the Microsoft Store analytics API. 
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one. 

> [!NOTE]
> This API does not provide daily aggregate data before Oct 1st 2016. 

## Request

### Request syntax
| Method | Request URI |
| --- | --- | 
| GET | `https://manage.devcenter.microsoft.com/v1.0/my/analytics/addonacquisitions` |

### Request header 
| Header | Type | Description | 
| --- | --- | --- |
| Authorization | string | Required. The Azure AD access token in the form **Bearer** `<token>`. |

### Request parameters
The *applicationId* or *addonProductId* parameter is required. To retrieve acquisition data for all add-ons registered to the app, specify the *applicationId* parameter. To retrieve acquisition data for a single add-on, specify the *addonProductId* parameter. If you specify both, the *applicationId* parameter is ignored. 

| Parameter | Type | Description | Required | 
| --- | --- | --- | --- |
| applicationId | string | The *productId* of the Xbox One game for which you are retrieving acquisition data. To get the *productId* of your game, navigate to your game in the XDP Analytics Program and retrieve the *productId* from the URL. Alternatively, if you download your acquisitions data from the Partner Center analytics report, the *productId* is included in the .tsv file. | Yes |
| addonProductId | string | The *productId* of the add-on for which you want to retrieve acquisition data. | Yes |
| startDate | date | The start date in the date range of add-on acquisition data to retrieve. The default is the current date. | No |
| endDate | date | The end date in the date range of add-on acquisition data to retrieve. The default is the current date. | No |
| filter | string | One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the eq or ne operators, and statements can be combined using and or or. String values must be surrounded by single quotes in the filter parameter. For example, filter=market eq 'US' and gender eq 'm'. <br/> You can specify the following fields from the response body: <ul><li>**acquisitionType**</li><li>**age**</li><li>**storeClient**</li><li>**gender**</li><li>**market**</li><li>**osVersion**</li><li>**deviceType**</li><li>**sandboxId**</li></ul> | No |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: **day**, **week**, or **month**. If unspecified, the default is **day**. | No |
| orderby | string | A statement that orders the result data values for each add-on acquisition. The syntax is *orderby=field [order],field [order],...* The *field* parameter can be one of the following strings: <ul><li>**date**</li><li>**acquisitionType**</li><li>**age**</li><li>**storeClient**</li><li>**gender**</li><li>**market**</li><li>**osVersion**</li><li>**deviceType**</li><li>**orderName**</li></ul> The order parameter is optional, and can be **asc** or **desc** to specify ascending or descending order for each field. The default is **asc**. <br/> Here is an example *orderby* string: *orderby=date,market* | No |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields: <ul><li>**date**</li><li>**applicationName**</li><li>**addonProductName**</li> <li>**acquisitionType**</li><li>**age**</li> <li>**storeClient**</li><li>**gender**</li> <li>**market**</li> <li>**osVersion**</li><li>**deviceType**</li><li>**paymentInstrumentType**</li><li>**sandboxId**</li><li>**xboxTitleIdHex**</li></ul> The returned data rows will contain the fields specified in the *groupby* parameter as well as the following: <ul><li>**date**</li><li>**applicationId**</li><li>**addonProductId**</li><li>**acquisitionQuantity**</li></ul> The groupby parameter can be used with the *aggregationLevel* parameter. For example: *&groupby=age,market&aggregationLevel=week* | No |

### Request example
The following examples demonstrates several requests for getting add-on acquisition data. Replace the *addonProductId* and *applicationId* values with the appropriate Store ID for your add-on or app. 

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/addonacquisitions?applicationId=9WZDNCRFJ314&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0 HTTP/1.1 

Authorization: Bearer <your access token> 

 

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/addonacquisitions?applicationId=9WZDNCRFJ314&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0&filter=market eq 'GB' and gender eq 'm' HTTP/1.1 

Authorization: Bearer <your access token>
```

## Response

### Response body

| Value | Type | Description |
| --- | --- | --- |
| Value | array | An array of objects that contain aggregate add-on acquisition data. For more information about the data in each object, see the [add-on acquisition values](#add-on-acquisition-values) section below. |
| TotalCount | int | The total number of rows in the data result for the query. |

### Add-on acquisition values
Elements in the Value array contain the following values.

| Value | Type | Description | 
| --- | --- | --- |
| date | string | The first date in the date range for the acquisition data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| addonProductId | string | The *productId* of the add-on for which you are retrieving acquisition data. |
| addonProductName | string | The display name of the add-on. This value only appears in the response data if the *aggregationLevel* parameter is set to **day**, unless you specify the **addonProductName** field in the *groupby* parameter. |
| applicationId | string | The *productId* of the app for which you want to retrieve add-on acquisition data. |
| applicationName | string | The display name of the game. |
| deviceType | string | One of the following strings that specifies the type of device that completed the acquisition: <ul><li>"PC"</li><li>"Phone"</li><li>"Console-Xbox One"</li><li>"Console-Xbox Series X"</li><li>"IoT"</li><li>"Server"</li><li>"Tablet"</li><li>"Holographic"</li><li>"Unknown"</li></ul> |
| storeClient | string | One of the following strings that indicates the version of the Store where the acquisition occurred: <ul><li>"Windows Phone Store (client)"</li><li>"Microsoft Store (client)" (or "Windows Store (client)" if querying for data before March 23, 2018)</li><li>"Microsoft Store (web)" (or "Windows Store (web)" if querying for data before March 23, 2018)</li><li>"Volume purchase by organizations"</li><li>"Other"</li></ul> |
| osVersion | string | The OS version on which the acquisition occurred. For this method, this value is always either **Windows 10** or **Windows 11**". |
| market | string | The ISO 3166 country code of the market where the acquisition occurred. |
| gender | string | One of the following strings that specifies the gender of the user who made the acquisition: <ul><li>"m"</li><li>"f"</li><li>"Unknown"</li></ul> |
| age | string | One of the following strings that indicates the age group of the user who made the acquisition: <ul><li>"less than 13"</li><li>"13-17"</li><li>"18-24"</li><li>"25-34"</li><li>"35-44"</li><li>"44-55"</li><li>"greater than 55"</li><li>"Unknown"</li></ul> |
| acquisitionType | string | One of the following strings that indicates the type of acquisition: <ul><li>"Free" </li><li>"Trial" </li><li>"Paid"</li><li>"Promotional code" </li><li>"Iap"</li><li>"Subscription Iap"</li><li>"Private Audience"</li><li>"Pre Order"</li><li>"Xbox Game Pass" (or "Game Pass" if querying for data before March 23, 2018)</li><li>"Disk"</li><li>"Prepaid Code"</li><li>"Charged Pre Order"</li><li>"Cancelled Pre Order"</li><li>"Failed Pre Order"</li></ul> |
| acquisitionQuantity | integer | The number of acquisitions that occurred. |
| inAppProductId | string | Product ID of the product where this add-on is used.  |
| inAppProductName | string | Product Name of the product where this add-on is used.  |
| paymentInstrumentType | string | Payment instrument type used for the acquisition.  |
| sandboxId | string | The Sandbox ID created for the game. This can be the value **RETAIL** or a private sandbox ID.  |
| xboxTitleId | string | Xbox Title ID of the product from XDP, if applicable.  |
| localCurrencyCode | string | Local Currency code based on the country of the Partner Center account.  |
| xboxProductId | string | Xbox Product ID of the product from XDP, if applicable.  |
| availabilityId | string | Availability ID of the product from XDP, if applicable.  |
| skuId | string | SKU ID of the product from XDP, if applicable.  |
| skuDisplayName | string | SKU Display Name of the product from XDP, if applicable.  |
| xboxParentProductId | string | Xbox Parent Product ID of the product from XDP, if applicable.  |
| parentProductName | string | Parent Product Name of the product from XDP, if applicable.  |
| productTypeName | string | Product Type Name of the product from XDP, if applicable.  |
| purchaseTaxType | string | Purchase Tax Type of the product from XDP, if applicable.  |
| purchasePriceUSDAmount | number | The amount paid by the customer for the add-on, converted to USD.  |
| purchasePriceLocalAmount | number | The amount paid by the customer for the add-on, in the region's currency.  |
| purchaseTaxUSDAmount | number | The tax amount applied to the add-on, converted to USD.  |
| purchaseTaxLocalAmount | number | Purchase Tax Local Amount of the product from XDP, if applicable.  |

### Response example
The following example demonstrates an example JSON response body for this request. 

```JSON
{ 
  "Value": [ 
    { 
            "inAppProductId": "9NBLGGH1864K", 
            "inAppProductName": "866879", 
            "addonProductId": "9NBLGGH1864K", 
            "addonProductName": "866879", 
            "date": "2017-11-05", 
            "applicationId": "9WZDNCRFJ314", 
            "applicationName": "Tetris Blitz", 
            "acquisitionType": "Iap", 
            "age": "35-49", 
            "deviceType": "Phone", 
            "gender": "m", 
            "market": "US", 
            "osVersion": "Windows Phone 8.1", 
            "paymentInstrumentType": "Credit Card", 
            "sandboxId": "RETAIL", 
            "storeClient": "Windows Phone Store (client)", 
            "xboxTitleId": "", 
            "localCurrencyCode": "USD", 
            "xboxProductId": "00000000-0000-0000-0000-000000000000", 
            "availabilityId": "", 
            "skuId": "", 
            "skuDisplayName": "Full", 
            "xboxParentProductId": "", 
            "parentProductName": "Tetris Blitz", 
            "productTypeName": "Add-On", 
            "purchaseTaxType": "", 
            "acquisitionQuantity": 1, 
            "purchasePriceUSDAmount": 1.08, 
            "purchasePriceLocalAmount": 0.09, 
            "purchaseTaxUSDAmount": 1.08, 
            "purchaseTaxLocalAmount": 0.09 
        } 
    ], 

    "@nextLink": null, 
    
    "TotalCount": 7601 
} 
```
