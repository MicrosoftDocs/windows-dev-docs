---
description: You can use the SendRequestAsync method to send requests to the Microsoft Store for operations that do not yet have an API available in the Windows SDK.
title: Send requests to the Microsoft Store
ms.assetid: 070B9CA4-6D70-4116-9B18-FBF246716EF0
ms.date: 07/16/2024
ms.topic: article
keywords: windows 10, uwp, StoreRequestHelper, SendRequestAsync
ms.localizationpriority: medium
---
# Send requests to the Microsoft Store

Starting in Windows 10, version 1607, the Windows SDK provides APIs for Store-related operations (such as in-app purchases) in the [Windows.Services.Store](/uwp/api/windows.services.store) namespace. However, although the services that support the Store are constantly being updated, expanded, and improved between OS releases, new APIs are typically added to the Windows SDK only during major OS releases.

We provide the [SendRequestAsync](/uwp/api/windows.services.store.storerequesthelper.sendrequestasync) method as a flexible way to make new Store operations available to Universal Windows Platform (UWP) apps before a new version of the Windows SDK is released. You can use this method to send requests to the Store for new operations that do not yet have a corresponding API available in the latest release of the Windows SDK.

> [!NOTE]
> The **SendRequestAsync** method is available only to apps that target Windows 10, version 1607, or later. Some of the requests supported by this method are only supported in releases after Windows 10, version 1607.

