---
title: PowerToys Power Display utility for Windows
description: Learn how to use PowerToys Power Display to control external monitor brightness, contrast, volume, input source, and color temperature from a single flyout, and save settings as profiles.
ms.date: 04/26/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Power Display, DDC/CI, VCP]
# Customer intent: As a Windows power user with multiple external monitors, I want to learn about the Power Display utility in PowerToys so I can control brightness, volume, input source, and other monitor settings from one place.
---

# Power Display utility

PowerToys Power Display is a display management utility that puts brightness and other monitor controls into a single flyout. It uses [DDC/CI](https://en.wikipedia.org/wiki/Display_Data_Channel#DDC/CI) to talk to your external monitors, so you can adjust brightness, contrast, volume, input source, rotation, color temperature, and power state without reaching for the buttons on the back of each display. You can also save combinations of settings as profiles and apply them with a single click.

:::image type="content" source="images/power-display/powerdisplay.png" alt-text="Screenshot of the Power Display flyout showing per-monitor controls for brightness, contrast, and volume.":::

## Open Power Display

Press the activation shortcut (or click **Open Power Display** in Settings) to bring up the Power Display flyout. The flyout lists each connected monitor and the controls you've enabled for it.

## Per-monitor controls

For each monitor, Power Display can show:

- **Brightness** slider
- **Contrast** slider
- **Volume** slider
- **Input source** control
- **Rotation** control
- **Color temperature** switcher
- **Power state** control

The available controls depend on what your monitor reports through DDC/CI. If a monitor doesn't report capabilities, advanced controls may be limited.

> [!WARNING]
> Changing the **color temperature** setting may cause unpredictable results on some monitors, including incorrect display colors. Power Display asks you to confirm before enabling color temperature control on a monitor.

## Profiles

Save your favorite combination of brightness, contrast, volume, and color temperature across one or more monitors as a **profile** (for example, *Gaming Mode* or *Work*). Apply a profile from the Settings page or directly from the flyout via the profile switcher button.

:::image type="content" source="images/power-display/profiles.png" alt-text="Screenshot of the Edit profile dialog in PowerToys Settings, showing a Gaming profile with brightness, contrast, volume, and color temperature settings selected per monitor.":::

You can also configure Power Display to **restore monitor brightness and color temperature when Power Display launches** so your preferred settings are reapplied at startup.

## Light Switch integration

Power Display integrates with the [Light Switch](light-switch.md) utility so your monitor profiles can follow your Windows theme. When both utilities are enabled, you can pick a Power Display profile to apply when Light Switch transitions Windows into **dark mode** and a different profile to apply when it transitions to **light mode**. This lets the brightness, contrast, color temperature, and other monitor settings you've saved in a profile switch automatically alongside the system theme.

Configure this integration from the Light Switch Settings page, under **Apply monitor settings to**, by selecting a **Dark mode profile** and a **Light mode profile** from your saved Power Display profiles. Power Display must be enabled for the profile pickers to be available.

## Custom VCP name mappings

Power Display lets you define custom display names for color temperature presets and input sources by mapping monitor [VCP codes](https://en.wikipedia.org/wiki/Monitor_Control_Command_Set) (for example, `0x14` for color temperature, `0x60` for input source) to a friendlier label. You can scope a mapping to a specific monitor or apply it to all monitors. The **VCP capabilities** card on each monitor shows the DDC/CI VCP codes and supported values reported by that display, which is useful when authoring mappings or debugging.

## Settings

Power Display has the following settings:

| Setting | Description |
| :--- | :--- |
| **Enable Power Display** | Turn the utility on or off. |
| **Activation shortcut** | The customizable keyboard shortcut to open the Power Display flyout. |
| **Monitor refresh delay** | Number of seconds to wait after display changes before refreshing monitors. Increase this if monitors are not detected after hot-plug. |
| **Restore monitor brightness and color temperature when Power Display launches** | Reapplies your saved settings when Power Display starts. |
| **Show system tray icon** | Show a Power Display icon in the Windows system tray. |
| **Show profile switcher button** | Show the profile switcher button in the Power Display flyout. |
| **Show identify monitors button** | Show a button that briefly displays a number on each monitor to help you tell them apart. |
| **Profiles** | Save and apply named combinations of brightness, contrast, volume, and color temperature across selected monitors. |
| **Custom VCP name mappings** | Define custom display names for color temperature presets and input sources, scoped to a specific monitor or applied to all monitors. |
| **Per-monitor toggles** | For each connected monitor, choose which controls (contrast, volume, input source, rotation, color temperature, power state) appear in the flyout, and optionally **Hide monitor** to remove it from the flyout entirely. |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
