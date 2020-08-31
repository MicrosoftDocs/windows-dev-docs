---
title: MACs, hashes, and signatures
description: This article discusses how message authentication codes (MACs), hashes, and signatures can be used in Universal Windows Platform (UWP) apps to detect message tampering.
ms.assetid: E674312F-6678-44C5-91D9-B489F49C4D3C
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---
# MACs, hashes, and signatures




This article discusses how message authentication codes (MACs), hashes, and signatures can be used in Universal Windows Platform (UWP) apps to detect message tampering.

## Message authentication codes (MACs)


Encryption helps prevent an unauthorized individual from reading a message, but it does not prevent that individual from tampering with the message. An altered message, even if the alteration results in nothing but nonsense, can have real costs. A message authentication code (MAC) helps prevent message tampering. For example, consider the following scenario:

-   Bob and Alice share a secret key and agree on a MAC function to use.
-   Bob creates a message and inputs the message and the secret key into a MAC function to retrieve a MAC value.
-   Bob sends the \[unencrypted\] message and the MAC value to Alice over a network.
-   Alice uses the secret key and the message as input to the MAC function. She compares the generated MAC value to the MAC value sent by Bob. If they are the same, the message was not changed in transit.

Note that Eve, a third party eavesdropping on the conversation between Bob and Alice, cannot effectively manipulate the message. Eve does not have access to the private key and cannot, therefore, create a MAC value which would make the tampered message appear legitimate to Alice.

Creating a message authentication code ensures only that the original message was not altered and, by using a shared secret key, that the message hash was signed by someone with access to that private key.

You can use the [**MacAlgorithmProvider**](/uwp/api/Windows.Security.Cryptography.Core.MacAlgorithmProvider) to enumerate the available MAC algorithms and generate a symmetric key. You can use static methods on the [**CryptographicEngine**](/uwp/api/Windows.Security.Cryptography.Core.CryptographicEngine) class to perform the necessary encryption that creates the MAC value.

Digital signatures are the public key equivalent of private key message authentication codes (MACs). Although MACs use private keys to enable a message recipient to verify that a message has not been altered during transmission, signatures use a private/public key pair.

This example code shows how to use the [**MacAlgorithmProvider**](/uwp/api/Windows.Security.Cryptography.Core.MacAlgorithmProvider) class to create a hashed message authentication code (HMAC).

```cs
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace SampleMacAlgorithmProvider
{
    sealed partial class MacAlgProviderApp : Application
    {
        public MacAlgProviderApp()
        {
            // Initialize the application.
            this.InitializeComponent();

            // Initialize the hashing process.
            String strMsg = "This is a message to be authenticated";
            String strAlgName = MacAlgorithmNames.HmacSha384;
            IBuffer buffMsg;
            CryptographicKey hmacKey;
            IBuffer buffHMAC;

            // Create a hashed message authentication code (HMAC)
            this.CreateHMAC(
                strMsg,
                strAlgName,
                out buffMsg,
                out hmacKey,
                out buffHMAC);

            // Verify the HMAC.
            this.VerifyHMAC(
                buffMsg,
                hmacKey,
                buffHMAC);
        }

        void CreateHMAC(
            String strMsg,
            String strAlgName,
            out IBuffer buffMsg,
            out CryptographicKey hmacKey,
            out IBuffer buffHMAC)
        {
            // Create a MacAlgorithmProvider object for the specified algorithm.
            MacAlgorithmProvider objMacProv = MacAlgorithmProvider.OpenAlgorithm(strAlgName);

            // Demonstrate how to retrieve the name of the algorithm used.
            String strNameUsed = objMacProv.AlgorithmName;

            // Create a buffer that contains the message to be signed.
            BinaryStringEncoding encoding = BinaryStringEncoding.Utf8;
            buffMsg = CryptographicBuffer.ConvertStringToBinary(strMsg, encoding);

            // Create a key to be signed with the message.
            IBuffer buffKeyMaterial = CryptographicBuffer.GenerateRandom(objMacProv.MacLength);
            hmacKey = objMacProv.CreateKey(buffKeyMaterial);

            // Sign the key and message together.
            buffHMAC = CryptographicEngine.Sign(hmacKey, buffMsg);

            // Verify that the HMAC length is correct for the selected algorithm
            if (buffHMAC.Length != objMacProv.MacLength)
            {
                throw new Exception("Error computing digest");
            }
         }

        public void VerifyHMAC(
            IBuffer buffMsg,
            CryptographicKey hmacKey,
            IBuffer buffHMAC)
        {
            // The input key must be securely shared between the sender of the HMAC and 
            // the recipient. The recipient uses the CryptographicEngine.VerifySignature() 
            // method as follows to verify that the message has not been altered in transit.
            Boolean IsAuthenticated = CryptographicEngine.VerifySignature(hmacKey, buffMsg, buffHMAC);
            if (!IsAuthenticated)
            {
                throw new Exception("The message cannot be verified.");
            }
        }
    }
}
```

