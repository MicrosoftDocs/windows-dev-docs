﻿---
Description: Manage submission options such as publishing hold options, notes for certification, and more.
title: Manage submission options
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, publishing hold, publish date, send submission to publish, restricted capability approval
ms.localizationpriority: medium
---
# Manage submission options

The **Submission options** page of the app submission process is where you can provide more information to help us test your product properly. This is an optional step, but is recommended for many submissions. You can also optionally set publishing hold options if you want to delay the publishing process.


## Publishing hold options

By default, we'll publish your submission as soon as it passes certification (or per any dates you specified in the  [Schedule](configure-precise-release-scheduling.md) section of the **Pricing and availability** page). You can optionally choose to place a hold on publishing your submission until a certain date, or until you manually indicate that it should be published. The options in this section are described below. 


### Publish your submission as soon as it passes certification (or per dates you specify)

**Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)** is the default selection, and means that your submission will begin the publishing process as soon as it passes certification, unless you have configured dates in the [Schedule](configure-precise-release-scheduling.md) section of the **Pricing and availability** page.   

For most submissions, we recommend leaving the **Publishing hold options** section set to this option. If you want to specify certain dates for your submission to be published, use the **Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)**. Leaving this section set to the default option will not cause the submission to be published earlier than the date(s) that you set in the **Schedule** section. The dates you selected in the **Schedule** section will be used to determine when your product becomes available to customers in the Store.


### Publish your submission manually

If you don’t want to set a release date yet, and you prefer your submission to remain unpublished until you manually decide to start the publishing process, you can choose **Don't publish this submission until I select Publish now**. Choosing this option means that your submission won’t be published until you indicate that it should be. After your submission passes certification, you can publish it by selecting **Publish now** on the certification status page, or by selecting a specific date in the same manner as described below.


### Start publishing your submission on a certain date

Choose **Start publishing this submission on** to ensure that the submission is not published until a certain date. With this option, your submission will be released as soon as possible on or after the date you specify. The date must be at least 24 hours in the future. Along with the date, you can also specify the time at which the submission should begin to be published. 

You can change this release date after submitting your product, as long as it hasn’t entered the Publish step yet. 
 
As noted earlier, if you want to specify certain dates for your submission to be published, use the **Publish this submission as soon as it passes certification (or per dates you selected in the Schedule section)** and leave the **Publishing hold options** set to the default selection. Using the **Start publishing this submission on** option means that your submission will not start the publishing process until that date, but delays during certification or publishing could cause the actual release date to be later than the date you select. 


## Notes for certification

As you submit your app, you have the option to use the **Notes for certification** section to provide additional info to the certification testers. This info can help ensure that your app is tested correctly. 

For more info, see [Notes for certification](notes-for-certification.md).


## Restricted capabilities

If we detect that your packages declare any [restricted capabilities](../packaging/app-capability-declarations.md#restricted-capabilities), you’ll need to provide info in this section in order to receive approval. For each capability, tell us why your app needs to declare the capability and how it is used. Be sure to provide as much detail as possible to help us understand why your product needs to declare the capability. 

During the certification process, our testers will review the info you provide to determine whether your submission is approved to use the capability. Note that this may add some additional time for your submission to complete the certification process. If we approve your use of the capability, your app will continue through the rest of the certification process. You generally will not have to repeat the capability approval process when you submit updates to your app (unless you declare additional capabilities). 

If we don’t approve your use of the capability, your submission will fail certification, and we will provide feedback in the certification report. You then have the option to create a new submission and upload packages which don’t declare the capability, or, if applicable, address any issues related to your use of the capability and request approval in a new submission.

Note that there are some restricted capabilities which will very rarely be approved. For more info about each restricted capability, see [App capability declarations](../packaging/app-capability-declarations.md#restricted-capabilities).

