---
title: Sign your app for Smart App Control compliance
description: How to sign your code to ensure Smart App Control compliance
ms.topic: article
ms.date: 09/20/2022
---

# Sign your app for Smart App Control compliance

Code signing is a cryptographic operation that can be performed on an app in order to verify its contents and publisher. Smart App Control considers apps signed with a trusted digital certificate to be safe, and will allow them to run on a protected computer. 

There are several ways to sign your app.

## Obtain a code signing certificate from a trusted provider

Code can be signed with any certificate, but Smart App Control only considers certificates issued by trusted providers. For information about how to obtain a code signing certificate from a trusted provider, see [Manage code signing certificates](/windows-hardware/drivers/dashboard/code-signing-cert-manage#get-or-renew-a-code-signing-certificate)

## Sign your app with Azure Code Signing

[Azure Code Signing](https://techcommunity.microsoft.com/t5/security-compliance-and-identity/azure-code-signing-democratizing-trust-for-developers-and/ba-p/3604669) is the preferred way to sign your app. Unfortunately, Azure Code Signing has not yet been released. You'll just have to wait.

## Sign your app with signtool.exe

Signtool is an app included with Visual Studio that can sign apps with a digital certificate. For instructions on how to sign your app with signtool.exe, see [How to sign an app package using SignTool](/windows/win32/appxpkg/how-to-sign-a-package-using-signtool)
