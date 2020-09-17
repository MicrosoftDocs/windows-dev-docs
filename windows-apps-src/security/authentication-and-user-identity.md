---
title: Authentication and user identity
description: Universal Windows Platform (UWP) apps have several options for user authentication, ranging from simple single sign-on (SSO) using Web authentication broker to highly secure two-factor authentication.
ms.assetid: 53E36DDC-200A-4850-AAF0-07ECA3662BB9
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---
# Authentication and user identity



Universal Windows Platform (UWP) apps have several options for user authentication, ranging from simple single sign-on (SSO) using [Web authentication broker](web-authentication-broker.md) to highly secure two-factor authentication.

For regular app connections to third-party identity provider services, such as Facebook, Twitter, Flickr, and so on, use the [Web authentication broker](web-authentication-broker.md). For added convenience, use [Credential Locker](credential-locker.md) to save and roam the user's login information.

Enterprises using Windows 10 should strongly consider using [Microsoft Passport and Windows Hello](microsoft-passport.md), which enables highly secure two-factor authentication. If using Microsoft Passport is not possible, [Smart cards](smart-cards.md) and [Fingerprint biometrics](fingerprint-biometrics.md) can add an additional layer of security.

<table>
<tr><th>Topic</th><th>Description</th></tr>
<tr><td><a href="credential-locker.md">Credential locker</a></td><td>This article describes how apps can use the Credential Locker to securely store and retrieve user credentials, and roam them between devices with the user's Microsoft account</td></tr>

<tr><td><a href="fingerprint-biometrics.md">Fingerprint biometrics</a> </td><td>This article explains how to add fingerprint biometrics to your app. Including a request for fingerprint authentication when the user must consent to a particular action increases the security of your app. For example, you could require fingerprint authentication before authorizing an in-app purchase, or access to restricted resources. Fingerprint authentication is managed using the <a href="/uwp/api/Windows.Security.Credentials.UI.UserConsentVerifier">UserConsentVerifier</a> class in the <a href="https://docs.microsoft.com/uwp/api/Windows.Security.Credentials.UI">Windows.Security.Credentials.UI</a> namespace.</td></tr>
<tr><td><a href="microsoft-passport.md">Microsoft Passport and Windows Hello</a></td><td>This article describes the new Windows 10 Microsoft Passport technology, and discusses how developers can implement this technology to protect their apps and backend services. It highlights specific capabilities of these technologies that help mitigate threats from conventional credentials and provides guidance about designing and deploying these technologies as part of your Windows 10 rollout. </td></tr>
<tr><td><a href="microsoft-passport-login.md">Create a Microsoft Passport login app</a></td><td>Part 1 of a complete walkthrough on how to create a Windows 10 UWP (Universal Windows Platform) app that uses Microsoft Passport as an alternative to traditional username and password authentication systems.</td></tr>
<tr><td><a href="microsoft-passport-login-auth-service.md">Create a Microsoft Passport login service</a></td><td>Part 2 of a complete walkthrough on how to use Microsoft Passport as an alternative to traditional username and password authentication systems in Windows 10 UWP (Universal Windows platform) apps.</td></tr>
<tr><td><a href="smart-cards.md">Smart cards</a></td><td>This topic explains how apps can use smart cards to connect users to secure network services, including how to access physical smart card readers, create virtual smart cards, communicate with smart cards, authenticate users, reset user PINs, and remove or disconnect smart cards.</td></tr>
<tr><td><a href="share-certificates.md">Share certificates between apps</a></td><td>UWP apps that require secure authentication beyond a user Id and password combination can use certificates for authentication. Certificate authentication provides a high level of trust when authenticating a user. In some cases, a group of services will want to authenticate a user for multiple apps. This article shows how you can authenticate multiple apps using the same certificate, and how you can provide convenient code for a user to import a certificate that was provided to access secured web services.</td></tr>
<tr><td><a href="companion-device-unlock.md">Windows Unlock with companion IoT devices</a></td><td>A companion device is a device that can act in conjunction with your Windows 10 desktop to enhance the user authentication experience. Using the Companion Device Framework, a companion device can provide a rich experience for Microsoft Passport even when Windows Hello is not available (for example, if the Windows 10 desktop lacks a camera for face authentication or fingerprint reader device, for example).</td></tr>
<tr><td><a href="web-account-manager.md">Web account manager</a></td><td>This article describes how to show the AccountsSettingsPane and connect your Universal Windows Platform (UWP) app to external identity providers, like Microsoft or Facebook, using the new Windows 10 Web Account Manager APIs. You'll learn how to request a user's permission to use their Microsoft account, obtain an access token, and use it to perform basic operations (like get profile data or upload files to their OneDrive). </td></tr>
<tr><td><a href="web-authentication-broker.md">Web authentication broker</a></td><td>This article explains how to connect your app to an online identity provider that uses authentication protocols like OpenID or OAuth, such as Facebook, Twitter, Flickr, Instagram, and so on. The <a href="/uwp/api/windows.security.authentication.web.webauthenticationbroker.authenticateasync">AuthenticateAsync</a> method sends a request to the online identity provider and gets back an access token that describes the provider resources to which the app has access.</td></tr>
</table>