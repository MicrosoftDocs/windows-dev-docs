---
title: Opening a tab/pane in the same directory
description: In this tutorial, you learn how to configure your shell to allow Windows Terminal to open tabs in the same path.
author: zadjii-msft
ms.author: migrie
ms.date: 11/18/2021
ms.topic: tutorial
#Customer intent: As a developer or IT admin, I want to open tabs in the same working directory as my current tab.
---

# Tutorial: Opening a tab/pane in the same directory in Windows Terminal

Typically, the "new tab" and "split pane" actions will always open a new tab/pane in whatever the `startingDirectory` is for that profile. However, on other platforms, it's common for new tabs to automatically use the working directory of the current tab as the starting directory for a new tab. This allows the user to quickly multitask in a single directory. 

Unfortunately, on Windows, it's tricky to determine what the current working directory ("CWD") for a process is. Even if we were able to look it up, not all applications actually set their CWD as they navigate. Notably, Windows PowerShell doesn't change its CWD as you `cd` around the file system! Duplicating the CWD of PowerShell automatically would almost always be wrong.

Fortunately, there's a workaround. Applications can emit a special escape sequence to manually tell the Terminal what the CWD should be.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Configure the shell to tell the Terminal about its current working directory
> * Use the `duplicateTab` action to open a tab with the same CWD
> * Use the `splitPane` action to open a pane with the same CWD
> * Using the tab context menu to open tabs or panes with the same CWD

## Configure your shell

To tell the Terminal what the CWD is, you'll need to modify your shell to emit an escape sequence as you navigate the OS. Fortunately, most shells have a mechanism for configuring the "prompt", which is run after every command. This is the perfect place to add such output. 

### Windows

#### Command Prompt: `cmd.exe`

`cmd` uses the `%PROMPT%` environment variable to configure the prompt. You can easily prepend the prompt with the command to set the CWD with the following command:

```cmd
set PROMPT=$e]9;9;$P$e\%PROMPT%
```

This will append `$e]9;9;$P$e\` to your current prompt. When cmd evaluates this prompt, it'll replace 
* the `$e` with the escape character 
* the `$p` with the current working directory

Note that the above command will only work for the current `cmd.exe` session. To set the value permantently, AFTER running the above command, you'll want to run

```cmd
setx PROMPT %PROMPT%
```

#### PowerShell: `powershell.exe` or `pwsh.exe`

If you've never changed your PowerShell prompt before, you should check out [about_Prompts](/powershell/module/microsoft.powershell.core/about/about_prompts) first.

Add the following to your [PowerShell profile](/powershell/module/microsoft.powershell.core/about/about_profiles):

```powershell
function prompt {
  $loc = $($executionContext.SessionState.Path.CurrentLocation);
  $out = "PS $loc$('>' * ($nestedPromptLevel + 1)) ";
  $out += "$([char]27)]9;9;`"$loc`"$([char]27)\"
  return $out
}
```

#### PowerShell with posh-git

If you're using posh-git, then that will already modify your prompt. In that case, you'll want to only add the necessary output to the already modified prompt. The following example is a lightly modified version of this example from [the ConEmu docs](https://conemu.github.io/en/ShellWorkDir.html#PowerShellPoshGit):

```powershell
function prompt
{
  $loc = Get-Location

  $prompt = & $GitPromptScriptBlock

  $prompt += "$([char]27)]9;12$([char]7)"
  if ($loc.Provider.Name -eq "FileSystem")
  {
    $prompt += "$([char]27)]9;9;`"$($loc.Path)`"$([char]7)"
  }

  $prompt
}
```

### WSL

#### `bash`

Add the following line to the end of your `.bashrc` file:

```bash
PROMPT_COMMAND=${PROMPT_COMMAND:+"$PROMPT_COMMAND; "}'printf "\e]9;9;%s\e\\" "$(wslpath -w "$PWD")"'
```

The `PROMPT_COMMAND` variable in bash tells bash what command to run before displaying the prompt. The `printf` statement is what we're using to append the sequence for setting the working directory with the Terminal. The `$(wslpath -w "$PWD")` bit will invoke the `wslpath` executable to convert the current directory into its Windows-like path. The `${PROMPT_COMMAND:+"$PROMPT_COMMAND; "}` bit is [some bash magic](https://unix.stackexchange.com/a/466100) to make sure we append this command to any existing command (if you've already set `PROMPT_COMMAND` somewhere else.)


> [!NOTE]
> Don't see your favorite shell here? If you figure it out, feel free to open a PR to contribute a solution for your preferred shell!

## Using actions to duplicate the path

Once you've got the shell configured to tell the Terminal what the current directory is, opening a new tab or pane with that path is easy.

### Open a new tab with `duplicateTab`

To open a new tab with the same path (and profile) as the currently active terminal, use the "Duplicate Tab" action. This is bound by default to <kbd>ctrl+shift+d</kbd>, as follows:

```json
        { "command": "duplicateTab", "keys": "ctrl+shift+d" },
```

(see [`duplicateTab`](../customize-settings/actions.md#duplicate-tab)) for more details.

### Open a new pane with `splitPane`

To open a new pane with the same path (and profile) as the currently active terminal, use the "Duplicate Pane" action. This is **NOT** bound by default. The simplest form of this action is:

```json
        { "command": { "action": "splitPane", "splitMode": "duplicate" } },
```

(see [`splitPane`](../customize-settings/actions.md#split-a-pane)) for more details.

## Using the menu to duplicate the path

the above actions are also available on the tab context menu, under the entries "Duplicate Tab" and "Split Pane".

![Image duplicate-tab-same-cwd](../images/duplicate-tab-same-cwd.gif)
![Image split-pane-same-cwd](../images/split-pane-same-cwd.gif)
