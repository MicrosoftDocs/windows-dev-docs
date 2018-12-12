---
ms.assetid: b556a245-6359-4ddc-a4bd-76f9873ab694
description: Use this method in the Microsoft Store analytics API to get the stack trace for an error in your app.
title: Get the stack trace for an error in your app
ms.date: 06/05/2018
ms.topic: article
keywords: windows 10, uwp, Store services, Microsoft Store analytics API, stack trace, error
ms.localizationpriority: medium
---
# Get the stack trace for an error in your app

Use this method in the Microsoft Store analytics API to get the stack trace for an error in your app. This method can only download the stack trace for an app error that occurred in the last 30 days. Stack traces are also available in the **Failures** section of the [Health report](../publish/health-report.md) in Partner Center.

Before you can use this method, you must first use the [get details for an error in your app](get-details-for-an-error-in-your-app.md) method to retrieve the ID of the CAB file that is associated with the error for which you want to retrieve the stack trace.

## Prerequisites


To use this method, you need to first do the following:

* If you have not done so already, complete all the [prerequisites](access-analytics-data-using-windows-store-services.md#prerequisites) for the Microsoft Store analytics API.
* [Obtain an Azure AD access token](access-analytics-data-using-windows-store-services.md#obtain-an-azure-ad-access-token) to use in the request header for this method. After you obtain an access token, you have 60 minutes to use it before it expires. After the token expires, you can obtain a new one.
* Get the ID of the CAB file that is associated with the error for which you want to retrieve the stack trace. To get this ID, use the [get details for an error in your app](get-details-for-an-error-in-your-app.md) method to retrieve details for a specific error in your app, and use the **cabId** value in the response body of that method.

## Request


### Request syntax

| Method | Request URI                                                          |
|--------|----------------------------------------------------------------------|
| GET    | ```https://manage.devcenter.microsoft.com/v1.0/my/analytics/stacktrace``` |


### Request header

| Header        | Type   | Description                                                                 |
|---------------|--------|-----------------------------------------------------------------------------|
| Authorization | string | Required. The Azure AD access token in the form **Bearer** &lt;*token*&gt;. |


### Request parameters

| Parameter        | Type   |  Description      |  Required  |
|---------------|--------|---------------|------|
| applicationId | string | The Store ID of the app for which you want to get the stack trace. The Store ID is available on the [App identity page](../publish/view-app-identity-details.md) in Partner Center. An example Store ID is 9WZDNCRFJ3Q8. |  Yes  |
| cabId | string | The unique ID of the CAB file that is associated with the error for which you want to retrieve the stack trace. To get this ID, use the [get details for an error in your app](get-details-for-an-error-in-your-app.md) method to retrieve details for a specific error in your app, and use the **cabId** value in the response body of that method. |  Yes  |

 
### Request example

The following example demonstrates how to get a stack trace using this method. Replace the *applicationId* value with the Store ID for your app.

```syntax
GET https://manage.devcenter.microsoft.com/v1.0/my/analytics/stacktrace?applicationId=9NBLGGGZ5QDR&cabId=1336373323853 HTTP/1.1
Authorization: Bearer <your access token>
```

## Response


### Response body

| Value      | Type    | Description                  |
|------------|---------|--------------------------------|
| Value      | array   | An array of objects that each contain one frame of stack trace data. For more information about the data in each object, see the [stack trace values](#stack-trace-values) section below. |
| @nextLink  | string  | If there are additional pages of data, this string contains a URI that you can use to request the next page of data. For example, this value is returned if the **top** parameter of the request is set to 10 but there are more than 10 rows of errors for the query. |
| TotalCount | integer | The total number of rows in the data result for the query.          |


### Stack trace values

Elements in the *Value* array contain the following values.

| Value           | Type    | Description      |
|-----------------|---------|----------------|
| level            | string  |  The frame number that this element represents in the call stack.  |
| image   | string  |   The name of the executable or library image that contains the function that is called in this stack frame.           |
| function | string  |  The name of the function that is called in this stack frame. This is available only if your app includes symbols for the executable or library.              |
| offset     | string  |  The byte offset of the current instruction relative to the start of the function.      |


### Response example

The following example demonstrates an example JSON response body for this request.

```json
{
  "Value": [
    {
      "level": "0",
      "image": "Contoso.ContosoApp",
      "function": "Contoso.ContosoApp.MainPage.DoWork",
      "offset": "0x25C"
    }
    {
      "level": "1",
      "image": "Contoso.ContosoApp",
      "function": "Contoso.ContosoApp.MainPage.Initialize",
      "offset": "0x26"
    }
    {
      "level": "2",
      "image": "Contoso.ContosoApp",
      "function": "Contoso.ContosoApp.Start",
      "offset": "0x66"
    }
  ],
  "@nextLink": null,
  "TotalCount": 3
}

```

## Related topics

* [Health report](../publish/health-report.md)
* [Access analytics data using Microsoft Store services](access-analytics-data-using-windows-store-services.md)
* [Get error reporting data](get-error-reporting-data.md)
* [Get details for an error in your app](get-details-for-an-error-in-your-app.md)
* [Download the CAB file for an error in your app](download-the-cab-file-for-an-error-in-your-app.md)
