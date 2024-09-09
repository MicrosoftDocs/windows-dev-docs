---
title: Tools and libraries for passkeys
description: This topic contains info about tools and libraries to help you implement passkeys.
ms.topic: article
ms.date: 06/20/2024
---

# Tools and libraries for passkeys

This topic contains info about tools and libraries to help you implement passkeys.

## Libraries

### Selection criteria

If you wish to own passwordless authentication internally, or you're looking to implement a turnkey solution for passkeys, then you'll likely be looking for libraries or vendors. When selecting a library to implement passkeys, what should you as a developer at a relying party (RP) keep an eye on?

> [!NOTE]
> A small set of these criteria are not specific to passkeys, but are useful to keep in mind when selecting an open-source solution.

### WebAuthn versions and capabilities

* **Version**. Check which version of the spec the library supports (Level 2, Level 3, and so on).
* **Features and capabilities**. Check whether the library includes key features and capabilities for your use case.
  * Does the library help with generating registration and authentication options? Does it help with verification of the registration and authentication response? From a relying party (RP) perspective, those are the key steps of your implementation. So make sure that the library you select provides useful functions for those steps.
  * If you're considering using attestation features:
    * Does the library help leverage FIDO MDS in some way?
    * Can the library verify all attestation statement formats?

### Verification steps

Check whether the library follows the necessary verification steps:

* During registration.
* During authentication.

### User interface (UI) and user experience (UX)

If you're looking for a library that offers UI elements:

* **Visual consistency**. Check that the solution uses standardized icons.
* **Clear language**. Instructions using plain language are critical for broader user understanding. Prioritize solutions aligned with the FIDO UX guidelines.

### Developer experience

* **Full-stack coverage**. A library that offers tightly-integrated frontend and backend components (for example, SimpleWebAuthn) can streamline your integration.
* **Developer documentation**. In order to ease the integration process, check that the library has a maintained documentation website .

### Developer involvement and maintenance

* **Open-source maintenance**. For open-source options, investigate their community activity. A few active issues, or many issues with up-to-date labels (assuming that those require manual assignment), and comments by contributors, are all signals of an active community.
* **Patience**. Standards can be slow-moving. So a WebAuthn/passkey library can go a long time between updates if there aren't any real issues with it. But that doesn't mean the library is unmaintained.

### Licensing

Review the solution's licensing model (for example, MIT, Apache, commercial) in the context of your project.

### Updated for passkeys

* **Rust**. [Webauthn-rs - Webauthn for Rust Server Applications](https://docs.rs/webauthn-rs/latest/webauthn_rs/) (William Brown).
* **TypeScript**. [SimpleWebAuthn](https://simplewebauthn.dev/) (Matthew Miller).
* **Java**. [java-webauthn-server](https://github.com/Yubico/java-webauthn-server) (Yubico).

### Other FIDO2/WebAuthn libraries

The [WebAuthn Awesome](https://github.com/yackermann/awesome-webauthn) GitHub repo is also regularly updated with libraries from the community.

* **.NET**. [FIDO2 .NET Library](https://fido2-net-lib.passwordless.dev/) (Anders Åberg, Alex Seigler).
* **Go**. [WebAuthn Library](https://github.com/go-webauthn/webauthn).
* **Java**. [WebAuthn4J](https://github.com/webauthn4j/webauthn4j) (Yoshikazu Nojima).
* **Python**. [py_webauthn](https://github.com/duo-labs/py_webauthn) (Duo Labs).
* **Ruby**. [webauthn-ruby](https://github.com/cedarcode/webauthn-ruby) (Cedarcode), [Devise::Passkeys](https://github.com/ruby-passkeys/devise-passkeys) (Ruby Passkeys, wrapper around webauthn-ruby), and [Warden::WebAuthn](https://github.com/ruby-passkeys/warden-webauthn) (Ruby Passkeys, wrapper around webauthn-ruby).

## Test sites and tools

In addition to the resources listed in the sections below, the [WebAuthn Awesome](https://github.com/yackermann/awesome-webauthn) GitHub repo is also regularly updated with tools and demos from the community.

### Basic FIDO2/WebAuthn tools

* [WebAuthn.io](https://webauthn.io/).
* [yubico](https://demo.yubico.com/webauthn-technical/registration) demo site.
* [webauthn](https://webauthn.me/).

### Advanced FIDO2/WebAuthn tools

* [WebAuthn Test App](https://webauthntest.identitystandards.io/).
* [lbuchs/WebAuthn](https://webauthn.lubu.ch/_test/client.html).
* [WebAuthn Debugger](https://debugger.simplewebauthn.dev/?).
* [WebAuthn Developer Tool](https://demo.yubico.com/webauthn-developers).

## Next steps

Next, see [Reference for passkeys](./reference.md).

## Further info

* [Intro to passkeys](./intro.md)
* [Passkeys.dev](https://passkeys.dev/)
* [Get Started on Your Passwordless Journey](https://fidoalliance.org/implement-passkeys-overview/) on the FIDO Alliance website
