---
title: Reference for passkeys
description: This topic offers some reference info, demos, and examples for passkeys on Windows.
ms.topic: article
ms.date: 08/27/2024
---

# Reference for passkeys on Windows

This topic offers some reference info, demos, and examples for passkeys on Windows.

## Overview

Passkeys can be used in all supported versions of Windows clients. As of the September 2023 moment, for Windows 11, version 22H2 with [KB5030310](https://support.microsoft.com/topic/september-26-2023-kb5030310-os-build-22621-2361-preview-363ac1ae-6ea8-41b3-b3cc-22a2a5682faf) or later, native support for passkey management is available (see [Support for passkeys in Windows](/windows/security/identity-protection/passkeys/)). Windows native credential UX is displayed when a user saves or signs in with a passkey (even from the browser). Users can perform passkey management from Windows settings or Edge. Cross-device authentication through the browser, leveraging chromium-based credential UI, is still available.

## Cross-Device authentication

Starting in Windows 11, version 23H2, [FIDO Cross-Device Authentication](https://passkeys.dev/docs/reference/terms/#cross-device-authentication-cda) (CDA) is supported globally at the operating system (OS) level, and is available for all apps and browsers. Persistent linking is available between Android devices (authenticator) and Windows 11, version 23H2, and later. iOS and iPadOS don't support persistent linking.

## User-verification behavior

When a user tries to interact with a passkey on Windows 11, an available screen unlock method is used for user verification via Windows Hello. Setting up facial recognition or fingerprint recognition are optional.

Where those biometrics are not configured or available, both passkey creation and authentication fall back to requesting the Windows Hello PIN.

## Demos and examples

See [Active Deployments](https://passkeys.dev/docs/demos-examples/active-deployments/) and [Demo Sites & Services](https://passkeys.dev/docs/demos-examples/demos/).

## Further info

* [Intro to passkeys](./intro.md)
* [Passkeys.dev](https://passkeys.dev/)
* [Get Started on Your Passwordless Journey](https://fidoalliance.org/implement-passkeys-overview/) on the FIDO Alliance website
