---
description: This article lists the media playback features that are available for WinUI 3 apps and links to the how-to articles that show how to use them.
title: Media playback
ms.date: 03/09/2026
ms.topic: article
keywords: windows 10, winui 3
ms.localizationpriority: medium
---

# Media playback

This section provides guidance for creating WinUI 3 apps that play back audio and video.

## Media playback features

| Topic | Description |
|-------|-------------|
| [Play audio and video with MediaPlayer](play-audio-and-video-with-mediaplayer.md) | Shows you how to play media in a WinUI app with MediaPlayer. |
| [Media items, playlists, and tracks](media-playback-with-mediasource.md) | Shows you how to use MediaSource in a WinUI app. This class provides a common way to reference and play back media from different sources such as local or remote files, and exposes a common model for accessing media data, regardless of the underlying media format. |
| [Integrate with the System Media Transport Controls](integrate-with-systemmediatransportcontrols.md) | Shows you how to interact with the System Media Transport Controls. |
| [Manual control of the System Media Transport Controls](system-media-transport-controls.md) | The SystemMediaTransportControls class enables your app to use the system media transport controls that are built into Windows and to update the metadata that the controls display about the media your app is currently playing. |
| [System-supported timed metadata cues](system-supported-metadata-cues.md) | Learn how to take advantage of several formats of timed metadata that may be embedded in media files or streams. |
| [Create, schedule, and manage media breaks](create-schedule-and-manage-media-breaks.md) | Shows you how to create, schedule, and manage media breaks to your media playback app. |
| [Adaptive streaming](adaptive-streaming.md) | Describes how to add playback of adaptive streaming multimedia content to a WinUI app. This feature currently supports playback of Http Live Streaming (HLS) and Dynamic Streaming over HTTP (DASH) content. |
| [HLS tag support](hls-tag-support.md) | Lists the HTTP Live Streaming (HLS) protocol tags supported for WinUI apps. |
| [DASH profile support](dash-profile-support.md) | Lists the Dynamic Adaptive Streaming over HTTP (DASH) profiles supported for WinUI apps. |
| [Media casting](media-casting.md) | Shows you how to cast media to remote devices from a WinUI app. |
| [Enable audio playback from remote Bluetooth-connected devices](enable-remote-audio-playback.md) | Shows you how to use AudioPlaybackConnection to enable Bluetooth-connected remote devices to play back audio on the local machine. |


## App samples for media playback

* [Adaptive streaming sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/AdaptiveStreaming)
* [Background Audio sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundMediaPlayback)
* [System Media Transport sample](https://github.com/Microsoft/Windows-universal-samples/tree/dev/Samples/SystemMediaTransportControls)

## Related topics

* [Audio, video, and camera](../audio-video-camera.md)
