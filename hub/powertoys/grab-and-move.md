---
title: PowerToys Grab And Move utility for Windows
description: Learn how to use PowerToys Grab And Move to move and resize windows by holding a modifier key and dragging anywhere inside the window.
ms.date: 04/26/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Grab And Move, Win, Alt]
# Customer intent: As a Windows power user, I want to learn about the Grab And Move utility in PowerToys so I can move and resize windows quickly without aiming for the title bar or window edges.
---

# Grab And Move utility

PowerToys Grab And Move lets you move and resize windows by holding a modifier key (<kbd>Alt</kbd> or <kbd>⊞ Win</kbd>) and dragging anywhere inside the window. Left-click drag moves the window, right-click drag resizes it from the nearest edge or corner—no need to aim for the title bar or thin window borders.

:::image type="content" source="images/grab-and-move/GrabAndMove.gif" alt-text="Animated GIF of the Grab And Move utility moving a window with a modifier key and mouse drag, then resizing from the nearest edge.":::

## Move a window

While Grab And Move is enabled, hold the activation modifier key (<kbd>Alt</kbd> by default) and drag with the **left** mouse button anywhere inside the window. The window follows the cursor until you release the mouse button.

## Resize a window

Hold the activation modifier key and drag with the **right** mouse button. Grab And Move resizes the window from the edge or corner closest to where you started dragging, so you can resize from any side without having to aim for the window border.

> [!NOTE]
> Grab And Move may not work reliably with some touchpads.

## Settings

Grab And Move has the following settings:

| Setting | Description |
| :--- | :--- |
| **Activation modifier key** | Choose **Alt** or **Windows key** as the modifier you hold to activate move and resize gestures. |
| **Prevent Alt from activating the window menu after a drag or resize operation** | When **Alt** is the modifier key, suppresses the window menu that would normally appear when releasing the Alt key. |
| **Resize windows with the modifier key + right-click** | Hold the modifier key and drag with the right mouse button from the nearest edge or corner to resize the window. |
| **Do not activate when Game Mode is on** | Prevents the feature from being activated when actively playing a game on the system. |
| **Show window geometry** | Displays the window position and size on the overlay during drag and resize operations. |
| **Excluded apps** | Excludes an application from being moved or resized by Grab And Move. Add one application name per line. |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
