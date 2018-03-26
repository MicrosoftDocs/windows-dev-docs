---
author: TylerMSFT
title: Launch the Windows Settings app
description: Learn how to launch the Windows Settings app from your app. This topic describes the ms-settings URI scheme. Use this URI scheme to launch the Windows Settings app to specific settings pages.
ms.assetid: C84D4BEE-1FEE-4648-AD7D-8321EAC70290
ms.author: twhitney
ms.date: 03/20/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Launch the Windows Settings app


**Important APIs**

-   [**LaunchUriAsync**](https://msdn.microsoft.com/library/windows/apps/hh701476)
-   [**PreferredApplicationPackageFamilyName**](https://msdn.microsoft.com/library/windows/apps/hh965482)
-   [**DesiredRemainingView**](https://msdn.microsoft.com/library/windows/apps/dn298314)

Learn how to launch the Windows Settings app. This topic describes the **ms-settings:** URI scheme. Use this URI scheme to launch the Windows Settings app to specific settings pages.

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

Alternatively, your app can call the [**LaunchUriAsync**](https://msdn.microsoft.com/library/windows/apps/hh701476) method to launch the **Settings** app. This example shows how to launch to the privacy settings page for the camera using the `ms-settings:privacy-webcam` URI.

```cs
bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));
```

The code above launches the privacy settings page for the camera:

![camera privacy settings.](images/privacyawarenesssettingsapp.png)

For more info about launching URIs, see [Launch the default app for a URI](launch-default-app.md).

## ms-settings: URI scheme reference

Use the following URIs to open various pages of the Settings app.

> Note that whether a settings page is available varies by Windows SKU. Not all settings page available on Windows 10 for desktop are available on Windows 10 Mobile, and vice-versa. The notes column also captures additional requirements that must be met for a page to be available.

## Accounts

|Settings Page| URI |
|-------------|-----|
|Access work or school | ms-settings:workplace |
|Email & app accounts  | ms-settings:emailandaccounts |
|Family & other people | ms-settings:otherusers |
|Sign-in options | ms-settings:signinoptions<br>ms-settings:signinoptions-dynamiclock |
|Sync your settings | ms-settings:sync |
|Your info | ms-settings:yourinfo |

## Apps

|Settings Page| URI |
|-------------|-----|
| Apps & Features | ms-settings:appsfeatures |
| App features | ms-settings:appsfeatures-app (Reset, manage add-on & downloadable content, etc. for the app)|
| Apps for websites | ms-settings:appsforwebsites |
| Default apps | ms-settings:defaultapps |
| Manage optional features | ms-settings:optionalfeatures |
| Startup apps | ms-settings:startupapps |

## Cortana

|Settings Page| URI |
|-------------|-----|
| Cortana Permissions & History | ms-settings:cortana-permissions |
| More details | ms-settings:cortana-moredetails |
| Notifications | ms-settings:cortana-notifications |
| Talk to Cortana | ms-settings:cortana-language |

## Devices

|Settings Page| URI |
|-------------|-----|
| Audio and speech | ms-settings:holographic-audio (only available if the Mixed Reality Portal app is installed--available in the Microsoft Store) |
| AutoPlay | ms-settings:autoplay |
| Bluetooth | ms-settings:bluetooth |
| Connected Devices | ms-settings:connecteddevices |
| Default camera | ms-settings:camera |
| Mouse & touchpad | ms-settings:mousetouchpad (touchpad settings only available on devices that have a touchpad) |
| Pen & Windows Ink | ms-settings:pen |
| Printers & scanners | ms-settings:printers |
| Touchpad | ms-settings:devices-touchpad (only available if touchpad hardware is present) |
| Typing | ms-settings:typing |
| USB | ms-settings:usb |
| Wheel | ms-settings:wheel (only available if Dial is paired) |
| Your phone | ms-settings:mobile-devices  |

## Ease of Access

|Settings Page| URI |
|-------------|-----|
| Audio | ms-settings:easeofaccess-audio |
| Closed captions | ms-settings:easeofaccess-closedcaptioning |
| Display | ms-settings:easeofaccess-display |
| Eye control | ms-settings:easeofaccess-eyecontrol |
| Fonts | ms-settings:fonts |
| High contrast | ms-settings:easeofaccess-highcontrast |
| Holographic headset | ms-settings:holographic-headset (requires holographic hardware) |
| Keyboard | ms-settings:easeofaccess-keyboard |
| Magnifier | ms-settings:easeofaccess-magnifier |
| Mouse | ms-settings:easeofaccess-mouse |
| Narrator | ms-settings:easeofaccess-narrator |
| Other options | ms-settings:easeofaccess-otheroptions |
| Speech | ms-settings:easeofaccess-speechrecognition |

## Extras

|Settings Page| URI |
|-------------|-----|
| Extras | ms-settings:extras (only available if "settings apps" are installed, e.g. by a 3rd party) |

## Gaming

|Settings Page| URI |
|-------------|-----|
| Broadcasting | ms-settings:gaming-broadcasting |
| Game bar | ms-settings:gaming-gamebar |
| Game DVR | ms-settings:gaming-gamedvr |
| Game Mode | ms-settings:gaming-gamemode |
| Playing a game full screen | ms-settings:quietmomentsgame |
| TruePlay | ms-settings:gaming-trueplay |
| Xbox Networking | ms-settings:gaming-xboxnetworking |

## Home page

|Settings Page| URI |
|-------------|-----|
| Settings home page | ms-settings: |


## Network, wireless & internet

|Settings Page| URI |
|-------------|-----|
| Airplane mode | ms-settings:network-airplanemode (use ms-settings:proximity on Windows 8.x) |
| Cellular & SIM | ms-settings:network-cellular |
| Data usage | ms-settings:datausage |
| Dial-up | ms-settings:network-dialup |
| DirectAccess | ms-settings:network-directaccess (only available if DirectAccess is enabled) |
| Ethernet | ms-settings:network-ethernet |
| Manage known networks | ms-settings:network-wifisettings |
| Mobile hotspot | ms-settings:network-mobilehotspot |
| NFC | ms-settings:nfctransactions |
| Proxy | ms-settings:network-proxy |
| Status | ms-settings:network-status |
| VPN | ms-settings:network-vpn |
| Wi-Fi | ms-settings:network-wifi (only available if the device has a wifi adapter) |
| Wi-Fi Calling | ms-settings:network-wificalling (only available if Wi-Fi calling is enabled) |

## Personalization

|Settings Page| URI |
|-------------|-----|
| Background | ms-settings:personalization-background |
| Choose which folders appear on Start | ms-settings:personalization-start-places |
| Colors | ms-settings:personalization-colors |
| Glance | ms-settings:personalization-glance |
| Lock screen | ms-settings:lockscreen |
| Navigation bar | ms-settings:personalization-navbar |
| Personalization (category) | ms-settings:personalization |
| Start | ms-settings:personalization-start |
| Sounds | ms-settings:sounds |
| Task Bar | ms-settings:taskbar |
| Themes | ms-settings:themes |

## Privacy

|Settings Page| URI |
|-------------|-----|
| Accessory apps | ms-settings:privacy-accessoryapps |
| Account info | ms-settings:privacy-accountinfo |
| Activity history | ms-settings:privacy-activityhistory |
| Advertising ID | ms-settings:privacy-advertisingid |
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
| General | ms-settings:privacy-general |
| Location | ms-settings:privacy-location |
| Messaging | ms-settings:privacy-messaging |
| Microphone | ms-settings:privacy-microphone |
| Motion | ms-settings:privacy-motion |
| Notifications | ms-settings:privacy-notifications |
| Other devices | ms-settings:privacy-customdevices |
| Pictures | ms-settings:privacy-pictures |
| Phone calls | ms-settings:privacy-phonecall |
| Radios | ms-settings:privacy-radios |
| Speech, inking & typing |ms-settings:privacy-speechtyping |
| Tasks | ms-settings:privacy-tasks |
| Videos | ms-settings:privacy-videos |

## Surface Hub

|Settings Page| URI |
|-------------|-----|
| Accounts | ms-settings:surfacehub-accounts |
| Session cleanup | ms-settings:surfacehub-sessioncleanup |
| Team Conferencing | ms-settings:surfacehub-calling |
| Team device management | ms-settings:surfacehub-devicemanagenent |
| Welcome screen | ms-settings:surfacehub-welcome |

## System

|Settings Page| URI |
|-------------|-----|
| About | ms-settings:about |
| Advanced display settings | ms-settings:display-advanced (only available on devices that support advanced display options) |
| Battery Saver | ms-settings:batterysaver (only available on devices that have a battery, such as a tablet) |
| Battery Saver settings | ms-settings:batterysaver-settings (only available on devices that have a battery, such as a tablet) |
| Battery use | ms-settings:batterysaver-usagedetails (only available on devices that have a battery, such as a tablet) |
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
| Offline Maps | ms-settings:maps |
| Phone | ms-settings:phone-defaultapps |
| Projecting to this PC | ms-settings:project |
| Shared experiences | ms-settings:crossdevice |
| Tablet mode | ms-settings:tabletmode |
| Taskbar | ms-settings:taskbar |
| Notifications & actions | ms-settings:notifications |
| Remote Desktop | ms-settings:remotedesktop |
| Phone | ms-settings:phone |
| Power & sleep | ms-settings:powersleep |
| Storage | ms-settings:storagesense |
| Storage Sense | ms-settings:storagepolicies |
| Video playback | ms-settings:videoplayback |

## Time and language

|Settings Page| URI |
|-------------|-----|
| Date & time | ms-settings:dateandtime |
| Japan IME settings | ms-settings:regionlanguage-jpnime (available if the Microsoft Japan input method editor is installed) |
| Pinyin IME settings | ms-settings:regionlanguage-chsime-pinyin (available if the Microsoft Pinyin input method editor is installed) |
| Region & language | ms-settings:regionlanguage |
| Speech Language | ms-settings:speech |
| Wubi IME settings  | ms-settings:regionlanguage-chsime-wubi (available if the Microsoft Wubi input method editor is installed) |

## Update & security

|Settings Page| URI |
|-------------|-----|
| Activation | ms-settings:activation |
| Backup | ms-settings:backup |
| Delivery Optimization | ms-settings:delivery-optimization |
| Find My Device | ms-settings:findmydevice |
| Recovery | ms-settings:recovery |
| Troubleshoot | ms-settings:troubleshoot |
| Windows Defender | ms-settings:windowsdefender |
| Windows Hello setup | ms-settings:signinoptions-launchfaceenrollment<br>ms-settings:signinoptions-launchfingerprintenrollment |
| Windows Insider Program | ms-settings:windowsinsider (only present if user is enrolled in WIP) |
| Windows Update | ms-settings:windowsupdate<br>ms-settings:windowsupdate-action |
| Windows Update-Advanced options | ms-settings:windowsupdate-options |
| Windows Update-Restart options | ms-settings:windowsupdate-restartoptions |
| Windows Update-View update history | ms-settings:windowsupdate-history |

## Developers

|Settings Page| URI |
|-------------|-----|
| For developers | ms-settings:developers |

## User  Accounts

|Settings Page| URI |
|-------------|-----|
| Provisioning | ms-settings:workplace-provisioning (only available if enterprise has deployed a provisioning package) |
| Provisioning | ms-settings:provisioning (only available on mobile and if the enterprise has deployed a provisioning package) |
| Windows Anywhere | ms-settings:windowsanywhere (device must be Windows Anywhere-capable) |
