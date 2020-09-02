---
title: Launch the Windows Settings app
description: Learn how to launch the Windows Settings app from your app. This topic describes the ms-settings URI scheme. Use this URI scheme to launch the Windows Settings app to specific settings pages.
ms.assetid: C84D4BEE-1FEE-4648-AD7D-8321EAC70290
ms.date: 04/19/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
ms.custom: 19H1
dev_langs:
  - csharp
  - cppwinrt
---

# Launch the Windows Settings app

**Important APIs**

-   [**LaunchUriAsync**](/uwp/api/windows.system.launcher.launchuriasync)
-   [**PreferredApplicationPackageFamilyName**](/uwp/api/windows.system.launcheroptions.preferredapplicationpackagefamilyname)
-   [**DesiredRemainingView**](/uwp/api/windows.system.launcheroptions.desiredremainingview)

Learn how to launch the Windows Settings app. This topic describes the **ms-settings:** URI scheme. Use this URI scheme to launch the Windows Settings app to specific settings pages.

Launching to the Settings app is an important part of writing a privacy-aware app. If your app can't access a sensitive resource, we recommend providing the user a convenient link to the privacy settings for that resource. For more info, see [Guidelines for privacy-aware apps](../security/index.md).

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

Alternatively, your app can call the [**LaunchUriAsync**](/uwp/api/windows.system.launcher.launchuriasync) method to launch the **Settings** app. This example shows how to launch to the privacy settings page for the camera using the `ms-settings:privacy-webcam` URI.

```cs
bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));
```
```cppwinrt
bool result = co_await Windows::System::Launcher::LaunchUriAsync(Windows::Foundation::Uri(L"ms-settings:privacy-webcam"));
```

The code above launches the privacy settings page for the camera:

![camera privacy settings.](images/privacyawarenesssettingsapp.png)

For more info about launching URIs, see [Launch the default app for a URI](launch-default-app.md).

## ms-settings: URI scheme reference

Use the following URIs to open various pages of the Settings app.

> Note that whether a settings page is available varies by Windows SKU. Not all settings page available on Windows 10 for desktop are available on Windows 10 Mobile, and vice-versa. The notes column also captures additional requirements that must be met for a page to be available.

<!-- TODO: 
* ms-settings:controlcenter
* ms-settings:holographic
* ms-settings:keyboard-advanced
* ms-settings:regionlanguage-adddisplaylanguage (crashed)
* ms-settings:regionlanguage-setdisplaylanguage (crashed)
* ms-settings:signinoptions-launchpinenrollment
* ms-settings:storagecleanup
* ms-settings:update-security -->

## Accounts

|Settings page| URI |
|-------------|-----|
| Access work or school | ms-settings:workplace |
| Email & app accounts  | ms-settings:emailandaccounts |
| Family & other people | ms-settings:otherusers |
| Set up a kiosk | ms-settings:assignedaccess |
| Sign-in options | ms-settings:signinoptions<br>ms-settings:signinoptions-dynamiclock |
| Sync your settings | ms-settings:sync |
| Windows Hello setup | ms-settings:signinoptions-launchfaceenrollment<br>ms-settings:signinoptions-launchfingerprintenrollment |
| Your info | ms-settings:yourinfo |

## Apps

|Settings page| URI |
|-------------|-----|
| Apps & Features | ms-settings:appsfeatures |
| App features | ms-settings:appsfeatures-app (Reset, manage add-on & downloadable content, etc. for the app)|
| Apps for websites | ms-settings:appsforwebsites |
| Default apps | ms-settings:defaultapps |
| Manage optional features | ms-settings:optionalfeatures |
| Offline Maps | ms-settings:maps<br/>ms-settings:maps-downloadmaps (Download maps) |
| Startup apps | ms-settings:startupapps |
| Video playback | ms-settings:videoplayback |

## Cortana

|Settings page| URI |
|-------------|-----|
| Cortana across my devices | ms-settings:cortana-notifications |
| More details | ms-settings:cortana-moredetails |
| Permissions & History | ms-settings:cortana-permissions |
| Searching Windows | ms-settings:cortana-windowssearch |
| Talk to Cortana | ms-settings:cortana-language<br/>ms-settings:cortana<br/>ms-settings:cortana-talktocortana |

