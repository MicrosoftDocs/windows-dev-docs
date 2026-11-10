---
title: Fix Microsoft Store submission and certification failures
description: Find the cause of a failed Microsoft Store submission by where it stopped, from local testing through package upload and certification, and learn how to fix it.
ms.date: 11/10/2026
ms.topic: troubleshooting-general
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Fix Microsoft Store submission and certification failures

A Microsoft Store submission can stop at three points: when you test the package locally, when Partner Center validates your upload, or during certification. This article is organized by those points, so start with the section that matches where your submission stopped.

For detailed error lists, see [Resolve submission errors](publish-your-app/msix/resolve-submission-errors.md) and [The app certification process](publish-your-app/msix/app-certification-process.md). To avoid failures before you submit, use the [Microsoft Store readiness checklist](store-readiness-checklist.md).

## Local testing and the Windows App Certification Kit

| Symptom | Likely cause | Fix |
|---|---|---|
| The package doesn't install on a test device. | The package is signed with a certificate the device doesn't trust. | Install the development certificate on the test device, or see [Code signing options for Windows app developers](../package-and-deploy/code-signing-options.md). |
| The app installs but closes immediately. | A runtime dependency is missing, such as the Windows App SDK runtime. | Declare and deploy runtime dependencies for your app's deployment model. For framework-dependent Windows App SDK apps, see [Deploy packaged apps](../windows-app-sdk/deploy-packaged-apps.md), including its Visual C++ runtime prerequisites. |
| The Windows App Certification Kit reports failures. | Varies by test. | Fix each failure. The same issues usually fail certification. |

## Package upload in Partner Center

| Symptom | Likely cause | Fix |
|---|---|---|
| Identity or publisher error on upload. | The `Name` or `Publisher` in your manifest doesn't match Partner Center. | Copy the values from **Product identity**. See [View app identity details](view-app-identity-details.md). |
| "The name found in the package is not one of your reserved app names." | The display name in the manifest isn't a name you reserved. | Use a reserved name, or [reserve the name](publish-your-app/msix/reserve-your-apps-name.md). |
| "Your submission contains a package that has the exact same package full name as another package you provided." | You rebuilt the package with new contents but kept the same version number. | Increase the package version, rebuild, and upload the new package. |
| Version error. | The version format or package combination doesn't meet the Store's version rules. | Read the validation message and follow [Package version numbering](publish-your-app/msix/app-package-requirements.md#package-version-numbering). Check the version format, architecture, and device-target rules before changing the version and rebuilding. |
| Image or asset error. | A file isn't a real PNG, or it doesn't meet the size requirements. | See [App screenshots, images, and trailers](publish-your-app/msix/screenshots-and-images.md). |

## Certification

| Symptom | Likely cause | Fix |
|---|---|---|
| Policy failure in the certification report. | The app or listing doesn't meet a [Microsoft Store Policy](store-policies.md). | Read the policy number in the report, fix the app or listing, and resubmit. |
| Crash or hang reported. | The app failed on the test device. | Test on a clean device and fix the issue. |
| Privacy policy failure (policy 10.5.1). | No privacy policy URL was provided. Win32 and Desktop Bridge apps, including packaged desktop apps built with WinUI 3, WPF, or Windows Forms, always need one. | Add a privacy policy URL on the **Properties** page of your submission, then resubmit. See [Microsoft Store Policies](store-policies.md). |
| Listing content failure. | Screenshots, description, or privacy policy don't meet requirements. | Update the listing. You don't always need a new package for listing-only fixes. |
| App crashes or stops responding without a network connection. | The app assumes it's always online. | Handle no-connection cases with a clear message, even if the app needs a connection to work. Test with networking turned off. |
| Testers couldn't reach features behind a sign-in. | No test account was provided. | Add a test account and any steps needed to reach locked features in [Notes for certification](publish-your-app/msix/manage-submission-options.md#notes-for-certification). |
| App appears incomplete. | Placeholder content, unfinished sections, or links to pages under construction. | Submit only when the app is finished. You can mention upcoming features in your description. |

After you fix an issue, rebuild and retest the package. For updates, increase the version of the package being replaced, following the package-version rules.

## For AI agents

An agent can match a symptom or error message to a row in this article, inspect the manifest, and propose fixes. If an error isn't listed here, check [Resolve submission errors](publish-your-app/msix/resolve-submission-errors.md) for the exact message. A person must review the fix, make Partner Center changes, and approve the resubmission.

## Next steps

- [Create an app submission](publish-your-app/msix/create-app-submission.md)
- [Publish an update to your app](publish-your-app/msix/publish-update-to-your-app-on-store.md)
