---
Description: Learn about several ways you can programmatically enable customers to rate and review your app.
title: Request ratings and reviews for your app
ms.date: 01/22/2019
ms.topic: article
keywords: windows 10, uwp, ratings, reviews
ms.localizationpriority: medium
---
# Request ratings and reviews for your app

You can add code to your Universal Windows Platform (UWP) app to programmatically prompt your customers to rate or review your app. There are several ways you can do this:
* You can show a rating and review dialog directly in the context of your app.
* You can programmatically open the rating and review page for your app in the Microsoft Store.

When you are ready to analyze your ratings and reviews data, you can view the data in Partner Center or use the Microsoft Store analytics API to retrieve this data programmatically.

> [!IMPORTANT]
> When adding a rating function within your app, all reviews must send the user to the Store's rating mechanisms, regardless of star rating chosen. If you collect feedback or comments from users, it must be clear that it is not related to the app rating or reviews in the Store but is sent directly to the app developer. See the Developer Code of Conduct for more information related to [Fraudulent or Dishonest Activities](/legal/windows/agreements/store-developer-code-of-conduct#3-fraudulent-or-dishonest-activities).

## Show a rating and review dialog in your app

To programmatically show a dialog from your app that asks your customer to rate your app and submit a review, call the [RequestRateAndReviewAppAsync](/uwp/api/windows.services.store.storecontext.requestrateandreviewappasync) method in the [Windows.Services.Store](/uwp/api/windows.services.store) namespace. 

> [!IMPORTANT]
> The request to show the rating and review dialog must be called on the UI thread in your app.

```csharp
using Windows.ApplicationModel.Store;

private StoreContext _storeContext;

public async Task Initialize()
{
    if (App.IsMultiUserApp) // pseudo-code
    {
        IReadOnlyList<User> users = await User.FindAllAsync();
        User firstUser = users[0];
        _storeContext = StoreContext.GetForUser(firstUser);
    }
    else
    {
        _storeContext = StoreContext.GetDefault();
    }
}

private async Task PromptUserToRateApp()
{
    // Check if we’ve recently prompted user to review, we don’t want to bother user too often and only between version changes
    if (HaveWePromptedUserInPastThreeMonths())  // pseudo-code
    {
        return;
    }

    StoreRateAndReviewResult result = await 
        _storeContext.RequestRateAndReviewAppAsync();

    // Check status
    switch (result.Status)
    { 
        case StoreRateAndReviewStatus.Succeeded:
            // Was this an updated review or a new review, if Updated is false it means it was a users first time reviewing
            if (result.UpdatedExistingRatingOrReview)
            {
                // This was an updated review thank user
                ThankUserForReview(); // pseudo-code
            }
            else
            {
                // This was a new review, thank user for reviewing and give some free in app tokens
                ThankUserForReviewAndGrantTokens(); // pseudo-code
            }
            // Keep track that we prompted user and don’t do it again for a while
            SetUserHasBeenPrompted(); // pseudo-code
            break;

        case StoreRateAndReviewStatus.CanceledByUser:
            // Keep track that we prompted user and don’t prompt again for a while
            SetUserHasBeenPrompted(); // pseudo-code

            break;

        case StoreRateAndReviewStatus.NetworkError:
            // User is probably not connected, so we’ll try again, but keep track so we don’t try too often
            SetUserHasBeenPromptedButHadNetworkError(); // pseudo-code

            break;

        // Something else went wrong
        case StoreRateAndReviewStatus.OtherError:
        default:
            // Log error, passing in ExtendedJsonData however it will be empty for now
            LogError(result.ExtendedError, result.ExtendedJsonData); // pseudo-code
            break;
    }
}
```

The **RequestRateAndReviewAppAsync** method was introduced in Windows 10, version 1809, and it can only be used in projects that target **Windows 10 October 2018 Update (10.0; Build 17763)** or a later release in Visual Studio.

### Response data for the rating and review request

After you submit the request to display the rating and review dialog, the [ExtendedJsonData](/uwp/api/windows.services.store.storerateandreviewresult.extendedjsondata) property of the [StoreRateAndReviewResult](/uwp/api/windows.services.store.storerateandreviewresult) class contains a JSON-formatted string that indicates whether the request was successful.

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

| Field          | Description                                                                                                                                   |
|----------------|-----------------------------------------------------------------------------------------------------------------------------------------------|
| *status*       | A string that indicates whether the customer successfully submitted a rating or review. The supported values are **success** and **aborted**. |
| *data*         | An object that contains a single Boolean value named *updated*. This value indicates whether the customer updated an existing rating or review. The *data* object is included in success responses only. |
| *errorDetails* | A string that contains the error details for the request.                                                                                     |

## Launch the rating and review page for your app in the Store

If you want to programmatically open the rating and review page for your app in the Store, you can use the [LaunchUriAsync](/uwp/api/windows.system.launcher.launchuriasync) method with the ```ms-windows-store://review``` URI scheme as demonstrated in this code example.

```csharp
bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9WZDNCRFHVJL"));
```

For more information, see [Launch the Microsoft Store app](../launch-resume/launch-store-app.md).

## Analyze your ratings and reviews data

To analyze the ratings and reviews data from your customers, you have several options:
* You can use the [Reviews](../publish/reviews-report.md) report in Partner Center to see the ratings and reviews from your customers. You can also download this report to view it offline.
* You can use the [Get app ratings](get-app-ratings.md) and [Get app reviews](get-app-reviews.md) methods in the Store analytics API to programmatically retrieve the ratings and reviews from your customers in JSON format.

## Related topics

* [Send requests to the Store](send-requests-to-the-store.md)
* [Launch the Microsoft Store app](../launch-resume/launch-store-app.md)
* [Reviews report](../publish/reviews-report.md)