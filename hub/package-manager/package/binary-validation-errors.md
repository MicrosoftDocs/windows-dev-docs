---
title: Binary validation errors
description: Provides additional information about binary validation errors reported by the Windows Package Manager service.
ms.date: 05/25/2021
ms.topic: overview
ms.localizationpriority: medium
---

# Binary validation errors

If your [pull request fails](winget-validation.md) to pass the **Installers Scan** test and is given the **Binary-Validation-Error** label, this indicates that your application failed to install on all environments. This article provides more background and guidance about this error.

## Understanding the Installers Scan test

Windows Package Manager goes to great lengths to create an excellent user experience when installing applications. In order to do this, we must ensure that all applications install on PCs without errors regardless of environment.

To that end, a key test we use for the Windows Package Manager is to ensure that all installers will install without warnings on a variety of popular antivirus configurations. While Windows provides Microsoft Defender as a built-in antivirus program, many enterprise customers and users employ a wide range of antivirus software.

Therefore, each submission to the Windows Package Manager will be run through several antivirus programs. These programs all have different virus detection algorithms for identifying [Potentially unwanted application (PUA)](/windows/security/threat-protection/intelligence/criteria) and malware.  

If an application fails validation, Microsoft will first attempt to verify that the flagged software is not a false positive with the antivirus vendors. In many cases, after notification and validation, the antivirus vendor will update their algorithm and the application will pass.

## What to do if you see the Binary-Validation-Error label

In some cases, the code anomaly detected is not able to be determined to be a false positive by the antivirus vendors. In this case the application cannot be added to the Windows Package Manager repository, and the pull request will be rejected with a **Binary-Validation-Error** label.

If the **Binary-Validation-Error** label is applied to your pull request, update your software to remove the code detected as PUA.

## What if I cannot remove that code?

Occasionally, genuine tools used for debugging and low-level activities will appear as PUA to the antivirus vendors. This is because the code necessary to do the debugging will have a similar signature to unwanted software. Even though this is a legitimate use of that coding practice, unfortunately we are unable to allow those applications into the Windows Package Manager repository.
