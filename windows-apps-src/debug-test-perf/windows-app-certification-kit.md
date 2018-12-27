---
ms.assetid: 78D833B9-E528-4BCA-9C48-A757F17E6C22
title: Windows App Certification Kit
description: To give your app the best chance of being published on the Microsoft Store, or becoming Windows Certified, validate and test it locally before you submit it for certification. This topic shows you how to install and run the Windows App Certification Kit.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, app certification
ms.localizationpriority: medium
---
# Windows App Certification Kit



To get your app [Windows Certified](https://msdn.microsoft.com/windows/desktop/jj134964.aspx) or prepare it for [publication to the Microsoft Store](https://msdn.microsoft.com/library/windows/apps/Hh694062), you should validate and test it locally first. This topic shows you how to install and run the [Windows App Certification Kit](http://go.microsoft.com/fwlink/p/?LinkID=309666) to ensure your app is safe and efficient.

## Prerequisites

Prerequisites for testing a Universal Windows app:

-   You must install and run Windows 10.
-   You must install [Windows App Certification Kit version 10]( http://go.microsoft.com/fwlink/p/?LinkID=309666), which is included in the Windows Software Development Kit (SDK) for Windows 10.
-   You must [enable your device for development](https://docs.microsoft.com/windows/uwp/get-started/enable-your-device-for-development).
-   You must deploy the Windows app that you want to test to your computer.

**A note about in-place upgrades**

The installation of a more recent [Windows App Certification Kit]( http://go.microsoft.com/fwlink/p/?LinkID=309666) will replace any previous version of the kit that is installed on the machine.

## Validate your Windows app using the Windows App Certification Kit interactively

1.  From the **Start** menu, search **Apps**, find **Windows Kits**, and click **Windows App Cert Kit**.

2.  From the Windows App Certification Kit, select the category of validation you would like to perform. For example: If you are validating a Windows app, select **Validate a Windows app**.

    You may browse directly to the app you're testing, or choose the app from a list in the UI. When the Windows App Certification Kit is run for the first time, the UI lists all the Windows apps that you have installed on your computer. For any subsequent runs, the UI will display the most recent Windows apps that you have validated. If the app that you want to test is not listed, you can click on **My app isn't listed** to get a comprehensive list of all apps installed on your system.

3.  After you have input or selected the app that you want to test, click **Next**.

4.  From the next screen, you will see the test workflow that aligns to the app type you are testing. If a test is grayed out in the list, the test is not applicable to your environment. For example, if you are testing a Windows 10 app on Windows 7, only static tests will apply to the workflow. Note that the Microsoft Store may apply all tests from this workflow. Select the tests you want to run and click **Next**.

    The Windows App Certification Kit begins validating the app.

5.  At the prompt after the test, enter the path to the folder where you want to save the test report.

    The Windows App Certification Kit creates an HTML along with an XML report and saves it in this folder.

6.  Open the report file and review the results of the test.

**Note**  If you're using Visual Studio, you can run the Windows App Certification Kit when you create your app package. See [Packaging UWP apps](https://msdn.microsoft.com/library/windows/apps/Mt627715) to learn how.

 

## Validate your Windows app using the Windows App Certification Kit from a command line

**Important**  The Windows App Certification Kit must be run within the context of an active user session.

1.  In the command window, navigate to the directory that contains the Windows App Certification Kit.

    **Note**   The default path is C:\\Program Files\\Windows Kits\\10\\App Certification Kit\\.

2.  Enter the following commands in this order to test an app that is already installed on your test computer:

    `appcert.exe reset`

    `appcert.exe test -packagefullname [package full name] -reportoutputpath [report file name]`

    Or you can use the following commands if the app is not installed. The Windows App Certification Kit will open the package and apply the appropriate test workflow:

    `appcert.exe reset`

    `appcert.exe test -appxpackagepath [package path] -reportoutputpath [report file name]`

3.  After the test completes, open the report file named `[report file name]` and review the test results.

**Note**  The Windows App Certification Kit can be run from a service, but the service must initiate the kit process within an active user session and cannot be run in Session0.

**Note**   For more info about the Windows App Certification Kit command line, enter the command `appcert.exe /?`

## Testing with a low-power computer

The performance test thresholds of the Windows App Certification Kit are based on the performance of a low-power computer.

The characteristics of the computer on which the test is performed can influence the test results. To determine if your app’s performance meets the [Microsoft Store Policies](https://msdn.microsoft.com/library/windows/apps/Dn764944), we recommend that you test your app on a low-power computer, such as an Intel Atom processor-based computer with a screen resolution of 1366x768 (or higher) and a rotational hard drive (as opposed to a solid-state hard drive).

As low-power computers evolve, their performance characteristics might change over time. Refer to the most current [Microsoft Store Policies](https://msdn.microsoft.com/library/windows/apps/Dn764944) and test your app with the most current version of the Windows App Certification Kit to make sure that your app complies with the latest performance requirements.

## Related topics

* [Windows App Certification Kit tests](windows-app-certification-kit-tests.md)
* [Microsoft Store Policies](https://msdn.microsoft.com/library/windows/apps/Dn764944)
 

 




