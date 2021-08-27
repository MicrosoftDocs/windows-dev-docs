---
title: PowerToys FancyZones utility for Windows 10
description: A window manager utility for arranging and snapping windows into efficient layouts
ms.date: 05/28/2021
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, FancyZones, Fancy, Zone, Zones]
---

# FancyZones utility

FancyZones is a window manager utility for arranging and snapping windows into efficient layouts to improve the speed of your workflow and restore layouts quickly. FancyZones allows the user to define a set of window locations for a desktop that are drag targets for windows. When the user drags a window into a zone, the window is resized and repositioned to fill that zone.

![FancyZones screenshot](../images/pt-fancy-zones2.png)

## Getting started

### Enable

To get started using FancyZones, you need to enable the utility in PowerToys settings and then invoke the FancyZones editor UI.  

### Launch zones editor

Launch the zones editor using the button in the PowerToys Settings menu or by pressing <kbd>Win</kbd> + <kbd>`</kbd> (note that this shortcut can be changed in the settings dialog).  

![FancyZones Settings UI](../images/pt-fancyzones-settings.png) 

### Elevated permission admin apps

If you have applications that are elevated, run in administrator mode, read [PowerToys and running as administrator](administrator.md) for more information.

## Choose your layout (Layout Editor)

When first launched, the zones editor presents a list of layouts that can be adjusted by how many windows are on the monitor. Choosing a layout shows a preview of that layout on the monitor. The selected layout is applied automatically. Double-clicking the layout will apply it and automatically close the editor.

![FancyZones Picker screenshot](../images/pt-fancyzones-picker.png)

If multiple displays are in use, the editor will detect the available monitors and display them for the user to choose between. The chosen monitor will then be the target of the selected layout.

![FancyZones Picker Multiple Monitors](../images/pt-fancyzones-multimon.png)

### Space around zones

The **Show space around zones** check box enables you to determine what sort of border or margin will surround each FancyZone window. The **Space around zones** field enables you to set a custom value for the width of the margin. With the Zones Editor open, change the **Show space around zones** after changing the values to see the new value applied.

The **Distance to highlight adjacent zones** enables you to set a custom value for the amount of space between zones until they snap together, or before both are highlighted enabling them to merge together.

![FancyZones space around zones screenshot](../images/pt-fancyzones-spacearound.png)

### Creating a custom layout

The zones editor also supports creating and saving custom layouts. Select the <kbd>+ Create new layout</kbd> button at the bottom-right.
  
There are two ways to create custom zone layouts: **Grid** layout and **Canvas** layout. These can also be thought of as subtractive and additive models.

The subtractive **Grid** model starts with a three column grid and allows zones to be created by splitting and merging zones, resizing the gutter between zones as desired.

To merge two zones, select and hold the left mouse button and drag the mouse until a second zone is selected, then release the button and a popup menu will show up.

![FancyZones Table Editor Mode](../images/pt-fancyzones-grideditor.png)

The additive **Canvas** model starts with a blank layout and supports adding zones that can be dragged and resized similar to windows.

Canvas layout also has keyboard support for zone editing. Use the <kbd>arrow</kbd> keys (up, down, left, right) to move a zone by 10 pixels, or <kbd>Ctrl</kbd> + <kbd>arrow</kbd> to move a zone by 1 pixel. Use the <kbd>Shift</kbd> + <kbd>arrow</kbd> keys to resize a zone by 10 pixels (5 per edge), or <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>arrow</kbd> to resize a zone by 2 pixels (1 per edge). To switch between the editor and dialog, press the <kbd>Ctrl</kbd> + <kbd>Tab</kbd> keys.

![FancyZones Window Editor Mode](../images/pt-fancyzones-canvaseditor.png)

### Quickly changing between layouts

With a custom layout, this layout can be configured to a user-defined hotkey to quickly apply it to the desired desktop. The hotkey can be set by opening the custom layout's edit menu. Once set, the custom layout can be applied by pressing the <kbd>Win</kbd> + <kbd>Ctrl</kbd> + <kbd>Alt</kbd> + <kbd>[number]</kbd> binding. The layout can also be applied by pressing the hotkey when dragging a window.

In the demo below, we start with a default template applied to the screen and 2 custom layouts that we assign hotkeys for. We then use the <kbd>Win</kbd> + <kbd>Ctrl</kbd> + <kbd>Alt</kbd> + <kbd>[number]</kbd> binding to apply the first custom layout and bind a window to it. Finally, we apply the second custom layout while dragging a window and bind the window to it.

![FancyZones Quick-Swap Layouts](../images/pt-fancyzones-quickswap.gif) 

## Snapping a window to two or more zones

If two zones are adjacent, a window can be snapped to the sum of their area (rounded to the minimum rectangle that contains both). When the mouse cursor is near the common edge of two zones, both zones are activated simultaneously, allowing you to drop the window into both zones.

It's also possible to snap to any number of zones: first drag the window until one zone is activated, then press and hold the <kbd>Control</kbd> key while dragging the window to select multiple zones.

To snap a window to multiple zone using only the keyboard, first turn on the two options **Override Windows Snap hotkeys (Win+Arrow) to move between zones** and **Move windows based on their position**. After snapping a window to one zone, use <kbd>Win</kbd> + <kbd>Ctrl</kbd> + <kbd>Alt</kbd> + <kbd>arrows</kbd> to expand the window to multiple zones.

![Two Zones Activation screenshot](../images/pt-fancyzones-twozones.png)

## Shortcut Keys

| Shortcut      | Action |
| ----------- | ----------- |
| <kbd>⊞ Win</kbd> + <kbd>\`</kbd> | Launches the editor (this shortcut can be changed in the settings window) |
| <kbd>⊞ Win</kbd> + <kbd>left/right</kbd> | Move focused window between zones (only if **Override Windows Snap hotkeys** setting is turned on, in that case only the <kbd>⊞ Win</kbd> + <kbd>←</kbd> and <kbd>⊞ Win</kbd> + <kbd>→</kbd> are overridden, while the <kbd>⊞ Win</kbd> + <kbd>↑</kbd> and <kbd>⊞ Win</kbd> + <kbd>↓</kbd> keep working as usual) |

FancyZones doesn't override the Windows 10 <kbd>⊞ Win</kbd> + <kbd>Shift</kbd> + <kbd>arrow</kbd> to quickly move a window to an adjacent monitor.

## Settings

| Setting | Description |
| --------- | ------------- |
| Configure the zone editor hotkey | To change the default hotkey, click on the textbox will(it's not necessary to select or delete the text) and then press the desired key combination on your keyboard |
| Hold <kbd>Shift</kbd> key to activate zones while dragging | Toggles between auto-snap mode with the <kbd>Shift</kbd> key (disabling snapping during a drag) and manual snap mode where pressing the shift key during a drag enables snapping |
| Use a non-primary mouse button to toggle zone activation | Clicking a non-primary mouse button toggles the zones activation |
| Override Windows Snap hotkeys (<kbd>⊞ Win</kbd> + <kbd>arrow</kbd>) to move between zones | When this option is checked and FancyZones is running, it overrides two Windows Snap keys: <kbd>⊞ Win</kbd> + <kbd>left</kbd> and <kbd>⊞ Win</kbd> + <kbd>right</kbd> |
| Move windows based on their position | Allows to use <kbd>⊞ Win</kbd> + <kbd>arrows</kbd> to snap a window based on its position relatively to the zone layout |
| Move windows between zones across all monitors | When this option is unchecked, snapping with <kbd>⊞ Win</kbd> + <kbd>arrow</kbd> cycles the window through the zones on the current monitor. When is checked, it cycles the window through all the zones on all monitors |
| Keep windows in their zones when the screen resolution changes | FancyZones will resize and reposition windows into the zones they were previously in after a screen resolution change |
| During zone layout changes, windows assigned to a zone will match new size/positions | FancyZones will resize and position windows into the new zone layout by maintaining the previous zone number location of each window |
| Move newly created windows to the last known zone | Automatically move a newly opened window into the last zone location that application was in |
| Move newly created windows to the current active monitor [EXPERIMENTAL] | When this option is checked, and **Move newly created windows to the last known zone** is unchecked or the application doesn't have a last known zone, it keeps the application on the current active monitor |
| Restore the original size of windows when unsnapping | Unsnapping a window will restore its size as before it was snapped |
| Follow mouse cursor instead of focus when launching editor in a multi-monitor environment | When checked, the editor will launch on the monitor where the mouse cursor is. When unchecked, the editor will launch on the monitor where the current active window is |
| Show zones on all monitors while dragging a window | By default, FancyZones shows only the zones available on the current monitor. (This feature may have a performance impact when chekced) |
| Allow zones to span across monitors (all monitors must have the same DPI scaling) | Allows to treat all connected monitors as one large screen. To work correctly, it requires all monitors to have the same DPI scaling factor |
| Make dragged window transparent | When the zones are activated, the dragged window is made transparent to improve the zones visibility |
| Zone highlight color (default: #008CFF) | The color that a zone becomes when it is the active drop target during a window drag |
| Zone Inactive color (default: #F5FCFF) | The color that zones become when they are not an active drop during a window drag |
| Zone border color (default: #FFFFFF) | The color of the border of active and inactive zones |
| Zone opacity (%) (default: 50%) | The percentage of opacity of active and inactive zones |
| Exclude applications from snapping to zones | Add the application's name, or part of the name, one per line (e.g. adding `Notepad` will match both `Notepad.exe` and `Notepad++.exe`; to match only `Notepad.exe` add the `.exe` extension) |

![FancyZones Settings bottom screenshot](../images/pt-fancyzones-settings2.png)
