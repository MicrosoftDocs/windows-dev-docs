---
author: jnHs
Description: When you finish creating your app's submission and click Submit to the Store, the submission enters the certification step.
title: The app certification process
ms.assetid: 0DCB4344-224D-4E5A-899F-FF7A89F23DBC
ms.author: wdg-dev-content
ms.date: 03/09/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, publishing, preprocessing, certification, release, pending, submit, publish, status
ms.localizationpriority: high
---

# The app certification process

When you finish creating your app's submission and click **Submit to the Store**, the submission enters the certification step. This process usually is completed within a few hours, though in some cases it may take up to three business days. After your submission passes certification, it can take up to 24 hours for customers to see the app’s listing (or your updates to a previously published app) in the Store. You'll see a notification when your submission is published and available to customers, and the app's status in the dashboard will be **In the Store**.

## Preprocessing

After you successfully upload the app's packages and submit the app for certification, the packages are queued for testing. We'll display a message if we detect any errors during preprocessing. For more info on possible errors, see [Resolve submission errors](resolve-submission-errors.md).

## Certification

During this phase, several tests are conducted:

-   **Security tests:** This first test checks your app's packages for viruses and malware. If your app fails this test, you'll need to check your development system by running the latest antivirus software, then rebuild your app's package on a clean system.
-   **Technical compliance tests:** Technical compliance is tested by the Windows App Certification Kit. (You should always make sure to [test your app with the Windows App Certification Kit](../debug-test-perf/windows-app-certification-kit.md) before you submit it to the Store.)
-   **Content compliance:** The amount of time this takes varies depending on how complex your app is, how much visual content it has, and how many apps have been submitted recently. Be sure to provide any info that testers should be aware of in the [Notes for certification](notes-for-certification.md) page.

After the certification process is complete, you'll get a certification report telling you whether or not your app passed certification. If it didn't pass, the report will indicate which test failed or which [policy](https://docs.microsoft.com/legal/windows/agreements/store-policies) was not met. After you fix the problem, you can create a new submission for your app to start the certification process again.

## Release

When your app passes certification, it's ready to move to the to the **Publishing** process. If you've indicated that your submission should be published as soon as possible, this will happen right away. If you've specified that it should not be released until a certain date, we'll wait until that date, unless you click the link to **Change release date**. If you've indicated that you want to publish the submission manually, then we won't publish it until you indicate that we should by clicking the button to **Publish now**, or if you click the link to **Change release date** and pick a specific date.

## Publishing

Your app's packages are digitally signed to protect them against tampering after they have been released. Once this phase has begun, you can no longer cancel your submission or change its release date.

While your app is in the publishing phase, the **Show details** link in the Status column for your app’s submission will let you know when your new packages and Store listing details become available to customers on each of your supported OS versions. Steps that have not yet completed will show **Pending**. Your app will remain in the publishing phase until the process has completed, meaning that the new packages and listing details are available to all of your app’s potential customers. This can take up to 24 hours. 

## In the Store 

After successfully going through the steps above, the submission's status will change from **Publishing** to **In the Store**. Your submission will then be available in the Microsoft Store for customers to download (unless you have chosen another [Discoverability](choose-visibility-options.md#discoverability) option). 

> [!NOTE]
> We also conduct spot checks of apps after they've been published so we can identify potential problems and ensure that your app complies with all of the [Microsoft Store Policies](https://docs.microsoft.com/legal/windows/agreements/store-policies). If we find any problems, you'll be notified about the issue and how to fix it, if applicable, or if it has been removed from the Store.

 

 

 




