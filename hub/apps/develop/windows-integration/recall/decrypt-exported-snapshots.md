---
title: Decrypt exported snapshots from Recall
description: Learn how to decrypt exported snapshot data from Windows Recall. This feature is only available to users located in the European Economic Area (EEA).
ms.author: mattwoj
author: mattwojo
ms.date: 06/17/2025
ms.topic: article
no-loc: [Recall]
---

# Decrypt exported snapshots from Recall

This guide shows developers how to decrypt exported Recall snapshots for use in applications. You'll learn the complete decryption process with working code samples that you can implement immediately.

**Exporting Recall snapshots is only supported on devices in the European Economic Area (EEA).** Export of Recall snapshots is a user-initiated process and is per user. Exported snapshots are encrypted.  

Learn more about how to [Export Recall snapshots](https://support.microsoft.com/windows/export-recall-snapshots-680bd134-4aaa-4bf5-8548-a8e2911c8069) or see the [Recall overview](index.md) for more about how this AI-backed feature works.

## Prerequisites

The option to export Recall snapshots is only available on [Copilot+ PC devices](https://www.microsoft.com/windows/copilot-plus-pcs) in the European Economic Area (EEA) that are running the latest [Windows Insider Program preview build](https://www.microsoft.com/windowsinsider).

Before you begin, you'll need:

- **Exported snapshots**: The user must first [export Recall snapshots](https://support.microsoft.com/windows/export-recall-snapshots-680bd134-4aaa-4bf5-8548-a8e2911c8069) and provide the folder path where they're saved.
- **Export code**: The 32-character Recall export code provided during snapshot export.
- **Output folder**: A destination folder path where the decrypted .jpg and .json files associated with the exported snapshots will be saved.

## How to decrypt exported Recall snapshots

Get started with sample code for decrypting exported Recall snapshots in the [RecallSnapshotsExport GitHub repository](https://github.com/microsoft/RecallSnapshotsExport). Follow the step-by-step process below to understand how the decryption works.

### Compute Export Key

The user will need to provide the location (folder path) where their exported Recall snapshots have been saved, in addition to the Recall export code that they were asked to save during the initial Recall setup. The Recall export code looks something like: `0a0a-0a0a-1111-bbbb-2222-3c3c-3c3c-3c3c`

First, remove the dash – to result in a 32-character string: `0a0a0a0a1111bbbb22223c3c3c3c3c3c`

```cpp
std::wstring UnexpandExportCode(std::wstring code)
{
    if (code.size() > 32)
    {
        code.erase(std::remove(code.begin(), code.end(), ' '), code.end()); // Remove spaces
        code.erase(std::remove(code.begin(), code.end(), '-'), code.end()); // Remove hyphens
    }


    if (code.size() != 32)
    {
        std::wcout << L"The export code has incorrect number of characters."<< std::endl;
    }


    return code;
}
```

[See sample code](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L307-L321)

Next, build an array containing the byte value for each pair of hex digits in turn.

```cpp
std::vector<uint8_t> HexStringToBytes(const std::wstring& hexString)
{
    std::vector<uint8_t> bytes;
    if (hexString.length() % 2 != 0)
    {
        throw std::invalid_argument("Hex string must have an even length");
    }


    for (size_t i = 0; i < hexString.length(); i += 2)
    {
        std::wstring byteString = hexString.substr(i, 2);
        uint8_t byte = static_cast<uint8_t>(std::stoi(byteString, nullptr, 16));
        bytes.push_back(byte);
    }


    return bytes;
}
```

[See sample code](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L171-L187)

Then, take that array and compute the SHA256 hash, which results in a 32-byte value, which is the export key. Now any number of snapshots can be decrypted using the resulting export key.

```cpp
    std::vector<uint8_t> exportKeyBytes(c_keySizeInBytes);
    THROW_IF_NTSTATUS_FAILED(BCryptHash(
        BCRYPT_SHA256_ALG_HANDLE,
        nullptr,
        0,
        exportCodeBytes.data(),
        static_cast<ULONG>(exportCodeBytes.size()),
        exportKeyBytes.data(),
        c_keySizeInBytes));
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L200-L208)

### Decrypt the encrypted snapshots

The layout of a snapshot (in little-endian format): `| uint32_t version | uint32_t encryptedKeySize | uint32_t encryptedContentSize | uint32_t contentType | uint8_t[KeySIze] encryptedContentKey | uint8_t[ContentSize] encryptedContent |`

First, read the four [uint32_t values](/cpp/c-runtime-library/standard-types#fixed-width-integral-types-stdinth).

```cpp
    EncryptedSnapshotHeader header{};
    reader.ByteOrder(winrt::ByteOrder::LittleEndian);


    header.Version = reader.ReadUInt32();
    header.KeySize = reader.ReadUInt32();
    header.ContentSize = reader.ReadUInt32();
    header.ContentType = reader.ReadUInt32();
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L222-L228)

Next, verify that version has the value, 2.

```cpp
    if (header.Version != 2)
    {
        throw std::runtime_error("Insufficient data header version.");
    }
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L230-L233)

Then, read the encryptedKeyContent.

```cpp
    std::vector<uint8_t> keybytes(header.KeySize);
    reader.ReadBytes(keybytes);
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L235-L236)

Decrypt the encryptedKeyContent

```cpp
wil::unique_bcrypt_key DecryptExportKey(BCRYPT_KEY_HANDLE key, std::span<uint8_t const> encryptedKey)
{
    THROW_HR_IF(E_INVALIDARG, encryptedKey.size() != c_totalSizeInBytes);


    BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO AuthInfo{};
    BCRYPT_INIT_AUTH_MODE_INFO(AuthInfo);
    AuthInfo.pbNonce = const_cast<uint8_t*>(encryptedKey.data()); 
    AuthInfo.cbNonce = c_nonceSizeInBytes;
    AuthInfo.pbTag = const_cast<uint8_t*>(encryptedKey.data() + c_nonceSizeInBytes + c_childKeySizeInBytes);
    AuthInfo.cbTag = c_tagSizeInBytes;


    uint8_t decryptedKey[c_childKeySizeInBytes] = { 0 };


    ULONG decryptedByteCount{};
    THROW_IF_FAILED(HResultFromBCryptStatus(BCryptDecrypt(
        key,
        const_cast<uint8_t*>(encryptedKey.data() + c_nonceSizeInBytes),
        c_childKeySizeInBytes,
        &AuthInfo,
        nullptr,
        0,
        decryptedKey,
        sizeof(decryptedKey),
        &decryptedByteCount,
        0)));


    wil::unique_bcrypt_key childKey;
    THROW_IF_NTSTATUS_FAILED(
        BCryptGenerateSymmetricKey(BCRYPT_AES_GCM_ALG_HANDLE, &childKey, nullptr, 0, decryptedKey, c_childKeySizeInBytes, 0));


    return childKey;
}
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L138-L169)

using the exportKey

```cpp
    wil::unique_bcrypt_key exportKey;
    THROW_IF_NTSTATUS_FAILED(BCryptGenerateSymmetricKey(
       BCRYPT_AES_GCM_ALG_HANDLE, &exportKey, nullptr, 0, exportKeyBytes.data(), static_cast<ULONG>(exportKeyBytes.size()), 0));
```

[See sample code](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L210-L212)

To get the contentKey (crypto algorithm is AES_GCM)

```cpp
    wil::unique_bcrypt_key contentKey = DecryptExportKey(exportKey.get(), keybytes);
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L237)

Read the encryptedContent

```cpp
    std::vector<uint8_t> contentbytes(header.ContentSize);
    reader.ReadBytes(contentbytes);
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L239-L240)

Decrypt the encryptedContent

```cpp
std::vector<uint8_t> DecryptPackedData(BCRYPT_KEY_HANDLE key, std::span<uint8_t const> payload)
{
    THROW_HR_IF(E_INVALIDARG, payload.size() < c_tagSizeInBytes);
    const auto dataSize = payload.size() - c_tagSizeInBytes;
    const auto data = payload.data();


    uint8_t zeroNonce[c_nonceSizeInBytes] = { 0 };
    BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO authInfo{};
    BCRYPT_INIT_AUTH_MODE_INFO(authInfo);
    authInfo.pbNonce = zeroNonce;
    authInfo.cbNonce = c_nonceSizeInBytes;
    authInfo.pbTag = const_cast<uint8_t*>(payload.data() + dataSize);
    authInfo.cbTag = c_tagSizeInBytes;


    std::vector<uint8_t> decryptedContent(dataSize);
    ULONG decryptedSize = 0;
    const auto result = BCryptDecrypt(
        key, const_cast<uint8_t*>(data), static_cast<ULONG>(dataSize), &authInfo, nullptr, 0, decryptedContent.data(), static_cast<ULONG>(dataSize), &decryptedSize, 0);
    decryptedContent.resize(decryptedSize);


    THROW_IF_FAILED(HResultFromBCryptStatus(result));


    return decryptedContent;
}
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L113-L136)

