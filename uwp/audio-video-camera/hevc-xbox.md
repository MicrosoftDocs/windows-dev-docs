---
title: 4K video playback for UWP apps on Xbox
description: This topic describes how to enable and implement the playback of 4K video content in your UWP app on Xbox.
ms.date: 03/14/2025
ms.topic: article
keywords: xbox, uwp
ms.localizationpriority: medium
---

# 4K video playback for UWP apps on Xbox

This topic describes how to enable and implement the playback of 4K video content in your UWP app on Xbox. The info applies to any supported 4K video playback format. But for the current generation of consoles, the preferred video codec for 4K playback is High Efficiency Video Coding (HEVC).

With HEVC, you can deliver high-quality video with improved compression efficiency and reduced bandwidth usage while supporting superior visual fidelity. By leveraging HEVC, media apps can stream 4K content with lower bitrate requirements. This will ensure smoother playback and a better user experience&mdash;even in bandwidth-constrained environments. In this topic we cover the necessary steps to enable the 4K (including HEVC) playback flag, along with best practices for refining playback performance on Xbox.

## Behavior differences when enabled

Enabling 4K playback in your application changes the way your application is treated by the Xbox operating system. In addition to allowing playback of 4K video, your application will be allocated 3.25GB of memory. This is 2GB more than apps without 4K playback enabled.

Additionally, your application will no longer be able to run simultaneously with games on the Xbox console&mdash;when the user launches a game, your app will be suspended and closed. Similarly, when your app is launched, the user will have to wait for any game that they were playing to fully close. A consequence of this is that apps will need to choose between being able to play background music and being able to play 4K video content.

## Enabling 4K playback in your appxmanifest

4K and HDR10 video playback is supported on the Xbox One S onwards (the original Xbox One is restricted to 1080p). All these capabilities are enabled using the special `hevcPlayback` capability in the app manifest. Again, the flag enables 4K playback video in any supported format (but HEVC is recommended).

### Code example

```xml
<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:mp="http://schemas.microsoft.com/appx/2014/phone/manifest"
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
  IgnorableNamespaces="uap mp rescap">

...

  <Capabilities>
    <Capability Name="internetClient" />
    <rescap:Capability Name="hevcPlayback" />
  </Capabilities>
</Package>
```

## Capability checks

Rather than going by console type, the recommended UWP API to determine HEVC/4K/HDCP support is [ProtectionCapabilities.IsTypeSupported](/uwp/api/windows.media.protection.protectioncapabilities.istypesupported).

Here are some short examples.

1. Hardware HEVC decode support

```csharp
IsTypeSupported(""video/mp4;codecs="hvc1,mp4a";features="decode-res-x=3840,decode-res-y=2160,decode-bitrate=20000,decode-fps=30,decode-bpc=10"", "com.microsoft.playready.hardware")
```

2. 4K HEVC decode/display support

```csharp
IsTypeSupported(""video/mp4;codecs="hvc1,mp4a";features="decode-res-x=3840,decode-res-y=2160,decode-bitrate=20000,decode-fps=30,decode-bpc=10,display-res-x=3840,display-res-y=2160,display-bpc=8"", "com.microsoft.playready.hardware")
```

3. 4K HEVC decode and display/HDR support

```csharp
IsTypeSupported(""video/mp4;codecs="hvc1,mp4a";features="decode-res-x=3840,decode-res-y=2160,decode-bitrate=20000,decode-fps=30,decode-bpc=10,display-res-x=3840,display-res-y=2160,display-bpc=10, hdr=1"", "com.microsoft.playready.hardware")
```

