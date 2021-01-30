---
title: Windows Terminal Tab Title Setup
description: In this tutorial, you learn how to set the tab title in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: tutorial
#Customer intent: As a developer or IT admin, I want to customize my tab titles so that I can have a more organized Windows Terminal.
---

# Tutorial: Configure tab titles in Windows Terminal

By default, the tab title is set to the shell's title. If a tab is composed of multiple panes, the tab's title is set to that of the currently focused pane. If you want to customize what is set as the tab title, follow this tutorial.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Use the `tabTitle` setting
> * Set the shell's title
> * Using the `suppressApplicationTitle` setting

## Use the `tabTitle` setting

The `tabTitle` setting allows you to define the starting title for a new instance of a shell. If it is not set, the profile `name` is used instead. Each shell responds to this setting differently.

| Shell | Behavior |
| ----- | -------- |
| PowerShell | The title is set. |
| Command Prompt | The title is set. If a command is running, it is temporarily appended to the end of the title. |
| Ubuntu | The title is ignored, and instead set to `user@machine:path` |
| Debian | The title is set. |

> [!NOTE]
> Though Ubuntu and Debian both run bash, they have different behaviors. This is to show that different distributions may have different behaviors.

## Set the shell's title

A shell has full control over its own title. However, each shell sets its title differently.

| Shell | Command |
| ----- | ------- |
| PowerShell | `$Host.UI.RawUI.WindowTitle = "New Title"` |
| Command Prompt | `TITLE "New Title"` |
| bash* | `echo -ne "\033]0;New Title\a"` |

Note that some Linux distributions (e.g. Ubuntu) set their title automatically as you interact with the shell. If the above command doesn't work, run the following command:

```bash
PS1=$
PROMPT_COMMAND=
echo -ne "\033]0;New Title\a"
```

This will change the title to 'New Title', and also set the prompt to '$'.

## Use the `suppressApplicationTitle` setting

Since a shell has control over its title, it may choose to overwrite the tab title at any time. For example, the `posh-git` module for PowerShell adds information about your Git repository to the title.

Windows Terminal allows you to suppress changes to the title by setting `suppressApplicationTitle` to `true` in your profile. This makes new instances of the profile set your visible title to `tabTitle`. If `tabTitle` is not set, the visible title is set to the profile's `name`.

Note that this decouples the shell's title from the visible title presented on the tab. If you read the shell's variable where the title is set, it may differ from the tab's title.

## Resources

* [Setting the console title to be your current working directory](https://devblogs.microsoft.com/powershell/setting-the-console-title-to-be-your-current-working-directory/)
* [Change the title of a terminal on Ubuntu 16.04](https://www.zachpfeffer.com/single-post/Change-the-title-of-a-terminal-on-Ubuntu-1604)
