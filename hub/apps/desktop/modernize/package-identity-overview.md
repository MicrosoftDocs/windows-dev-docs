---
description: An overview of Windows package identity and how it uniquely identifies a package.
title: Package identity overview
ms.date: 01/10/2023
ms.topic: article
keywords: windows 10, windows 11, desktop, package, identity, MSIX, Win32
ms.localizationpriority: medium
ms.custom: RS5
---

# Package identity overview

**Package identity** is a unique identifier across Windows. Just as your DNA uniquely identifies you, package identity uniquely identifies a package. No two packages with different bits have the same identity.

## What is package identity?

A [package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) is a logical construct, uniquely identifying a package. The identity has 5 parts:

- **Name:** This is an arbitrary name chosen by the app developer. The Microsoft Store enforces uniqueness of all app names across all app developers within the Store, but names are not guaranteed to be unique in the general ecosystem.
- **Version:** Version number of the package. The app developer can choose arbitrary version numbers but must ensure version numbers increase with updates.
- **Architecture:** The processor architecture being targeted by the package. The same app can be built targeting different processor architectures, with each build residing in its own package.
- **ResourceId:** Arbitrary string chosen by the app developer to uniquely identify resource packages, for example different languages or different display scales. All resource packages are architecture-neutral. For bundles, the **ResourceId** is always "~".
- **Publisher:** The app developer's subject name as identified by their signing certificate. This is theoretically unique for each app developer, because reputable certification authorities use unique real-world names and identities to populate the certificate's subject name field.

This construct is sometimes referred to as the _5-part tuple_.

### Package identity fields limits

