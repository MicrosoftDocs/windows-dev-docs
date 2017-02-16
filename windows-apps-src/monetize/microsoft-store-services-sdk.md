---
author: mcleanbyron
Description: The Microsoft Store Services SDK provides libraries and tools that you can use to add features to your apps that help you make more money and gain customers.
title: Microsoft Store Services SDK
ms.assetid: 518516DB-70A7-49C4-B3B6-CD8A98320B9C
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, Microsoft Store Services SDK
---

# Microsoft Store Services SDK

The Microsoft Store Services SDK provides features that help you make more money and engage with customers in your Universal Windows Platform (UWP) apps, such as displaying ads and running A/B experiments in your apps. This SDK is an extension for Visual Studio 2015 and later versions of Visual Studio.

## Scenarios supported by the SDK

The SDK currently supports the following scenarios for UWP apps. The SDK will evolve over time to support new engagement and monetization scenarios. For reference documentation about the APIs in the SDK, see [Microsoft Store Services SDK API reference](https://msdn.microsoft.com/library/windows/apps/mt691886.aspx).

|  Scenario  |  Description   |
|------------|----------------|
|  [Run experiments in your UWP app with A/B testing](run-app-experiments-with-a-b-testing.md)    |  Run A/B tests in your Universal Windows Platform (UWP) app to measure the effectiveness of features on some customers before you release the features to everyone. After you define an experiment in your Dev Center dashboard, use the [StoreServicesExperimentVariation](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesexperimentvariation.aspx) class to get variations for your experiment in your app, use this data to modify the behavior of the feature you are testing, and then use the [LogForVariation](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicescustomeventlogger.logforvariation.aspx) method to send view event and conversion events to Dev Center. Finally, use your dashboard to view the results and manage the experiment.  |
|  [Launch Feedback Hub from your UWP app](launch-feedback-hub-from-your-app.md)    |  Use the [StoreServicesFeedbackLauncher](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesfeedbacklauncher.aspx) class in your UWP app to direct your Windows 10 customers to Feedback Hub, where they can submit problems, suggestions, and upvotes. Then, manage this feedback in the [Feedback report](../publish/feedback-report.md) in the Dev Center dashboard. |
|  [Configure your UWP app to receive Dev Center push notifications](configure-your-app-to-receive-dev-center-notifications.md)    |  Use the [StoreServicesEngagementManager](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicesengagementmanager.aspx) class in your UWP app to register your app to receive targeted push notifications that you send to your customers using the Windows Dev Center dashboard.  |
|   [Log custom events in your UWP app for the Usage report in Dev Center](log-custom-events-for-dev-center.md)   |  Use the [StoreServicesCustomEventLogger](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.storeservicescustomeventlogger.log.aspx) class in your UWP app to log custom events that are associated with your app in Dev Center. Then, review the total occurrences for your custom events in the **Custom events** section of the [Usage report](https://msdn.microsoft.com/windows/uwp/publish/usage-report) in the Dev Center dashboard.  |
|  [Display ads in your UWP app](display-ads-in-your-app.md)    |  Use the [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) or [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx) controls in your UWP app to increase your revenue by displaying banner ads or interstitial ads.<br/><br/>**Note**&nbsp;&nbsp;The Microsoft Store Services SDK only supports UWP apps for Windows 10. To display ads in Windows 8.1 and Windows Phone 8.x apps, use the [Microsoft Advertising SDK for Windows and Windows Phone 8.x](http://aka.ms/store-8-sdk).  |

<span id="prerequisites" />
## Prerequisites

The Microsoft Store Services SDK requires:

* Visual Studio 2015 or a later version.
* Visual Studio Tools for Universal Windows Apps installed with your version of Visual Studio.

>**Note**&nbsp;&nbsp;To install the SDK with Visual Studio 2015, you must have version 1.1 or later of the Visual Studio Tools for Universal Windows Apps installed. For more information about this update to the Visual Studio Tools for Universal Windows Apps, see the [release notes](http://go.microsoft.com/fwlink/?LinkID=624516).

<span id="install" />
## Install the SDK

There are two options for installing the Microsoft Store Services SDK for use with Visual Studio 2015 (or a later release) on your development computer:

* **MSI installer**&nbsp;&nbsp;You can install the SDK via the MSI installer available [here](http://aka.ms/store-em-sdk). With this option, the SDK libraries are installed in a shared location on your development computer so that they can be referenced by any UWP project in Visual Studio.
* **NuGet package**&nbsp;&nbsp;You can install the SDK libraries for a specific UWP project in Visual Studio using NuGet. With this option, the SDK libraries are installed only for the project in which you installed the NuGet package.

Microsoft periodically releases new versions of the Microsoft Store Services SDK with performance improvements and new features. If you have existing projects that use the SDK and you want to use the latest version, download and install the latest version of the SDK on your development computer.

>**Note**&nbsp;&nbsp;To install the SDK with Visual Studio 2015, you must have version 1.1 or later of the Visual Studio Tools for Universal Windows Apps installed. For more information about this update to the Visual Studio Tools for Universal Windows Apps, see the [release notes](http://go.microsoft.com/fwlink/?LinkID=624516).

<span id="install-msi" />
### Install via MSI

To install the Microsoft Store Services SDK via the MSI installer:

1.  Close all instances of Visual Studio 2015 (or a later release). If you previously installed any previous version of the Microsoft Advertising SDK, Universal Ad Client SDK, Ad Mediator extension, or Microsoft Store Engagement and Monetization SDK, uninstall these SDK versions now.

2.	Open a **Command Prompt** window and run these commands to clean out any older advertising SDK versions that may have been installed with Visual Studio, but which may not appear in the list of installed programs on your computer:
  ```
  MsiExec.exe /x{5C87A4DB-31C7-465E-9356-71B485B69EC8}
  MsiExec.exe /x{6AB13C21-C3EC-46E1-8009-6FD5EBEE515B}
  MsiExec.exe /x{6AC81125-8485-463D-9352-3F35A2508C11}
  ```

3.  Download and install the [Microsoft Store Services SDK](http://aka.ms/store-em-sdk). It may take a few minutes to install. Be sure and wait until the process has finished.

4.  Restart Visual Studio.

5.  If you have an existing project that reference libraries from any earlier version of the Microsoft Store Services SDK, Microsoft Advertising SDK, Universal Ad Client SDK, or Microsoft Store Engagement and Monetization SDK, we recommend that you open your project in Visual Studio and clean and rebuild your project (in **Solution Explorer**, right-click your project node and choose **Clean**, and then right-click your project node again and choose **Rebuild**).

  Otherwise, if you are using the SDK for the first time in your project, you are now ready to [add the appropriate Microsoft Store Services SDK library references to your project](#references).

<span id="install-nuget" />
### Install via NuGet

To install the Microsoft Store Services SDK libraries for a specific project via NuGet:

1.  Close all instances of Visual Studio 2015 (or a later release). If you previously installed any previous version of the Microsoft Advertising SDK, Universal Ad Client SDK, Ad Mediator extension, or Microsoft Store Engagement and Monetization SDK, uninstall these SDK versions now.

2.	Open a **Command Prompt** window and run these commands to clean out any older advertising SDK versions that may have been installed with Visual Studio, but which may not appear in the list of installed programs on your computer:
  ```
  MsiExec.exe /x{5C87A4DB-31C7-465E-9356-71B485B69EC8}
  MsiExec.exe /x{6AB13C21-C3EC-46E1-8009-6FD5EBEE515B}
  MsiExec.exe /x{6AC81125-8485-463D-9352-3F35A2508C11}
  ```

3.  Start Visual Studio and open the project in which you want to use the Microsoft Store Services SDK libraries.

  >**Note**&nbsp;&nbsp;If your project already includes library references from an earlier MSI installation of the SDK, remove these references from your project. These references will have warning icons next to them because the libraries they reference were removed in the previous steps.

4. In Visual Studio, click **Project** and **Manage NuGet Packages**.

5. In the search box, type **Microsoft.Services.Store.SDK** and install the Microsoft.Services.Store.SDK package.

  >**Note**&nbsp;&nbsp;If the **Output** window reports an *Install-Package* error that indicates the specified path is too long, you may need to configure NuGet to extract packages to an alternate location with a shorter path than the default location. To do this, add the ```repositoryPath``` value to a nuget.config file on your computer and assign it to a short folder path where NuGet packages can be extracted. For more information, see [this article](http://docs.nuget.org/ndocs/consume-packages/configuring-nuget-behavior) in the NuGet documentation. Alternatively, you can try moving your Visual Studio project to an alternate folder with a shorter path.

6. Close your project and then reopen it.

7.  If your project already references libraries from an earlier version of the Microsoft Store Services SDK that was installed via NuGet and you have updated your project to a newer release of the SDK, we recommend that you clean and rebuild your project (in **Solution Explorer**, right-click your project node and choose **Clean**, and then right-click your project node again and choose **Rebuild**).

  Otherwise, if you are using the SDK for the first time in your project, you are now ready to [add the appropriate Microsoft Store Services SDK library references to your project](#references).

<span id="references" />
## Add SDK library references to your project

After you install the Microsoft Store Services SDK via the MSI installer or NuGet, follow these instructions to reference the SDK libraries in your UWP project.

1. Open your project in Visual Studio.

  >**Note**&nbsp;&nbsp;If your project is a JavaScript app that targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**).

2. In **Solution Explorer**, right click **References** and select **Add Reference…**

3. In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to one of the following items.

  * To use the APIs in the [Microsoft.Services.Store.Engagement](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.aspx) namespace for customer engagement scenarios, select the check box next to **Microsoft Engagement Framework**. Choose this option if you want to [run A/B experiments](run-app-experiments-with-a-b-testing.md), [launch Feedback Hub](launch-feedback-hub-from-your-app.md), [receive targeted push notifications from Dev Center](configure-your-app-to-receive-dev-center-notifications.md), or [log custom events to Dev Center](log-custom-events-for-dev-center.md).

  * To use the APIs for [displaying banner ads or interstitial ads in your app](display-ads-in-your-app.md), select the check box next to **Microsoft Advertising SDK for XAML** or **Microsoft Advertising SDK for JavaScript**, depending on your project type.

3. Click **OK**.

>**Note**&nbsp;&nbsp;If you installed the SDK libraries via NuGet, your project will contain a **Microsoft.Services.Store.SDK** reference in addition to **Microsoft Advertising SDK for XAML** or **Microsoft Advertising SDK for JavaScript**. The **Microsoft.Services.Store.SDK** reference represents the NuGet package (rather than the libraries in it), and you can ignore it.

<span id="framework" />
## Understanding framework packages in the SDK

The following libraries in the Microsoft Store Services SDK are configured as *framework packages*:

* Microsoft.Advertising.dll. This library contains the advertising APIs in the [Microsoft.Advertising](https://msdn.microsoft.com/library/windows/apps/mt313187.aspx) and [Microsoft.Advertising.WinRT.UI](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.aspx) namespaces.
* Microsoft.Services.Store.Engagement.dll. This library contains the APIs in the [Microsoft.Services.Store.Engagement](https://msdn.microsoft.com/library/windows/apps/microsoft.services.store.engagement.aspx) namespace.

This means that after a user installs a version of your app that uses these libraries, these libraries are automatically updated on their device through Windows Update whenever we publish new versions of the libraries with fixes and performance improvements. This helps to ensure that your customers always have the latest available version of the libraries installed on their devices.

If we release a new version of the SDK that introduces new APIs or features in these libraries, you will need to install the latest version of the SDK to use those features. In this scenario, you would also need to publish your updated app to the Store.

## Related topics

* [Microsoft Store Services SDK API reference](https://msdn.microsoft.com/library/windows/apps/mt691886.aspx)
* [Run experiments with A/B testing](run-app-experiments-with-a-b-testing.md)
* [Launch Feedback Hub from your app](launch-feedback-hub-from-your-app.md)
* [Configure your app to receive Dev Center push notifications](configure-your-app-to-receive-dev-center-notifications.md)
* [Log custom events for Dev Center](log-custom-events-for-dev-center.md)
* [Display ads in your app](display-ads-in-your-app.md)
