---
author: mtoepke
title: Add sound
description: In this step, we examine how the shooting game sample creates an object for sound playback using the XAudio2 APIs.
ms.assetid: aa05efe2-2baa-8b9f-7418-23f5b6cd2266
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, sound
---

# Add sound


\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

In this step, we examine how the shooting game sample creates an object for sound playback using the [XAudio2](https://msdn.microsoft.com/library/windows/desktop/ee415813) APIs.

## Objective


-   To add sound output using [XAudio2](https://msdn.microsoft.com/library/windows/desktop/ee415813).

In the game sample, the audio objects and behaviors are defined in three files:

-   **Audio.h/.cpp**. This code file defines the **Audio** object, which contains the XAudio2 resources for sound playback. It also defines the method for suspending and resuming audio playback if the game is paused or deactivated.
-   **MediaReader.h/.cpp**. This code defines the methods for reading audio .wav files from local storage.
-   **SoundEffect.h/.cpp**. This code defines an object for in-game sound playback.

## Defining the audio engine


When the game sample starts, it creates an **Audio** object that allocates the audio resources for the game. The code that declares this object looks like this:

```cpp
public:
    Audio();

    void Initialize();
    void CreateDeviceIndependentResources();
    IXAudio2* MusicEngine();
    IXAudio2* SoundEffectEngine();
    void SuspendAudio();
    void ResumeAudio();

protected:
    bool                                m_audioAvailable;
    Microsoft::WRL::ComPtr<IXAudio2>    m_musicEngine;
    Microsoft::WRL::ComPtr<IXAudio2>    m_soundEffectEngine;
    IXAudio2MasteringVoice*             m_musicMasteringVoice;
    IXAudio2MasteringVoice*             m_soundEffectMasteringVoice;
};
```

The **Audio::MusicEngine** and **Audio::SoundEffectEngine** methods return references to [**IXAudio2**](https://msdn.microsoft.com/library/windows/desktop/ee415908) objects that define the mastering voice for each audio type. A mastering voice is the audio device used for playback. Sound data buffers cannot be submitted directly to mastering voices, but data submitted to other types of voices must be directed to a mastering voice to be heard.

## Initializing the audio resources


The sample initializes the [**IXAudio2**](https://msdn.microsoft.com/library/windows/desktop/ee415908) objects for the music and sound effect engines with calls to [**XAudio2Create**](https://msdn.microsoft.com/library/windows/desktop/ee419212). After the engines have been instantiated, it creates a mastering voice for each with calls to [**IXAudio2::CreateMasteringVoice**](https://msdn.microsoft.com/library/windows/desktop/hh405048), as here:

```cpp

void Audio::CreateDeviceIndependentResources()
{
    UINT32 flags = 0;

    DX::ThrowIfFailed(
        XAudio2Create(&m_musicEngine, flags)
        );

    HRESULT hr = m_musicEngine->CreateMasteringVoice(&m_musicMasteringVoice);
    if (FAILED(hr))
    {
        // Unable to create an audio device
        m_audioAvailable = false;
        return;
    }

    DX::ThrowIfFailed(
        XAudio2Create(&m_soundEffectEngine, flags)
        );

    DX::ThrowIfFailed(
        m_soundEffectEngine->CreateMasteringVoice(&m_soundEffectMasteringVoice)
        );

    m_audioAvailable = true;
}
```

As a music or sound effect audio file is loaded, this method calls [**IXAudio2::CreateSourceVoice**](https://msdn.microsoft.com/library/windows/desktop/ee418607) on the mastering voice, which creates an instance of a source voice for playback. We look at the code for this as soon as we finish reviewing how the game sample loads audio files.

## Reading an audio file


In the game sample, the code for reading audio format files is defined in **MediaReader.cpp**. The specific method that reads in an encoded .wav audio file, **MediaReader::LoadMedia**, looks like this:

```cpp
Platform::Array<byte>^  MediaReader::LoadMedia(_In_ Platform::String^ filename)
{
    DX::ThrowIfFailed(
        MFStartup(MF_VERSION)
        );

    ComPtr<IMFSourceReader> reader;
    DX::ThrowIfFailed(
        MFCreateSourceReaderFromURL(
            Platform::String::Concat(m_installedLocationPath, filename)->Data(),
            nullptr,
            &reader
            )
        );

    // Set the decoded output format as PCM
    // XAudio2 on Windows can process PCM and ADPCM-encoded buffers.
    // When using MediaFoundation, this sample always decodes into PCM.
    Microsoft::WRL::ComPtr<IMFMediaType> mediaType;
    DX::ThrowIfFailed(
        MFCreateMediaType(&mediaType)
        );

    DX::ThrowIfFailed(
        mediaType->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio)
        );

    DX::ThrowIfFailed(
        mediaType->SetGUID(MF_MT_SUBTYPE, MFAudioFormat_PCM)
        );

    DX::ThrowIfFailed(
        reader->SetCurrentMediaType(MF_SOURCE_READER_FIRST_AUDIO_STREAM, 0, mediaType.Get())
        );

    // Get the complete WAVEFORMAT from the Media Type.
    Microsoft::WRL::ComPtr<IMFMediaType> outputMediaType;
    DX::ThrowIfFailed(
        reader->GetCurrentMediaType(MF_SOURCE_READER_FIRST_AUDIO_STREAM, &outputMediaType)
        );

    UINT32 size = 0;
    WAVEFORMATEX* waveFormat;
    DX::ThrowIfFailed(
        MFCreateWaveFormatExFromMFMediaType(outputMediaType.Get(), &waveFormat, &size)
        );

    CopyMemory(&m_waveFormat, waveFormat, sizeof(m_waveFormat));
    CoTaskMemFree(waveFormat);

    // Get the total length of the stream in bytes.
    PROPVARIANT propVariant;
    DX::ThrowIfFailed(
        reader->GetPresentationAttribute(MF_SOURCE_READER_MEDIASOURCE, MF_PD_DURATION, &propVariant)
        );
    LONGLONG duration = propVariant.uhVal.QuadPart;
    unsigned int maxStreamLengthInBytes;

    double durationInSeconds = (duration / static_cast<double>(10000 * 1000));
    maxStreamLengthInBytes = static_cast<unsigned int>(durationInSeconds * m_waveFormat.nAvgBytesPerSec);

    // Make the length a multiple of 4 bytes.
    maxStreamLengthInBytes = (maxStreamLengthInBytes + 3) / 4 * 4;

    Platform::Array<byte>^ fileData = ref new Platform::Array<byte>(maxStreamLengthInBytes);

    ComPtr<IMFSample> sample;
    ComPtr<IMFMediaBuffer> mediaBuffer;
    DWORD flags = 0;

    int positionInData = 0;
    bool done = false;
    while (!done)
    {
        DX::ThrowIfFailed(
            reader->ReadSample(MF_SOURCE_READER_FIRST_AUDIO_STREAM, 0, nullptr, &flags, nullptr, &sample)
            );

        if (sample != nullptr)
        {
            DX::ThrowIfFailed(
                sample->ConvertToContiguousBuffer(&mediaBuffer)
                );

            BYTE *audioData = nullptr;
            DWORD sampleBufferLength = 0;
            DX::ThrowIfFailed(
                mediaBuffer->Lock(&audioData, nullptr, &sampleBufferLength)
                );

            for (DWORD i = 0; i < sampleBufferLength; i++)
            {
                fileData[positionInData++] = audioData[i];
            }
        }
        if (flags & MF_SOURCE_READERF_ENDOFSTREAM)
        {
            done = true;
        }
    }

    // Fix up the array size on match the actual length.
    Platform::Array<byte>^ realfileData = ref new Platform::Array<byte>((positionInData + 3) / 4 * 4);
    memcpy(realfileData->Data, fileData->Data, positionInData);
    return realfileData;
}
```

This method uses the [Media Foundation](https://msdn.microsoft.com/library/windows/desktop/ms694197) APIs to read in the .wav audio file as a Pulse Code Modulation (PCM) buffer.

1.  Creates a media source reader ([**IMFSourceReader**](https://msdn.microsoft.com/library/windows/desktop/dd374655)) object by calling [**MFCreateSourceReaderFromURL**](https://msdn.microsoft.com/library/windows/desktop/dd388110).
2.  Creates a media type ([**IMFMediaType**](https://msdn.microsoft.com/library/windows/desktop/ms704850)) for the decoding of the audio file by calling [**MFCreateMediaType**](https://msdn.microsoft.com/library/windows/desktop/ms693861). This method specifies that the decoded output is PCM audio, which is an audio type that XAudio2 can use.
3.  Sets the decoded output media type for the reader by calling [**IMFSourceReader::SetCurrentMediaType**](https://msdn.microsoft.com/library/windows/desktop/bb970432).
4.  Creates a [**WAVEFORMATEX**](https://msdn.microsoft.com/library/windows/hardware/ff538799) buffer and copies the results of a call to [**IMFMediaType::MFCreateWaveFormatExFromMFMediaType**](https://msdn.microsoft.com/library/windows/desktop/ms702177) on the [**IMFMediaType**](https://msdn.microsoft.com/library/windows/desktop/ms704850) object. This formats the buffer that holds the audio file after it is loaded.
5.  Gets the duration, in seconds, of the audio stream by calling [**IMFSourceReader::GetPresentationAttribute**](https://msdn.microsoft.com/library/windows/desktop/dd374662) and then converts the duration to bytes.
6.  Reads the audio file in as a stream by calling [**IMFSourceReader::ReadSample**](https://msdn.microsoft.com/library/windows/desktop/dd374665).
7.  Copies the contents of the audio sample buffer into an array returned by the method.

The most important thing in **SoundEffect::Initialize** is the creation of the source voice object, **m\_sourceVoice**, from the mastering voice. We use the source voice for the actual play back of the sound data buffer obtained from **MediaReader::LoadMedia**.

The sample game calls this method when it initializes the **SoundEffect** object, like this:

```cpp
void SoundEffect::Initialize(
    _In_ IXAudio2 *masteringEngine,
    _In_ WAVEFORMATEX *sourceFormat,
    _In_ Platform::Array<byte>^ soundData)
{
    m_soundData = soundData;

    if (masteringEngine == nullptr)
    {
        // Audio is not available, so return.
        m_audioAvailable = false;
        return;
    }

    // Create and reuse a single source voice for the single sound effect in this sample.
    DX::ThrowIfFailed(
        masteringEngine->CreateSourceVoice(
            &m_sourceVoice,
            sourceFormat
            )
        );
    m_audioAvailable = true;
}
```

This method is passed the results of calls to **Audio::SoundEffectEngine** (or **Audio::MusicEngine**), **MediaReader::GetOutputWaveFormatEx**, and the buffer returned by a call to **MediaReader::LoadMedia**, as seen here.

```cpp
MediaReader^ mediaReader = ref new MediaReader;
auto targetHitSound = mediaReader->LoadMedia("hit.wav");
```

```cpp
myTarget->HitSound(ref new SoundEffect());
myTarget->HitSound()->Initialize(
                m_audioController->SoundEffectEngine(),
                mediaReader->GetOutputWaveFormatEx(),
                targetHitSound);
```

**SoundEffect::Initialize** is called from the **Simple3DGame:Initialize** method that initializes the main game object.

Now that the sample game has an audio file in memory, let's see how it plays it back during game play!

## Playing back an audio file


```cpp
void SoundEffect::PlaySound(_In_ float volume)
{
    XAUDIO2_BUFFER buffer = {0};
    XAUDIO2_VOICE_STATE state = {0};

    if (!m_audioAvailable)
    {
        // Audio is not available, so just return.
        return;
    }

    // Interrupt sound effect if currently playing.
    DX::ThrowIfFailed(
        m_sourceVoice->Stop()
        );
    DX::ThrowIfFailed(
        m_sourceVoice->FlushSourceBuffers()
        );

    // Queue in-memory buffer for playback and start the voice.
    buffer.AudioBytes = m_soundData->Length;
    buffer.pAudioData = m_soundData->Data;
    buffer.Flags = XAUDIO2_END_OF_STREAM;

    m_sourceVoice->SetVolume(volume);
    DX::ThrowIfFailed(
        m_sourceVoice->SubmitSourceBuffer(&buffer)
        );
    DX::ThrowIfFailed(
        m_sourceVoice->Start()
        );
}
```

To play the sound, this method uses the source voice object **m\_sourceVoice** to start the playback of the sound data buffer **m\_soundData**. It creates an [**XAUDIO2\_BUFFER**](https://msdn.microsoft.com/library/windows/desktop/ee419228), to which it provides a reference to the sound data buffer, and then submits it with a call to [**IXAudio2SourceVoice::SubmitSourceBuffer**](https://msdn.microsoft.com/library/windows/desktop/ee418473). With the sound data queued up, **SoundEffect::PlaySound** starts play back by calling [**IXAudio2SourceVoice::Start**](https://msdn.microsoft.com/library/windows/desktop/ee418471).

Now, whenever a collision between the ammo and a target occurs, a call to **SoundEffect::PlaySound** causes a noise to play.

## Next steps


That was a whirlwind tour of Universal Windows Platform (UWP) DirectX game development! At this point, you have an idea of what you need to do to make your own game for Windows 8 a great experience. Remember, your game can be played on a wide variety of Windows 8 devices and platforms, so design your components: your graphics, your controls, your user interface, and your audio for as wide a set of configurations as you can!

For more info about ways to modify the game sample provided in these documents, see [Extending the game sample](tutorial-resources.md).

## Complete sample code for this section


Audio.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

ref class Audio
{
public:
    Audio();

    void Initialize();
    void CreateDeviceIndependentResources();
    IXAudio2* MusicEngine();
    IXAudio2* SoundEffectEngine();
    void SuspendAudio();
    void ResumeAudio();

protected:
    bool                                m_audioAvailable;
    Microsoft::WRL::ComPtr<IXAudio2>    m_musicEngine;
    Microsoft::WRL::ComPtr<IXAudio2>    m_soundEffectEngine;
    IXAudio2MasteringVoice*             m_musicMasteringVoice;
    IXAudio2MasteringVoice*             m_soundEffectMasteringVoice;
};
```

Audio.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "Audio.h"
#include "DirectXSample.h"

using namespace Microsoft::WRL;
using namespace Windows::Foundation;
using namespace Windows::UI::Core;
using namespace Windows::Graphics::Display;

Audio::Audio():
    m_audioAvailable(false)
{
}

void Audio::Initialize()
{
}

void Audio::CreateDeviceIndependentResources()
{
    UINT32 flags = 0;

    DX::ThrowIfFailed(
        XAudio2Create(&m_musicEngine, flags)
        );

#if defined(_DEBUG)
    XAUDIO2_DEBUG_CONFIGURATION debugConfiguration = {0};
    debugConfiguration.BreakMask = XAUDIO2_LOG_ERRORS;
    debugConfiguration.TraceMask = XAUDIO2_LOG_ERRORS;
    m_musicEngine->SetDebugConfiguration(&debugConfiguration);
#endif


    HRESULT hr = m_musicEngine->CreateMasteringVoice(&m_musicMasteringVoice);
    if (FAILED(hr))
    {
        // Unable to create an audio device
        m_audioAvailable = false;
        return;
    }

    DX::ThrowIfFailed(
        XAudio2Create(&m_soundEffectEngine, flags)
        );

#if defined(_DEBUG)
    m_soundEffectEngine->SetDebugConfiguration(&debugConfiguration);
#endif

    DX::ThrowIfFailed(
        m_soundEffectEngine->CreateMasteringVoice(&m_soundEffectMasteringVoice)
        );

    m_audioAvailable = true;
}

IXAudio2* Audio::MusicEngine()
{
    return m_musicEngine.Get();
}

IXAudio2* Audio::SoundEffectEngine()
{
    return m_soundEffectEngine.Get();
}

void Audio::SuspendAudio()
{
    if (m_audioAvailable)
    {
        m_musicEngine->StopEngine();
        m_soundEffectEngine->StopEngine();
    }
}

void Audio::ResumeAudio()
{
    if (m_audioAvailable)
    {
        DX::ThrowIfFailed(m_musicEngine->StartEngine());
        DX::ThrowIfFailed(m_soundEffectEngine->StartEngine());
    }
}
```

SoundEffect.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

ref class SoundEffect
{
public:
    SoundEffect();

    void Initialize(
        _In_ IXAudio2*              masteringEngine,
        _In_ WAVEFORMATEX*          sourceFormat,
        _In_ Platform::Array<byte>^ soundData
        );

    void PlaySound(_In_ float volume);

protected:
    bool                    m_audioAvailable;
    IXAudio2SourceVoice*    m_sourceVoice;
    Platform::Array<byte>^  m_soundData;
};
```

SoundEffect.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "SoundEffect.h"
#include "DirectXSample.h"

SoundEffect::SoundEffect():
    m_audioAvailable(false)
{
}
//----------------------------------------------------------------------
void SoundEffect::Initialize(
    _In_ IXAudio2 *masteringEngine,
    _In_ WAVEFORMATEX *sourceFormat,
    _In_ Platform::Array<byte>^ soundData)
{
    m_soundData = soundData;

    if (masteringEngine == nullptr)
    {
        // Audio is not available, so return.
        m_audioAvailable = false;
        return;
    }

    // Create and reuse a single source voice for the single sound effect in this sample.
    DX::ThrowIfFailed(
        masteringEngine->CreateSourceVoice(
            &m_sourceVoice,
            sourceFormat
            )
        );
    m_audioAvailable = true;
}
//----------------------------------------------------------------------
void SoundEffect::PlaySound(_In_ float volume)
{
    XAUDIO2_BUFFER buffer = {0};
    XAUDIO2_VOICE_STATE state = {0};

    if (!m_audioAvailable)
    {
        // Audio is not available, so just return.
        return;
    }

    // Interrupt sound effect if currently playing.
    DX::ThrowIfFailed(
        m_sourceVoice->Stop()
        );
    DX::ThrowIfFailed(
        m_sourceVoice->FlushSourceBuffers()
        );

    // Queue in-memory buffer for playback and start the voice.
    buffer.AudioBytes = m_soundData->Length;
    buffer.pAudioData = m_soundData->Data;
    buffer.Flags = XAUDIO2_END_OF_STREAM;

    m_sourceVoice->SetVolume(volume);
    DX::ThrowIfFailed(
        m_sourceVoice->SubmitSourceBuffer(&buffer)
        );
    DX::ThrowIfFailed(
        m_sourceVoice->Start()
        );
}
```

 

 




