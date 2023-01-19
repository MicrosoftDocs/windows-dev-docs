---
description: An overview of Windows package identity and how it uniquely identifies a package.
title: An overview of Package Identity in Windows apps
ms.date: 01/10/2023
ms.topic: article
keywords: windows 10, windows 11, desktop, package, identity, MSIX, Win32
ms.localizationpriority: medium
ms.custom: RS5
---

# An overview of Package Identity in Windows apps

**Package identity** is a unique identifier across space and time. Just as your DNA uniquely identifies you, package identity uniquely identifies a package.

A package has an associated set of bits (files etc). No two packages have the same identity, and any changes to the bits associated with a package requires a different identity.

## What is package identity?

A [package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) is a logical construct, uniquely identifying a package. The identity has 5 parts:

- **Name:** This is a name chosen by the app developer. The Microsoft Store enforces uniqueness of all app names across all app developers within the Store, but names are not guaranteed to be unique in the general ecosystem.
- **Version:** Version number of the package. The app developer can choose arbitrary version numbers but must ensure version numbers increase with updates.
- **Architecture:** The processor architecture being targeted by the package. The same app can be built targeting different processor architectures, with each build residing in its own package.
- **ResourceId:** A string chosen by the app developer to uniquely identify resource packages, for example different languages or different display scales. Resource packages are typically architecture-neutral. For bundles, the **ResourceId** is always `~`.
- **Publisher:** The app developer's subject name as identified by their signing certificate. This is theoretically unique for each app developer, because reputable certification authorities use unique real-world names and identities to populate the certificate's subject name field.

This construct is sometimes referred to as the _5-part tuple_.

> [!NOTE]
> [Unsigned packages](/windows/msix/package/unsigned-package) (1) still require a **Publisher**, (2) the **Publisher** must contain the Unsigned marker (OID.2.25.311729368913984317654407730594956997722=1), (3) the Unsigned marker _must_ be the last field in the **Publisher** string, and (4) there is no certificate or signing for an Unsigned package.

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

### Package Full Name

A **Package Full Name** is an opaque string derived from all 5 part of a package's identity (name, version, architecture, resourceid, publisher)

`<Name>_<Version>_<Architecture>_<ResourceId>_<PublisherId>`

For example, one package full name for the Windows Photos app is "Microsoft.Windows.Photos_2020.20090.1002.0_x64__8wekyb3d8bbwe", where "Microsoft.Windows.Photos" is the name, "2020.20090.1002.0" is the version number, "x64" is the target processor architecture, the resource ID is empty (no content between the last two underscores), and "8wekyb3d8bbwe" is the publisher ID for Microsoft.

The **Package Full Name** uniquely identifies an MSIX package or bundle. It is an error to have two packages or bundles with different contents but with the same Package Full Name.

> [!NOTE]
> MSIX is the new name for the previous term APPX.

### Package Family Name

A **Package Family Name** is an opaque string derived from only two parts of a package identity - _name_ and _publisher_:

`<Name>_<PublisherId>`

For example, the Package Family Name of the Windows Photos app is "Microsoft.Windows.Photos_8wekyb3d8bbwe", where "Microsoft.Windows.Photos" is the name and "8wekyb3d8bbwe" is the publisher ID for Microsoft.

Package Family Name is often referred to as a 'version-less Package Full Name'.

> [!NOTE]
> This isn't strictly true, as Package Family Name also lacks architecture and Resource ID.

> [!NOTE]
> Data and security are typically scoped to a package family. For example, it would be a poor experience if you configured the Notepad app installed from a Notepad version 1.0.0.0 package to enable Wordwrap. Then Notepad was subsequently updated to 1.0.0.1 and your configuration data wasn't carried over to the newer version of the package.

### Publisher Id

A Package Family Name is a string with the format:

`<name>_<publisherid>`

where Publisher Id has some very specific properties:

- Derived from Publisher
- MinLength = MaxLength = 13 chars [fixed-size]
- Allowed Characters (as regex) = a-hj-km-np-tv-z0-9
  - Base-32, Crockford Variant, i.e. alphanumeric (A-Z0-9) except no I (eye), L (ell), O (oh) or U (you)
- Case-insensitive for comparisons --- ABCDEFABCDEFG == abcdefabcdefg

So you’ll never see % : \ / " ? or other characters in a Publisher Id.

See [PackageFamilyNameFromId](/windows/win32/api/appmodel/nf-appmodel-packagefamilynamefromid) and [PackageNameAndPublisherIdFromFamilyName](/windows/win32/api/appmodel/nf-appmodel-packagenameandpublisheridfromfamilyname) for more details.

Publisher Id is often referred to as PublisherId.

#### Why does Publisher Id exist?

Publisher Id exists because Publisher needs to match your cert’s X.509 name/signer, thus:

- It can be very big (length <= 8192 chars) – difficult to use in file system, registry, URLs, etc.
- It can include restricted characters (backslash etc.)

