---
ms.assetid: 3aeddb83-5314-447b-b294-9fc28273cd39
description: Learn how to install the Microsoft Advertising SDK to display ads in Universal Windows Platform (UWP) apps for Windows 10.
title: Install the Microsoft Advertising SDK
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, install, SDK, advertising library
ms.localizationpriority: medium
---
# Install the Microsoft Advertising SDK

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

To display ads in your UWP apps for Windows 10, install the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK). This SDK is an extension to Visual Studio 2015 and later versions.

> [!NOTE]
> If you are developing a JavaScript/HTML UWP app and you have installed Windows 10 SDK version 10.0.14393 (Anniversary Update) or later, you must also install the [WinJS](https://github.com/winjs/winjs) library. This library used to be included in previous versions of the Windows 10 SDK, but starting with the Windows 10 SDK version 10.0.14393 (Anniversary Update) this library must be installed separately.

<span id="install-msi" />

## Install via MSI

To install the Microsoft Advertising SDK via the MSI installer:

1.  Close all instances of Visual Studio.

2. If you previously installed any previous version of the Microsoft Advertising SDK, Universal Ad Client SDK, Ad Mediator extension, or Microsoft Store Engagement and Monetization SDK, uninstall these SDK versions now. Optionally, open a **Command Prompt** window and run these commands to clean out any older advertising SDK versions that may have been installed with Visual Studio, but which may not appear in the list of installed programs on your computer:
    ```console
    MsiExec.exe /x{5C87A4DB-31C7-465E-9356-71B485B69EC8}
    MsiExec.exe /x{6AB13C21-C3EC-46E1-8009-6FD5EBEE515B}
    MsiExec.exe /x{6AC81125-8485-463D-9352-3F35A2508C11}
    ```

3.  Download and install the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK). It may take a few minutes to install. Be sure and wait until the process has finished.

4.  Restart Visual Studio.

5.  If you have an existing project that references advertising libraries from any earlier version of the Microsoft Advertising SDK, Universal Ad Client SDK, or Microsoft Store Engagement and Monetization SDK, we recommend that you open your project in Visual Studio and clean and rebuild your project (in **Solution Explorer**, right-click your project node and choose **Clean**, and then right-click your project node again and choose **Rebuild**).

  Otherwise, if you are using the Microsoft Advertising SDK for the first time in your project, you are now ready to [add a reference to the Microsoft Advertising SDK](#reference).

<span id="install-nuget" />

## Install via NuGet

To install the Microsoft Advertising SDK in a specific UWP project via NuGet:

1.  Close all instances of Visual Studio.

2.  If you previously installed any previous version of the Microsoft Advertising SDK, Universal Ad Client SDK, Ad Mediator extension, or Microsoft Store Engagement and Monetization SDK, uninstall these SDK versions now. Optionally, open a **Command Prompt** window and run these commands to clean out any older advertising SDK versions that may have been installed with Visual Studio, but which may not appear in the list of installed programs on your computer:
    ```console
    MsiExec.exe /x{5C87A4DB-31C7-465E-9356-71B485B69EC8}
    MsiExec.exe /x{6AB13C21-C3EC-46E1-8009-6FD5EBEE515B}
    MsiExec.exe /x{6AC81125-8485-463D-9352-3F35A2508C11}
    ```

3.  Start Visual Studio and open the project in which you want to use the Microsoft Advertising SDK.
    > [!NOTE]
    > If your project already includes library references from an earlier MSI installation of the SDK, remove these references from your project. These references will have warning icons next to them because the libraries they reference were removed in the previous steps.

4. In Visual Studio, click **Project** and **Manage NuGet Packages**.

5. In the search box, type **Microsoft.Advertising.XAML** (for a XAML project) or **Microsoft.Advertising.JS** (for a JavaScript/HTML project) and install the corresponding package. When the package is done installing, save your solution.
    > [!NOTE]
    > If the **Output** window reports an *Install-Package* error that indicates the specified path is too long, you may need to configure NuGet to extract packages to an alternate location with a shorter path than the default location. To do this, add the `repositoryPath` value to a nuget.config file on your computer and assign it to a short folder path where NuGet packages can be extracted. For more information, see [this article](/nuget/consume-packages/configuring-nuget-behavior) in the NuGet documentation. Alternatively, you can try moving your Visual Studio project to an alternate folder with a shorter path.

6. Close your solution and then reopen it.

7.  If your project already references libraries from an earlier version of the Microsoft Advertising SDK that was installed via NuGet and you have updated your project to a newer release of the SDK, we recommend that you clean and rebuild your project (in **Solution Explorer**, right-click your project node and choose **Clean**, and then right-click your project node again and choose **Rebuild**).

  Otherwise, if you are using the SDK for the first time in your project, you are now ready to [add a reference to the Microsoft Advertising SDK](#reference).

<span id="reference" />

## Add a reference to the Microsoft Advertising SDK

After you install the Microsoft Advertising SDK, follow these instructions to reference the SDK in your project so you can use the advertising APIs.

1. Open your project in Visual Studio.
    > [!NOTE]
    > If your project targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**). If your project targets **Any CPU**, you will not be able to successfully add a reference to the Microsoft Advertising SDK in the following steps. For more information, see [Reference errors caused by targeting Any CPU in your project](known-issues-for-the-advertising-libraries.md#reference_errors).

2. In **Solution Explorer**, right click **References** and select **Add Reference…**

3. In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for XAML** (for XAML apps) or **Microsoft Advertising SDK for JavaScript** (for apps built using JavaScript and HTML).

4.  In **Reference Manager**, click OK.

For walkthroughs that show how to get started using the advertising APIs, see the following articles:

* [Interstitial ads](interstitial-ads.md)
* [Native ads](native-ads.md)
* [AdControl in XAML and .NET](adcontrol-in-xaml-and--net.md)
* [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md)

<span id="framework" />

## Understanding framework packages in the Microsoft Advertising SDK

The Microsoft.Advertising.dll library in the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) (for UWP apps) is configured as a *framework package*. This library contains the advertising APIs in the [Microsoft.Advertising](/uwp/api/microsoft.advertising) and [Microsoft.Advertising.WinRT.UI](/uwp/api/microsoft.advertising.winrt.ui) namespaces.

Because this library is a framework package, this means that after a user installs a version of your app that uses this library, this library is automatically updated on their device through Windows Update whenever we publish a new version of the library with fixes and performance improvements. This helps to ensure that your customers always have the latest available version of the library installed on their devices.

If we release a new version of the SDK that introduces new APIs or features in this library, you will need to install the latest version of the SDK to use those features. In this scenario, you would also need to publish your updated app to the Store.