---
description: When you finish creating your app add-on's submission and click Submit to the Store, the submission enters the certification step.
title: The app certification process for add-on
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# The app certification process for add-on

When you finish creating your app's submission and click **Submit to the Store**, the submission enters the certification step. This process usually is completed within a few hours, though in some cases it may take up to three business days. After your submission passes certification, it can take up to 24 hours for customers to see the app’s listing for a new submission, or for an updated submission with changes to packages. If your update only changes Store listing details, the publishing process will be completed in less than an hour. You'll be notified when your submission is published, and the app's status in the dashboard will be **In the Store**.

## Preprocessing

After you successfully upload the app's packages and submit the app for certification, the packages are queued for testing. We'll display a message if we detect any errors during preprocessing. For more info on possible errors, see [Resolve submission errors](./resolve-submission-errors.md).

## Certification

During this phase, several tests are conducted:

- **Security tests:** This first test checks your app's packages for viruses and malware. If your app fails this test, you'll need to check your development system by running the latest antivirus software, then rebuild your app's package on a clean system.
- **Technical compliance tests:** Technical compliance is tested by the Windows App Certification Kit. (You should always make sure to [test your app with the Windows App Certification Kit](/windows/uwp/debug-test-perf/windows-app-certification-kit) before you submit it to the Store.)
- **Content compliance:** The amount of time this takes varies depending on how complex your app is, how much visual content it has, and how many apps have been submitted recently. Be sure to provide any info that testers should be aware of in the [Notes for certification](./manage-submission-options.md#notes-for-certification) page.

After the certification process is complete, you'll get a certification report telling you whether or not your app passed certification. If it didn't pass, the report will indicate which test failed or which [policy](../../store-policies.md) was not met. After you fix the problem, you can create a new submission for your app to start the certification process again.

## Release

When your app passes certification, it's ready to move to the **Publishing** process.

- If you've indicated that your submission should be published as soon as possible (the default option), the publishing process will begin right away.
- If this is the first time you've published the app, and you specified a **Release date** in the [Schedule](./schedule-pricing-changes.md#configure-precise-release-scheduling) section, the app will become available according to your **Release date** selections.
- If you've used [Publishing hold options](./manage-submission-options.md#publishing-hold-options) to specify that it should not be released until a certain date, we'll wait until that date to begin the publishing process, unless you select **Change release date**.
- If you've used [Publishing hold options](./manage-submission-options.md#publishing-hold-options) to specify that you want to publish the submission manually, we won't start the publishing process until you select **Publish now** (or select **Change release date** and pick a specific date).

## Publishing

Your app's packages are digitally signed to protect them against tampering after they have been released. Once this phase has begun, you can no longer cancel your submission or change its release date.

For new apps and updates which include changes to the app's packages, the publishing process will be completed within 24 hours. For updates that only change options such as Store listing details, but don't change the app's packages, the publishing process will take less than one hour.

While your app is in the publishing phase, the **Show details** link in the Status column for your app’s submission lets you know when your new packages and Store listing details are available to customers on each of your supported OS versions. Steps that have not yet completed will show **Pending**. Your app will remain in the publishing phase until the process has completed, meaning that the new packages and/or listing details are available to all of your app’s potential customers.

## In the Store

After successfully going through the steps above, the submission's status will change from **Publishing** to **In the Store**. Your submission will then be available in the Microsoft Store for customers to download (unless you have chosen another [Discoverability](./visibility-options.md#discoverability) option).

> [!NOTE]
> We also conduct spot checks of apps after they've been published so we can identify potential problems and ensure that your app complies with all of the [Microsoft Store Policies](../../store-policies.md). If we find any problems, you'll be notified about the issue and how to fix it, if applicable, or if it has been removed from the Store.
