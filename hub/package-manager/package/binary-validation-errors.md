---
title: Binary validation errors
description: Learn about binary validation errors that the Windows Package Manager service reports, and how to address them.
ms.date: 06/22/2022
ms.topic: troubleshooting
ms.localizationpriority: medium
ms.custom: kr2b-contr-experiment
---

# Binary validation errors

This article provides background and guidance about binary validation errors. If [pull request validation](winget-validation.md) fails the **Installers Scan** test with a **Binary-Validation-Error** label, it means that your application failed to install on all environments.

## Understand the Installers Scan test

To provide an excellent application installation user experience, the Windows Package Manager must ensure that all applications install on PCs without errors, regardless of environment. One key test is to ensure that all applications install without warnings on various popular antivirus configurations. Windows provides the built-in Microsoft Defender antivirus program, but many enterprise customers and users use other antivirus software.

Each submission to the Windows Package Manager Repository is run through several antivirus programs. These programs all have different virus detection algorithms for identifying [potentially unwanted applications (PUA)](/windows/security/threat-protection/intelligence/criteria) and malware.

## Address binary validation errors

If an application fails validation, Microsoft first attempts to verify with the antivirus vendor whether the flagged software is a false positive. In many cases, after notification and validation, the antivirus vendor updates their algorithm, and the application passes.

In some cases, the antivirus vendor can't determine whether the detected code anomaly is a false positive. In this case, the application can't be added to the Windows Package Manager repository. The pull request is rejected with a **Binary-Validation-Error** label.

If you get a **Binary-Validation-Error** label on your pull request, update your software to remove the code detected as PUA.

Sometimes, genuine tools used for debugging and low-level activities appear as PUA to antivirus software. This is because the necessary debugging code has a similar signature to unwanted software. Even though this coding practice is legitimate, the Windows Package Manager repository unfortunately can't allow these applications.
