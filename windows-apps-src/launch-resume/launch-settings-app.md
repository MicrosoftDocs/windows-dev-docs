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
    <tr>
        <th>Category</th>
        <th>Settings page</th>
        <th>Supported SKUs</th>
        <th>URI</th>
    </tr>
    <tr>
        <td>Home page</td>
        <td>Landing page for Settings</td>
        <td>Both</td>
        <td>ms-settings:</td>
    </tr>
    <tr>
        <td rowspan="13">System</td>
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
        <td>Default Save Locations</td>
        <td>Desktop only</td>
        <td>ms-settings:savelocations</td>
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
        <td rowspan="4">Devices</td>
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
        <td rowspan="3">Network & Wireless</td>
        <td>NFC</td>
        <td>Both</td>
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
        <td rowspan="5">Network & Internet</td>
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
        <td rowspan="5">Personalization</td>
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
        <td>Mobile only </td>
        <td>ms-settings:sounds</td>
    </tr>
    <tr>
        <td>Lock screen</td>
        <td>Both</td>
        <td>ms-settings:lockscreen</td>
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
        <td>High contrast </td>
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
        <td rowspan="15">Privacy</td>
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
        <td>Update & security</td>
        <td>For developers</td>
        <td>Both</td>
        <td>ms-settings:developers</td>
    </tr>
</table><br/>
