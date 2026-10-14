---
title: Launch Windows Settings
description: Learn how to launch Windows Settings from your Windows apps using the ms-settings URI scheme.
ms.date: 09/26/2026
ms.topic: concept-article
keywords: windows 10, windows, settings, windows 11, uwp, uri
ms.localizationpriority: medium
no-loc: ["ms-settings:"]
dev_langs:
  - csharp
  - cppwinrt
# Customer intent: As a Windows developer, I want to learn how to launch Windows Settings from my app using the ms-settings URI scheme.
---

# Launch Windows Settings

Learn how to launch Windows Settings. This topic describes the `ms-settings:` URI scheme. Use this URI scheme to launch Windows Settings to specific settings pages.

Launching to Settings is an important part of writing a privacy-aware app. If your app can't access a sensitive resource, we recommend providing the user a convenient link to the privacy settings for that resource. For more information, see [Security and identity](../security/index.md).

## Important APIs

The following Windows Runtime (WinRT) APIs are used in this topic:

- [LaunchUriAsync](/uwp/api/windows.system.launcher.launchuriasync)
- [PreferredApplicationPackageFamilyName](/uwp/api/windows.system.launcheroptions.preferredapplicationpackagefamilyname)
- [DesiredRemainingView](/uwp/api/windows.system.launcheroptions.desiredremainingview)

Note that the WinRT APIs used in this topic can be used in both WinUI and other desktop apps. To read more about enabling your desktop app to work with WinRT APIs, see [Call Windows Runtime APIs in desktop apps](/windows/apps/desktop/modernize/desktop-to-uwp-enhance).

## How to launch Settings

To launch **Settings**, use the `ms-settings:` URI scheme as shown in the following examples.

### XAML hyperlink control

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

### Calling LaunchUriAsync

Alternatively, your app can call the [LaunchUriAsync](/uwp/api/windows.system.launcher.launchuriasync) method to launch **Settings**. This example shows how to launch to the privacy settings page for the camera using the `ms-settings:privacy-webcam` URI.

```cs
bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));
```

```cppwinrt
bool result = co_await Windows::System::Launcher::LaunchUriAsync(Windows::Foundation::Uri(L"ms-settings:privacy-webcam"));
```

The code above launches the privacy settings page for the camera:

:::image type="content" source="images/privacyawarenesssettingsapp.png" alt-text="A screenshot of the Windows camera privacy settings.":::

For more info about launching URIs, see [Launch the default app for a URI](launch-default-app.md).

## ms-settings: URI scheme reference

The following sections describe different categories of ms-settings URIs used to open various pages of Settings:

