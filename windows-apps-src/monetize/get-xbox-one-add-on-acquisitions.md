---

description: Use this method in the Microsoft Store analytics API to get aggregate add-on acquisition data.
title: Get Xbox One add-on acquisitions

ms.date: 10/18/2018
ms.topic: article


keywords: windows 10, uwp, Store services, Microsoft Store analytics API, Xbox One add-on acquisitions
ms.localizationpriority: medium
---

# Get Xbox One add-on acquisitions

Use this method in the Microsoft Store analytics API to get aggregate add-on acquisition data in JSON format for an Xbox One game that was ingested through the Xbox Developer Portal (XDP) and available in the XDP Analytics Partner Center dashboard.

## Prerequisites

To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.

## Request


### Request syntax

| Method | Request URI                                                                |
|--------|----------------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/addonacquisitions``` |


### Request header

| Header        | Type   | Description          |
|---------------|--------|--------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

The *applicationId* or *addonProductId* parameter is required. To retrieve acquisition data for all add-ons registered to the app, specify the *applicationId* parameter. To retrieve acquisition data for a single add-on, specify the *addonProductId* parameter. If you specify both, the *applicationId* parameter is ignored.

| Parameter        | Type   |  Description      |  Required  |
|---------------|--------|---------------|------|
| applicationId | string | The *productId* of the Xbox One game for which you are retrieving acquisition data. To get the *productId* of your game, navigate to your game in the XDP Analytics Program and retrieve the *productId* from the URL. Alternatively, if you download your acquisitions data from the Partner Center analytics report, the *productId* is included in the .tsv file. |  Yes  |
| addonProductId | string | The *productId* of the add-on for which you want to retrieve acquisition data.  | Yes  |
| startDate | date | The start date in the date range of add-on acquisition data to retrieve. The default is the current date. |  No  |
| endDate | date | The end date in the date range of add-on acquisition data to retrieve. The default is the current date. |  No  |
| top | int | The number of rows of data to return in the request. The maximum value and the default value if not specified is 10000. If there are more rows in the query, the response body includes a next link that you can use to request the next page of data. |  No  |
| skip | int | The number of rows to skip in the query. Use this parameter to page through large data sets. For example, top=10000 and skip=0 retrieves the first 10000 rows of data, top=10000 and skip=10000 retrieves the next 10000 rows of data, and so on. |  No  |
| filter |string  | <p>One or more statements that filter the rows in the response. Each statement contains a field name from the response body and value that are associated with the eq or ne operators, and statements can be combined using and or or. String values must be surrounded by single quotes in the filter parameter. For example, filter=market eq 'US' and gender eq 'm'.</p> <p>You can specify the following fields from the response body:</p> <ul><li><strong>acquisitionType</strong></li><li><strong>age</strong></li><li><strong>storeClient</strong></li><li><strong>gender</strong></li><li><strong>market</strong></li><li><strong>osVersion</strong></li><li><strong>deviceType</strong></li><li><strong>sandboxId</strong></li></ul>| No   |
| aggregationLevel | string | Specifies the time range for which to retrieve aggregate data. Can be one of the following strings: <strong>day</strong>, <strong>week</strong>, or <strong>month</strong>. If unspecified, the default is <strong>day</strong>. | No |
| orderby | string | A statement that orders the result data values for each add-on acquisition. The syntax is <em>orderby=field [order],field [order],...</em> The <em>field</em> parameter can be one of the following strings:<ul><li><strong>date</strong></li><li><strong>acquisitionType</strong></li><li><strong>age</strong></li><li><strong>storeClient</strong></li><li><strong>gender</strong></li><li><strong>market</strong></li><li><strong>osVersion</strong></li><li><strong>deviceType</strong></li><li><strong>orderName</strong></li></ul><p>The <em>order</em> parameter is optional, and can be <strong>asc</strong> or <strong>desc</strong> to specify ascending or descending order for each field. The default is <strong>asc</strong>.</p><p>Here is an example <em>orderby</em> string: <em>orderby=date,market</em></p> |  No  |
| groupby | string | A statement that applies data aggregation only to the specified fields. You can specify the following fields:<ul><li><strong>date</strong></li><li><strong>applicationName</strong></li><li><strong>addonProductName</strong></li><li><strong>acquisitionType</strong></li><li><strong>age</strong></li><li><strong>storeClient</strong></li><li><strong>gender</strong></li><li><strong>market</strong></li><li><strong>osVersion</strong></li><li><strong>deviceType</strong></li><li><strong>paymentInstrumentType</strong></li><li><strong>sandboxId</strong></li><li><strong>xboxTitleIdHex</strong></li></ul><p>The returned data rows will contain the fields specified in the <em>groupby</em> parameter as well as the following:</p><ul><li><strong>date</strong></li><li><strong>applicationId</strong></li><li><strong>addonProductId</strong></li><li><strong>acquisitionQuantity</strong></li></ul><p>The <em>groupby</em> parameter can be used with the <em>aggregationLevel</em> parameter. For example: <em>&amp;groupby=age,market&amp;aggregationLevel=week</em></p> |  No  |


### Request example

The following examples demonstrates several requests for getting add-on acquisition data. Replace the *addonProductId* and *applicationId* values with the appropriate Store ID for your add-on or app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/addonacquisitions?addonProductId=BRRT4NJ9B3D2&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/addonacquisitions?applicationId=BRRT4NJ9B3D1&startDate=1/1/2015&endDate=2/1/2015&top=10&skip=0 HTTP/1.1
Authorization: Bearer <your access token>

GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/xbox/addonacquisitions?addonProductId=BRRT4NJ9B3D2&startDate=1/1/2015&endDate=7/3/2015&top=100&skip=0&filter=market ne 'US' and gender ne 'Unknown' and gender ne 'm' and market ne 'NO' and age ne '>55' HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type   | Description         |
|------------|--------|------------------|
| Value      | array  | An array of objects that contain aggregate add-on acquisition data. For more information about the data in each object, see the [add-on acquisition values](#add-on-acquisition-values) section below.                                                                                                              |
| @nextLink  | string | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10000 but there are more than 10000 rows of add-on acquisition data for the query. |
| TotalCount | int    | The total number of rows in the data result for the query.    |


<span id="add-on-acquisition-values" />

### Add-on acquisition values

Elements in the *Value* array contain the following values.

| Value               | Type    | Description        |
|---------------------|---------|---------------------|
| date                | string  | The first date in the date range for the acquisition data. If the request specified a single day, this value is that date. If the request specified a week, month, or other date range, this value is the first date in that date range. |
| addonProductId      | string  | The *productId* of the add-on for which you are retrieving acquisition data.                                                                                                                                                                 |
| addonProductName    | string  | The display name of the add-on. This value only appears in the response data if the *aggregationLevel* parameter is set to **day**, unless you specify the **addonProductName** field in the *groupby* parameter.                                                                                                                                                                                                            |
| applicationId       | string  | The *productId* of the app for which you want to retrieve add-on acquisition data.                                                                                                                                                           |
| applicationName     | string  | The display name of the game.                                                                                                                                                                                                             |
| deviceType          | string  | <p>One of the following strings that specifies the type of device that completed the acquisition:</p> <ul><li>"PC"</li><li>"Phone"</li><li>"Console"</li><li>"IoT"</li><li>"Server"</li><li>"Tablet"</li><li>"Holographic"</li><li>"Unknown"</li></ul>                                                                                                  |
| storeClient         | string  | <p>One of the following strings that indicates the version of the Store where the acquisition occurred:</p> <ul><li>"Windows Phone Store (client)"</li><li>"Microsoft Store (client)" (or "Windows Store (client)" if querying for data before March 23, 2018)</li><li>"Microsoft Store (web)" (or "Windows Store (web)" if querying for data before March 23, 2018)</li><li>"Volume purchase by organizations"</li><li>"Other"</li></ul>                                                                                            |
| osVersion           | string  | The OS version on which the acquisition occurred. For this method, this value is always "Windows 10".                                                                                                   |
| market              | string  | The ISO 3166 country code of the market where the acquisition occurred.                                                                                                                                                                  |
| gender              | string  | <p>One of the following strings that specifies the gender of the user who made the acquisition:</p> <ul><li>"m"</li><li>"f"</li><li>"Unknown"</li></ul>                                                                                                    |
| age            | string  | <p>One of the following strings that indicates the age group of the user who made the acquisition:</p> <ul><li>"less than 13"</li><li>"13-17"</li><li>"18-24"</li><li>"25-34"</li><li>"35-44"</li><li>"44-55"</li><li>"greater than 55"</li><li>"Unknown"</li></ul>                                                                                                 |
| acquisitionType     | string  | <p>One of the following strings that indicates the type of acquisition:</p> <ul><li>"Free"</li><li>"Trial"</li><li>"Paid"</li><li>"Promotional code"</li><li>"Iap"</li><li>"Subscription Iap"</li><li>"Private Audience"</li><li>"Pre Order"</li><li>"Xbox Game Pass" (or "Game Pass" if querying for data before March 23, 2018)</li><li>"Disk"</li><li>"Prepaid Code"</li><li>"Charged Pre Order"</li><li>"Cancelled Pre Order"</li><li>"Failed Pre Order"</li></ul>                                                                                                    |
| acquisitionQuantity | integer | The number of acquisitions that occurred.                        |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "date": "2018-10-18",
      "addonProductId": " BRRT4NJ9B3D2",
      "addonProductName": "Contoso add-on 7",
      "applicationId": "BRRT4NJ9B3D1",
      "applicationName": "Contoso Demo",
      "deviceType": "Console",
      "storeClient": "Windows Phone Store (client)",
      "osVersion": "Windows 10",
      "market": "GB",
      "gender": "m",
      "age": "50orover",
      "acquisitionType": "iap",
      "acquisitionQuantity": 1
    }
  ],
  "@nextLink": "addonacquisitions?applicationId=BRRT4NJ9B3D1&addonProductId=&aggregationLevel=day&startDate=2015/01/01&endDate=2016/02/01&top=1&skip=1",
  "TotalCount": 33677
}
```