> [!NOTE] 
> This Settings section on desktop will be called Search when the PC is set to regions where Cortana is not currently available or Cortana has been disabled. Cortana-specific pages (Cortana across my devices, and Talk to Cortana) will not be listed in this case. 

## Devices

|Settings page| URI |
|-------------|-----|
| AutoPlay | ms-settings:autoplay |
| Bluetooth | ms-settings:bluetooth |
| Connected Devices | ms-settings:connecteddevices |
| Default camera | ms-settings:camera (**Deprecated in Windows 10, version 1809 and later**) |
| Mouse & touchpad | ms-settings:mousetouchpad (touchpad settings only available on devices that have a touchpad) |
| Pen & Windows Ink | ms-settings:pen |
| Printers & scanners | ms-settings:printers |
| Touchpad | ms-settings:devices-touchpad (only available if touchpad hardware is present) |
| Typing | ms-settings:typing |
| USB | ms-settings:usb |
| Wheel | ms-settings:wheel (only available if Dial is paired) |
| Your phone | ms-settings:mobile-devices  |

## Ease of Access

|Settings page| URI |
|-------------|-----|
| Audio | ms-settings:easeofaccess-audio |
| Closed captions | ms-settings:easeofaccess-closedcaptioning |
| Color filters | ms-settings:easeofaccess-colorfilter |
| Cursor & pointer size | ms-settings:easeofaccess-cursorandpointersize |
| Display | ms-settings:easeofaccess-display |
| Eye control | ms-settings:easeofaccess-eyecontrol |
| Fonts | ms-settings:fonts |
| High contrast | ms-settings:easeofaccess-highcontrast |
| Keyboard | ms-settings:easeofaccess-keyboard |
| Magnifier | ms-settings:easeofaccess-magnifier |
| Mouse | ms-settings:easeofaccess-mouse |
| Narrator | ms-settings:easeofaccess-narrator |
| Other options | ms-settings:easeofaccess-otheroptions (**Deprecated in Windows 10, version 1809 and later**) |
| Speech | ms-settings:easeofaccess-speechrecognition |

## Extras

|Settings page| URI |
|-------------|-----|
| Extras | ms-settings:extras (only available if "settings apps" are installed, for example, by a 3rd party) |

## Gaming

|Settings page| URI |
|-------------|-----|
| Broadcasting | ms-settings:gaming-broadcasting |
| Game bar | ms-settings:gaming-gamebar |
| Game DVR | ms-settings:gaming-gamedvr |
| Game Mode | ms-settings:gaming-gamemode |
| Playing a game full screen | ms-settings:quietmomentsgame |
| TruePlay | ms-settings:gaming-trueplay (**As of Windows 10, version 1809 (10.0; Build 17763), this feature is removed from Windows**) |
| Xbox Networking | ms-settings:gaming-xboxnetworking |

## Home page

|Settings page| URI |
|-------------|-----|
| Settings home page | ms-settings: |

## Mixed reality

> [!NOTE]
> These settings are only available if the Mixed Reality Portal app is installed.

| Settings page | URI |
|---------------|-----|
| Audio and speech | ms-settings:holographic-audio |
| Environment | ms-settings:privacy-holographic-environment |
| Headset display | ms-settings:holographic-headset |
| Uninstall | ms-settings:holographic-management |

## Network & internet

|Settings page| URI |
|-------------|-----|
| Airplane mode | ms-settings:network-airplanemode<br/>ms-settings:proximity |
| Cellular & SIM | ms-settings:network-cellular |
| Data usage | ms-settings:datausage |
| Dial-up | ms-settings:network-dialup |
| DirectAccess | ms-settings:network-directaccess (only available if DirectAccess is enabled) |
| Ethernet | ms-settings:network-ethernet |
| Manage known networks | ms-settings:network-wifisettings |
| Mobile hotspot | ms-settings:network-mobilehotspot |
| NFC | ms-settings:nfctransactions |
| Proxy | ms-settings:network-proxy |
| Status | ms-settings:network-status<br/>ms-settings:network |
| VPN | ms-settings:network-vpn |
| Wi-Fi | ms-settings:network-wifi (only available if the device has a wifi adapter) |
| Wi-Fi Calling | ms-settings:network-wificalling (only available if Wi-Fi calling is enabled) |

