---
title: Add sound
description: Develop a simple sound engine using XAudio2 APIs to playback game music and sound effects.
ms.assetid: aa05efe2-2baa-8b9f-7418-23f5b6cd2266
ms.date: 10/24/2017
ms.topic: article
keywords: windows 10, uwp, games, sound
ms.localizationpriority: medium
---
# Add sound

In this topic, we create a simple sound engine using [XAudio2](https://docs.microsoft.com/windows/desktop/xaudio2/xaudio2-introduction) APIs. If you are new to __XAudio2__, we have included a short intro under [Audio concepts](#audio-concepts).

>[!Note]
>If you haven't downloaded the latest game code for this sample, go to [Direct3D game sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Simple3DGameDX). This sample is part of a large collection of UWP feature samples. For instructions on how to download the sample, see [Get the UWP samples from GitHub](https://docs.microsoft.com/windows/uwp/get-started/get-uwp-app-samples).

## Objective

Add sounds into the sample game using [XAudio2](/windows/desktop/xaudio2/xaudio2-introduction).

## Define the audio engine

In the game sample, the audio objects and behaviors are defined in three files:

* __[Audio.h](#audioh)/.cpp__: Defines the __Audio__ object, which contains the __XAudio2__ resources for sound playback. It also defines the method for suspending and resuming audio playback if the game is paused or deactivated.
* __[MediaReader.h](#mediareaderh)/.cpp__: Defines the methods for reading audio .wav files from local storage.
* __[SoundEffect.h](#soundeffecth)/.cpp__: Defines an object for in-game sound playback.

## Overview

There are three main parts in getting set up for audio playback into your game.

1. [Create and initialize the audio resources](#create-and-initialize-the-audio-resources)
2. [Load audio file](#load-audio-file)
3. [Associate sound to object](#associate-sound-to-object)

They are all defined in the [Simple3DGame::Initialize](#simple3dgameinitialize-method) method. So let's first examine this method and then dive into more details in each of the sections.

After setting up, we learn how to trigger the sound effects to play. For more info, go to [Play the sound](#play-the-sound).

### Simple3DGame::Initialize method

In __Simple3DGame::Initialize__, where __m\_controller__ and __m\_renderer__ are also initialized, we set up the audio engine and get it ready to play sounds.

 * Create __m\_audioController__, which is an instance of the [Audio](#audioh) class.
 * Create the audio resources needed using the [Audio::CreateDeviceIndependentResources](#audiocreatedeviceindependentresources-method) method. Here, two __XAudio2__ objects &mdash; a music engine object and a sound engine object, and a mastering voice for each of them were created. The music engine object can be used to play background music for your game. The sound engine can be used to play sound effects in your game. For more info, see [Create and initialize the audio resources](#create-and-initialize-the-audio-resources).
 * Create __mediaReader__, which is an instance of [MediaReader](#mediareaderh) class. [MediaReader](#mediareaderh), which is a helper class for the [SoundEffect](#soundeffecth) class, reads small audio files synchronously from file location and returns sound data as a byte array.
 * Use [MediaReader::LoadMedia](#mediareaderloadmedia-method) to load sound files from its location and create a __targetHitSound__ variable to hold the loaded .wav sound data. For more info, see [Load audio file](#load-audio-file). 

Sound effects are associated with the game object. So when a collision occurs with that game object, it triggers the sound effect to be played. In this game sample, we have sound effects for the ammo (what we use to shoot targets with) and for the target. 
    
* In the __GameObject__ class, there's a __HitSound__ property that is used to associate the sound effect to the object.
* Create a new instance of the [SoundEffect](#soundeffecth) class and initialize it. During initialization, a source voice for the sound effect is created. 
* This class plays a sound using a mastering voice provided from the [Audio](#audioh) class. Sound data is read from file location using the [MediaReader](#mediareaderh) class. For more info, see [Associate sound to object](#associate-sound-to-object).

>[!Note]
>The actual trigger to play the sound is determined by the movement and collision of these game objects. Hence, the call to actually play these sounds are defined in the [Simple3DGame::UpdateDynamics](#simple3dgameupdatedynamics-method) method. For more info, go to [Play the sound](#play-the-sound).

```cpp
void Simple3DGame::Initialize(
    _In_ MoveLookController^ controller,
    _In_ GameRenderer^ renderer
    )
{
    // ...
    // Create a new Audio class object.
    m_audioController = ref new Audio;

    // Create the audio resources needed.
    // Two XAudio2 objects are created - one for music engine,
    // the other for sound engine. A mastering voice is also
    // created for each of the objects.
    m_audioController->CreateDeviceIndependentResources();

    m_ammo = std::vector<Sphere^>(GameConstants::MaxAmmo);

    // ..
    // Create a media reader which is used to read audio files from its file location.
    MediaReader^ mediaReader = ref new MediaReader;
    auto targetHitSound = mediaReader->LoadMedia("Assets\\hit.wav");

    // Instantiate the targets for use in the game.
    // Each target has a different initial position, size, and orientation.
    // But share a common set of material properties.
    for (int a = 1; a < GameConstants::MaxTargets; a++)
    {
        // ...
        // Create a new sound effect object and associate it
        // with the game object's (target) HitSound property.
        target->HitSound(ref new SoundEffect());

        // Initialize the sound effect object with
        // the sound effect engine, format of the audio wave, and audio data
        // During initialization, source voice of this sound effect is also created.
        target->HitSound()->Initialize(
            m_audioController->SoundEffectEngine(),
            mediaReader->GetOutputWaveFormatEx(),
            targetHitSound
            );
        // ...
    }

    // Instantiate a set of spheres to be used as ammunition for the game
    // and set the material properties of the spheres.
    auto ammoHitSound = mediaReader->LoadMedia("Assets\\bounce.wav");

    for (int a = 0; a < GameConstants::MaxAmmo; a++)
    {
        m_ammo[a] = ref new Sphere;
        m_ammo[a]->Radius(GameConstants::AmmoRadius);
        m_ammo[a]->HitSound(ref new SoundEffect());
        m_ammo[a]->HitSound()->Initialize(
            m_audioController->SoundEffectEngine(),
            mediaReader->GetOutputWaveFormatEx(),
            ammoHitSound
            );
        m_ammo[a]->Active(false);
        m_renderObjects.push_back(m_ammo[a]);
    }
    // ...
}
```

## Create and initialize the audio resources

* Use [XAudio2Create](https://docs.microsoft.com/windows/desktop/api/xaudio2/nf-xaudio2-xaudio2create), an XAudio2 API, to create two new XAudio2 objects which define the music and sound effect engines. This method returns a pointer to the object's [IXAudio2](https://docs.microsoft.com/windows/desktop/api/xaudio2/nn-xaudio2-ixaudio2) interface that manages all audio engine states, the audio processing thread, the voice graph, and more.
* After the engines have been instantiated, use [IXAudio2::CreateMasteringVoice](https://docs.microsoft.com/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2-createmasteringvoice) to create a mastering voice for each of the sound engine objects.

For more info, go to [How to: Initialize XAudio2](https://docs.microsoft.com/windows/desktop/xaudio2/how-to--initialize-xaudio2).

### Audio::CreateDeviceIndependentResources method

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

## Load audio file

In the game sample, the code for reading audio format files is defined in [MediaReader.h](#mediareaderh)/cpp__.  To read an encoded .wav audio file, call [MediaReader::LoadMedia](#mediareaderloadmedia-method), passing in the filename of the .wav as the input parameter.

### MediaReader::LoadMedia method

This method uses the [Media Foundation](https://docs.microsoft.com/windows/desktop/medfound/microsoft-media-foundation-sdk) APIs to read in the .wav audio file as a Pulse Code Modulation (PCM) buffer.

#### Set up the Source Reader

1. Use [MFCreateSourceReaderFromURL](https://docs.microsoft.com/windows/desktop/api/mfreadwrite/nf-mfreadwrite-mfcreatesourcereaderfromurl) to create a media source reader ([IMFSourceReader](https://docs.microsoft.com/windows/desktop/api/mfreadwrite/nn-mfreadwrite-imfsourcereader)).
2. Use [MFCreateMediaType](https://docs.microsoft.com/windows/desktop/api/mfapi/nf-mfapi-mfcreatemediatype) to create a media type ([IMFMediaType](https://docs.microsoft.com/windows/desktop/api/mfobjects/nn-mfobjects-imfmediatype)) object (_mediaType_). It represents a description of a media format. 
3. Specify that the _mediaType_'s decoded output is PCM audio, which is an audio type that __XAudio2__ can use.
4. Sets the decoded output media type for the source reader by calling [IMFSourceReader::SetCurrentMediaType](https://docs.microsoft.com/windows/desktop/api/mfreadwrite/nf-mfreadwrite-imfsourcereader-setcurrentmediatype).

For more info on why we use the Source Reader, go to [Source Reader](https://docs.microsoft.com/windows/desktop/medfound/source-reader).

#### Describe the data format of the audio stream

1. Use [IMFSourceReader::GetCurrentMediaType](https://docs.microsoft.com/windows/desktop/api/mfreadwrite/nf-mfreadwrite-imfsourcereader-getcurrentmediatype) to get the current media type for the stream.
2. Use [IMFMediaType::MFCreateWaveFormatExFromMFMediaType](https://docs.microsoft.com/windows/desktop/api/mfapi/nf-mfapi-mfcreatewaveformatexfrommfmediatype) to convert the current audio media type to a [WAVEFORMATEX](https://docs.microsoft.com/windows/desktop/api/mmreg/ns-mmreg-twaveformatex) buffer, using the results of the earlier operation as input. This structure specifies the data format of the wave audio stream that is used after audio is loaded. 

The __WAVEFORMATEX__ format can be used to describe the PCM buffer. As compared to the [WAVEFORMATEXTENSIBLE](https://docs.microsoft.com/windows-hardware/drivers/ddi/content/ksmedia/ns-ksmedia-waveformatextensible) structure, it can only be used to describe a subset of audio wave formats. For more info about the differences between __WAVEFORMATEX__ and __WAVEFORMATEXTENSIBLE__, see [Extensible Wave-Format Descriptors](https://docs.microsoft.com/windows-hardware/drivers/audio/extensible-wave-format-descriptors).

#### Read the audio stream

1.  Get the duration, in seconds, of the audio stream by calling [IMFSourceReader::GetPresentationAttribute](https://docs.microsoft.com/windows/desktop/api/mfreadwrite/nf-mfreadwrite-imfsourcereader-getpresentationattribute) and then converts the duration to bytes.
2.  Read the audio file in as a stream by calling [IMFSourceReader::ReadSample](https://docs.microsoft.com/windows/desktop/api/mfreadwrite/nf-mfreadwrite-imfsourcereader-readsample). __ReadSample__ reads the next sample from the media source.
3.  Use [IMFSample::ConvertToContiguousBuffer](https://docs.microsoft.com/windows/desktop/api/mfobjects/nf-mfobjects-imfsample-converttocontiguousbuffer) to copy contents of the audio sample buffer (_sample_) into an array (_mediaBuffer_).

```cpp
Platform::Array<byte>^ MediaReader::LoadMedia(_In_ Platform::String^ filename)
{
    DX::ThrowIfFailed(
        MFStartup(MF_VERSION)
        );

    // Creates a media source reader.
    ComPtr<IMFSourceReader> reader;
    DX::ThrowIfFailed(
        MFCreateSourceReaderFromURL(
            Platform::String::Concat(m_installedLocationPath, filename)->Data(),
            nullptr,
            &reader
            )
        );

    // Set the decoded output format as PCM.
    // XAudio2 on Windows can process PCM and ADPCM-encoded buffers.
    // When using MediaFoundation, this sample always decodes into PCM.
    Microsoft::WRL::ComPtr<IMFMediaType> mediaType;
    DX::ThrowIfFailed(
        MFCreateMediaType(&mediaType)
        );

    // Define the major category of the media as audio. For more info about major media types,
    // go to: https://msdn.microsoft.com/library/windows/desktop/aa367377.aspx
    DX::ThrowIfFailed(
        mediaType->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio)
        );

    // Define the sub-type of the media as uncompressed PCM audio. For more info about audio sub-types,
    // go to: https://msdn.microsoft.com/library/windows/desktop/aa372553.aspx
    DX::ThrowIfFailed(
        mediaType->SetGUID(MF_MT_SUBTYPE, MFAudioFormat_PCM)
        );
    
    // Sets the media type for a stream. This media type defines that format that the Source Reader 
    // produces as output. It can differ from the native format provided by the media source.
    // For more info, go to https://msdn.microsoft.com/library/windows/desktop/dd374667.aspx
    DX::ThrowIfFailed(
        reader->SetCurrentMediaType(static_cast<uint32>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), 0, mediaType.Get())
        );

    // Get the current media type for the stream.
    // For more info, go to:
    // https://msdn.microsoft.com/library/windows/desktop/dd374660.aspx
        Microsoft::WRL::ComPtr<IMFMediaType> outputMediaType;
    DX::ThrowIfFailed(
        reader->GetCurrentMediaType(static_cast<uint32>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), &outputMediaType)
        );

    // Converts the current media type into the WaveFormatEx buffer structure.
    UINT32 size = 0;
    WAVEFORMATEX* waveFormat;
    DX::ThrowIfFailed(
        MFCreateWaveFormatExFromMFMediaType(outputMediaType.Get(), &waveFormat, &size)
        );

    // Copies the waveFormat's block of memory to the starting address of the m_waveFormat variable in MediaReader.
    // Then free the waveFormat memory block.
    // For more info, go to https://msdn.microsoft.com/library/windows/desktop/aa366535.aspx and
    // https://msdn.microsoft.com/library/windows/desktop/ms680722.aspx
    CopyMemory(&m_waveFormat, waveFormat, sizeof(m_waveFormat));
    CoTaskMemFree(waveFormat);

    PROPVARIANT propVariant;
    DX::ThrowIfFailed(
        reader->GetPresentationAttribute(static_cast<uint32>(MF_SOURCE_READER_MEDIASOURCE), MF_PD_DURATION, &propVariant)
        );

    // 'duration' is in 100ns units; convert to seconds, and round up
    // to the nearest whole byte.
    LONGLONG duration = propVariant.uhVal.QuadPart;
    unsigned int maxStreamLengthInBytes =
        static_cast<unsigned int>(
            ((duration * static_cast<ULONGLONG>(m_waveFormat.nAvgBytesPerSec)) + 10000000) /
            10000000
            );

    Platform::Array<byte>^ fileData = ref new Platform::Array<byte>(maxStreamLengthInBytes);

    ComPtr<IMFSample> sample;
    ComPtr<IMFMediaBuffer> mediaBuffer;
    DWORD flags = 0;

    int positionInData = 0;
    bool done = false;
    while (!done)
    {
        //...
        // Read audio data.
    }

    return fileData;
}
```
## Associate sound to object

Associating sounds to the object takes place when the game initializes, in the [Simple3DGame::Initialize](#simple3dgameinitialize-method) method.

Recap:
* In the __GameObject__ class, there's a __HitSound__ property that is used to associate the sound effect to the object.
* Create a new instance of the [SoundEffect](#soundeffecth) class object and associate it with the game object. This class plays a sound using __XAudio2__ APIs.  It uses a mastering voice provided by the [Audio](#audioh) class. The sound data can be read from file location using the [MediaReader](#mediareaderh) class.

[SoundEffect::Initialize](#soundeffectinitialize-method) is used to initalize the __SoundEffect__ instance with the following input parameters: pointer to sound engine object (IXAudio2 objects created in the [Audio::CreateDeviceIndependentResources](#audiocreatedeviceindependentresources-method) method), pointer to format of the .wav file using __MediaReader::GetOutputWaveFormatEx__, and the sound data loaded using [MediaReader::LoadMedia](#mediareaderloadmedia-method) method. During initialization, the source voice for the sound effect is also created.

### SoundEffect::Initialize method

```cpp
void SoundEffect::Initialize(
    _In_ IXAudio2 *masteringEngine,
    _In_ WAVEFORMATEX *sourceFormat,
    _In_ Platform::Array<byte>^ soundData)
{
    m_soundData = soundData;

    if (masteringEngine == nullptr)
    {
        // Audio is not available so just return.
        m_audioAvailable = false;
        return;
    }

    // Create a source voice for this sound effect.
    DX::ThrowIfFailed(
        masteringEngine->CreateSourceVoice(
            &m_sourceVoice,
            sourceFormat
            )
        );
    m_audioAvailable = true;
}
```

## Play the sound

Triggers to play sound effects are defined in [Simple3DGame::UpdateDynamics](#simple3dgameupdatedynamics-method) method because this is where movement of the objects are updated and collision between objects is determined.

Since interaction of between objects differs greatly, depending on the game, we are not going to discuss the dynamics of the game objects here. If you're interested to understand its implementation, go to [Simple3DGame::UpdateDynamics](#simple3dgameupdatedynamics-method) method.

In principle, when a collision occurs, it triggers the sound effect to play by calling **SoundEffect::PlaySound**. This method stops any sound effects that's currently playing and queues the in-memory buffer with the desired sound data. It uses source voice to set the volume, submit sound data, and start the playback.

### SoundEffect::PlaySound method

* Uses the source voice object **m\_sourceVoice** to start the playback of the sound data buffer **m\_soundData**
* Creates an [XAUDIO2\_BUFFER](https://docs.microsoft.com/windows/desktop/api/xaudio2/ns-xaudio2-xaudio2_buffer), to which it provides a reference to the sound data buffer, and then submits it with a call to [IXAudio2SourceVoice::SubmitSourceBuffer](https://docs.microsoft.com/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2sourcevoice-submitsourcebuffer). 
* With the sound data queued up, **SoundEffect::PlaySound** starts play back by calling [IXAudio2SourceVoice::Start](https://docs.microsoft.com/windows/desktop/api/xaudio2/nf-xaudio2-ixaudio2sourcevoice-start).

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

### Simple3DGame::UpdateDynamics method

The __Simple3DGame::UpdateDynamics__ method takes care the interaction and collision between game objects. When objects collide (or intersect), it triggers the associated sound effect to play.

```cpp
void Simple3DGame::UpdateDynamics()
{
    // ...
    // Check for collisions between ammo.
#pragma region inter-ammo collision detection
    if (m_ammoCount > 1)
    {
       // ...
       // Check collision between instances One and Two.
       // ...
       if (distanceSquared < (GameConstants::AmmoSize * GameConstants::AmmoSize))
       {
           // The two ammo are intersecting.
           // ...
           // Start playing the sounds for the impact between the two balls.
              m_ammo[one]->PlaySound(impact, m_player->Position());
              m_ammo[two]->PlaySound(impact, m_player->Position());

       }
    }
#pragma endregion

#pragma region Ammo-Object intersections
    // Check for intersections between the ammo and the other objects in the scene.
    // ...
    // Ball is in contact with Object.
    // ...

    // Make sure that the ball is actually headed towards the object. At grazing angles there
    // could appear to be an impact when the ball is actually already hit and moving away.
    if (impact > 0.0f)
    {
        // ...
        // Play the sound associated with the Ammo hitting something.
           m_ammo[one]->PlaySound(impact, m_player->Position());

        if (m_objects[i]->Target() && !m_objects[i]->Hit())
        {
            // The object is a target and isn't currently hit, so mark it as hit and
            // play the sound associated with the impact.
             m_objects[i]->Hit(true);
             m_objects[i]->HitTime(timeTotal);
             m_totalHits++;

             m_objects[i]->PlaySound(impact, m_player->Position());
        }
        // ...
    }
#pragma endregion

#pragma region Apply Gravity and world intersection
    // Apply gravity and check for collision against enclosing volume.
    // ...
    if (position.z < limit)
    {
        // The ammo instance hit the a wall in the min Z direction.
        // Align the ammo instance to the wall, invert the Z component of the velocity and
        // play the impact sound.
           position.z = limit;
           m_ammo[i]->PlaySound(-velocity.z, m_player->Position());
           velocity.z = -velocity.z * GameConstants::Physics::GroundRestitution;
    }
    // ...
#pragma endregion
}
```
## Next steps

We have covered the UWP framework, graphics, controls, user interface, and audio of a Windows 10 game. The next part of this tutorial, [Extending the game sample](tutorial-resources.md), explains other options that can be used when developing a game.

## Audio concepts

For Windows 10 games development, use XAudio2 version 2.9. This version is shipped with Windows 10. For more info, go to [XAudio2 Versions](https://docs.microsoft.com/windows/desktop/xaudio2/xaudio2-versions).

__AudioX2__ is a low-level API that provides signal processing and mixing foundation. For more info, see [XAudio2 Key Concepts](https://docs.microsoft.com/windows/desktop/xaudio2/xaudio2-key-concepts).

### XAudio2 voices

There are three types of XAudio2 voice objects: source, submix, and mastering voices. Voices are the objects XAudio2 use to process, to manipulate, and to play audio data. 
* Source voices operate on audio data provided by the client. 
* Source and submix voices send their output to one or more submix or mastering voices. 
* Submix and mastering voices mix the audio from all voices feeding them, and operate on the result. 
* Mastering voices receive data from source voices and submix voices, and sends that data to the audio hardware.

For more info, go to [XAudio2 voices](/windows/desktop/xaudio2/xaudio2-voices).

### Audio graph

Audio graph is a collection of [XAudio2 voices](/windows/desktop/xaudio2/xaudio2-voices). Audio starts at one side of an audio graph in source voices, optionally passes through one or more submix voices, and ends at a mastering voice. An audio graph will contain a source voice for each sound currently playing, zero or more submix voices, and one mastering voice. The simplest audio graph, and the minimum needed to make a noise in XAudio2, is a single source voice outputting directly to a mastering voice. For more info, go to [Audio graphs](https://docs.microsoft.com/windows/desktop/xaudio2/audio-graphs).

### Additional reading

* [How to: Initialize XAudio2](https://docs.microsoft.com/windows/desktop/xaudio2/how-to--initialize-xaudio2)
* [How to: Load Audio Data Files in XAudio2](https://docs.microsoft.com/windows/desktop/xaudio2/how-to--load-audio-data-files-in-xaudio2)
* [How to: Play a Sound with XAudio2](https://docs.microsoft.com/windows/desktop/xaudio2/how-to--play-a-sound-with-xaudio2)

## Key audio .h files

### Audio.h

```cpp
// Audio:
// This class uses XAudio2 to provide sound output.  It creates two
// engines - one for music and the other for sound effects - each as
// a separate mastering voice.
// The SuspendAudio and ResumeAudio methods can be used to stop
// and start all audio playback.
public:
    Audio();

    void Initialize();
    void CreateDeviceIndependentResources();
    IXAudio2* MusicEngine();
    IXAudio2* SoundEffectEngine();
    void SuspendAudio();
    void ResumeAudio();

protected:
    // ...
};
```
### MediaReader.h

```cpp
// MediaReader:
// This is a helper class for the SoundEffect class.  It reads small audio files
// synchronously from the package installed folder and returns sound data as a
// byte array.

ref class MediaReader
{
internal:
    MediaReader();

    Platform::Array<byte>^          LoadMedia(_In_ Platform::String^ filename);
    WAVEFORMATEX*                   GetOutputWaveFormatEx();

protected private:
    Windows::Storage::StorageFolder^ m_installedLocation;
    Platform::String^               m_installedLocationPath;
    WAVEFORMATEX                    m_waveFormat;
};
```
### SoundEffect.h

```cpp
// SoundEffect:
// This class plays a sound using XAudio2.  It uses a mastering voice provided
// from the Audio class.  The sound data can be read from disk using the MediaReader
// class.

ref class SoundEffect
{
internal:
    SoundEffect();

    void Initialize(
        _In_ IXAudio2*              masteringEngine,
        _In_ WAVEFORMATEX*          sourceFormat,
        _In_ Platform::Array<byte>^ soundData
        );

    void PlaySound(_In_ float volume);

protected private:
    //...

};
```


