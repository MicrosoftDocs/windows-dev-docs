---
title: Custom audio effects
description: Learn how to create a Windows Runtime component that implements the IBasicAudioEffect interface to create custom audio effects for your app.
ms.topic: how-to
ms.date: 07/08/2026
author: GrantMeStrength
ms.author: jken
---

# Custom audio effects

This article describes how to create a Windows Runtime component that implements the [IBasicAudioEffect](/uwp/api/Windows.Media.Effects.IBasicAudioEffect) interface to create custom effects for audio streams. You can use custom effects with [AudioGraph](/uwp/api/Windows.Media.Audio.AudioGraph), [MediaCapture](/uwp/api/Windows.Media.Capture.MediaCapture), and [MediaComposition](/uwp/api/Windows.Media.Editing.MediaComposition).

> [!NOTE]
> The `IBasicAudioEffect` interface is a Windows Runtime API in the `Windows.Media.Effects` namespace. These APIs work the same way in WinUI 3 desktop apps as in UWP. The main difference is in how you set up the project and reference the effect component.

## Add a Windows Runtime component for your audio effect

In Visual Studio, add a **Windows Runtime Component (C#)** project to your WinUI 3 solution:

1. Right-click your solution in **Solution Explorer** and select **Add** > **New Project**.
2. Select the **Windows Runtime Component** project template.
3. Name the project *AudioEffectComponent*.
4. Add a project reference from your main WinUI 3 app to this component project.
5. Rename the default class file to *ExampleAudioEffect.cs*.

## Implement the IBasicAudioEffect interface

Your audio effect must implement all methods and properties of the [IBasicAudioEffect](/uwp/api/Windows.Media.Effects.IBasicAudioEffect) interface.

```csharp
using System.Collections.Generic;
using Windows.Media;
using Windows.Media.Effects;
using Windows.Media.MediaProperties;
using Windows.Foundation.Collections;

namespace AudioEffectComponent;

public sealed class ExampleAudioEffect : IBasicAudioEffect
{
    private AudioEncodingProperties _currentEncodingProperties;
    private IPropertySet _propertySet;
    private float _echoDelay = 0.5f; // delay in seconds
    private float _echoMix = 0.5f;   // mix level (0-1)

    public bool UseInputFrameForOutput => true;

    public IReadOnlyList<AudioEncodingProperties> SupportedEncodingProperties
    {
        get
        {
            var supportedProperties = new List<AudioEncodingProperties>();

            AudioEncodingProperties encodingProps1 =
                AudioEncodingProperties.CreatePcm(44100, 1, 32);
            encodingProps1.Subtype = MediaEncodingSubtypes.Float;
            supportedProperties.Add(encodingProps1);

            AudioEncodingProperties encodingProps2 =
                AudioEncodingProperties.CreatePcm(48000, 1, 32);
            encodingProps2.Subtype = MediaEncodingSubtypes.Float;
            supportedProperties.Add(encodingProps2);

            return supportedProperties;
        }
    }

    public void SetEncodingProperties(
        AudioEncodingProperties encodingProperties)
    {
        _currentEncodingProperties = encodingProperties;
    }

    public void SetProperties(IPropertySet configuration)
    {
        _propertySet = configuration;

        // Read custom properties if provided
        if (configuration != null &&
            configuration.TryGetValue("EchoDelay", out object delay))
        {
            _echoDelay = (float)delay;
        }

        if (configuration != null &&
            configuration.TryGetValue("EchoMix", out object mix))
        {
            _echoMix = (float)mix;
        }
    }

    public void ProcessFrame(ProcessAudioFrameContext context)
    {
        // Audio processing logic goes here.
        // See the echo effect example below.
    }

    public void Close(MediaEffectClosedReason reason)
    {
        // Clean up resources
    }

    public void DiscardQueuedFrames()
    {
        // Discard any cached frames
    }
}
```

## Create an echo effect

The following example shows a `ProcessFrame` implementation that creates an echo effect. The effect stores previous audio data in a buffer and mixes it with the current audio data after a configurable delay.

To access the raw audio buffer, you need the [IMemoryBufferByteAccess](/windows/win32/api/windows.foundation/nn-windows-foundation-imemorybufferbyteaccess) COM interface. Add the following definition in your effect namespace:

```csharp
using System.Runtime.InteropServices;

[ComImport]
[System.Runtime.InteropServices.Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
unsafe interface IMemoryBufferByteAccess
{
    void GetBuffer(out byte* buffer, out uint capacity);
}
```

> [!IMPORTANT]
> This technique accesses a native, unmanaged audio buffer. You must add `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>` to your project's `.csproj` file to enable unsafe code.

```csharp
private float[] _echoBuffer;
private int _echoBufferPosition;
private float _echoDelay = 0.5f; // delay in seconds
private float _echoMix = 0.5f;   // mix level (0-1)
private Windows.Media.MediaProperties.AudioEncodingProperties _currentEncodingProperties;

public unsafe void ProcessFrame(ProcessAudioFrameContext context)
{
    AudioFrame inputFrame = context.InputFrame;

    using (AudioBuffer inputBuffer =
        inputFrame.LockBuffer(AudioBufferAccessMode.ReadWrite))
    using (IMemoryBufferReference inputReference =
        inputBuffer.CreateReference())
    {
        byte* dataInBytes;
        uint capacityInBytes;

        ((IMemoryBufferByteAccess)inputReference).GetBuffer(
            out dataInBytes, out capacityInBytes);

        float* dataInFloat = (float*)dataInBytes;
        int dataInFloatLength =
            (int)capacityInBytes / sizeof(float);

        // Initialize echo buffer on first call
        if (_echoBuffer == null)
        {
            int delaySamples = (int)(
                _currentEncodingProperties.SampleRate * _echoDelay);
            _echoBuffer = new float[delaySamples];
        }

        for (int i = 0; i < dataInFloatLength; i++)
        {
            float input = dataInFloat[i];
            float echo = _echoBuffer[_echoBufferPosition];

            // Mix the original signal with the echo
            dataInFloat[i] = input + (echo * _echoMix);

            // Store the current sample in the echo buffer
            _echoBuffer[_echoBufferPosition] = input;
            _echoBufferPosition =
                (_echoBufferPosition + 1) % _echoBuffer.Length;
        }
    }
}
```

## Add the effect to an audio graph

To use the custom effect with an [AudioGraph](/uwp/api/Windows.Media.Audio.AudioGraph), create an instance of the effect definition and add it to a node:

```csharp
using Windows.Media.Audio;
using Windows.Media.Effects;

var effectDefinition = new AudioEffectDefinition(
    typeof(AudioEffectComponent.ExampleAudioEffect).FullName);

audioFileInputNode.EffectDefinitions.Add(effectDefinition);
```

## Pass configuration to the effect

You can pass configuration values to your effect by using a [PropertySet](/uwp/api/Windows.Foundation.Collections.PropertySet):

```csharp
var properties = new PropertySet();
properties["EchoDelay"] = 0.3f;
properties["EchoMix"] = 0.6f;

var effectDefinition = new AudioEffectDefinition(
    typeof(AudioEffectComponent.ExampleAudioEffect).FullName,
    properties);
```

In your effect's `SetProperties` method (shown earlier), read these values from the property set to configure the effect at runtime.

## Add the effect to MediaCapture

You can also add the custom audio effect to a [MediaCapture](/uwp/api/Windows.Media.Capture.MediaCapture) audio stream:

```csharp
var effectDefinition = new AudioEffectDefinition(
    typeof(AudioEffectComponent.ExampleAudioEffect).FullName);

await _mediaCapture.AddAudioEffectAsync(effectDefinition);
```

## Related content

- [Custom video effects](../media-authoring-processing/custom-video-effects.md)
- [Camera](../camera/camera.md)
- [Audio graphs](/windows/apps/develop/media-authoring-processing/audio-graphs)
