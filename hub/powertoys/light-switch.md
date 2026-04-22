---
title: Manage your Windows theme with Light Switch
description: Learn how to automatically switch between light and dark themes in Windows using PowerToys Light Switch. Reduce eye strain and save battery life.
ms.date: 09/30/2025
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

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