**SendRequestAsync** is a static method of the [StoreRequestHelper](/uwp/api/windows.services.store.storerequesthelper) class. To call this method, you must pass the following information to the method:
* A [StoreContext](/uwp/api/windows.services.store.storecontext) object that provides information about the user for which you want to perform the operation. For more information about this object, see [Get started with the StoreContext class](in-app-purchases-and-trials.md#get-started-with-the-storecontext-class).
* An integer that identifies the request that you want to send to the Store.
* If the request supports any arguments, you can also pass a JSON-formatted string that contains the arguments to pass along with the request.

The following example demonstrates how to call this method. This example requires using statements for the **Windows.Services.Store** and **System.Threading.Tasks** namespaces.

```csharp
public async Task<bool> AddUserToFlightGroup()
{
    StoreSendRequestResult result = await StoreRequestHelper.SendRequestAsync(
        StoreContext.GetDefault(), 8,
        "{ \"type\": \"AddToFlightGroup\", \"parameters\": { \"flightGroupId\": \"your group ID\" } }");

    if (result.ExtendedError == null)
    {
        return true;
    }

    return false;
}
```

See the following sections for information about the requests that are currently available via the **SendRequestAsync** method. We will update this article when support for new requests are added.

## Request for in-app ratings and reviews

You can programmatically launch a dialog from your app that asks your customer to rate your app and submit a review by passing the request integer 16 to the **SendRequestAsync** method. For more information, see [Show a rating and review dialog in your app](request-ratings-and-reviews.md#show-a-rating-and-review-dialog-in-your-app).

## Requests for flight group scenarios

> [!IMPORTANT]
> All of the flight group requests described in this section are currently not available to most developer accounts. These requests will fail unless your developer account is specially provisioned by Microsoft.

The **SendRequestAsync** method supports a set of requests for flight group scenarios, such as adding a user or device to a flight group. To submit these requests, pass the value 7 or 8 to the *requestKind* parameter along with a JSON-formatted string to the *parametersAsJson* parameter that indicates the request you want to submit along with any related arguments. The method also supports requests to map a PFN with a BigID (ProductID from Display Catalog). To submit this request, pass the value 9 to the requestKind parameter. These *requestKind* values differ in the following ways.

|  Request kind value  |  Description  |
|----------------------|---------------|
|  7                   |  The requests are performed in the context of the current device. This value can only be used on Windows 10, version 1703, or later.  |
|  8                   |  The requests are performed in the context of the user who is currently signed in to the Store. This value can be used on Windows 10, version 1607, or later.  |
|  9                   |  The requests return the BigID (ProductId) by PackageFamilyName via an HTTP request to the Display Catalog with anonymous authentication. |

The following flight group requests are currently implemented.

### Retrieve remote variables for the highest-ranked flight group

> [!IMPORTANT]
> This request is currently not available to most developer accounts. This request will fail unless your developer account is specially provisioned by Microsoft.

This request retrieves the remote variables for the highest-ranked flight group for the current user or device. To send this request, pass the following information to the *requestKind* and *parametersAsJson* parameters of the **SendRequestAsync** method.

|  Parameter  |  Description  |
|----------------------|---------------|
|  *requestKind*                   |  Specify 7 to return the highest-ranked flight group for the device, or specify 8 to return the highest-ranked flight group for the current user and device. We recommend using the value 8 for the *requestKind* parameter, because this value will return the highest-ranked flight group across the membership for both the current user and device.  |
|  *parametersAsJson*                   |  Pass a JSON-formatted string that contains the data shown in the example below.  |

The following example shows the format of the JSON data to pass to *parametersAsJson*. The *type* field must be assigned to the string *GetRemoteVariables*. Assign the *projectId* field to the ID of the project in which you defined the remote variables in Partner Center.

```json
{ 
    "type": "GetRemoteVariables", 
    "parameters": "{ \"projectId\": \"your project ID\" }" 
}
```

After you submit this request, the [Response](/uwp/api/windows.services.store.storesendrequestresult.Response) property of the [StoreSendRequestResult](/uwp/api/windows.services.store.storesendrequestresult) return value contains a JSON-formatted string with the following fields.

|  Field  |  Description  |
|----------------------|---------------|
|  *anonymous*                   |  A Boolean value, where **true** indicates that the user or device identity was not present in the request, and **false** indicates that user or device identity was present in the request.  |
|  *name*                   |  A string that contains the name of the highest-ranked flight group to which the device or user belongs.  |
|  *settings*                   |  A dictionary of key/value pairs that contain the name and value of the remote variables that the developer has configured for the flight group.  |

The following example demonstrates a return value for this request.

```json
{ 
  "anonymous": false, 
  "name": "Insider Slow",
  "settings":
  {
      "Audience": "Slow"
      ...
  }
}
```

### Add the current device or user to a flight group

> [!IMPORTANT]
> This request is currently not available to most developer accounts. This request will fail unless your developer account is specially provisioned by Microsoft.

To send this request, pass the following information to the *requestKind* and *parametersAsJson* parameters of the **SendRequestAsync** method.

|  Parameter  |  Description  |
|----------------------|---------------|
|  *requestKind*                   |  Specify 7 to add the device to a flight group, or specify 8 to add the user who is currently signed in to the Store to a flight group.  |
|  *parametersAsJson*                   |  Pass a JSON-formatted string that contains the data shown in the example below.  |

The following example shows the format of the JSON data to pass to *parametersAsJson*. The *type* field must be assigned to the string *AddToFlightGroup*. Assign the *flightGroupId* field to the ID of the flight group to which you want to add the device or user.

```json
{ 
    "type": "AddToFlightGroup", 
    "parameters": "{ \"flightGroupId\": \"your group ID\" }" 
}
```

If there is an error with the request, the [HttpStatusCode](/uwp/api/windows.services.store.storesendrequestresult.HttpStatusCode) property of the [StoreSendRequestResult](/uwp/api/windows.services.store.storesendrequestresult) return value contains the response code.

### Remove the current device or user from a flight group

> [!IMPORTANT]
> This request is currently not available to most developer accounts. This request will fail unless your developer account is specially provisioned by Microsoft.

To send this request, pass the following information to the *requestKind* and *parametersAsJson* parameters of the **SendRequestAsync** method.

|  Parameter  |  Description  |
|----------------------|---------------|
|  *requestKind*                   |  Specify 7 to remove the device from a flight group, or specify 8 to remove the user who is currently signed in to the Store from a flight group.  |
|  *parametersAsJson*                   |  Pass a JSON-formatted string that contains the data shown in the example below.  |

The following example shows the format of the JSON data to pass to *parametersAsJson*. The *type* field must be assigned to the string *RemoveFromFlightGroup*. Assign the *flightGroupId* field to the ID of the flight group from which you want to remove the device or user.

```json
{ 
    "type": "RemoveFromFlightGroup", 
    "parameters": "{ \"flightGroupId\": \"your group ID\" }" 
}
```

If there is an error with the request, the [HttpStatusCode](/uwp/api/windows.services.store.storesendrequestresult.HttpStatusCode) property of the [StoreSendRequestResult](/uwp/api/windows.services.store.storesendrequestresult) return value contains the response code.

## Related topics

* [Show a rating and review dialog in your app](request-ratings-and-reviews.md#show-a-rating-and-review-dialog-in-your-app)
* [SendRequestAsync](/uwp/api/windows.services.store.storerequesthelper.sendrequestasync)