#### How do I create a PublisherId?

Use [PackageNameAndPublisherIdFromFamilyName](/windows/win32/api/appmodel/nf-appmodel-packagenameandpublisheridfromfamilyname) to extract the `PublisherId` from a `PackageFamilyName`.

Use [PackageIdFromFullName](/windows/win32/api/appmodel/nf-appmodel-packageidfromfullname) to extract the `PublisherId` from a `PackageFullName`.

It's rare to need to create a `PublisherId` from `Publisher`, but it can be done with the use of available APIs:

```cpp
#include <appmodel.h>

HRESULT PublisherIdFromPublisher(
    _In_ PCWSTR publisher,
    _Out_writes_(PACKAGE_PUBLISHERID_MAX_LENGTH) PWSTR publisherId)
{
    const PCWSTR name{ L"xyz" };
    const size_t nameLength{ ARRAYSIZE(L"xyz") - 1 };
    const size_t offsetToPublisherId{ name + 1 }; // xyz_...publisherid...
    PACKAGE_ID id{};
    id.name = name;
    id.publisher = publisher;
 
    WCHAR familyName[PACKAGE_PUBLISHERID_MAX_LENGTH + 1]{};
    UINT32 n{ ARRAYSIZE(familyName) };
    RETURN_IF_WIN32_ERROR(PackageFamilyNameFromId(&id, &n, familyName);
    RETURN_IF_FAILED(StringCchCopyW(publisherId, PACKAGE_PUBLISHERID_MAX_LENGTH + 1, familyName + offsetToPublisherId));
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
    CopyMemory(publisherId,
        familyName + NAME_FOR_PUBLISHER_TO_PUBLISHERID_LENGTH + 1,
        (n - NAME_FOR_PUBLISHER_TO_PUBLISHERID_LENGTH - 1) * sizeof(*publisherId));
    return ERROR_SUCCESS;
}
```

This creates the PublisherId by converting a Package Id to a Package Family Name with the resulting format `xyz_<publisherid>`. This recipe is stable and reliable.

This only requires you compile with appmodel.h from the SDK and link with kernel32.lib (or api-ms-win-appmodel-runtime-l1.lib if using APIsets).

### Understanding processor architecture in package identity

A common misunderstanding is that `Architecture=x64` means the package can only contain x64 code. This is not true. It means that the package works on systems supporting x64 code, and can be used by x64 apps. You could make a package containing only PDF files but declare it with `<Identity Architecture=x64...>` because it is only intended to be installed on x64-compatible systems (e.g. x64 packages can only be installed on x64 and (as of Windows 11) Arm64 systems, because x86, Arm, and Windows 10 Arm64 systems don't support x64).

Even more misunderstood, `Architecture=neutral` does _not_ mean the package contains no executable code. It means the package works on all architectures. For example, you could make a package containing an AES encryption API written in JavaScript, Python, C#, etc. but the performance is not acceptable on Arm64 systems. So, you include an optimized Arm64 binary and implement the API to handle it:

``` cpp
void Encrypt(...)
{
    HANDLE h{};
    if (GetCpu() == arm64)
        h = LoadLibrary(GetCurrentPackagePath() + "\bin\encrypt-arm64.dll")
        p = GetProcAddress(h, "Encrypt")
        return (*p)(...)
    else
        // ...call other implementation...
}
```

Or you can make a neutral package with multiple variants:

```YAML
\
    bin\
        encrypt-x86.dll
        encrypt-x64.dll
        encrypt-arm.dll
        encrypt-arm64.dll
```

Developers can then `LoadLibrary("bin\encrypt-" + cpu + ".dll")` to get the appropriate binary for their process at runtime.

Typically, neutral packages have no per-architecture content, but they can. There are limits to what one can do (e.g. you could make a Notepad package containing notepad.exe compiled for x86 + x64 + arm + arm64 but **appxmanifest.xml** can only declare `<Application Executable=...>` pointing to one of them). Given that there are bundles that let you install only the needed bits, this is a very uncommon thing to do. It's not illegal, just advanced.

Also, `Architecture=x86` (or x64|arm|arm64) doesn't mean the package only contains executable code for the specified architecture. It's just the overwhelmingly common case.

> [!NOTE]
> When discussing "code" or "executable code" in this context, we are referring to portable executable (PE) files.

### Is package identity case sensitive?

Mostly, no, but `Publisher` is case-sensitive.

The remaining fields (`Name`, `ResourceId`, `PublisherId`, `PackageFullName` and `PackageFamilyName`) are not. These fields will preserve case but compare case-insensitively.

## See also

[Package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity)

[PackageFamilyNameFromId](/windows/win32/api/appmodel/nf-appmodel-packagefamilynamefromid)

[PackageNameAndPublisherIdFromFamilyName](/windows/win32/api/appmodel/nf-appmodel-packagenameandpublisheridfromfamilyname)
