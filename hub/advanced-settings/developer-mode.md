---
ms.assetid: 54973C62-9669-4988-934E-9273FB0425FD
title: Settings for developers
description: Activate Developer Mode on your PC to develop apps.
keywords: Developer mode, Visual Studio, enable device
ms.date: 11/12/2025
ms.topic: how-to
---

# Settings for developers

The  **[System > Advanced](ms-settings:developers)** page in Windows settings includes Developer Mode and additional features that you can use when Developer Mode is enabled. Developer Mode unlocks tools, settings, and features designed for building, deploying, and testing apps on Windows.

:::image type="content" source="images/for-developers.png" alt-text="A screenshot of Windows Advanced settings for developers, showing Developer Mode, Device Portal, and Device discovery.":::

> [!NOTE]
> Prior to Windows 11 25H2, these settings appear on the **For developers** page in Windows settings. In Windows 11 25H2 and later, they appear in the **For developers** section of the [Advanced settings](index.md) page.

## Enable Developer Mode

If you're writing software with Visual Studio, you *will* need to enable Developer Mode on both the development PC and on any devices you'll use to test your code.

> [!IMPORTANT]
> If you're using your computer for ordinary day-to-day activities (such as gaming, web browsing, email, or Office apps), there is no need to activate Developer Mode. If you're trying to fix an issue with your computer, check out [Windows help](/windows).

To enable Developer Mode, or access other settings:

1. Open Windows Settings.
2. Search for **Advanced** or go to  **[System > Advanced](ms-settings:developers)**, then scroll to the **For developers** section.
3. Toggle the **Developer Mode setting**, at the top of the **For developers** section.
4. Read the disclaimer. Click **Yes** to accept the change.

   :::image type="content" source="images/use-developer-features.png" alt-text="Developer Mode dialog in Visual Studio":::

> [!NOTE]
> Enabling Developer mode requires administrator access. If your device is owned by an organization, this option may be disabled.

If you try to build a Windows project in Visual Studio when Developer Mode *isn't* enabled, the following dialog appears in Visual Studio:

:::image type="content" source="images/enable-developer-mode-dialog.jpg" alt-text="Developer Mode dialog in Visual Studio that says Developer Mode needs to be enabled, with a link to settings for developers.":::

If you see this dialog, select **settings for developers** to open the  **[System > Advanced](ms-settings:developers)** settings page.

> [!NOTE]
> You can go to the **Advanced** settings page at any time to *enable* or *disable* Developer Mode.

## Developer Mode features

Developer Mode replaces the requirements for a developer license. In addition to sideloading, the Developer Mode setting enables debugging and additional deployment options. This includes starting an SSH service to allow deployment to this device. In order to stop this service, you need to disable Developer Mode.

When you enable Developer Mode on desktop, a package of features is installed, including:

- **Windows Device Portal**: Device Portal is only enabled (and firewall rules are only configured for it) when the **Enable Device Portal** option is turned on.
- Installs and configures firewall rules for SSH services that allow remote installation of apps. Enabling **Device Discovery** will turn on the SSH server.

> [!NOTE]
> _Device Portal_ and _Device discovery_ are useful when you need to develop on one machine, but deploy your app to another machine for testing. For example, if you need to deploy your app to a tablet to test a touch-optimized tablet user interface.

### Device Portal

To learn more about Device Portal, see [Windows Device Portal overview](/windows/uwp/debug-test-perf/device-portal).

For specific setup instructions, see [Device Portal for desktop](/windows/uwp/debug-test-perf/device-portal-desktop).

### Device Discovery

When you enable Device Discovery, you're allowing your device to be visible to other devices on the network through mDNS. This feature also allows you to get the SSH PIN for pairing to the device by pressing the **Pair** button exposed immediately after Device Discovery is enabled. This PIN prompt must be displayed on the screen in order to complete your first Visual Studio deployment targeting the device.

:::image type="content" source="images/pair-device-with-device-discovery.png" alt-text="A screenshot of the Pair device dialog that displays the SSH Pin for device pairing.":::

You should enable Device Discovery only if you intend to make the device a deployment target. For example, if you use Device Portal to deploy an app to a tablet for testing, you need to enable Device Discovery on the tablet, but not on your development PC.

#### SSH

SSH services are enabled when you enable Device Discovery on your device. This is used when your device is a remote deployment target for MSIX packaged applications. The names of the services are *SSH Server Broker* and *SSH Server Proxy*.

> [!NOTE]
> This is not Microsoft's OpenSSH implementation, which you can find on [GitHub](https://github.com/PowerShell/Win32-OpenSSH).

In order to take advantage of the SSH services, you can enable Device Discovery to allow pin pairing. If you intend to run another SSH service, you can set this up on a different port or turn off the Developer Mode SSH services. To turn off the SSH services, turn off Device Discovery.

