---
title: Fingerprint biometrics
description: This article explains how to add fingerprint biometrics to your packaged Windows app using WinRT APIs from the Windows SDK.
ms.date: 08/05/2024
ms.topic: how-to
#customer intent: As a Windows developer, I want to add fingerprint biometrics to my Windows apps.
---

# Fingerprint biometrics

This article explains how to add fingerprint biometrics to your Windows app, including a request for fingerprint authentication when the user must consent to a particular action increases the security of your app. For example, you could require fingerprint authentication before authorizing an in-app purchase, or access to restricted resources. Fingerprint authentication is managed using the [UserConsentVerifier](/uwp/api/Windows.Security.Credentials.UI.UserConsentVerifier) class in the [Windows.Security.Credentials.UI](/uwp/api/Windows.Security.Credentials.UI) namespace.

The Windows Runtime (WinRT) APIs for fingerprint biometrics are part of the [Windows Software Development Kit (SDK)](https://developer.microsoft.com/windows/downloads/windows-sdk/). These APIs were created for use in Universal Windows Platform (UWP) apps, but they can also be used in WinUI apps or in packaged desktop apps, including WPF and Windows Forms. For more information about using WinRT APIs in your Windows desktop app, see [Call Windows Runtime APIs in desktop apps](/windows/apps/desktop/modernize/desktop-to-uwp-enhance).

## Check the device for a fingerprint reader

To find out whether the device has a fingerprint reader, call [UserConsentVerifier.CheckAvailabilityAsync](/uwp/api/windows.security.credentials.ui.userconsentverifier.checkavailabilityasync). Even if a device supports fingerprint authentication, your app should still provide users with an option in Settings to enable or disable it.

```cs
public async System.Threading.Tasks.Task<string> CheckFingerprintAvailability()
{
    string returnMessage = "";

    try
    {
        // Check the availability of fingerprint authentication.
        var ucvAvailability = await Windows.Security.Credentials.UI.UserConsentVerifier.CheckAvailabilityAsync();

        switch (ucvAvailability)
        {
            case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.Available:
                returnMessage = "Fingerprint verification is available.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.DeviceBusy:
                returnMessage = "Biometric device is busy.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.DeviceNotPresent:
                returnMessage = "No biometric device found.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.DisabledByPolicy:
                returnMessage = "Biometric verification is disabled by policy.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerifierAvailability.NotConfiguredForUser:
                returnMessage = "The user has no fingerprints registered. Please add a fingerprint to the " +
                                "fingerprint database and try again.";
                break;
            default:
                returnMessage = "Fingerprints verification is currently unavailable.";
                break;
        }
    }
    catch (Exception ex)
    {
        returnMessage = $"Fingerprint authentication availability check failed: {ex.ToString()}";
    }

    return returnMessage;
}
```

## Request consent and return results

1. To request user consent from a fingerprint scan, call the [UserConsentVerifier.RequestVerificationAsync](/uwp/api/windows.security.credentials.ui.userconsentverifier.requestverificationasync) method. For fingerprint authentication to work, the user must have previously added a fingerprint "signature" to the fingerprint database.
1. When you call the [UserConsentVerifier.RequestVerificationAsync](/uwp/api/windows.security.credentials.ui.userconsentverifier.requestverificationasync), the user is presented with a modal dialog requesting a fingerprint scan. You can supply a message to the **UserConsentVerifier.RequestVerificationAsync** method that will be displayed to the user as part of the modal dialog, as shown in the following image.

```cs
private async System.Threading.Tasks.Task<string> RequestConsent(string userMessage)
{
    string returnMessage;

    if (String.IsNullOrEmpty(userMessage))
    {
        userMessage = "Please provide fingerprint verification.";
    }

    try
    {
        // Request the logged on user's consent via fingerprint swipe.
        var consentResult = await Windows.Security.Credentials.UI.UserConsentVerifier.RequestVerificationAsync(userMessage);

        switch (consentResult)
        {
            case Windows.Security.Credentials.UI.UserConsentVerificationResult.Verified:
                returnMessage = "Fingerprint verified.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerificationResult.DeviceBusy:
                returnMessage = "Biometric device is busy.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerificationResult.DeviceNotPresent:
                returnMessage = "No biometric device found.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerificationResult.DisabledByPolicy:
                returnMessage = "Biometric verification is disabled by policy.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerificationResult.NotConfiguredForUser:
                returnMessage = "The user has no fingerprints registered. Please add a fingerprint to the " +
                                "fingerprint database and try again.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerificationResult.RetriesExhausted:
                returnMessage = "There have been too many failed attempts. Fingerprint authentication canceled.";
                break;
            case Windows.Security.Credentials.UI.UserConsentVerificationResult.Canceled:
                returnMessage = "Fingerprint authentication canceled.";
                break;
            default:
                returnMessage = "Fingerprint authentication is currently unavailable.";
                break;
        }
    }
    catch (Exception ex)
    {
        returnMessage = $"Fingerprint authentication failed: {ex.ToString()}";
    }

    return returnMessage;
}
```

## Related content

- [UserConsentVerifier](/uwp/api/Windows.Security.Credentials.UI.UserConsentVerifier)
- [Windows.Security.Credentials.UI namespace](/uwp/api/Windows.Security.Credentials.UI)
- [Call Windows Runtime APIs in desktop apps](/windows/apps/desktop/modernize/desktop-to-uwp-enhance)
