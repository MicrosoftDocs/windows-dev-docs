---
description: You can publish line-of-business (LOB) apps directly to enterprises for volume acquisition via the Microsoft Store for Business or Microsoft Store for Education, without making the apps broadly available in the Store.
title: Distribute LOB apps to enterprises
ms.assetid: 2050126E-CE49-4DE3-AC2B-A572AC895158
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp, lob, line-of-business, enterprise apps, store for business, store for education, enterprise
ms.localizationpriority: medium
---

# Distribute LOB apps to enterprises

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

For more info, see [Organizational licensing options](organizational-licensing.md).

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
