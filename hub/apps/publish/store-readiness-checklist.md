---
title: Microsoft Store readiness checklist for MSIX apps
description: Check your MSIX package, Partner Center setup, and Store listing before you submit, so you avoid common upload and certification failures.
ms.date: 11/10/2026
ms.topic: concept-article
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Microsoft Store readiness checklist for MSIX apps

Use this checklist before you upload an MSIX package to Partner Center. A submission can stop at three different points: when you test the package locally, when Partner Center validates the upload, or during certification. Each failure means another round of fixes and resubmission, so catch problems before you submit.

This checklist summarizes common requirements and links to the articles that define them. It applies to MSIX packages. For MSI or EXE installers, see [App package requirements for MSI/EXE apps](publish-your-app/msi/app-package-requirements.md).

## Account and app reservation

> [!div class="checklist"]
> * Your developer account is created and verification is complete. See [Set up your Microsoft Store developer account early](prepare-developer-account.md).
> * You've [reserved your app's name](publish-your-app/msix/reserve-your-apps-name.md) in Partner Center.
> * If you plan to publish updates from the command line, your Partner Center account has an associated Microsoft Entra ID tenant. See the [Microsoft Store Developer CLI prerequisites](msstore-dev-cli/overview.md#prerequisites).

## Package identity and version

> [!div class="checklist"]
> * The package manifest's `Identity/Name`, `Identity/Publisher`, and `Properties/PublisherDisplayName` values match your app's **Product identity** page in Partner Center. See [View app identity details](view-app-identity-details.md).
> * The display name in your manifest matches a name you reserved for the app.
> * The package version uses four parts (Major.Minor.Build.Revision) and follows the version rules in [App package requirements](publish-your-app/msix/app-package-requirements.md). For an update, the version is higher than the installed package it replaces; same-version packages for different architectures and lower-version packages for additional device targets are also supported.
> * The package is built in the Release configuration, not Debug.

## Signing

> [!div class="checklist"]
> * You don't need your own code-signing certificate for Store distribution, because the Store signs your package. A local development certificate is fine for installing and testing, but don't distribute packages signed with it outside the Store. For other channels, see [Code signing options for Windows app developers](../package-and-deploy/code-signing-options.md).

## Dependencies and capabilities

> [!div class="checklist"]
> * Runtime dependencies are declared and deployed as required by your app's deployment model. For framework-dependent Windows App SDK apps, see [Deploy packaged apps](../windows-app-sdk/deploy-packaged-apps.md), including its Visual C++ runtime prerequisites.
> * The manifest declares only the [capabilities](../package-and-deploy/app-capability-declarations.md) your app uses. If you declare a restricted capability, be ready to explain why your app needs it when you submit.

## Store listing

> [!div class="checklist"]
> * Store logos and screenshots are real PNG files (not other formats renamed to .png) and meet the requirements in [App screenshots, images, and trailers](publish-your-app/msix/screenshots-and-images.md).
> * Screenshots show the current version of your app.
> * Your description and features are complete. See [Add and edit Store listing info](publish-your-app/msix/add-and-edit-store-listing-info.md).
> * You have a privacy policy URL. Win32 and Desktop Bridge apps, including packaged desktop apps built with WinUI 3, WPF, or Windows Forms, always need one, even if they don't collect personal information. Other apps need one if they access, collect, or transmit personal information, or when required by law. See [Microsoft Store Policies](store-policies.md).
> * You're ready to complete the [age ratings](publish-your-app/msix/age-ratings.md) questionnaire.

## Test the package locally

> [!div class="checklist"]
> * You installed the release package on a clean test device or virtual machine with only documented user prerequisites and, if needed for sideload testing, the public development certificate. The app launches and works. See [Run the app on a clean device](../develop/test-before-you-ship.md#1-run-the-app-on-a-clean-device).
> * You ran the [Windows App Certification Kit](/windows/uwp/debug-test-perf/windows-app-certification-kit) and fixed or understand every failure.
> * You tested core scenarios with the keyboard and a screen reader.

## If a submission fails

See [Fix Microsoft Store submission and certification failures](fix-submission-failures.md), which is organized by where a submission stops: local testing, package upload in Partner Center, or certification. After you fix an issue, rebuild and retest the package. For updates, increase the version of the package being replaced, following the package-version rules.

## For AI agents and automation

An AI agent can help with most of this checklist: reading the manifest, comparing identity values that the developer copies from Partner Center, checking image formats and dimensions, running the Windows App Certification Kit, and drafting listing text for review. A person must review the listing, complete account and agreement steps in Partner Center, and confirm every submission.

## Next steps

- [Create an app submission](publish-your-app/msix/create-app-submission.md)
- [Publish an update to your app](publish-your-app/msix/publish-update-to-your-app-on-store.md)
- [Microsoft Store Developer CLI](msstore-dev-cli/overview.md)
