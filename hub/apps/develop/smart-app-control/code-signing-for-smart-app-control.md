---
title: Sign your app for Smart App Control compliance
description: Learn how to sign your code to ensure Smart App Control compliance using one of several supported methods.
ms.topic: concept-article
ms.date: 09/22/2026
# customer intent: As a Windows developer, I want to learn how to sign my code to ensure Smart App Control compliance.
---

# Sign your app for Smart App Control compliance

Code signing is a cryptographic operation that can be performed on an app in order to verify its contents and publisher. Smart App Control allows applications signed with trusted digital certificates to run on protected devices. Smart App Control supports both RSA and elliptic curve cryptography (ECC) code signing certificates. Developers should ensure their applications are signed with a valid code signing certificate issued by a trusted provider.

There are several ways to sign your app.

## Obtain a code signing certificate from a trusted provider

Code can be signed with any certificate, but Smart App Control only considers certificates issued by trusted providers. For information about how to obtain a code signing certificate from a trusted provider, see [Manage code signing certificates](/windows-hardware/drivers/dashboard/code-signing-cert-manage#get-or-renew-a-code-signing-certificate).

## Sign your app with Artifact Signing

[Artifact Signing](/azure/artifact-signing/) is the preferred way to sign your app. Artifact Signing is now Generally Available (GA) to customers.

## Sign your app with signtool.exe

Signtool.exe is an app included with Visual Studio that can sign apps with a digital certificate. For instructions on how to sign your app with signtool.exe, see [How to sign an app package using SignTool](/windows/win32/appxpkg/how-to-sign-a-package-using-signtool).

## Related content

- [Manage code signing certificates](/windows-hardware/drivers/dashboard/code-signing-cert-manage#get-or-renew-a-code-signing-certificate)
- [Artifact Signing](/azure/artifact-signing/)

- [SignTool](/windows/win32/seccrypto/signtool)
