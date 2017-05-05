---
author: mcleanbyron
description: Learn how to update your app to use the latest supported Microsoft advertising libraries and make sure that your app continues to receive banner ads.
title: Update your app to the latest advertising libraries
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, AdControl, AdMediatorControl, migrate
ms.assetid: f8d5b2ad-fcdb-4891-bd68-39eeabdf799c
---

# Update your app to the latest advertising libraries

Apps that show banner ads must use **AdControl** or **AdMediatorControl** from one of the following SDKs:

  * [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp) (for UWP apps)
  * [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk) (for Windows 8.1 and Windows Phone 8.x apps)

These SDK releases support a minimum set of advertising capabilities, including the ability to serve HTML5 rich media via the [Mobile Rich-media Ad Interface Definitions (MRAID) 1.0 specification](http://www.iab.com/wp-content/uploads/2015/08/IAB_MRAID_VersionOne.pdf) from the Interactive Advertising Bureau (IAB). Many of our advertisers seek these capabilities, and we require app developers to use one of these SDK releases to help make our app ecosystem more attractive to advertisers and ultimately drive more revenue to you.

Before these SDKs were released, we previously provided these controls in several older advertising SDK releases for Windows and Windows Phone apps. These older advertising SDK releases are no longer supported because they do not support the minimum advertising capabilities described above.

As of April 1, 2017, we no longer serve banner ads to apps that use an unsupported advertising SDK release. If you have an app that still uses an unsupported advertising SDK release, you will see the following behavior:

* Banner ads will no longer be served to any **AdControl** or **AdMediatorControl** controls in your app, and you will no longer earn advertising revenue from those controls.

* When the **AdControl** or **AdMediatorControl** in your app requests a new ad, the **ErrorOccurred** event of the control will be raised and the **ErrorCode** property of the event args will have the value **NoAdAvailable**.

* Any ad units that are associated with that app will be deactivated. You cannot remove these deactivated ad units from your Dev Center account. If you update your app to use a supported advertising SDK, ignore these ad units and create new ones.

* Banner ads will also no longer be served for any ad unit that is used in more than one app. Make sure that your ad units are each used in only one app.

If you have an existing app (already in the Store or still under development) that displays banner ads using **AdControl** or **AdMediatorControl** and you aren't sure which advertising SDK is being used by your app, follow the instructions in this article to determine whether you need to update your app to a supported SDK. If you encounter any issues or you need assistance, please [contact support](http://go.microsoft.com/fwlink/?LinkId=393643).

> [!NOTE]
> If your app already uses the [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp) (for UWP apps) or [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk) (for Windows 8.1 and Windows Phone 8.x apps), or you have previously updated your app to use one of these SDKs, your app already uses the latest available SDK and you do not need to make any further changes to your app.

## Prerequisites

* The complete source code and Visual Studio project files for your app that uses **AdControl** or **AdMediatorControl**.
* The .appx or .xap package for your app.
    > [!NOTE]
    > If you no longer have the .appx or .xap package for your app but you do still have a development computer with the version of Visual Studio and the advertising SDK that was used to build your app, you can regenerate the .appx or .xap package in Visual Studio.

<span id="part-1" />
## Part 1: Determine whether you need to update your app

Follow the instructions in the following sections to determine if you need to update your app.

### Your app uses AdControl

If your app uses **AdControl** to display banner ads, follow these instructions.

**UWP apps for Windows 10**

1. Create a copy of the .appx package for your app so you do not disturb the original, rename the copy so it has a .zip extension, and extract the contents of the file.

2. Check the extracted contents of your app package:
  * If you see a Microsoft.Advertising.dll file, your app uses an old SDK and you must update your project by following the instructions in the sections below. Proceed to [Part 2](update-your-app-to-the-latest-advertising-libraries.md#part-2).
  * If you do not see a Microsoft.Advertising.dll file, your UWP app already uses the latest available advertising SDK and you do not need to make any changes to your project.

<span/>

**Windows 8.1 or Windows Phone 8.x apps**

1. Create a copy of the the .appx or .xap package for your app so you do not disturb the original, rename the copy so it has a .zip extension, and extract the contents of the file.

2. For XAML or JavaScript/HTML apps, check the extracted contents of your app package:
  * If  you see a Microsoft.Advertising.winmd file but no UniversalXamlAdControl.\*.dll file (for XAML apps) or UniversalSharedLibrary.Windows.dll file (for JavaScript/HTML apps), your app uses an old SDK and you must update your project by following the instructions in the sections below. Proceed to [Part 2](update-your-app-to-the-latest-advertising-libraries.md#part-2).
  * Otherwise, continue with the following step.

3. Open Windows PowerShell, enter the following command, and assign the ```-Path``` argument to the full path to the extracted contents of your app package. This command displays all the advertising libraries that are referenced by your project and the version of each library.
    ```
    get-childitem -Path "<path to your extracted package>" * -Recurse -include *advert*.dll,*admediator*.dll,*xamladcontrol*.dll,*universalsharedlibrary*.dll | where-object {$_.Name -notlike "*resources*" -and $_.Name -notlike "*design*" } | foreach-object { "{0}`t{1}" -f $_.FullName, [System.Diagnostics.FileVersionInfo]::GetVersionInfo($_).FileVersion }
    ```

4. Locate the file listed in the following table for the target platform of your app, and compare the versions of that file with the version listed in the table.

    <table>
      <colgroup>
        <col width="33%" />
        <col width="33%" />
        <col width="33%" />
      </colgroup>
      <thead>
        <tr class="header">
          <th align="left">Target platform</th>
          <th align="left">Files</th>
          <th align="left">Version</th>
        </tr>
      </thead>
      <tbody>
        <tr class="odd">
        <td align="left"><p>Windows 8.1 XAML</p></td>
        <td align="left"><p>UniversalXamlAdControl.Windows.dll</p></td>
        <td align="left"><p>8.5.1601.07018</p></td>
      </tr>
      <tr class="odd">
        <td align="left"><p>Windows Phone 8.1 XAML</p></td>
        <td align="left"><p>UniversalXamlAdControl.WindowsPhone.dll</p></td>
        <td align="left"><p>8.5.1601.07018</p></td>
      </tr>
      <tr class="odd">
        <td align="left"><p>Windows 8.1 JavaScript/HTML<br/>Windows Phone 8.1 JavaScript/HTML</p></td>
        <td align="left"><p>UniversalSharedLibrary.Windows.dll</p></td>
        <td align="left"><p>8.5.1601.07018</p></td>
      </tr>
      <tr class="odd">
        <td align="left"><p>Windows Phone 8.1 Silverlight</p></td>
        <td align="left"><p>Microsoft.Advertising.\*.dll</p></td>
        <td align="left"><p>8.1.50112.0</p></td>
      </tr>
      <tr class="odd">
        <td align="left"><p>Windows Phone 8.0 Silverlight</p></td>
        <td align="left"><p>Microsoft.Advertising.\*.dll</p></td>
        <td align="left"><p>6.2.40501.0</p></td>
      </tr>
      </tbody>
    </table>

5. If the file has a version that is equal to or later than the version listed in the previous table, you do not need to make any changes to your project. If the file has a lower version number, you must update your project by following the instructions in the sections below. Proceed to [Part 2](update-your-app-to-the-latest-advertising-libraries.md#part-2).

<span/>

**Windows 8.0 apps**

* Apps that target Windows 8.0 may no longer be served banner ads in the future. To avoid lost impressions, we recommend that you convert your project to a UWP app that targets Windows 10. Most of the Windows 8.0 app traffic is now running on devices with Windows 10.

<span/>

**Windows Phone 7.x apps**

* Apps that target Windows Phone 7.x may no longer be served banner ads in the future. To avoid lost impressions, we recommend that you convert your project to target Windows Phone 8.1 app or to a UWP app that targets Windows 10. Most of the Windows 7.x app traffic is now running on devices with Windows Phone 8.1 or Windows 10.

<span/>

### Your app uses AdMediatorControl

If your app uses **AdMediatorControl** to display banner ads, follow these instructions to determine if you need to update your app.

**UWP apps for Windows 10**

* **AdMediatorControl** is no longer supported for UWP apps. You must migrate to using **AdControl** by following the instructions in the sections below. Proceed to [Part 2](update-your-app-to-the-latest-advertising-libraries.md#part-2).

<span/>

**Windows 8.1 or Windows Phone 8.1 apps**

1. Create a copy of the the .appx or .xap package for your app so you do not disturb the original, rename the copy so it has a .zip extension, and extract the contents of the file.

2. Open Windows PowerShell, enter the following command, and assign the ```-Path``` argument to the full path to the extracted contents of your app package. This command displays all the advertising libraries that are referenced by your project and the version of each library.
    ```
    get-childitem -Path "<path to your extracted package>" * -Recurse -include *advert*.dll,*admediator*.dll,*xamladcontrol*.dll,*universalsharedlibrary*.dll | where-object {$_.Name -notlike "*resources*" -and $_.Name -notlike "*design*" } | foreach-object { "{0}`t{1}" -f $_.FullName, [System.Diagnostics.FileVersionInfo]::GetVersionInfo($_).FileVersion }
    ```

3. If the version of the Microsoft.AdMediator.\*.dll files listed in the output are version 2.0.1603.18005 or later, you do not need to make any changes to your project. If these files have a lower version number, you must update your project by following the instructions in the sections below. Proceed to [Part 2](update-your-app-to-the-latest-advertising-libraries.md#part-2).

<span id="part-2" />
## Part 2: Install the latest SDK

If your app uses an old SDK release, follow these instructions to make sure you have the latest SDK on your development computer.

1. Make sure your development computer has Visual Studio 2015 or a later release installed for UWP projects, or Visual Studio 2015 or Visual Studio 2013 installed for Windows 8.1 or Windows Phone 8.x projects.
    > [!NOTE]
    > If Visual Studio is open on your development computer, close it before you perform the following steps.

1.	Uninstall all prior versions of the Microsoft Advertising SDK and Ad Mediator SDK from your development computer.

2.	Open a **Command Prompt** window and run these commands to clean out any SDK versions that may have been installed with Visual Studio, but which may not appear in the list of installed programs on your computer:
    ```syntax
    MsiExec.exe /x{5C87A4DB-31C7-465E-9356-71B485B69EC8}
    MsiExec.exe /x{6AB13C21-C3EC-46E1-8009-6FD5EBEE515B}
    MsiExec.exe /x{6AC81125-8485-463D-9352-3F35A2508C11}
    ```

3.	Install the latest SDK for your app:
  * For UWP apps on Windows 10, install the [Microsoft Advertising SDK](http://aka.ms/ads-sdk-uwp).
  * For apps that target an earlier OS version, install the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk).

## Part 3: Update your project

Follow these instructions to update your project.

### UWP projects for Windows 10

<span/>

If your app uses **AdMediatorControl**, [refactor your app to use AdControl](migrate-from-admediatorcontrol-to-adcontrol.md) instead. **AdMediatorControl** is no longer supported for UWP apps.

If your app uses **AdControl**, remove all existing references to the Microsoft advertising libraries from the project and follow the [AdControl in XAML](adcontrol-in-xaml-and--net.md) or [AdControl in HTML](adcontrol-in-html-5-and-javascript.md) instructions to add the required references. This will ensure that your project uses the correct libraries. You can preserve your existing XAML markup and code.

<span/>

### Windows 8.1 or Windows Phone 8.1 (XAML or JavaScript/HTML) projects

<span/>

1. Remove all Microsoft.Advertising.\* and Microsoft.AdMediator.\* references from your project. You may have two references (one each for Windows and Windows Phone) if you used the Universal project template.

2. If your app uses **AdMediatorControl**, add back the library references by following the  instructions in [Adding and using the ad mediation control](https://msdn.microsoft.com/library/windows/apps/xaml/dn864355.aspx). If your app uses **AdControl**, add back the library references by following the instructions in [AdControl in XAML](adcontrol-in-xaml-and--net.md) or [AdControl in HTML](adcontrol-in-html-5-and-javascript.md).

Note the following:
* If your app was previously compiled to the **Any CPU** platform, you must recompile your project to an architecture-specific platform (x86, x64, or ARM).
* If you have a Windows Phone 8.x XAML app that previously used a version of the SDK in which the **AdControl** class was defined in the **Microsoft.Advertising.Mobile.UI** namespace, you must update your code to refer to the **AdControl** class in the **Microsoft.Advertising.WinRT.UI** namespace (this class moved namespaces in the more recent SDK releases).
* Other than the previous issue, you can preserve your existing XAML markup and code.

<span/>

### Windows Phone 8.x Silverlight projects

<span/>

1. Remove all Microsoft.Advertising.\* and Microsoft.AdMediator.\* references from your project.

2. If your app uses **AdMediatorControl**, add back the library references by following the  instructions in [Adding and using the ad mediation control](https://msdn.microsoft.com/library/windows/apps/xaml/dn864355.aspx). If your app uses **AdControl**, add back the library references by following the instructions in [AdControl in Windows Phone Silverlight](adcontrol-in-windows-phone-silverlight.md).

Note the following:
* You can preserve your existing XAML markup and code.
* From **Solution Explorer**, check the properties for the **Microsoft.Advertising.Mobile.UI** reference in your project. It should be version 6.2.40501.0 if your app targets Windows Phone 8.0, or 8.1.50112.0 if your app targets Windows Phone 8.1.
* For Windows Phone 8.x Silverlight apps, testing production units on an emulator is not supported. We recommend that you test on a device.

## Part 4: Test and republish your app

Test your app to make sure it displays banner ads as expected.

If the previous version of your app is already available in the Store, [create a new submission](../publish/app-submissions.md) for your updated app in the Dev Center dashboard to republish your app.
