---
title: Create random numbers
description: Learn how to generate cryptographically secure random numbers and buffers in a Windows app by using the Windows.Security.Cryptography APIs.
ms.assetid: 15746824-F93A-4DC7-836E-EBA916D2CFD3
ms.date: 07/15/2026
ms.topic: article
---
# Create random numbers



This example code shows how to create a random number or buffer for use in cryptography in a WinUI app.

```csharp
public string GenerateRandomData()
{
    // Define the length, in bytes, of the buffer.
    uint length = 32;

    // Generate random data and copy it to a buffer.
    IBuffer buffer = CryptographicBuffer.GenerateRandom(length);

    // Encode the buffer to a hexadecimal string (for display).
    string randomHex = CryptographicBuffer.EncodeToHexString(buffer);

    return randomHex;
}

public uint GenerateRandomNumber()
{
    // Generate a random number.
    uint random = CryptographicBuffer.GenerateRandomNumber();
    return random;
}
```
