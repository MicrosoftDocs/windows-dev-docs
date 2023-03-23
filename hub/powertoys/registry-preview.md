---
title: PowerToys Registry Preview utility for Windows
description: A quick little utility to visualize and edit complex Windows Registry files.
ms.date: 3/22/2023
ms.topic: article
no-loc: [PowerToys, Windows, Registry Preview, Win]
---

# Registry Preview utility

Registry Preview is Windows application that opens an existing REG file to create a visual preview of how this data will look, if it were imported into the Registry.

![Registry Preview screenshot](https://user-images.githubusercontent.com/18704482/227104250-c5101f74-32af-45ac-869a-579298e869b3.png)

## How to use Registry Preview

Once installed and enabled in PowerToys Settings, a right-click on any .REG file in File Explorer will now show a Preview option on the popup context menu. Selecting Preview will open the file in Registry Preview, showing this file's contents as plain text, Keys in a hierarchical tree, and Values in a grid-based format.  

With any .REG file open, by editing the displayed text file, the graphical representation of the data will update and reflect editing  in real time; changes made within the application can be saved to the existing or even to a new file.  .REG files opened in Registry Preview can also merged into the Registry by a single press of a toolbar.

The application can also be launched from the PowerToys launcher or settings page, opening to an "empty" environment and offering the ability to open an existing .REG file directly from with in the Registry Preview window.
