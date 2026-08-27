---
title: Age signals overview for Windows developers
description: Learn how Windows Age APIs help apps provide safe, age-appropriate experiences by querying a user's age group and age verified status.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 08/20/2026
---

# Digital safety age signals overview

The Windows Age APIs provide a privacy-preserving mechanism for apps to determine the age group and age verified status of the signed-in user. These APIs are designed for apps that need to comply with child safety regulations without requiring the app to collect, store, or process date of birth or other personally identifiable information. Apps use these signals to adapt content, features, or access controls based on a user's age and verified status — without needing to implement their own age verification system.

## What are age signals?

Age signals are values that the Windows provides to applications when a user takes an action to access age-related or restricted content. The caller application makes a request to access the user's age group and/or age verified status to inform the appropriate end-user experience.

An age signal does **not** directly expose the user's age or date of birth. Instead, it provides an age range that the app can use to make content decisions. The _**GetUserAgeRangeAsync**_ API offers the following age groups:

- Under 10
- 10-12
- 13-15
- 16-17
- 18+

## Digital Safety APIs

### GetUserAgeRangeAsync
Returns the user’s age range as a {AgeLower, AgeUpper} struct.

| Age group | Return values |
|-------|---------|
| **Under 10** | {0, 9}|
| **10-12**| {10, 12}|
| **13-15**| {13, 15} |
| **16-17**| {16,17} |
| **18+**| {18, -1} |
| **Unknown** |{-1, -1} |

When the result is **Unknown**, apps should fall back to their default behavior or age-gate mechanism.

### GetAgeVerificationStatusAsync
Returns the user's age verification status to indicate whether the user's age has been independently verified by the identity provider. This API is available to both 1P and 3P callers. Possible outputs include:

- Verified
- Unverified
- OptedOut
- TemporarilyUnavailable
- NotApplicable 

### Caller identity

The Digital Safety platform validates the calling app's identity before returning signal values. This prevents unauthorized apps from querying another app's safety settings. Your app must be properly registered and identified to receive meaningful signal data.

## When to use age signals

Use age signals when your app:

- Displays user-generated content, social features, or communication tools that may not be appropriate for all ages.
- Offers in-app purchases or virtual currencies where age restrictions are relevant.
- Streams or plays media content with maturity ratings.

## How age signals work at a high level

:::image type="content" source="images/age-process-diagram.png" alt-text="Diagram showing the flow between Your app, the Digital Safety API local service, and Configuration.":::

## Privacy and data handling

Age signals are designed with privacy as a core principle:

- **No personal data is exposed.** The API does not return the user's age, date of birth, or family relationship details.
- **Age groups are generic.** They communicate common age groups for various use cases such as gaming or regional law.

## Platform requirements

| Requirement | Details |
|-------------|---------|
| **Minimum OS version** | Windows 11 |
| **API surface** | Win32 COM (C/C++) |
| **User context** | Must run in a user session with a Microsoft account signed in |
| **App registration** | App identity must be established with the Digital Safety platform |

## See also

- [Microsoft Family Safety](https://www.microsoft.com/microsoft-365/family-safety)
- [Security and identity development for Windows apps](../index.md)
