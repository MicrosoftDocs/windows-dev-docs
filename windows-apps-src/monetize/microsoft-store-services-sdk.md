---
Description: The Microsoft Store Services SDK provides libraries and tools that you can use to add features to your apps that help you make more money and gain customers.
title: Engage customers with the Microsoft Store Services SDK
ms.assetid: 518516DB-70A7-49C4-B3B6-CD8A98320B9C
ms.date: 08/21/2017
ms.topic: article
keywords: windows 10, uwp, Microsoft Store Services SDK
ms.localizationpriority: medium
---
# Engage customers with the Microsoft Store Services SDK

The Microsoft Store Services SDK provides features that help you engage with customers in your Universal Windows Platform (UWP) apps, such as sending targeted notifications to your apps and running A/B experiments in your apps. This SDK is an extension for Visual Studio 2015 and later versions of Visual Studio.

> [!NOTE]
> To display ads in your UWP apps, use the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) instead of the Microsoft Store Services SDK. The advertising libraries have been moved from the Microsoft Store Services SDK to the Microsoft Advertising SDK. For more information, see [Display ads in your app](display-ads-in-your-app.md).



## Scenarios supported by the Microsoft Store Services SDK

The Microsoft Store Services SDK currently supports the following scenarios for UWP apps. For API reference documentation, see [Microsoft Store Services SDK API reference](/uwp/api/overview/engagement).

|  Scenario  |  Description   |
|------------|----------------|
|  [Run experiments in your UWP app with A/B testing](run-app-experiments-with-a-b-testing.md)    |  Run A/B tests in your Universal Windows Platform (UWP) app to measure the effectiveness of features on some customers before you release the features to everyone. After you define an experiment in Partner Center, use the [StoreServicesExperimentVariation](/uwp/api/microsoft.services.store.engagement.storeservicesexperimentvariation) class to get variations for your experiment in your app, use this data to modify the behavior of the feature you are testing, and then use the [LogForVariation](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger.logforvariation) method to send view event and conversion events to Partner Center. Finally, use Partner Center to view the results and manage the experiment.  |
|  [Launch Feedback Hub from your UWP app](launch-feedback-hub-from-your-app.md)    |  Use the [StoreServicesFeedbackLauncher](/uwp/api/microsoft.services.store.engagement.storeservicesfeedbacklauncher) class in your UWP app to direct your Windows 10 customers to Feedback Hub, where they can submit problems, suggestions, and upvotes. Then, manage this feedback in the [Feedback report](../publish/feedback-report.md) in Partner Center. |
|  [Configure your UWP app to receive Partner Center push notifications](configure-your-app-to-receive-dev-center-notifications.md)    |  Use the [StoreServicesEngagementManager](/uwp/api/microsoft.services.store.engagement.storeservicesengagementmanager) class in your UWP app to register your app to receive targeted push notifications that you send to your customers using Partner Center.  |
|   [Log custom events in your UWP app for the Usage report in Partner Center](log-custom-events-for-dev-center.md)   |  Use the [StoreServicesCustomEventLogger](/uwp/api/microsoft.services.store.engagement.storeservicescustomeventlogger.log) class in your UWP app to log custom events that are associated with your app in Partner Center. Then, review the total occurrences for your custom events in the **Custom events** section of the [Usage report](../publish/usage-report.md) in Partner Center.  |

<span id="prerequisites" />

## Prerequisites

The Microsoft Store Services SDK requires:

* Visual Studio 2015 or a later version.
* Visual Studio Tools for Universal Windows Apps installed with your version of Visual Studio.

<span id="install" />

## Install the SDK

There are two options for installing the Microsoft Store Services SDK on your development computer:

* **MSI installer**&nbsp;&nbsp;You can install the SDK via the MSI installer available [here](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftStoreServicesSDK).
* **NuGet package**&nbsp;&nbsp;You can install the SDK as a NuGet package.

Microsoft periodically releases new versions of the Microsoft Store Services SDK with performance improvements and new features. If you have existing projects that use the SDK and you want to use the latest version, download and install the latest version of the SDK on your development computer.

<span id="install-msi" />

### Install via MSI

To install the Microsoft Store Services SDK via the MSI installer:

1.  Close all instances of Visual Studio.

2. If you previously installed the Microsoft Store Engagement and Monetization SDK, Universal Ad Client SDK, or Ad Mediator extension, uninstall these SDKs now. Optionally, open a **Command Prompt** window and run these commands to clean out any older SDK versions that may have been installed with Visual Studio, but which may not appear in the list of installed programs on your computer:
    ```console
    MsiExec.exe /x{5C87A4DB-31C7-465E-9356-71B485B69EC8}
    MsiExec.exe /x{6AB13C21-C3EC-46E1-8009-6FD5EBEE515B}
    MsiExec.exe /x{6AC81125-8485-463D-9352-3F35A2508C11}
    ```

3.  Download and install the [Microsoft Store Services SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftStoreServicesSDK). It may take a few minutes to install. Be sure and wait until the process has finished.

