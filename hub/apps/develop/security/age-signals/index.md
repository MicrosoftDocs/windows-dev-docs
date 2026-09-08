---
title: Age signals overview for Windows developers
description: Learn how Windows Age APIs help apps provide safe, age-appropriate experiences by querying a user's age group and age verified status.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 09/04/2026
---

# Age signals overview

The Windows Age APIs provide privacy-preserving signals that apps can use to understand the age group and age-verification status of the signed-in user. The APIs return coarse signals instead of the user's exact age or date of birth. Apps can use these signals to adapt content, features, or access controls and must provide appropriate fallback behavior when a signal is unavailable.

## What are age signals?

Age signals are values that Windows returns when an app calls the Age APIs for the current user. The app decides when to request the user's age group or age-verification status to inform the appropriate end-user experience.

An age signal does **not** directly expose the user's exact age or date of birth. Instead, it provides an age range that the app can use to make content decisions. The `GetUserAgeRangeAsync` API offers the following age groups:

- Under 10
- 10-12
- 13-15
- 16-17
- 18+


## Windows Age APIs

> [!NOTE]
> The Windows Age APIs are broadly available to Windows Insiders now, and will be available to all Windows users soon.

### GetUserAgeRangeAsync

Returns the user’s age range as a `UserAgeRange` object with `Lower` and `Upper` properties.

| Age group | Return values |
| --- | --- |
| **Under 10** | {0, 9} |
| **10-12** | {10, 12} |
| **13-15** | {13, 15} |
| **16-17** | {16, 17} |
| **18+** | {18, `INT32_MAX`} |
| **Unknown** | `null` |

When the result is `null`, the age range is unknown or unavailable. Apps must use their fallback experience and must not interpret `null` as a specific age group.

### GetAgeVerificationStatusAsync

Returns a `UserAgeVerificationStatus` value indicating the age-verification state reported for the user. Possible values are:

- `Verified`: The user's age has been verified.
- `Unverified`: The user's age has not been verified.
- `OptedOut`: The user has opted out of age verification.
- `TemporarilyUnavailable`: The status cannot currently be determined.
- `NotApplicable`: Verification does not apply, no verification signal is available, or the feature is unavailable or disabled.

Apps must handle every value. `NotApplicable` and `TemporarilyUnavailable` do not establish that the user is either verified or unverified.

### Caller authorization and consent

The app package must declare the `userAccountInformation` capability. Access to age signals is also subject to the user's consent to let the app access account information in Windows privacy settings. If access isn't granted, the call can fail with `E_ACCESSDENIED`. The `User` object must represent the user running the current process.

### Policy and availability

When the Age APIs are unavailable or disabled, `GetUserAgeRangeAsync` returns `null` and `GetAgeVerificationStatusAsync` returns `NotApplicable`. Administrators can also configure a default age group or verification status through Group Policy or MDM; when configured, the APIs return the corresponding policy-defined value. Apps must check that the methods are available before calling them and retain fallback behavior for unavailable signals.

Identity-provider age signals are currently retrieved only for Microsoft accounts. For other account types, when no administrative default is configured, `GetUserAgeRangeAsync` returns `null` and `GetAgeVerificationStatusAsync` returns `NotApplicable`.

## When to use age signals

Use age signals when your app:

- Displays user-generated content, social features, or communication tools that may not be appropriate for all ages.
- Offers in-app purchases or virtual currencies where age restrictions are relevant.
- Streams or plays media content with maturity ratings.

## How age signals work at a high level

:::image type="content" source="images/age-process-diagram.png" alt-text="Diagram showing an app exchanging age-group information with the Digital Safety API local service, which communicates with configuration settings.":::

## Privacy and data handling

Age signals are designed with privacy as a core principle:

- **No exact age or date of birth is returned.** The APIs return an age bucket and an age-verification status.
- **Age groups are generic.** They communicate common age groups for various use cases such as gaming or regional law.

## Platform requirements

| Requirement | Details |
| --- | --- |
| **Minimum OS version** | Windows 11 |
| **API surface** | Windows Runtime methods on `Windows.System.User` |
| **User context** | Calls apply only to the user running the current process. |
| **App capability** | The app package must declare `userAccountInformation` |

## See also

- [Microsoft Family Safety](https://www.microsoft.com/microsoft-365/family-safety)
- [Security and identity development for Windows apps](../index.md)
