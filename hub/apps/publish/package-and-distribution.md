---
description: Details of how to package and distribute your app.
title: Package and distribution
ms.assetid: AC3A45E8-7BBD-44E9-B2D3-B74B7C9B2BC9
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Package and Distribution

## Generate preinstall packages for OEMs

If your developer account has been granted the appropriate permissions, you can generate and download preinstall packages so that an OEM can include your app in their OS image. Preinstall permissions are only enabled on developer accounts that are sponsored by OEMs.

## Important preinstall policy & limitations

Preinstall apps must be certified through [Partner Center](https://partner.microsoft.com/dashboard) to have the latest Store license so that they are able to connect to the Store and receive app updates.

Any app that is preinstalled must be and remain free in all markets.

## Generating preinstall packages

Once an account has been enabled with preinstall permissions, complete the following steps:

1. In Partner Center, navigate to the app that is to be preinstalled.
1. In the left navigation menu, expand **App management** and select **Current packages**.
1. In the **Request packages for OS preinstallation** section, select **Enable downloadable packages**.
1. In the confirmation dialog will, select **Enable**.
1. Find the package that you want to download and select the appropriate **Generate package** link.

    > [!NOTE]
    > Generation time for preinstall packages will vary depending on the size of the package you have selected. You can leave this page and come back later, or you can leave the page open while your package is being generated.

1. After the package has been generated, a link to **Download package** will appear. Select this link to download the .zip file.

You can then provide the .zip file to the OEM for inclusion in their OS image.

## Support

If you have further questions about generating preinstall packages, please email <storeops@microsoft.com>.


## Organizational licensing options

You can indicate whether and how your app can be offered for volume purchases through Microsoft Store for Business and Microsoft Store for Education in the **Organizational licensing** section of the [Pricing and availability](publish-your-app/msix/price-and-availability.md#organizational-licensing) page of an app submission.

Through these settings, you can opt to allow your app to be made available to organizations (business and educational) who acquire and deploy multiple licenses for their users, providing an opportunity to increase your reach to organizations across Windows 10 device types, including PCs, tablets and phones.

You will also need to allow organizational licensing for any [line-of-business (LOB) apps](#distribute-lob-apps-to-enterprises) that you publish directly to enterprises.

> [!NOTE]
> Selections for each of your apps are configured independently from each other. You may change your preferences for an app at any time by creating a new submission, and your changes will take effect after the submission completes the [certification process](publish-your-app/msix/app-certification-process.md).

> [!IMPORTANT]
> Submissions that use the [Microsoft Store submission API](/windows/uwp/monetize/create-and-manage-submissions-using-windows-store-services) won't be made available to Microsoft Store for Business and Microsoft Store for Education. To make your app available for volume purchases by organizations, you must create and submit your submissions in Partner Center.


## Allowing your app to be offered to organizations

By default, the box labeled **Make my app available to organizations with Store-managed (online) licensing and distribution** is checked. This means that you wish your app to be available for inclusion in catalogs of apps that will be made available to organizations for volume acquisition, with app licenses managed through the Store's online licensing system.

> [!NOTE]
> This does not guarantee that your app will be made available to all organizations.

If you prefer not to allow us to offer your app to organizations for volume acquisition, uncheck this box. Note that this change will only take place after the app completes the certification process. If any organizations had previously acquired licenses to your app, those licenses will still be valid, and the people who have the app already can continue to use it.

> [!TIP]
> To publish line-of-business (LOB) apps exclusively to a specific organization, you can set up an enterprise association and allow the organization to add the apps directly their private store. For more info, see [Distribute LOB apps to enterprises](#distribute-lob-apps-to-enterprises).


## Allowing disconnected (offline) licensing

Many organizations need apps enabled for offline licensing. For example, some organizations need to deploy apps to devices which rarely or never connect to the internet. If you want to allow your app to be made available to these customers, check the box labeled **Allow organization-managed (offline) licensing and distribution for organizations**.

Note that this box is **unchecked** by default. You must check the box to allow us to make your app available to verified organizations who will install it using organization-managed (offline) licensing. Organizations must go through additional validation in order to install paid apps to their end users in this way.

Offline licensing allows organizations to acquire your app on a volume basis, and then install the app without requiring each device to contact the Store's licensing system. The organization is able to download your app's package along with a license which lets them install it to devices (via their own management tools or by preloading apps on OS images) without notifying the Store when a particular license has been used. Enabling this scenario greatly increases deployment flexibility, and it may substantially increase the attractiveness of your app with these customers.

> [!IMPORTANT]
> Offline licensing is not supported for .xap packages.

 
## Paid app support

Currently, developer accounts located in certain markets are able to offer paid apps for volume acquisition through Microsoft Store for Business. 

> [!NOTE]
> In some markets, the price shown for an app in Microsoft Store for Business or Microsoft Store for Education may be different than the price shown to retail customers in the Microsoft Store for the same price tier. Payout of proceeds from organizational purchases works just the same as it does for consumer purchases of your app. For more info, see [Getting paid](/partner-center/marketplace-get-paid) and the [App Developer Agreement](https://go.microsoft.com/fwlink/?linkid=528905). For a list of markets where Microsoft Store for Business and Microsoft Store for Education are available, see [Microsoft Store for Business and Microsoft Store for Education overview](/windows/manage/windows-store-for-business-overview#supported-markets).

If your country or region is not listed below, your paid apps currently will not be offered in Microsoft Store for Business and Microsoft Store for Education. If this is the case, the organizational licensing selections you make for your paid apps may be applied at a later time, as we may add support for submissions from additional developer account markets in the future.

At this time, developers located in the following countries and regions can distribute paid apps to organizational customers via Microsoft Store for Business and Microsoft Store for Education:

- Austria
- Belgium
- Bulgaria
- Canada
- Croatia
- Cyprus
- Czechia
- Denmark
- Estonia
- Finland
- France
- Germany
- Greece
- Hungary
- Ireland
- Isle of Man
- Italy
- Latvia
- Liechtenstein
- Lithuania
- Luxembourg
- Malta
- Monaco
- Netherlands
- Norway
- Poland
- Portugal
- Romania
- Slovakia
- Slovenia
- Spain
- Sweden
- Switzerland
- United Kingdom
- United States

## Package flights

You can use package flights to distribute specific packages to a limited group of testers. The packages you've already published to the Store will be used for your other customers, so their experience won't be disrupted.

With package flights, only the packages are different; the Store listing details will be the same for all of your customers. Anyone in your flight group will receive the packages that you include in the package flight, while customers who aren't in the flight group continue to receive your regular (non-flighted) packages.  If you later decide that you want to make packages from a package flight available to all your customers, you can easily use those same packages in a non-flighted submission. 

Note that the [certification process](publish-your-app/msix/app-certification-process.md) is applied to package flights just the same as any submission, however some WACK failures are reported as **passing with notes** and will allow submission for flighting. This relaxation of the WACK checks is only while the package is flighting to a limited audience and is intended  to assist with package testing and preparation for release. WACK failures must be fixed before general release.   

When you set up package flights, you can specify the people who should get specific packages by adding them to a **known user group** (sometimes referred to as flight group). Anyone in a flight group who is using a device running a version of Windows 10 or Windows 11 that supports package flights (Windows.Desktop build 10586 or later; or Xbox One) will get the packages from the package flight(s) that you designate for that particular group. Anyone who has not been added to one of your flight groups, or is using a device that doesn’t support package flights, will get packages from the non-flighted submission.

> [!IMPORTANT] 
> On desktop devices, people in your flight groups will get the packages in your flight automatically whenever you provide updates. However, **people in your flight groups who are using Xbox devices will need to check for updates manually** in order to get the latest packages, making sure they are signed into their device using their Microsoft account (with the associated email address that you included in your known user group).

Note that package flights will not be distributed via [Microsoft Store for Business](https://businessstore.microsoft.com/store) and [Microsoft Store for Education](https://educationstore.microsoft.com/store). This is because people in your known user groups must be signed in with their Microsoft accounts in order to receive a package flight. All acquisitions made via Microsoft Store for Business or Microsoft Store for Education will receive your non-flighted packages.

> [!TIP]
> Package flights offer packages only to the selected customers that you specify. To distribute packages to a random selection of customers in a specified percentage, you can use [gradual package rollout](#gradual-package-rollout). You can also combine rollout with your package flights if you want to gradually distribute an update to one of your flight groups.
>
> Unlike package flights, your gradual package rollout selections do apply to customers who acquire your app via Microsoft Store for Business and Microsoft Store for Education. 

> [!TIP]
> Consider how the people in your package flight will be able to give their input about the app. We suggest [adding a control into your app to launch Feedback Hub](/windows/uwp/monetize/launch-feedback-hub-from-your-app) so that customers can provide their input directly; you can then review their feedback in your app's [Feedback report](feedback-report.md)).


## Create a new package flight

After you have published a submission for your app, you'll see a **Package flights** section on the App overview page. Click **New package flight** to get started.

If you haven't created any known user groups yet, you'll be prompted to create one before you can proceed. For more info, see [Create known user groups](create-customer-groups.md#create-known-user-groups). You can create a new known user group directly from this page by selecting **Create a flight group**.

On the package flight creation page, you'll need to enter a name for your flight and specify at least one flight group. Once you've done so, select **Create flight**. You won't be able to change these details later (though if you're not happy with what you've entered, you can delete this flight and create a new one to use instead).

> [!NOTE]
> If you have more than one package flight, you'll need to assign a rank to each one. For more info, see [Add and rank additional package flights](#add-and-rank-additional-package-flights) below.


## Specify packages to include in your package flight

After you've saved your package flight details, you'll see its overview page. Click **Packages** to specify the packages you'd like to include in the flight. You can include packages targeting any OS version that your app supports.

You have the option to select packages that were associated with a previous published submission (either a non-flighted submission, or one of your other package flights, if you have more than one). If you need to upload new packages to use for this package flight, you can upload them here (using the [same process as when you upload app packages to a regular non-flighted submission](publish-your-app/msix/upload-app-packages.md)). Click **Save** when you have finished specifying the packages to be included in this package flight.

If your app supports multiple device families, make sure you include packages to support the same set of device families in your flight. People in your flight groups will **only** be able to get packages from that flight. They won't be able to access packages from other flights, or from your non-flighted submission. 

Also remember that your Store listing info and device family availability is based on your non-flighted submission. Customers in your flight groups will only be able to download the app on a device family that is supported by your non-flighted submission. For more info, see [Device family support](#device-family-support). 

## Configure additional package flight options

By default, your package flight will be published and made available to your flight group as soon as it completes the certification process. If you'd like to change the [publish date](publish-your-app/msix/price-and-availability.md#publish-date), you can do so in the **Flight options** section. Click **Save** to return to the package flight overview page. 


## Submit your package flight to the Store

When you've specified packages and configured any options needed, click **Submit to the Store**. Your package flight will then go through the [app certification process](publish-your-app/msix/app-certification-process.md). 

Note that the [certification process](publish-your-app/msix/app-certification-process.md) is applied to package flights just the same as any submission, however some WACK failures are reported as **passing with notes** and will allow submission for flighting. This relaxation of the WACK checks is only while the package is flighting to a limited audience and is intended  to assist with package testing and preparation for release. WACK failures must be fixed before general release.

People in your flight group(s) associated with that package flight who already have your app will now get an update using the packages you included in your package flight. If those people don’t have your app yet, they’ll get the packages from your package flight when they install it. 

> [!NOTE]
> People who have a package that is only available in a package flight can give the app a star rating and leave reviews, but their ratings and reviews won’t be shown to other customers. You can see ratings and feedback from all customers, including those in your flight groups, in the **Reviews** and **Feedback** reports for the app.


## Device family support

In most cases, you’ll want to include packages that support the same set of device families supported by your non-flighted submission. Device family availability for an app will always be based on the non-flighted submission, whether or not a customer is in a flight group.

**If your non-flighted submission supports a device family that your package flight doesn’t support**, people in your flight group won’t be able to download the app on that device family.

**If your package flight supports a device family that your non-flighted submission doesn’t support**, no one will be able to download the app on that device family, whether they’re in your flight group or not. For the best experience for all of your app’s customers, your non-flighted submission should support the same device families as your package flight. 

> [!NOTE]
> Packages added to your package flights can support any OS version (or any build of Windows 10 or Windows 11), but as noted above, people in flight groups running Windows 10 must be using a device running a version that supports package flights (Windows.Desktop build 10586 or laterr) in order to get packages from the package flight.


## Update or modify your package flight

To create a new submission for a package flight you've already published, click **Update** next to the flight name on your App overview page. You can then upload new packages (and remove unneeded packages), just as you would with a non-flighted submission. Make any other needed changes, and then click **Submit to the Store** to send the updated package flight through the [app certification process](publish-your-app/msix/app-certification-process.md).

To modify an existing flight without creating and submitting a new update, click **Modify** next to the flight name. This lets you change details such as the flight groups, name, and rank, without  requiring that the package flight go through the certification process again. Note that if you have an update in progress, or if your package flight hasn’t been published yet, you won’t see the **Modify** option. 


## Add and rank additional package flights

You can create multiple package flights for the same app in order to distribute several different packages to different sets of customers. 

Once you have created your first package flight, you create another by following the process outlined above. The only difference is that if you've already created one package flight, you'll need to specify the priority order of all package flights in the **Rank** section. This lets the Store determine which package to give to any individual customer if they are in more than one of your flight groups. People in your flight groups will always get the highest-ranked package flight available to them, even if a lower-ranked package flight contains packages with a higher version number.

By default, your new package flight will be ranked highest. If you'd like to change its rank, you can move it down (or back up) to place it in the right location among your other package flights.

Note that your non-flighted submission is always ranked the lowest (#1). That is, people who aren’t in any of your flight groups can only get packages from your non-flighted submission through the Store. People in a flight group will always get packages from the highest-ranked package flight available to them (but never the non-flighted submission, since it has the lowest rank). This gives you flexibility in determining how to distribute your packages to people who may be members of more than one of your flight groups.

For example, let's say you want to create two package flights in addition to your regular non-flighted submission: one that is relatively stable and ready for testing with a wide audience, and one that you're not so sure about and want to limit to only a few testers. You could create a flight group called Testers and include it in a package flight called Tester Flight, then create a flight group called Enthusiasts with a larger membership and include it in another package flight called Enthusiast Flight. If you rank Tester Flight higher than Enthusiast Flight, you can use packages that you're fairly confident about in Enthusiast Flight, while using riskier packages meant for Testers only in Tester Flight. Members of your Testers group will always get the packages you provide in Tester Flight, even if they also belong to your Enthusiasts group. (Then later, if it turns out that the packages in Tester Flight are performing well, you could update Enthusiast Flight to use the packages originally distributed to Tester Flight—and maybe eventually use those packages in your non-flighted submission.)


## Make packages from a package flight available to all your customers

If you decide that one or more of the packages you included in a published package flight should be made available to customers who aren’t in a flight group, you can update your non-flighted submission to use those packages, without having to upload the same packages all over again. 

When you create your new submission, on the [**Packages**](publish-your-app/msix/upload-app-packages.md) page you’ll see a drop-down with the option to copy packages from one of your package flights. Select the package flight that has the packages you want to pull in. You can then select any or all of its packages to include in the non-flighted submission.

Note that all of the same package validation rules will apply, even when using packages from a previously published submission. 


## Delete a package flight

To delete a package flight that you no longer want to support, click its name from the App overview page. On the flight overview page, click **Modify**, then click the **Delete** link to delete the package flight. (If you have an unpublished submission of the package flight in progress, you’ll need to delete that submission first.) It may take up to 30 minutes for this to be completed.

When you delete a package flight, any customers who have the packages you distributed in that package flight will get an app update if there is a package with a higher version number (or as soon as such a package becomes available). If they uninstall the app and then install it again later, this will be treated as a new acquisition, and they'll get the highest version currently available.

## Gradual package rollout

When you publish an update to a submission, you can choose to gradually roll out the updated packages to a percentage of customers who have your package installed on Windows 10 or Windows 11 (including Xbox). This allows you to monitor feedback and analytic data for the specific packages to make sure you’re confident about the update before rolling it out more broadly. You can increase the percentage (or halt the update) any time without having to create a new submission. 

> [!IMPORTANT]
> Your rollout selections apply to all of your packages, but will only apply to your customers running OS versions that support package flights (Windows.Desktop build 10586 or later and Xbox), including any customers who get the app via [Store-managed (online) licensing](#organizational-licensing) via [Microsoft Store for Business](https://businessstore.microsoft.com/store) or [Microsoft Store for Education](https://educationstore.microsoft.com/store). When using gradual package rollout, customers on earlier OS versions will not get packages from the latest submission until you finalize the package rollout as described below.

Note that all of your customers will see the Store listing details that you entered with your latest submission. The rollout settings only apply to the packages that customers install, for updates to existing customers.

> [!TIP]
> Package rollout distributes packages to a random selection of customers who have your package installed
in the percentages that you specify. To distribute specific packages to selected customers that you specify, you can use package flights. You can also combine rollout with your package flights if you want to gradually distribute an update to one of your flight groups.


## Setting the rollout percentage

You can select to roll out your update on the **Packages** page of an updated submission. To do so, check the box that says **Roll out update gradually after this submission is published (to Windows 10 or Windows 11 customers only)**. Then enter the percentage of customers who should get the update when the submission is first published. For example, you might enter 5 if you want to start by rolling the update out to only a small percentage of your app’s existing customers who have already installed this app.

Click **Update** to save your selections. After your app completes the certification process, the packages will be distributed to existing customers according to the percentage that you specified for updates.


## Adjusting the rollout after the submission is published

To adjust the rollout after the submission has been published, go to your app’s Overview page. You can drag the selector to change the percentage of customers getting the packages from your newest submission. Click **Update** to save your selections. The packages will then start to be distributed to existing customers who have your package installed according to the percentage that you specified for updates.


## Completing the rollout

Before you can create a new submission, you'll need to complete the package rollout. You can **finalize** the rollout and distribute the latest packages to all of your customers, or **halt** the rollout to stop distributing the latest packages.

If you have confidence in the update and would like to make it available to all of your customers, click **Finalize package rollout** to distribute the newest packages to all of your customers.

> [!TIP]
> Changing the rollout percentage to 100% does not ensure that all of your customers will get the packages from the latest submissions, because some customers may be on OS versions that don’t support rollout. You must finalize the rollout in order to stop distributing the older packages and update all existing customers to the newer ones.

If you find that there are problems with the update and you don’t want to distribute it any further, you can click **Halt package rollout** to stop distributing packages from the latest submission. Once you halt a package rollout, those packages will no longer be distributed to any customers; only the packages from the previous submission will be used for any new or updating customers. However, any customers who already had the newer packages will keep those packages; they won’t be rolled back to the previous version. To provide an update to these customers, you’ll need to create a new submission with the packages you’d like them to get. Note that if you use a gradual rollout in your next submission, customers who had the package you halted will be offered the new update in the same order they were offered the halted package. The new rollout will be between your last finalized submission and your newest submission; once you halt a package rollout, those packages will no longer be distributed to any customers.

## Beta testing and targeted distribution

No matter how carefully you test your app, there’s nothing like the real-world test of having other people use it. Your testers may discover issues that you’ve overlooked, such as misspellings, confusing app flow, or errors that could cause the app to crash. You’ll then have a chance to fix those problems before you release the submission to the public, resulting in a more polished final product. 

Partner Center gives you several options to let testers try out your app before you offer it to the public.

Whichever method you choose, here are some things to keep in mind as you beta test your app.

- You can’t revoke access to the app after a tester has downloaded it. Once they have downloaded the app, they can continue to use it, and they’ll get any updates that you subsequently publish.
- You will need to determine how you’d like to collect feedback from your testers. Consider providing a link that lets your testers easily give feedback via email (or via [Feedback Hub](/windows/uwp/monetize/launch-feedback-hub-from-your-app), if confidentiality is not a concern). 
- You can review [analytic reports](analytics.md) for your app, including usage and health reports and any ratings or reviews left by your testers.
- You can include add-ons when you distribute your app to testers. Since you probably don’t want to charge them money for an add-on, you can [generate promotional codes](generate-promotional-codes.md) and distribute them to your testers to let them get the add-on for free, or you can set the price for the add-on to **Free** during  testing (then before you make the app available to other customers, create a new submission for the add-on to change its price). Note that each add-on can only be purchased once per Microsoft account, so the same tester won't be able to test the add-on acquisition process more than one time. 
- You can give your testers an updated version of your app at any time by creating a new submission with new packages. Your testers will get the update after it goes through the certification process, just like they got the original package, but no one else will be able to get it (unless you make additional changes, such as moving an app from **Private audience** to **Public audience** or changing the membership of groups who can get it).

## Private audience

If you want to let testers use your app before it’s available to others, and make sure that no one else can see its listing, use the **Private audience** option under [Visibility](publish-your-app/msix/visibility-options.md) (on the **Pricing and availability** page of your submission). This is the only method that lets you distribute your app to testers while completely preventing anyone else from seeing the app’s Store listing, even if they were able to type in its direct link. 

The **Private audience** option can only be used when you have not already published your app to a public audience. You can use this option with apps targeting any OS version, but your testers must be running Windows 10, version 1607 or later (including Xbox), and must be signed in with the Microsoft account associated with the email address that you provide.

For more info, see [Private audience](publish-your-app/msix/visibility-options.md#audience).

## Hiding the app in the Store and using promotional codes

This option offers another way to limit distribution of an app to only a certain group of testers, while preventing anyone else from discovering your app in the Store (or acquiring it without a promotional code). However, unlike the private audience option, it could be possible for anyone to see your app’s listing if they have the direct link. If confidentiality is critical for your submission, we recommend publishing to a private audience instead.

Hiding the app and using promotional codes can be used with apps targeting any OS version, but your testers can only get the app if they are running Windows 10 or Windows 11.

To use this option:

- In the **Visibility** section of the **Pricing and availability** page, under [Discoverability](publish-your-app/msix/visibility-options.md#discoverability), select **Make this product available but not discoverable in the Store**. Choose the option for **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 10 or Windows 11 device**. 
- After the app passes certification, [generate promotional codes](generate-promotional-codes.md) for the app and distribute them to your testers. You can generate codes that allow up to 1600 redemptions for a single app in a six month period. These codes will give your testers a direct link to the app’s listing, and will allow them to download it for free, even if you have set a price for it when you created your submission.
- When you're ready to make your app available to the public, create a new submission and change the **Visibility** option to **Make this product available and discoverable in the Store** (along with any other changes you'd like).


## Targeted distribution with a link to your app's listing

With this, no customers will be able to find the app by searching or browsing the Store, but anyone with the direct link to its Store listing can download it on a device running on Windows 10 or Windows 11. Keep in mind that in order for your testers to download the app at no cost, you must set its price to **Free**.

To use this option:
- In the **Visibility** section of the **Pricing and availability** page, under [Discoverability](publish-your-app/msix/visibility-options.md#discoverability), select **Make this product available but not discoverable in the Store**. Choose the option for **Direct link only: Any customer with a direct link to the product’s listing can download it, except on Windows 8.x.**.
- After your product has been published, distribute the link (the **URL** on the [App identity page](view-app-identity-details.md)) to your testers so they can try it out.
- When you're ready to make your app available to the public, create a new submission and change the **Visibility** option to **Make this product available and discoverable in the Store** (along with any other changes you'd like).

## Distribute LOB apps to enterprises

You have several options for distributing line of business (LOB) apps to your organization’s users using [MSIX packages](/windows/msix/) without making the apps broadly available to the public. You can use device management tools, configure an App Installer-based deployment, sideload the apps directly, or publish the apps to the Microsoft Store for Business or Microsoft Store for Education.

## Microsoft Endpoint Configuration Manager and Microsoft Intune

If your organization uses Microsoft Endpoint Configuration Manager or Microsoft Intune to manage devices, you can deploy LOB apps using these tools. For more information, see these articles:

* [Introduction to application management in Configuration Manager](/configmgr/apps/understand/introduction-to-application-management)
* [Overview of the app lifecycle in Microsoft Intune](/intune/apps/app-lifecycle)

## App Installer

App Installer enables Windows 10 or Windows 11 apps to be installed by double-clicking an MSIX app package directly, or by double-clicking an .appinstaller file that installs the app package from a web server. This means that users don't need to use PowerShell or other developer tools to install LOB apps. App Installer can also install app packages that include optional packages and related sets.

App Installer can be downloaded for offline use in the enterprise from Microsoft Store for Business [web portal](https://businessstore.microsoft.com/store/details/app-installer/9NBLGGH4NNS1). For more information about App Installer, see [Install Windows 10 or Windows 11 apps with App Installer](/windows/msix/app-installer/app-installer-root).

## Sideloading

Another option for distributing LOB apps directly to users in your organization is sideloading. This option is similar to App Install-based deployment in that it enables users to install MSIX app packages directly. Starting in Windows 10 version 2004, sideloading is enabled by default and users can install apps by double-clicking signed MSIX app packages. On Windows 10 version 1909 and earlier, sideloading requires some additional configuration and the use of a PowerShell script. For more info, see [Sideload LOB apps in Windows 10 or Windows 11](/windows/application-management/sideload-apps-in-windows-10).

## Microsoft Store for Business or Microsoft Store for Education

> [!NOTE]
> Microsoft [has announced](/lifecycle/announcements/microsoft-store-for-business-education-retiring) the retirement of the Microsoft Store for Business and Microsoft Store for Education in 2023. The instructions below are therefore no longer valid. For the replacement, please refer to [Add a Windows line-of-business app to Microsoft Intune](/mem/intune/apps/lob-apps-windows).

You can publish line-of-business (LOB) apps directly to enterprises for volume acquisition via Microsoft Store for Business or Microsoft Store for Education, without making the apps broadly available in the Store. When using this option, the apps are signed by the Store and must comply with the standard Store Policies.

> [!NOTE]
> At this time, only free apps can be distributed exclusively to enterprises via Microsoft Store for Business or Microsoft Store for Education. If you submit a paid app as LOB, it will not be available to the enterprise. 

> [!IMPORTANT]
> You cannot use the [Microsoft Store submission API](/windows/uwp/monetize/create-and-manage-submissions-using-windows-store-services) to publish LOB apps directly to enterprises. All submissions for LOB apps must be published through Partner Center.

### Set up the enterprise association

The first step in publishing LOB apps exclusively to an enterprise is to establish the association between your account and the enterprise’s private store.

> [!IMPORTANT]
> This association process must be initiated by the enterprise, and must use the email address associated with the Microsoft account that was used to create the developer account. For more info, see [Working with line-of-business apps](/microsoft-store/working-with-line-of-business-apps).

When an enterprise chooses to invite you to publish apps for their exclusive use, you’ll get an email that includes a link to confirm the association. You can also confirm these associations by going to the **Enterprise associations** section of your **Account settings** (as long as you are signed in with the Microsoft account that was used to open the developer account).

To confirm the association, click **Accept**. Your account will then be able to publish apps for that enterprise’s exclusive use.

### Submit LOB apps

Once you’re ready to publish an app for an enterprise’s exclusive use, the process is similar to the app submission process. The app goes through the same [certification process](publish-your-app/msix/app-certification-process.md), and must comply with all [Microsoft Store Policies](store-policies.md). There are just a few parts of the process that are different.

#### Visibility

After you've set up an enterprise association, every time you submit an app you’ll see a drop-down box in the **Visibility** section of the submission’s **Pricing and availability** page. By default, this is set to **Retail distribution**. To make the app exclusive to an enterprise, you’ll need to choose **Line-of-business (LOB) distribution**.

Once **Line-of-business (LOB) distribution** is selected, the usual **Visibility** options will be replaced with a list of the enterprises to which you can publish exclusive apps. No one outside of the enterprise(s) you select will be able to view or download the app.

You must select at least one enterprise in order to publish an app as line-of-business.

<span id="organizational"></span>

#### Organizational licensing

By default, the box for **Store-managed (online) volume licensing** is checked when you submit an app. When publishing LOB apps, this box must remain checked so that the enterprise can acquire your app in volume. This will not make the app available to anyone outside of the enterprise(s) that you selected in the **Distribution and visibility** section.

If you’d like to make the app available to the enterprise via disconnected (offline) licensing, you can check the **Disconnected (offline) licensing** box as well.

#### Age ratings

For LOB apps, the [age ratings](publish-your-app/msix/age-ratings.md) step of the submission process works the same as for retail apps, but you also have an additional option that allows you to indicate the Store age rating of your app manually rather than completing the questionnaire or importing an existing IARC rating ID. This manual rating can only be used with LOB distribution, so if you ever change the **Visibility** setting of the app to **Retail distribution**, you'll need to take the age ratings questionnaire before you can publish the submission.

### Enterprise deployment of LOB apps

After you click **Submit to the Store**, the app will go through the certification process. Once it’s ready, an admin for the enterprise must add it to their private store in the Microsoft Store for Business or Microsoft Store for Education portal. The enterprise can then deploy the app to its users.

> [!NOTE]
> In order to get your LOB app, the organization must be located in a [supported market](/windows/whats-new/windows-store-for-business-overview#supported-markets), and you must not have [excluded that market](publish-your-app/msix/market-selection.md) when submitting your app. 

For more info, see [Working with line-of-business apps](/microsoft-store/working-with-line-of-business-apps) and [Distribute apps using your private store](/microsoft-store/distribute-apps-from-your-private-store).

### Update LOB apps

To publish updates to an app that you’ve already published as LOB, simply create a new submission. You can upload new packages or make any other changes, then click **Submit to the Store** to make the updated version available. Be sure to keep the enterprise selections in **Visibility** the same, unless you intentionally want to make changes such as selecting an additional enterprise to acquire the app, or removing one of the enterprises to which you’d previously distributed it.

If you want to stop offering an app that you’ve previously published as line-of-business, and prevent any new acquisitions, you’ll need to create a new submission. First, you’ll need to change your **Visibility** selection from **Line-of-business (LOB) distribution** to **Retail distribution**. Then, in the [Discoverability](publish-your-app/msix/visibility-options.md#discoverability) section, choose **Make this product available but not discoverable in the Store** with the **Stop acquisition** option.

After the submission goes through the certification process, the app will no longer be available for new acquisitions (although anyone who already has it will continue to be able to use it).

> [!NOTE]
> When changing an app to **Retail distribution**, you'll need to complete the [age ratings questionnaire](publish-your-app/msix/age-ratings.md) if you haven't done so already, even if the app will not be available for new acquisitions.

