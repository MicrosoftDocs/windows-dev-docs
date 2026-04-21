---
title: Configure PowerToys General Settings for Windows
description: Configure PowerToys general settings including updates, admin mode, themes, and startup behavior. Learn how to customize your Windows PowerToys experience with our step-by-step guide.
ms.date: 08/28/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows power user, I want to learn how to configure the general settings for PowerToys.
---

# General settings for PowerToys

:::image type="content" source="images/pt-general.png" alt-text="Screenshot of PowerToys general settings interface showing version updates, administrator mode, and appearance options.":::

The General settings section of Microsoft PowerToys allows you to configure essential app behaviors including updates, administrator permissions, appearance themes, and startup options. These settings help you customize your PowerToys experience to match your Windows workflow preferences.

## Version & updates

Here you can check for new updates, and if one is available, you can download and install it.

| Setting | Description |
| :--- | :--- |
| Download updates automatically | When enabled, PowerToys automatically downloads available updates in the background. |
| Show new updates as toast notifications | When enabled, displays a Windows toast notification when a new update is available. |
| Show what's new after updates | When enabled, displays a "What's New" dialog after updates are applied so you can see the latest changes. |

## Settings search

Use the search bar at the top of the settings window to quickly find specific settings or features within PowerToys. You can type keywords related to the setting you're looking for, and the results will filter in real-time as you type.

:::image type="content" source="images/pt-settings-search.png" alt-text="A screenshot of the PowerToys Settings search feature.":::

Select the desired setting from the filtered results to jump directly to it, or you can press **Enter** or select **Show all results** to see all results for the current search.

:::image type="content" source="images/pt-settings-search-results.png" alt-text="A screenshot of the PowerToys Settings Search Results page.":::

## Shortcut conflict detection

PowerToys includes a feature to detect shortcut conflicts. If you have multiple PowerToys modules that use the same keyboard shortcut, the conflict detection feature will alert you so you can resolve the issue. When the **PowerToys shortcut conflicts** window appears, you can view all conflicting shortcuts. From this window, you can reassign shortcuts to resolve conflicts, choose to ignore specific conflicts, or remove shortcuts entirely.

:::image type="content" source="images/pt-shortcut-conflicts.png" alt-text="A screenshot of the PowerToys shortcut conflicts window.":::

## Administrator mode

PowerToys can run in administrator mode for enhanced capabilities. Read more about the administrator mode in the [PowerToys running with administrator permissions](./administrator.md) section of the documentation.

| Setting | Description |
| :--- | :--- |
| Warnings for elevated apps | When enabled, shows warnings when an elevated (administrator) application may interfere with PowerToys utilities. This helps you understand when certain features might not work as expected due to Windows security boundaries between elevated and non-elevated processes. |

## Appearance & behavior

### Application language

PowerToys will use the language of your Windows installation. If you want to change the language, you can select a different one from the dropdown menu. The change will take effect after you restart PowerToys. Supported languages: Arabic (Saudi Arabia), Chinese (simplified), Chinese (traditional), Czech, Dutch, English, French, German, Hebrew, Hungarian, Italian, Japanese, Korean, Persian, Polish, Portuguese, Portuguese (Brazil), Russian, Spanish, Swedish Turkish, Ukrainian.

### App theme

Here you can set the theme of the PowerToys settings app and the PowerToys flyout: **Dark**, **Light**, or **Windows default**.

### Run at startup

If activated, PowerToys will start automatically when you log in to Windows.

### Show system tray icon

When activated, PowerToys shows an icon in the system tray area of the taskbar. You can use this icon to open the PowerToys settings app or the PowerToys flyout.

| Setting | Description |
| :--- | :--- |
| Show theme-adaptive system tray icon | When enabled, the system tray icon uses a monochrome icon that adapts to your current Windows theme (light or dark) for a more consistent appearance. |

### Quick Access flyout

The Quick Access flyout provides a shortcut to toggle PowerToys utilities on or off without opening the full Settings window.

| Setting | Description |
| :--- | :--- |
| Enable Quick Access | Enable or disable the Quick Access flyout. |
| Quick Access shortcut | Set the keyboard shortcut to open the Quick Access flyout. |

## Back up & restore

Set a location where you want to save your PowerToys settings. You can also restore your settings from an existing backup.

## Experimentation

Will activate experimentation with new features on Windows Insider builds, if available.

## Diagnostics & feedback

Here you can enable or disable the collection of diagnostic data, which helps the PowerToys team to improve the app. You can also generate a bug report package to send to the PowerToys team and view your diagnostic data.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
