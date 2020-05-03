---
title: Windows Terminal Command Line Arguments
description: Learn how to create command line arguments for Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: how-to
ms.service: terminal
---

# Create command line arguments for Windows Terminal

You can use `wt.exe` to open a new instance of Windows Terminal from the command line. You can also use the execution alias `wt` instead. If you built the Windows Terminal from the source code on [GitHub](https://github.com/microsoft/terminal), you can open that build using `wtd.exe` or `wtd`. This will always open a new window. Windows Terminal is not yet capable of opening new tabs or panes to an existing instance.

![Windows Terminal command line argument for split panes](./images/terminal-command-args.gif)

## Command line syntax

The `wt` command line accepts two types of values: options and commands:

```bash
wt [options] [command ; ]
```

Options are a list of flags and other parameters that can control the behavior of the `wt` command line as a whole. Commands are a semicolon-delimited list of commands and arguments for those commands.

If no command is specified, then the command is assumed to be `new-tab` by default. This allows for the following interpretations:

| Command | Interpretation |
| ------- | -------------- |
| `wt cmd.exe` | `wt new-tab cmd.exe` |
| `wt ; ; ;` | `wt new-tab ; new-tab ; new-tab ; new-tab` |

## Command line options

| Option | Description |
| ------ | ----------- |
| `--help`, `-h`, `-?`, `/?` | Displays the help message. |

## Command line commands

### Create a new tab

This opens a new tab with the given customizations. On its first invocation, this also opens a new window. Subsequent `new-tab` commands will all open new tabs in the same window.

```bash
new-tab [parameters]
```

#### Parameters

| Parameter | Description |
| --------- | ----------- |
[!INCLUDE [terminal-parameters](./terminal-parameters.md)]

<br />

___

### Open a new pane

This opens a new pane with the given customizations in the currently focused tab by splitting the given pane vertically or horizontally.

```bash
split-pane [-H, --horizontal]|[-V, --vertical] [parameters]
```

#### Parameters

| Parameter | Description |
| --------- | ----------- |
| `-H,--horizontal`, `-V,--vertical` | Used to indicate which direction to split the pane. `-V` is "vertically" (think `[|]`), and `-H` is "horizontally" (think `[-]`). If omitted, defaults to "auto", which splits the current pane in whatever the larger dimension is. If both `-H` and `-V` are provided, defaults to vertical. |
[!INCLUDE [terminal-parameters](./terminal-parameters.md)]

<br />

___

### Focus on a tab

Set the focus on a given tab.

```bash
focus-tab [--target, -t tab-index]
```

#### Parameters

| Parameter | Description |
| --------- | ----------- |
| `--target, -t tab-index` | Moves focus to the tab at index `tab-index`. If omitted, defaults to 0 (the first tab). |

## Running multiple commands from PowerShell

The Windows Terminal uses the semicolon character `;` as a delimiter for separating commands in the `wt` command line. Unfortunately, PowerShell also uses `;` as a command separator. To work around this, you can use the following tricks to help run multiple `wt` commands from PowerShell. In all the following examples, a new Terminal window is created with three panes - one running Command Prompt, one with PowerShell, and the last one running WSL.

The following examples use the `Start-Process` command to run `wt`. For more information on why the Terminal uses `Start-Process`, see [Using start](#using-start) below.

### Single quoted parameters (if you aren't calculating anything):

In this example, the `wt` parameters are wrapped in single quotes (`'`). This syntax is useful if nothing is being calculated.

```PowerShell
start wt 'new-tab "cmd"; split-pane -p "Windows PowerShell" ; split-pane -H wsl.exe'
```

### Escaped quotes

When passing a value contained in a variable to the `wt` command line, use the following syntax:

```PowerShell
$ThirdPane = "wsl.exe"
start wt "new-tab cmd; split-pane -p `"Windows PowerShell`" ; split-pane -H $ThirdPane"
```

Note the usage of  `` ` `` to escape the double-quotes (`"`) around "Windows PowerShell" in the `-p` parameter to the `split-pane` parameter.

### Using `start`

All the above examples explicitly used `start` to launch the Terminal.

The following examples do not use `start` to run the command line. Instead, there are two other methods of escaping the command line:

* Only escaping the semicolons so that `PowerShell` will ignore them and pass them straight to `wt`.
* Using `--%`, so PowerShell will treat the rest of the command line as arguments to the application.

```PowerShell
wt new-tab "cmd" `; split-pane -p "Windows PowerShell" `; split-pane -H wsl.exe
```

```PowerShell
wt --% new-tab cmd ; split-pane -p "Windows PowerShell" ; split-pane -H wsl.exe
```

In both of these examples, the newly created Windows Terminal window will create the window by correctly parsing all the provided command line arguments.

However, these methods are _not_ recommended currently, as PowerShell will wait for the newly-created Terminal window to be closed before returning control to PowerShell. By default, PowerShell will always wait for Windows Store applications (like the Windows Terminal) to close before returning to the prompt. Note that this is different than the behavior of Command Prompt, which will return to the prompt immediately.

## Examples

| Command | Description |
| ------- | ----------- |
| `wt -d .` | Opens the Terminal with the default profile in the current working directory. |
| `wt -d . ; new-tab -d C:\ pwsh.exe` | Opens the Terminal with two tabs. The first is running the default profile starting in the current working directory. The second is using the default profile with pwsh.exe as the `commandline` (instead of the default profile's `commandline`) starting in the C:\ directory. |
| `wt -p "Windows PowerShell" -d . ; split-pane -V wsl.exe` | Opens the Terminal with two panes, split vertically. The top pane is running the profile with the name "Windows PowerShell" and the bottom pane is running the default profile using wsl.exe as the `commandline` (instead of the default profile's `commandline`). |
