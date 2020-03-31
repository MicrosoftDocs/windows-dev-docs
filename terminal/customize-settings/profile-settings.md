---
title: Windows Terminal Profile Settings
description: Learn how to customize the individual profiles within Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Profile settings in the Windows Terminal

The properties listed below are specific to each unique profile.

## Unique identifier

Profiles can use a GUID as a unique identifier. To make a profile your default profile, it needs a GUID for the `defaultProfile` global setting.

**Property name:** `guid`

**Necessity:** Required

**Accepts:** GUID as a string in registry format: `"{00000000-0000-0000-0000-000000000000}"`

<br />

___

## Executable settings

### Command line

The executable used in the profile.

**Property name:** `commandline`

**Necessity:** Optional

**Accepts:** Executable file name as a string

### Source

Stores the name of the profile generator that originated this profile. _There are no discoverable values for this field._

**Property name:** `source`

**Necessity:** Optional

**Accepts:** String

### Starting directory

The directory the shell starts in when it is loaded.

**Property name:** `startingDirectory`

**Necessity:** Optional

**Accepts:** Folder location as a string

**Default value:** `"%USERPROFILE%"`

<br />

___

## Dropdown settings

### Name

This is the name of the profile that will be displayed in the dropdown menu. This value is also used as the "title" to pass to the shell on startup. Some shells (like `bash`) may choose to ignore this initial value, while others (`cmd`, `powershell`) may use this value over the lifetime of the application. This "title" behavior can be overridden by using `tabTitle`.

**Property name:** `name`

**Necessity:** Required

**Accepts:** String

### Icon

Set the icon that displays within the tab and the dropdown menu.

**Property name:** `icon`

**Necessity:** Optional

**Accepts:** File location as a string

### Hide a profile from the dropdown

If `"hidden"` is set to `true`, the profile will not appear in the list of profiles. This can be used to hide default profiles and dynamically generated profiles, while leaving them in your settings file.

**Property name:** `hidden`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

<br />

___

## Tab title settings

### Custom tab title

If set, this will replace the `name` as the title to pass to the shell on startup. Some shells (like `bash`) may choose to ignore this initial value, while others (`cmd`, `powershell`) may use this value over the lifetime of the application. If you'd like to learn how to have the shell set your title, visit the [tab title tutorial](./../tutorials/tab-title.md).

**Property name:** `tabTitle`

**Necessity:** Optional

**Accepts:** String

### Suppress title changes from the shell

When set to `true`, `tabTitle` overrides the default title of the tab and any title change messages from the application will be suppressed. If `tabTitle` isn't set, `name` will be used instead. When set to `false`, `tabTitle` behaves as normal.

**Property name:** `suppressApplicationTitle`

**Necessity:** Optional

**Accepts:** `true`, `false`

<br />

___

## Text settings

### Font face

Name of the font face used in the profile. The Terminal will try to fallback to Consolas if this can't be found or is invalid. For other variants of the default font, Cascadia Code, visit the [Cascadia Code GitHub repository](https://github.com/microsoft/cascadia-code).

**Property name:** `fontFace`

**Necessity:** Optional

**Accepts:** Font name as a string

**Default value:** `"Cascadia Code"`

### Font size

Sets the profile's font size.

**Property name:** `fontSize`

**Necessity:** Optional

**Accepts:** Integer

**Default value:** `12`

### Padding

Set the padding around the text within the window. This will accept three different formats: `"#"` sets the same padding for all sides, `"#, #"` sets the same padding for left-right and top-bottom, and `"#, #, #, #"` sets the padding individually for left, top, right, and bottom.

**Property name:** `padding`

**Necessity:** Optional

**Accepts:** Values as a string in the following formats: `"#"`, `"#, #"`, `"#, #, #, #"`

**Default value:** `"8, 8, 8, 8"`

### Antialiasing text

You can control how text is antialiased in the renderer. Note that changing this setting will require starting a new terminal instance.

**Property name:** `antialiasingMode`

**Necessity:** Optional

**Accepts:** `"grayscale"`, `"cleartype"`, `"aliased"`

**Default value:** `"grayscale"`

<br />

___

## Cursor settings

### Cursor shape

