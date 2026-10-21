---
title: PowerToys Command Palette Settings
description: Learn how to configure the PowerToys Command Palette settings, including activation key, appearance, and dock options.
ms.date: 10/14/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: Learn about the PowerToys Command Palette settings and how to configure them.
---

# PowerToys Command Palette settings

Use the **Settings** button in the Command Palette to open the settings page:

:::image type="content" source="../images/command-palette/cmdpal-settings.png" alt-text="A screenshot of the Command Palette interface with the Settings button highlighted in red.":::

The Command Palette settings page provides the following options.

## Search settings

Use the search box at the top of the settings window, or press <kbd>Ctrl</kbd>+<kbd>E</kbd>, to find settings, settings pages, extensions, commands, aliases, and assigned shortcuts. Search is fuzzy, so results still appear when you make a small typo. Select a result to go directly to it; Command Palette scrolls to and highlights the matching setting. To see every match, select **Show all results**.

## Copy links to settings

Right-click a settings page in the navigation pane, a section header, a setting, or an extension to copy a link to it. Depending on what you select, choose **Copy link to page**, **Copy link to setting**, **Copy link to extension settings**, or **Copy link to fallback order**. Settings links use the `x-cmdpal://settings/` format, for example `x-cmdpal://settings/appearance`. When you open a settings link, Command Palette settings opens, scrolls to the target, and highlights it. Settings links don't require confirmation.

## General

