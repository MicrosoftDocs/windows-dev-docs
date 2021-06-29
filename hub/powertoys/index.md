---
title: Microsoft PowerToys
description: Microsoft PowerToys is a set of utilities for customizing Windows 10. Utilities include ColorPicker (click anywhere to grab a color value), FancyZones (shortcuts to position windows into a grid layout), File Explorer Add-ons (preview SVGs or Markdown files), Image Resizer (resize one or many images with a simple right-click), Keyboard Manager (remap keys or create your own shortcuts), PowerRename (bulk rename using search and replace), PowerToys Run (Alt + Space to launch apps), a Shortcut Guide, and more to come.
ms.date: 12/02/2020
ms.topic: overview
ms.localizationpriority: medium
no-loc: [PowerRename, Windows, File Explorer, PowerToys, Run]
---

# Microsoft PowerToys: Utilities to customize Windows 10

Microsoft PowerToys is a set of utilities for power users to tune and streamline their Windows 10 experience for greater productivity.

> [!div class="nextstepaction"]
> [Install PowerToys](install.md)

## Processor support

- **x64**: Supported
- **x86**: In development (see [issue #602](https://github.com/microsoft/PowerToys/issues/602))
- **ARM**: In development (see [issue #490](https://github.com/microsoft/PowerToys/issues/490))

## Current PowerToy utilities

The currently available utilities include:

### Awake

:::row:::
    :::column:::
        [![Awake screenshot](../images/pt-awake-menu.png)](awake.md)
    :::column-end:::
    :::column span="2":::
        [Awake](awake.md) is designed to keep a computer awake without having to manage its power & sleep settings. This behavior can be helpful when running time-consuming tasks, ensuring that the computer does not go to sleep or turn off its screens.
    :::column-end:::
:::row-end:::

### Color Picker

:::row:::
    :::column:::
        [![ColorPicker screenshot](../images/pt-color-picker.png)](color-picker.md)
    :::column-end:::
    :::column span="2":::
        [ColorPicker](color-picker.md) is a system-wide color picking utility activated with <kbd>Win</kbd>+<kbd>Shift</kbd>+<kbd>C</kbd>. Pick colors from any currently running application, the picker automatically copies the color into your clipboard in a configurable format. Color Picker also contains an editor that shows a history of previously picked colors, allows you to fine-tune the selected color and to copy different string representations. This code is based on [Martin Chrzan's Color Picker](https://github.com/martinchrzan/ColorPicker).
    :::column-end:::
:::row-end:::

### Fancy Zones

:::row:::
    :::column:::
        [![FancyZones screenshot](../images/pt-fancy-zones.png)](fancyzones.md)
    :::column-end:::
    :::column span="2":::
        [FancyZones](fancyzones.md) is a window manager that makes it easy to create complex window layouts and quickly position windows into those layouts.
    :::column-end:::
:::row-end:::

### File Explorer add-ons

:::row:::
    :::column:::
        [![File Explorer screenshot](../images/pt-file-explorer.png)](file-explorer.md)
    :::column-end:::
    :::column span="2":::
        [File Explorer](file-explorer.md) add-ons  enable preview pane rendering in File Explorer to display SVG icons (.svg) and Markdown (.md) file previews. To enable the preview pane, select the "View" tab in File Explorer, then select "Preview Pane".
    :::column-end:::
:::row-end:::

### Image Resizer

:::row:::
    :::column:::
        [![Image Resizer screenshot](../images/pt-image-resizer.png)](image-resizer.md)
    :::column-end:::
    :::column span="2":::
        [Image Resizer](image-resizer.md) is a Windows Shell Extension for quickly resizing images.  With a simple right click from File Explorer, resize one or many images instantly. This code is based on [Brice Lambson's Image Resizer](https://github.com/bricelam/ImageResizer).
    :::column-end:::
:::row-end:::

### Keyboard Manager

:::row:::
    :::column:::
        [![Keyboard Manager screenshot](../images/pt-keyboard-manager.png)](keyboard-manager.md)
    :::column-end:::
    :::column span="2":::
        [Keyboard Manager](keyboard-manager.md) allows you to customize the keyboard to be more productive by remapping keys and creating your own keyboard shortcuts. This PowerToy requires Windows 10 1903 (build 18362) or later.
    :::column-end:::
:::row-end:::

### PowerRename

:::row:::
    :::column:::
        [![PowerRename screenshot](../images/pt-rename.png)](powerrename.md)
    :::column-end:::
    :::column span="2":::
        [PowerRename](powerrename.md) enables you to perform bulk renaming, searching and replacing file names. It includes advanced features, such as using regular expressions, targeting specific file types, previewing expected results, and the ability to undo changes. This code is based on [Chris Davis's SmartRename](https://github.com/chrdavis/SmartRename).
    :::column-end:::
:::row-end:::

### PowerToys Run

:::row:::
    :::column:::
        [![PowerToys Run screenshot](../images/pt-run.png)](run.md)
    :::column-end:::
    :::column span="2":::
        [PowerToys Run](run.md) can help you search and launch your app instantly - just enter the shortcut <kbd>Alt</kbd>+<kbd>Space</kbd> and start typing. It is open source and modular for additional plugins. Window Walker is now included as well. This PowerToy requires Windows 10 1903 (build 18362) or later.
    :::column-end:::
:::row-end:::

### Shortcut Guide

:::row:::
    :::column:::
        [![Shortcut Guide screenshot](../images/pt-shortcut-guide.png)](shortcut-guide.md)
    :::column-end:::
    :::column span="2":::
        [Windows key shortcut guide](shortcut-guide.md) appears when a user holds the Windows key down for more than one second and shows the available shortcuts for the current state of the desktop.
    :::column-end:::
:::row-end:::

## PowerToys video walk-through

In this video, Clint Rutkas (PM for PowerToys) walks through how to install and use the various utilities available, in addition to sharing some tips, info on how to contribute, and more.

> [!VIDEO https://channel9.msdn.com/Shows/Tabs-vs-Spaces/PowerToys-Utilities-to-customize-Windows-10/player?format=ny]

## Future PowerToy utilities

### Experimental PowerToys

Install the pre-release experimental version of PowerToys to try the latest experimental utilities, including:

#### Video Conference Mute (Experimental)

:::row:::
    :::column:::
        [![Video Conference Mute screenshot](../images/pt-video-conference-mute.png)](video-conference-mute.md)
    :::column-end:::
    :::column span="2":::
        [Video Conference Mute](video-conference-mute.md) is a quick way to globally "mute" both your microphone and camera using <kbd>⊞ Win</kbd>+<kbd>N</kbd> while on a conference call, regardless of the application that currently has focus. This is only included in the [pre-release/experimental version of PowerToys](https://github.com/microsoft/PowerToys/releases/) and requires Windows 10 1903 (build 18362) or later.
    :::column-end:::
:::row-end:::

## Known issues

Search known issues or file a new issue in the [Issues](https://github.com/microsoft/PowerToys/issues) tab of the PowerToys repository on GitHub.

## Contribute to PowerToys (Open Source)

PowerToys welcomes your contributions! The PowerToys development team is excited to partner with the power user community to build tools that help users get the most out of Windows. There are a variety of ways to contribute:

- Write a [tech spec](https://codeburst.io/on-writing-tech-specs-6404c9791159)
- Submit a [design concept or recommendation](https://www.microsoft.com/design/inclusive/)
- [Contribute to documentation](/contribute/)
- Identify and fix bugs in the [source code](https://github.com/microsoft/PowerToys/tree/master/src)
- [Code new features and PowerToy utilities](https://github.com/microsoft/PowerToys/tree/master/doc/devdocs)

Before starting work on a feature that you would like to contribute, **read the [Contributor's Guide](https://github.com/microsoft/PowerToys/blob/master/CONTRIBUTING.md)**. The PowerToys team will be happy to work with you to figure out the best approach, provide guidance and mentorship throughout feature development, and help avoid any wasted or duplicate effort.

## PowerToys release notes

PowerToys [release notes](https://github.com/microsoft/PowerToys/releases/) are listed on the install page of the GitHub repo. For reference, you can also find the [Release checklist](https://github.com/microsoft/PowerToys/wiki/Release-check-list) on the PowerToys wiki.

## PowerToys history

Inspired by the [Windows 95 era PowerToys project](https://en.wikipedia.org/wiki/Microsoft_PowerToys), this reboot provides power users with ways to squeeze more efficiency out of the Windows 10 shell and customize it for individual workflows.  A great overview of the Windows 95 PowerToys can be found [here](https://socket3.wordpress.com/2016/10/22/using-windows-95-powertoys/).

## PowerToys roadmap

PowerToys is a rapid-incubation, open source team aimed at providing power users with ways to squeeze more efficiency out of the Windows 10 shell and customize it for individual workflows. Work priorities will consistently be examined, reassessed, and adjusted with the aim of improving our users productivity.

- [New specs for possible PowerToys](https://github.com/microsoft/PowerToys/wiki/Specs)
- [Backlog priority list](https://github.com/microsoft/PowerToys/wiki/Roadmap#backlog-priority-list-in-order)
- [Version 1.0 Strategy spec](https://github.com/microsoft/PowerToys/wiki/Version-1.0-Strategy), February 2020
