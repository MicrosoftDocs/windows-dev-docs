---
title: Authentication and user identity
description: Universal Windows Platform (UWP) apps have several options for user authentication, ranging from simple single sign-on (SSO) using Web authentication broker to highly secure two-factor authentication.
ms.date: 07/08/2024
ms.topic: article
ms.localizationpriority: medium
---

# Authentication and user identity

Universal Windows Platform (UWP) apps have several options for user authentication, ranging from simple single sign-on (SSO) using [Web authentication broker](web-authentication-broker.md) to highly secure two-factor authentication.

For regular app connections to third-party identity provider services, use the [Web authentication broker](web-authentication-broker.md). For added convenience, use [Credential Locker](credential-locker.md) to save and roam the user's login information.

Enterprises using Windows should strongly consider using [Windows Hello](/windows/apps/develop/security/windows-hello), which enables highly secure two-factor authentication. If using Windows Hello is not possible, [Smart cards](smart-cards.md) and [Fingerprint biometrics](fingerprint-biometrics.md) can add an additional layer of security.

| Topic | Description |
|-------|-------------|
| [Credential locker](credential-locker.md) | This article describes how apps can use the Credential Locker to securely store and retrieve user credentials, and roam them between devices with the user's Microsoft account. |
| [Fingerprint biometrics](fingerprint-biometrics.md) | This article explains how to add fingerprint biometrics to your app. Including a request for fingerprint authentication when the user must consent to a particular action increases the security of your app. For example, you could require fingerprint authentication before authorizing an in-app purchase, or access to restricted resources. Fingerprint authentication is managed using the [UserConsentVerifier](/uwp/api/Windows.Security.Credentials.UI.UserConsentVerifier) class in the [Windows.Security.Credentials.UI](/uwp/api/Windows.Security.Credentials.UI) namespace. |
| [Windows Hello](/windows/apps/develop/security/windows-hello) | This article describes the Windows Hello technology, and discusses how developers can implement this technology to protect their apps and backend services. It highlights specific capabilities of these technologies that help mitigate threats from conventional credentials and provides guidance about designing and deploying these technologies as part of your packaged Windows apps. |
| [Create a Windows Hello login app](/windows/apps/develop/security/windows-hello-login) | Part 1 of a complete walkthrough on how to create a packaged Windows app that uses Windows Hello as an alternative to traditional username and password authentication systems. |
| [Create a Windows Hello login service](/windows/apps/develop/security/windows-hello-auth-service) | Part 2 of a complete walkthrough on how to use Windows Hello as an alternative to traditional username and password authentication systems in packaged Windows apps. |
| [Smart cards](smart-cards.md) | This topic explains how apps can use smart cards to connect users to secure network services, including how to access physical smart card readers, create virtual smart cards, communicate with smart cards, authenticate users, reset user PINs, and remove or disconnect smart cards. |
| [Share certificates between apps](share-certificates.md) | UWP apps that require secure authentication beyond a user Id and password combination can use certificates for authentication. Certificate authentication provides a high level of trust when authenticating a user. In some cases, a group of services will want to authenticate a user for multiple apps. This article shows how you can authenticate multiple apps using the same certificate, and how you can provide convenient code for a user to import a certificate that was provided to access secured web services. |
| [Windows Unlock with companion IoT devices](companion-device-unlock.md) | A companion device is a device that can act in conjunction with Windows to enhance the user authentication experience. Using the Companion Device Framework, a companion device can provide a rich experience even when Windows Hello is not available (for example, if the Windows machine lacks a camera for face authentication or fingerprint reader device). |
| [Web account manager](web-account-manager.md) | This article describes how to show the AccountsSettingsPane and connect your Universal Windows Platform (UWP) app to external identity providers, like Microsoft or Facebook, using the new Windows 10/11 Web Account Manager APIs. You'll learn how to request a user's permission to use their Microsoft account, obtain an access token, and use it to perform basic operations (like get profile data or upload files to their OneDrive). |
| [Web authentication broker](web-authentication-broker.md) | This article explains how to connect your app to an online identity provider that uses authentication protocols like OpenID or OAuth. The [AuthenticateAsync](/uwp/api/windows.security.authentication.web.webauthenticationbroker.authenticateasync) method sends a request to the online identity provider and gets back an access token that describes the provider resources to which the app has access. |