## Hashes


A cryptographic hash function takes an arbitrarily long block of data and returns a fixed-size bit string. Hash functions are typically used when signing data. Because most public key signature operations are computationally intensive, it is typically more efficient to sign (encrypt) a message hash than it is to sign the original message. The following procedure represents a common, albeit simplified, scenario:

-   Bob and Alice share a secret key and agree on a MAC function to use.
-   Bob creates a message and inputs the message and the secret key into a MAC function to retrieve a MAC value.
-   Bob sends the \[unencrypted\] message and the MAC value to Alice over a network.
-   Alice uses the secret key and the message as input to the MAC function. She compares the generated MAC value to the MAC value sent by Bob. If they are the same, the message was not changed in transit.

Note that Alice sent an unencrypted message. Only the hash was encrypted. The procedure ensures only that the original message was not altered and, by using Alice's public key, that the message hash was signed by someone with access to Alice's private key, presumably Alice.

You can use the [**HashAlgorithmProvider**](/uwp/api/Windows.Security.Cryptography.Core.HashAlgorithmProvider) class to enumerate the available hash algorithms and create a [**CryptographicHash**](/uwp/api/Windows.Security.Cryptography.Core.CryptographicHash) value.

Digital signatures are the public key equivalent of private key message authentication codes (MACs). Whereas MACs use private keys to enable a message recipient to verify that a message has not been altered during transmission, signatures use a private/public key pair.

The [**CryptographicHash**](/uwp/api/Windows.Security.Cryptography.Core.CryptographicHash) object can be used to repeatedly hash different data without having to re-create the object for each use. The [**Append**](/uwp/api/windows.security.cryptography.core.cryptographichash.append) method adds new data to a buffer to be hashed. The [**GetValueAndReset**](/uwp/api/windows.security.cryptography.core.cryptographichash.getvalueandreset) method hashes the data and resets the object for another use. This is shown by the following example.

```cs
public void SampleReusableHash()
{
    // Create a string that contains the name of the hashing algorithm to use.
    String strAlgName = HashAlgorithmNames.Sha512;

    // Create a HashAlgorithmProvider object.
    HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm(strAlgName);

    // Create a CryptographicHash object. This object can be reused to continually
    // hash new messages.
    CryptographicHash objHash = objAlgProv.CreateHash();

    // Hash message 1.
    String strMsg1 = "This is message 1.";
    IBuffer buffMsg1 = CryptographicBuffer.ConvertStringToBinary(strMsg1, BinaryStringEncoding.Utf16BE);
    objHash.Append(buffMsg1);
    IBuffer buffHash1 = objHash.GetValueAndReset();

    // Hash message 2.
    String strMsg2 = "This is message 2.";
    IBuffer buffMsg2 = CryptographicBuffer.ConvertStringToBinary(strMsg2, BinaryStringEncoding.Utf16BE);
    objHash.Append(buffMsg2);
    IBuffer buffHash2 = objHash.GetValueAndReset();

    // Hash message 3.
    String strMsg3 = "This is message 3.";
    IBuffer buffMsg3 = CryptographicBuffer.ConvertStringToBinary(strMsg3, BinaryStringEncoding.Utf16BE);
    objHash.Append(buffMsg3);
    IBuffer buffHash3 = objHash.GetValueAndReset();

    // Convert the hashes to string values (for display);
    String strHash1 = CryptographicBuffer.EncodeToBase64String(buffHash1);
    String strHash2 = CryptographicBuffer.EncodeToBase64String(buffHash2);
    String strHash3 = CryptographicBuffer.EncodeToBase64String(buffHash3);
}

```

## Digital signatures


Digital signatures are the public key equivalent of private key message authentication codes (MACs). Whereas MACs use private keys to enable a message recipient to verify that a message has not been altered during transmission, signatures use a private/public key pair.

Because most public key signature operations are computationally intensive, however, it is typically more efficient to sign (encrypt) a message hash than it is to sign the original message. The sender creates a message hash, signs it, and sends both the signature and the (unencrypted) message. The recipient calculates a hash over the message, decrypts the signature, and compares the decrypted signature to the hash value. If they match, the recipient can be fairly certain that the message did, in fact, come from the sender and was not altered during transmission.

Signing ensures only that the original message was not altered and, by using the sender's public key, that the message hash was signed by someone with access to the private key.

You can use an [**AsymmetricKeyAlgorithmProvider**](/uwp/api/Windows.Security.Cryptography.Core.AsymmetricKeyAlgorithmProvider) object to enumerate the available signature algorithms and generate or import a key pair. You can use static methods on the [**CryptographicHash**](/uwp/api/Windows.Security.Cryptography.Core.CryptographicHash) class to sign a message or verify a signature.