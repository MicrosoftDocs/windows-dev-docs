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

You can use `wt.exe` to open a new instance of Windows Terminal from the command line. You can also use the execution alias `wt` instead.

> [!NOTE]
> If you built the Windows Terminal from the source code on [GitHub](https://github.com/microsoft/terminal), you can open that build using `wtd.exe` or `wtd`.

![Windows Terminal command line argument for split panes](./images/terminal-command-args.gif)

## Command line syntax

The `wt` command line accepts two types of values: **options** and **commands**. **Options** are a list of flags and other parameters that can control the behavior of the `wt` command line as a whole. **Commands** provide the action, or list of actions separated by semicolons, that should be implemented. If no command is specified, then the command is assumed to be `new-tab` by default.

```bash
wt [options] [command ; ]
```

To display a help message listing the available command line arguments, enter: `wt -h`, `wt --help`, or `wt -?`.

## Command line argument examples

Commands may vary slightly depending on which command line you're using.

### Open a new profile instance

To open a new Terminal instance, in this case the command will open the profile named "Ubuntu-18.04", enter:

```bash
# Command Prompt
wt -p "Ubuntu-18.04"

# PowerShell
wt -p "Ubuntu-18.04"

# Linux distro
cmd.exe /c "wt.exe" -p "Ubuntu-18.04"

# Execution aliases do not work in WSL distributions. If you want to use wt.exe from a WSL command line, you can spawn it from CMD directly by running `cmd.exe`. The `/c` option tells CMD to terminate after running.
```

 The `-p` flag is used to specify the Windows Terminal profile that should be opened. Substitute "Ubuntu-18.04" with the name of any Terminal profile that you have installed. This will always open a new window. Windows Terminal is not yet capable of opening new tabs or panes to an existing instance.

### Target a directory

To specify the folder that should be used as the starting directory for the console, in this case the d:\ directory, enter:

```bash
# Command Prompt
wt -d d:\

# PowerShell
wt -d d:\

# Linux distro
cmd.exe /c "wt.exe" -d d:\

# Execution aliases do not work in WSL distributions. If you want to use wt.exe from a WSL command line, you can spawn it from CMD directly by running `cmd.exe`. The `/c` option tells CMD to terminate after running.
```

### Multiple tabs

To open a new Terminal instance with multiple tabs, enter:

```bash
# Command Prompt
wt ; ; ;

# PowerShell
wt `; `; `;

# PowerShell uses a semicolon ; to delimit statements. To interpret a semicolon ; as a command delimiter for wt command line arguments, you need to escape semicolon characters using backticks `. PowerShell also has the stop parsing operator (--%), which instructs it to stop interpreting anything after it and just pass it on verbatim.

# Linux distro
cmd.exe /c "wt.exe" \; \; \;

# Execution aliases do not work in WSL distributions. If you want to use wt.exe from a WSL command line, you can spawn it from CMD directly by running `cmd.exe`. The `/c` option tells CMD to terminate after running.
```

To open a new Terminal instance with multiple tabs, in this case a Command Prompt profile and a PowerShell profile, enter:

```bash
# Command Prompt
wt -p "Command Prompt" ; new-tab -p "Windows PowerShell"

# PowerShell
wt -p "Command Prompt" `; new-tab -p "Windows PowerShell"

# Linux distro
cmd.exe /c "wt.exe" -p "Command Prompt" \; new-tab -p "Windows Powershell"

# Execution aliases do not work in WSL distributions. If you want to use wt.exe from a WSL command line, you can spawn it from CMD directly by running `cmd.exe`. The `/c` option tells CMD to terminate after running.
```

### Multiple panes

To open a new Terminal instance with one tab containing two panes running a Command Prompt profile and a PowerShell profile, enter:

```bash
# Command Prompt
wt -p "Command Prompt" ; split-pane -p "Windows PowerShell"

# PowerShell
wt -p "Command Prompt" `; split-pane -p "Windows PowerShell"

# PowerShell uses a semicolon ; to delimit statements. To interpret a semicolon ; as a command delimiter for wt command line arguments, you need to escape semicolon characters using backticks `. PowerShell also has the stop parsing operator (--%), which instructs it to stop interpreting anything after it and just pass it on verbatim.
    
# Linux distro
cmd.exe /c "wt.exe" -p "Command Prompt" \; split-pane -p "Windows PowerShell"

# Execution aliases do not work in WSL distributions. If you want to use wt.exe from a WSL command line, you can spawn it from CMD directly by running `cmd.exe`. The `/c` option tells CMD to terminate after running and the `\;` forward-slash + semicolon separates commands.
```

To open a new Terminal instance with one tab containing three panes running a Command Prompt profile, a PowerShell profile, and your default profile running a WSL command line, enter:

```bash
# Command Prompt
wt -p "Command Prompt" ; split-pane -p "Windows PowerShell" ; split-pane -H wsl.exe

# PowerShell
wt -p "Command Prompt" `; split-pane -p "Windows PowerShell" `; split-pane -H wsl.exe

# PowerShell uses a semicolon ; to delimit statements. To interpret a semicolon ; as a command delimiter for wt command line arguments, you need to escape semicolon characters using backticks `. PowerShell also has the stop parsing operator (--%), which instructs it to stop interpreting anything after it and just pass it on verbatim.

# Linux distro
cmd.exe /c "wt.exe" -p "Command Prompt" \; split-pane -p "Windows PowerShell" \; split-pane -H wsl.exe

# Execution aliases do not work in WSL distributions. If you want to use wt.exe from a WSL command line, you can spawn it from CMD directly by running `cmd.exe`. The `/c` option tells CMD to terminate after running and the `\;` forward-slash + semicolon separates commands.
```

The `-H` flag (or `--horizontal`) indicates that you would like the panes to be split horizontally. The `-V` flag (or `--vertical`) indicates that you would like the panes split vertically.

### Tab focus

To open a new Terminal instance with a specific tab in focus, use the `-t` flag (or `--target`), along with the tab-index number. To open your default profile in the first tab and the "Ubuntu-18.04" profile focused in the second tab (`-t 1`), enter:

```bash
# Command Prompt
wt ; new-tab -p "Ubuntu-18.04" ; focus-tab -t 1

# PowerShell
wt `; new-tab -p "Ubuntu-18.04" `; focus-tab -t 1

# Linux distro
cmd.exe /c "wt.exe" \; new-tab -p "Ubuntu-18.04" \; focus-tab -t 1

# Execution aliases do not work in WSL distributions. If you want to use wt.exe from a WSL command line, you can spawn it from CMD directly by running `cmd.exe`. The `/c` option tells CMD to terminate after running and the `\;` forward-slash + semicolon separates commands.
```

## Examples of multiple commands from PowerShell

The Windows Terminal uses the semicolon character `;` as a delimiter for separating commands in the `wt` command line. Unfortunately, PowerShell also uses `;` as a command separator. To work around this, you can use the following tricks to run multiple `wt` commands from PowerShell. In all the following examples, a new Terminal window is created with three panes - one running Command Prompt, one with PowerShell, and the last one running WSL.

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
