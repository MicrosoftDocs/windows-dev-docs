---
title: Windows Terminal Appearance Profile Settings
description: Learn how to customize the appearance profile settings within Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 07/14/2021
ms.topic: how-to
ms.localizationpriority: high
---

# Appearance profile settings in Windows Terminal

The settings listed below are specific to each unique profile. If you'd like a setting to apply to all of your profiles, you can add it to the `defaults` section above the list of profiles in your [settings.json file](../get-started.md#settings-json-file).

```json
"defaults":
{
    // SETTINGS TO APPLY TO ALL PROFILES
},
"list":
[
    // PROFILE OBJECTS
]
```

## Text

### Color scheme

This is the name of the color scheme used in the profile. Color schemes are defined in the `schemes` object. More detailed information can be found on the [Color schemes page](./color-schemes.md).

**Property name:** `colorScheme`

**Necessity:** Optional

**Accepts:** Name of color scheme as a string

**Default value:** `"Campbell"`

### Font

This is the structure within which the other font settings must be defined. An example of what this could look like in the JSON file is shown below.

**Property name:** `font`

**Necessity:** Optional

### Font face

This is the name of the font face used in the profile. The terminal will try to fallback to Consolas if this can't be found or is invalid. To learn about the other variants of the default font, Cascadia Mono, visit the [Cascadia Code page](./../cascadia-code.md).

**Property name:** `face` (defined within the `font` object)

**Necessity:** Optional

**Accepts:** Font name as a string

**Default value:** `"Cascadia Mono"`

### Font size

This sets the profile's font size in points.

**Property name:** `size` (defined within the `font` object)

**Necessity:** Optional

**Accepts:** Integer

**Default value:** `12`

### Font weight

This sets the weight (lightness or heaviness of the strokes) for the profile's font.

**Property name:** `weight` (defined within the `font` object)

**Necessity:** Optional

**Accepts:** `"normal"`, `"thin"`, `"extra-light"`, `"light"`, `"semi-light"`, `"medium"`, `"semi-bold"`, `"bold"`, `"extra-bold"`, `"black"`, `"extra-black"`, or an integer corresponding to the numeric representation of the OpenType font weight

**Default value:** `"normal"`

### Font example

```json
"font": {
    "face": "Cascadia Mono",
    "size": 12,
    "weight": "normal"
}
```

## Retro terminal effects

:::row:::
:::column span="":::
When this is set to `true`, the terminal will emulate a classic CRT display with scan lines and blurry text edges. This is an experimental feature and its continued existence is not guaranteed.

If `experimental.pixelShaderPath` is set, it will override this setting.

**Property name:** `experimental.retroTerminalEffect`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

:::column-end:::
:::column span="":::
![Windows Terminal experimental retro terminal effect](./../images/experimental-retro-terminal-effect.gif)
_Configuration: [Retro Command Prompt](./../custom-terminal-gallery/retro-command-prompt.md)_

:::column-end:::
:::row-end:::

<br />

___

## Cursor

### Cursor shape

