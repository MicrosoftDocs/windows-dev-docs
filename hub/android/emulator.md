---
title: Running Android device or emulator from Windows
description: Guide to testing your app on an Android device or emulator from Windows using Visual Studio or VS Code.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: android on windows
ms.date: 02/19/2020
---

# Testing on an Android device or emulator

There are several ways to test and debug your Android application using a real device or emulator on your Windows machine. We have outlined a few recommendations in this guide.

## Android Studio emulator for native development

When building and testing a native Android app, we recommend [using Android Studio](./native-android.md). Once your app is ready for testing, you can build and run your app by:

1. In the Android Studio toolbar, select your app from the **run configurations** drop-down menu.

    ![Android Studio Run Configuration menu](../images/android-run-config-menu.png)

2. From the **target device** drop-down menu, select the device that you want to run your app on.

    ![Android Studio Target Device menu](../images/android-target-device-menu.png)

3. Select Run ▷. This will launch the [Android Emulator](https://developer.android.com/studio/run/emulator).

> [!TIP]
> Once your app is installed on the emulator device, you can use [Apply Changes](https://developer.android.com/studio/run#apply-changes) to deploy certain code and resource changes without building a new APK.

<!-- From Jon D's email: "Android Studio does not have any emulation checks in their product" ...not quite sure what this means (practically). Is this just a speed/perf issue? It seems very responsive on my Surface Book 2... but maybe I already had Hyper-V enabled... tho I didn't have WHPX enabled. Will perf improve now that I have?  -->

## Run on a real Android device

### Enable your device for development

To run your app on a real Android device, you will first need to enable your Android device for development.

1. Connect your device to your development machine with a USB cable. You may need to install a USB driver for your device.
2. Open the Settings app on your Android device.
3. Scroll to the bottom and select **About phone**.
4. Scroll to the bottom and tap **Build number** seven times.
5. Return to the previous screen, select **System**.
6. Select **Advanced**, scroll to the bottom, and tap **Developer options**.
7. In the **Developer options** window, scroll down to find and enable **USB debugging**.

### Run your app on the device

Run the app on your device:

1. In the Android Studio toolbar, select your app from the **run configurations** drop-down menu.

    ![Android Studio Run Configuration menu](../images/android-run-config-menu.png)

2. From the **target device** drop-down menu, select the device that you want to run your app on.

    ![Android Studio Target Device menu](../images/android-target-device-menu.png)

3. Select Run ▷. This will launch the app on your connected device.

## Cross-platform Android emulators

To do. Intro about the broad array of emulators and our 'golden path' recommendation.

There are many [Android emulator options](https://www.androidauthority.com/best-android-emulators-for-pc-655308/) available for Windows PCs. We recommend using Google's [Android emulator](https://docs.microsoft.com/xamarin/android/get-started/installation/android-emulator/), as it offers access to the latest Android OS images and Google Play services.

### Enable Hyper-V and Windows Hypervisor

Before creating a virtual device with the Android emulator, it is recommended that you enable Hyper-V and the Windows Hypervisor Platform (WHPX) to improve your machine's performance.

1. To verify that your computer hardware and software is compatible with Hyper-V, open a command prompt and type the following command: `systeminfo`

    ![Hyper-V requirements from systeminfo in command prompt](../images/systeminfo.png)

2. In the Windows search box (lower left) and enter "windows features". Select **Turn Windows features on or off** from the search results.

3. Once the **Windows Features** list appears, scroll to find **Hyper-V** (includes both Management Tools and Platform) and **Windows Hypervisor Platform**, ensure that the box is checked to enable both, then select **OK**.

4. Restart your computer when prompted.

> [!NOTE]
> To run Hyper-V and Windows Hypervisor, your computer must have 4GB of memory, with a 64-bit Intel processor or an AMD Ryzen CPU with Second Level Address Translation (SLAT). You also must be updated to Windows 10 April 2018 update (build 1803) or later. To verify: Enter "About" in the Windows search box. Select **About your PC** in the search results. Verify that the version is at least 1803 in the Windows specifications section. For more information, see the article: [Hardware acceleration for emulator performance](https://docs.microsoft.com/xamarin/android/get-started/installation/android-emulator/hardware-acceleration?tabs=vswin&pivots=windows#related-links).

### Install Android emulator with Visual Studio

To install the Android emulator with Visual Studio:

1. If you don't already have it installed, download [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/). Use the Visual Studio Installer to [Modify your workloads](https://docs.microsoft.com/visualstudio/install/modify-visual-studio?view=vs-2019#modify-workloads) and ensure that you have the **Mobile development with .NET workload**.

2. Once you've created a new project, you can use the [Android Device Manager](https://docs.microsoft.com/xamarin/android/get-started/installation/android-emulator/device-manager?tabs=windows&pivots=windows#requirements) to create, duplicate, customize, and launch a variety of Android virtual devices. Launch the Android Device Manager from the Tools menu with: **Tools** > **Android** > **Android Device Manager**.

3. Once the Android Device Manager opens, select **+ New** to create a new device.

4. You will need to give the device a name, choose the base device type from a drop-down menu, choose a processor, and OS version, along with several other variables for the virtual device. For more information, [Android Device Manager Main Screen](https://docs.microsoft.com/xamarin/android/get-started/installation/android-emulator/device-manager?tabs=windows&pivots=windows#main-screen).
