---
author: mcleblanc
ms.assetid: A5320094-DF53-42FC-A6BA-A958F8E9210B
title: Test Surface Hub apps using Visual Studio
description: The Visual Studio simulator provides an environment to design, develop, debug, and test UWP apps, including apps built for Surface Hub.
ms.author: markl
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Test Surface Hub apps using Visual Studio
The Visual Studio simulator provides an environment where you can design, develop, debug, and test Universal Windows Platform (UWP) apps, including apps that you have built for Microsoft Surface Hub. The simulator does not use the same user interface as Surface Hub, but it is useful for testing how your app looks and behaves at the Surface Hub's screen size and resolution.

For more information, see [Run Windows Store apps in the simulator](https://msdn.microsoft.com/library/hh441475.aspx).

## Add Surface Hub resolutions to the simulator
To add Surface Hub resolutions to the simulator:

1. Create a configuration for the 55" Surface Hub by saving the following XML into a file named **HardwareConfigurations-SurfaceHub55.xml**.  

    ```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <ArrayOfHardwareConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                                  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
        <HardwareConfiguration>
            <Name>SurfaceHub55</Name>
            <DisplayName>Surface Hub 55"</DisplayName>
            <Resolution>
                <Height>1080</Height>
                <Width>1920</Width>
            </Resolution>
            <DeviceSize>55</DeviceSize>
            <DeviceScaleFactor>100</DeviceScaleFactor>
        </HardwareConfiguration>
    </ArrayOfHardwareConfiguration>
    ```

2. Create a configuration for the 84" Surface Hub by saving the following XML into a file named  **HardwareConfigurations-SurfaceHub84.xml**.

    ```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <ArrayOfHardwareConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                                  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
        <HardwareConfiguration>
            <Name>SurfaceHub84</Name>
            <DisplayName>Surface Hub 84"</DisplayName>
            <Resolution>
                <Height>2160</Height>
                <Width>3840</Width>
            </Resolution>
            <DeviceSize>84</DeviceSize>
            <DeviceScaleFactor>150</DeviceScaleFactor>
        </HardwareConfiguration>
    </ArrayOfHardwareConfiguration>
    ```

3. Copy the two XML files into **C:\Program Files (x86)\Common Files\Microsoft Shared\Windows Simulator\\&lt;version number&gt;\HardwareConfigurations**.

   > **Note**&nbsp;&nbsp;Administrative privileges are required to save files into this folder.

4. Run your app in the Visual Studio simulator. Click the **Change Resolution** button on the palette and select a Surface Hub configuration from the list.

    ![Visual Studio simulator resolutions](images/vs-simulator-resolutions.png)

   > **Tip**&nbsp;&nbsp;[Turn on Tablet mode](http://windows.microsoft.com/windows-10/getstarted-like-a-tablet) to better simulate the experience on a Surface Hub.

## Deploy apps to a Surface Hub from Visual Studio
Manually deploying an app is a simple process.

### Enable developer mode
By default, Surface Hub only installs apps from the Windows Store. To install apps signed by other sources, you must enable developer mode.

> **Note**&nbsp;&nbsp;After developer mode has been enabled, you will need to reset the Surface Hub to disable it again. Resetting the device removes all local user files and configurations and then reinstalls Windows.

1. From the Surface Hub's **Start** menu, open the Settings app.

   >  **Note**&nbsp;&nbsp;Administrative privileges are required to access the Settings app.

2. Navigate to **Update & security > For developers**.

3. Choose **Developer mode** and accept the warning prompt.

### Deploy your app from Visual Studio
For more information, see [Deploying and debugging Universal Windows Platform (UWP) apps](https://msdn.microsoft.com/windows/uwp/debug-test-perf/deploying-and-debugging-uwp-apps).

   > **Note**&nbsp;&nbsp;This feature requires at least **Visual Studio 2015 Update 1**.

1. Navigate to the debug target dropdown next to the **Start Debugging** button and select **Remote Machine**.

    <!--lcap: in your screenshot, you have local machine selected-->

   ![Visual Studio debug targets dropdown](images/vs-debug-target.png)

2. Enter the Surface Hub's IP address. Ensure that the **Universal** authentication mode is selected.

   > **Tip**&nbsp;&nbsp;After you have enabled developer mode, you can find the Surface Hub's IP address on the welcome screen.

3. Choose **Start Debugging (F5)** to deploy and debug your app on the Surface Hub, or press Ctrl+F5 to just deploy your app.

   > **Tip**&nbsp;&nbsp;If the Surface Hub is on the welcome screen, dismiss it by choosing any button.
