---
author: mcleanbyron
Description: Learn about several ways you can programmatically enable customers to rate and review your app.
title: Request ratings and reviews for your app
ms.author: mcleans
ms.date: 06/15/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ratings, reviews
ms.localizationpriority: medium
---

# Request ratings and reviews for your app

You can add code to your Universal Windows Platform (UWP) app to programmatically prompt your customers to rate or review your app. There are several ways you can do this:
* You can show a rating and review dialog directly in the context of your app.
* You can programmatically open the rating and review page for your app in the Microsoft Store.

When you are ready to analyze your ratings and reviews data, you can view the data in the Windows Dev Center dashboard or use the Microsoft Store analytics API to retrieve this data programmatically.

> [!IMPORTANT]
> When adding a rating function within your app, all reviews must send the user to the Store’s rating mechanisms, regardless of star rating chosen. If you collect feedback or comments from users, it must be clear that it is not related to the app rating or reviews in the Store but is sent directly to the app developer. See the Developer Code of Conduct for more information related to [Fraudulent or Dishonest Activities](https://docs.microsoft.com/legal/windows/agreements/store-developer-code-of-conduct#3-fraudulent-or-dishonest-activities).

## Show a rating and review dialog in your app

To programmatically show a dialog from your app that asks your customer to rate your app and submit a review, call the [SendRequestAsync](https://docs.microsoft.com/uwp/api/windows.services.store.storerequesthelper.sendrequestasync) method in the [Windows.Services.Store](https://docs.microsoft.com/uwp/api/windows.services.store) namespace. Pass the integer 16 to the *requestKind* parameter and an empty string to the *parametersAsJson* parameter as shown in this code example. This example requires the [Json.NET](http://www.newtonsoft.com/json) library from Newtonsoft, and it requires using statements for the **Windows.Services.Store**, **System.Threading.Tasks**, and **Newtonsoft.Json.Linq** namespaces.

> [!IMPORTANT]
> The request to show the rating and review dialog must be called on the UI thread in your app.

```csharp
public async Task<bool> ShowRatingReviewDialog()
{
    StoreSendRequestResult result = await StoreRequestHelper.SendRequestAsync(
        StoreContext.GetDefault(), 16, String.Empty);

    if (result.ExtendedError == null)
    {
        JObject jsonObject = JObject.Parse(result.Response);
        if (jsonObject.SelectToken("status").ToString() == "success")
        {
            // The customer rated or reviewed the app.
            return true;
        }
    }

    // There was an error with the request, or the customer chose not to
    // rate or review the app.
    return false;
}
```

The **SendRequestAsync** method uses a simple integer-based request system and JSON-based data parameters to expose miscellaneous Store operations to apps. When you pass the integer 16 to the *requestKind* parameter, you issue a request to show the rating and review dialog and send the related data to the Store. This method was introduced in Windows 10, version 1607, and it can only be used in projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. For a general overview of this method, see [Send requests to the Store](send-requests-to-the-store.md).

### Response data for the rating and review request

After you submit the request to display the rating and review dialog, the [Response](https://docs.microsoft.com/uwp/api/windows.services.store.storesendrequestresult.Response) property of the [StoreSendRequestResult](https://docs.microsoft.com/uwp/api/windows.services.store.storesendrequestresult) return value contains a JSON-formatted string that indicates whether the request was successful.

The following example demonstrates the return value for this request after the customer successfully submits a rating or review.

```json
{ 
  "status": "success", 
  "data": {
    "updated": false
  },
  "errorDetails": "Success"
}
```

The following example demonstrates the return value for this request after the customer chooses not to submit a rating or review.

```json
{ 
  "status": "aborted", 
  "errorDetails": "Navigation was unsuccessful"
}
```

The following table describes the fields in the JSON-formatted data string.

|  Field  |  Description  |
|----------------------|---------------|
|  *status*                   |  A string that indicates whether the customer successfully submitted a rating or review. The supported values are **success** and **aborted**.   |
|  *data*                   |  An object that contains a single Boolean value named *updated*. This value indicates whether the customer updated an existing rating or review. The *data* object is included in success responses only.   |
|  *errorDetails*                   |  A string that contains the error details for the request. |

## Launch the rating and review page for your app in the Store

If you want to programmatically open the rating and review page for your app in the Store, you can use the [LaunchUriAsync](https://docs.microsoft.com/uwp/api/windows.system.launcher.launchuriasync) method with the ```ms-windows-store://review``` URI scheme as demonstrated in this code example.

```csharp
bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9WZDNCRFHVJL"));
```

For more information, see [Launch the Microsoft Store app](../launch-resume/launch-store-app.md).

## Analyze your ratings and reviews data

To analyze the ratings and reviews data from your customers, you have several options:
* You can use the [Reviews](../publish/reviews-report.md) report in the Windows Dev Center dashboard to see the ratings and reviews from your customers. You can also download this report to view it offline.
* You can use the [Get app ratings](get-app-ratings.md) and [Get app reviews](get-app-reviews.md) methods in the Store analytics API to programmatically retrieve the ratings and reviews from your customers in JSON format.

## Related topics

* [Send requests to the Store](send-requests-to-the-store.md)
* [Launch the Microsoft Store app](../launch-resume/launch-store-app.md)
* [Reviews report](../publish/reviews-report.md)
