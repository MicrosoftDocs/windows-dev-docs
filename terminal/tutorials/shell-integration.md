---
title: Shell integration in the Windows Terminal
description: In this tutorial, you learn how to configure your shell to enable shell integration features in the Windows Terminal
author: zadjii-msft
ms.author: migrie
ms.date: 05/23/2023
ms.topic: tutorial
#Customer intent: As a developer or IT admin, I want to enable shell integration
---

# Tutorial: Enable shell integration in the Windows Terminal

Starting in Terminal 1.15 Preview, the Windows Terminal has started experimentally supporting some "shell integration" features. These features make the command-line easier to use. In earlier releases, we enabled shell to tell the Terminal what the current working directory is. Now, we've added support for more sequences to allow your shell to semantically describe parts of the terminal output as a "prompt", a "command", or "output". The shell can also tell the terminal whether a command succeeded or failed.

This is a guide to some of the shell integration features we've rolled out as of Terminal v1.18. We're planning on building even more features on top of these in the future, so we'd love to get some additional feedback on how folks using them.

> **Note**:
>  Notably, "marks" are still experimental, and are **only enabled for [Preview](https://aka.ms/terminal-preview) builds of the Terminal**. The settings for these features may change in a future release.

## How does this work?

Shell integration works by having the shell (or any command line application) write special "escape sequences" to the Terminal. These escape sequences aren't printed to the Terminal - instead, they provide bits of metadata the terminal can use to know more about what's going on in the application. By sticking these sequences into your shell's prompt, you can have the shell continuously provide info to the terminal that only the shell knows.

For the following sequences:

* `OSC` is the string `"\x1b]"` - an escape character, followed by `]`
* `ST` is the "string terminator", and can be either `\x1b\` (an ESC character, followed by `\`) or `\x7` (the BEL character)
* Spaces are merely illustrative.
* Strings in `<>` are parameters that should be replaced by some other value.

The relevant supported shell integration sequences as of Terminal v1.18 are:

* `OSC 133 ; A ST` ("_FTCS_PROMPT_") - The start of a prompt.
* `OSC 133 ; B ST` ("_FTCS_COMMAND_START_") - The start of a commandline (READ: the end of the prompt).
* `OSC 133 ; C ST` ("_FTCS_COMMAND_EXECUTED_") - The start of the command output / the end of the commandline.
* `OSC 133 ; D ; <ExitCode> ST` ("_FTCS_COMMAND_FINISHED_") - the end of a command. `ExitCode` If `ExitCode` is provided, then the Terminal will treat `0` as "success" and anything else as an error. If omitted, the terminal will just leave the mark the default color.

## How to enable shell integration marks

Supporting these features requires cooperation between your shell and the Terminal. You'll need to both enable settings in the Terminal to use these new features, as well as modify your shell's prompt.

To enable these features in the Terminal, you'll want to add the following to your settings:

```jsonc
"profiles":
{
    "defaults":
    {
        // Marks in general
        "experimental.showMarksOnScrollbar": true,

        // Needed for both pwsh and CMD shell integration
        "experimental.autoMarkPrompts": true,

        // Add support for a right-click context menu
        // You can also just bind the `showContextMenu` action
        "experimental.rightClickContextMenu": true,
    },
}
"actions":
[
    { "keys": "ctrl+up",   "command": { "action": "scrollToMark", "direction": "previous" }, },
    { "keys": "ctrl+down", "command": { "action": "scrollToMark", "direction": "next" }, },

    // Add the ability to select a whole command (or its output)
    { "keys": "ctrl+shift+w", "command": { "action": "selectOutput", "direction": "prev" }, },
    { "keys": "ctrl+shift+s", "command": { "action": "selectOutput", "direction": "next" }, },

    { "keys": "ctrl+alt+shift+w", "command": { "action": "selectCommand", "direction": "prev" }, },
    { "keys": "ctrl+alt+shift+s", "command": { "action": "selectCommand", "direction": "next" }, },
]
```

How you enable these marks in your shell varies from shell to shell. Below are tutorials for CMD and PowerShell.

### PowerShell (`pwsh.exe`)

If you've never changed your PowerShell prompt before, you should check out [about_Prompts](/powershell/module/microsoft.powershell.core/about/about_prompts) first.

We'll need to edit your `prompt` to make sure we tell the Terminal about the CWD, and mark up the prompt with the appropriate marks. PowerShell also lets us include the error code from the previous command in the `133;D` sequence, which will let the terminal automatically colorize the mark based if the command succeeeded or failed.

Add the following to your [PowerShell profile](/powershell/module/microsoft.powershell.core/about/about_profiles):

```pwsh
$Global:__LastHistoryId = -1

function Global:__Terminal-Get-LastExitCode {
  if ($? -eq $True) {
    return 0
  }
  $LastHistoryEntry = $(Get-History -Count 1)
  $IsPowerShellError = $Error[0].InvocationInfo.HistoryId -eq $LastHistoryEntry.Id
  if ($IsPowerShellError) {
    return -1
  }
  return $LastExitCode
}

function prompt {

  # First, emit a mark for the _end_ of the previous command.

  $gle = $(__Terminal-Get-LastExitCode);
  $LastHistoryEntry = $(Get-History -Count 1)
  # Skip finishing the command if the first command has not yet started
  if ($Global:__LastHistoryId -ne -1) {
    if ($LastHistoryEntry.Id -eq $Global:__LastHistoryId) {
      # Don't provide a command line or exit code if there was no history entry (eg. ctrl+c, enter on no command)
      $out += "`e]133;D`a"
    } else {
      $out += "`e]133;D;$gle`a"
    }
  }


  $loc = $($executionContext.SessionState.Path.CurrentLocation);

  # Prompt started
  $out += "`e]133;A$([char]07)";

  # CWD
  $out += "`e]9;9;`"$loc`"$([char]07)";

  # (your prompt here)
  $out += "PWSH $loc$('>' * ($nestedPromptLevel + 1)) ";

  # Prompt ended, Command started
  $out += "`e]133;B$([char]07)";

  $Global:__LastHistoryId = $LastHistoryEntry.Id

  return $out
}
```

### Command Prompt

Command Prompt sources it's prompt from the `PROMPT` environment variable. CMD.exe reads `$e` as the `ESC` character.  Unfortunately, CMD.exe doesn't have a way to get the return code of the previous command in the prompt, so we're not able to provide success / error information in CMD prompts.

You can change the prompt for the current CMD.exe instance by running:

```cmd
PROMPT $e]133;D$e\$e]133;A$e\$e]9;9;$P$e\$P$G$e]133;B$e\
```

Or, you can set the variable from the commandline for all future sessions:

```cmd
setx PROMPT $e]133;D$e\$e]133;A$e\$e]9;9;$P$e\$P$G$e]133;B$e\
```

These examples assume your current `PROMPT` is just `$P$G`. You can instead choose to wrap your current prompt with something like:

```cmd
PROMPT $e]133;D$e\$e]133;A$e\$e]9;9;$P$e\%PROMPT%$e]133;B$e\
```

> **Note**:
> Don't see your favorite shell here? If you figure it out, feel free to [contribute a solution for your preferred shell!](https://github.com/MicrosoftDocs/terminal/compare)

## Shell integration demos

### Open new tabs in the same working directory
![Open new tabs in the same working directory](../images/duplicate-tab-same-cwd.gif)

### Show marks for each command in the scrollbar
![Show marks for each command in the scrollbar](https://user-images.githubusercontent.com/18356694/164290075-a9f0b92c-3dde-4ce3-88cf-da5e451fe46c.gif)

### Automatically jump between commands

![Automatically jump between commands](https://user-images.githubusercontent.com/18356694/164290677-ffaafe09-81c4-4181-a4b8-db79a8aed235.gif)

### Select the entire output of a command
![Select the entire output of a command](https://user-images.githubusercontent.com/18356694/207696859-a227abe2-ccd4-4b81-8a2c-8a22219cd0dd.gif)

![Select the command using the right-click context menu](https://user-images.githubusercontent.com/18356694/222840120-7a2493b2-2264-4e94-af2b-17bfacc90353.gif)