This sets the cursor shape for the profile. The possible cursors are as follows: `"bar"` ( &#x2503; ), `"vintage"` ( &#x2583; ), `"underscore"` ( &#x2581; ), `"filledBox"` ( &#x2588; ), `"emptyBox"` ( &#x25AF; ), `"doubleUnderscore"` ( &#x2017; )

**Property name:** `cursorShape`

**Necessity:** Optional

**Accepts:** `"bar"`, `"vintage"`, `"underscore"`, `"filledBox"`, `"emptyBox"`, `"doubleUnderscore"`

**Default value:** `"bar"`

### Cursor height

This sets the percentage height of the cursor starting from the bottom. This will only work when `cursorShape` is set to `"vintage"`.

**Property name:** `cursorHeight`

**Necessity:** Optional

**Accepts:** Integer from 1-100

<br />

___

## Background image

### Background image path

This sets the file location of the image to draw over the window background. The background image can be a .jpg, .png, or .gif file. `"desktopWallpaper"` will set the background image to the desktop's wallpaper.

**Property name:** `backgroundImage`

**Necessity:** Optional

**Accepts:** File location as a string or `"desktopWallpaper"`

### Background image stretch mode

:::row:::
:::column span="":::
This sets how the background image is resized to fill the window.

**Property name:** `backgroundImageStretchMode`

**Necessity:** Optional

**Accepts:** `"none"`, `"fill"`, `"uniform"`, `"uniformToFill"`

**Default value:** `"uniformToFill"`

:::column-end:::
:::column span="":::
![Windows Terminal background image stretch mode](./../images/background-image-stretch-mode.gif)
_[Background image source](https://wallpaperhub.app/wallpapers/6287)_

:::column-end:::
:::row-end:::

### Background image alignment

:::row:::
:::column span="":::
This sets how the background image aligns to the boundaries of the window.

**Property name:** `backgroundImageAlignment`

**Necessity:** Optional

**Accepts:** `"center"`, `"left"`, `"top"`, `"right"`, `"bottom"`, `"topLeft"`, `"topRight"`, `"bottomLeft"`, `"bottomRight"`

**Default value:** `"center"`

:::column-end:::
:::column span="":::
![Windows Terminal background image alignment](./../images/background-image-alignment.gif)
_[Background image source](https://design.ubuntu.com/brand/ubuntu-logo/)_

:::column-end:::
:::row-end:::

### Background image opacity

:::row:::
:::column span="":::
This sets the transparency of the background image.

:::column-end:::
:::row-end:::

**Property name:** `backgroundImageOpacity`

**Necessity:** Optional

**Accepts:** Number as a floating point value from 0-1

**Default value:** `1.0`

<br />

___

## Acrylic

### Enable acrylic

:::row:::
:::column span="":::
When this is set to `true`, the window will have an acrylic background. When it's set to `false`, the window will have a plain, untextured background. The transparency only applies to focused windows due to OS limitations.

**Property name:** `useAcrylic`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

:::column-end:::
:::column span="":::
![Windows Terminal acrylic](./../images/acrylic.gif)

:::column-end:::
:::row-end:::

### Acrylic opacity

:::row:::
:::column span="":::
When `useAcrylic` is set to `true`, this sets the transparency of the window for the profile. This accepts floating point values from 0-1.

**Property name:** `acrylicOpacity`

**Necessity:** Optional

**Accepts:** Number as a floating point value from 0-1

**Default value:** `0.5`

:::column-end:::
:::column span="":::
![Windows Terminal acrylic opacity](./../images/acrylic-opacity.gif)

:::column-end:::
:::row-end:::

<br />

___

## Window

### Padding

:::row:::
:::column span="":::
This sets the padding around the text within the window. This will accept three different formats: `"#"` and `#` set the same padding for all sides, `"#, #"` sets the same padding for left-right and top-bottom, and `"#, #, #, #"` sets the padding individually for left, top, right, and bottom.

**Property name:** `padding`

**Necessity:** Optional

**Accepts:** Values as a string in the following formats: `"#"`, `"#, #"`, `"#, #, #, #"` or value as an integer: `#`

**Default value:** `"8, 8, 8, 8"`

:::column-end:::
:::column span="":::
![Windows Terminal padding](./../images/padding.gif)

:::column-end:::
:::row-end:::

### Scrollbar visibility

This sets the visibility of the scrollbar.

**Property name:** `scrollbarState`

**Necessity:** Optional

**Accepts:** `"visible"`, `"hidden"`

<br />

___

## Color settings

### Tab color

This sets the color of the profile's tab. Using the tab color picker will override this color.

**Property name:** `tabColor`

**Necessity:** Optional

**Accepts:** Color as a string in hex format: `"#rgb"` or `"#rrggbb"`

### Foreground color

This changes the foreground color of the profile. This overrides `foreground` set in the color scheme if `colorScheme` is set.

**Property name:** `foreground`

**Necessity:** Optional

**Accepts:** Color as a string in hex format: `"#rgb"` or `"#rrggbb"`

### Background color

This changes the background color of the profile with this setting. This overrides `background` set in the color scheme if `colorScheme` is set.

**Property name:** `background`

**Necessity:** Optional

**Accepts:** Color as a string in hex format: `"#rgb"` or `"#rrggbb"`

### Selection background color

This sets the background color of a selection within the profile. This will override the `selectionBackground` set in the color scheme if `colorScheme` is set.

**Property name:** `selectionBackground`

**Necessity:** Optional

**Accepts:** Color as a string in hex format: `"#rgb"` or `"#rrggbb"`

### Cursor color

This sets the cursor color of the profile. This will override the `cursorColor` set in the color scheme if `colorScheme` is set.

**Property name:** `cursorColor`

**Necessity:** Optional

**Accepts:** Color as a string in hex format: `"#rgb"` or `"#rrggbb"`

<br />

___

## Unfocused appearance settings

An object you can add to a profile that applies settings to the profile when it is unfocused. This setting only accepts appearance settings.

**Property name:** `unfocusedAppearance`

**Necessity:** Optional

**Accepts:** `backgroundImage`, `backgroundImageAlignment`, `backgroundImageOpacity`, `backgroundImageStretchMode`, `cursorHeight`, `cursorShape`, `cursorColor`, `colorScheme`, `foreground`, `background`, `selectionBackground`, `experimental.retroTerminalEffect`, `experimental.pixelShaderPath`

**Example:**
```json
// Sets the profile's background image opacity to 0.3 when it is unfocused
"unfocusedAppearance": 
{
    "backgroundImageOpacity": 0.3
},
```

<br />

___

## Pixel shader effects

This setting allows a user to specify the path to a custom pixel shader to use with the terminal content. This is an experimental feature and its continued existence is not guaranteed. For more details on authoring custom pixel shaders for the terminal, see [this documentation](https://github.com/microsoft/terminal/blob/main/samples/PixelShaders/README.md).

If set, this will override the `experimental.retroTerminalEffect` setting.

**Property name:** `experimental.pixelShaderPath`

**Necessity:** Optional

**Accepts:** A path to an `.hlsl` shader file, as a string
