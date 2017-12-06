---
title: Security
description: This section contains articles on building secure Universal Windows Platform (UWP) apps for Windows 10.
ms.assetid: 41E2EEFB-E8A9-4592-814C-72B703CD952C
author: awkoren
ms.author: alkoren
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Security



This section contains articles on building secure Universal Windows Platform (UWP) apps for Windows 10.

## Introduction 

If you're new to Windows or UWP development, start with the [Intro to secure Windows app development](intro-to-secure-windows-app-development.md). This introductory-level article provides an overview of security considerations for apps and the various features available in Windows 10.

## Authentication and user identity

The [authentication and user identity section](authentication-and-user-identity.md) contains walkthroughs for scenarios related to user login and identity. Apps have several options for user authentication, ranging from simple single sign-on (SSO) using [Web authentication broker](web-authentication-broker.md) to highly secure two-factor authentication.

<table>
<tr><th>Topic</th><th>Description</th></tr>
<tr><td>[Credential locker](credential-locker.md)</td><td>This article describes how apps can use the Credential Locker to securely store and retrieve user credentials, and roam them between devices with the user's Microsoft account</td></tr>

<tr><td>[Fingerprint biometrics](fingerprint-biometrics.md) </td><td>This article explains how to add fingerprint biometrics to your app. Including a request for fingerprint authentication when the user must consent to a particular action increases the security of your app. For example, you could require fingerprint authentication before authorizing an in-app purchase, or access to restricted resources. Fingerprint authentication is managed using the [UserConsentVerifier](https://msdn.microsoft.com/library/windows/apps/dn279134) class in the [Windows.Security.Credentials.UI](https://msdn.microsoft.com/library/windows/apps/hh701356) namespace.</td></tr>
<tr><td>[Microsoft Passport and Windows Hello](microsoft-passport.md)</td><td>This article describes the new Windows 10 Microsoft Passport technology, and discusses how developers can implement this technology to protect their apps and backend services. It highlights specific capabilities of these technologies that help mitigate threats from conventional credentials and provides guidance about designing and deploying these technologies as part of your Windows 10 rollout. </td></tr>
<tr><td>[Create a Microsoft Passport login app](microsoft-passport-login.md)</td><td>Part 1 of a complete walkthrough on how to create a Windows 10 UWP (Universal Windows Platform) app that uses Microsoft Passport as an alternative to traditional username and password authentication systems.</td></tr>
<tr><td>[Create a Microsoft Passport login service](microsoft-passport-login-auth-service.md)</td><td>Part 2 of a complete walkthrough on how to use Microsoft Passport as an alternative to traditional username and password authentication systems in Windows 10 UWP (Universal Windows platform) apps.</td></tr>
<tr><td>[Smart cards](smart-cards.md)</td><td>This topic explains how apps can use smart cards to connect users to secure network services, including how to access physical smart card readers, create virtual smart cards, communicate with smart cards, authenticate users, reset user PINs, and remove or disconnect smart cards.</td></tr>
<tr><td>[Share certificates between apps](share-certificates.md)</td><td>UWP apps that require secure authentication beyond a user Id and password combination can use certificates for authentication. Certificate authentication provides a high level of trust when authenticating a user. In some cases, a group of services will want to authenticate a user for multiple apps. This article shows how you can authenticate multiple apps using the same certificate, and how you can provide convenient code for a user to import a certificate that was provided to access secured web services.</td></tr>
<tr><td>[Windows Unlock with companion IoT devices](companion-device-unlock.md)</td><td>A companion device is a device that can act in conjunction with your Windows 10 desktop to enhance the user authentication experience. Using the Companion Device Framework, a companion device can provide a rich experience for Microsoft Passport even when Windows Hello is not available (e.g., if the Windows 10 desktop lacks a camera for face authentication or fingerprint reader device, for example).</td></tr>
<tr><td>[Web Account Manager](web-account-manager.md)</td><td>This article describes how to show the AccountsSettingsPane and connect your Universal Windows Platform (UWP) app to external identity providers, like Microsoft or Facebook, using the Windows 10 Web Account Manager APIs. You'll learn how to request a user's permission to use their Microsoft account, obtain an access token, and use it to perform basic operations (like get profile data or upload files to their OneDrive). </td></tr>
<tr><td>[Web authentication broker](web-authentication-broker.md)</td><td>This article explains how to connect your app to an online identity provider that uses authentication protocols like OpenID or OAuth, such as Facebook, Twitter, Flickr, Instagram, and so on. The [AuthenticateAsync](https://msdn.microsoft.com/library/windows/apps/br212066) method sends a request to the online identity provider and gets back an access token that describes the provider resources to which the app has access.</td></tr>
</table>

## Cryptography 

The cryptography section contains information on more complex, cryptographic related topics. 

| Topic                                                                         | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
|-------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Intro to certificates](certificates.md)                                      | This article discusses the use of certificates in apps. Digital certificates are used in public key cryptography to bind a public key to a person, computer, or organization. The bound identities are most often used to authenticate one entity to another. For example, certificates are often used to authenticate a web server to a user and a user to a web server. You can create certificate requests and install or import issued certificates. You can also enroll a certificate in a certificate hierarchy. |
| [Cryptographic keys](cryptographic-keys.md)                                   | This article shows how to use standard key derivation functions to derive keys and how to encrypt content using symmetric and asymmetric keys.                                                                                                                                                                                                                                                                                                                                                                         |
| [Data protection](data-protection.md)                                         | This article explains how to use the [DataProtectionProvider](https://msdn.microsoft.com/library/windows/apps/br241559) class in the [Windows.Security.Cryptography.DataProtection](https://msdn.microsoft.com/library/windows/apps/br241585) namespace to encrypt and decrypt digital data in a UWP app.                                                                                                                                                                                                              |
| [MACs, hashes, and signatures](macs-hashes-and-signatures.md)               | This article discusses how message authentication codes (MACs), hashes, and signatures can be used in apps to detect message tampering.                                                                                                                                                                                                                                                                                                                                                                                |
| [Export restrictions on cryptography](export-restrictions-on-cryptography.md) | Use this info to determine if your app uses cryptography in a way that might prevent it from being listed in the .                                                                                                                                                                                                                                                                                                                                                                                                     |
| [Common cryptography tasks](common-cryptography-tasks.md)                     | These articles provide example code for common cryptography tasks, such as creating random numbers, comparing buffers, converting between strings and binary data, copying to and from byte arrays, and encoding and decoding data.                                                                                                                                                                                                                                                                                    |
