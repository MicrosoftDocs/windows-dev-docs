---
author: TylerMSFT
title: Launch the Windows Settings app
description: Learn how to launch the Windows Settings app from your app. This topic describes the ms-settings URI scheme. Use this URI scheme to launch the Windows Settings app to specific settings pages.
ms.assetid: C84D4BEE-1FEE-4648-AD7D-8321EAC70290
ms.author: twhitney
ms.date: 04/05/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Launch the Windows Settings app

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

**Important APIs**

-   [**LaunchUriAsync**](https://msdn.microsoft.com/library/windows/apps/hh701476)
-   [**PreferredApplicationPackageFamilyName**](https://msdn.microsoft.com/library/windows/apps/hh965482)
-   [**DesiredRemainingView**](https://msdn.microsoft.com/library/windows/apps/dn298314)

Learn how to launch the Windows Settings app from your app. This topic describes the **ms-settings:** URI scheme. Use this URI scheme to launch the Windows Settings app to specific settings pages.

Launching to the Settings app is an important part of writing a privacy-aware app. If your app can't access a sensitive resource, we recommend providing the user a convenient link to the privacy settings for that resource. For more info, see [Guidelines for privacy-aware apps](https://msdn.microsoft.com/library/windows/apps/hh768223).

## How to launch the Settings app

To launch the **Settings** app, use the `ms-settings:` URI scheme as shown in the following examples.

In this example, a Hyperlink XAML control is used to launch the privacy settings page for the microphone using the `ms-settings:privacy-microphone` URI.

```xml
<!--Set Visibility to Visible when access to the microphone is denied -->  
<TextBlock x:Name="LocationDisabledMessage" FontStyle="Italic"
                 Visibility="Collapsed" Margin="0,15,0,0" TextWrapping="Wrap" >
          <Run Text="This app is not able to access the microphone. Go to " />
              <Hyperlink NavigateUri="ms-settings:privacy-microphone">
                  <Run Text="Settings" />
              </Hyperlink>
          <Run Text=" to check the microphone privacy settings."/>
</TextBlock>
```

Alternatively, your app can call the [**LaunchUriAsync**](https://msdn.microsoft.com/library/windows/apps/hh701476) method to launch the **Settings** app from code. This example shows how to launch to the privacy settings page for the camera using the `ms-settings:privacy-webcam` URI.

```cs
bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));
```

The code above launches the privacy settings page for the camera:

![camera privacy settings.](images/privacyawarenesssettingsapp.png)

For more info about launching URIs, see [Launch the default app for a URI](launch-default-app.md).

## ms-settings: URI scheme reference

Use the following URIs to open various pages of the Settings app. Note that the Supported SKUs column indicates whether the settings page exists in Windows 10 for desktop editions (Home, Pro, Enterprise, and Education), Windows 10 Mobile, or both.

<table border="1">
  <th>Category</th>
  <th>Settings page</th>
  <th>Supported SKUs</th>
  <th>URI</th>
 </tr>
 <tr>
  <td rowspan="7">Accounts</td>
  <td>Access work or school</td>
  <td>Both</td>
  <td>ms-settings:workplace</td>
 </tr>
 <tr>
  <td>Email & app accounts</td>
  <td>Both</td>
  <td>ms-settings:emailandaccounts</td>
 </tr>
 <tr>
  <td>Family & other people</td>
  <td>Both</td>
  <td>ms-settings:otherusers</td>
 </tr>
 <tr>
  <td>Sign-in options</td>
  <td>Both</td>
  <td>ms-settings:signinoptions</td>
 </tr>
 <tr>
  <td>Sync your settings</td>
  <td>Both</td>
  <td>ms-settings:sync</td>
 </tr>
 <tr>
  <td>Other people</td>
  <td>Both</td>
  <td>ms-settings:otherusers</td>
 </tr>
 <tr>
  <td>Your info</td>
  <td>Both</td>
  <td>ms-settings:yourinfo</td>
 </tr>
 <tr>
  <td rowspan="4">Apps</td>
  <td>Apps & Features</td>
  <td>Both</td>
  <td>ms-settings:appsfeatures</td>
 </tr>
 <tr>
  <td>Apps for websites</td>
  <td>Both</td>
  <td>ms-settings:appsforwebsites</td>
 </tr>
 <tr>
  <td>Default apps</td>
  <td>Desktop only</td>
  <td>ms-settings:defaultapps</td>
 </tr>
 <tr>
  <td>Apps & features</td>
  <td>Desktop only</td>
  <td>ms-settings:optionalfeatures</td>
 </tr>
 <tr>
  <td rowspan="11">Devices</td>
  <td>USB</td>
  <td>Both</td>
  <td>ms-settings:usb</td>
 </tr>
 <tr>
  <td>AutoPlay</td>
  <td>Desktop only</td>
  <td>ms-settings:autoplay</td>
 </tr>
 <tr>
  <td>Touchpad</td>
  <td>Desktop only<br>Only if touchpad hardware is present</td>
  <td>ms-settings:devices-touchpad</td>
 </tr>
 <tr>
  <td>Pen & Windows Ink</td>
  <td>Desktop only</td>
  <td>ms-settings:pen</td>
 </tr>
 <tr>
  <td>Printers & scanners</td>
  <td>Desktop only</td>
  <td>ms-settings:printers</td>
 </tr>
 <tr>
  <td>Typing</td>
  <td>Desktop only</td>
  <td>ms-settings:typing</td>
 </tr>
 <tr>
  <td>Wheel</td>
  <td>Desktop only<br>Only if Dial is paired</td>
  <td>ms-settings:wheel</td>
 </tr>
 <tr>
  <td>Default camera</td>
  <td>Mobile only</td>
  <td>ms-settings:camera</td>
 </tr>
 <tr>
  <td>Bluetooth</td>
  <td>Both</td>
  <td>ms-settings:bluetooth</td>
 </tr>
 <tr>
  <td>Connected Devices</td>
  <td>Desktop only</td>
  <td>ms-settings:connecteddevices</td>
 </tr>
 <tr>
  <td>Mouse & touchpad</td>
  <td>Both<br>Touchpad settings only available on devices that have a touchpad</td>
  <td>ms-settings:mousetouchpad</td>
 </tr>

 <tr>
  <td rowspan="7">Ease of Access</td>
  <td>Narrator</td>
  <td>Both</td>
  <td>ms-settings:easeofaccess-narrator</td>
 </tr>
 <tr>
  <td>Magnifier</td>
  <td>Both</td>
  <td>ms-settings:easeofaccess-magnifier</td>
 </tr>
 <tr>
  <td>High contrast</td>
  <td>Both</td>
  <td>ms-settings:easeofaccess-highcontrast</td>
 </tr>
 <tr>
  <td>Closed captions</td>
  <td>Both</td>
  <td>ms-settings:easeofaccess-closedcaptioning</td>
 </tr>
 <tr>
  <td>Keyboard</td>
  <td>Both</td>
  <td>ms-settings:easeofaccess-keyboard</td>
 </tr>
 <tr>
  <td>Mouse</td>
  <td>Both</td>
  <td>ms-settings:easeofaccess-mouse</td>
 </tr>
 <tr>
  <td>Other options</td>
  <td>Both</td>
  <td>ms-settings:easeofaccess-otheroptions</td>
 </tr>
 <tr>
  <td>Extras</td>
  <td>Extras</td>
  <td>Both<br>Only if "settings apps" are installed (e.g. by 3rd party)</td>
  <td>ms-settings:extras
</td>
 </tr>
 <tr>
  <td rowspan="4">Gaming</td>
  <td>Broadcasting</td>
  <td>Desktop only</td>
  <td>ms-settings:gaming-broadcasting</td>
 </tr>
 <tr>
  <td>Game bar</td>
  <td>Desktop only</td>
  <td>ms-settings:gaming-gamebar</td>
 </tr>
 <tr>
  <td>Game DVR</td>
  <td>Desktop only</td>
  <td>ms-settings:gaming-gamedvr</td>
 </tr>
 <tr>
  <td>Game Mode</td>
  <td>Desktop only</td>
  <td>ms-settings:gaming-gamemode</td>
 </tr>
 <tr>
  <td>Home page</td>
  <td>Landing page for Settings</td>
  <td>Both</td>
  <td>ms-settings:</td>
 </tr>
 <tr>
  <td rowspan="14">Network & internet</td>
  <td>Ethernet</td>
  <td>Both</td>
  <td>ms-settings:network-ethernet</td>
 </tr>
 <tr>
  <td>VPN</td>
  <td>Both</td>
  <td>ms-settings:network-vpn</td>
 </tr>
 <tr>
  <td>Cellular & SIM</td>
  <td>Both</td>
  <td>ms-settings:network-cellular</td>
 </tr>
 <tr>
  <td>Dial-up</td>
  <td>Desktop only</td>
  <td>ms-settings:network-dialup</td>
 </tr>
 <tr>
  <td>DirectAccess</td>
  <td>Desktop only<br>Only if DirectAccess is enabled</td>
  <td>ms-settings:network-directaccess</td>
 </tr>
 <tr>
  <td>Wi-Fi Calling</td>
  <td>Both<br>Only if Wi-Fi calling is enabled</td>
  <td>ms-settings:network-wificalling</td>
 </tr>
 <tr>
  <td>Data usage</td>
  <td>Both</td>
  <td>ms-settings:datausage</td>
 </tr>
 <tr>
  <td>Cellular & SIM</td>
  <td>Both</td>
  <td>ms-settings:network-cellular</td>
 </tr>
 <tr>
  <td>Mobile hotspot</td>
  <td>Both</td>
  <td>ms-settings:network-mobilehotspot</td>
 </tr>
 <tr>
  <td>Proxy</td>
  <td>Desktop only</td>
  <td>ms-settings:network-proxy</td>
 </tr>
 <tr>
  <td>Status</td>
  <td>Desktop only</td>
  <td>ms-settings:network-status</td>
 </tr>
 <tr>
  <td>NFC</td>
  <td>Mobile only</td>
  <td>ms-settings:nfctransactions</td>
 </tr>
 <tr>
  <td>Wi-Fi</td>
  <td>Both</td>
  <td>ms-settings:network-wifi</td>
 </tr>
 <tr>
  <td>Airplane mode</td>
  <td>Both</td>
  <td>ms-settings:network-airplanemode</td>
 </tr>
 <tr>
  <td rowspan="12">Personalization</td>
  <td>Start</td>
  <td>Desktop only</td>
  <td>ms-settings:personalization-start</td>
 </tr>
 <tr>
  <td>Themes</td>
  <td>Desktop only</td>
  <td>ms-settings:themes</td>
 </tr>
 <tr>
  <td>Glance</td>
  <td>Mobile only</td>
  <td>ms-settings:personalization-glance</td>
 </tr>
 <tr>
  <td>Navigation bar</td>
  <td>Mobile only</td>
  <td>ms-settings:personalization-navbar</td>
 </tr>
 <tr>
  <td>Personalization (category)</td>
  <td>Both</td>
  <td>ms-settings:personalization</td>
 </tr>
 <tr>
  <td>Background</td>
  <td>Desktop only</td>
  <td>ms-settings:personalization-background</td>
 </tr>
 <tr>
  <td>Colors</td>
  <td>Both</td>
  <td>ms-settings:personalization-colors</td>
 </tr>
 <tr>
  <td>Sounds</td>
  <td>Mobile only</td>
  <td>ms-settings:sounds</td>
 </tr>
 <tr>
  <td>Lock screen</td>
  <td>Both</td>
  <td>ms-settings:lockscreen</td>
 </tr>
 <tr>
  <td>Themes</td>
  <td>Desktop only</td>
  <td>ms-settings:themes</td>
 </tr>
 <tr>
  <td>Start</td>
  <td>Desktop only</td>
  <td>ms-settings:personalization-start</td>
 </tr>
 <tr>
  <td>Task Bar</td>
  <td>Desktop only</td>
  <td>ms-settings:taskbar</td>
 </tr>
 <tr>
  <td rowspan="22">Privacy</td>
  <td>App diagnostics</td>
  <td>Both</td>
  <td>ms-settings:privacy-appdiagnostics</td>
 </tr>
 
 <tr>
  <td>Notifications</td>
  <td>Both</td>
  <td>ms-settings:privacy-notifications</td>
 </tr>
 <tr>
  <td>Tasks</td>
  <td>Both</td>
  <td>ms-settings:privacy-tasks</td>
 </tr>
 <tr>
  <td>General</td>
  <td>Desktop only</td>
  <td>ms-settings:privacy-general</td>
 </tr>
 <tr>
  <td>Accessory apps</td>
  <td>Mobile only</td>
  <td>ms-settings:privacy-accessoryapps</td>
 </tr>
 <tr>
  <td>Advertising ID</td>
  <td>Mobile only</td>
  <td>ms-settings:privacy-advertisingid</td>
 </tr>
 <tr>
  <td>Phone calls</td>
  <td>Mobile only</td>
  <td>ms-settings:privacy-phonecall</td>
 </tr>
 <tr>
  <td>Location</td>
  <td>Both</td>
  <td>ms-settings:privacy-location</td>
 </tr>
 <tr>
  <td>Camera</td>
  <td>Both</td>
  <td>ms-settings:privacy-webcam</td>
 </tr>
 <tr>
  <td>Microphone</td>
  <td>Both</td>
  <td>ms-settings:privacy-microphone</td>
 </tr>
 <tr>
  <td>Motion</td>
  <td>Both</td>
  <td>ms-settings:privacy-motion</td>
 </tr>
 <tr>
  <td>Speech, inking & typing</td>
  <td>Both</td>
  <td>ms-settings:privacy-speechtyping</td>
 </tr>
 <tr>
  <td>Account info</td>
  <td>Both</td>
  <td>ms-settings:privacy-accountinfo</td>
 </tr>
 <tr>
  <td>Contacts</td>
  <td>Both</td>
  <td>ms-settings:privacy-contacts</td>
 </tr>
 <tr>
  <td>Calendar</td>
  <td>Both</td>
  <td>ms-settings:privacy-calendar</td>
 </tr>
 <tr>
  <td>Call history</td>
  <td>Both</td>
  <td>ms-settings:privacy-callhistory</td>
 </tr>
 <tr>
  <td>Email</td>
  <td>Both</td>
  <td>ms-settings:privacy-email</td>
 </tr>
 <tr>
  <td>Messaging</td>
  <td>Both</td>
  <td>ms-settings:privacy-messaging</td>
 </tr>
 <tr>
  <td>Radios</td>
  <td>Both</td>
  <td>ms-settings:privacy-radios</td>
 </tr>
 <tr>
  <td>Background Apps</td>
  <td>Both</td>
  <td>ms-settings:privacy-backgroundapps</td>
 </tr>
 <tr>
  <td>Other devices</td>
  <td>Both</td>
  <td>ms-settings:privacy-customdevices</td>
 </tr>
 <tr>
  <td>Feedback & diagnostics</td>
  <td>Both</td>
  <td>ms-settings:privacy-feedback</td>
 </tr>
 <tr>
  <td rowspan="19">System</td>
  <td>Shared experiences</td>
  <td>Both</td>
  <td>ms-settings:crossdevice</td>
 </tr>
 <tr>
  <td>Display</td>
  <td>Both</td>
  <td>ms-settings:display</td>
 </tr>
 <tr>
  <td>Multitasking</td>
  <td>Desktop only</td>
  <td>ms-settings:multitasking</td>
 </tr>
 <tr>
  <td>Projecting to this PC</td>
  <td>Desktop only</td>
  <td>ms-settings:project</td>
 </tr>
 <tr>
  <td>Tablet mode</td>
  <td>Desktop only</td>
  <td>ms-settings:tabletmode</td>
 </tr>
 <tr>
  <td>Taskbar</td>
  <td>Desktop only</td>
  <td>ms-settings:taskbar</td>
 </tr>
 <tr>
  <td>Phone</td>
  <td>Mobile only</td>
  <td>ms-settings:phone-defaultapps</td>
 </tr>
 <tr>
  <td>Display</td>
  <td>Both</td>
  <td>ms-settings:screenrotation</td>
 </tr>
 <tr>
  <td>Notifications & actions</td>
  <td>Both</td>
  <td>ms-settings:notifications</td>
 </tr>
 <tr>
  <td>Phone</td>
  <td>Mobile only</td>
  <td>ms-settings:phone</td>
 </tr>
 <tr>
  <td>Messaging</td>
  <td>Mobile only</td>
  <td>ms-settings:messaging</td>
 </tr>
 <tr>
  <td>Battery Saver</td>
  <td>Both<br>Only available on devices that have a battery, such as a tablet</td>
  <td>ms-settings:batterysaver</td>
 </tr>
 <tr>
  <td>Battery use</td>
  <td>Both<br>Only available on devices that have a battery, such as a tablet</td>
  <td>ms-settings:batterysaver-usagedetails</td>
 </tr>
 <tr>
  <td>Power & sleep</td>
  <td>Desktop only</td>
  <td>ms-settings:powersleep</td>
 </tr>
 <tr>
  <td>About</td>
  <td>Both</td>
  <td>ms-settings:about</td>
 </tr>
 <tr>
  <td>Storage</td>
  <td>Both</td>
  <td>ms-settings:storagesense</td>
 </tr>
 <tr>
  <td>Storage Sense</td>
  <td>Desktop only</td>
  <td>ms-settings:storagepolicies</td>
 </tr>
 <tr>
  <td>Encryption</td>
  <td>Both</td>
  <td>ms-settings:deviceencryption</td>
 </tr>
 <tr>
  <td>Offline Maps</td>
  <td>Both</td>
  <td>ms-settings:maps</td>
 </tr>
 <tr>
  <td>Time & language</td>
  <td>Speech</td>
  <td>Both</td>
  <td>ms-settings:speech</td>
 </tr>
 <tr>
  <td rowspan="2">Time and language</td>
  <td>Date & time</td>
  <td>Both</td>
  <td>ms-settings:dateandtime</td>
 </tr>
 <tr>
  <td>Region & language</td>
  <td>Desktop only</td>
  <td>ms-settings:regionlanguage</td>
 </tr>
 <tr>
  <td rowspan="13">Update & security</td>
  <td>Backup</td>
  <td>Both</td>
  <td>ms-settings:backup</td>
 </tr>
 <tr>
  <td>Find My Device</td>
  <td>Both</td>
  <td>ms-settings:findmydevice</td>
 </tr>
 <tr>
  <td>Windows Insder Program</td>
  <td>Both<br>Only present if user is enrolled in WIP</td>
  <td>ms-settings:windowsinsider</td>
 </tr>
 <tr>
  <td>Windows Update</td>
  <td>Both</td>
  <td>ms-settings:windowsupdate</td>
 </tr>
 <tr>
  <td>Windows Update</td>
  <td>Both</td>
  <td>ms-settings:windowsupdate-history</td>
 </tr>
 <tr>
  <td>Windows Update</td>
  <td>Both</td>
  <td>ms-settings:windowsupdate-options</td>
 </tr>
 <tr>
  <td>Windows Update</td>
  <td>Both</td>
  <td>ms-settings:windowsupdate-restartoptions</td>
 </tr>
 <tr>
  <td>Windows Update</td>
  <td>Both</td>
  <td>ms-settings:windowsupdate-action</td>
 </tr>
 <tr>
  <td>Activation</td>
  <td>Desktop only</td>
  <td>ms-settings:activation</td>
 </tr>
 <tr>
  <td>Recovery</td>
  <td>Desktop only</td>
  <td>ms-settings:recovery</td>
 </tr>
 <tr>
  <td>Troubleshoot</td>
  <td>Desktop only</td>
  <td>ms-settings:troubleshoot</td>
 </tr>
 <tr>
  <td>Windows Defender</td>
  <td>Desktop only</td>
  <td>ms-settings:windowsdefender</td>
 </tr>
 <tr>
  <td>For developers</td>
  <td>Both</td>
  <td>ms-settings:developers</td>
 </tr>
 <tr>
  <td rowspan="2">User  Accounts</td>
  <td>Windows Anywhere</td>
  <td>Both<br>Device must be Windows Anywhere-capable
</td>
  <td>ms-settings:windowsanywhere</td>
 </tr>
 <tr>
  <td>Provisioning</td>
  <td>Both<br>Only if enterprise has deployed a provisioning package.</td>
  <td>ms-settings:workplace-provisioning</td>
 </tr>
</table>