## Personalization

|Settings page| URI |
|-------------|-----|
| Background | ms-settings:personalization-background |
| Choose which folders appear on Start | ms-settings:personalization-start-places |
| Colors | ms-settings:personalization-colors<br/>ms-settings:colors |
| Glance | ms-settings:personalization-glance (**Deprecated in Windows 10, version 1809 and later**) |
| Lock screen | ms-settings:lockscreen |
| Navigation bar | ms-settings:personalization-navbar (**Deprecated in Windows 10, version 1809 and later**) |
| Personalization (category) | ms-settings:personalization |
| Start | ms-settings:personalization-start |
| Taskbar | ms-settings:taskbar |
| Themes | ms-settings:themes |

## Phone

|Settings page| URI |
|-------------|-----|
| Your phone | ms-settings:mobile-devices<br/>ms-settings:mobile-devices-addphone<br/>ms-settings:mobile-devices-addphone-direct (Opens **Your Phone** app) |

## Privacy

|Settings page| URI |
|-------------|-----|
| Accessory apps | ms-settings:privacy-accessoryapps (**Deprecated in Windows 10, version 1809 and later**) |
| Account info | ms-settings:privacy-accountinfo |
| Activity history | ms-settings:privacy-activityhistory |
| Advertising ID | ms-settings:privacy-advertisingid (**Deprecated in Windows 10, version 1809 and later**) |
| App diagnostics | ms-settings:privacy-appdiagnostics |
| Automatic file downloads | ms-settings:privacy-automaticfiledownloads |
| Background Apps | ms-settings:privacy-backgroundapps |
| Calendar | ms-settings:privacy-calendar |
| Call history | ms-settings:privacy-callhistory |
| Camera | ms-settings:privacy-webcam |
| Contacts | ms-settings:privacy-contacts |
| Documents | ms-settings:privacy-documents |
| Email | ms-settings:privacy-email |
| Eye tracker | ms-settings:privacy-eyetracker (requires eyetracker hardware) |
| Feedback & diagnostics | ms-settings:privacy-feedback |
| File system | ms-settings:privacy-broadfilesystemaccess |
| General | ms-settings:privacy or ms-settings:privacy-general |
| Inking & typing |ms-settings:privacy-speechtyping |
| Location | ms-settings:privacy-location |
| Messaging | ms-settings:privacy-messaging |
| Microphone | ms-settings:privacy-microphone |
| Motion | ms-settings:privacy-motion |
| Notifications | ms-settings:privacy-notifications |
| Other devices | ms-settings:privacy-customdevices |
| Phone calls | ms-settings:privacy-phonecalls |
| Pictures | ms-settings:privacy-pictures |
| Radios | ms-settings:privacy-radios |
| Speech | ms-settings:privacy-speech |
| Tasks | ms-settings:privacy-tasks |
| Videos | ms-settings:privacy-videos |
| Voice activation | ms-settings:privacy-voiceactivation |

## Surface Hub

|Settings page| URI |
|-------------|-----|
| Accounts | ms-settings:surfacehub-accounts |
| Session cleanup | ms-settings:surfacehub-sessioncleanup |
| Team Conferencing | ms-settings:surfacehub-calling |
| Team device management | ms-settings:surfacehub-devicemanagenent |
| Welcome screen | ms-settings:surfacehub-welcome |

## System

