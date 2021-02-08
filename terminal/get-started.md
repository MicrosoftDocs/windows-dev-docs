---
title: Windows Terminal installation
description: In this quickstart, you will learn how to install and set up Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 01/28/2021
ms.topic: quickstart
ms.localizationpriority: high
---

# Install and set up Windows Terminal

## Installation

You can install Windows Terminal from the [Microsoft Store](https://aka.ms/terminal).

If you don't have access to the Microsoft Store, the builds are published on the [GitHub releases page](https://github.com/microsoft/terminal/releases). If you install from GitHub, the terminal will not automatically update with new versions.

## First run

After installation, when you open the terminal, it will start with PowerShell as the default profile in the open tab.

![Windows Terminal first run](./images/first-run.png)

### Dynamic profiles

The terminal will automatically create profiles for you if you have WSL distros or multiple versions of PowerShell installed. Learn more about dynamic profiles on the [Dynamic profiles page](./dynamic-profiles.md).

## Open a new tab

You can open a new tab of the default profile by pressing <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>T</kbd> or by selecting the + (plus) button. To open a different profile, select the ˅ (arrow) next to the + button to open the dropdown menu. From there, you can select which profile to open.

## Invoke the command palette

You can invoke most features of Windows Terminal through the [command palette](./command-palette.md). The default key combination to invoke it is <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>P</kbd>.

![Example of the command palette in use](./images/command-palette-iterable-commands.gif)

## Open a new pane

You can run multiple shells side-by-side using panes. To open a pane, you can use <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>+</kbd> for a vertical pane or <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>-</kbd> for a horizontal one. You can also use <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>D</kbd> to open a duplicate pane of your focused profile. Learn more about panes on the [Panes page](./panes.md).

## Configuration

To customize the settings of your Windows Terminal, select **Settings** in the dropdown menu. This will open the `settings.json` file in your default text editor. (The default text editor is defined in your [Windows settings](ms-settings:defaultapps).)

The terminal supports customization of global properties that affect the whole application, [profile properties](./customize-settings/profile-general.md) that affect the settings of each profile, and [actions](./customize-settings/actions.md) that allow you to interact with the terminal using your keyboard or the command palette.

> [!TIP]
> You can also use the settings UI to configure your settings if you are using [Windows Terminal Preview](https://aka.ms/terminal-preview). You can learn how to open the settings UI on the [Actions page](./customize-settings/actions.md#application-level-commands).

## Command line arguments

You can launch the terminal in a specific configuration using command line arguments. These arguments let you open the terminal with specific tabs and panes with custom profile settings. Learn more about command line arguments on the [Command line arguments page](./command-line-arguments.md).

## Troubleshooting

If you encounter any difficulties using the terminal, reference the [Troubleshooting page](./troubleshooting.md). If you find any bugs or have a feature request, you can select the feedback link in the **About** menu of the terminal to go to the [GitHub page](https://github.com/microsoft/terminal) where you can file a new issue.
