---
title: Windows Terminal installation
description: In this quickstart, you will learn how to install and run Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: quickstart
ms.service: terminal
# Customer intent: As a developer or IT admin, I want to install and run Windows Terminal so that I can have an improved workflow.
---

# Install and set up Windows Terminal

## Installation

You can install the Windows Terminal from the [Microsoft Store](https://aka.ms/terminal).

If you don't have access to the Microsoft Store, the builds are published on the [GitHub releases page](https://github.com/microsoft/terminal/releases). If you install from GitHub, the Terminal will not automatically update with new versions.

## First run

After installation, when you open the Terminal, it will start with PowerShell as the default profile in the open tab.

![Windows Terminal first run](./images/first-run.png)

### Dynamic profiles

The Terminal will automatically create profiles for you if you have WSL distros or multiple versions of PowerShell installed. Learn more about dynamic profiles on the [Dynamic profiles page](./dynamic-profiles.md).

## Open a new tab

You can open a new tab of the default profile by pressing <kbd>ctrl+shift+t</kbd> or by clicking the plus button. If you want to open a different profile, you can click the arrow next to the plus button to open the dropdown menu. From there, you can select which profile to open.

## Open a new pane

You can run multiple shells side-by-side using panes. To open a pane, you can use <kbd>alt+shift+d</kbd>. This key binding will open a duplicate pane of your focused profile. Learn more about panes on the [Panes page](./panes.md).

## Configuration

If you want to customize the settings of your Terminal, you can click on the settings button in the dropdown menu. This will open the settings.json file in your default JSON text editor. The application that opens is defined in your Windows OS settings.

The Terminal supports customization of [global properties](./customize-settings/global-settings.md) that affect the whole application, [profile properties](./customize-settings/profile-settings.md) that affect the settings of each profile, and [key bindings](./customize-settings/key-bindings.md) that allow you to interact with the Terminal using your keyboard.

## Command line arguments

You can launch the Terminal in a specific configuration using command line arguments. These arguments let you open the Terminal with specific tabs and panes with custom profile settings. Learn more about command line arguments on the [Command line arguments page](./command-line-arguments.md).

## Troubleshooting

If you encounter any difficulties using the Terminal, reference the [Troubleshooting page](./troubleshooting.md). If you find any bugs or have a feature request, you can click the feedback link in the About menu of the Terminal to go to the [GitHub page](https://github.com/microsoft/terminal) where you can file a new issue.
