---
description: You can use package flights to distribute packages that are only given to a limited test group.
title: Package flights
ms.assetid: 5B094822-A8DE-4EE3-B55D-3E306C04EE79
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp, flighting
ms.localizationpriority: medium
---
# Package flights

You can use package flights to distribute specific packages to a limited group of testers. The packages you've already published to the Store will be used for your other customers, so their experience won't be disrupted.

With package flights, only the packages are different; the Store listing details will be the same for all of your customers. Anyone in your flight group will receive the packages that you include in the package flight, while customers who aren't in the flight group continue to receive your regular (non-flighted) packages.  If you later decide that you want to make packages from a package flight available to all your customers, you can easily use those same packages in a non-flighted submission. 

Note that the [certification process](publish-your-app/app-certification-process.md?pivots=store-installer-msix) is applied to package flights just the same as any submission, however some WACK failures are reported as **passing with notes** and will allow submission for flighting. This relaxation of the WACK checks is only while the package is flighting to a limited audience and is intended  to assist with package testing and preparation for release. WACK failures must be fixed before general release.   

When you set up package flights, you can specify the people who should get specific packages by adding them to a **known user group** (sometimes referred to as flight group). Anyone in a flight group who is using a device running a version of Windows 10 or Windows 11 that supports package flights (Windows.Desktop build 10586 or later; or Xbox One) will get the packages from the package flight(s) that you designate for that particular group. Anyone who has not been added to one of your flight groups, or is using a device that doesn’t support package flights, will get packages from the non-flighted submission.

> [!IMPORTANT] 
> On desktop and mobile devices, people in your flight groups will get the packages in your flight automatically whenever you provide updates. However, **people in your flight groups who are using Xbox devices will need to check for updates manually** in order to get the latest packages, making sure they are signed into their device using their Microsoft account (with the associated email address that you included in your known user group).

