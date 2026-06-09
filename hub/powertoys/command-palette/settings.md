---
title: PowerToys Command Palette Settings
description: Learn how to configure the PowerToys Command Palette settings, including activation key, appearance, and dock options.
ms.date: 04/10/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: Learn about the PowerToys Command Palette settings and how to configure them.
---

# PowerToys Command Palette settings

Use the **Settings** button in the Command Palette to open the settings page:

:::image type="content" source="../images/command-palette/cmdpal-settings.png" alt-text="A screenshot of the Command Palette interface with the Settings button highlighted in red.":::

The Command Palette settings page provides the following options.

## General

| Setting | Description |
| :--- | :--- |
| Activation key | Define the keyboard shortcut to show or hide the Command Palette. |
| Use low-level keyboard hook | Enable this option to use a low-level keyboard hook for activation key detection. This setting can help improve responsiveness but might cause compatibility issues with some software. |
| Ignore shortcut in fullscreen mode | When enabled, the Command Palette activation shortcut is ignored when an application is in fullscreen mode. |
| Ignore shortcut when the system heuristically detects fullscreen | Windows may detect that a fullscreen application is running or Presentation Settings are applied. Some applications (such as NVIDIA overlay) can trigger this incorrectly, preventing the shortcut from working. |
| Allow breakthrough with rapid shortcut presses | Press the activation shortcut 3 times within 2 seconds to bypass the fullscreen or busy state guard. |
| Auto-go-home interval | Configure how long Command Palette waits before automatically returning to the home page. Options include Infinite (never), immediate, or a set interval (10, 20, 30, 60, 90, 120, or 180 seconds). |
| Highlight search on activate | The previous search text is selected when you open the Command Palette. |
| Keep previous query | Retain the last search query when reopening the Command Palette. |
| Preferred monitor position | Choose the preferred monitor for the Command Palette to open on. Options include **Monitor with mouse cursor**, **Primary monitor**, **Focused window monitor**, **In place**, or **Last position**. |
| Escape key behavior | Configure how the <kbd>Escape</kbd> key behaves: clear search first then go back, always go back, always dismiss, or always hide. |
| Show app details | App details automatically expand when displaying an app as a result. |
| Backspace goes back | Typing <kbd>Backspace</kbd> takes you back to the previous page. |
| Single-click activation | Activate list items with a single click. When disabled, single clicking selects the item and double clicking activates it. |
| Show system tray icon | Show or hide the Command Palette icon in the system tray. |
| Disable animations | Disable animations in the Command Palette interface. |
| Enable external reload | Allow external processes to request a reload of Command Palette with the `x-cmdpal://reload` command. |

## Appearance

| Setting | Description |
| :--- | :--- |
| Theme | Choose the Command Palette theme: **Light**, **Dark**, or **Default** (follows system settings). |
| Backdrop style | Select the window backdrop effect: **Acrylic** (default), **Transparent**, **Mica**, or **Mica Alt**. |
| Backdrop opacity | Adjust the opacity of the backdrop effect (0–100). |
| Colorization mode | Customize how the Command Palette theme color is applied. |
| Custom theme color | Set a custom RGB color for the Command Palette theme. |
| Custom theme color intensity | Adjust the intensity of the custom theme color (1–100). |
| Background image path | Set a custom background image for the Command Palette window. |
| Background image opacity | Adjust the opacity of the background image (0–100). |
| Background image tint intensity | Adjust the tint overlay intensity on the background image (0–100). |
| Background image blur amount | Set the blur effect applied to the background image. |
| Background image brightness | Adjust the brightness of the background image. |
| Background image fit | Choose how the background image is scaled to fit the window. |

## Dock

For a full description of Dock settings, including appearance and behavior options, see [Command Palette Dock](dock.md#dock-settings).

## Related content

- [Command Palette overview](overview.md)
- [Command Palette Dock](dock.md)
- [Extensibility overview](extensibility-overview.md)
