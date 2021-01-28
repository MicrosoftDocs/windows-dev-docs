---
title: Windows Terminal Rendering Settings
description: Learn how to customize rendering settings within Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 01/28/2021
ms.topic: how-to
ms.localizationpriority: high
---

# Rendering settings in Windows Terminal

The properties listed below affect the entire terminal window, regardless of the profile settings. These should be placed at the root of your settings.json file.

If you are thinking about changing the rendering settings, additional information is provided on the [Troubleshooting page](./../troubleshooting.md#the-text-is-blurry) to help guide you.

> [!IMPORTANT]
> The settings UI is only available in [Windows Terminal Preview](https://aka.ms/terminal-preview). Detailed instructions on how to enable the settings UI can be found on the [Troubleshooting page](./../troubleshooting.md#open-the-settings-ui).

## Redraw entire screen when display updates

When this set to `true`, the terminal will redraw the entire screen each frame. When set to `false`, it will render only the updates to the screen between frames.

**Property name:** `experimental.rendering.forceFullRepaint`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

<br />

___

## Use software rendering

When this is set to `true`, the terminal will use the software renderer (a.k.a. WARP) instead of the hardware one.

**Property name:** `experimental.rendering.software`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`
