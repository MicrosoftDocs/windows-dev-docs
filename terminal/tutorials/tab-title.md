---
title: Windows Terminal Tab Title Setup
description: Learn how to set the tab title in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Tab Titles in Windows Terminal

By default, the tab title is set to the shell's title. If a tab is composed of multiple panes, the tab's title is set to that of the currently focused pane.

## Using the `tabTitle` setting

The `tabTitle` setting allows you to define the starting title for a new instance of a shell. If it is not set, the profile `name` is used instead. Each shell responds to this setting differently. 

| Shell | Behavior |
| --|--|
| Powershell | The title is set. |
| Command Prompt | The title is set. If a command is running, it is temporarily appended to the end of the title. |
| Ubuntu | The title is ignored, and instead set to `user@machine:path` |
| Debian | The title is set. |

NOTE: Though Ubuntu and Debian both run bash, they have different behaviors. This is to show that different distributions may have different behavior.


## Setting the shell's title

A shell has full control over its own title. However, each shell sets its title differently.

| Shell | Command |
|--|--|
| Powershell | `$Host.UI.RawUI.WindowTitle = "New Title"` |
| Command Prompt | `TITLE "New Title"` |
| bash* | `echo -ne "\033]0;New Title\a"` |

NOTE: Some linux distributions (i.e. Ubuntu) set their title automatically as you use interact with the shell. If the above command doesn't work, run the following command:
```bash
PS1=$ 
PROMPT_COMMAND=
echo -ne "\033]0;New Title\a"
```
This will change the title to 'New Title', and also set the prompt to '$'.

Source: [https://www.zachpfeffer.com/single-post/Change-the-title-of-a-terminal-on-Ubuntu-1604](https://www.zachpfeffer.com/single-post/Change-the-title-of-a-terminal-on-Ubuntu-1604)

## Using the `suppressApplicationTitle` setting

Since a shell has control over its title, it may choose to overwrite the tab title at any time. For example, the `posh-git` module for Powershell adds information about your git repository to the title.

Windows Terminal allows you to suppress changes to the title by setting `suppressApplicationTitle` to `true` in your profile. This makes new instances of the profile set your visible title to `tabTitle`. If `tabTitle` is not set, the visible title is set to the profile's `name`. 

Note: This decouples the shell's title from the visible title presented on the tab. If you read the shell's variable where the title is set, it may differ from the tab's title.