---
description: When you finish creating your MSI/EXE app's submission and click Submit to the Store, the submission enters the certification step.
title: The app certification process for MSI/EXE app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# The app certification process for MSI/EXE app

When you finish creating your app's submission and submit it to the Microsoft Store, the submission enters the certification step. This process can take up to three business days. After your submission passes certification, on an average, customers will be able to see the app’s listing within 15 minutes depending on their location.

Your app package will be downloaded from the package URL you specified. Any instructions in the certification notes will be followed. We'll display a message if we detect any errors during preprocessing. During this phase, several tests are conducted to validate your app submission. You’ll be notified if your submission fails any of these tests.

When your submission is published, you'll be notified and the app's status in the dashboard will be **In the Store**.

Before publishing, apps are subject to two categories of tests: security tests and content compliance.

## Security tests

Your app submission will be subject to a series of checks.

### Package URL

You must provide a secure (HTTPS) package URL. Your submission will not proceed to the next step if this test has failed.

The package URL must host your app’s installer packaged as an .exe or .msi file. Your submission will not proceed to the next step if this test has failed.

> [!IMPORTANT]
> The installer binary on the package URL must not change once it has been submitted. We recommend that you create and submit versioned package URLs (such as `https://contoso.com/downloads/1.1/myinstaller.msi`). If you need to update the package URL, you may create a new [app submission](./create-app-submission.md) with a new package URL.

### Malware test

This test checks your app for viruses, malware, and unwanted applications using static and dynamic scanning technologies. If your app fails this test, you'll need to check your development system by running the latest antivirus software, then rebuild your app's package on a clean system.

We highly recommended that you scan your app with Microsoft Defender or another consumer antivirus software that's compatible with Windows to ensure that it is free from malware and unwanted apps.

### Silent install

This test checks typically checks for the following behavior in your app:

- Can install silently without any user interfaces visible to the user. Any installer parameters you provide will be used when installing your package.
- Can be successfully installed when logged in with a standard user account.
- Can make an entry in the Windows Start menu and Programs list, so users can discover it. If your app does not need to do this, you should mention this in the _Notes for Certification_ section of your submission.
- Your app's installer is configured appropriately for Windows to query information such as ProductName, Publisher Name, Default Language, and Version info (as applicable) in places where customers expect to find such information, like in Add/Remove Programs in Windows. This information is part of your app’s installer package. See [setting installer properties](/windows/win32/msi/property-reference#product-information-properties) for details about how to set properties for your Windows installer.
- Can uninstall cleanly without leaving remnants of files, folder, and registry entries.

### Standalone/offline installer

This test checks if the installer you submitted is a standalone/offline installer and is not a downloader that downloads binaries when invoked. This is required to certify the binaries that get installed are the same ones that passed the certification process.

### Bundleware check

This test checks if your app is attempting to install any additional third-party apps that may not be related to the core purpose of your app.

### Dependency on non-Microsoft drivers/ NT services

This test will check to see if your app has a dependency on any type of non-Microsoft drivers or NT services. You are required to disclose such dependency in Partner Center during app submission.

Digital signature/code signing is an integral part of ensuring a verified and trusted ecosystem of apps and updates on Windows.
It is highly recommended that your EXE/MSI app and the Portable Executable (PE) files inside of it are digitally signed with a certificate that chains up to a certificate of a Certificate Authority (CA) that is part of the [Microsoft Trusted Root Program](/security/trusted-root/participants-list).

### Privacy policy

Include a valid privacy policy URL if your app requires one; for example, if your app accesses any kind of personal information in any way or is otherwise required by law. To help determine if your app requires a privacy policy, review the [App Developer Agreement](https://go.microsoft.com/fwlink/?linkid=528905) and the [Microsoft Store Policies](../../store-policies.md#105-personal-information).

### Additional tests

Depending on the type of app submitted, additional tests related to the app’s performance, security, stability, and reliability may be performed and observations shared with you for next steps.

## Avoid common certification failures

Review this list to help avoid issues that frequently prevent apps from getting certified, or that might be identified during a spot check after the app is published.

- Do not promote third-party apps during or after installation.
- Submit your app only when it's finished. You're welcome to use your app's description to mention upcoming features, but make sure that your app does not contain incomplete sections, links to web pages that are under construction, or anything else that would give a customer the impression that your app is incomplete.
- Test your app on several different configurations to ensure that it's as stable as possible.
- Ensure that your app does not crash without network connectivity. Even if a connection is required to use your app, it needs to perform appropriately when no connection is present.
- Provide any necessary info required to use your app, such as the username and password for a test account if your app requires users to log in to a service, or any steps required to access hidden or locked features.
- Configure your app's installer to provide your app’s information such as ProductName, Publisher Name, Default Language, Version info (as applicable) in places where customers expect to find such information such as ‘Add/Remove Programs’ in Windows. This information is part of your app’s installer package. See [setting installer properties](/windows/win32/msi/property-reference#product-information-properties) for more details on how to set properties for your Windows installer
- Include a privacy policy URL if your app requires one; for example, if your app accesses any kind of personal information in any way or is otherwise required by law. To help determine if your app requires a privacy policy, review the [App Developer Agreement](https://go.microsoft.com/fwlink/?linkid=528905) and the [Microsoft Store Policies](../../store-policies.md#105-personal-information).
- Make sure that your app's description clearly represents what it does. For help, see our guidance on [writing a great app description](./write-great-app-description.md).
- Do not declare your app as accessible unless you have specifically engineered and tested it for accessibility scenarios.
- Review the [Microsoft Store Policies](../../store-policies.md) to ensure your app meets all the requirements listed there.

## Content compliance

The amount of time this test takes varies depending on how complex your app is, how much visual content it has, and how many apps have been submitted recently. Be sure to provide any info that testers should be aware of in the notes for certification section.

When the certification process is complete, if it did not pass, you'll receive an email that includes a report that indicates which test failed or which policy was not met. After you fix the problem, you can create a new submission for your app to start the certification process again.

> [!IMPORTANT]
> Your app's content should comply with the Microsoft Store [Content Policies](../../store-policies.md#content-policies), and it will be tested in accordance with the policies. We highly recommend that you understand these policies prior to submitting your app.

## Publishing

Your app will be published after it is certified. When this phase has begun, you can no longer cancel your submission.

We also conduct spot checks of apps after they've been published so we can identify potential problems and ensure that your app complies with all the [Microsoft Store Policies](../../store-policies.md). If we find any problems, you'll be notified about the issue and how to fix it, if applicable, or if it has been removed from the Microsoft Store.