Sets the cursor shape for the profile. The possible cursors are as follows: `"vintage"` ( &#x2583; ), `"bar"` ( &#x2503; ), `"underscore"` ( &#x2581; ), `"filledBox"` ( &#x2588; ), `"emptyBox"` ( &#x25AF; )

**Property name:** `cursorShape`

**Necessity:** Optional

**Accepts:** `"bar"`, `"vintage"`, `"underscore"`, `"filledBox"`, `"emptyBox"`

**Default value:** `"bar"`

### Cursor color

Set the cursor color of the profile. This will override the `cursorColor` set in the color scheme if `colorScheme` is set.

**Property name:** `cursorColor`

**Necessity:** Optional

**Accepts:** Color as a string in hex format: `"#rgb"` or `"#rrggbb"`

### Cursor height

Set the percentage height of the cursor starting from the bottom. This will only work when `cursorShape` is set to `"vintage"`.

**Property name:** `cursorHeight`

**Necessity:** Optional

**Accepts:** Integer from 25-100

<br />

___

## Color settings

### Color scheme

The name of the color scheme is used to define which color scheme is used in the profile. Color schemes are defined in the `schemes` object. More information can be found on the [color schemes page](./color-schemes.md).

**Property name:** `colorScheme`

**Necessity:** Optional

**Accepts:** Name of color scheme as a string

**Default value:** `"Campbell"`

### Foreground color

Change the foreground color of the profile. This overrides `foreground` set in the color scheme if `colorScheme` is set.

**Property name:** `foreground`

**Necessity:** Optional

**Accepts:** Color as a string in hex format: `"#rgb"` or `"#rrggbb"`

### Background color

Change the background color of the profile with this setting. This overrides `background` set in the color scheme if `colorScheme` is set.

**Property name:** `background`

**Necessity:** Optional

**Accepts:** Color as a string in hex format: `"#rgb"` or `"#rrggbb"`

### Selection background color

Set the background color of a selection within the profile. This will override the `selectionBackground` set in the color scheme if `colorScheme` is set.

**Property name:** `selectionBackground`

**Necessity:** Optional

**Accepts:** Color as a string in hex format: `"#rgb"` or `"#rrggbb"`

<br />

___

## Acrylic settings

### Enable acrylic

When set to `true`, the window will have an acrylic background. When set to `false`, the window will have a plain, untextured background. The transparency only applies to focused windows due to OS limitation.

**Property name:** `useAcrylic`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

### Acrylic opacity

When `useAcrylic` is set to `true`, this sets the transparency of the window for the profile. Accepts floating point values from 0-1.

**Property name:** `acrylicOpacity`

**Necessity:** Optional

**Accepts:** Number as a floating point value from 0-1

**Default value:** `0.5`

<br />

___

## Background image settings

### Setting the background image

This sets the file location of the image to draw over the window background. More information can be found on the [background images page](./../background-images.md).

**Property name:** `backgroundImage`

**Necessity:** Optional

**Accepts:** File location as a string

### Background image stretch mode

You can set how the background image is resized to fill the window. More information can be found on the [background images page](./../background-images.md).

**Property name:** `backgroundImageStretchMode`

**Necessity:** Optional

**Accepts:** `"none"`, `"fill"`, `"uniform"`, `"uniformToFill"`

**Default value:** `"uniformToFill"`

### Background image alignment

This sets how the background image aligns to the boundaries of the window. More information can be found on the [background images page](./../background-images.md).

**Property name:** `backgroundImageAlignment`

**Necessity:** Optional

**Accepts:** `"center"`, `"left"`, `"top"`, `"right"`, `"bottom"`, `"topLeft"`, `"topRight"`, `"bottomLeft"`, `"bottomRight"`

**Default value:** `"center"`

### Background image opacity

You can set the transparency of the background image. More information can be found on the [background images page](./../background-images.md).

**Property name:** `backgroundImageOpacity`

**Necessity:** Optional

**Accepts:** Number as a floating point value from 0-1

**Default value:** `1.0`

<br />

___

## Scroll settings

### Scrollbar visibility

Set the visibility of the scrollbar.

**Property name:** `scrollbarState`

**Necessity:** Optional

**Accepts:** `"visible"`, `"hidden"`

### Scroll to input line when typing

When set to `true`, the window will scroll to the command input line when typing. When set to `false`, the window will not scroll when you start typing.

**Property name:** `snapOnInput`

**Necessity:** Optional

**Accepts:** `true`, `true`

**Default value:** `true`

### History size

Set the number of lines above the ones displayed in the window you can scroll back to.

**Property name:** `historySize`

**Necessity:** Optional

**Accepts:** Integer

**Default value:** `9001`

<br />

___

## How the profile closes when exiting

Set how the profile reacts to termination or failure to launch. `"graceful"` will close the profile when `exit` is typed or when the process exits normally. `"always"` will always close the profile and `"never"` will never close the profile. `true` and `false` are accepted as synonyms for `"graceful"` and `"never"` respectively.

**Property name:** `closeOnExit`

**Necessity:** Optional

**Accepts:** `"graceful"`, `"always"`, `"never"`, `true`, `false`

**Default value:** `"graceful"`

<br />

___

## Retro Terminal Effects

When set to `true`, enable retro terminal effects. This is an experimental feature and its continued existence is not guaranteed.

**Property name:** `experimental.retroTerminalEffect`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`
