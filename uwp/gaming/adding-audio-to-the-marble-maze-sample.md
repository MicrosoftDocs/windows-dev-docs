---
title: Adding audio to the Marble Maze sample
description: This document describes the key practices to consider when you work with audio and shows how Marble Maze applies these practices.
ms.assetid: 77c23d0a-af6d-17b5-d69e-51d9885b0d44
ms.date: 10/18/2017
ms.topic: article
keywords: windows 10, uwp, audio, games, sample
ms.localizationpriority: medium
---
# Adding audio to the Marble Maze sample

This document describes the key practices to consider when you work with audio and shows how Marble Maze applies these practices. Marble Maze uses [Microsoft Media Foundation](/windows/desktop/medfound/microsoft-media-foundation-sdk) to load audio resources from files, and [XAudio2](/windows/desktop/xaudio2/xaudio2-apis-portal) to mix and play audio and to apply effects to audio.

Marble Maze plays music in the background, and also uses gameplay sounds to indicate game events, such as when the marble hits a wall. An important part of the implementation is that Marble Maze uses a reverb, or echo, effect to simulate the sound of a marble when it bounces. The reverb effect implementation causes echoes to reach you more quickly and loudly in small rooms; echoes are quieter and reach you more slowly in larger rooms.

> [!NOTE]
> The sample code that corresponds to this document is found in the [DirectX Marble Maze game sample](https://github.com/microsoft/Windows-appsample-marble-maze).

Here are some of the key points that this document discusses for when you work with audio in your game:

- Consider using Media Foundation to decode audio assets and XAudio2 to play audio. However, if you have an existing audio asset-loading mechanism that works in a Universal Windows Platform (UWP) app, you can use it.

- An audio graph contains one source voice for each active sound, zero or more submix voices, and one mastering voice. Source voices can feed into submix voices and/or the mastering voice. Submix voices feed into other submix voices or the mastering voice.

- If your background music files are large, consider streaming your music into smaller buffers so that less memory is used.

- If it makes sense to do so, pause audio playback when the app loses focus or visibility, or is suspended. Resume playback when your app regains focus, becomes visible, or is resumed.

- Set audio categories to reflect the role of each sound. For example, you typically use **AudioCategory\_GameMedia** for game background audio and **AudioCategory\_GameEffects** for sound effects.

- Handle device changes, including headphones, by releasing and recreating all audio resources and interfaces.

- Consider whether to compress audio files when minimizing disk space and streaming costs is a requirement. Otherwise, you can leave audio uncompressed so that it loads faster.

## Introducing XAudio2 and Microsoft Media Foundation

XAudio2 is a low-level audio library for Windows that specifically supports game audio. It provides a digital signal processing (DSP) and audio-graph engine for games. XAudio2 expands on its predecessors, DirectSound and XAudio, by supporting computing trends such as SIMD floating-point architectures and HD audio. It also supports the more complex sound processing demands of today’s games.

The document [XAudio2 Key Concepts](/windows/desktop/xaudio2/xaudio2-key-concepts) explains the key concepts for using XAudio2. In brief, the concepts are:

- The [IXAudio2](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2) interface is the core of the XAudio2 engine. Marble Maze uses this interface to create voices and to receive notification when the output device changes or fails.

- A **voice** processes, adjusts, and plays audio data.

- A **source voice** is a collection of audio channels (mono, 5.1, and so on) and represents one stream of audio data. In XAudio2, a source voice is where audio processing begins. Typically, sound data is loaded from an external source, such as a file or a network, and is sent to a source voice. Marble Maze uses [Media Foundation](/windows/desktop/medfound/microsoft-media-foundation-sdk) to load sound data from files. Media Foundation is introduced later in this document.

- A **submix voice** processes audio data. This processing can include changing the audio stream or combining multiple streams into one. Marble Maze uses submixes to create the reverb effect.

- A **mastering voice** combines data from source and submix voices and sends that data to the audio hardware.

- An **audio graph** contains one source voice for each active sound, zero or more submix voices, and only one mastering voice.

- A **callback** informs client code that some event has occurred in a voice or in an engine object. By using callbacks, you can reuse memory when XAudio2 is finished with a buffer, react when the audio device changes (for example, when you connect or disconnect headphones), and more. [Handling headphones and device changes](#handling-headphones-and-device-changes) later in this document explains how Marble Maze uses this mechanism to handle device changes.

Marble Maze uses two audio engines (in other words, two [IXAudio2](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2) objects) to process audio. One engine processes the background music, and the other engine processes gameplay sounds.

Marble Maze must also create one mastering voice for each engine. Recall that a mastering engine combines audio streams into one stream and sends that stream to the audio hardware. The background music stream, a source voice, outputs data to a mastering voice and to two submix voices. The submix voices perform the reverb effect.

Media Foundation is a multimedia library that supports many audio and video formats. XAudio2 and Media Foundation complement each other. Marble Maze uses Media Foundation to load audio assets from files and uses XAudio2 to play audio. You don't have to use Media Foundation to load audio assets. If you have an existing audio asset loading mechanism that works in Universal Windows Platform (UWP) apps, use it. [Audio, video, and camera](../audio-video-camera/index.md) discusses several ways of implementing audio in a UWP app.

For more information about XAudio2, see [Programming Guide](/windows/desktop/xaudio2/programming-guide). For more information about Media Foundation, see [Microsoft Media Foundation](/windows/desktop/medfound/microsoft-media-foundation-sdk).

## Initializing audio resources

Marble Mazes uses a Windows Media Audio (.wma) file for the background music, and WAV (.wav) files for gameplay sounds. These formats are supported by Media Foundation. Although the .wav file format is natively supported by XAudio2, a game has to parse the file format manually to fill out the appropriate XAudio2 data structures. Marble Maze uses Media Foundation to more easily work with .wav files. For the complete list of the media formats that are supported by Media Foundation, see [Supported Media Formats in Media Foundation](/windows/desktop/medfound/supported-media-formats-in-media-foundation). Marble Maze does not use separate design-time and run-time audio formats, and does not use XAudio2 ADPCM compression support. For more information about ADPCM compression in XAudio2, see [ADPCM Overview](/windows/desktop/xaudio2/adpcm-overview).

The **Audio::CreateResources** method, which is called from **MarbleMazeMain::LoadDeferredResources**, loads the audio streams from the files, initializes the XAudio2 engine objects, and creates the source, submix, and mastering voices.

### Creating the XAudio2 engines

Recall that Marble Maze creates one [IXAudio2](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2) object to represent each audio engine that it uses. To create an audio engine, call the [XAudio2Create](/windows/desktop/api/xaudio2/nf-xaudio2-xaudio2create) method. The following example shows how Marble Maze creates the audio engine that processes background music.

```cpp
// In Audio.h
class Audio
{
private:
    IXAudio2*                   m_musicEngine;
// ...
}

// In Audio.cpp
void Audio::CreateResources()
{
    try
    {
        // ...
        DX::ThrowIfFailed(
            XAudio2Create(&m_musicEngine)
            );
        // ...
    }
    // ...
}
```

Marble Maze performs a similar step to create the audio engine that plays gameplay sounds.

How to work with the [IXAudio2](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2) interface in a UWP app differs from a desktop app in two ways. First, you don't have to call [CoInitializeEx](/windows/desktop/api/combaseapi/nf-combaseapi-coinitializeex) before you call [XAudio2Create](/windows/desktop/api/xaudio2/nf-xaudio2-xaudio2create). In addition, **IXAudio2** no longer supports device enumeration. For information about how to enumerate audio devices, see [Enumerating devices](/previous-versions/windows/apps/hh464977(v=win.10)).

### Creating the mastering voices

The following example shows how the **Audio::CreateResources** method creates the mastering voice for the background music using the [IXAudio2::CreateMasteringVoice](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2-createmasteringvoice) method. In this example, **m\_musicMasteringVoice** is an [IXAudio2MasteringVoice](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2masteringvoice) object. We specify two input channels; this simplifies the logic for the reverb effect. 

We specify 48000 as the input sample rate. We chose this sample rate because it represented a balance between audio quality and the amount of required CPU processing. A greater sample rate would have required more CPU processing without having a noticeable quality benefit. 

Finally, we specify **AudioCategory_GameMedia** as the audio stream category so that users can listen to music from a different application as they play the game. When a music app is playing, Windows mutes any voices that are created by the **AudioCategory\_GameMedia** option. The user still hears gameplay sounds because they are created by the **AudioCategory\_GameEffects** option. For more info about audio categories, see [AUDIO\_STREAM\_CATEGORY](/windows/win32/api/audiosessiontypes/ne-audiosessiontypes-audio_stream_category).

```cpp
// This sample plays the equivalent of background music, which we tag on the  
// mastering voice as AudioCategory_GameMedia. In ordinary usage, if we were  
// playing the music track with no effects, we could route it entirely through 
// Media Foundation. Here, we are using XAudio2 to apply a reverb effect to the 
// music, so we use Media Foundation to decode the data then we feed it through 
// the XAudio2 pipeline as a separate Mastering Voice, so that we can tag it 
// as Game Media. We default the mastering voice to 2 channels to simplify  
// the reverb logic.
DX::ThrowIfFailed(
    m_musicEngine->CreateMasteringVoice(
        &m_musicMasteringVoice,
        2,
        48000,
        0,
        nullptr,
        nullptr,
        AudioCategory_GameMedia
        )
);
```

The **Audio::CreateResources** method performs a similar step to create the mastering voice for the gameplay sounds, except that it specifies **AudioCategory\_GameEffects** for the *StreamCategory* parameter, which is the default.

### Creating the reverb effect

For each voice, you can use XAudio2 to create sequences of effects that process audio. Such a sequence is known as an effect chain. Use effect chains when you want to apply one or more effects to a voice. Effect chains can be destructive; that is, each effect in the chain can overwrite the audio buffer. This property is important because XAudio2 makes no guarantee that output buffers are initialized with silence. Effect objects are represented in XAudio2 by cross-platform audio processing objects (XAPO). For more information about XAPO, see [XAPO Overview](/windows/desktop/xaudio2/xapo-overview).

When you create an effect chain, follow these steps:

1. Create the effect object.

2. Populate an [XAUDIO2\_EFFECT\_DESCRIPTOR](/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_effect_descriptor) structure with effect data.

3. Populate an [XAUDIO2\_EFFECT\_CHAIN](/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_effect_chain) structure with data.

4. Apply the effect chain to a voice.

5. Populate an effect parameter structure and apply it to the effect.

6. Disable or enable the effect whenever appropriate.

The **Audio** class defines the **CreateReverb** method to create the effect chain that implements reverb. This method calls the [XAudio2CreateReverb](/windows/desktop/api/xaudio2fx/nf-xaudio2fx-xaudio2createreverb) method to create a **ComPtr&lt;IUnknown&gt;** object, **soundEffectXAPO**, which acts as the submix voice for the reverb effect.

```cpp
Microsoft::WRL::ComPtr<IUnknown> soundEffectXAPO;

DX::ThrowIfFailed(
    XAudio2CreateReverb(&soundEffectXAPO)
    );
```

The [XAUDIO2\_EFFECT\_DESCRIPTOR](/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_effect_descriptor) structure contains information about an XAPO for use in an effect chain, for example, the target number of output channels. The **Audio::CreateReverb** method creates an **XAUDIO2\_EFFECT\_DESCRIPTOR** object, **soundEffectdescriptor**, that is set to the disabled state, uses two output channels, and references **soundEffectXAPO** for the reverb effect. **soundEffectdescriptor** starts in the disabled state because the game must set parameters before the effect starts modifying game sounds. Marble Maze uses two output channels to simplify the logic for the reverb effect.

```cpp
soundEffectdescriptor.InitialState = false;
soundEffectdescriptor.OutputChannels = 2;
soundEffectdescriptor.pEffect = soundEffectXAPO.Get();
```

If your effect chain has multiple effects, each effect requires an object. The [XAUDIO2\_EFFECT\_CHAIN](/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_effect_chain) structure holds the array of [XAUDIO2\_EFFECT\_DESCRIPTOR](/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_effect_descriptor) objects that participate in the effect. The following example shows how the **Audio::CreateReverb** method specifies the one effect to implement reverb.

```cpp
XAUDIO2_EFFECT_CHAIN soundEffectChain;

// ...

soundEffectChain.EffectCount = 1;
soundEffectChain.pEffectDescriptors = &soundEffectdescriptor;
```

The **Audio::CreateReverb** method calls the [IXAudio2::CreateSubmixVoice](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2-createsubmixvoice) method to create the submix voice for the effect. It specifies the [XAUDIO2\_EFFECT\_CHAIN](/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_effect_chain) object, **soundEffectChain**, for the *pEffectChain* parameter to associate the effect chain with the voice. Marble Maze also specifies two output channels and a sample rate of 48 kilohertz.

```cpp
DX::ThrowIfFailed(
    engine->CreateSubmixVoice(newSubmix, 2, 48000, 0, 0, nullptr, &soundEffectChain)
    );
```

> [!TIP]
> If you want to attach an existing effect chain to an existing submix voice, or you want to replace the current effect chain, use the [IXAudio2Voice::SetEffectChain](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2voice-seteffectchain) method.

The **Audio::CreateReverb** method calls [IXAudio2Voice::SetEffectParameters](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2voice-seteffectparameters) to set additional parameters that are associated with the effect. This method takes a parameter structure that is specific to the effect. An [XAUDIO2FX\_REVERB\_PARAMETERS](/windows/desktop/api/xaudio2fx/ns-xaudio2fx-xaudio2fx_reverb_parameters) object, **m_reverbParametersSmall**, which contains the effect parameters for reverb, is initialized in the **Audio::Initialize** method because every reverb effect shares the same parameters. The following example shows how the **Audio::Initialize** method initializes the reverb parameters for near-field reverb.

```cpp
m_reverbParametersSmall.ReflectionsDelay = XAUDIO2FX_REVERB_DEFAULT_REFLECTIONS_DELAY;
m_reverbParametersSmall.ReverbDelay = XAUDIO2FX_REVERB_DEFAULT_REVERB_DELAY;
m_reverbParametersSmall.RearDelay = XAUDIO2FX_REVERB_DEFAULT_REAR_DELAY;
m_reverbParametersSmall.PositionLeft = XAUDIO2FX_REVERB_DEFAULT_POSITION;
m_reverbParametersSmall.PositionRight = XAUDIO2FX_REVERB_DEFAULT_POSITION;
m_reverbParametersSmall.PositionMatrixLeft = XAUDIO2FX_REVERB_DEFAULT_POSITION_MATRIX;
m_reverbParametersSmall.PositionMatrixRight = XAUDIO2FX_REVERB_DEFAULT_POSITION_MATRIX;
m_reverbParametersSmall.EarlyDiffusion = 4;
m_reverbParametersSmall.LateDiffusion = 15;
m_reverbParametersSmall.LowEQGain = XAUDIO2FX_REVERB_DEFAULT_LOW_EQ_GAIN;
m_reverbParametersSmall.LowEQCutoff = XAUDIO2FX_REVERB_DEFAULT_LOW_EQ_CUTOFF;
m_reverbParametersSmall.HighEQGain = XAUDIO2FX_REVERB_DEFAULT_HIGH_EQ_GAIN;
m_reverbParametersSmall.HighEQCutoff = XAUDIO2FX_REVERB_DEFAULT_HIGH_EQ_CUTOFF;
m_reverbParametersSmall.RoomFilterFreq = XAUDIO2FX_REVERB_DEFAULT_ROOM_FILTER_FREQ;
m_reverbParametersSmall.RoomFilterMain = XAUDIO2FX_REVERB_DEFAULT_ROOM_FILTER_MAIN;
m_reverbParametersSmall.RoomFilterHF = XAUDIO2FX_REVERB_DEFAULT_ROOM_FILTER_HF;
m_reverbParametersSmall.ReflectionsGain = XAUDIO2FX_REVERB_DEFAULT_REFLECTIONS_GAIN;
m_reverbParametersSmall.ReverbGain = XAUDIO2FX_REVERB_DEFAULT_REVERB_GAIN;
m_reverbParametersSmall.DecayTime = XAUDIO2FX_REVERB_DEFAULT_DECAY_TIME;
m_reverbParametersSmall.Density = XAUDIO2FX_REVERB_DEFAULT_DENSITY;
m_reverbParametersSmall.RoomSize = XAUDIO2FX_REVERB_DEFAULT_ROOM_SIZE;
m_reverbParametersSmall.WetDryMix = XAUDIO2FX_REVERB_DEFAULT_WET_DRY_MIX;
m_reverbParametersSmall.DisableLateField = TRUE;
```

This example uses the default values for most of the reverb parameters, but it sets **DisableLateField** to TRUE to specify near-field reverb, **EarlyDiffusion** to 4 to simulate flat near surfaces, and **LateDiffusion** to 15 to simulate very diffuse distant surfaces. Flat near surfaces cause echoes to reach you more quickly and loudly; diffuse distant surfaces cause echoes to be quieter and reach you more slowly. You can experiment with reverb values to get the desired effect in your game or use the **ReverbConvertI3DL2ToNative** method to use industry-standard I3DL2 (Interactive 3D Audio Rendering Guidelines Level 2.0) parameters.

The following example shows how **Audio::CreateReverb** sets the reverb parameters. **newSubmix** is an [IXAudio2SubmixVoice](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2submixvoice)** object. **parameters** is an [XAUDIO2FX\_REVERB\_PARAMETERS](/windows/desktop/api/xaudio2fx/ns-xaudio2fx-xaudio2fx_reverb_parameters)* object.

```cpp
DX::ThrowIfFailed(
    (*newSubmix)->SetEffectParameters(0, parameters, sizeof(m_reverbParametersSmall))
    );
```

The **Audio::CreateReverb** method finishes by enabling the effect using [IXAudio2Voice::EnableEffect](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2voice-enableeffect) if the **enableEffect** flag is set. It also sets its volume using [IXAudio2Voice::SetVolume](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2voice-setvolume) and output matrix using [IXAudio2Voice::SetOutputMatrix](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2voice-setoutputmatrix). This part sets the volume to full (1.0) and then specifies the volume matrix to be silence for both left and right inputs and left and right output speakers. We do this because other code later cross-fades between the two reverbs (simulating the transition from being near a wall to being in a large room), or mutes both reverbs if required. When the reverb path is later unmuted, the game sets a matrix of {1.0f, 0.0f, 0.0f, 1.0f} to route left reverb output to the left input of the mastering voice and right reverb output to the right input of the mastering voice.

```cpp
if (enableEffect)
{
    DX::ThrowIfFailed(
        (*newSubmix)->EnableEffect(0)
        );    
}

DX::ThrowIfFailed(
    (*newSubmix)->SetVolume (1.0f)
    );

float outputMatrix[4] = {0, 0, 0, 0};
DX::ThrowIfFailed(
    (*newSubmix)->SetOutputMatrix(masteringVoice, 2, 2, outputMatrix)
    );
```

Marble Maze calls the **Audio::CreateReverb** method four times: two times for the background music and two times for the gameplay sounds. The following shows how Marble Maze calls the **CreateReverb** method for the background music.

```cpp
CreateReverb(
    m_musicEngine, 
    m_musicMasteringVoice, 
    &m_reverbParametersSmall, 
    &m_musicReverbVoiceSmallRoom, 
    true
    );
CreateReverb(
    m_musicEngine, 
    m_musicMasteringVoice, 
    &m_reverbParametersLarge, 
    &m_musicReverbVoiceLargeRoom, 
    true
    );
```

For a list of possible sources of effects for use with XAudio2, see [XAudio2 Audio Effects](/windows/desktop/xaudio2/xaudio2-audio-effects).

### Loading audio data from file

Marble Maze defines the **MediaStreamer** class, which uses Media Foundation to load audio resources from files. Marble Maze uses one **MediaStreamer** object to load each audio file.

Marble Maze calls the **MediaStreamer::Initialize** method to initialize each audio stream. Here's how the **Audio::CreateResources** method calls **MediaStreamer::Initialize** to initialize the audio stream for the background music:

```cpp
// Media Foundation is a convenient way to get both file I/O and format decode for 
// audio assets. You can replace the streamer in this sample with your own file I/O 
// and decode routines.
m_musicStreamer.Initialize(L"Media\\Audio\\background.wma");
```

The **MediaStreamer::Initialize** method starts by calling the [MFStartup](/windows/desktop/api/mfapi/nf-mfapi-mfstartup) method to initialize Media Foundation. **MF_VERSION** is a macro defined in **mfapi.h**, and is what we specify as the version of Media Foundation to use.

```cpp
DX::ThrowIfFailed(
    MFStartup(MF_VERSION)
    );
```

**MediaStreamer::Initialize** then calls [MFCreateSourceReaderFromURL](/windows/desktop/api/mfreadwrite/nf-mfreadwrite-mfcreatesourcereaderfromurl) to create an [IMFSourceReader](/windows/desktop/api/mfreadwrite/nn-mfreadwrite-imfsourcereader) object. An **IMFSourceReader** object, **m_reader**, reads media data from the file that is specified by **url**.

```cpp
DX::ThrowIfFailed(
    MFCreateSourceReaderFromURL(url, nullptr, &m_reader)
    );
```

The **MediaStreamer::Initialize** method then creates an [IMFMediaType](/windows/desktop/api/mfobjects/nn-mfobjects-imfmediatype) object using [MFCreateMediaType](/windows/desktop/api/mfapi/nf-mfapi-mfcreatemediatype) to describe the format of the audio stream. An audio format has two types: a major type and a subtype. The major type defines the overall format of the media, such as video, audio, script, and so on. The subtype defines the format, such as PCM, ADPCM, or WMA.

The **MediaStreamer::Initialize** method uses the [IMFAttributes::SetGUID](/windows/desktop/api/mfobjects/nf-mfobjects-imfattributes-setguid) method to specify the major type ([MF_MT_MAJOR_TYPE](/windows/desktop/medfound/mf-mt-major-type-attribute)) as audio (**MFMediaType\_Audio**) and the minor type ([MF_MT_SUBTYPE](/windows/desktop/medfound/mf-mt-subtype-attribute)) as uncompressed PCM audio (**MFAudioFormat\_PCM**). **MF_MT_MAJOR_TYPE** and **MF_MT_SUBTYPE** are [Media Foundation Attributes](/windows/desktop/medfound/media-foundation-attributes). **MFMediaType_Audio** and **MFAudioFormat_PCM** are type and subtype GUIDs; see [Audio Media Types](/windows/desktop/medfound/audio-media-types) for more information. The [IMFSourceReader::SetCurrentMediaType](/windows/desktop/api/mfreadwrite/nf-mfreadwrite-imfsourcereader-setcurrentmediatype) method associates the media type with the stream reader.

```cpp
// Set the decoded output format as PCM. 
// XAudio2 on Windows can process PCM and ADPCM-encoded buffers. 
// When this sample uses Media Foundation, it always decodes into PCM.

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
    m_reader->SetCurrentMediaType(MF_SOURCE_READER_FIRST_AUDIO_STREAM, 0, mediaType.Get())
    );
```

The **MediaStreamer::Initialize** method then obtains the complete output media format from Media Foundation using [IMFSourceReader::GetCurrentMediaType](/windows/desktop/api/mfreadwrite/nf-mfreadwrite-imfsourcereader-getcurrentmediatype) and calls the [MFCreateWaveFormatExFromMFMediaType](/windows/desktop/api/mfapi/nf-mfapi-mfcreatewaveformatexfrommfmediatype) method to convert the Media Foundation audio media type to a [WAVEFORMATEX](/windows/win32/api/mmeapi/ns-mmeapi-waveformatex) structure. The **WAVEFORMATEX** structure defines the format of waveform-audio data. Marble Maze uses this structure to create the source voices and to apply the low-pass filter to the marble rolling sound.

```cpp
// Get the complete WAVEFORMAT from the Media Type.
DX::ThrowIfFailed(
    m_reader->GetCurrentMediaType(MF_SOURCE_READER_FIRST_AUDIO_STREAM, &outputMediaType)
    );

uint32 formatSize = 0;
WAVEFORMATEX* waveFormat;
DX::ThrowIfFailed(
    MFCreateWaveFormatExFromMFMediaType(outputMediaType.Get(), &waveFormat, &formatSize)
    );
CopyMemory(&m_waveFormat, waveFormat, sizeof(m_waveFormat));
CoTaskMemFree(waveFormat);
```

> [!IMPORTANT]
> The [MFCreateWaveFormatExFromMFMediaType](/windows/desktop/api/mfapi/nf-mfapi-mfcreatewaveformatexfrommfmediatype) method uses **CoTaskMemAlloc** to allocate the [WAVEFORMATEX](/windows/win32/api/mmeapi/ns-mmeapi-waveformatex) object. Therefore, make sure that you call **CoTaskMemFree** when you are finished using this object.

 

The **MediaStreamer::Initialize** method finishes by computing the length of the stream, **m\_maxStreamLengthInBytes**, in bytes. To do so, it calls the [IMFSourceReader::GetPresentationAttribute](/windows/desktop/api/mfreadwrite/nf-mfreadwrite-imfsourcereader-getpresentationattribute) method to get the duration of the audio stream in 100-nanosecond units, converts the duration to sections, and then multiplies by the average data transfer rate in bytes per second. Marble Maze later uses this value to allocate the buffer that holds each gameplay sound.

```cpp
// Get the total length of the stream, in bytes.
PROPVARIANT var;
DX::ThrowIfFailed(
    m_reader->
        GetPresentationAttribute(MF_SOURCE_READER_MEDIASOURCE, MF_PD_DURATION, &var)
    );

// duration is in 100ns units; convert to seconds, and round up
// to the nearest whole byte.
ULONGLONG duration = var.uhVal.QuadPart;
m_maxStreamLengthInBytes =
    static_cast<unsigned int>(
        ((duration * static_cast<ULONGLONG>(m_waveFormat.nAvgBytesPerSec)) + 10000000)
        / 10000000
        );
```

### Creating the source voices

Marble Maze creates XAudio2 source voices to play each of its game sounds and music in source voices. The **Audio** class defines an [IXAudio2SourceVoice](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2sourcevoice) object for the background music and an array of **SoundEffectData** objects to hold the gameplay sounds. The **SoundEffectData** structure holds the **IXAudio2SourceVoice** object for an effect and also defines other effect-related data, such as the audio buffer. **Audio.h** defines the **SoundEvent** enumeration. Marble Maze uses this enumeration to identify each gameplay sound. The **Audio** class also uses this enumeration to index the array of **SoundEffectData** objects.

```cpp
enum SoundEvent
{
    RollingEvent        = 0,
    FallingEvent        = 1,
    CollisionEvent      = 2,
    CheckpointEvent     = 3,
    MenuChangeEvent     = 4,
    MenuSelectedEvent   = 5,
    LastSoundEvent,
};
```

The following table shows the relationship between each of these values, the file that contains the associated sound data, and a brief description of what each sound represents. The audio files are located in the **\\Media\\Audio** folder.

| SoundEvent value  | File name      | Description                                              |
|-------------------|----------------|----------------------------------------------------------|
| RollingEvent      | MarbleRoll.wav | Played as the marble rolls.                              |
| FallingEvent      | MarbleFall.wav | Played when the marble falls off the maze.               |
| CollisionEvent    | MarbleHit.wav  | Played when the marble collides with the maze.           |
| CheckpointEvent   | Checkpoint.wav | Played when the marble passes over a checkpoint.         |
| MenuChangeEvent   | MenuChange.wav | Played when the user changes the current menu item. |
| MenuSelectedEvent | MenuSelect.wav | Played when the user selects a menu item.           |

 

The following example shows how the **Audio::CreateResources** method creates the source voice for the background music. The [XAUDIO2\_SEND\_DESCRIPTOR](/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_send_descriptor) structure defines the target destination voice from another voice and specifies whether a filter should be used. Marble Maze calls the **Audio::SetSoundEffectFilter** method to use the filters to change the sound of the ball as it rolls. The [XAUDIO2\_VOICE\_SENDS](/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_voice_sends) structure defines the set of voices to receive data from a single output voice. Marble Maze sends data from the source voice to the mastering voice (for the dry, or unaltered, portion of a playing sound) and to the two submix voices that implement the wet, or reverberant, portion of a playing sound.

The [IXAudio2::CreateSourceVoice](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2-createsourcevoice) method creates and configures a source voice. It takes a [WAVEFORMATEX](/windows/win32/api/mmeapi/ns-mmeapi-waveformatex) structure that defines the format of the audio buffers that are sent to the voice. As mentioned previously, Marble Maze uses the PCM format.

```cpp
XAUDIO2_SEND_DESCRIPTOR descriptors[3];
descriptors[0].pOutputVoice = m_musicMasteringVoice;
descriptors[0].Flags = 0;
descriptors[1].pOutputVoice = m_musicReverbVoiceSmallRoom;
descriptors[1].Flags = 0;
descriptors[2].pOutputVoice = m_musicReverbVoiceLargeRoom;
descriptors[2].Flags = 0;
XAUDIO2_VOICE_SENDS sends = {0};
sends.SendCount = 3;
sends.pSends = descriptors;
WAVEFORMATEX& waveFormat = m_musicStreamer.GetOutputWaveFormatEx();

DX::ThrowIfFailed(
    m_musicEngine->CreateSourceVoice(&m_musicSourceVoice, &waveFormat, 0, 1.0f, &m_voiceContext, &sends, nullptr)
    );

DX::ThrowIfFailed(
    m_musicMasteringVoice->SetVolume(0.4f)
    );
```

## Playing background music


A source voice is created in the stopped state. Marble Maze starts the background music in the game loop. The first call to **MarbleMazeMain::Update** calls **Audio::Start** to start the background music.

```cpp
if (!m_audio.m_isAudioStarted)
{
    m_audio.Start();
}
```

The **Audio::Start** method calls [IXAudio2SourceVoice::Start](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2sourcevoice-start) to start to process the source voice for the background music.

```cpp
void Audio::Start()
{     
    if (m_engineExperiencedCriticalError)
    {
        return;
    }

    HRESULT hr = m_musicSourceVoice->Start(0);

    if SUCCEEDED(hr) {
        m_isAudioStarted = true;
    }
    else
    {
        m_engineExperiencedCriticalError = true;
    }
}
```

The source voice passes that audio data to the next stage of the audio graph. In the case of Marble Maze, the next stage contains two submix voices that apply the two reverb effects to the audio. One submix voice applies a close late-field reverb; the second applies a far late-field reverb.

The amount that each submix voice contributes to the final mix is determined by the size and shape of the room. The near-field reverb contributes more when the ball is near a wall or in a small room, and the late-field reverb contributes more when the ball is in a large space. This technique produces a more realistic echo effect as the marble moves through the maze. To learn more about how Marble Maze implements this effect, see **Audio::SetRoomSize** and **Physics::CalculateCurrentRoomSize** in the Marble Maze source code.

> [!NOTE]
> In a game in which most room sizes are relatively the same, you can use a more basic reverb model. For example, you can use one reverb setting for all rooms or you can create a predefined reverb setting for each room.

The **Audio::CreateResources** method uses Media Foundation to load the background music. At this point, however, the source voice does not have audio data to work with. In addition, because the background music loops, the source voice must be regularly updated with data so that the music continues to play.

To keep the source voice filled with data, the game loop updates the audio buffers every frame. The **MarbleMazeMain::Render** method calls **Audio::Render** to process the background music audio buffer. The **Audio** class defines an array of three audio buffers, **m\_audioBuffers**. Each buffer holds 64 KB (65536 bytes) of data. The loop reads data from the Media Foundation object and writes that data to the source voice until the source voice has three queued buffers.

> [!CAUTION]
> Although Marble Maze uses a 64 KB buffer to hold music data, you may need to use a larger or smaller buffer. This amount depends on the requirements of your game.

```cpp
// This sample processes audio buffers during the render cycle of the application.
// As long as the sample maintains a high-enough frame rate, this approach should
// not glitch audio. In game code, it is best for audio buffers to be processed
// on a separate thread that is not synced to the main render loop of the game.
void Audio::Render()
{
    if (m_engineExperiencedCriticalError)
    {
        m_engineExperiencedCriticalError = false;
        ReleaseResources();
        Initialize();
        CreateResources();
        Start();
        if (m_engineExperiencedCriticalError)
        {
            return;
        }
    }

    try
    {
        bool streamComplete;
        XAUDIO2_VOICE_STATE state;
        uint32 bufferLength;
        XAUDIO2_BUFFER buf = {0};

        // Use MediaStreamer to stream the buffers.
        m_musicSourceVoice->GetState(&state);
        while (state.BuffersQueued <= MAX_BUFFER_COUNT - 1)
        {
            streamComplete = m_musicStreamer.GetNextBuffer(
                m_audioBuffers[m_currentBuffer],
                STREAMING_BUFFER_SIZE,
                &bufferLength
                );

            if (bufferLength > 0)
            {
                buf.AudioBytes = bufferLength;
                buf.pAudioData = m_audioBuffers[m_currentBuffer];
                buf.Flags = (streamComplete) ? XAUDIO2_END_OF_STREAM : 0;
                buf.pContext = 0;
                DX::ThrowIfFailed(
                    m_musicSourceVoice->SubmitSourceBuffer(&buf)
                    );

                m_currentBuffer++;
                m_currentBuffer %= MAX_BUFFER_COUNT;
            }

            if (streamComplete)
            {
                // Loop the stream.
                m_musicStreamer.Restart();
                break;
            }

            m_musicSourceVoice->GetState(&state);
        }
    }
    catch (...)
    {
        m_engineExperiencedCriticalError = true;
    }
}
```

The loop also handles when the Media Foundation object reaches the end of the stream. In this case, it calls the [IMFSourceReader::SetCurrentPosition](/windows/desktop/api/mfreadwrite/nf-mfreadwrite-imfsourcereader-setcurrentposition) method to reset the position of the audio source.

```cpp
void MediaStreamer::Restart()
{
    if (m_reader == nullptr)
    {
        return;
    }

    PROPVARIANT var = {0};
    var.vt = VT_I8;

    DX::ThrowIfFailed(
        m_reader->SetCurrentPosition(GUID_NULL, var)
        );
}
```

To implement audio looping for a single buffer (or for an entire sound that is fully loaded into memory), you can set the [XAUDIO2_BUFFER](/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_buffer)::LoopCount field to **XAUDIO2\_LOOP\_INFINITE** when you initialize the sound. Marble Maze uses this technique to play the rolling sound for the marble.

```cpp
if (sound == RollingEvent)
{
    m_soundEffects[sound].m_audioBuffer.LoopCount = XAUDIO2_LOOP_INFINITE;
}
```

However, for the background music, Marble Maze manages the buffers directly so that it can better control the amount of memory that is used. When your music files are large, you can stream the music data into smaller buffers. Doing so can help balance memory size with the frequency of the game’s ability to process and stream audio data.

> [!TIP]
> If your game has a low or varying frame rate, processing audio on the main thread can produce unexpected pauses or pops in the audio because the audio engine has insufficient buffered audio data to work with. If your game is sensitive to this issue, consider processing audio on a separate thread that does not perform rendering. This approach is especially useful on computers that have multiple processors because your game can use idle processors.

## Reacting to game events

The **Audio** class provides methods such as **PlaySoundEffect**, **IsSoundEffectStarted**, **StopSoundEffect**, **SetSoundEffectVolume**, **SetSoundEffectPitch**, and **SetSoundEffectFilter** to enable the game to control when sounds play and stop, and to control sound properties such as volume and pitch. For example, if the marble falls off the maze, **MarbleMazeMain::Update** calls the **Audio::PlaySoundEffect** method to play the **FallingEvent** sound.

```cpp
m_audio.PlaySoundEffect(FallingEvent);
```

The **Audio::PlaySoundEffect** method calls the [IXAudio2SourceVoice::Start](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2sourcevoice-start) method to begin playback of the sound. If the **IXAudio2SourceVoice::Start** method has already been called, it is not started again. **Audio::PlaySoundEffect** then performs custom logic for certain sounds.

```cpp
void Audio::PlaySoundEffect(SoundEvent sound)
{
    XAUDIO2_BUFFER buf = {0};
    XAUDIO2_VOICE_STATE state = {0};

    if (m_engineExperiencedCriticalError)
    {
        // If there's an error, then we'll recreate the engine on the next
        // render pass.
        return;
    }

    SoundEffectData* soundEffect = &m_soundEffects[sound];
    HRESULT hr = soundEffect->m_soundEffectSourceVoice->Start();

    if FAILED(hr)
    {
        m_engineExperiencedCriticalError = true;
        return;
    }

    // For one-off voices, submit a new buffer if there's none queued up,
    // and allow up to two collisions to be queued up. 
    if (sound != RollingEvent)
    {
        XAUDIO2_VOICE_STATE state = {0};

        soundEffect->m_soundEffectSourceVoice->
            GetState(&state, XAUDIO2_VOICE_NOSAMPLESPLAYED);

        if (state.BuffersQueued == 0)
        {
            soundEffect->m_soundEffectSourceVoice->
                SubmitSourceBuffer(&soundEffect->m_audioBuffer);
        }
        else if (state.BuffersQueued < 2 && sound == CollisionEvent)
        {
            soundEffect->m_soundEffectSourceVoice->
                SubmitSourceBuffer(&soundEffect->m_audioBuffer);
        }

        // For the menu clicks, we want to stop the voice and replay the click
        // right away.
        // Note that stopping and then flushing could cause a glitch due to the
        // waveform not being at a zero-crossing, but due to the nature of the 
        // sound (fast and 'clicky'), we don't mind.
        if (state.BuffersQueued > 0 && sound == MenuChangeEvent)
        {
            soundEffect->m_soundEffectSourceVoice->Stop();
            soundEffect->m_soundEffectSourceVoice->FlushSourceBuffers();

            soundEffect->m_soundEffectSourceVoice->
                SubmitSourceBuffer(&soundEffect->m_audioBuffer);

            soundEffect->m_soundEffectSourceVoice->Start();
        }
    }

    m_soundEffects[sound].m_soundEffectStarted = true;
}
```

For sounds other than rolling, the **Audio::PlaySoundEffect** method calls [IXAudio2SourceVoice::GetState](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2sourcevoice-getstate) to determine the number of buffers that the source voice is playing. It calls [IXAudio2SourceVoice::SubmitSourceBuffer](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2sourcevoice-submitsourcebuffer) to add the audio data for the sound to the voice’s input queue if no buffers are active. The **Audio::PlaySoundEffect** method also enables the collision sound to be played two times in sequence. This occurs, for example, when the marble collides with a corner of the maze.

As already described, the Audio class uses the **XAUDIO2\_LOOP\_INFINITE** flag when it initializes the sound for the rolling event. The sound starts looped playback the first time that **Audio::PlaySoundEffect** is called for this event. To simplify the playback logic for the rolling sound, Marble Maze mutes the sound instead of stopping it. As the marble changes velocity, Marble Maze changes the pitch and volume of the sound to give it a more realistic effect. The following shows how the **MarbleMazeMain::Update** method updates the pitch and volume of the marble as its velocity changes and how it mutes the sound by setting its volume to zero when the marble stops.

```cpp
// Play the roll sound only if the marble is actually rolling.
if (ci.isRollingOnFloor && volume > 0)
{
    if (!m_audio.IsSoundEffectStarted(RollingEvent))
    {
        m_audio.PlaySoundEffect(RollingEvent);
    }

    // Update the volume and pitch by the velocity.
    m_audio.SetSoundEffectVolume(RollingEvent, volume);
    m_audio.SetSoundEffectPitch(RollingEvent, pitch);

    // The rolling sound has at most 8000Hz sounds, so we linearly
    // ramp up the low-pass filter the faster we go.
    // We also reduce the Q-value of the filter, starting with a
    // relatively broad cutoff and get progressively tighter.
    m_audio.SetSoundEffectFilter(
        RollingEvent,
        600.0f + 8000.0f * volume,
        XAUDIO2_MAX_FILTER_ONEOVERQ - volume*volume
        );
}
else
{
    m_audio.SetSoundEffectVolume(RollingEvent, 0);
}
```

## Reacting to suspend and resume events

[Marble Maze application structure](marble-maze-application-structure.md) describes how Marble Maze supports suspend and resume. When the game is suspended, the game pauses the audio. When the game resumes, the game resumes the audio where it left off. We do so to follow the best practice of not using resources when you know they’re not needed.

The **Audio::SuspendAudio** method is called when the game is suspended. This method calls the [IXAudio2::StopEngine](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2-stopengine) method to stop all audio. Although **IXAudio2::StopEngine** stops all audio output immediately, it preserves the audio graph and its effect parameters (for example, the reverb effect that’s applied when the marble bounces).

```cpp
// Uses the IXAudio2::StopEngine method to stop all audio immediately.  
// It leaves the audio graph untouched, which preserves all effect parameters   
// and effect histories (like reverb effects) voice states, pending buffers,  
// cursor positions and so on. 
// When the engines are restarted, the resulting audio will sound as if it had  
// never been stopped except for the period of silence. 
void Audio::SuspendAudio()
{
    if (m_engineExperiencedCriticalError)
    {
        return;
    }

    if (m_isAudioStarted)
    {
        m_musicEngine->StopEngine();
        m_soundEffectEngine->StopEngine();
    }

    m_isAudioStarted = false;
}
```

The **Audio::ResumeAudio** method is called when the game is resumed. This method uses the [IXAudio2::StartEngine](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2-startengine) method to restart the audio. Because the call to [IXAudio2::StopEngine](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2-stopengine) preserves the audio graph and its effect parameters, the audio output resumes where it left off.

```cpp
// Restarts the audio streams. A call to this method must match a previous call
// to SuspendAudio. This method causes audio to continue where it left off.
// If there is a problem with the restart, the m_engineExperiencedCriticalError
// flag is set. The next call to Render will recreate all the resources and
// reset the audio pipeline.
void Audio::ResumeAudio()
{
    if (m_engineExperiencedCriticalError)
    {
        return;
    }

    HRESULT hr = m_musicEngine->StartEngine();
    HRESULT hr2 = m_soundEffectEngine->StartEngine();

    if (FAILED(hr) || FAILED(hr2))
    {
        m_engineExperiencedCriticalError = true;
    }
}
```

## Handling headphones and device changes

Marble Maze uses engine callbacks to handle XAudio2 engine failures, such as when the audio device changes. A likely cause of a device change is when the game user connects or disconnects the headphones. We recommend that you implement the engine callback that handles device changes. Otherwise, your game will stop playing sound when the user plugs in or removes headphones, until the game is restarted.

**Audio.h** defines the **AudioEngineCallbacks** class. This class implements the [IXAudio2EngineCallback](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2enginecallback) interface.

```cpp
class AudioEngineCallbacks: public IXAudio2EngineCallback
{
private:
    Audio* m_audio;

public :
    AudioEngineCallbacks(){};
    void Initialize(Audio* audio);

    // Called by XAudio2 just before an audio processing pass begins.
    void _stdcall OnProcessingPassStart(){};

    // Called just after an audio processing pass ends.
    void  _stdcall OnProcessingPassEnd(){};

    // Called when a critical system error causes XAudio2
    // to be closed and restarted. The error code is given in Error.
    void  _stdcall OnCriticalError(HRESULT Error);
};
```

The [IXAudio2EngineCallback](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2enginecallback) interface enables your code to be notified when audio processing events occur and when the engine encounters a critical error. To register for callbacks, Marble Maze calls the [IXAudio2::RegisterForCallbacks](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2-registerforcallbacks) method in **Audio::CreateResources**, after it creates the [IXAudio2](/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2) object for the music engine.

```cpp
m_musicEngineCallback.Initialize(this);
m_musicEngine->RegisterForCallbacks(&m_musicEngineCallback);
```

Marble Maze does not require notification when audio processing starts or ends. Therefore, it implements the [IXAudio2EngineCallback::OnProcessingPassStart](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2enginecallback-onprocessingpassstart) and [IXAudio2EngineCallback::OnProcessingPassEnd](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2enginecallback-onprocessingpassend) methods to do nothing. For the [IXAudio2EngineCallback::OnCriticalError](/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2enginecallback-oncriticalerror) method, Marble Maze calls the **SetEngineExperiencedCriticalError** method, which sets the **m\_engineExperiencedCriticalError** flag.

```cpp
// Audio.cpp

// Called when a critical system error causes XAudio2 
// to be closed and restarted. The error code is given in Error. 
void  _stdcall AudioEngineCallbacks::OnCriticalError(HRESULT Error)
{
    m_audio->SetEngineExperiencedCriticalError();
}
```

```cpp
// Audio.h (Audio class)

// This flag can be used to tell when the audio system 
// is experiencing critical errors.
// XAudio2 gives a critical error when the user unplugs
// the headphones and a new speaker configuration is generated.
void SetEngineExperiencedCriticalError()
{
    m_engineExperiencedCriticalError = true;
}
```

When a critical error occurs, audio processing stops and all additional calls to XAudio2 fail. To recover from this situation, you must release the XAudio2 instance and create a new one. The **Audio::Render** method, which is called from the game loop every frame, first checks the **m\_engineExperiencedCriticalError** flag. If this flag is set, it clears the flag, releases the current XAudio2 instance, initializes resources, and then starts the background music.

```cpp
if (m_engineExperiencedCriticalError)
{
    m_engineExperiencedCriticalError = false;
    ReleaseResources();
    Initialize();
    CreateResources();
    Start();
    if (m_engineExperiencedCriticalError)
    {
        return;
    }
}
```

Marble Maze also uses the **m\_engineExperiencedCriticalError** flag to guard against calling into XAudio2 when no audio device is available. For example, the **MarbleMazeMain::Update** method does not process audio for rolling or collision events when this flag is set. The app attempts to repair the audio engine every frame if it is required; however, the **m\_engineExperiencedCriticalError** flag might always be set if the computer does not have an audio device or the headphones are unplugged and there is no other available audio device.

> [!CAUTION]
> As a rule, do not perform blocking operations in the body of an engine callback. Doing so can cause performance issues. Marble Maze sets a flag in the **OnCriticalError** callback and later handles the error during the regular audio processing phase. For more information about XAudio2 callbacks, see [XAudio2 Callbacks](/windows/desktop/xaudio2/xaudio2-callbacks).

## Conclusion

That wraps up the Marble Maze game sample! Though it is a relatively simple game, it contains many of the important parts that go into any UWP DirectX game, and is a good example to follow when making your own game.

Now that you've finished following along, try tinkering around with the source code and seeing what happens. Or check out [Create a simple UWP game with DirectX](tutorial--create-your-first-uwp-directx-game.md), another UWP DirectX game sample.

Ready to go further with DirectX? Then check out our guides at [DirectX programming](directx-programming.md).

If you're interested in game development on UWP in general, see the documentation at [Game programming](index.md).

## Related topics

* [Adding input and interactivity to the Marble Maze sample](adding-input-and-interactivity-to-the-marble-maze-sample.md)
* [Developing Marble Maze, a UWP game in C++ and DirectX](developing-marble-maze-a-windows-store-game-in-cpp-and-directx.md)