| Setting | Description |
| :--- | :--- |
| Activation shortcut | Define the keyboard shortcut to show or hide the Command Palette. |
| Use low-level keyboard hook | Use a low-level keyboard hook to detect the activation shortcut. This setting might fix shortcut issues, but the shortcut doesn't work while an app running as administrator has focus. |
| Ignore shortcut in fullscreen mode | When enabled, the Command Palette activation shortcut is ignored when an application is in fullscreen mode. |
| Ignore shortcut when the system heuristically detects fullscreen | Windows may detect that a fullscreen application is running or Presentation Settings are applied. Some applications (such as NVIDIA overlay) can trigger this incorrectly, preventing the shortcut from working. |
| Press the shortcut 3 times within 2 seconds to override suppression | Press the activation shortcut 3 times within 2 seconds to bypass the fullscreen or busy state guard. |
| Return home after closing | Configure how long Command Palette waits before automatically returning to the home page. Options include **Never**, immediately, or a set interval (10, 20, 30, 60, 90, 120, or 180 seconds). |
| Select search text when opened | Select the previous search text when you open Command Palette. |
| Keep search text when reopened | Retain the last search query when you reopen Command Palette. |
| Recent items: Show on Home | Show recently used commands on the Home page: **Don't show** (default) or **After pinned**. |
| Recent items: Maximum items | Set how many recent items to show (1–10). Applies to Home and the quick access shelf. Default: 5. |
| Recent items: History | Select **Clear history** to remove all recently used commands. |
| Open Command Palette on | Choose the monitor Command Palette opens on: **Monitor with mouse cursor**, **Primary monitor**, **Monitor with focused window**, **Don't move**, or **Last position**. |
| Escape key behavior | Configure how the <kbd>Escape</kbd> key behaves: clear search first then go back, always go back, always dismiss, or always hide. |
| Automatically expand app details | Automatically expand app details when an app appears as a result. |
| Go back with Backspace when search is empty | Press <kbd>Backspace</kbd> on an empty search box to return to the previous page. |
| Activate list items with a single click | Activate list items with a single click. When disabled, single-clicking selects the item and double-clicking activates it. |
| Alt+number shortcuts in lists | Choose whether <kbd>Alt</kbd>+<kbd>1</kbd> through <kbd>Alt</kbd>+<kbd>9</kbd> runs (**Run item**, default) or selects (**Select item**) the corresponding list item. <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>1</kbd> through <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>9</kbd> always selects the item without running it. |
| Show Command Palette in system tray | Show or hide the Command Palette icon in the system tray. |
| Quit Command Palette with Alt+F4 | Let <kbd>Alt</kbd>+<kbd>F4</kbd> quit Command Palette instead of leaving it running in the background. This setting is off by default. |
| Language | Choose the language of the Command Palette interface. By default, Command Palette uses your Windows display language. After you change the language, select **Restart now** to apply it. |
| Disable page transition animations | Disable page transition animations in the Command Palette interface. |
| Enable external command links | Allow `x-cmdpal` links to request commands and extension reloads. New requests require your confirmation. This setting is on by default. For more information, see [Command links](#command-links). |
| Always allowed command links | Review the command links you allowed with **Always allow**. Select **Revoke** to remove a single permission, or **Remove all** to require confirmation again for every link. |

## Personalization

| Setting | Description |
| :--- | :--- |
| App theme mode | Choose the Command Palette theme: **Light**, **Dark**, or **Use system settings**. |
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
| On-screen indicator position | Choose where Command Palette confirmation messages and other on-screen indicators appear: **Use system settings**, **Bottom center**, **Top left**, or **Top center**. |

## Layout and positioning

| Setting | Description |
| :--- | :--- |
| How Command Palette opens | Select a preview to choose how Command Palette opens: **Full palette** (default) or **Compact search box**. In compact mode, Command Palette opens as a compact search box and expands only when results or nested pages need more space. |
| Vertical search box position | Choose where the compact search box appears on the screen. Select **Choose position** to open a full-screen picker, then drag the preview, scroll, or press <kbd>Up Arrow</kbd> and <kbd>Down Arrow</kbd> to move it in small steps, or <kbd>Page Up</kbd> and <kbd>Page Down</kbd> to move it in larger steps. Press <kbd>Home</kbd> or <kbd>End</kbd> to move it to the highest or lowest position. Hatched areas are reserved for the taskbar and toolbars. Select **Save** to apply the position, or **Default** to restore the default position (75% from the bottom). Available only when Command Palette opens as a compact search box. |
| Quick access shelf | Show pinned and recent items below the compact search box. This setting is off by default and is available only when Command Palette opens as a compact search box. For more information, see [Quick access shelf](overview.md#quick-access-shelf). |
| Maximum pinned items | Set how many pinned commands appear on the quick access shelf (0–99). Use 0 to hide pinned items. Default: 9. |
| Recent items | Choose whether recently used commands appear on the quick access shelf: **Don't show** (default) or **After pinned**. Select **Manage recent items** to change how many recent items appear or to clear your history. |

## Command links

Command links let you start a Command Palette command from a link, a script, or another app. To copy a link, right-click a top-level command and select **Copy command link**. Command links use the format `x-cmdpal://commands/<provider-id>/<command-id>`.

The first time a link requests a command, a page, or an extension reload, Command Palette asks you to confirm the request. Because Command Palette can't identify which app or website opened the link, continue only if you expected the request. Choose one of the following options:

- **Run once**, **Open once**, or **Reload once**: Allow this request one time.
- **Always allow**: Allow this request now and in the future without asking again. You can revoke this permission later in **Always allowed command links**.
- **Cancel**: Ignore the request.

To block all command links, turn off **Enable external command links**.

## Use Command Palette with the Copilot key

You can choose Command Palette as the app that opens when you press the Copilot key on your keyboard. Select Command Palette in the Windows settings for the Copilot key. When Command Palette is running, pressing the Copilot key or <kbd>Win</kbd>+<kbd>C</kbd> again hides Command Palette, so you can use the same key to show and hide it.

## Dock

For a full description of Dock settings, including appearance and behavior options, see [Command Palette Dock](dock.md#dock-settings).

## Related content

- [Command Palette overview](overview.md)
- [Command Palette Dock](dock.md)
- [Extensibility overview](extensibility-overview.md)
