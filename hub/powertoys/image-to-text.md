---
title: PowerToys Image to Text utility for Windows
description: Image to Text is a convenient way to copy text from anywhere on your screen.
ms.date: 04/27/2022
ms.topic: article
no-loc: [PowerToys, Windows, Image to Text, Win]
---

# Image to Text utility

Image to Text is a convenient way to copy text from anywhere on your screen. This code is based on [Joe Finney's Text Grab](https://github.com/TheJoeFin/Text-Grab).

## How to activate

With the activation shortcut (default: <kbd>⊞ Win</kbd>+<kbd>Shift</kbd>+<kbd>T</kbd>), you'll see an overlay on the screen. Click and hold your primary mouse button and drag to activate your capture.  The text will be saved to your clipboard

## Adjust while trying to capture

By holding <kbd>shift<kbd>, you will change from adjusting the capture region's size to location. When you release <kbd>shift<kbd>, you will be able to resize again.

## Things to note

This uses OCR (Optical character recognition) to read the text on the screen.  It may not be perfect so you will have to do a quick proof read of the output.

## Settings

From the Settings menu, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Activation shortcut | The customizable keyboard command to turn on or off always on top for that window. |
| Do not activate when Game Mode is on | Prevents the feature from being activated when actively playing a game on the system. |
| Color | The custom color of the highlight border. |
| Border thickness (px) | The thickness of the highlight border. Measured in pixels |
| Play a sound | A short alert chirp is played. Activating and deactivating use different sounds. |
| Excluded apps | Add an application's name, or part of the name, one per line. (e.g. adding `Notepad` will match both `Notepad.exe` and `Notepad++.exe`; to match only `Notepad.exe` add the `.exe` extension) |
