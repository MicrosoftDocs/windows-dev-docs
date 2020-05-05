---
title: Windows Terminal Powerline Setup
description: In this tutorial, you learn how to set up Powerline in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: tutorial
ms.service: terminal
#Customer intent: As a developer or IT admin, I want to set up Powerline in my Windows Terminal so that I can have a customized command line experience.
---

# Tutorial: Set up Powerline in Windows Terminal

Powerline provides a customized command prompt experience.

![Windows Terminal Powerline PowerShell](./../images/powerline-powershell.png)

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Set up Powerline in PowerShell
> * Set up Powerline in Ubuntu/WSL
> * Add missing Powerline glyphs

## Prerequisites

### Install a Powerline font

Powerline uses glyphs in order to style the prompt. If your font does not include Powerline glyphs, you may see several Unicode replacement characters '' throughout your prompt. Though [Cascadia Mono](./../cascadia-code.md) does not include Powerline glyphs, you can install Cascadia Code PL or Cascadia Mono PL, which have the Powerline glyphs included. These fonts can be installed from the [Cascadia Code GitHub releases page](https://github.com/microsoft/cascadia-code/releases).

## Set up Powerline in PowerShell

### PowerShell prerequisites

If you don't have Git for Windows, install it [here](https://git-scm.com/downloads).

Using PowerShell, install Posh-Git and Oh-My-Posh:

```powershell
Install-Module posh-git -Scope CurrentUser
Install-Module oh-my-posh -Scope CurrentUser
```

[Posh-Git](https://github.com/dahlbyk/posh-git) adds Git status information to your prompt as well as tab completion for Git commands, parameters, remotes, and branch names. [Oh-My-Posh](https://github.com/JanDeDobbeleer/oh-my-posh) provides theming capabilities for your PowerShell prompt.

If you are using PowerShell Core, install PSReadline:

```powershell
Install-Module -Name PSReadLine -AllowPrerelease -Scope CurrentUser -Force -SkipPublisherCheck
```

[PSReadline](https://docs.microsoft.com/powershell/module/psreadline/?view=powershell-6) lets you customize the command line editing environment in PowerShell.

### Customize your prompt

Open your PowerShell profile with `notepad $PROFILE` or the text editor of your choice. This is not your Windows Terminal profile. Your PowerShell profile is a script that runs every time PowerShell starts. 

[Learn more about PowerShell profiles](https://docs.microsoft.com/powershell/module/microsoft.powershell.core/about/about_profiles?view=powershell-7).

In your PowerShell profile, add the following to the end of the file:

```powershell
Import-Module posh-git
Import-Module oh-my-posh
Set-Theme Paradox
```

Now, each new instance starts by importing Posh-Git and Oh-My-Posh, then setting the Paradox theme from Oh-My-Posh. Oh-My-Posh comes with several [built-in themes](https://github.com/JanDeDobbeleer/oh-my-posh#themes).

## Set up Powerline in Ubuntu/WSL

### Ubuntu/WSL prerequisites

Ubuntu has several Powerline options to install from. This tutorial will be using Go and Powerline-Go:

```bash
sudo apt install golang-go
go get -u github.com/justjanne/powerline-go
```

### Customize your prompt

Open your `~/.bashrc` file with `nano ~/.bashrc` or the text editor of your choice. This is a bash script that runs every time bash starts. Add the following, though beware that GOPATH may already exist:

```bash
GOPATH=$HOME/go
function _update_ps1() {
    PS1="$($GOPATH/bin/powerline-go -error $?)"
}
if [ "$TERM" != "linux" ] && [ -f "$GOPATH/bin/powerline-go" ]; then
    PROMPT_COMMAND="_update_ps1; $PROMPT_COMMAND"
fi
```

## Resources

- [Scott Hanselman's "How to make a pretty prompt in Windows Terminal"](https://www.hanselman.com/blog/HowToMakeAPrettyPromptInWindowsTerminalWithPowerlineNerdFontsCascadiaCodeWSLAndOhmyposh.aspx)
- [How to change/set up bash custom prompt (PS1) in Linux](https://www.cyberciti.biz/tips/howto-linux-unix-bash-shell-setup-prompt.html)
