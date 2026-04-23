---
description: Manage your app submission options, including publishing hold options and notes for certification to provide additional info to the certification testers. Additionally, monitor the status of your draft app add-on submissions.
title: Manage submission options for add-on
ms.date: 10/30/2022
ms.topic: how-to
ms.localizationpriority: medium
---

# Manage submission options

The **Submission options** page of the app submission process is where you can provide more information to help us test your product properly. This is an optional step, but is recommended for many submissions. You can also optionally set publishing hold options if you want to delay the publishing process.

## Publishing hold options

By default, we'll publish your submission as soon as it passes certification (or per any dates you specified in the [Schedule](./configure-release-schedule.md#configure-precise-release-scheduling) section of the **Configure release schedule** page). You can optionally choose to place a hold on publishing your submission until a certain date, or until you manually indicate that it should be published. The options in this section are described below.

### Publish your submission as soon as it passes certification (or per dates you specify)

**Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)** is the default selection, and means that your submission will begin the publishing process as soon as it passes certification, unless you have configured dates in the [Schedule](./configure-release-schedule.md#configure-precise-release-scheduling) section of the **Configure release schedule** page.

For most submissions, we recommend keeping the **Publishing hold options** section set to the default. If you want your submission to be published on specific dates, configure those dates in the **Schedule** section. The default publishing hold option will ensure your submission is not published before the dates you set in the **Schedule** section. Your product will become available to customers in the Store based on the schedule you specify.

### Publish your submission manually

If you don’t want to set a release date yet, and you prefer your submission to remain unpublished until you manually decide to start the publishing process, you can choose **Don't publish this submission until I select Publish now**. Choosing this option means that your submission won’t be published until you indicate that it should be. After your submission passes certification, you can publish it by selecting **Publish now** on the certification status page, or by selecting a specific date in the same manner as described below.

### Start publishing your submission on a certain date

Choose **Start publishing this submission on** if you want to delay publishing until a specific date. With this option, your submission will be released as soon as possible on or after the date you specify (which must be at least 24 hours in the future). You can also set the exact time for publishing to begin.

You can update this release date after submitting your product, as long as it hasn’t entered the Publish step.

Remember, if you want your submission to be published on certain dates, you can use the **Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)** option and leave the **Publishing hold options** set to default. The **Start publishing this submission on** option ensures your submission won’t begin publishing until your chosen date, but delays during certification or publishing may result in the actual release occurring later than planned.

## Restricted capabilities

If we detect that your packages declare any [restricted capabilities](/windows/uwp/packaging/app-capability-declarations#restricted-capabilities), you’ll need to provide info in this section in order to receive approval. For each capability, tell us why your app needs to declare the capability and how it is used. Be sure to provide as much detail as possible to help us understand why your product needs to declare the capability.

During the certification process, our testers will review the info you provide to determine whether your submission is approved to use the capability. Note that this may add some additional time for your submission to complete the certification process. If we approve your use of the capability, your app will continue through the rest of the certification process. You generally will not have to repeat the capability approval process when you submit updates to your app (unless you declare additional capabilities).

If we don’t approve your use of the capability, your submission will fail certification, and we will provide feedback in the certification report. You then have the option to create a new submission and upload packages which don’t declare the capability, or, if applicable, address any issues related to your use of the capability and request approval in a new submission.

Note that there are some restricted capabilities which will very rarely be approved. For more info about each restricted capability, see [App capability declarations](/windows/uwp/packaging/app-capability-declarations).

## Notes for certification

As you submit your app, you can use the **Notes for certification** page to give certification testers extra information that will help them test your app correctly. Providing these notes is especially important for apps that use Xbox Live Services or require users to log in. If testers can’t fully test your app, your submission may fail certification.

Be sure to include the following details if they apply to your app:

- **User names and passwords for test accounts**: If your app requires login to a service or social media, provide a test account’s user name and password. Testers will use this account during their review.

- **Steps to access hidden or locked features**: Briefly explain how testers can access any features, modes, or content that aren’t obvious. Apps that seem incomplete may fail certification.

- **Steps to verify background audio usage**: If your app supports background audio, give instructions so testers can confirm this feature works as intended.

- **Expected differences in behavior based on region or other customer settings**: If your app behaves differently for users in different regions or with different settings, explain these differences so testers know what to expect.

- **Info about what's changed in an app update**: For updates, let testers know what’s new, especially if only your app listing has changed (like screenshots, category, or description) and not the app package itself.

- **The date you're entering the notes**: This is important if you’re using a development sandbox in Partner Center (such as for Xbox Live integration). The date helps testers know if any issues mentioned might no longer apply.

- **Anything else you think testers will need to understand about your submission**

When writing your notes, keep these tips in mind:

- **A real person will read these notes.** Testers appreciate polite, clear, and helpful instructions.

- **Be succinct and keep instructions simple.** If you need to provide detailed explanations, you can include a URL for more information. Remember, customers won’t see these notes. If your instructions are complicated, consider simplifying your app so both customers and testers can use it easily.

- **Services and external components must be online and available.** If your app depends on an online service, make sure it’s available during testing and provide any needed information, such as login details. If testers can’t connect to required services, your app may fail certification.

## App submission controls

### Delete draft submissions and apps

To delete a draft submission, follow these steps:

1. Go to the Apps and Games overview page and open the app for which you want to delete the app add-on. You can only delete an app add-on that is not submitted or live on the Store.
1. Click the app add-on you want to delete.
1. Click delete from the Action section.
1. Confirm the delete action.

### Cancel review

To cancel a review process, follow these steps:

1. Go to the Apps and Games overview page and open the app for which you submitted the app add-on for review or certification.
1. Click the app add-on for which you want to cancel the certification process.
1. Click **View progress** to for the app add-on submission for which you'd like to cancel the review process.
1. Click **Cancel certification**.
1. Confirm that you want to cancel the review process.

### Make product available or unavailable

To make your app unavailable without deleting it from the store, follow these steps.

1. Go to the Apps and Games overview page and open the app for which you want to make the app add-on unavailable.
1. Click the app add-on you want to make unavailable.
1. From the action section on the right-hand side, click **Make product unavailable**.
1. Confirm that you want to make your app add-on unavailable.

To make your app add-on available again, perform the same steps as above, but click **Make product available** instead.