4. HDCP 2.2 support (we recommended doing this after 4K checks because it's slow)

```csharp
IsTypeSupported (""video/mp4;codecs="hvc1,mp4a";features="hdcp=2"", "com.microsoft.playready.hardware")
```

Those queries will return `NotSupported` on the original Xbox One, which means that there's no hardware HEVC decode support (it supports software decode only). On all other consoles&mdash;Xbox One S and later&mdash;the first query will always return `Probably`, and remaining queries will return `Probably` depending on the display that's connected.

## Tips for 4K applications

### Memory usage information

There's also an API to retrieve more accurate memory usage information that works only on Xbox for apps that have HEVC enabled. The following code example is in C++. To use it, add to your project a new header file named `IApplicationStatics2.h`, and paste the following code listing into it.

```cpp
#pragma once

#include "Windows.Foundation.h"
#include "Inspectable.h"
#include <cstdint>

typedef struct ExtendedMemoryInfo
{
    uint64_t appMemoryUsage;
    uint64_t appMemoryLimit;
    uint64_t extendedMemoryUsage;
    uint64_t extendedMemoryLimit;
} ExtendedMemoryInfo;

interface __declspec(uuid("dd36a017-b640-45f7-a023-1615cf098923"))
    IApplicationResourcesStatics2 : ::IInspectable
{
    virtual HRESULT GetExtendedMemoryInfo(ExtendedMemoryInfo* memoryInfo) = 0;
};
```

Next, add to your main XAML page a **TextBlock** named `MemoryInfo`, which you can use to view the output of the memory information.

```xaml
<Canvas Width="500"
        HorizontalAlignment="Right"
        VerticalAlignment="Top"
        ZIndex="1">
    <TextBlock x:Name="MemoryInfo"
               Foreground="#88FFFFFF"
               FontSize="30" />
</Canvas>
```


Then, in the `.cpp` file for your main XAML page, add the following function (also declare it in your page's `.h` file).

```cpp
#include "IApplicationResourcesStatics2.h"
 
// ...
 
fire_and_forget MainPage::PeriodicMemoryQuery()
{
    co_await resume_background();
 
    auto factoryInspectable =
        winrt::get_activation_factory<IInspectable>(L"Windows.Xbox.ApplicationModel.ApplicationResources").as<::IInspectable>();
    winrt::com_ptr<IApplicationResourcesStatics2> appResources;
    factoryInspectable->QueryInterface(IID_PPV_ARGS(&appResources));
    ExtendedMemoryInfo memInfo;
 
    while (true)
    {
        // Check memory every 3s, and post to the UI thread to display.
        Sleep(3000);
 
        appResources->GetExtendedMemoryInfo(&memInfo);
        Dispatcher().RunAsync(Windows::UI::Core::CoreDispatcherPriority::Low, [memInfo, this]() {
            std::wstring memPayload = L"AppUsage: ";
            memPayload += winrt::to_hstring(static_cast<uint64_t>(memInfo.appMemoryUsage / 1024.0f / 1024.0f)).c_str();
            memPayload += L"MB\nExtendedUsage: ";
            memPayload += winrt::to_hstring(static_cast<uint64_t>(memInfo.extendedMemoryUsage / 1024.0f / 1024.0f)).c_str();
            memPayload += L"MB\nTotal: ";
            memPayload += winrt::to_hstring(static_cast<uint64_t>((memInfo.appMemoryUsage + memInfo.extendedMemoryUsage) / 1024.0f / 1024.0f)).c_str();
            memPayload += L"MB";
            MemoryInfo().Text(memPayload.c_str());
            });
    }
}
```
 
Call that function from your page's constructor, and it will update `MemoryInfo` periodically.
 
The `appMemory` properties refer to general heap usage. The `extendedMemory` properties provide graphics partition information on Xbox Series S and Series X.

## Switching Display modes

Before starting playback, your app must switch the display/TV to the appropriate display mode for the content (to match the type/resolution/refresh rate). Enumerating and switching display modes on the Xbox is handled by the [HdmiDisplayInformation](/uwp/api/windows.graphics.display.core.hdmidisplayinformation).

1. Use [HdmiDisplayInformation.GetSupportedDisplayModes](/uwp/api/windows.graphics.display.core.hdmidisplayinformation.getsupporteddisplaymodes) to retrieve a list of supported display modes.

2. Use [HdmiDisplayInformation.RequestSetCurrentDisplayModeAsync](/uwp/api/windows.graphics.display.core.hdmidisplayinformation.requestsetcurrentdisplaymodeasync) HDMIDisplayInformation::RequestSetDisplayModeAsync to set the desired display mode.

> [!NOTE]
> SDR video content will play in HDR10 display modes if needed (the display pipe will do basic colorspace/eotf conversions). Playback of HDR10 video content in SDR display modes is also supported with automatic tone mapping applied (this is done in the media pipeline).
>
> We do however recommend that once playback has completed you switch back to the default display mode (SDR) by using [HdmiDisplayInformation.SetDefaultDisplayModeAsync](/uwp/api/windows.graphics.display.core.hdmidisplayinformation.setdefaultdisplaymodeasync). That's because the application UI may not be accurately color-converted when in HDR/Dolby Vision display modes, so you might notice color issues (text rendering in particular).

Automatic 3:2 pulldown is supported, so it's fine to always use 60Hz modes for all media playback. 120Hz modes are not supported for media playback.

Here are some recommendations when using a custom media source or MSE (using the built-in AdaptiveMediaSource will set these attributes correctly).

1. We strongly recommend that you set the **MF_MT_DECODER_USE_MAX_RESOLUTION** attribute to **TRUE** when setting the media type. That will ensure smoother/glitch-free playback, and optimize memory usage.

2. For the same reason, set **MF_MT_DECODER_MAX_DPB_COUNT** to 3.

## See also

* [Media playback](/windows/uwp/audio-video-camera/media-playback)
* [Media app samples for Xbox](https://github.com/microsoft/Media-App-Samples-for-Xbox) on GitHub
* [Spatial Sound for app developers for Windows, Xbox, and Hololens 2](/windows/win32/coreaudio/spatial-sound)
* [PlayReady DRM](/windows/uwp/audio-video-camera/playready-client-sdk)

* [Supported technologies on Xbox](/windows/uwp/apps-for-xbox/supported-technologies)

* [Supported audio and video codecs on Xbox](/windows/uwp/audio-video-camera/supported-codecs)