4.  Restart Visual Studio.

5.  If you have an existing project that references libraries from any earlier version of the Microsoft Store Services SDK, Microsoft Advertising SDK, Universal Ad Client SDK, or Microsoft Store Engagement and Monetization SDK, we recommend that you open your project in Visual Studio and clean and rebuild your project (in **Solution Explorer**, right-click your project node and choose **Clean**, and then right-click your project node again and choose **Rebuild**).

  Otherwise, if you are using the SDK for the first time in your project, you are now ready to [add the assembly reference to your project](#references).

<span id="install-nuget" />

### Install via NuGet

To install the Microsoft Store Services SDK libraries via NuGet:

1.  Close all instances of Visual Studio.

2. If you previously installed the Microsoft Store Engagement and Monetization SDK, Universal Ad Client SDK, or Ad Mediator extension, uninstall these SDKs now. Optionally, open a **Command Prompt** window and run these commands to clean out any older SDK versions that may have been installed with Visual Studio, but which may not appear in the list of installed programs on your computer:
    ```console
    MsiExec.exe /x{5C87A4DB-31C7-465E-9356-71B485B69EC8}
    MsiExec.exe /x{6AB13C21-C3EC-46E1-8009-6FD5EBEE515B}
    MsiExec.exe /x{6AC81125-8485-463D-9352-3F35A2508C11}
    ```

3.  Start Visual Studio and open the project in which you want to use the Microsoft Store Services SDK.
    > [!NOTE]
    > If your project already includes library references from an earlier MSI installation of the SDK, remove these references from your project. These references will have warning icons next to them because the libraries they reference were removed in the previous steps.

4. In Visual Studio, click **Project** and **Manage NuGet Packages**.

5. In the search box, type **Microsoft.Services.Store.Engagement** and install the Microsoft.Services.Store.Engagement package. When the package is done installing, save your solution.
    > [!NOTE]
    > If the **Output** window reports an *Install-Package* error that indicates the specified path is too long, you may need to configure NuGet to extract packages to an alternate location with a shorter path than the default location. To do this, add the `repositoryPath` value to a nuget.config file on your computer and assign it to a short folder path where NuGet packages can be extracted. For more information, see [this article](/nuget/consume-packages/configuring-nuget-behavior) in the NuGet documentation. Alternatively, you can try moving your Visual Studio project to an alternate folder with a shorter path. The problem could also be caused by your global packages path being too long. In this case, add the `globalPackagesFolder` value into your nuget.config file.

6. Close the Visual Studio solution that contains your project and then reopen the solution.

7.  If your project already references libraries from an earlier version of the Microsoft Store Services SDK that was installed via NuGet and you have updated your project to a newer release of the SDK, we recommend that you clean and rebuild your project (in **Solution Explorer**, right-click your project node and choose **Clean**, and then right-click your project node again and choose **Rebuild**).

  Otherwise, if you are using the SDK for the first time in your project, you are now ready to [add the assembly reference to your project](#references).

<span id="references" />

## Add the assembly reference to your project

After you install the Microsoft Store Services SDK via the MSI installer or NuGet, follow these instructions to reference the SDK assembly in your UWP project.

1. Open your project in Visual Studio.
    > [!NOTE]
    > If your project is a JavaScript app that targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**).

2. In **Solution Explorer**, right click **References** and select **Add Reference…**

3. In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Engagement Framework**. This enables you to use the APIs in the [Microsoft.Services.Store.Engagement](/uwp/api/microsoft.services.store.engagement) namespace.

3. Click **OK**.

> [!NOTE]
> If you installed the SDK libraries via NuGet, your project will contain a **Microsoft.Services.Store.Engagement** reference. The **Microsoft.Services.Store.Engagement** reference represents the NuGet package (rather than the libraries in it), and you can ignore it.

<span id="framework" />

## Understanding framework packages in the SDK

The Microsoft.Services.Store.Engagement.dll library in the Microsoft Store Services SDK is configured as a *framework package*. This library contains the APIs in the [Microsoft.Services.Store.Engagement](/uwp/api/microsoft.services.store.engagement) namespace.

Because this library is a framework package, this means that after a user installs a version of your app that uses this library, this library is automatically updated on their device through Windows Update whenever we publish a new version of the library with fixes and performance improvements. This helps to ensure that your customers always have the latest available version of the library installed on their devices.

If we release a new version of the SDK that introduces new APIs or features in this library, you will need to install the latest version of the SDK to use those features. In this scenario, you would also need to publish your updated app to the Store.

## Related topics

* [Microsoft Store Services SDK API reference](/uwp/api/overview/engagement)
* [Run experiments with A/B testing](run-app-experiments-with-a-b-testing.md)
* [Launch Feedback Hub from your app](launch-feedback-hub-from-your-app.md)
* [Configure your app to receive Partner Center push notifications](configure-your-app-to-receive-dev-center-notifications.md)
* [Log custom events for Partner Center](log-custom-events-for-dev-center.md)