|Settings page| URI |
|-------------|-----|
| About | ms-settings:about |
| Advanced display settings | ms-settings:display-advanced (only available on devices that support advanced display options) |
| App volume and device preferences | ms-settings:apps-volume (**Added in Windows 10, version 1903**)|
| Battery Saver | ms-settings:batterysaver (only available on devices that have a battery, such as a tablet) |
| Battery Saver settings | ms-settings:batterysaver-settings (only available on devices that have a battery, such as a tablet) |
| Battery use | ms-settings:batterysaver-usagedetails (only available on devices that have a battery, such as a tablet) |
| Clipboard | ms-settings:clipboard |
| Display | ms-settings:display |
| Default Save Locations | ms-settings:savelocations |
| Display | ms-settings:screenrotation |
| Duplicating my display | ms-settings:quietmomentspresentation |
| During these hours | ms-settings:quietmomentsscheduled |
| Encryption | ms-settings:deviceencryption |
| Focus assist | ms-settings:quiethours <br> ms-settings:quietmomentshome |
| Graphics Settings | ms-settings:display-advancedgraphics (only available on devices that support advanced graphics options) |
| Messaging | ms-settings:messaging |
| Multitasking | ms-settings:multitasking |
| Night light settings | ms-settings:nightlight |
| Phone | ms-settings:phone-defaultapps |
| Projecting to this PC | ms-settings:project |
| Shared experiences | ms-settings:crossdevice |
| Tablet mode | ms-settings:tabletmode |
| Taskbar | ms-settings:taskbar |
| Notifications & actions | ms-settings:notifications |
| Remote Desktop | ms-settings:remotedesktop |
| Phone | ms-settings:phone (**Deprecated in Windows 10, version 1809 and later**) |
| Power & sleep | ms-settings:powersleep |
| Sound | ms-settings:sound |
| Storage | ms-settings:storagesense |
| Storage Sense | ms-settings:storagepolicies |

## Time and language

|Settings page| URI |
|-------------|-----|
| Date & time | ms-settings:dateandtime |
| Japan IME settings | ms-settings:regionlanguage-jpnime (available if the Microsoft Japan input method editor is installed) |
| Region | ms-settings:regionformatting |
| Language | ms-settings:keyboard<br/>ms-settings:regionlanguage<br/>ms-settings:regionlanguage-bpmfime<br/>ms-settings:regionlanguage-cangjieime<br/>ms-settings:regionlanguage-chsime-pinyin-domainlexicon<br/>ms-settings:regionlanguage-chsime-pinyin-keyconfig<br/>ms-settings:regionlanguage-chsime-pinyin-udp<br/>ms-settings:regionlanguage-chsime-wubi-udp<br/>ms-settings:regionlanguage-quickime |
| Pinyin IME settings | ms-settings:regionlanguage-chsime-pinyin (available if the Microsoft Pinyin input method editor is installed) |
| Speech | ms-settings:speech |
| Wubi IME settings  | ms-settings:regionlanguage-chsime-wubi (available if the Microsoft Wubi input method editor is installed) |

## Update & security

|Settings page| URI |
|-------------|-----|
| Activation | ms-settings:activation |
| Backup | ms-settings:backup |
| Delivery Optimization | ms-settings:delivery-optimization |
| Find My Device | ms-settings:findmydevice |
| For developers | ms-settings:developers |
| Recovery | ms-settings:recovery |
| Troubleshoot | ms-settings:troubleshoot |
| Windows Security | ms-settings:windowsdefender |
| Windows Insider Program | ms-settings:windowsinsider (only present if user is enrolled in WIP)<br/>ms-settings:windowsinsider-optin |
| Windows Update | ms-settings:windowsupdate<br>ms-settings:windowsupdate-action |
| Windows Update-Advanced options | ms-settings:windowsupdate-options |
| Windows Update-Restart options | ms-settings:windowsupdate-restartoptions |
| Windows Update-View update history | ms-settings:windowsupdate-history |

## User Accounts

|Settings page| URI |
|-------------|-----|
| Provisioning | ms-settings:workplace-provisioning (only available if enterprise has deployed a provisioning package) |
| Provisioning | ms-settings:provisioning (only available on mobile and if the enterprise has deployed a provisioning package) |
| Windows Anywhere | ms-settings:windowsanywhere (device must be Windows Anywhere-capable) |