SSH login is done via the *DevToolsUser* account, which accepts a password for authentication. This password is the PIN displayed on the device after pressing the Device Discovery **Pair** button, and it's only valid while the PIN is displayed. A SFTP subsystem is also enabled for manual management of the `DevelopmentFiles` folder where loose file deployments are installed from Visual Studio.

##### Caveats for SSH usage

The existing SSH server used in Windows is not yet protocol compliant. Using an SFTP or SSH client may require special configuration. In particular, the SFTP subsystem runs at version 3 or less, so any connecting client should be configured to expect an old server. The SSH server on older devices uses `ssh-dss` for public key authentication (which OpenSSH has deprecated). To connect to such devices, the SSH client must be manually configured to accept `ssh-dss`.

## Failure to install Developer Mode package

Sometimes, due to network or administrative issues, Developer Mode won't install correctly. The Developer Mode package is required for remote deployment to this PC (using Device Portal from a browser or Device Discovery to enable SSH), but not for local development. Even if you encounter these issues, you can still deploy your app locally using Visual Studio (or from this device to another device).

If Developer Mode doesn't install correctly, we encourage you to file a feedback request using the Feedback Hub app.

> [!NOTE]
> 1. Install the [Feedback Hub app](https://apps.microsoft.com/store/detail/feedback-hub/9NBLGGH4R32N) (if you don't already have it) and open it.
> 2. Click **Add new feedback**.
> 3. Choose the **Developer Platform** category and the **Developer Mode** subcategory.
> 4. Fill out the fields (you may optionally attach a screenshot) and click **Submit**.
>
> Submitting feedback will help Microsoft resolve the issue you encountered.

### Failed to locate the package

> Developer Mode package couldn't be located in Windows Update. Error Code 0x80004005. Learn more.

This error may occur due to a network connectivity problem, Enterprise settings, or the package may be missing.

To fix this issue:

1. Ensure that your computer is connected to the internet.
2. If you're on a domain-joined computer, speak to your network administrator. The Developer Mode package (like all Features on Demand) is blocked by default in WSUS 2.1. In order to unblock the Developer Mode package in the current and previous releases, the following KBs should be allowed in WSUS:

    - 4016509
    - 3180030
    - 3197985

3. Check for Windows updates in **Settings &rarr; Updates and Security &rarr; Windows Updates**.
1. Verify that the Windows Developer Mode package is present in **Settings &rarr; System &rarr; Optional features &rarr; Add a feature** (on versions older than Windows 10 22H2, look under **Settings** **→** **Apps** **→** **Apps & features** **→ Optional features** **→** **Add a feature**). If it's missing, Windows can't find the correct package for your computer.

5. After performing the above steps, *disable* and then *re-enable* Developer Mode to verify the fix.

### Failed to install the package

> Developer Mode package failed to install. Error code 0x80004005. Learn more.

This error may occur due to incompatibilities between your build of Windows and the Developer Mode package.

To fix this issue:

1. Check for Windows updates in the **Settings &rarr; Updates and Security &rarr; Windows Updates**.
2. Restart your computer to ensure all updates are applied.

## Use group policies or registry keys to enable a device

For most developers, you'll want to use Windows Settings to enable your device for debugging. In certain scenarios (such as automated tests) you can use other ways to enable your Windows desktop device for development.

> [!NOTE]
> These steps will not enable the SSH server or allow the device to be targeted for remote deployment and debugging.

You can use `gpedit.msc` to set the group policies to enable your device, unless you have *Windows 10 Home* or *Windows 11 Home*. If you do, you'll need to use regedit or PowerShell commands to set the registry keys directly to enable your device.

### Use gpedit to enable your device

1. Run `gpedit.msc`.

2. Go to **Local Computer Policy &rarr; Computer Configuration &rarr; Administrative Templates &rarr; Windows Components &rarr; App Package Deployment**.

3. Edit the following policies to enable sideloading:

    - Allow all trusted apps to install.

    OR

    Edit the following policies to enable both sideloading and Developer Mode:

    - Allow all trusted apps to install.
    - Allows development of UWP apps and installation from an *Integrated Development Environment (IDE)*.
    - Reboot your machine.

### Use regedit to enable your device

1. Run `regedit`.

2. To enable sideloading, set the value of this `DWORD` to `1`:

    `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\AppModelUnlock\AllowAllTrustedApps`

    OR

    To enable developer mode, set the values of this `DWORD` to `1`:

    `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\AppModelUnlock\AllowDevelopmentWithoutDevLicense`

### Use PowerShell to enable your device

1. Run PowerShell with administrator privileges.

2. To enable sideloading, run this command:

    ```powershell
    PS C:\WINDOWS\system32> reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AppModelUnlock" /t REG_DWORD /f /v "AllowAllTrustedApps" /d "1"
    ```

    OR

    To enable developer mode, run this command:

    ```powershell
    PS C:\WINDOWS\system32> reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AppModelUnlock" /t REG_DWORD /f /v "AllowDevelopmentWithoutDevLicense" /d "1"
    ```
