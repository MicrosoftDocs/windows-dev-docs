---
title: Windows Age APIs FAQ
description: Find answers about Windows Age APIs, including age groups, verified adult status, privacy, app requirements, and fallback handling.
author: GrantMeStrength
ms.author: jken
ms.topic: faq
ms.date: 11/03/2026
---

# Windows Age APIs FAQ

This article answers frequently asked questions about using Windows Age APIs to build age-appropriate app experiences.

## Overview

### What are the Windows Age APIs?

The Windows Age APIs provide privacy-preserving age signals for the user running the current process. Instead of returning an exact age or date of birth, Windows returns an age range or adult-verification status that an app can use to provide an age-appropriate experience.

These APIs are Windows Runtime methods on [`Windows.System.User`](/uwp/api/windows.system.user). They help apps make age-related experience and policy decisions without receiving the user's exact date of birth.

If a user consents, a developer can access age signals that applications can use when designing age-appropriate experiences. Application developers remain responsible for determining how applicable laws and requirements apply to their products.

### What scenarios are the Windows Age APIs intended for?

Apps can use age signals when age is relevant to the experience. For example, an app might use an age signal if it:

- Displays user-generated content, social features, or communications that might not be appropriate for all ages.
- Offers in-app purchases or virtual currencies for which age restrictions might apply.
- Streams or plays media with maturity ratings.

More generally, an app can use an age signal to adapt its content, features, or access controls without collecting a date of birth.

### Which Windows Age APIs are available?

The following APIs support different scenarios:

- [`GetUserAgeRangeAsync`](/uwp/api/windows.system.user.getuseragerangeasync) returns a [`UserAgeRange`](/uwp/api/windows.system.useragerange) containing the inclusive lower and upper bounds of the user's age group. The predefined groups are under 10, 10–12, 13–15, 16–17, and 18 or older. A `null` result means that the age range is unknown or unavailable.
- [`GetAgeVerificationStatusAsync`](/uwp/api/windows.system.user.getageverificationstatusasync) returns a [`UserAgeVerificationStatus`](/uwp/api/windows.system.userageverificationstatus) indicating the user's adult-verification status. It doesn't verify an arbitrary age or age group.

### Does age verification verify a user's age or age group?

No. Age verification provides **verified adult status**, not a verified status for any age or age group. For account-based verification, a user who claims to be an adult must go through a flow that checks whether that person is an adult. The flow checks the adult claim; it doesn't establish a verified age for a child or teenager.

