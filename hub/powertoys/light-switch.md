---
title: Manage your Windows theme with Light Switch
description: Learn how to automatically switch between light and dark themes in Windows using PowerToys Light Switch. Reduce eye strain and save battery life.
ms.date: 10/14/2026
ms.topic: concept-article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, themes, Light Switch]
# Customer intent: As a Windows power user, I want to use PowerToys Light Switch to automatically change my Windows theme based on the time of day, so that I can have a more comfortable viewing experience.
---

# Light Switch utility

Light Switch is a PowerToys utility that automatically switches your Windows theme between light and dark modes based on time of day. You can configure Light Switch to change themes automatically using sunrise and sunset times in your location, or set custom schedules that work best for your daily routine.

Benefits of using Light Switch include:

- **Reduced eye strain** - Dark themes are easier on your eyes in low-light environments
- **Better battery life** - Dark themes can help extend battery life on devices with OLED displays
- **Automatic adaptation** - No need to manually change themes throughout the day

## How to activate and use Light Switch

Light Switch can be activated or deactivated by opening PowerToys Settings and changing the **Enable Light Switch** toggle. You can then configure the settings to specify the times for switching between light and dark themes based on your local sunrise and sunset times or manually set times.

:::image type="content" source="images/light-switch/settings.gif" alt-text="An animated GIF of the Light Switch settings.":::

The following settings are available for Light Switch:

| Setting | Description |
| :-- | :-- |
| Enable Light Switch | Turn the Light Switch utility on or off. |
| Theme toggle shortcut | Set a keyboard shortcut to manually toggle between light and dark themes. |
| Schedule Mode | Choose between using dark mode from **Sunset to Sunrise** (based on your location), **Fixed hours** (set specific times), or **Follow Night Light** (synchronize with the Windows Night Light schedule). Note that you must have location services enabled and synced for the Sunset to Sunrise mode to work. You can also disable the schedule by using the **Off** mode while still having access to the shortcut to toggle your theme. |
| Location | Enter your coordinates manually or use location services to automatically switch between light and dark mode based on local sunrise and sunset times. |
| Offset in minutes | Adjust the time for switching themes by a specified number of minutes before or after sunrise and sunset. |
| Turn on dark mode | Set the time to switch to dark mode when using manual mode. |
| Turn on light mode | Set the time to switch to light mode when using manual mode. |
| Apply dark mode to | Choose whether Light Switch applies dark mode to **System** (Taskbar, Start, and other system UI) and/or **Apps** (supported apps). |
| Apply monitor settings to | When the [Power Display](power-display.md) utility is enabled, choose a Power Display profile to apply when Light Switch transitions Windows into **Dark mode profile** and a profile to apply when it transitions to **Light mode profile**. This makes your saved monitor brightness, contrast, and color temperature settings switch automatically with the system theme. |

## Command-line interface

Light Switch includes a command-line interface that controls the running Light Switch service, so you can check or change the theme from scripts and terminals. PowerToys adds its `bin` folder to your `PATH`, so you can run `PowerToys.LightSwitch.CLI.exe` from any folder in Windows Terminal, PowerShell, or Command Prompt. If a terminal was already open when you installed PowerToys, open a new one so it picks up the updated `PATH`.

Light Switch must be enabled in PowerToys and its service must be running. The CLI doesn't start PowerToys or turn on Light Switch for you.

| Command | Description |
| :-- | :-- |
| `status` | Show the current system and app themes, which theme targets Light Switch changes, the schedule mode, and whether a manual override is active. |
| `light` | Apply the light theme to the targets selected in **Apply dark mode to** (**System**, **Apps**, or both). |
| `dark` | Apply the dark theme to the selected targets. |
| `toggle` | Toggle the selected targets between light and dark, the same as the **Theme toggle shortcut**. |
| `schedule enable [--mode <mode>]` | Turn on scheduling. Use `--mode` with `fixed-hours`, `sunset-to-sunrise`, or `follow-night-light` to choose the schedule. |
| `schedule disable` | Turn off scheduling by setting the schedule mode to **Off**. |

| Option | Description |
| :-- | :-- |
| `--json` | Write a single JSON object, including for errors. |
| `-h`, `--help` | Show help. Doesn't contact the service. |
| `--version` | Show the CLI version. Doesn't contact the service. |

When `light`, `dark`, or `toggle` sets a theme that's different from what the schedule calls for, Light Switch keeps that theme as a manual override until the next scheduled change or until you change a relevant setting.

`schedule enable --mode` uses the times, location, and offsets already saved in Light Switch settings. If you omit `--mode`, the command succeeds when a schedule is already on. If the schedule mode is **Off**, the command fails and asks you to specify a mode.

**Usage example**

```powershell
# Check the current theme and schedule state
PowerToys.LightSwitch.CLI.exe status

# Switch the configured targets to dark mode
PowerToys.LightSwitch.CLI.exe dark

# Turn on the sunset-to-sunrise schedule
PowerToys.LightSwitch.CLI.exe schedule enable --mode sunset-to-sunrise

# Get the current state as JSON
PowerToys.LightSwitch.CLI.exe status --json
```

With `--json`, the output contains a `version`, a `success` value, and either a `state` object or an `error` object with a `code` and `message`. For example:

```json
{"version":1,"success":true,"state":{"systemTheme":"dark","appsTheme":"dark","changeSystem":true,"changeApps":true,"scheduleMode":"FixedHours","manualOverride":true}}
```

The CLI returns the following exit codes:

| Exit code | Meaning |
| :-- | :-- |
| `0` | Success. |
| `1` | The command failed, the theme couldn't be read or changed, or the service returned an unexpected response. |
| `2` | Invalid arguments or configuration. |
| `3` | The Light Switch service isn't available. Turn on Light Switch in PowerToys and try again. |
| `4` | The command timed out or the connection to the service was lost. The change might still have been applied, so run `status` before you try again. |

The CLI never retries a command that changes the theme or schedule automatically.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
