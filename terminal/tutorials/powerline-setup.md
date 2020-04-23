---
title: Windows Terminal Powerline Setup
description: Learn how to set up Powerline in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Powerline in Windows Terminal

## PowerShell instructions

### Install prerequisites

If you don't have Git for Windows, install it [here](https://git-scm.com/downloads).

Using PowerShell, install Posh-Git and Oh-My-Posh:

```powershell
Install-Module posh-git -Scope CurrentUser
Install-Module oh-my-posh -Scope CurrentUser
```

[Posh-Git](https://github.com/dahlbyk/posh-git) provides your prompt with Git status information and tab completion for Git commands, parameters, remotes, and branch names.
[Oh-My-Posh](https://github.com/JanDeDobbeleer/oh-my-posh) provides theming capabilities for your PowerShell prompt.

If you're on PowerShell Core, install PSReadline:

```powershell
Install-Module -Name PSReadLine -AllowPrerelease -Scope CurrentUser -Force -SkipPublisherCheck
```

[PSReadline](https://docs.microsoft.com/en-us/powershell/module/psreadline/?view=powershell-6) lets you customize the command-line editing environment in PowerShell.

### Customize your prompt

Now, open your PowerShell profile with `notepad $PROFILE` or the text editor of your choice. This is not your Windows Terminal profile. Your PowerShell profile is a script that runs every time PowerShell starts. To learn more about PowerShell profiles, click [here](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_profiles?view=powershell-7).

In your PowerShell profile, add the following to the end:

```powershell
Import-Module posh-git
Import-Module oh-my-posh
Set-Theme Paradox
```

Now, each new instance starts by importing Posh-Git and Oh-My-Posh, then setting the Paradox theme from Oh-My-Posh. Oh-My-Posh comes with several built-in themes that you can find [here](https://github.com/JanDeDobbeleer/oh-my-posh#themes).

## Ubuntu/WSL instructions

### Install prerequisites

Ubuntu has several Powerline options to install from. For the purposes of this tutorial, we'll install Go and Powerline-Go:

```bash
sudo apt install golang-go
go get -u github.com/justjanne/powerline-go
```

### Customize your prompt

Now, open your `~/.bashrc` file with `nano ~/.bashrc` or the text editor of your choice. This is a bash script that runs every time bash starts. Add the following, though beware that GOPATH may already exist:

```bash
GOPATH=$HOME/go
function _update_ps1() {
    PS1="$($GOPATH/bin/powerline-go -error $?)"
}
if [ "$TERM" != "linux" ] && [ -f "$GOPATH/bin/powerline-go" ]; then
    PROMPT_COMMAND="_update_ps1; $PROMPT_COMMAND"
fi
```

## Adding the missing Powerline glyphs

After installing Powerline, you may see several Unicode replacement characters '' throughout your prompt. This is because your font doesn't include Powerline glyphs by default. Though Cascadia Code does not include Powerline glyphs by default, you can install Cascadia Code PL, which has embedded Powerline symbols.

Go to the Cascadia Code releases page [here](https://github.com/microsoft/cascadia-code/releases), and install CascadiaPL.ttf. You'll also be able to find some other versions of Cascadia Code.

# Resources

- [Scott Hanselman's "How to make a pretty prompt in Windows Terminal"](https://www.hanselman.com/blog/HowToMakeAPrettyPromptInWindowsTerminalWithPowerlineNerdFontsCascadiaCodeWSLAndOhmyposh.aspx)
- [How to Change/Set up bash custom prompt (PS1) in Linux](https://www.cyberciti.biz/tips/howto-linux-unix-bash-shell-setup-prompt.html)
