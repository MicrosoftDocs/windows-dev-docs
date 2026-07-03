---
description: Learn how to customize your WinUI app for interaction with specific types of inputs including pen, touch, keyboard, and speech.
title: Input and interactions
author: GrantMeStrength
ms.author: jken
ms.date: 07/02/2026
ms.topic: overview
---

# Input and interactions

WinUI apps automatically handle a wide variety of inputs and run on a variety of devices—there's nothing extra you need to do to enable touch input, for example. But there are times when you might want to optimize your app for certain types of input or devices. For example, if you're creating a painting app, you might want to customize the way you handle pen input.

The design and coding instructions in this section help you customize your WinUI app for specific types of inputs.

## Using Windows Runtime input APIs in WinUI 3 desktop apps

WinUI 3 desktop apps can use most [Windows Runtime (WinRT) APIs](/uwp/api/) directly, including many input-related APIs from the `Windows.Devices.Input`, `Windows.Gaming.Input`, and `Windows.UI.Input` namespaces. You don't need to build a UWP app to access these capabilities.

There are a few things to be aware of:

- **Works in any WinUI 3 app** — Pointer properties (pressure, tilt, barrel button), device detection, gamepad hardware access (`Windows.Gaming.Input`), and touch/mouse input all work with no special configuration.
- **Requires package identity (MSIX)** — Some APIs, such as [SpeechRecognizer](/uwp/api/windows.media.speechrecognition.speechrecognizer) and [SpeechSynthesizer](/uwp/api/windows.media.speechsynthesis.speechsynthesizer), require your app to have [package identity](/windows/apps/desktop/modernize/modernize-packaged-apps). The default WinUI 3 project template uses MSIX packaging, so this requirement is met automatically for most apps.
- **UWP-only** — APIs that depend on [ApplicationView](/uwp/api/windows.ui.viewmanagement.applicationview), [CoreWindow](/uwp/api/windows.ui.core.corewindow), or the `GetForCurrentView` pattern aren't available in desktop apps. This includes gaze tracking ([GazeInputSourcePreview](/uwp/api/windows.devices.input.preview.gazeinputsourcepreview)) and input injection ([InputInjector](/uwp/api/windows.ui.input.preview.injection.inputinjector)). For these features, the UWP reference documentation remains the best resource.

For the complete list of supported and unsupported WinRT APIs in desktop apps, see [Windows Runtime APIs supported in desktop apps](/windows/apps/desktop/modernize/winrt-api-desktop-app-support).

> [!TIP]
> Topics in this section that link to UWP documentation cover features that use UWP-only APIs. You can still reference these topics for design guidance and concepts, even though the APIs aren't directly available in WinUI 3 desktop apps.

## Get started

| Topic | Description |
| --- | --- |
| [Input primer](input-primer.md) | Familiarize yourself with each input device type and its behaviors, capabilities, and limitations when paired with certain form factors. |
| [Gaze input](/windows/uwp/design/input/gaze-interactions) | Track a user's gaze based on the location and movement of their eyes and head. Gaze APIs require UWP; see the linked UWP reference. |

## Input devices

| Topic | Description |
| --- | --- |
| [Identify input devices](identify-input-devices.md) | Identify the input devices connected to a Windows app device and identify their capabilities and attributes. |
| [Pointer](handle-pointer-input.md) | Receive, process, and manage input data from pointing devices such as touch, mouse, pen/stylus, and touchpad. |
| [Pen and Windows Ink](pen-and-stylus-interactions.md) | Handle pen-specific input properties such as pressure, tilt, barrel button, and eraser detection. |
| [Touch](touch-interactions.md) | Handle touch input, enabling immersive experiences that your users can explore with confidence. |
| [Mouse](mouse-interactions.md) | Handle mouse input in your app, including button clicks, scroll wheel, and pointer movement. |
| [Keyboard](keyboard-interactions.md) | Handle keyboard input, an essential part of the interaction experience and indispensable for accessibility. |
| [Gamepad and remote control](/windows/uwp/design/input/gamepad-and-remote-interactions) | Design for Xbox gamepad and remote control input. The [Windows.Gaming.Input](/uwp/api/windows.gaming.input) APIs work directly in WinUI 3 desktop apps; the linked UWP topic covers 10-foot design patterns. |
| [Touchpad](touchpad-interactions.md) | Handle touchpad input, which combines indirect multi-touch input with the precision of a pointing device. |
| [Multiple inputs](multiple-input-design-guidelines.md) | Design your app to work with as many input types as possible to maximize flexibility, usability, and accessibility. |
| [Custom text input](custom-text-input.md) | Use the core text APIs to receive text input from any text service supported on Windows devices, in any language and from any input type. |

## Interactions

| Topic | Description |
| --- | --- |
| [Drag and drop](../data/drag-and-drop.md) | Enable drag and drop interactions in your app. |
| [Panning](guidelines-for-panning.md) | Guidelines for implementing panning interactions. |
| [Rotation](guidelines-for-rotation.md) | Guidelines for implementing rotation interactions. |
| [Selecting text and images](guidelines-for-textselection.md) | Guidelines for selecting and manipulating text, images, and controls. |
| [Targeting](guidelines-for-targeting.md) | Guidelines for touch targeting and visual feedback. |
| [Visual feedback](guidelines-for-visualfeedback.md) | Provide visual feedback to users during interactions. |
| [Optical zoom and resizing](guidelines-for-optical-zoom.md) | Guidelines for zooming and resizing elements. |

## Speech and feedback

| Topic | Description |
| --- | --- |
| [Speech](speech-interactions.md) | Integrate speech recognition and text-to-speech directly into the user experience of your app. Requires package identity. |
| [Haptics](/windows/apps/design/signature-experiences/haptics) | Add touch feedback to reinforce user input and create more responsive, intuitive interactions. |

## Related topics

- [Input injection (UWP)](/windows/uwp/design/input/input-injection)
- [Input method editors](input-method-editors.md)
- [Respond to the touch keyboard](respond-to-the-presence-of-the-touch-keyboard.md)
- [Text scaling](text-scaling.md)
- [Focus navigation](focus-navigation.md)
- [Keyboard accelerators](keyboard-accelerators.md)
- [Access keys](access-keys.md)
