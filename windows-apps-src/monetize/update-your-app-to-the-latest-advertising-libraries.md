---
description: Learn how to update your app to use the latest supported Microsoft advertising libraries and make sure that your app continues to receive banner ads.
title: Use the latest advertising libraries for banner ads
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, AdControl, AdMediatorControl, migrate
ms.assetid: f8d5b2ad-fcdb-4891-bd68-39eeabdf799c
ms.localizationpriority: medium
---
# Update your app to the latest advertising libraries for banner ads

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

As of April 1, 2017, we no longer serve banner ads to apps that use an unsupported advertising SDK release. If you use **AdControl** to display banner ads in your Universal Windows Platform (UWP) app, use the information in this article to determine whether you are using an unsupported advertising SDK and migrate your app to a supported SDK.

## Overview

UWP apps that show banner ads must use **AdControl** from the advertising libraries that are distributed in the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK). This SDK supports a minimum set of advertising capabilities, including the ability to serve HTML5 rich media via the [Mobile Rich-media Ad Interface Definitions (MRAID) 1.0 specification](https://www.iab.com/wp-content/uploads/2015/08/IAB_MRAID_VersionOne.pdf) from the Interactive Advertising Bureau (IAB). Many of our advertisers seek these capabilities, and we require app developers to use one of these SDK releases to help make our app ecosystem more attractive to advertisers and ultimately drive more revenue to you.

Before this SDK was released, we previously provided the **AdControl** class in several older advertising SDK releases. These older advertising SDK releases are no longer supported because they do not support the minimum advertising capabilities described above. As of April 1, 2017, we no longer serve banner ads to apps that use an unsupported advertising SDK release. If you have an app that still uses an unsupported advertising SDK release, you will see the following behavior:

* Banner ads will no longer be served to any **AdControl** in your app, and you will no longer earn advertising revenue from those controls.

* When the **AdControl** in your app requests a new ad, the **ErrorOccurred** event of the control will be raised and the **ErrorCode** property of the event args will have the value **NoAdAvailable**.

* Any ad units that are associated with your app will be deactivated. You cannot remove these deactivated ad units from your DePartnerv Center account. If you update your app to use the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK), ignore these ad units and create new ones.

* Banner ads will also no longer be served for any ad unit that is used in more than one app. Make sure that your ad units are each used in only one app.

If you have an existing app (already in the Store or still under development) that displays banner ads using **AdControl** and you aren't sure which advertising SDK is being used by your app, follow the instructions in this article to determine whether you need to update your app to a supported SDK. If you encounter any issues or you need assistance, please [contact support](https://support.microsoft.com/getsupport/hostpage.aspx?locale=EN-US&supportregion=EN-US&ccfcode=US&ln=EN-US&pesid=14654&oaspworkflow=start_1.0.0.0&tenant=store&supporttopic_L1=32136151).

> [!NOTE]
> If your app already uses the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) (for UWP apps), you do not need to make any further changes to your app.

## Prerequisites

* The complete source code and Visual Studio project files for your app that uses **AdControl**.
* The .appx package for your app.

> [!NOTE]
> If you no longer have the .appx package for your app but you do still have a development computer with the version of Visual Studio and the advertising SDK that was used to build your app, you can regenerate the .appx package in Visual Studio.

<span id="part-1" />

## Part 1: Determine whether you need to update your UWP app

Follow the instructions in the following sections to determine if you need to update your app.

1. Create a copy of the .appx package for your app so you do not disturb the original, rename the copy so it has a .zip extension, and extract the contents of the file.

2. Check the extracted contents of your app package:
  * If you see a Microsoft.Advertising.dll file, your app uses an old SDK and you must update your project by following the instructions in the sections below. Proceed to [Part 2](update-your-app-to-the-latest-advertising-libraries.md#part-2).
  * If you do not see a Microsoft.Advertising.dll file, your UWP app already uses the latest available advertising SDK and you do not need to make any changes to your project.


<span id="part-2" />

## Part 2: Install the latest SDK

If your app uses an old SDK release, follow these instructions to make sure you have the latest SDK on your development computer.

1. Make sure your development computer has Visual Studio 2015 or a later release installed.
    > [!NOTE]
    > If Visual Studio is open on your development computer, close it before you perform the following steps.

1.	Uninstall all prior versions of the Microsoft Advertising SDK and Ad Mediator SDK from your development computer.

2.	Open a **Command Prompt** window and run these commands to clean out any SDK versions that may have been installed with Visual Studio, but which may not appear in the list of installed programs on your computer:
    ```syntax
    MsiExec.exe /x{5C87A4DB-31C7-465E-9356-71B485B69EC8}
    MsiExec.exe /x{6AB13C21-C3EC-46E1-8009-6FD5EBEE515B}
    MsiExec.exe /x{6AC81125-8485-463D-9352-3F35A2508C11}
    ```

3.	Install the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK).

## Part 3: Update your project

Remove all existing references to the Microsoft advertising libraries from the project and follow [these instructions](install-the-microsoft-advertising-libraries.md#reference) to add the required references. This will ensure that your project uses the correct libraries. You can preserve your existing markup and code.

## Part 4: Test and republish your app

Test your app to make sure it displays banner ads as expected.

If the previous version of your app is already available in the Store, [create a new submission](../publish/app-submissions.md) for your updated app in Partner Center to republish your app.