Note that package flights will not be distributed via [Microsoft Store for Business](https://businessstore.microsoft.com/store) and [Microsoft Store for Education](https://educationstore.microsoft.com/store). This is because people in your known user groups must be signed in with their Microsoft accounts in order to receive a package flight. All acquisitions made via Microsoft Store for Business or Microsoft Store for Education will receive your non-flighted packages.

> [!TIP]
> Package flights offer packages only to the selected customers that you specify. To distribute packages to a random selection of customers in a specified percentage, you can use [gradual package rollout](gradual-package-rollout.md). You can also combine rollout with your package flights if you want to gradually distribute an update to one of your flight groups.
>
> Unlike package flights, your gradual package rollout selections do apply to customers who acquire your app via Microsoft Store for Business and Microsoft Store for Education. 

> [!TIP]
> Consider how the people in your package flight will be able to give their input about the app. We suggest [adding a control into your app to launch Feedback Hub](/windows/uwp/monetize/launch-feedback-hub-from-your-app) so that customers can provide their input directly; you can then review their feedback in your app's [Feedback report](feedback-report.md)).


## Create a new package flight

After you have published a submission for your app, you'll see a **Package flights** section on the App overview page. Click **New package flight** to get started.

If you haven't created any known user groups yet, you'll be prompted to create one before you can proceed. For more info, see [Create known user groups](create-known-user-groups.md). You can create a new known user group directly from this page by selecting **Create a flight group**.

On the package flight creation page, you'll need to enter a name for your flight and specify at least one flight group. Once you've done so, select **Create flight**. You won't be able to change these details later (though if you're not happy with what you've entered, you can delete this flight and create a new one to use instead).

> [!NOTE]
> If you have more than one package flight, you'll need to assign a rank to each one. For more info, see [Add and rank additional package flights](#add-and-rank-additional-package-flights) below.


## Specify packages to include in your package flight

After you've saved your package flight details, you'll see its overview page. Click **Packages** to specify the packages you'd like to include in the flight. You can include packages targeting any OS version that your app supports.

You have the option to select packages that were associated with a previous published submission (either a non-flighted submission, or one of your other package flights, if you have more than one). If you need to upload new packages to use for this package flight, you can upload them here (using the [same process as when you upload app packages to a regular non-flighted submission](publish-your-app/upload-app-packages.md?pivots=store-installer-msix)). Click **Save** when you have finished specifying the packages to be included in this package flight.

If your app supports multiple device families, make sure you include packages to support the same set of device families in your flight. People in your flight groups will **only** be able to get packages from that flight. They won't be able to access packages from other flights, or from your non-flighted submission. 

Also remember that your Store listing info and device family availability is based on your non-flighted submission. Customers in your flight groups will only be able to download the app on a device family that is supported by your non-flighted submission. For more info, see [Device family support](#device-family-support). 


## Gradual package rollout

By default, the packages in your submission will be made available to everyone in your flight group at the same time. To change this, you can check the box that says **Roll out update gradually after this submission is published (to Windows 10 or Windows 11 customers only)**. You can choose a percentage of people in your flight group to get the packages from the new submission, so that you can monitor feedback and analytic data to make sure you’re confident about the update before rolling it out more broadly to the rest of the flight group. You can increase the percentage (or halt the update) any time without having to create a new submission for your package flight. 

> [!IMPORTANT]
> When gradually rolling out packages in a package flight, the people who aren't included in the percentage that gets your new packages will get the packages from the previous package flight submission (unless there is a higher-ranked flight available to them).

For more info, see [Gradual package rollout](gradual-package-rollout.md).


## Configure additional package flight options

By default, your package flight will be published and made available to your flight group as soon as it completes the certification process. If you'd like to change the [publish date](publish-your-app/price-and-availability.md?pivots=store-installer-msix#publish-date), you can do so in the **Flight options** section. Click **Save** to return to the package flight overview page. 


## Submit your package flight to the Store

When you've specified packages and configured any options needed, click **Submit to the Store**. Your package flight will then go through the [app certification process](publish-your-app/app-certification-process.md?pivots=store-installer-msix). 

Note that the [certification process](publish-your-app/app-certification-process.md?pivots=store-installer-msix) is applied to package flights just the same as any submission, however some WACK failures are reported as **passing with notes** and will allow submission for flighting. This relaxation of the WACK checks is only while the package is flighting to a limited audience and is intended  to assist with package testing and preparation for release. WACK failures must be fixed before general release.

People in your flight group(s) associated with that package flight who already have your app will now get an update using the packages you included in your package flight. If those people don’t have your app yet, they’ll get the packages from your package flight when they install it. 

> [!NOTE]
> People who have a package that is only available in a package flight can give the app a star rating and leave reviews, but their ratings and reviews won’t be shown to other customers. (This excludes legacy 7.x or 8.0 XAP packages; ratings and reviews left by members of your flight groups using those packages will be visible to other customers.) You can see ratings and feedback from all customers, including those in your flight groups, in the **Reviews** and **Feedback** reports for the app.


## Device family support

In most cases, you’ll want to include packages that support the same set of device families supported by your non-flighted submission. Device family availability for an app will always be based on the non-flighted submission, whether or not a customer is in a flight group.

**If your non-flighted submission supports a device family that your package flight doesn’t support**, people in your flight group won’t be able to download the app on that device family. For example, if your non-flighted submission includes Mobile and Desktop packages, and you then create a package flight that only includes a Mobile package, people in your flight group will only be able to download the app on mobile devices, even though you do have a desktop package available to customers who aren’t in the flight. Even if you're only using the package flight to test changes in your Mobile package, you should include the Desktop package from your non-flighted submission in the package flight so that customers in the flight group are able to download your app on desktop devices.

**If your package flight supports a device family that your non-flighted submission doesn’t support**, no one will be able to download the app on that device family, whether they’re in your flight group or not. For example, if your non-flighted submission only includes a Mobile package, and you then create a package flight that includes both Mobile and Desktop packages, people in your flight group will still only be able to download the app on mobile devices. The desktop package won’t be offered to anyone, even people in your flight group. If you want to make a desktop package available to people in your flight group, you’ll need to first update your non-flighted submission to include a desktop package. For the best experience for all of your app’s customers, your non-flighted submission should support the same device families as your package flight. 

> [!NOTE]
> Packages added to your package flights can support any OS version (or any build of Windows 10 or Windows 11), but as noted above, people in flight groups running Windows 10 must be using a device running a version that supports package flights (Windows.Desktop build 10586 or later; Windows.Mobile build 10586.63 or later) in order to get packages from the package flight.


## Update or modify your package flight

To create a new submission for a package flight you've already published, click **Update** next to the flight name on your App overview page. You can then upload new packages (and remove unneeded packages), just as you would with a non-flighted submission. Make any other needed changes, and then click **Submit to the Store** to send the updated package flight through the [app certification process](publish-your-app/app-certification-process.md?pivots=store-installer-msix).

To modify an existing flight without creating and submitting a new update, click **Modify** next to the flight name. This lets you change details such as the flight groups, name, and rank, without  requiring that the package flight go through the certification process again. Note that if you have an update in progress, or if your package flight hasn’t been published yet, you won’t see the **Modify** option. 


## Add and rank additional package flights

You can create multiple package flights for the same app in order to distribute several different packages to different sets of customers. 

Once you have created your first package flight, you create another by following the process outlined above. The only difference is that if you've already created one package flight, you'll need to specify the priority order of all package flights in the **Rank** section. This lets the Store determine which package to give to any individual customer if they are in more than one of your flight groups. People in your flight groups will always get the highest-ranked package flight available to them, even if a lower-ranked package flight contains packages with a higher version number.

By default, your new package flight will be ranked highest. If you'd like to change its rank, you can move it down (or back up) to place it in the right location among your other package flights.

Note that your non-flighted submission is always ranked the lowest (#1). That is, people who aren’t in any of your flight groups can only get packages from your non-flighted submission through the Store. People in a flight group will always get packages from the highest-ranked package flight available to them (but never the non-flighted submission, since it has the lowest rank). This gives you flexibility in determining how to distribute your packages to people who may be members of more than one of your flight groups.

For example, let's say you want to create two package flights in addition to your regular non-flighted submission: one that is relatively stable and ready for testing with a wide audience, and one that you're not so sure about and want to limit to only a few testers. You could create a flight group called Testers and include it in a package flight called Tester Flight, then create a flight group called Enthusiasts with a larger membership and include it in another package flight called Enthusiast Flight. If you rank Tester Flight higher than Enthusiast Flight, you can use packages that you're fairly confident about in Enthusiast Flight, while using riskier packages meant for Testers only in Tester Flight. Members of your Testers group will always get the packages you provide in Tester Flight, even if they also belong to your Enthusiasts group. (Then later, if it turns out that the packages in Tester Flight are performing well, you could update Enthusiast Flight to use the packages originally distributed to Tester Flight—and maybe eventually use those packages in your non-flighted submission.)


## Make packages from a package flight available to all your customers

If you decide that one or more of the packages you included in a published package flight should be made available to customers who aren’t in a flight group, you can update your non-flighted submission to use those packages, without having to upload the same packages all over again. 

When you create your new submission, on the [**Packages**](publish-your-app/upload-app-packages.md?pivots=store-installer-msix) page you’ll see a drop-down with the option to copy packages from one of your package flights. Select the package flight that has the packages you want to pull in. You can then select any or all of its packages to include in the non-flighted submission.

Note that all of the same package validation rules will apply, even when using packages from a previously published submission. 


## Delete a package flight

To delete a package flight that you no longer want to support, click its name from the App overview page. On the flight overview page, click **Modify**, then click the **Delete** link to delete the package flight. (If you have an unpublished submission of the package flight in progress, you’ll need to delete that submission first.) It may take up to 30 minutes for this to be completed.

When you delete a package flight, any customers who have the packages you distributed in that package flight will get an app update if there is a package with a higher version number (or as soon as such a package becomes available). If they uninstall the app and then install it again later, this will be treated as a new acquisition, and they'll get the highest version currently available.
