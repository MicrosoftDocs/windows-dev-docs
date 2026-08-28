---
title: Implement age signals in a Windows app
description: Learn how to use the Windows Age APIs to query age signals and adapt your app's content based on a user's age group and verified status.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 08/27/2026
---

# Implement age signals in a Windows app

This guide shows you how to use the Windows Age APIs to query age signals for your application on Windows. By the end of this article, you will be able to utilize the Age APIs to understand a user's age group and verified status, ensuring your application is providing safe, age-appropriate experiences to all users.

## Prerequisites

- Windows 11 or later
- Visual Studio 2022 or later with the **Desktop development with C++** workload
- A packaged app that declares the `userAccountInformation` capability
- A test account with Microsoft account sign-in

## Overview of the API surface

The Age APIs are Windows Runtime methods on `Windows.System.User`. The primary methods are:

| Method | Purpose |
| --- | --- |
| **GetUserAgeRangeAsync** | Returns the user's age range as a nullable `UserAgeRange` object with `Lower` and `Upper` properties. |
| **GetAgeVerificationStatusAsync** | Returns the user's `UserAgeVerificationStatus`. |

## Step 1: Include the C++/WinRT headers

Include the C++/WinRT headers for Windows Foundation and `Windows.System`.

```cpp
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.System.h>
#include <cstdio>

using namespace winrt;
using namespace winrt::Windows::System;
```

## Step 2: Query the age signals

Initialize Windows Runtime, get the current user, and call the asynchronous methods. Use `.get()` to obtain their results.

```cpp
int wmain()
{
    init_apartment();

    try
    {
        User user = User::GetDefault();

        UserAgeRange ageRange = user.GetUserAgeRangeAsync().get();
        if (ageRange)
        {
            int32_t lower = ageRange.Lower();
            int32_t upper = ageRange.Upper();

            // Adapt the experience using lower and upper.
        }
        else
        {
            // The age range is unknown or unavailable.
        }

        UserAgeVerificationStatus status =
            user.GetAgeVerificationStatusAsync().get();

        switch (status)
        {
        case UserAgeVerificationStatus::Verified:
            break;
        case UserAgeVerificationStatus::Unverified:
            break;
        case UserAgeVerificationStatus::OptedOut:
            break;
        case UserAgeVerificationStatus::TemporarilyUnavailable:
            break;
        case UserAgeVerificationStatus::NotApplicable:
            break;
        }
    }
    catch (hresult_error const& error)
    {
        wprintf(
            L"Call failed: hr=0x%08X %ls\n",
            static_cast<unsigned int>(error.code().value),
            error.message().c_str());
        return error.code().value;
    }

    return 0;
}
```

`GetUserAgeRangeAsync` returns `null` when the age range is unknown or unavailable. For the 18-or-older age range, `Upper` is `INT32_MAX`.

## API reference

> [!NOTE]
> Detailed API reference documentation will be available when the Windows Age APIs ship publicly. The interface and method names in this article are based on the current specification and may change before release.

- `GetUserAgeRangeAsync`: Returns a nullable `UserAgeRange` object with `Lower` and `Upper` properties.
- `GetAgeVerificationStatusAsync`: Returns the user's `UserAgeVerificationStatus`.