| Field | Data type | Limits | Comments |
|--------|-----------|--------|----------|
| Name | Package String | Min: 3<br/>Max: 50 | Allowed values per Validate API (see [Package String](#what-is-a-package-string)) |
| Version | DotQuad | Min: 0.0.0.0<br/>Max: 65535.65535.65535.65535 | String form uses base-10 dotted notation, "Major.Minor.Build.Revision" |
| Architecture | Enumeration | Min: N/A<br/>Max: N/A | Allowed values are "neutral", "x86", "x64", "arm", "arm64", "x86a64" |
| ResourceId | Package String | Min: 0<br/>Max: 30 | Allowed values per Validate API (see [Package String](#what-is-a-package-string)) |
| Publisher | String | Min: 1<br/>Max: 8192 | Allowed values per X.509 |
| PublisherId | String | Min: 13<br/>Max: 13 | [Base32 encoded, Crockford variant](http://en.wikipedia.org/wiki/Base32#Crockford.27s_Base32), i.e. [a-hjkmnp-tv-z0-9] |

### What is a ‘Package String’?

A **package string** is a string that allows the following characters:

- Allowed Input Characters (ASCII subset)
  - Uppercase letters (U+0041 thru U+005A)
  - Lowercase letters (U+0061 thru U+007A)
  - Numbers (U+0030 thru U+0039)
  - Dot (U+002E)
  - Dash (U+002D)

The following values are prohibited from being used as package strings:

| Condition | Prohibited values |
|--------|--------|
| Cannot equal | ".", "..", "con", "prn", "aux", "nul", "com1", "com2", "com3", "com4", "com5", "com6", "com7", "com8", "com9", "lpt1", "lpt2", "lpt3", "lpt4", "lpt5", "lpt6", "lpt7", "lpt8", "lpt9" |
| Cannot begin with | "con.", "prn.", "aux.", "nul.", "com1.", "com2.", "com3.", "com4.", "com5.", "com6.", "com7.", "com8.", "com9.", "lpt1.", "lpt2.", "lpt3.", "lpt4.", "lpt5.", "lpt6.", "lpt7.", "lpt8.", "lpt9.", "xn--" |
| Cannot end with | "." |
| Cannot contain | ".xn--" |

A **package string** must be compared using a case-insensitive string comparison APIs (e.g. _wcsicmp).

Package Identity’s `name` and `resourceid` fields are package strings.

### PackageId object

A [PackageId](/uwp/api/windows.applicationmodel.packageid) is an object containing the 5-part tuple as individual fields (`Name`, `Version`, `Architecture`, `ResourceId`, `Publisher`).

### Package full name

A **package full name** is an opaque string derived from all 5 part of a package's identity (name, version, architecture, resourceid, publisher)

`<Name>_<Version>_<Architecture>_<ResourceId>_<PublisherId>`

For example, one package full name for the Windows Photos app is "Microsoft.Windows.Photos_2020.20090.1002.0_x64__8wekyb3d8bbwe", where "Microsoft.Windows.Photos" is the name, "2020.20090.1002.0" is the version number, "x64" is the target processor architecture, the resource ID is empty (no content between the last two underscores), and "8wekyb3d8bbwe" is the publisher ID for Microsoft.

The **package full name** uniquely identifies an APPX/MSIX package or bundle. It is an error to have two packages or bundles with different contents but with the same package full name.

Package full name should be abbreviated "PFuN", to distinguish it from **package family name**.

### Package family name

A **package family name** is an opaque string derived from only two parts of a package identity - _name_ and _publisher_:

`<Name>_<PublisherId>`

For example, the package family name of the Windows Photos app is "Microsoft.Windows.Photos_8wekyb3d8bbwe", where "Microsoft.Windows.Photos" is the name and "8wekyb3d8bbwe" is the publisher ID for Microsoft.

Package family name is often referred to as a 'version-less package full name'.

>[!NOTE]
> This isn't strictly true, as package family name also lacks architecture and resource ID.

Package family name should be abbreviated "PFaN", to distinguish it from package full name.

### Publisher Id

A package family name is a string with the format:

`<name>_<publisherid>`

where Publisher Id has some very specific properties:

- Derived from Publisher
- MinLength = MaxLength = 13 chars [fixed-size]
- Allowed Characters (as regex) = a-hj-km-np-tv-z0-9
  - Base-32, Crockford Variant, i.e. alphanumeric (A-Z0-9) except no I (eye), L (ell), O (oh) or U (you)
- Case-insensitive for comparisons --- ABCDEFABCDEFG == abcdefabcdefg

So you’ll never see % : \ / " ? or other characters in a Publisher Id.

See the ARI Dev Design spec, section 2.1.5 Conversion APIs for more details.

Publisher Id is often referred to as PublisherId.

#### Why does Publisher Id exist?

Publisher Id exists because Publisher needs to match your cert’s X.509 name/signer, thus:

- It can be very big (length <= 8192 chars) – problematic to use in file system, registry, URLs, etc.
- It can include problematic characters (backslash etc.)

#### How do I create a PublisherId?

Use `PackageFamilyNameAndPuiblisherIdFromFamilyName` to extract the `PublisherId` from a `PackageFamilyName`.

Use `PackageIdFromFullName` to extract the `PublisherId` from a `PackageFullName`.

It's rare to need to create a `PublisherId` from `Publisher`, but it can be done with the use of available APIs:

```cpp
#include <appmodel.h>

HRESULT PublisherIdFromPublisher(
    _In_ PCWSTR publisher,
    _Out_writes_(PACKAGE_PUBLISHERID_MAX_LENGTH) PWSTR publisherId)
{
    const PCWSTR name = L"xyz";
    const size_t nameLength = 3;
    const size_t offsetToPublisherId = name + 1; // xyz_...publisherid...

    PACKAGE_ID id = {};
    id.name = name;
    id.publisher = publisher;
 
    WCHAR familyName[PACKAGE_PUBLISHERID_MAX_LENGTH + 1] = {};
    UINT32 n = ARRAYSIZE(familyName);
    RETURN_IF_WIN32_ERROR(PackageFamilyNameFromId(&id, &n, familyName);
    RETURN_IF_FAILED(StringCchCopyW(publisherId, PACKAGE_PUBLISHERID_MAX_LENGTH + 1, familyName + offsetToPublisherId )
    return S_OK;
}
```

The following is a classic Windows C implementation of the same operation:

```c
#include <appmodel.h>

#define NAME_FOR_PUBLISHER_TO_PUBLISHERID L"xyz"
#define NAME_FOR_PUBLISHER_TO_PUBLISHERID_LENGTH (ARRAYSIZE(NAME_FOR_PUBLISHER_TO_PUBLISHERID) - 1)
_Check_return_ _Success_(return == ERROR_SUCCESS) LONG PublisherIdFromPublisher(
    _In_ PCWSTR publisher,
    _Out_writes_(PACKAGE_PUBLISHERID_MAX_LENGTH) PWSTR publisherId)
{
    PACKAGE_ID id;
    ZeroMemory(&id, sizeof(id));
    C_ASSERT(NAME_FOR_PUBLISHER_TO_PUBLISHERID_LENGTH == PACKAGE_NAME_MIN_LENGTH);
    id.name = NAME_FOR_PUBLISHER_TO_PUBLISHERID;
    id.publisher = publisher;

    WCHAR familyName[PACKAGE_PUBLISHERID_MAX_LENGTH + 1];
    UINT32 n = ARRAYSIZE(familyName);
    LONG rc = PackageFamilyNameFromId(&id, &n, familyName);
    if (rc != ERROR_SUCCESS)
        return rc;
    NT_ASSERT(n == NAME_FOR_PUBLISHER_TO_PUBLISHERID_LENGTH + 1 + PACKAGE_PUBLISHERID_MAX_LENGTH + 1);
    NT_ASSERT(familyName[0] == NAME_FOR_PUBLISHER_TO_PUBLISHERID[0]);
    NT_ASSERT(familyName[1] == NAME_FOR_PUBLISHER_TO_PUBLISHERID[1]);
    NT_ASSERT(familyName[2] == NAME_FOR_PUBLISHER_TO_PUBLISHERID[2]);
    NT_ASSERT(familyName[3] == L'_');
    CopyMemory(publisherId,
        familyName + NAME_FOR_PUBLISHER_TO_PUBLISHERID_LENGTH + 1,
        (n - NAME_FOR_PUBLISHER_TO_PUBLISHERID_LENGTH - 1) * sizeof(*publisherId));
    return ERROR_SUCCESS;
}
```

This creates the PublisherId by converting a Package Id to a Package Family Name with the resulting format `xyz_<publisherid>`. This recipe is stable and reliable, and was used in various places across Windows before the internal PackageIdFromPublisher() was available.

This only requires you compile with appmodel.h from the SDK and link with kernel32.lib (or api-ms-win-appmodel-runtime-l1.lib if using APIsets).

### Is package identity case sensitive?

Mostly, no, but `Publisher` is case-sensitive.

The remaining fields (`Name`, `ResourceId`, `PublisherId`, `PackageFullName` and `PackageFamilyName`) are not. These fields will preserve case but compare case-insensitively.