For information about verifying your age for a Microsoft account, see [Verify your age for a Microsoft account](https://support.microsoft.com/en-us/accounts-billing/manage/verify-your-age-microsoft-account).

`GetAgeVerificationStatusAsync` reports the adult-verification status. A `Verified` result reports verified adult status. Other results don't establish that the user is a minor: your app must distinguish an `Unverified` result, an `OptedOut` result, and unavailable or indeterminate results.

### Are the Windows Age APIs generally available?

`GetUserAgeRangeAsync` and `GetAgeVerificationStatusAsync` are generally available. The supported retail Windows builds are:

- Windows 11, version 24H2: build 26100.9550 or later.
- Windows 11, version 25H2: build 26200.9550 or later.

Windows SDK version 10.0.26100.9169 or later includes these APIs.

### Which API should my app use?

Use:

- `GetUserAgeRangeAsync` when your app needs an age bracket and applies its own experience or policy logic.
- `GetAgeVerificationStatusAsync` when a decision depends on whether the user has verified adult status.

An app can use more than one age signal when appropriate for its scenario. An age-range result of 18 or older doesn't by itself establish verified adult status.

## Privacy and data handling

### Can I configure my own age ranges for GetUserAgeRangeAsync?

No. The age groups returned by `GetUserAgeRangeAsync` are predefined.

### Do the APIs return the user's exact age or date of birth?

No. `GetUserAgeRangeAsync` returns an age group, not an exact age or date of birth. For example, a result with lower and upper bounds of `{13, 15}` means that the user's age is in the 13–15 age group.

`GetAgeVerificationStatusAsync` reports adult-verification status, not a verified age or verified age group.

### Does my app need to collect a user's date of birth to use the Windows Age APIs?

No. One purpose of the APIs is to let apps make age-related decisions without directly receiving the user's date of birth.

### Should apps store an age signal?

Apps should avoid unnecessarily persisting age signals. Retrieve the signal when it's needed because a user's age group, verification status, account state, or applicable administrative policy can change.

## User and account behavior

### Which account types are supported?

Identity-provider age signals are currently available only for Microsoft accounts. For other account types, when an administrative default isn't configured, `GetUserAgeRangeAsync` returns `null`, and `GetAgeVerificationStatusAsync` returns `NotApplicable`.

### Which user does an Age API return information about?

Age API calls apply to the user running the current process. On a device with multiple Windows profiles, the APIs query the current user's profile for the active app experience. The APIs don't provide a mechanism for querying a different profile.

### Can an app query an age signal for another user on the device?

No. The APIs operate in the context of the user running the process. Calling an Age API with a [`User`](/uwp/api/windows.system.user) object that represents a different user can result in `E_ACCESSDENIED`.

### What happens when a user has multiple account types connected to Windows?

The Windows Age APIs operate in the context of the currently signed-in Windows user and use the Microsoft account associated with that user's Windows sign-in. Apps can't select another connected account for the query.

### What if an age signal can't be determined?

Apps must be prepared for an age signal to be unavailable or indeterminate.

For `GetUserAgeRangeAsync`, `null` means that the age range is unknown or unavailable. Conditions that can produce `null` include:

- The account type isn't supported.
- Age information isn't available for the account.
- The API is disabled by administrative policy.

For `GetAgeVerificationStatusAsync`, `NotApplicable` or `TemporarilyUnavailable` represents an unavailable or indeterminate verification signal. These results don't establish whether the user has verified adult status.

Apps must provide an appropriate fallback experience and must not interpret an unavailable result as a specific age group or verification status.

### Should apps assume that an age signal is always available?

No. Apps should design fallback behavior as part of their Age API integration. Treat a valid signal, an unavailable or indeterminate signal, and an access-denied error as distinct states.

## App requirements and implementation

### What operating system and SDK are required?

Windows 11 is the minimum supported operating system. `GetUserAgeRangeAsync` and `GetAgeVerificationStatusAsync` are available in the following supported retail builds:

- Windows 11, version 24H2: build 26100.9550 or later.
- Windows 11, version 25H2: build 26200.9550 or later.

Use Windows SDK version 10.0.26100.9169 or later to build an app that calls these APIs.

Because an API can be absent on an older or unserviced system, check for API availability before calling it.

### Does my app need to declare a capability?

Yes. A UWP app must declare the `userAccountInformation` capability in its package manifest:

```xml
<uap:Capability Name="userAccountInformation" />
```

Without the required access, the app can't read the user's age data.

### How do I obtain the User object used to call an Age API?

Use [`User.GetDefault`](/uwp/api/windows.system.user.getdefault) to obtain the user running the current process. [`User.FindAllAsync`](/uwp/api/windows.system.user.findallasync) can enumerate users, but an Age API must be called with the `User` instance that represents the user running the process.

### How long does it take for a birth-date change or birthday to affect an age signal?

If a user changes the birth date associated with their Microsoft account or has a birthday, the change is reflected within 24 hours after the last age-signal fetch.

### Can an app force an age-signal refresh?

No. A forced refresh isn't supported.

## Error and fallback handling

### How should an app handle an unavailable age range?

Handle an unavailable age range explicitly rather than assuming an age. A `null` result from `GetUserAgeRangeAsync` means that the age range is unknown or unavailable.

Use a deliberately designed fallback experience. Don't interpret `null` as a particular age group, including the 18-or-older group.

### What does E_ACCESSDENIED mean?

`E_ACCESSDENIED` can mean that the caller doesn't have access to Account info or that the app called the API with a `User` object for someone other than the user running the process.

Treat this error as distinct from an unavailable signal. An unavailable signal is returned as `null`, `NotApplicable`, or `TemporarilyUnavailable`, depending on the API.

### What should an app do with an unknown verification status?

Handle unavailable or indeterminate verification states explicitly and provide a fallback experience. `NotApplicable` and `TemporarilyUnavailable` don't establish whether the user has verified adult status. Don't interpret these results, `Unverified`, or `OptedOut` as proof that the user is a minor.

## See also

- [Age signals overview](index.md)
- [`User.GetUserAgeRangeAsync`](/uwp/api/windows.system.user.getuseragerangeasync)
- [`User.GetAgeVerificationStatusAsync`](/uwp/api/windows.system.user.getageverificationstatusasync)
