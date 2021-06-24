---
description: The certification process for MSI and EXE apps, and a description of some of the tests that app packages must pass to be distributed on the Microsoft Store
title: The MSI or EXE app certification process
ms.assetid: 72BA6FA2-E689-4019-8611-7DC28BAE785C
ms.date: 06/24/2021
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, msi, exe, unpackaged, unpackaged app, desktop app, traditional desktop app, game settings, display mode, system requirements, hardware requirements, minimum hardware, recommended hardware, privacy policy, support contact info, app website, support info
ms.localizationpriority: medium
---

# The MSI or EXE app certification process

> [!NOTE]
> MSI and EXE support in the Microsoft Store is currently in a limited public preview phase. As the size of the preview expands, we'll be adding new participants from the wait list. To join the wait list, click [here](https://aka.ms/storepreviewwaitlist).

When you finish creating your app's submission and submit it to the Microsoft Store, the submission enters the certification step. This process usually takes 24 hours, though in some cases it may take up to three business days. After your submission passes certification, it can take up to 24 hours for customers to see the app’s listing.

You'll be notified when your submission is published, and the app's status in the dashboard will be **In the Store**.

## Preprocessing

Your app package will be downloaded from the package URL you specified, and any instructions in the certification notes will be followed. We'll display a message if we detect any errors during preprocessing.

## Certification

During this phase, several tests are conducted to validate your app submission. You’ll be notified in case your submission fails any of these tests.

## Security tests

Your app submission will be subject to a series of checks such as the below:

### Package URL

- You must provide a secure (HTTPS) package URL. Your submission will not proceed to the next step if this test has failed. 
- The package URL must host your app’s installer packaged as a .exe or .msi file. Your submission will not proceed to the next step if this test has failed.

> [!IMPORTANT]
> The installer binary on the package URL must not change once it has been submitted. It is recommended you create and submit versioned package URLs (such as https://contoso.com/downloads/1.1/myinstaller.msi). If you need to update the package URL, you may create a new [app submission](app-submissions.md) with a new package URL.

### Malware test

- This test checks your app for viruses, malware, and unwanted applications using static and dynamic scanning technologies. If your app fails this test, you'll need to check your development system by running the latest antivirus software, then rebuild your app's package on a clean system.

### Silent install

- This test checks if your app can install silently without any user interfaces visible to the user. Any installer parameters you provide will be used when installing your package.

### Standalone/offline installer

- This test checks if the installer you submitted is a standalone/offline installer and is not a downloader that downloads binaries when invoked. This is required to certify the binaries that get installed are the same ones that passed the certification process.

### Bundleware check

- This test checks if your app is attempting to install any additional apps that may not be related to the core purpose of your app.

### Dependency on non-Microsoft drivers/ NT services

- This test will check to see if your app has a dependency on any type of non-Microsoft drivers or NT services. You are required to disclose such dependency in Partner Center during app submission.

### Additional tests

- Depending on the type of app submitted, additional tests related to the app’s performance, security, stability, and reliability may be performed and observations shared with you for next steps.

## Avoid common certification failures

Review this list to help avoid issues that frequently prevent apps from getting certified, or that might be identified during a spot check after the app is published.

- Do not promote third-party apps during or after installation.
- Submit your app only when it's finished. You're welcome to use your app's description to mention upcoming features, but make sure that your app does not contain incomplete sections, links to web pages that are under construction, or anything else that would give a customer the impression that your app is incomplete.
- Scan your app with Microsoft Defender or any consumer antivirus software compatible with Windows to ensure it is free from any malware.
- Test your app on several different configurations to ensure that it's as stable as possible.
- Ensure that your app does not crash without network connectivity. Even if a connection is required to use your app, it needs to perform appropriately when no connection is present.
- Provide any necessary info required to use your app, such as the username and password for a test account if your app requires users to log in to a service, or any steps required to access hidden or locked features.
- Include a privacy policy URL if your app requires one; for example, if your app accesses any kind of personal information in any way or is otherwise required by law. To help determine if your app requires a privacy policy, review the [App Developer Agreement](/legal/windows/agreements/app-developer-agreement) and the [Microsoft Store Policies](/legal/windows/agreements/store-policies#105-personal-information)..
- Make sure that your app's description clearly represents what it does. For help, see our guidance on [writing a great app description](../write-a-great-app-description.md).
- Do not declare your app as accessible unless you have specifically engineered and tested it for accessibility scenarios.
- Review the [Microsoft Store Policies](/legal/windows/agreements/store-policies) to ensure your app meets all the requirements listed there.

## Content compliance

The amount of time this test takes varies depending on how complex your app is, how much visual content it has, and how many apps have been submitted recently. Be sure to provide any info that testers should be aware of in the notes for certification section.

After the certification process is complete, you'll get a certification report through email telling you whether your app passed certification. If it did not pass, the report will indicate which test failed or which policy was not met. After you fix the problem, you can create a new submission for your app to start the certification process again.

## Publishing

Your app will be published once it has been certified. Once this phase has begun, you can no longer cancel your submission.

## In the Store

After successfully going through the steps above, the submission's status will change from **Publishing** to **In the Store**. Your submission will then be available in the Microsoft Store for customers to download.

We also conduct spot checks of apps after they've been published so we can identify potential problems and ensure that your app complies with all the  [Microsoft Store Policies](/legal/windows/agreements/store-policies). If we find any problems, you'll be notified about the issue and how to fix it, if applicable, or if it has been removed from the Microsoft Store.
