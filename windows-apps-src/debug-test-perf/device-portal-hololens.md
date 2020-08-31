---
ms.assetid: bf0a8b01-79f1-4944-9d78-9741e235dbe9
title: Device Portal for HoloLens
description: Learn how the Windows Device Portal for HoloLens lets you remotely configure and manage your HoloLens device.
ms.date: 01/03/2019
ms.topic: article
keywords: windows 10, uwp, device portal
ms.localizationpriority: medium
---
# Device Portal for HoloLens


## Set up device portal on HoloLens

### Enable Device Portal

1. Power on your HoloLens and put on the device.
2. Perform the [Start gesture](/hololens/hololens2-basic-usage#start-gesture) or [bloom](https://developer.microsoft.com/mixed-reality#Bloom) gesture for HoloLens (1st Gen) to launch the main menu.
3. Gaze at the **Settings** tile and perform the [tap](https://developer.microsoft.com/mixed-reality#Press_and_release) gesture on HoloLens (1st Gen) or select it on HoloLens 2 by [touching it or using a Hand ray](/hololens/hololens2-basic-usage). The Settings app will launch after you select it.
4. Select the **Update** menu item.
5. Select the **For developers** menu item.
6. Enable **Developer Mode**.
7. [Scroll down](https://developer.microsoft.com/mixed-reality#Navigation) and enable Device Portal.


### Pair your device

#### Connect over Wi-Fi 

1. Connect your HoloLens to Wi-Fi.
2. Look up your device's IP address. Find the IP address on the device under **Settings > Network & Internet > Wi-Fi > Hardware properties**. You can also ask, "Hey Cortana, what is my IP address?"

3. From a web browser on your PC, go to `https://<YOUR_HOLOLENS_IP_ADDRESS>`
    - The browser will display the following message: "There's a problem with this website's security certificate". This happens because the certificate which is issued to the Device Portal is a test certificate. You can ignore this certificate error for now and proceed.

#### Connect over USB 

1. Install the tools to make sure you have Visual Studio Update 1 with the Windows 10 developer tools installed on your PC. This enables USB connectivity.
2. Connect your HoloLens to your PC with a micro-USB cable for HoloLens (1st Gen) or USB-C for HoloLens 2.
3. From a web browser on your PC, go to `http://127.0.0.1:10080`.

> [!IMPORTANT]
> If your PC is unable to find the device, try using the real network IP address of the HoloLens device, rather than `http://127.0.0.1:10080`.

#### Connect to an emulator 

You can also use the Device Portal with your emulator. To connect to the Device Portal, use the toolbar. Click on this icon:
- Open Device Portal: Open the Windows Device Portal for the HoloLens OS in the emulator.

#### Create a Username and Password 

The first time you connect to the Device Portal on your HoloLens, you will need to create a username and password.
1. In a web browser on your PC, enter the IP address of the HoloLens. The Set up access page opens.
2. Click or tap Request pin and look at the HoloLens display to get the generated PIN.
3. Enter the PIN in the PIN displayed on your device textbox.
4. Enter the user name you will use to connect to the Device Portal. It doesn't need to be a Microsoft Account (MSA) name or a domain name.
5. Enter a password and confirm it. The password must be at least seven characters in length. It doesn't need to be an MSA or domain password.
6. Click Pair to connect to Windows Device Portal on the HoloLens.

If you wish to change this username or password at any time, you can repeat this process by visiting the device security page by either clicking the Security link along the top right, or navigating to: `https://<YOUR_HOLOLENS_IP_ADDRESS>/devicesecurity.htm`.

#### Security certificate 

If you are see a "certificate error" in your browser, you can fix it by creating a trust relationship with the device.

Each HoloLens generates a unique self-signed certificate for its SSL connection. By default, this certificate is not trusted by your PC's web browser and you may get a "certificate error". By downloading this certificate from your HoloLens (over USB or a Wi-Fi network you trust) and trusting it on your PC, you can securely connect to your device.
1. Make sure you are on a secure network (USB or a Wi-Fi network you trust).
2. Download this device's certificate from the "Security" page on the Device Portal.- Either click the Security link from the top right list of icons or navigate to: `https://<YOUR_HOLOLENS_IP_ADDRESS>/devicesecurity.htm`

3. Install the certificate in the "Trusted Root Certification Authorities" store on your PC.- From the Windows menu, type: Manage Computer Certificates and start the applet.
    - Expand the Trusted Root Certification Authority folder.
    - Click on the Certificates folder.
    - From the Action menu, select: All Tasks > Import...
    - Complete the Certificate Import Wizard, using the certificate file you downloaded from the Device Portal.

4. Restart the browser.


## Device Portal Pages 

### Home 

Your Device Portal session starts at the Home page. Access other pages from the navigation bar along the left side of the home page.

The toolbar at the top of the page provides access to commonly used status and features.
- **Online**: Indicates whether the device is connected to Wi-Fi.
- **Shutdown**: Turns off the device.
- **Restart**: Cycles power on the device.
- **Security**: Opens the Device Security page.
- **Cool**: Indicates the temperature of the device.
- **A/C**: Indicates whether the device is plugged in and charging.
- **Help**: Opens the REST interface documentation page.

The home page shows the following info:
- **Device** Status: monitors the health of your device and reports critical errors.
- **Windows information**: shows the name of the HoloLens and the currently installed version of Windows.
- **Preferences** section contains the following settings:
    - **IPD**: Sets the interpupillary distance (IPD), which is the distance, in millimeters, between the center of the user's pupils when looking straight ahead. The setting takes effect immediately. The default value was calculated automatically when you set up your device. **Valid for HoloLens (1st Gen) only, Hololens 2 computes eye position.** 
    - **Device name**: Assign a name to the HoloLens. You must reboot the device after changing this value for it to take effect. After clicking Save, a dialog will ask if you want to reboot the device immediately or reboot later.
    - **Sleep settings**: Sets the length of time to wait before the device goes to sleep when it's plugged in and when it's on battery.

### 3D View 

Use the 3D View page to see how HoloLens interprets your surroundings. Navigate the view by using the mouse:
- **Rotate**: left click + mouse;
- **Pan**: right click + mouse;
- **Zoom**: mouse scroll.
- **Tracking options**: Turn on continuous visual tracking by checking Force visual tracking. Pause stops visual tracking.
- **View options**: Set options on the 3D view:- Tracking: Indicates whether visual tracking is active.
- **Show floor**: Displays a checkered floor plane.
- **Show frustum**: Displays the view frustum.
- **Show stabilization plane**: Displays the plane that HoloLens uses for stabilizing motion.
- **Show mesh**: Displays the surface mapping mesh that represents your surroundings.
- **Show details**: Displays hand positions, head rotation quaternions, and the device origin vector as they change in real time.
- **Full screen button**: Shows the 3D View in full screen mode. Press ESC to exit full screen view.

- Surface reconstruction: Click or tap Update to display the latest spatial mapping mesh from the device. A full pass may take some time to complete, up to a few seconds. The mesh does not update automatically in the 3D view, and you must manually click Update to get the latest mesh from the device. Click Save to save the current spatial mapping mesh as an obj file on your PC.

### Mixed Reality Capture 

Use the Mixed Reality Capture page to save media streams from the HoloLens.
- Settings: Control the media streams that are captured by checking the following settings:- Holograms: Captures the holographic content in the video stream. Holograms are rendered in mono, not stereo.
- **PV camera**: Captures the video stream from the photo/video camera.
- **Mic Audio**: Captures audio from the microphone array.
- **App Audio**: Captures audio from the currently running app.
- **Live preview quality**: Select the screen resolution, frame rate, and streaming rate for the live preview.

- Click or tap the Live preview button to show the capture stream. Stop live preview stops the capture stream.
- Click or tap Record to start recording the mixed-reality stream, using the specified settings. Stop recording ends the recording and saves it.
- Click or tap Take photo to take a still image from the capture stream.
- **Videos and photos**: Shows a list of video and photo captures taken on the device.

Note that HoloLens apps will not be able to capture an MRC photo or video while you are recording or streaming a live preview from the Device Portal.

### System Performance 

The System Performance tool on HoloLens has 3 additional metrics that can be recorded. 

These are the available metrics:
- **SoC power**: Instantaneous system-on-chip power utilization, averaged over one minute
- **System power**: Instantaneous system power utilization, averaged over one minute
- **Frame rate**: Frames per second, missed VBlanks per second, and consecutive missed VBlanks

### App Crash Dumps Page 

This page allows you to collect crash dumps for your side-loaded apps. Check the Crash Dumps Enabled checkbox for each app for which you want to collect crash dumps. Return to this page to collect crash dumps. Dump files can be opened in Visual Studio for debugging.

### Kiosk Mode 

Enables kiosk mode, which limits the user's ability to launch new apps or change the running app. When kiosk mode is enabled, the Bloom gesture and Cortana are disabled, and placed apps aren't shown in the user's surroundings.

Check Enable Kiosk Mode to put the HoloLens into kiosk mode. Select the app to run at startup from the Startup app dropdown. Click or tap Save to commit the settings.

Note that the app will run at startup even if kiosk mode is not enabled. Select None to have no app run at startup.

### Simulation 

Allows you to record and play back input data for testing.
- **Capture room**: Used to download a simulated room file that contains the spatial mapping mesh for the user's surroundings. Name the room and then click Capture to save the data as a .xef file on your PC. This room file can be loaded into the HoloLens emulator.
- **Recording**: Check the streams to record, name the recording, and click or tap Record to start recording. Perform actions with your HoloLens and then click Stop to save the data as a .xef file on your PC. This file can be loaded on the HoloLens emulator or device.
- **Playback**: Click or tap Upload recording to select a xef file from your PC and send the data to the HoloLens.
- **Control mode**: Select Default or Simulation from the dropdown, and click or tap the Set button to select the mode on the HoloLens. Choosing "Simulation" disables the real sensors on your HoloLens and uses uploaded simulated data instead. If you switch to "Simulation", your HoloLens will not respond to the real user until you switch back to "Default".


### Virtual Input 

Sends keyboard input from the remote machine to the HoloLens.

Click or tap the region under Virtual keyboard to enable sending keystrokes to the HoloLens. Type in the Input text textbox and click or tap Send to send the keystrokes to the active app.

## See also

* [Windows Device Portal overview](device-portal.md)
* [Device Portal core API reference](./device-portal-api-core.md) (APIs common to all Windows 10 devices)
* [Device Portal mixed reality API reference](/windows/mixed-reality/device-portal-api-reference) (an extended list of all REST APIs available for HoloLens)