---
title: C++ API error handling
author: KevinAsgari
description: Learn how to handle errors when making an Xbox Live service call with the C++ APIs.
ms.assetid: 10b47e68-8b1f-4023-96a4-404f3f6a9850
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, error handling
ms.localizationpriority: low
---

# C++ API error handling

In the C++ API, rather than throwing exceptions, most calls will return xbox_live_result<payload_type> as appropriate.

## xbox_live_result structure
xbox_live_result has 3 items:
1. The error returned by the operation,
2. Specific error message used for debugging purposes and
3. The payload of the result (can be empty if there was an error)"

You can get more information on xbox_live_result as well as what the error codes are in Xbox Live documentation.

The structure is as follows:

```cpp
template<typename T>
class xbox_live_result
{
    const std::error_code& err();
    const std::string& err_message();
    T& payload();
};
```

**err** - Returns the error.  Will be a NULL reference with no error.  This behaves as the C++ STL error does in that you can get the primitive value by calling value().  Calling message() will get you a string representation.  So if the error code means "Invalid Argument", then ```err().message()``` would be the text "Invalid Argument".

**err_message** - Elaborates on the error.  For example if **err** is "Invalid Argument", then **err_message** would elaborate on which argument is invalid.

**payload** - Return the item of interest.  For example consider ```xbox_live_result<achievement>``` which you might get from calling get_achievement.  In this example, the payload would be the achievement itself (if no error is present).

## Example

```cpp
// Function which returns an xbox_live_result
xbox_live_result<std::shared_ptr<title_presence_change_subscription>> presenceChangeSubscriptionResult =
xbox::services::presence::subscribe_to_title_presence_change(
    xboxUserId,
    titleId
    );

printf("Error value %d, string %s", achievementResult.err().value(), achievementResult.err().message());

// Would output:
// "0 Success" if successful
// "401 Unauthorized" if auth issue

if (!achievementResult.err())
{
  // Do things on success.  Payload will be populated if applicable.
  std::shared_ptr<title_presence_change_subscription> presenceChangeSubscription = presenceChangeSubscriptionResult->payload();

  // ...
}
else if (achievement.err() == xboxlive_error_code::http_status_403_forbidden)
{
  // Special handling for 403 errors
}
else if (achievementResult.err() == xbox_live_error_condition::auth)
{
  // Handle broad auth failures.  See below section for more info on xbox_live_error_condition
}

```

## Using xbox_live_error_condition to test against broad error categories
In the above example, we test the error code against 403 errors, as well as something called ```xbox_live_error_condition::auth```.

 When using xbox_live_result err() function, one can test against error codes individually.  For example, for 400 class errors you could have individual testing and control flow for:

* xbox_live_error_code::http_status_400_bad_request
* xbox_live_error_code::http_status_401_unauthorized
* xbox_live_error_code::http_status_403_forbidden
* etc

But typically this is not what you want to do and you want to test against a class of errors as one.  So you can test against a class of errors using the enums available in the ```xbox_live_error_condition``` class.  We implement an overload for the equality operator which will automate testing against many error codes.  In addition to ```auth```, there are categories like ```rta``` and ```http```.  The full list can be found in *errors.h* or in *xblsdk_cpp.chm*.

For a video that covers this, and some other features of the C++ Xbox Service API, please check out our XFest talk in [Xfest 2015 Videos](https://developer.xboxlive.com/en-us/platform/documentlibrary/events/Pages/Xfest2015.aspx) under *XSAPI: C++, No Exceptions!*
