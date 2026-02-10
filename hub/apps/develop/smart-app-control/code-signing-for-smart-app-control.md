---
title: Sign your app for Smart App Control compliance
description: Learn how to sign your code to ensure Smart App Control compliance using one of several supported methods.
ms.topic: concept-article
ms.date: 09/20/2022
# customer intent: As a Windows developer, I want to learn how to sign my code to ensure Smart App Control compliance.
---

# Sign your app for Smart App Control compliance

Code signing is a cryptographic operation that can be performed on an app in order to verify its contents and publisher.Smart App Control allows applications signed with RSA-based digital certificates to run on protected devices. It does not currently support elliptic-curve cryptography (ECC).

> [!NOTE]
> Smart App Control's signature check does not currently support Elliptic-curve cryptography (ECC) signatures. Please use ensure all applications are signed with RSA-based digital certificates.

There are several ways to sign your app.

## Obtain a code signing certificate from a trusted provider

Code can be signed with any certificate, but Smart App Control only considers certificates issued by trusted providers. For information about how to obtain a code signing certificate from a trusted provider, see [Manage code signing certificates](/windows-hardware/drivers/dashboard/code-signing-cert-manage#get-or-renew-a-code-signing-certificate).

## Sign your app with Trusted Signing

[Trusted Signing](https://techcommunity.microsoft.com/t5/security-compliance-and-identity/trusted-signing-is-in-public-preview/ba-p/4103457) (formerly Azure Code Signing) is the preferred way to sign your app. Trusted Signing is currently in public preview.

## Sign your app with signtool.exe

Signtool.exe is an app included with Visual Studio that can sign apps with a digital certificate. For instructions on how to sign your app with signtool.exe, see [How to sign an app package using SignTool](/windows/win32/appxpkg/how-to-sign-a-package-using-signtool).

## Related content

- [Manage code signing certificates](/windows-hardware/drivers/dashboard/code-signing-cert-manage#get-or-renew-a-code-signing-certificate)
- [Trusted Signing](https://techcommunity.microsoft.com/t5/security-compliance-and-identity/trusted-signing-is-in-public-preview/ba-p/4103457)
- [SignTool](/windows/win32/seccrypto/signtool)