- [Accounts](#accounts)
- [Apps](#apps)
- [Control Center](#control-center)
- [Devices](#devices)
- [Ease of access](#ease-of-access)
- [Extras](#extras)
- [Family Group](#family-group)
- [Gaming](#gaming)
- [Network and internet](#network-and-internet)
- [Personalization](#personalization)
- [Phone](#phone)
- [Privacy](#privacy)
- [Search](#search)
- [Sound](#sound)
- [System](#system)
- [Time and language](#time-and-language)
- [Update and security](#update-and-security)
> The availability of some settings pages varies by Windows version and SKU. For some settings, the URI column also captures some usage information and any additional requirements that must be met for a page to be available.

### Accounts

|Settings page| URI |
|-------------|-----|
| Access work or school | :::no-loc text="ms-settings:workplace"::: |
| Email & app accounts  | :::no-loc text="ms-settings:emailandaccounts"::: |
| Family & other people | :::no-loc text="ms-settings:otherusers"::: |
| Provisioning | :::no-loc text="ms-settings:provisioning"::: (only available on mobile and if the enterprise has deployed a provisioning package)<br>:::no-loc text="ms-settings:workplace-provisioning"::: (only available if enterprise has deployed a provisioning package) |
| Repair token | :::no-loc text="ms-settings:workplace-repairtoken"::: |
| Set up a kiosk | :::no-loc text="ms-settings:assignedaccess"::: |
| Sign-in options | :::no-loc text="ms-settings:signinoptions":::<br>:::no-loc text="ms-settings:signinoptions-dynamiclock"::: |
| Sync your settings | :::no-loc text="ms-settings:sync":::<br>:::no-loc text="ms-settings:backup"::: (**Backup page deprecated in Windows 11**) |
| Windows Anywhere | :::no-loc text="ms-settings:windowsanywhere"::: (device must be Windows Anywhere-capable) |
| Windows Hello setup | :::no-loc text="ms-settings:signinoptions-launchfaceenrollment":::<br>:::no-loc text="ms-settings:signinoptions-launchfingerprintenrollment"::: |
| Your info | :::no-loc text="ms-settings:yourinfo"::: |

### Apps

|Settings page| URI |
|-------------|-----|
| Apps & Features | :::no-loc text="ms-settings:appsfeatures"::: |
| App features | :::no-loc text="ms-settings:appsfeatures-app"::: (Reset, manage add-on & downloadable content, etc. for the app) <br><br> To access this page with a URI, use the :::no-loc text="ms-settings:appsfeatures-app"::: URI and pass an optional parameter of the *package family name* of the app.<br><br>Example: :::no-loc text="ms-settings:appsfeatures-app?<PFN>"::: |
| Apps for websites | :::no-loc text="ms-settings:appsforwebsites"::: |
| Default apps | :::no-loc text="ms-settings:defaultapps"::: (**Behavior introduced in Windows 11, version 21H2 (with 2023-04 Cumulative Update) or 22H2 (with 2023-04 Cumulative Update), or later.**)<br/>Append the query string parameter in the following formats using the Uri-escaped name of an app to directly launch the default settings page for that app:<br/><br/>- registeredAppMachine=\<Uri-escaped per machine installed name of app\><br/>- registeredAppUser=\<Uri-escaped per user installed name of app\><br/>- registeredAUMID=\<Uri-escaped Application User Model ID\><br/><br/>For more information, see [Launch the Default Apps settings page](launch-default-apps-settings.md). |
| Default browser settings | :::no-loc text="ms-settings:defaultbrowsersettings"::: (**Deprecated in Windows 11**) |
| Manage optional features | :::no-loc text="ms-settings:optionalfeatures"::: |
| Offline Maps | :::no-loc text="ms-settings:maps":::<br/>:::no-loc text="ms-settings:maps-downloadmaps"::: (Download maps) |
| Startup apps | :::no-loc text="ms-settings:startupapps"::: |
| Video playback | :::no-loc text="ms-settings:videoplayback"::: |

### Control Center

|Settings page| URI |
|-------------|-----|
| Control center | :::no-loc text="ms-settings:controlcenter"::: |

### Devices

|Settings page| URI |
|-------------|-----|
| AutoPlay | :::no-loc text="ms-settings:autoplay"::: |
| Bluetooth | :::no-loc text="ms-settings:bluetooth"::: |
| Connected Devices | :::no-loc text="ms-settings:connecteddevices"::: |
| Default camera | :::no-loc text="ms-settings:camera"::: (**Behavior deprecated in Windows 10, version 1809 and later**) |
| Camera settings | :::no-loc text="ms-settings:camera"::: (**Behavior introduced in Windows 11, build 22000 and later**) Append the query string parameter `cameraId` set to the Uri-escaped symbolic link name of a camera device to directly launch the settings for that camera. For more information, see [Launch the camera settings page](../camera/launch-camera-settings.md).|
| Mouse & touchpad | :::no-loc text="ms-settings:mousetouchpad"::: (touchpad settings only available on devices that have a touchpad) |
| Pen & Windows Ink | :::no-loc text="ms-settings:pen"::: |
| Pen shortcut button | :::no-loc text="ms-settings:pen-button"::: |
| Printers & scanners | :::no-loc text="ms-settings:printers"::: |
| Touch | :::no-loc text="ms-settings:devices-touch"::: |
| Touchpad | :::no-loc text="ms-settings:devices-touchpad"::: (only available if touchpad hardware is present) |
| Text Suggestions | :::no-loc text="ms-settings:devicestyping-hwkbtextsuggestions"::: |
| Typing | :::no-loc text="ms-settings:typing"::: |
| USB | :::no-loc text="ms-settings:usb"::: |
| Wheel | :::no-loc text="ms-settings:wheel"::: (only available if a **Surface Dial** device is paired) |
| Your phone | :::no-loc text="ms-settings:mobile-devices":::  |

### Ease of access

|Settings page| URI |
|-------------|-----|
| Audio | :::no-loc text="ms-settings:easeofaccess-audio"::: |
| Closed captions | :::no-loc text="ms-settings:easeofaccess-closedcaptioning"::: |
| Color filters | :::no-loc text="ms-settings:easeofaccess-colorfilter"::: </br> :::no-loc text="ms-settings:easeofaccess-colorfilter-adaptivecolorlink"::: </br> :::no-loc text="ms-settings:easeofaccess-colorfilter-bluelightlink"::: |
| Display | :::no-loc text="ms-settings:easeofaccess-display"::: |
| Eye control | :::no-loc text="ms-settings:easeofaccess-eyecontrol"::: |
| Hearing devices | :::no-loc text="ms-settings:easeofaccess-hearingaids"::: (**Added in Windows 11, Version 24H2**) |
| High contrast | :::no-loc text="ms-settings:easeofaccess-highcontrast"::: |
| Keyboard | :::no-loc text="ms-settings:easeofaccess-keyboard"::: |
| Magnifier | :::no-loc text="ms-settings:easeofaccess-magnifier"::: |
| Mouse | :::no-loc text="ms-settings:easeofaccess-mouse"::: |
| Mouse pointer & touch | :::no-loc text="ms-settings:easeofaccess-mousepointer":::|
| Narrator | :::no-loc text="ms-settings:easeofaccess-narrator"::: </br> :::no-loc text="ms-settings:easeofaccess-narrator-isautostartenabled"::: |
| Speech | :::no-loc text="ms-settings:easeofaccess-speechrecognition"::: |
| Text cursor | :::no-loc text="ms-settings:easeofaccess-cursor":::|
| Visual Effects | :::no-loc text="ms-settings:easeofaccess-visualeffects"::: |

### Extras

|Settings page| URI |
|-------------|-----|
| Extras | :::no-loc text="ms-settings:extras"::: (only available if apps with these settings have been installed, for example, by a 3rd party) |

### Family Group

|Settings page| URI |
|-------------|-----|
| Family Group | :::no-loc text="ms-settings:family-group"::: |

### Gaming

|Settings page| URI |
|-------------|-----|
| Game bar | :::no-loc text="ms-settings:gaming-gamebar"::: |
| Game DVR | :::no-loc text="ms-settings:gaming-gamedvr"::: |
| Game Mode | :::no-loc text="ms-settings:gaming-gamemode"::: |
| Xbox Mode | :::no-loc text="ms-settings:gaming-fullscreen"::: |
| Playing a game full screen | :::no-loc text="ms-settings:quietmomentsgame"::: |
| TruePlay | :::no-loc text="ms-settings:gaming-trueplay"::: (**As of Windows 10, version 1809 (10.0; Build 17763), this feature is removed from Windows**) |

| Settings page | URI |
|---------------|-----|
| Audio and speech | :::no-loc text="ms-settings:holographic-audio"::: |
| Environment | :::no-loc text="ms-settings:privacy-holographic-environment"::: |
| Headset display | :::no-loc text="ms-settings:holographic-headset"::: |
| Uninstall | :::no-loc text="ms-settings:holographic-management"::: |
| Startup and desktop | :::no-loc text="ms-settings:holographic-startupandesktop"::: |

### Network and internet

|Settings page| URI |
|-------------|-----|
| Network & internet | :::no-loc text="ms-settings:network-status"::: |
| Advanced settings | :::no-loc text="ms-settings:network-advancedsettings"::: |
| Airplane mode | :::no-loc text="ms-settings:network-airplanemode":::<br/>:::no-loc text="ms-settings:proximity"::: |
| Cellular & SIM | :::no-loc text="ms-settings:network-cellular"::: |
| Dial-up | :::no-loc text="ms-settings:network-dialup"::: |
| DirectAccess | :::no-loc text="ms-settings:network-directaccess"::: (only available if DirectAccess is enabled) |
| Ethernet | :::no-loc text="ms-settings:network-ethernet"::: |
| Manage known networks | :::no-loc text="ms-settings:network-wifisettings"::: |
| Mobile hotspot | :::no-loc text="ms-settings:network-mobilehotspot"::: |
| Proxy | :::no-loc text="ms-settings:network-proxy"::: |
| VPN | :::no-loc text="ms-settings:network-vpn"::: |
| Wi-Fi | :::no-loc text="ms-settings:network-wifi"::: (only available if the device has a wifi adapter) |
| Wi-Fi provisioning | :::no-loc text="ms-settings:wifi-provisioning"::: |

### Personalization

|Settings page| URI |
|-------------|-----|
| Background | :::no-loc text="ms-settings:personalization-background"::: |
| Choose which folders appear on Start | :::no-loc text="ms-settings:personalization-start-places"::: |
| Colors | :::no-loc text="ms-settings:personalization-colors":::<br/>:::no-loc text="ms-settings:colors"::: |
| Customize Copilot key on keyboard | :::no-loc text="ms-settings:personalization-textinput-copilot-hardwarekey"::: |
| Dynamic Lighting | :::no-loc text="ms-settings:personalization-lighting"::: |
| Fonts | :::no-loc text="ms-settings:fonts"::: |
| Glance | :::no-loc text="ms-settings:personalization-glance"::: (**Deprecated in Windows 10, version 1809 and later**) |
| Lock screen | :::no-loc text="ms-settings:lockscreen"::: |
| Navigation bar | :::no-loc text="ms-settings:personalization-navbar"::: (**Deprecated in Windows 10, version 1809 and later**) |
| Personalization (category) | :::no-loc text="ms-settings:personalization"::: |
| Start | :::no-loc text="ms-settings:personalization-start"::: |
| Taskbar | :::no-loc text="ms-settings:taskbar"::: |
| Text input | :::no-loc text="ms-settings:personalization-textinput"::: |
| Touch Keyboard| :::no-loc text="ms-settings:personalization-touchkeyboard"::: |
| Themes | :::no-loc text="ms-settings:themes"::: |

### Phone

|Settings page| URI |
|-------------|-----|
| Phone Link | :::no-loc text="ms-settings:mobile-devices":::<br/>:::no-loc text="ms-settings:mobile-devices-addphone":::<br/>:::no-loc text="ms-settings:mobile-devices-addphone-direct"::: (Opens **Phone Link** app) |
| Device Usage | :::no-loc text="ms-settings:deviceusage"::: |

### Privacy

|Settings page| URI |
|-------------|-----|
| Accessory apps | :::no-loc text="ms-settings:privacy-accessoryapps"::: (**Deprecated in Windows 10, version 1809 and later**) |
| Account info | :::no-loc text="ms-settings:privacy-accountinfo"::: |
| Activity history | :::no-loc text="ms-settings:privacy-activityhistory"::: |
| Advertising ID | :::no-loc text="ms-settings:privacy-advertisingid"::: (**Deprecated in Windows 10, version 1809 and later**) |
| App diagnostics | :::no-loc text="ms-settings:privacy-appdiagnostics"::: |
| Automatic file downloads | :::no-loc text="ms-settings:privacy-automaticfiledownloads"::: |
| Background Apps | :::no-loc text="ms-settings:privacy-backgroundapps"::: (**Deprecated in Windows 11, 21H2 and later**) <br><br> **Note:** In Windows 11, the background app permissions are accessed individually. To view the permissions, go to **Apps->Installed apps** and then select **"..."** on a modern app and choose **Advanced options**. The advanced page is present for modern apps, and the **Background apps permissions** section will be present unless a group policy has been set or the user’s global toggle value (the deprecated setting from Windows 10) is set. To access this page with a URI, use the :::no-loc text="ms-settings:appsfeatures-app"::: URI and pass an optional parameter of the *package family name* of the app. |
| Background Spatial Perception | :::no-loc text="ms-settings:privacy-backgroundspatialperception"::: |
| Calendar | :::no-loc text="ms-settings:privacy-calendar"::: |
| Call history | :::no-loc text="ms-settings:privacy-callhistory"::: |
| Camera | :::no-loc text="ms-settings:privacy-webcam"::: |
| Contacts | :::no-loc text="ms-settings:privacy-contacts"::: |
| Documents | :::no-loc text="ms-settings:privacy-documents"::: |
| Downloads folder | :::no-loc text="ms-settings:privacy-downloadsfolder"::: |
| Email | :::no-loc text="ms-settings:privacy-email"::: |
| Eye tracker | :::no-loc text="ms-settings:privacy-eyetracker"::: (requires eyetracker hardware) |
| Feedback & diagnostics | :::no-loc text="ms-settings:privacy-feedback"::: |
| File system | :::no-loc text="ms-settings:privacy-broadfilesystemaccess"::: |
| General | :::no-loc text="ms-settings:privacy"::: or :::no-loc text="ms-settings:privacy-general"::: |
| Graphics | :::no-loc text="ms-settings:privacy-graphicscaptureprogrammatic"::: </br> :::no-loc text="ms-settings:privacy-graphicscapturewithoutborder":::|
| Inking & typing |:::no-loc text="ms-settings:privacy-speechtyping"::: |
| Location | :::no-loc text="ms-settings:privacy-location"::: |
| Messaging | :::no-loc text="ms-settings:privacy-messaging"::: |
| Microphone | :::no-loc text="ms-settings:privacy-microphone"::: |
| Motion | :::no-loc text="ms-settings:privacy-motion"::: |
| Music Library | :::no-loc text="ms-settings:privacy-musiclibrary"::: |
| Notifications | :::no-loc text="ms-settings:privacy-notifications"::: |
| Other devices | :::no-loc text="ms-settings:privacy-customdevices"::: |
| Phone calls | :::no-loc text="ms-settings:privacy-phonecalls"::: |
| Pictures | :::no-loc text="ms-settings:privacy-pictures"::: |
| Radios | :::no-loc text="ms-settings:privacy-radios"::: |
| Speech | :::no-loc text="ms-settings:privacy-speech"::: |
| Tasks | :::no-loc text="ms-settings:privacy-tasks"::: |
| Videos | :::no-loc text="ms-settings:privacy-videos"::: |
| Voice activation | :::no-loc text="ms-settings:privacy-voiceactivation"::: |

### Search

|Settings page| URI |
|-------------|-----|
| Search | :::no-loc text="ms-settings:search"::: |
| Search more details | :::no-loc text="ms-settings:search-moredetails"::: |
| Search Permissions | :::no-loc text="ms-settings:search-permissions"::: |

### Sound

|Settings page| URI |
|-------------|-----|
| Volume mixer | :::no-loc text="ms-settings:apps-volume"::: |
| Sound | :::no-loc text="ms-settings:sound"::: |
| Sound devices | :::no-loc text="ms-settings:sound-devices"::: |
| Default microphone | :::no-loc text="ms-settings:sound-defaultinputproperties"::: |
| Default audio output| :::no-loc text="ms-settings:sound-defaultoutputproperties"::: |
| Audio device properties<br/>(specific device) | :::no-loc text="ms-settings:sound-properties?endpointId={0.0.0.00000000}.{aaaaaaaa-0000-1111-2222-bbbbbbbbbbbb}":::<br/><br/>**Note:** User of URI must know the `endpointId` string to use. |
| Audio device properties<br/>(specific device) | :::no-loc text="ms-settings:sound-properties?interfaceId=\\\\?\\SWD#MMDEVAPI#{3.0.0.00000003}.{bbbbbbbb-1111-2222-3333-cccccccccccc}#{cccccccc-2222-3333-4444-dddddddddddd}":::<br/><br/>**Note:** User of URI must know the `interfaceId` string to use and the string must be escaped correctly before sending. |

### System

|Settings page| URI |
|-------------|-----|
| About | :::no-loc text="ms-settings:about"::: |
| Advanced display settings | :::no-loc text="ms-settings:display-advanced":::<br/><br/>**Note:** On Windows 11, this URI opens the **System > Display** settings page; the Advanced display view is a sub-section of Display and doesn't have a dedicated URI. |
| Battery Saver | :::no-loc text="ms-settings:batterysaver"::: (only available on devices that have a battery, such as a tablet) |
| Battery Saver settings | :::no-loc text="ms-settings:batterysaver-settings"::: (only available on devices that have a battery, such as a tablet) |
| Battery use | :::no-loc text="ms-settings:batterysaver-usagedetails"::: (only available on devices that have a battery, such as a tablet) |
| Clipboard | :::no-loc text="ms-settings:clipboard"::: |
| Default Save Locations | :::no-loc text="ms-settings:savelocations"::: |
| Display | :::no-loc text="ms-settings:display":::<br>:::no-loc text="ms-settings:screenrotation"::: |
| Duplicating my display | :::no-loc text="ms-settings:quietmomentspresentation"::: |
| During these hours | :::no-loc text="ms-settings:quietmomentsscheduled"::: |
| Encryption | :::no-loc text="ms-settings:deviceencryption"::: |
| Energy recommendations | :::no-loc text="ms-settings:energyrecommendations"::: **(Available in February Moment update for Windows 11, Version 22H2, Build 22624 or later)** |
| Focus assist | :::no-loc text="ms-settings:quiethours"::: |
| Graphics Settings | :::no-loc text="ms-settings:display-advancedgraphics"::: (only available on devices that support advanced graphics options) |
| Graphics Default Settings | :::no-loc text="ms-settings:display-advancedgraphics-default"::: |
| Multitasking | :::no-loc text="ms-settings:multitasking"::: </br>:::no-loc text="ms-settings:multitasking-sgupdate"::: |
| Night light settings | :::no-loc text="ms-settings:nightlight"::: |
| Projecting to this PC | :::no-loc text="ms-settings:project"::: |
| Shared experiences | :::no-loc text="ms-settings:crossdevice"::: |
| Tablet mode | :::no-loc text="ms-settings:tabletmode"::: (**Removed in Windows 11**) |
| Taskbar | :::no-loc text="ms-settings:taskbar"::: |
| Notifications & actions | :::no-loc text="ms-settings:notifications"::: |
| Remote Desktop | :::no-loc text="ms-settings:remotedesktop"::: |
| Phone | :::no-loc text="ms-settings:phone"::: (**Deprecated in Windows 10, version 1809 and later**) |
| Power & sleep | :::no-loc text="ms-settings:powersleep"::: |
| Presence sensing | :::no-loc text="ms-settings:presence"::: (**Added in May Moment update for Windows 11, Version 22H2, Build 22624**) |
| Storage | :::no-loc text="ms-settings:storagesense"::: |
| Storage Sense | :::no-loc text="ms-settings:storagepolicies"::: |
| Storage recommendations | :::no-loc text="ms-settings:storagerecommendations"::: |
| Disks & volumes | :::no-loc text="ms-settings:disksandvolumes"::: |

### Time and language

|Settings page| URI |
|-------------|-----|
| Date & time | :::no-loc text="ms-settings:dateandtime"::: |
| Japan IME settings | :::no-loc text="ms-settings:regionlanguage-jpnime"::: (available if the Microsoft Japan input method editor is installed) |
| Region | :::no-loc text="ms-settings:regionformatting"::: |
| Language | :::no-loc text="ms-settings:keyboard":::<br/>:::no-loc text="ms-settings:keyboard-advanced":::</br>:::no-loc text="ms-settings:regionlanguage":::<br/>:::no-loc text="ms-settings:regionlanguage-bpmfime":::<br/>:::no-loc text="ms-settings:regionlanguage-cangjieime":::<br/>:::no-loc text="ms-settings:regionlanguage-chsime-wubi-udp":::<br/>:::no-loc text="ms-settings:regionlanguage-quickime"::: <br/> :::no-loc text="ms-settings:regionlanguage-korime"::: |
| Pinyin IME settings | :::no-loc text="ms-settings:regionlanguage-chsime-pinyin"::: (available if the Microsoft Pinyin input method editor is installed)</br>:::no-loc text="ms-settings:regionlanguage-chsime-pinyin-domainlexicon":::<br/>:::no-loc text="ms-settings:regionlanguage-chsime-pinyin-keyconfig":::<br/>:::no-loc text="ms-settings:regionlanguage-chsime-pinyin-udp"::: |
| Speech | :::no-loc text="ms-settings:speech"::: |
| Wubi IME settings  | :::no-loc text="ms-settings:regionlanguage-chsime-wubi"::: (available if the Microsoft Wubi input method editor is installed) |

### Update and security

|Settings page| URI |
|-------------|-----|
| Activation | :::no-loc text="ms-settings:activation"::: |
| Backup | :::no-loc text="ms-settings:backup"::: (page removed in Windows 11; opens Sync) |
| Delivery Optimization | :::no-loc text="ms-settings:delivery-optimization"::: </br> :::no-loc text="ms-settings:delivery-optimization-activity"::: </br> :::no-loc text="ms-settings:delivery-optimization-advanced":::|
| Find My Device | :::no-loc text="ms-settings:findmydevice"::: |
| For developers | :::no-loc text="ms-settings:developers"::: |
| Recovery | :::no-loc text="ms-settings:recovery"::: |
| Launch Security Key Enrollment | :::no-loc text="ms-settings:signinoptions-launchsecuritykeyenrollment"::: |
| Troubleshoot | :::no-loc text="ms-settings:troubleshoot"::: |
| Windows Security | :::no-loc text="ms-settings:windowsdefender"::: |
| Windows Insider Program | :::no-loc text="ms-settings:windowsinsider"::: (only present if user is enrolled in WIP)<br/>:::no-loc text="ms-settings:windowsinsider-optin"::: |
| Windows Update | :::no-loc text="ms-settings:windowsupdate":::<br>:::no-loc text="ms-settings:windowsupdate-action"::: |
| Windows Update-Active hours| :::no-loc text="ms-settings:windowsupdate-activehours"::: |
| Windows Update-Advanced options | :::no-loc text="ms-settings:windowsupdate-options"::: |
| Windows Update-Optional updates| :::no-loc text="ms-settings:windowsupdate-optionalupdates"::: |
| Windows Update-Restart options | :::no-loc text="ms-settings:windowsupdate-restartoptions"::: |
| Windows Update-Seeker on demand| :::no-loc text="ms-settings:windowsupdate-seekerondemand"::: |
| Windows Update-View update history | :::no-loc text="ms-settings:windowsupdate-history"::: |

## Related content

[Launch the default app for a URI](launch-default-app.md)

[Launching, resuming, and background tasks](index.md)
