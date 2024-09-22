---
title: Security
description: This section contains articles on building secure Universal Windows Platform (UWP) apps for Windows.
ms.assetid: 41E2EEFB-E8A9-4592-814C-72B703CD952C
ms.date: 07/08/2024
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---

# Security

This section contains articles on building secure Universal Windows Platform (UWP) apps for Windows.

## Introduction

If you're new to Windows or UWP development, start with the [Intro to secure Windows app development](intro-to-secure-windows-app-development.md). This introductory-level article provides an overview of security considerations for apps and the various features available in Windows.

## Authentication and user identity

The [authentication and user identity section](authentication-and-user-identity.md) contains walkthroughs for scenarios related to user login and identity. Apps have several options for user authentication, ranging from simple single sign-on (SSO) using [Web authentication broker](web-authentication-broker.md) to highly secure two-factor authentication.

| Topic | Description |
|-------|-------------|
| [Credential locker](/windows/apps/develop/security/credential-locker) | This article describes how apps can use the Credential Locker to securely store and retrieve user credentials, and roam them between devices with the user's Microsoft account. |
| [Fingerprint biometrics](/windows/apps/develop/security/fingerprint-biometrics) | This article explains how to add fingerprint biometrics to your app. Including a request for fingerprint authentication when the user must consent to a particular action increases the security of your app. For example, you could require fingerprint authentication before authorizing an in-app purchase, or access to restricted resources. Fingerprint authentication is managed using the [UserConsentVerifier](/uwp/api/Windows.Security.Credentials.UI.UserConsentVerifier) class in the [Windows.Security.Credentials.UI](/uwp/api/Windows.Security.Credentials.UI) namespace. |
| [Windows Hello](/windows/apps/develop/security/windows-hello) | This article describes the Windows Hello technology, and discusses how developers can implement this technology to protect their apps and backend services. It highlights specific capabilities of these technologies that help mitigate threats from conventional credentials and provides guidance about designing and deploying these technologies as part of your packaged Windows apps. |
| [Create a Windows Hello login app](/windows/apps/develop/security/windows-hello-login) | Part 1 of a complete walkthrough on how to create a packaged Windows app that uses Windows Hello as an alternative to traditional username and password authentication systems. |
| [Create a Windows Hello login service](/windows/apps/develop/security/windows-hello-auth-service) | Part 2 of a complete walkthrough on how to use Windows Hello as an alternative to traditional username and password authentication systems in packaged Windows apps. |
| [Smart cards](/windows/apps/develop/security/smart-cards) | This topic explains how apps can use smart cards to connect users to secure network services, including how to access physical smart card readers, create virtual smart cards, communicate with smart cards, authenticate users, reset user PINs, and remove or disconnect smart cards. |
| [Share certificates between apps](share-certificates.md) | UWP apps that require secure authentication beyond a user Id and password combination can use certificates for authentication. Certificate authentication provides a high level of trust when authenticating a user. In some cases, a group of services will want to authenticate a user for multiple apps. This article shows how you can authenticate multiple apps using the same certificate, and how you can provide convenient code for a user to import a certificate that was provided to access secured web services. |
| [Windows Unlock with companion IoT devices](companion-device-unlock.md) | A companion device is a device that can act in conjunction with Windows to enhance the user authentication experience. Using the Companion Device Framework, a companion device can provide a rich experience even when Windows Hello is not available (for example, if the Windows machine lacks a camera for face authentication or fingerprint reader device, for example). |
| [Web Account Manager](web-account-manager.md) | This article describes how to show the AccountsSettingsPane and connect your Universal Windows Platform (UWP) app to external identity providers, like Microsoft or Facebook, using the Windows Web Account Manager APIs. You'll learn how to request a user's permission to use their Microsoft account, obtain an access token, and use it to perform basic operations (like get profile data or upload files to their OneDrive). |
| [Web authentication broker](web-authentication-broker.md) | This article explains how to connect your app to an online identity provider that uses authentication protocols like OpenID or OAuth. The [AuthenticateAsync](/uwp/api/windows.security.authentication.web.webauthenticationbroker.authenticateasync) method sends a request to the online identity provider and gets back an access token that describes the provider resources to which the app has access. |

## Cryptography

The cryptography section contains information on more complex, cryptographic related topics.

| Topic | Description |
|-------|-------------|
| [Intro to certificates](certificates.md) | This article discusses the use of certificates in apps. Digital certificates are used in public key cryptography to bind a public key to a person, computer, or organization. The bound identities are most often used to authenticate one entity to another. For example, certificates are often used to authenticate a web server to a user and a user to a web server. You can create certificate requests and install or import issued certificates. You can also enroll a certificate in a certificate hierarchy. |
| [Cryptographic keys](cryptographic-keys.md) | This article shows how to use standard key derivation functions to derive keys and how to encrypt content using symmetric and asymmetric keys. |
| [Data protection](data-protection.md) | This article explains how to use the [DataProtectionProvider](/uwp/api/Windows.Security.Cryptography.DataProtection.DataProtectionProvider) class in the [Windows.Security.Cryptography.DataProtection](/uwp/api/Windows.Security.Cryptography.DataProtection) namespace to encrypt and decrypt digital data in a UWP app. |
| [MACs, hashes, and signatures](macs-hashes-and-signatures.md) | This article discusses how message authentication codes (MACs), hashes, and signatures can be used in apps to detect message tampering. |
| [Export restrictions on cryptography](export-restrictions-on-cryptography.md) | Use this info to determine if your app uses cryptography in a way that might prevent it from being listed in the Microsoft Store. |
| [Common cryptography tasks](common-cryptography-tasks.md) | These articles provide example code for common cryptography tasks, such as creating random numbers, comparing buffers, converting between strings and binary data, copying to and from byte arrays, and encoding and decoding data. |
