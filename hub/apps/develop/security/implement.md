---
title: Implement passkeys
description: This topic describes how to implement passkey sign-ins across online, enterprise, and government applications, and for payments.
ms.topic: article
ms.date: 08/06/2024
---

# Implement passkeys

Also see [Intro to passkeys](./intro.md)

This topic describes how to implement passkey sign-ins across online, enterprise, and government applications, and for payments. You can implement passkeys (instead of passwords) to provide your websites and apps with user-friendly and cryptographically secure sign-ins&mdash;all courtesy of the public [WebAuthn API](https://www.w3.org/TR/webauthn-2/).

Consumer service providers, enterprises, and governments around the world are moving from forms authentication (such as passwords and SMS OTPs) to passkey-based sign-ins. Every organization has its own varying use cases and business requirements.

## How passkeys work

Passkeys use standard public key cryptography techniques. When a user registers with an online service, the user's client device creates a new cryptographic key pair that is bound to the web service domain. The device retains the private key, and registers the public key with the online service. These cryptographic key pairs, called passkeys, are unique and bound to the online service domain.

The way authentication works is that the user's device must prove possession of the private key by presenting a challenge for sign-in to be completed. This occurs after the user approves the sign-in locally on their device, via quick and easy entry of a biometric (such as a thumbprint), or local PIN, or touch of a FIDO security key. Sign-in is completed via a challenge-response from the user's device and the online service. The service doesn't see or ever store the private key.

To enroll a passkey with an online service:
* The user is prompted to create a passkey.
* The user verifies the passkey creation via a local authentication method (for example, biometrics).
* The user's device creates a new public/private key pair (a passkey) that's unique for the local device, the online service, and the user's account.
* The public key (and *only* that) is sent to the online service, and is associated with the user's account.

To subsequently sign in by using a passkey:
* The user is prompted to sign in by using a passkey.
* The user verifies the sign-in-with-passkey via a local authentication method (for example, biometrics).
* The device uses the user's account identifier (provided by the service) to select the correct key, and sign the service's challenge.
* The client device sends the signed challenge back to the service, which verifies it with the stored public key, and signs the user in.

## Implement passkeys for consumers, enterprise, government, or payments

This section is for you if you're considering implementing passkeys.

If you're new to FIDO, then we recommend that you first review the [User Authentication Specifications Overview](https://fidoalliance.org/specifications/). Then, you can access the latest versions of the FIDO Alliance user authentication specifications at [Download Authentication Specifications](https://fidoalliance.org/specifications/download/).

The [FIDO Certified Showcase](https://fidoalliance.org/fido-certified-showcase/) highlights FIDO Alliance members and their FIDO Certified solutions. It's a great resource if you're looking to deploy FIDO.

The FIDO Enterprise Deployment Working Group (EDWG) has developed a series of white papers that provide guidance for leaders and practitioners considering passkeys&mdash;scaling from SMBs to large enterprises. To understand the key decision points, see [Enterprise](https://fidoalliance.org/passkey-use-case/enterprise/).

If your field is along the lines of citizen services or government employee applications, then see [Government](https://fidoalliance.org/passkey-use-case/government/).

For more info about payment scenarios that are ideal for passkeys, see [Payments](https://fidoalliance.org/passkey-use-case/payments/).

## Next steps

Next, see [Design guidelines for passkeys](./design.md).

## Further info

* [Intro to passkeys](./intro.md)
* [WebAuthn APIs for passwordless authentication on Windows](/windows/security/identity-protection/hello-for-business/webauthn-apis)
* [Passkeys.dev](https://passkeys.dev/)
* [Get Started on Your Passwordless Journey](https://fidoalliance.org/implement-passkeys-overview/) on the FIDO Alliance website
