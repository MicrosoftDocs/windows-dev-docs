---
title: Set speech recognition timeouts in Windows apps
description: Configure how long the speech recognizer ignores silence or unrecognizable sounds before ending a recognition session in your Windows App SDK app.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Set speech recognition timeouts

Set how long a speech recognizer ignores silence or unrecognizable sounds (babble) and continues listening for speech input.

> [!IMPORTANT]
> Speech recognition requires MSIX package identity. The `Windows.Media.SpeechRecognition` APIs are available only when your app runs with package identity (packaged or packaged with external location). Unpackaged apps cannot use these APIs.

## Key APIs

- [Timeouts](/uwp/api/windows.media.speechrecognition.speechrecognizer.timeouts)
- [SpeechRecognizerTimeouts](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognizerTimeouts)

## Overview

The `SpeechRecognizer.Timeouts` property returns a `SpeechRecognizerTimeouts` object with three configurable `TimeSpan` values:

| Property | Description |
|---|---|
| **InitialSilenceTimeout** | How long the `SpeechRecognizer` listens for silence before it concludes that no speech is forthcoming and finalizes an empty result. |
| **EndSilenceTimeout** | How long the `SpeechRecognizer` listens for silence after recognized speech before it finalizes the result. |
| **BabbleTimeout** | How long the `SpeechRecognizer` continues to listen for unrecognizable sounds (babble) before it assumes speech input has ended and finalizes the recognition operation. |

## Set timeout values

```csharp
// Set the initial silence timeout to 6 seconds.
speechRecognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(6.0);

// Set the end silence timeout to 1.2 seconds.
speechRecognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(1.2);

// Set the babble timeout to 0 (infinite — keep listening).
speechRecognizer.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(0);
```

> [!NOTE]
> Timeout values are set on the `SpeechRecognizer` before you start the recognition session. Changes after starting a session have no effect until the next session.

## Related articles

- [Speech recognition](speech-recognition.md)
- [Enable continuous dictation](enable-continuous-dictation.md)
- [Manage issues with audio input](manage-issues-with-audio-input.md)
