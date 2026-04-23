---
title: Credential locker for Windows apps
description: This article describes how Windows apps can use the Credential Locker to securely store and retrieve user credentials.
ms.date: 08/05/2024
ms.topic: how-to
#customer intent: As a Windows developer, I want to learn how I can integrate the Credential Locker APIs into my native apps to store and retrieve user credentials.
---

# Credential locker for Windows apps

This article describes how Windows apps can use the Credential Locker to securely store and retrieve user credentials, and roam them between devices with the user's Microsoft account.

The Windows Runtime (WinRT) APIs for Credential Locker access are part of the [Windows Software Development Kit (SDK)](https://developer.microsoft.com/windows/downloads/windows-sdk/). These APIs were created for use in Universal Windows Platform (UWP) apps, but they can also be used in WinUI apps or in packaged desktop apps, including WPF and Windows Forms. For more information about using WinRT APIs in your Windows desktop app, see [Call Windows Runtime APIs in desktop apps](/windows/apps/desktop/modernize/desktop-to-uwp-enhance).

## Overview of the sample scenario

For example, you have an app that connects to a service to access protected resources such as media files, or social networking. Your service requires login information for each user. You've built UI into your app that gets the username and password for the user, which is then used to log the user into the service. Using the Credential Locker API, you can store the username and password for your user and easily retrieve them and log the user in automatically the next time they open your app, regardless of what device they're on.

User credentials stored in the Credential Locker do *not* expire, are *not* affected by the [ApplicationData.RoamingStorageQuota](/uwp/api/windows.storage.applicationdata.roamingstoragequota), and will *not* be cleared out due to inactivity like traditional roaming data. However, you can only store up to 20 credentials per app in the Credential Locker.

Credential Locker works a little differently for domain accounts. If there are credentials stored with your Microsoft account, and you associate that account with a domain account (such as the account that you use at work), your credentials will roam to that domain account. However, any new credentials added when signed on with the domain account won’t roam. This ensures that private credentials for the domain aren't exposed outside of the domain.

## Storing user credentials

1. Obtain a reference to the Credential Locker using the [PasswordVault](/uwp/api/Windows.Security.Credentials.PasswordVault) object from the [Windows.Security.Credentials](/uwp/api/Windows.Security.Credentials) namespace.
1. Create a [PasswordCredential](/uwp/api/Windows.Security.Credentials.PasswordCredential) object that contains an identifier for your app, the username and the password, and pass that to the [PasswordVault.Add](/uwp/api/windows.security.credentials.passwordvault.add) method to add the credential to the locker.

```cs
var vault = new Windows.Security.Credentials.PasswordVault();
vault.Add(new Windows.Security.Credentials.PasswordCredential(
    "My App", username, password));
```

## Retrieving user credentials

You have several options for retrieving user credentials from the Credential Locker after you have a reference to the [PasswordVault](/uwp/api/Windows.Security.Credentials.PasswordVault) object.

- You can retrieve all the credentials the user has supplied for your app in the locker with the [PasswordVault.RetrieveAll](/uwp/api/windows.security.credentials.passwordvault.retrieveall) method.
- If you know the username for the stored credentials, you can retrieve all the credentials for that username with the [PasswordVault.FindAllByUserName](/uwp/api/windows.security.credentials.passwordvault.findallbyusername) method.
- If you know the resource name for the stored credentials, you can retrieve all the credentials for that resource name with the [PasswordVault.FindAllByResource](/uwp/api/windows.security.credentials.passwordvault.findallbyresource) method.
- Finally, if you know both the username and the resource name for a credential, you can retrieve just that credential with the [PasswordVault.Retrieve](/uwp/api/windows.security.credentials.passwordvault.retrieve) method.

Let’s look at an example where we have stored the resource name globally in an app and we log the user on automatically if we find a credential for them. If we find multiple credentials for the same user, we ask the user to select a default credential to use when logging on.

```cs
private string resourceName = "My App";
private string defaultUserName;

private void Login()
{
    var loginCredential = GetCredentialFromLocker();

    if (loginCredential != null)
    {
        // There is a credential stored in the locker.
        // Populate the Password property of the credential
        // for automatic login.
        loginCredential.RetrievePassword();
    }
    else
    {
        // There is no credential stored in the locker.
        // Display UI to get user credentials.
        loginCredential = GetLoginCredentialUI();
    }

    // Log the user in.
    ServerLogin(loginCredential.UserName, loginCredential.Password);
}

private Windows.Security.Credentials.PasswordCredential GetCredentialFromLocker()
{
    Windows.Security.Credentials.PasswordCredential credential = null;

    var vault = new Windows.Security.Credentials.PasswordVault();

    IReadOnlyList<PasswordCredential> credentialList = null;

    try
    {
        credentialList = vault.FindAllByResource(resourceName);
    }
    catch(Exception)
    {
        return null;
    }

    if (credentialList.Count > 0)
    {
        if (credentialList.Count == 1)
        {
            credential = credentialList[0];
        }
        else
        {
            // When there are multiple usernames,
            // retrieve the default username. If one doesn't
            // exist, then display UI to have the user select
            // a default username.
            defaultUserName = GetDefaultUserNameUI();

            credential = vault.Retrieve(resourceName, defaultUserName);
        }
    }

    return credential;
}
```

## Deleting user credentials

Deleting user credentials in the Credential Locker is also a quick, two-step process.

1. Obtain a reference to the Credential Locker using the [PasswordVault](/uwp/api/Windows.Security.Credentials.PasswordVault) object from the [Windows.Security.Credentials](/uwp/api/Windows.Security.Credentials) namespace.
1. Pass the credential you want to delete to the [PasswordVault.Remove](/uwp/api/windows.security.credentials.passwordvault.remove) method.

```cs
var vault = new Windows.Security.Credentials.PasswordVault();
vault.Remove(new Windows.Security.Credentials.PasswordCredential(
    "My App", username, password));
```

## Best practices

Only use the credential locker for passwords and not for larger data blobs.

Save passwords in the credential locker only if the following criteria are met:

- The user has successfully signed in.
- The user has opted to save passwords.

Never store credentials in plain-text using app data or roaming settings.

## Related content

- [PasswordVault](/uwp/api/Windows.Security.Credentials.PasswordVault)
- [Call Windows Runtime APIs in desktop apps](/windows/apps/desktop/modernize/desktop-to-uwp-enhance)
