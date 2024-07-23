---
description: Manage your MSIX app submission options, including publishing hold options and notes for certification to provide additional info to the certification testers. Additionally, monitor the status of your MSIX draft app submissions.
title: Manage submission options for MSIX app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Submission options for MSIX app

The **Submission options** page of the app submission process is where you can provide more information to help us test your product properly. This is an optional step, but is recommended for many submissions. You can also optionally set publishing hold options if you want to delay the publishing process.

:::image type="content" source="images/msix-submission-options.png" lightbox="images/msix-submission-options.png" alt-text="A screenshot showing the submission options page for a MSIX/PWA app.":::

### Publishing hold options

By default, we'll publish your submission as soon as it passes certification (or per any dates you specified in the [Schedule](./schedule-pricing-changes.md#configure-precise-release-scheduling) section of the **Pricing and availability** page). You can optionally choose to place a hold on publishing your submission until a certain date, or until you manually indicate that it should be published. The options in this section are described below.

#### Publish your submission as soon as it passes certification (or per dates you specify)

**Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)** is the default selection, and means that your submission will begin the publishing process as soon as it passes certification, unless you have configured dates in the [Schedule](./schedule-pricing-changes.md#configure-precise-release-scheduling) section of the **Pricing and availability** page.

For most submissions, we recommend leaving the **Publishing hold options** section set to this option. If you want to specify certain dates for your submission to be published, use the **Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)**. Leaving this section set to the default option will not cause the submission to be published earlier than the date(s) that you set in the **Schedule** section. The dates you selected in the **Schedule** section will be used to determine when your product becomes available to customers in the Store.

#### Publish your submission manually

If you don’t want to set a release date yet, and you prefer your submission to remain unpublished until you manually decide to start the publishing process, you can choose **Don't publish this submission until I select Publish now**. Choosing this option means that your submission won’t be published until you indicate that it should be. After your submission passes certification, you can publish it by selecting **Publish now** on the certification status page, or by selecting a specific date in the same manner as described below.

#### Start publishing your submission on a certain date

Choose **Start publishing this submission on** to ensure that the submission is not published until a certain date. With this option, your submission will be released as soon as possible on or after the date you specify. The date must be at least 24 hours in the future. Along with the date, you can also specify the time at which the submission should begin to be published.

You can change this release date after submitting your product, as long as it hasn’t entered the Publish step yet.

As noted earlier, if you want to specify certain dates for your submission to be published, use the **Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)** and leave the **Publishing hold options** set to the default selection. Using the **Start publishing this submission on** option means that your submission will not start the publishing process until that date, but delays during certification or publishing could cause the actual release date to be later than the date you select.

### Restricted capabilities

If we detect that your packages declare any [restricted capabilities](/windows/uwp/packaging/app-capability-declarations#restricted-capabilities), you’ll need to provide info in this section in order to receive approval. For each capability, tell us why your app needs to declare the capability and how it is used. Be sure to provide as much detail as possible to help us understand why your product needs to declare the capability.

During the certification process, our testers will review the info you provide to determine whether your submission is approved to use the capability. Note that this may add some additional time for your submission to complete the certification process. If we approve your use of the capability, your app will continue through the rest of the certification process. You generally will not have to repeat the capability approval process when you submit updates to your app (unless you declare additional capabilities).

If we don’t approve your use of the capability, your submission will fail certification, and we will provide feedback in the certification report. You then have the option to create a new submission and upload packages which don’t declare the capability, or, if applicable, address any issues related to your use of the capability and request approval in a new submission.

Note that there are some restricted capabilities which will very rarely be approved. For more info about each restricted capability, see [App capability declarations](/windows/uwp/packaging/app-capability-declarations#restricted-capabilities).

## Notes for certification

As you submit your app, you have the option to use the **Notes for certification** page to provide additional info to the certification testers. This info can help ensure that your app is tested correctly. Including these notes is particularly important for products that use Xbox Live Services and/or that require logging in to an account. If we can't fully test your submission, it may fail certification.

Make sure to include the following (if applicable for your app):

- **User names and passwords for test accounts**: If your app requires users to log in to a service or social media account, provide the user name and password for a test account. The certification testers will use this account when reviewing your app.

- **Steps to access hidden or locked features**: Briefly describe how testers can access any features, modes, or content that might not be obvious. Apps that appear to be incomplete may fail certification.

- **Steps to verify background audio usage**: If your app allows audio to run in the background, testers may need instructions on how to access this feature so they can confirm it functions appropriately.

- **Expected differences in behavior based on region or other customer settings**: For example, if customers in different regions will see different content, make sure to call this out so that testers understand the differences and review appropriately.

- **Info about what's changed in an app update**: For updates to previously published apps, you may want to let the testers know what has changed, especially if your packages are the same and you're just making changes to your app listing (such as adding more screenshots, changing your app's category, or editing the description).

- **The date you're entering the notes**: This is particularly important if you are using a development sandbox in Partner Center (for example, this is the case for any game that integrates with Xbox Live), since the notes you enter when publishing to a sandbox will remain when you request certification. Seeing the date helps testers evaluate whether there were any temporary issues that may no longer apply.

- **Anything else you think testers will need to understand about your submission**

When considering what to write, remember:

- **A real person will read these notes.** Testers will appreciate a polite note and clear, helpful instructions.

- **Be succinct and keep instructions simple.** If you really need to explain something in detail, you can include the URL to a page with more info. However, keep in mind that customers of your app won't see these notes. If you feel that you need to provide complicated instructions for testing your app, consider whether your app might need to be made simpler so that customers (and testers) will know how to use it.

- **Services and external components must be online and available.** If your app needs to connect to a service in order to function, make sure that the service will be online and available. Include any information about the service that testers will need, such as login info. If your app can't connect to a service it needs during testing, it may fail certification.

## App submission controls

Submission controls let you manage your app submission more easily. You can delete a draft submission, cancel a review process, or make your app unavailable.

### Delete draft submissions and apps

To delete a draft submission, follow these steps:

1. Go to the Apps and Games overview page and open the app you want to delete. You can only delete an app that is not submitted or live on the Store.
1. From the **Product submission** (or product update card in case of an update submission) card, click **Delete submission**.
1. Confirm that you want to delete the app submission and all its information.

If there are no remaining draft submissions, you can delete your app.

1. Go to the app overview page for the app you want to delete.
1. Beside the app name click on 3 dots and then click **Delete product**.
1. Confirm that you want to delete the app.

### Cancel certification

To cancel certification, follow these steps:

1. Go to the Apps and Games overview page and open the app you submitted for review or certification.
1. Navigate to the Application overview page for your app.
1. From the **Certification status** card under Product release section, click on 3 dots on the far right side and then click on **Cancel certification**.
1. Confirm that you want to cancel the review process.

### Make product available or unavailable

To make your app unavailable without deleting it from the store, follow the following steps.

1. Go to the Apps and Games overview page and open the app you want to make unavailable.
1. From the Store presence card under Product release section, turn off the toogle switch to make product unavailable.
1. Confirm that you want to make your app unavailable.

To make your app available again, perform the same steps as above, but turn on the toggle switch instead.
