---
description: Learn how to customize your WinUI for interaction with specific types of inputs including pen and speech.
title: Input and interactions
keywords: app inputs, customize WinUI application
label: Input and interactions
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
ms.assetid: b771d452-c3ac-4d97-8482-eaf81bf34306
ms.localizationpriority: medium
---

# Input and interactions

WinUI apps automatically handle a wide variety of inputs and run on a variety of devices—there’s nothing extra you need to do to enable touch input, for example. But there are times when you might want to optimize your app for certain types of input or devices. For example, if you’re creating a painting app, you might want to customize the way you handle pen input.

The design and coding instructions in this section help you customize your WinUI app for specific types of inputs.

:::row:::
    :::column:::
        <h3><a href="input-primer.md">Input primer</a></h3>
        <p>Familiarize yourself with each input device type and its behaviors, capabilities, and limitations when paired with certain form factors.</p>
    :::column-end:::
    :::column:::
        <h3><a href="gaze-interactions.md">Gaze input</a></h3>
        <p>Track a user's gaze based on the location and movement of their eyes and head.</p>
    :::column-end:::
:::row-end:::

<!-- 
## Input primer

See our <b>[Input primer](../../design/input/index.md)</b> to familiarize yourself with each input device type and its behaviors, capabilities, and limitations when paired with certain form factors. -->

:::row:::
    :::column:::
        <h2>Input</h2>
        <a href="/windows/uwp/design/input/identify-input-devices">Identify input devices</a><br/>
        <a href="/windows/uwp/design/input/handle-pointer-input">Pointer</a><br/>
        <a href="/windows/uwp/design/input/pen-and-stylus-interactions">Pen and Windows Ink</a><br/>
        <a href="/windows/uwp/design/input/touch-interactions">Touch</a><br/>
        <a href="/windows/uwp/design/input/mouse-interactions">Mouse</a><br/>
        <a href="/windows/uwp/design/input/keyboard-interactions">Keyboard</a><br/>
        <a href="/windows/uwp/design/input/gamepad-and-remote-interactions">Gamepad and remote control</a><br/>
        <a href="/windows/uwp/design/input/touchpad-interactions">Touchpad</a><br/>
        <a href="/windows/uwp/design/input/windows-wheel-interactions">Surface Dial</a><br/>
        <a href="/windows/uwp/design/input/multiple-input-design-guidelines">Multiple inputs</a><br/>
        <a href="/windows/uwp/design/input/input-injection">Input injection</a><br/>
        <a href="/windows/uwp/design/input/custom-text-input">Custom text input</a><br/>
    :::column-end:::
    :::column:::
        <h2>Interactions</h2>
        <a href="/windows/uwp/design/input/drag-and-drop">Drag and drop</a><br/>
        <a href="/windows/uwp/design/input/guidelines-for-panning">Panning</a><br/>
        <a href="/windows/uwp/design/input/guidelines-for-rotation">Rotation</a><br/>
        <a href="/windows/uwp/design/input/guidelines-for-textselection">Selecting text and images</a><br/>
        <a href="/windows/uwp/design/input/guidelines-for-targeting">Targeting</a><br/>
        <a href="/windows/uwp/design/input/guidelines-for-visualfeedback">Visual feedback</a><br/>
    :::column-end:::
    :::column:::
        <h2>Speech and AI</h2>
        <a href="/windows/uwp/design/input/speech-interactions">Speech</a><br/>
        <a href="/windows/uwp/design/input/cortana-interactions">Cortana</a><br/>
    :::column-end:::
:::row-end:::


<!-- <div class="side-by-side">
<div class="side-by-side-content">
</div>
</div>

<div class="side-by-side">
<div class="side-by-side-content">
<div class="side-by-side-content-right">
<p>
<b>[Speech](../../design/input/speech-interactions.md)</b><br/>
Integrate speech recognition and text-to-speech (also known as TTS, or speech synthesis) directly into the user experience of your app.
</p>
</div>
</div>
</div>

