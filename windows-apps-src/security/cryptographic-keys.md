---
title: Cryptographic keys
description: This article shows how to use standard key derivation functions to derive keys and how to encrypt content using symmetric and asymmetric keys.
ms.assetid: F35BEBDF-28C5-4F91-A94E-F7D862B6ED59
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---
# Cryptographic keys




This article shows how to use standard key derivation functions to derive keys and how to encrypt content using symmetric and asymmetric keys. 

## Symmetric keys


Symmetric key encryption, also called secret key encryption, requires that the key used for encryption also be used for decryption. You can use a [**SymmetricKeyAlgorithmProvider**](/uwp/api/Windows.Security.Cryptography.Core.SymmetricKeyAlgorithmProvider) class to specify a symmetric algorithm and create or import a key. You can use static methods on the [**CryptographicEngine**](/uwp/api/Windows.Security.Cryptography.Core.CryptographicEngine) class to encrypt and decrypt data by using the algorithm and key.

Symmetric key encryption typically uses block ciphers and block cipher modes. A block cipher is a symmetric encryption function that operates on fixed size blocks. If the message you want to encrypt is longer than the block length, you must use a block cipher mode. A block cipher mode is a symmetric encryption function built by using a block cipher. It encrypts plaintext as a series of fixed size blocks. The following modes are supported for apps:

-   The ECB (electronic codebook) mode encrypts each block of the message separately. This is not considered a secure encryption mode.
-   The CBC (cipher block chaining) mode uses the previous ciphertext block to obfuscate the current block. You must determine what value to use for the first block. This value is called the initialization vector (IV).
-   The CCM (counter with CBC-MAC) mode combines the CBC block cipher mode with a message authentication code (MAC).
-   The GCM (Galois counter mode) mode combines the counter encryption mode with the Galois authentication mode.

Some modes such as CBC require that you use an initialization vector (IV) for the first ciphertext block. The following are common initialization vectors. You specify the IV when calling [**CryptographicEngine.Encrypt**](/uwp/api/windows.security.cryptography.core.cryptographicengine.encrypt). For most cases it is important that the IV never be reused with the same key.

-   Fixed uses the same IV for all messages to be encrypted. This leaks information and its use is not recommended.
-   Counter increments the IV for each block.
-   Random creates a pseudorandom IV. You can use [**CryptographicBuffer.GenerateRandom**](/uwp/api/windows.security.cryptography.cryptographicbuffer.generaterandom) to create the IV.
-   Nonce-Generated uses a unique number for each message to be encrypted. Typically, the nonce is a modified message or transaction identifier. The nonce does not have to be kept secret, but it should never be reused under the same key.

Most modes require that the length of the plaintext be an exact multiple of the block size. This usually requires that you pad the plaintext to obtain the appropriate length.

While block ciphers encrypt fixed size blocks of data, stream ciphers are symmetric encryption functions that combine plaintext bits with a pseudorandom bit stream (called a key stream) to generate the ciphertext. Some block cipher modes such as output feedback mode (OTF) and counter mode (CTR) effectively turn a block cipher into a stream cipher. Actual stream ciphers such as RC4, however, typically operate at higher speeds than block cipher modes are capable of achieving.

The following example shows how to use the [**SymmetricKeyAlgorithmProvider**](/uwp/api/Windows.Security.Cryptography.Core.SymmetricKeyAlgorithmProvider) class to create a symmetric key and use it to encrypt and decrypt data.

## Asymmetric keys


Asymmetric key cryptography, also called public key cryptography, uses a public key and a private key to perform encryption and decryption. The keys are different but mathematically related. Typically the private key is kept secret and is used to decrypt data while the public key is distributed to interested parties and is used to encrypt data. Asymmetric cryptography is also useful for signing data.

Because asymmetric cryptography is much slower than symmetric cryptography, it is seldom used to encrypt large amounts of data directly. Instead, it is typically used in the following manner to encrypt keys.

-   Alice requires that Bob send her only encrypted messages.
-   Alice creates a private/public key pair, keeps her private key secret and publishes her public key.
-   Bob has a message he wants to send to Alice.
-   Bob creates a symmetric key.
-   Bob uses his new symmetric key to encrypt his message to Alice.
-   Bob uses Alice’s public key to encrypt his symmetric key.
-   Bob sends the encrypted message and the encrypted symmetric key to Alice (enveloped).
-   Alice uses her private key (from the private/public pair) to decrypt Bob’s symmetric key.
-   Alice uses Bob’s symmetric key to decrypt the message.

You can use an [**AsymmetricKeyAlgorithmProvider**](/uwp/api/Windows.Security.Cryptography.Core.AsymmetricKeyAlgorithmProvider) object to specify an asymmetric algorithm or a signing algorithm, to create or import an ephemeral key pair, or to import the public key portion of a key pair.

## Deriving keys


It is often necessary to derive additional keys from a shared secret. You can use the [**KeyDerivationAlgorithmProvider**](/uwp/api/Windows.Security.Cryptography.Core.KeyDerivationAlgorithmProvider) class and one of the following specialized methods in the [**KeyDerivationParameters**](/uwp/api/Windows.Security.Cryptography.Core.KeyDerivationParameters) class to derive keys.

| Object                                                                            | Description                                                                                                                                |
|-----------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------|
| [**BuildForPbkdf2**](/uwp/api/windows.security.cryptography.core.keyderivationparameters.buildforpbkdf2)    | Creates a KeyDerivationParameters object for use in the password-based key derivation function 2 (PBKDF2).                                 |
| [**BuildForSP800108**](/uwp/api/windows.security.cryptography.core.keyderivationparameters.buildforsp800108)  | Creates a KeyDerivationParameters object for use in a counter mode, hash-based message authentication code (HMAC) key derivation function. |
| [**BuildForSP80056a**](/uwp/api/windows.security.cryptography.core.keyderivationparameters.buildforsp80056a)  | Creates a KeyDerivationParameters object for use in the SP800-56A key derivation function.                                                 |

 