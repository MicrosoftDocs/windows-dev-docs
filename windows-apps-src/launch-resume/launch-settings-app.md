---
author: TylerMSFT
title: Launch the Windows Settings app
description: Learn how to launch the Windows Settings app from your app. This topic describes the ms-settings URI scheme. Use this URI scheme to launch the Windows Settings app to specific settings pages.
ms.assetid: C84D4BEE-1FEE-4648-AD7D-8321EAC70290
ms.author: twhitney
ms.date: 06/12/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# Launch the Windows Settings app


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

Use the following URIs to open various pages of the Settings app.

> Note that whether a settings page is available varies by Windows SKU. Not all settings page available on Windows 10 for desktop are available on Windows 10 Mobile, and vice-versa. The notes column also captures additional requirements that must be met for a page to be available.

<table border="1">
 <tr>
  <th>Category</th>
  <th>Settings page</th>
  <th>URI</th>
  <th>Notes</th>
 </tr>
 <tr>
  <td rowspan="6">Accounts</td>
  <td>Access work or school</td>
  <td>ms-settings:workplace</td>
  <td></td>
 </tr>
 <tr>
  <td>Email & app accounts</td>
  <td>ms-settings:emailandaccounts</td>
  <td></td>
 </tr>
 <tr>
  <td>Family & other people</td>
  <td>ms-settings:otherusers</td>
  <td></td>
 </tr>
 <tr>
  <td>Sign-in options</td>
  <td>ms-settings:signinoptions</td>
  <td></td>
 </tr>
 <tr>
  <td>Sync your settings</td>
  <td>ms-settings:sync</td>
  <td></td>
 </tr>
 <tr>
  <td>Your info</td>
  <td>ms-settings:yourinfo</td>
  <td></td>
 </tr>
 <tr>
  <td rowspan="4">Apps</td>
  <td>Apps & Features</td>
  <td>ms-settings:appsfeatures</td>
  <td></td>
 </tr>
 <tr>
  <td>Apps for websites</td>
  <td>ms-settings:appsforwebsites</td>
  <td></td>
 </tr>
 <tr>
  <td>Default apps</td>
  <td>ms-settings:defaultapps</td>
  <td></td>
 </tr>
 <tr>
  <td>Apps & features</td>
  <td>ms-settings:optionalfeatures</td>
  <td></td>
 </tr>
 <tr>
   <td rowspan="3">Cortana</td>
   <td>Talk to Cortana</td>
   <td>ms-settings:cortana-language</td>
   <td></td>
 </tr>
 <tr>
   <td>More details</td>
   <td>ms-settings:cortana-moredetails</td>
   <td></td>
 </tr>
 <tr>
   <td>Notifications</td>
   <td>ms-settings:cortana-notifications</td>
   <td></td>
 </tr>
 <tr>
  <td rowspan="12">Devices</td>
  <td>USB</td>
  <td>ms-settings:usb</td>
  <td></td>
 </tr>
 <tr>
  <td>Audio and speech</td>
  <td>ms-settings:holographic-audio</td>
  <td>Only available if the Mixed Reality Portal app is installed (available in the Microsoft Store)</td>
 </tr>
 <tr>
  <td>AutoPlay</td>
  <td>ms-settings:autoplay</td>
  <td></td>
 </tr>
 <tr>
  <td>Touchpad</td>
  <td>ms-settings:devices-touchpad</td>
  <td>Only available if touchpad hardware is present</td>
 </tr>
 <tr>
  <td>Pen & Windows Ink</td>
  <td>ms-settings:pen</td>
  <td></td>
 </tr>
 <tr>
  <td>Printers & scanners</td>
  <td>ms-settings:printers</td>
  <td></td>
 </tr>
 <tr>
  <td>Typing</td>
  <td>ms-settings:typing</td>
  <td></td>
 </tr>
 <tr>
  <td>Wheel</td>
  <td>ms-settings:wheel</td>
  <td>Only available if Dial is paired</td>
 </tr>
 <tr>
  <td>Default camera</td>
  <td>ms-settings:camera</td>
  <td></td>
 </tr>
 <tr>
  <td>Bluetooth</td>
  <td>ms-settings:bluetooth</td>
  <td></td>
 </tr>
 <tr>
  <td>Connected Devices</td>
  <td>ms-settings:connecteddevices</td>
  <td></td>
 </tr>
 <tr>
  <td>Mouse & touchpad</td>
  <td>ms-settings:mousetouchpad</td>
  <td>Touchpad settings only available on devices that have a touchpad</td>
 </tr>
 <tr>
  <td rowspan="7">Ease of Access</td>
  <td>Narrator</td>
  <td>ms-settings:easeofaccess-narrator</td>
  <td></td>
 </tr>
 <tr>
  <td>Magnifier</td>
  <td>ms-settings:easeofaccess-magnifier</td>
  <td></td>
 </tr>
 <tr>
  <td>High contrast</td>
  <td>ms-settings:easeofaccess-highcontrast</td>
  <td></td>
 </tr>
 <tr>
  <td>Closed captions</td>
  <td>ms-settings:easeofaccess-closedcaptioning</td>
  <td></td>
 </tr>
 <tr>
  <td>Keyboard</td>
  <td>ms-settings:easeofaccess-keyboard</td>
  <td></td>
 </tr>
 <tr>
  <td>Mouse</td>
  <td>ms-settings:easeofaccess-mouse</td>
  <td></td>
 </tr>
 <tr>
  <td>Other options</td>
  <td>ms-settings:easeofaccess-otheroptions</td>
 </tr>
 <tr>
  <td>Extras</td>
  <td>Extras</td>
  <td>ms-settings:extras</td>
  <td>Only available if "settings apps" are installed (e.g. by 3rd party)</td>
 </tr>
 <tr>
  <td rowspan="6">Gaming</td>
  <td>Broadcasting</td>
  <td>ms-settings:gaming-broadcasting</td>
  <td></td>
 </tr>
 <tr>
  <td>Game bar</td>
  <td>ms-settings:gaming-gamebar</td>
  <td></td>
 </tr>
 <tr>
  <td>Game DVR</td>
  <td>ms-settings:gaming-gamedvr</td>
  <td></td>
 </tr>
 <tr>
  <td>Game Mode</td>
  <td>ms-settings:gaming-gamemode</td>
  <td></td>
 </tr>
 <tr>
  <td>TruePlay</td>
  <td>ms-settings:gaming-trueplay</td>
  <td></td>
 </tr>
 <tr>
   <td>Xbox Networking</td>
   <td>ms-settings:gaming-xboxnetworking</td>
   <td></td>
  </tr>
 <tr>
 <tr>
  <td>Home page</td>
  <td>Landing page for Settings</td>
  <td>ms-settings:</td>
  <td></td>
 </tr>
 <tr>
  <td rowspan="10">Network & internet</td>
  <td>Ethernet</td>
  <td>ms-settings:network-ethernet</td>
  <td></td>
 </tr>
 <tr>
  <td>VPN</td>
  <td>ms-settings:network-vpn</td>
  <td></td>
 </tr>
 <tr>
  <td>Dial-up</td>
  <td>ms-settings:network-dialup</td>
  <td></td>
 </tr>
 <tr>
  <td>DirectAccess</td>
  <td>ms-settings:network-directaccess</td>
  <td>Only available if DirectAccess is enabled</td>
 </tr>
 <tr>
  <td>Wi-Fi Calling</td>
  <td>ms-settings:network-wificalling</td>
  <td>Only available if Wi-Fi calling is enabled</td>
 </tr>
 <tr>
  <td>Data usage</td>
  <td>ms-settings:datausage</td>
  <td></td>
 </tr>
 <tr>
  <td>Cellular & SIM</td>
  <td>ms-settings:network-cellular</td>
  <td></td>
 </tr>
 <tr>
  <td>Mobile hotspot</td>
  <td>ms-settings:network-mobilehotspot</td>
  <td></td>
 </tr>
 <tr>
  <td>Proxy</td>
  <td>ms-settings:network-proxy</td>
  <td></td>
 </tr>
 <tr>
  <td>Status</td>
  <td>ms-settings:network-status</td>
  <td></td>
 </tr>
 <tr>
  <td>Manage known networks</td>
  <td>ms-settings:network-wifisettings</td>
  <td></td>
 </tr>
 <tr>
  <td rowspan="3">Network & wireless</td>
  <td>NFC</td>
  <td>ms-settings:nfctransactions</td>
  <td></td>
 </tr>
 <tr>
  <td>Wi-Fi</td>
  <td>ms-settings:network-wifi</td>
  <td>Only available if the device has a wifi adaptor</td>
 </tr>
 <tr>
  <td>Airplane mode</td>
  <td>ms-settings:network-airplanemode</td>
  <td>Use ms-settings:proximity on Windows 8.x</td>
 </tr>
 <tr>
  <td rowspan="10">Personalization</td>
  <td>Start</td>
  <td>ms-settings:personalization-start</td>
  <td></td>
 </tr>
 <tr>
  <td>Themes</td>
  <td>ms-settings:themes</td>
  <td></td>
 </tr>
 <tr>
  <td>Glance</td>
  <td>ms-settings:personalization-glance</td>
  <td></td>
 </tr>
 <tr>
  <td>Navigation bar</td>
  <td>ms-settings:personalization-navbar</td>
  <td></td>
 </tr>
 <tr>
  <td>Personalization (category)</td>
   <td>ms-settings:personalization</td>
   <td></td>
 </tr>
 <tr>
  <td>Background</td>
   <td>ms-settings:personalization-background</td>
   <td></td>
 </tr>
 <tr>
  <td>Colors</td>
   <td>ms-settings:personalization-colors</td>
   <td></td>
 </tr>
 <tr>
  <td>Sounds</td>
   <td>ms-settings:sounds</td>
   <td></td>
 </tr>
 <tr>
  <td>Lock screen</td>
   <td>ms-settings:lockscreen</td>
   <td></td>
 </tr>
 <tr>
  <td>Task Bar</td>
   <td>ms-settings:taskbar</td>
   <td></td>
 </tr>
 <tr>
  <td rowspan="22">Privacy</td>
  <td>App diagnostics</td>
   <td>ms-settings:privacy-appdiagnostics</td>
   <td></td>
 </tr>
 <tr>
  <td>Notifications</td>
   <td>ms-settings:privacy-notifications</td>
   <td></td>
 </tr>
 <tr>
  <td>Tasks</td>
   <td>ms-settings:privacy-tasks</td>
   <td></td>
 </tr>
 <tr>
  <td>General</td>
   <td>ms-settings:privacy-general</td>
   <td></td>
 </tr>
 <tr>
  <td>Accessory apps</td>
   <td>ms-settings:privacy-accessoryapps</td>
   <td></td>
 </tr>
 <tr>
  <td>Advertising ID</td>
   <td>ms-settings:privacy-advertisingid</td>
   <td></td>
 </tr>
 <tr>
  <td>Phone calls</td>
   <td>ms-settings:privacy-phonecall</td>
   <td></td>
 </tr>
 <tr>
  <td>Location</td>
   <td>ms-settings:privacy-location</td>
   <td></td>
 </tr>
 <tr>
  <td>Camera</td>
   <td>ms-settings:privacy-webcam</td>
   <td></td>
 </tr>
 <tr>
  <td>Microphone</td>
   <td>ms-settings:privacy-microphone</td>
   <td></td>
 </tr>
 <tr>
  <td>Motion</td>
   <td>ms-settings:privacy-motion</td>
   <td></td>
 </tr>
 <tr>
  <td>Speech, inking & typing</td>
   <td>ms-settings:privacy-speechtyping</td>
   <td></td>
 </tr>
 <tr>
  <td>Account info</td>
   <td>ms-settings:privacy-accountinfo</td>
   <td></td>
 </tr>
 <tr>
  <td>Contacts</td>
   <td>ms-settings:privacy-contacts</td>
   <td></td>
 </tr>
 <tr>
  <td>Calendar</td>
   <td>ms-settings:privacy-calendar</td>
   <td></td>
 </tr>
 <tr>
  <td>Call history</td>
   <td>ms-settings:privacy-callhistory</td>
   <td></td>
 </tr>
 <tr>
  <td>Email</td>
  <td>ms-settings:privacy-email</td>
  <td></td>
 </tr>
 <tr>
  <td>Messaging</td>
    <td>ms-settings:privacy-messaging</td>
  <td></td>
 </tr>
 <tr>
  <td>Radios</td>
    <td>ms-settings:privacy-radios</td>
  <td></td>
 </tr>
 <tr>
  <td>Background Apps</td>
    <td>ms-settings:privacy-backgroundapps</td>
  <td></td>
 </tr>
 <tr>
  <td>Other devices</td>
    <td>ms-settings:privacy-customdevices</td>
  <td></td>
 </tr>
 <tr>
  <td>Feedback & diagnostics</td>
    <td>ms-settings:privacy-feedback</td>
  <td></td>
 </tr>
 <tr>
  <td rowspan="5">Surface Hub</td>
  <td>Accounts</td>
    <td>ms-settings:surfacehub-accounts</td>
      <td></td>
  </tr>
  <tr>
    <td>Team Conferencing</td>
      <td>ms-settings:surfacehub-calling</td>
      <td></td>
  </tr>
  <tr>
    <td>Team device management</td>
      <td>ms-settings:surfacehub-devicemanagenent</td>
      <td></td>
  </tr>
  <tr>
    <td>Session cleanup</td>
      <td>ms-settings:surfacehub-sessioncleanup</td>
      <td></td>
  </tr>
  <tr>
    <td>Welcome screen</td>
      <td>ms-settings:surfacehub-welcome</td>
      <td></td>
  </tr>
    <td rowspan="20">System</td>
    <td>Shared experiences</td>
      <td>ms-settings:crossdevice</td>
    <td></td>
 </tr>
 <tr>
  <td>Display</td>
    <td>ms-settings:display</td>
  <td></td>
 </tr>
 <tr>
  <td>Multitasking</td>
    <td>ms-settings:multitasking</td>
  <td></td>
 </tr>
 <tr>
  <td>Projecting to this PC</td>
    <td>ms-settings:project</td>
  <td></td>
 </tr>
 <tr>
  <td>Tablet mode</td>
    <td>ms-settings:tabletmode</td>
  <td></td>
 </tr>
 <tr>
  <td>Taskbar</td>
    <td>ms-settings:taskbar</td>
  <td></td>
 </tr>
 <tr>
  <td>Phone</td>
    <td>ms-settings:phone-defaultapps</td>
  <td></td>
 </tr>
 <tr>
  <td>Display</td>
    <td>ms-settings:screenrotation</td>
  <td></td>
 </tr>
 <tr>
  <td>Notifications & actions</td>
    <td>ms-settings:notifications</td>
  <td></td>
 </tr>
 <tr>
  <td>Phone</td>
    <td>ms-settings:phone</td>
  <td></td>
 </tr>
 <tr>
  <td>Messaging</td>
    <td>ms-settings:messaging</td>
  <td></td>
 </tr>
 <tr>
  <td>Battery Saver</td>
  <td>ms-settings:batterysaver</td>
  <td>Only available on devices that have a battery, such as a tablet</td>
 </tr>
 <tr>
  <td>Battery use</td>
  <td>ms-settings:batterysaver-usagedetails</td>
  <td>Only available on devices that have a battery, such as a tablet</td>
 </tr>
 <tr>
  <td>Power & sleep</td>
  <td>ms-settings:powersleep</td>
  <td></td>
 </tr>
 <tr>
  <td>About</td>
    <td>ms-settings:about</td>
  <td></td>
 </tr>
 <tr>
  <td>Storage</td>
    <td>ms-settings:storagesense</td>
  <td></td>
 </tr>
 <tr>
  <td>Storage Sense</td>
    <td>ms-settings:storagepolicies</td>
  <td></td>
 </tr>
 <tr>
  <td>Default Save Locations</td>
    <td>ms-settings:savelocations</td>
  <td></td>
 </tr>
 <tr>
  <td>Encryption</td>
    <td>ms-settings:deviceencryption</td>
  <td></td>
 </tr>
 <tr>
  <td>Offline Maps</td>
    <td>ms-settings:maps</td>
  <td></td>
 </tr>
 <tr>
  <td rowspan="5">Time and language</td>
  <td>Date & time</td>
    <td>ms-settings:dateandtime</td>
  <td></td>
 </tr>
 <tr>
  <td>Region & language</td>
    <td>ms-settings:regionlanguage</td>
  <td></td>
 </tr>
 <tr>
     <td>Speech Language</td>
     <td>ms-settings:speech</td>
     <td></td>
 </tr>
 <tr>
     <td>Pinyin keyboard</td>
     <td>ms-settings:regionlanguage-chsime-pinyin</td>
     <td>Available if the Microsoft Pinyin input method editor is installed</td>
 </tr>
 <tr>
     <td>Wubi input mode</td>
     <td>ms-settings:regionlanguage-chsime-wubi</td>
     <td>Available if the Microsoft Wubi input method editor is installed</td>
 </tr>
 <tr>
  <td rowspan="13">Update & security</td>
  <td>Windows Hello setup</td>
    <td>ms-settings:signinoptions-launchfaceenrollment<br>ms-settings:signinoptions-launchfingerprintenrollment</td>
  </tr>
  <tr>
    <td>Backup</td>
      <td>ms-settings:backup</td>
    <td></td>
 </tr>
 <tr>
  <td>Find My Device</td>
    <td>ms-settings:findmydevice</td>
  <td></td>
 </tr>
 <tr>
  <td>Windows Insider Program</td>
  <td>ms-settings:windowsinsider</td>
  <td>Only present if user is enrolled in WIP</td>
 </tr>
 <tr>
  <td>Windows Update</td>
  <td>ms-settings:windowsupdate</td>
  <td></td>
 </tr>
 <tr>
  <td>Windows Update</td>
    <td>ms-settings:windowsupdate-history</td>
  <td></td>
 </tr>
 <tr>
  <td>Windows Update</td>
    <td>ms-settings:windowsupdate-options</td>
  <td></td>
 </tr>
 <tr>
  <td>Windows Update</td>
    <td>ms-settings:windowsupdate-restartoptions</td>
  <td></td>
 </tr>
 <tr>
  <td>Windows Update</td>
    <td>ms-settings:windowsupdate-action</td>
  <td></td>
 </tr>
 <tr>
  <td>Activation</td>
    <td>ms-settings:activation</td>
  <td></td>
 </tr>
 <tr>
  <td>Recovery</td>
    <td>ms-settings:recovery</td>
  <td></td>
 </tr>
 <tr>
  <td>Troubleshoot</td>
    <td>ms-settings:troubleshoot</td>
  <td></td>
 </tr>
 <tr>
  <td>Windows Defender</td>
    <td>ms-settings:windowsdefender</td>
  <td></td>
 </tr>
 <tr>
  <td>For developers</td>
    <td>ms-settings:developers</td>
  <td></td>
 </tr>
 <tr>
  <td rowspan="3">User  Accounts</td>
  <td>Windows Anywhere</td>
  <td>ms-settings:windowsanywhere</td>
  <td>Device must be Windows Anywhere-capable</td>
 </tr>
 <tr>
  <td>Provisioning</td>
  <td>ms-settings:workplace-provisioning</td>
  <td>Only available if enterprise has deployed a provisioning package</td>
 </tr>
 <tr>
   <td>Provisioning</td>
   <td>ms-settings:provisioning</td>
   <td>Only available on mobile and if the enterprise has deployed a provisioning package</td>
 </tr>
</table><br/>  
