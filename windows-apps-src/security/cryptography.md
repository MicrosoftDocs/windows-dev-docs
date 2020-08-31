---
title: Cryptography
description: The article provides an overview of the cryptography features available to Universal Windows Platform (UWP) apps. For detailed information on particular tasks, see the table at the end of this article.
ms.assetid: 9C213036-47FD-4AA4-99E0-84006BE63F47
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---
# Cryptography




The article provides an overview of the cryptography features available to Universal Windows Platform (UWP) apps. For detailed information on particular tasks, see the table at the end of this article.

## Terminology


The following terminology is commonly used in cryptography and public key infrastructure (PKI).

| Term                        | Description                                                                                                                                                                                           |
|-----------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Encryption                  | The process of transforming data by using a cryptographic algorithm and key. The transformed data can be recovered only by using the same algorithm and the same (symmetric) or related (public) key. |
| Decryption                  | The process of returning encrypted data to its original form.                                                                                                                                         |
| Plaintext                   | Originally referred to an unencrypted text message. Currently refers to any unencrypted data.                                                                                                         |
| Ciphertext                  | Originally referred to an encrypted, and therefore unreadable, text message. Currently refers to any encrypted data.                                                                                  |
| Hashing                     | The process of converting variable length data into a fixed length, typically smaller, value. By comparing hashes, you can obtain reasonable assurance that two or more data are the same.            |
| Signature                   | Encrypted hash of digital data typically used to authenticate the sender of the data or verify that the data was not tampered with during transmission.                                               |
| Algorithm                   | A step-by-step procedure for encrypting data.                                                                                                                                                         |
| Key                         | A random or pseudorandom number used as input to a cryptographic algorithm to encrypt and decrypt data.                                                                                               |
| Symmetric Key Cryptography  | Cryptography in which encryption and decryption use the same key. This is also known as secret key cryptography.                                                                                      |
| Asymmetric Key Cryptography | Cryptography in which encryption and decryption use a different but mathematically related key. This is also called public key cryptography.                                                          |
| Encoding                    | The process of encoding digital messages, including certificates, for transport across a network.                                                                                                     |
| Algorithm Provider          | A DLL that implements a cryptographic algorithm.                                                                                                                                                      |
| Key Storage Provider        | A container for storing key material. Currently, keys can be stored in software, smart cards, or the trusted platform module (TPM).                                                                   |
| X.509 Certificate           | A digital document, typically issued by a certification authority, to verify the identity of an individual, system, or entity to other interested parties.                                            |

 
## Namespaces

The following namespaces are available for use in apps.

### Windows.Security.Cryptography

Contains the CryptographicBuffer class and static methods that enable you to:

-   Convert data to and from strings
-   Convert data to and from byte arrays
-   Encode messages for network transport
-   Decode messages after transport

### Windows.Security.Cryptography.Certificates

Contains classes, interfaces, and enumeration types that enable you to:

-   Create a certificate request
-   Install a certificate response
-   Import a certificate in a PFX file
-   Specify and retrieve certificate request properties

### Windows.Security.Cryptography.Core

Contains classes and enumeration types that enable you to:

-   Encrypt and decrypt data
-   Hash data
-   Sign data and verify signatures
-   Create, import, and export keys
-   Work with asymmetric key algorithm providers
-   Work with symmetric key algorithm providers
-   Work with hash algorithm providers
-   Work with machine authentication code (MAC) algorithm providers
-   Work with key derivation algorithm providers

### Windows.Security.Cryptography.DataProtection

Contains classes that enable you to:

-   Asynchronously encrypt and decrypt static data
-   Asynchronously encrypt and decrypt data streams

## Crypto and PKI application capabilities


The simplified application programming interface available for apps enables the following cryptographic and public key infrastructure (PKI) capabilities.

### Cryptography support

You can perform the following cryptographic tasks. For more information, see the [**Windows.Security.Cryptography.Core**](/uwp/api/Windows.Security.Cryptography.Core) namespace.

-   Create symmetric keys
-   Perform symmetric encryption
-   Create asymmetric keys
-   Perform asymmetric encryption
-   Derive password based keys
-   Create message authentication codes (MACs)
-   Hash content
-   Digitally sign content

The SDK also provides a simplified interface for password-based data protection. You can use this to perform the following tasks. For more information, see the [**Windows.Security.Cryptography.DataProtection**](/uwp/api/Windows.Security.Cryptography.DataProtection) namespace.

-   Asynchronous protection of static data
-   Asynchronous protection of a data stream

### Encoding support

An app can encode cryptographic data for transmission across a network and decode data received from a network source. For more information, see the static methods available in the [**Windows.Security.Cryptography**](/uwp/api/Windows.Security.Cryptography) namespace.

### PKI support

Apps can perform the following PKI tasks. For more information, see the [**Windows.Security.Cryptography.Certificates**](/uwp/api/Windows.Security.Cryptography.Certificates) namespace.

-   Create a certificate
-   Create a self-signed certificate
-   Install a certificate response
-   Import a certificate in PFX format
-   Use smart card certificates and keys (sharedUserCertificates capabilities set)
-   Use certificates from the user MY store (sharedUserCertificates capabilities set)

Additionally, you can use the manifest to perform the following actions:

-   Specify per application trusted root certificates
-   Specify per application peer trusted certificates
-   Explicitly disable inheritance from system trust
-   Specify the certificate selection criteria
    -   Hardware certificates only
    -   Certificates that chain through a specified set of issuers
    -   Automatically select a certificate from the application store

## Detailed articles


The following articles provide more detail on security scenarios:

| Topic                                                                         | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
|-------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Certificates](certificates.md)                                               | This article discusses the use of certificates in UWP apps. Digital certificates are used in public key cryptography to bind a public key to a person, computer, or organization. The bound identities are most often used to authenticate one entity to another. For example, certificates are often used to authenticate a web server to a user and a user to a web server. You can create certificate requests and install or import issued certificates. You can also enroll a certificate in a certificate hierarchy. |
| [Cryptographic keys](cryptographic-keys.md)                                   | This article shows how to use standard key derivation functions to derive keys and how to encrypt content using symmetric and asymmetric keys.                                                                                                                                                                                                                                                                                                                                                                             |
| [Data protection](data-protection.md)                                         | This article explains how to use the [DataProtectionProvider](/uwp/api/Windows.Security.Cryptography.DataProtection.DataProtectionProvider) class in the [Windows.Security.Cryptography.DataProtection](/uwp/api/Windows.Security.Cryptography.DataProtection) namespace to encrypt and decrypt digital data in a UWP app.                                                                                                                                                                                                                  |
| [MACs, hashes, and signatures](macs-hashes-and-signatures.md)               | This article discusses how message authentication codes (MACs), hashes, and signatures can be used in UWP apps to detect message tampering.                                                                                                                                                                                                                                                                                                                                                                                |
| [Export restrictions on cryptography](export-restrictions-on-cryptography.md) | Use this info to determine if your app uses cryptography in a way that might prevent it from being listed in the Microsoft Store.                                                                                                                                                                                                                                                                                                                                                                                            |
| [Common cryptography tasks](common-cryptography-tasks.md)                     | These articles provide example code for common UWP cryptography tasks, such as creating random numbers, comparing buffers, converting between strings and binary data, copying to and from byte arrays, and encoding and decoding data.                                                                                                                                                                                                                                                                                    |

 