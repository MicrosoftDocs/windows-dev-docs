---
title: Windows Terminal Troubleshooting
description: Learn fixes to common obstacles in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Troubleshooting in Windows Terminal

Here are some common errors/obstacles you may encounter when using Windows Terminal.

## How do I set my tab title?

If you'd like to have the shell automatically set your tab title, [visit the set the tab title tutorial](./tutorials/tab-title.md). If you want to set your tab title in your settings file, use the following steps:

1. Add `"suppressApplicationTitle": true` to the profile you want to suppress any title change events that get sent from the shell. Having only this setting added to your profile will set your tab title to the name of your profile.

2. If you want a custom tab title that is not the name of your profile, you can add `"tabTitle": "TITLE"` where TITLE is replaced with your preferred tab title.

## Command line arguments in PowerShell

Powershell uses a semicolon `;` to delimit statements. To interpret a semicolon `;` as a command delimiter for `wt` command line arguments, you need to escape semicolon characters using backticks `` ` ``. Powershell also has the stop parsing operator (`--%`), which instructs it to stop interpreting anything after it and just pass it on verbatim.

| Powershell Command | Description |
|--|--|
| `wt foo ; bar` | Open a tab running the command `foo1`. In original powershell instance, run `bar`. |
| ``wt foo `; bar`` | Open a total of 2 tabs. Each running the following command...<ul> <li>`foo` </li> <li>`bar`</li> </ul> |
| `wt --% foo ; bar ; baz` | Open a total of 3 tabs. Each running the following command...<ul> <li>`foo`</li> <li>`bar`</li> <li>`baz`</li> </ul> |

## Command line arguments in WSL

Execution aliases do not work in WSL. If you want to use `wt.exe` from a WSL setting, you can spawn it from CMD directly by running the following:

```sh
$ cmd.exe /c "wt.exe"
```

The `/c` option makes CMD run the command, then terminate.

## My profile's `startingDirectory` setting is being ignored

First, check to make sure your settings.json's syntax is correct. We automatically inject `"$schema": "https://aka.ms/terminal-profiles-schema"` at the top of your settings.json to help with that. Some apps, like Visual Studio Code, use the attached schema to validate your json as you make edits.

If your settings are correct, it may be that you are running a startup script that sets the starting directory of your terminal separately. For example, PowerShell has its own separate concept of profiles. If you are changing your starting directory there, that takes precedence over the one defined in Windows Terminal.

Alternatively, if you are running a script using the `commandline` profile setting, it may be that you are setting the location there. Similar to PowerShell profiles, your commands there take precedence over the `startingDirectory` profile setting.

In the end, `startingDirectory` launches a new terminal instance in the given directory. If the terminal runs any code that changes its directory, that may be a good place to take a look.
