---
author: jnHs
Description: You can use package flights to distribute packages that are only given to a limited test group.
title: Package flights
ms.assetid: 5B094822-A8DE-4EE3-B55D-3E306C04EE79
ms.author: wdg-dev-content
ms.date: 06/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, flighting
---

# Package flights

You can use package flights to distribute packages that are only given to a limited test group. 

Package flights allow you to provide different packages to designated set of testers without disrupting the experience of your other customers. Only the packages are different; the Store listing details will be the same for all of your customers.

Note that package flights must pass the [certification process](the-app-certification-process.md), just the same as a regular, non-flighted submission. If you later decide that you want to make packages from a package flight available to all your customers, you can pull those packages into your non-flighted submission as described below.

When you set up package flights, you can choose the specific people who should get specific packages by adding them to a **flight group**. Anyone in a flight group who is using a device running a version of Windows 10 that supports package flights (Windows.Desktop build 10586 or later; Windows.Mobile build 10586.63 or later; or Xbox One) will get the packages from the package flight(s) that you designate for that particular group. (Your package flights can include packages targeting any OS version, including Windows 8.1/Windows Phone 8.1 or earlier.) Anyone who has not been added to one of your flight groups, or is using a device that doesn’t support package flights, will get packages from the non-flighted submission.

> [!IMPORTANT] 
> On desktop and mobile devices, people in your flight groups will get the packages in your flight automatically whenever you provide updates. However, **people in your flight groups who are using Xbox devices will need to check for updates manually** in order to get the latest packages, making sure they are signed into their device using their Microsoft account (with the associated email address that you included in your flight group).