with the contentKey (crypto algorithm is AES_GCM)

```cpp
    std::vector<uint8_t> decryptedContent = DecryptPackedData(contentKey.get(), contentbytes);
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L241)

Output decrypted Recall snapshot content in the form of a .jpg image with corresponding .json metadata into the designated folder path

```cpp
void WriteSnapshotToOutputFolder(winrt::StorageFolder const& outputFolder, winrt::hstring const& fileName, winrt::IRandomAccessStream const& decryptedStream)
```

[See sample code.](https://github.com/microsoft/RecallSnapshotsExport/blob/5442b3a90aed91888a67c283c3132290ecddb044/RecallSnapshotsExport.cpp#L251)

The expected output will include:

- Decrypted snapshots saved as .jpg files.
- Corresponding metadata saved as .json files.

Both file types will share the same filename and be found in the specified output folder.  

## Learn more about Recall

- [Learn more about Windows Recall](index.md).
- [Export Recall snapshots with an export code](https://support.microsoft.com/topic/680bd134-4aaa-4bf5-8548-a8e2911c8069)
- [Manage Recall: Allow export of Recall and snapshot information](/windows/client-management/manage-recall#allow-export-of-recall-and-snapshot-information): IT Administrator guidance on how to manage Recall settings for users within your company, including the ability to export Recall snapshot data.
- [Configuration Service Provider (CSP) Policy for Windows AI: Allow Recall Export](/windows/client-management/mdm/policy-csp-windowsai#allowrecallexport): Guidance for IT administrators to establish the policy settings that determine whether the optional Recall feature is available for end users to enable on their device, including the policy for enabling the export of snapshot data.