<div class="side-by-side">
<div class="side-by-side-content">
<div class="side-by-side-content-left">
<p>
<b>[Pen](../../design/input/pen-and-stylus-interactions.md)</b><br/>
Optimize your UWP app for pen input to provide both standard pointer device functionality and the best Windows Ink experience for your users.
</p>
</div>
<div class="side-by-side-content-right">
<p>
<b>[Keyboard](../../design/input/keyboard-interactions.md)</b><br/>
<div class="side-by-side">
<div class="side-by-side-content">
<div class="side-by-side-content-left">
<p>
<b>[Pen](../../design/input/pen-and-stylus-interactions.md)</b><br/>
Optimize your WinUI app for pen input to provide both standard pointer device functionality and the best Windows Ink experience for your users.
</p>
</div>
<div class="side-by-side-content-right">
<p>
<b>[Keyboard](../../design/input/keyboard-interactions.md)</b><br/>
Keyboard input is an important part of the overall user interaction experience for apps. The keyboard is indispensable to people with certain disabilities or users who just consider it a more efficient way to interact with an app.
</p>
</div>
</div>
</div>
<b>[Touchpad](../../design/input/touchpad-interactions.md)</b><br/>
<div class="side-by-side">
<div class="side-by-side-content">
<div class="side-by-side-content-left">
<p>
<b>[Touch](../../design/input/touch-interactions.md)</b><br/>
WinUI includes a number of different mechanisms for handling touch input, all of which enable you to create an immersive experience that your users can explore with confidence.
</p>
</div>
<div class="side-by-side-content-right">
<p>
<b>[Touchpad](../../design/input/touchpad-interactions.md)</b><br/>
A touchpad combines both indirect multi-touch input with the precision input of a pointing device, such as a mouse. This combination makes the touchpad suited to both a touch-optimized UI and the smaller targets of productivity apps.
</p>
</div>
</div>
</div>

<div class="side-by-side">
<div class="side-by-side-content">
<p>
<b>[Multiple inputs](../../design/input/multiple-input-design-guidelines.md)</b><br/>
To accommodate as many users and devices as possible, we recommend that you design your apps to work with as many input types as possible (speech, touch, touchpad, mouse, and keyboard). Doing so will maximize flexibility, usability, and accessibility.
</p>
</div>
</div>

<div class="side-by-side">
<div class="side-by-side-content">
<div class="side-by-side-content-left">
<p>
<b>[Identify input devices](../../design/input/identify-input-devices.md)</b><br/>
Identify the input devices connected to a Windows app device and identify their capabilities and attributes.
</p>
</div>
<div class="side-by-side-content-right">
<p>
<b>[Handle pointer input](../../design/input/handle-pointer-input.md)</b><br/>
Receive, process, and manage input data from pointing devices, such as touch, mouse, pen/stylus, and touchpad, in Windows apps.
</p>
</div>
</div>
</div>

<div class="side-by-side">
<div class="side-by-side-content">
<div class="side-by-side-content-left">
<p><b>[Custom text input](../../design/input/custom-text-input.md)</b><br/>
The core text APIs in the Windows.UI.Text.Core namespace enable a WinUI app to receive text input from any text service supported on Windows devices. This enables the app to receive text in any language and from any input type, like keyboard, speech, or pen.
</p>
</div>
<div class="side-by-side-content-right">
<p>
<b>[Selecting text and images](../../design/input/guidelines-for-textselection.md)</b><br/>
This article describes selecting and manipulating text, images, and controls and provides user experience guidelines that should be considered when using these mechanisms in your apps.
</p>
</div>
</div>
</div>

<div class="side-by-side">
<div class="side-by-side">
<div class="side-by-side-content">
<div class="side-by-side-content-left">
<p><b>[Custom text input](../../design/input/custom-text-input.md)</b><br/>
The core text APIs in the Windows.UI.Text.Core namespace enable a WinUI app to receive text input from any text service supported on Windows devices. This enables the app to receive text in any language and from any input type, like keyboard, speech, or pen.
</p>
</div>
<div class="side-by-side-content-right">
<p>
<b>[Selecting text and images](../../design/input/guidelines-for-textselection.md)</b><br/>
This article describes selecting and manipulating text, images, and controls and provides user experience guidelines that should be considered when using these mechanisms in your apps.
</p>
</div>
</div>
</div>
</div>
<div class="side-by-side-content-right">
<p>
<b>[Rotation](../../design/input/guidelines-for-rotation.md)</b><br/>
This article describes the new Windows UI for rotation and provides user experience guidelines that should be considered when using this new interaction mechanism in your WinUI app.
</p>
</div>
</div>
</div>

<div class="side-by-side">
<div class="side-by-side-content">
<div class="side-by-side-content-left">
<p>
<b>[Optical zoom and resizing](../../design/input/guidelines-for-optical-zoom.md)</b><br/>
This article describes Windows zooming and resizing elements and provides user experience guidelines for using these interaction mechanisms in your apps.
</p>
</div>
<div class="side-by-side-content-right">
<p>
<b>[Rotation](../../design/input/guidelines-for-rotation.md)</b><br/>
This article describes the new Windows UI for rotation and provides user experience guidelines that should be considered when using this new interaction mechanism in your WinUI app.
</p>
</div>
</div>
</div>