Note that package flights will not be distributed via [Microsoft Store for Business](https://businessstore.microsoft.com/store) and [Microsoft Store for Education](https://educationstore.microsoft.com/store). This is because people in your flight groups must be signed in with their Microsoft accounts in order to receive a package flight. All acquisitions made via Microsoft Store for Business or Microsoft Store for Education will receive your non-flighted packages.

> [!TIP]
> Package flights offer packages only to the selected customers that you specify. To distribute packages to a random selection of customers in a specified percentage, you can use [gradual package rollout](gradual-package-rollout.md). You can also combine rollout with your package flights if you want to gradually distribute an update to one of your flight groups.
>
> Unlike package flights, your gradual package rollout selections do apply to customers who acquire your app via Microsoft Store for Business and Microsoft Store for Education. 

After you have published a submission for your app, you'll see a **Package flights** section on the App overview page. Click **New package flight** to get started. If you haven't set up any flight groups yet, you'll be prompted to create one before you can proceed.


## Create a new flight group

Flight groups let you specify the people that you'd like to include in the group. In order to get your flighted packages, each person must be authenticated with the Store using the Microsoft account associated with the email address you provide, and must be using a Windows 10 device (as specified above) to download the app.

When you create a flight group, you must give it a name. Each flight group must contain at least one email address, with a maximum of 10,000 email addresses. You can enter email addresses directly into the field (separated by spaces, commas, or semicolons), or you can click the **Import .csv** link to create the flight group from a list of email addresses in a .csv file.

Click **Create group** to save the group and continue setting up the package flight. (You can also create a flight group by expanding the **Engage** menu in the left navigation menu and selecting **Customer groups**.)

> [!TIP]
> Be sure that you have obtained any necessary consent from people that you add to your flight group, and that they understand that they will be getting packages that are different from your non-flighted submission. 
>
> You may also want to consider how the people in your package flight can give you their input about the app. We suggest [adding a control into your app to launch Feedback Hub](../monetize/launch-feedback-hub-from-your-app.md) so that customers can provide their input directly; you can then review their feedback in your app's [Feedback report](feedback-report.md)).

To edit your flight group later, click **View and manage existing groups** when creating a new flight and then select the flight group you wish to modify, or click the flight group's name from a package flight's overview page. You can add or remove email addresses directly in the field, or for larger changes, click **Export .csv** to save your flight group membership info to a .csv file. Make your changes in this file, then click **Import .csv** to use the new version to update the group membership. Note that it may take up to 30 minutes for flight group membership changes to be implemented. If you add people to a flight group after you've published an associated package flight, the packages will be delivered to the new people automatically; you don't have to create and publish a new submission for that package flight. 

You can also review and edit your flight groups by expanding the **Engage** menu in the left navigation menu and selecting **Customer groups**.


## Create a new package flight

After you've created your first flight group, you'll see a page where you can add details to complete setting it up. You'll need to give the package flight a name and specify at least one flight group. If you want to set up a new group, you can do that from this page.

Click **Create flight** once you've entered the name and selected the flight group(s). You won't be able to change these details later (though you can always delete it and create a new package flight to use instead).

> [!NOTE]
> If you have more than one package flight, you'll need to assign a rank to each one. For more info, see Add and rank additional package flights below.


## Specify packages to include in your package flight

After you've saved your package flight details, you'll see its overview page. Click **Packages** to specify the packages you'd like to include in the flight. You can include packages targeting any OS version, including Windows 10, Windows 8.x, and Windows Phone 8.x or earlier.

You have the option to select packages that were associated with a previous published submission (either a non-flighted submission, or one of your other package flights, if you have more than one). If you need to [upload new packages](upload-app-packages.md) to use for this package flight, you can upload them here (using the same process as when you upload app packages to a regular non-flighted submission). Click **Save** when you have finished specifying the packages to be included in this package flight.

If your app supports multiple device families, make sure you include packages to support the same set of device families in your flight. People in your flight groups will **only** be able to get packages from that flight. They won't be able to access packages from other flights, or from your non-flighted submission. 

Also remember that your Store listing info comes from your non-flighted submission, including which device families your app supports. Customers in your flight groups will only be able to download the app on a device family that is supported by your non-flighted submission. For more info, see [Device family support](#device-family-support). 


## Gradual package rollout

By default, the packages in your submission will be made available to everyone in your flight group at the same time. To change this, you can check the box that says **Roll out update gradually after this submission is published (to Windows 10 customers only)**. You can choose a percentage of people in your flight group to get the packages from the new submission, so that you can monitor feedback and analytic data to make sure you’re confident about the update before rolling it out more broadly to the rest of the flight group. You can increase the percentage (or halt the update) any time without having to create a new submission for your package flight. 

> [!IMPORTANT]
> When gradually rolling out packages in a package flight, the people who aren't included in the percentage which gets your new packages will get the packages from the previous package flight submission (unless there is a higher-ranked flight available to them).

For more info, see [Gradual package rollout](gradual-package-rollout.md).


## Configure additional package flight options

By default, your package flight will be published and made available to your flight group as soon as it completes the certification process. If you'd like to change the [publish date](set-app-pricing-and-availability.md#publish-date), or want to add [Notes for certification](notes-for-certification.md), you can do so in the **Flight options** section. Click **Save** to return to the package flight overview page. 


## Submit your package flight to the Store

When you've specified packages and configured any options needed, click **Submit to the Store**. Your package flight will then go through the [app certification process](the-app-certification-process.md). Note that packages included in your package flight must comply with the [Windows Store Policies](https://msdn.microsoft.com/library/windows/apps/dn764944.aspx), as with all submissions.

People in your flight group(s) associated with that package flight who already have your app will now get an update using the packages you included in your package flight. If those people don’t have your app yet, they’ll get the packages from your package flight when they install it. 

> [!NOTE]
> People who have a package that is only available in a package flight can give the app a star rating and leave reviews, but their ratings and reviews won’t be shown to other customers. (This excludes legacy 7.x or 8.0 XAP packages; ratings and reviews left by members of your flight groups using those packages will be visible to other customers.) You can see ratings and feedback from all customers, including those in your flight groups, in the **Reviews** and **Feedback** reports for the app.


## Device family support

In most cases, you’ll want to include packages that support the same set of device families supported by your non-flighted submission. Device family availability for an app will always be based on the non-flighted submission, whether or not a customer is in a flight group.

**If your non-flighted submission supports a device family that your package flight doesn’t support**, people in your flight group won’t be able to download the app on that device family. For example, if your non-flighted submission includes Mobile and Desktop packages, and you then create a package flight that only includes a Mobile package, people in your flight group will only be able to download the app on mobile devices, even though you do have a desktop package available to customers who aren’t in the flight. Even if you're only using the package flight to test changes in your Mobile package, you should include the Desktop package from your non-flighted submission in the package flight so that customers in the flight group are able to download your app on desktop devices.

**If your package flight supports a device family that your non-flighted submission doesn’t support**, no one will be able to download the app on that device family, whether they’re in your flight group or not. For example, if your non-flighted submission only includes a Mobile package, and you then create a package flight that includes both Mobile and Desktop packages, people in your flight group will still only be able to download the app on mobile devices. The desktop package won’t be offered to anyone, even people in your flight group. If you want to make a desktop package available to people in your flight group, you’ll need to first update your non-flighted submission to include a desktop package. For the best experience for all of your app’s customers, your non-flighted submission should support the same device families as your package flight. 

> [!NOTE]
> Packages added to your package flights can support any OS version (or any build of Windows 10), but as noted above, people in flight groups must be using a device running a version of Windows 10 that supports package flights (Windows.Desktop build 10586 or later; Windows.Mobile build 10586.63 or later) in order to get packages from the package flight.


## Update or modify your package flight

To create a new submission for an existing package flight, click **Update** next to the flight name on your App overview page. You can then upload new packages (and remove unneeded packages), just as you would with a non-flighted submission. Make any other needed changes, and then click **Submit to the Store** to send the updated package flight through the [app certification process](the-app-certification-process.md).

To modify an existing flight without creating and submitting a new update, click **Modify** next to the flight name. This lets you change details such as the flight groups, name, and rank, without  requiring that the package flight go through the certification process again.


## Add and rank additional package flights

You can create multiple package flights for the same app in order to distribute several different packages to different sets of customers. 

Once you have created your first package flight, you create another by following the process outlined above. The only difference is that if you've already created one package flight, you'll need to specify the priority order of all package flights in the **Rank** section. This lets the Store can determine which package to give to any individual customer, even if they are in more than one of your flight groups. People in your flight groups will always get the highest-ranked package flight available to them, even if a lower-ranked package flight contains packages with a higher version number.

By default, your new package flight will be ranked highest. If you'd like to change its rank, you can move it down (or back up) to place it in the right location among your other package flights.

Note that your non-flighted submission is always ranked the lowest. That is, people who aren’t in any of your flight groups can only get packages from your non-flighted submission through the Store. People in a flight group will always get packages from the highest-ranked package flight available to them (but never the non-flighted submission). This gives you flexibility in determining how to distribute your packages to people who may be members of more than one of your flight groups.

For example, let's say you want to create two package flights in addition to your regular non-flighted submission: one that is relatively stable and ready for testing with a wide audience, and one that you're not so sure about and want to limit to only a few testers. You could create a flight group called Testers and include it in a package flight called Tester Flight, then create a flight group called Enthusiasts with a larger membership and include it in another package flight called Enthusiast Flight. If you rank Tester Flight higher than Enthusiast Flight, you can use packages that you're fairly confident about in Enthusiast Flight, while using riskier packages meant for Testers only in Tester Flight. Members of your Testers group will always get the packages you provide in Tester Flight, even if they also belong to your Enthusiasts group. (Then later, if it turns out that the packages in Tester Flight are performing well, you could update Enthusiast Flight to use the packages originally distributed to Tester Flight—and maybe eventually use those packages in your non-flighted submission.)


## Make packages from a package flight available to all your customers

If you decide that one or more of the packages you included in a published package flight should be made available to customers who aren’t in a flight group, you can update your non-flighted submission to use those packages, without having to upload the same packages all over again. 

When you create your new submission, on the [**Packages**](upload-app-packages.md) page you’ll see a drop-down with the option to copy packages from one of your package flights. Select the package flight that has the packages you want to pull in. You can then select any or all of its packages to include in the non-flighted submission.

Note that all of the same package validation rules will apply, even when using packages from a previously published submission. 


## Delete a package flight

To delete a package flight that you no longer want to support, click its name from the App overview page. On the flight overview page, click **Modify**, then click the **Delete** link to delete the package flight. (If you have an unpublished submission of the package flight in progress, you’ll need to delete that submission first.) It may take up to 30 minutes for this to be completed.

When you delete a package flight, any customers who have the packages you distributed in that package flight will get an app update if there is a package with a higher version number (or as soon as such a package becomes available). If they uninstall the app and then install it again later, this will be treated as a new acquisition, and they'll get the highest version currently available. 
