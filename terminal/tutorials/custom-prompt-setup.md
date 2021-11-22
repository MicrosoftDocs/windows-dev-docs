---
title: Windows Terminal Custom Prompt Setup
description: In this tutorial, you learn how to set up Oh My Posh and Terminal-Icons in Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 10/08/2021
ms.topic: tutorial
#Customer intent: As a developer or IT admin, I want to set up a customized command line experience using Oh My Posh, Terminal-Icons, and posh-git in my Windows Terminal.
---

# Tutorial: Set up a custom prompt in Windows Terminal using Oh My Posh, Terminal-Icons, and Posh Git

[Oh My Posh](https://ohmyposh.dev) provides theme capabilities for a customized command prompt experience providing Git status color-coding and prompts. [Terminal-Icons](https://github.com/devblackops/Terminal-Icons) adds file and folder icons when displaying items in the terminal.

![Windows Terminal Custom Prompt](./../images/custom-prompt.png)

In this tutorial, you learn how to:

> [!div class="checklist"]
>
> * Set up Oh My Posh in PowerShell
> * Set up Oh My Posh in Ubuntu/WSL
> * Set up Terminal-Icons in PowerShell
> * Add missing glyphs

## Install a Nerd Font

Oh My Posh and Terminal-Icons use glyphs in order to style the prompt. If your font does not include the appropriate glyphs, you may see several Unicode replacement characters '&#x25AF;' throughout your prompt. In order to see all of the glyphs in your terminal, you should install a [Nerd Font](https://nerdfonts.com). (If you'd like a font that looks like Cascadia Code, the Caskaydia Cove Nerd Font was built from the Cascadia Code repository by a community member.) 

After downloading, you will need to unzip and install the font on your system. ([How to add a new font to Windows](https://support.microsoft.com/en-us/office/add-a-font-b7c5f17c-4426-4b53-967f-455339c564c1)).

### Install Oh My Posh

Oh My Posh enables you to use a full color set to define and render your terminal prompt, including the ability to use built-in themes or create your own custom theme.

### Install for PowerShell only

If you are only interested in using Oh My Posh with PowerShell, you can follow these installation instructions. If you want to use Oh My Posh for adding themes to both PowerShell and WSL command lines, skip down to the winget installation instructions below.

1. Using PowerShell, install Oh My Posh with the command:

    ```powershell
    Install-Module oh-my-posh -Scope CurrentUser
    ```

2. Browse the prompt themes, with the command:

    ```powershell
    Get-PoshThemes
    ```

3. Choose a theme and update your PowerShell profile with this command. (You can replace `notepad` with the text editor of your choice.)

    ```powershell
    notepad $PROFILE
    ```

4. Add the following to the end of your PowerShell profile file to set the `paradox` theme. (Replace `paradox` with the theme of your choice.)

    ```powershell
    Import-Module oh-my-posh
    Set-PoshPrompt -Theme paradox
    ```

Now, each new PowerShell instance will start by importing Oh My Posh and setting your command line theme.

> [!NOTE]
> This is not your Windows Terminal profile. Your PowerShell profile is a script that runs every time PowerShell starts. [Learn more about PowerShell profiles](/powershell/module/microsoft.powershell.core/about/about_profiles).

### Install for PowerShell or WSL using Winget

If you would like to use Oh My Posh to style both Windows and Windows Subsystem for Linux (WSL) command lines, we recommend using the [Windows Package Manager](/windows/package-manager). You can [install winget](https://github.com/microsoft/winget-cli#installing-the-client), the package manager client, using the App Installer from the Microsoft Store. (This may require an Insiders build if you're running Windows 10.)

1. Using PowerShell, install Oh My Posh using winget with the command:

    ```powershell
    winget install JanDeDobbeleer.OhMyPosh
    ```

    This will install:
    * oh-my-posh.exe - Windows executable, added to your $PATH.
    * oh-my-posh-wsl - Linux executable, added to your $PATH for use in the WSL.
    * themes - The latest Oh My Posh themes.

2. Using PowerShell, browse the prompt themes by entering:

    ```powershell
    Get-ChildItem -Path "~\AppData\Local\Programs\oh-my-posh\themes\*" -Include '*.omp.json' | Sort-Object Name | ForEach-Object -Process {
    $esc = [char]27
    Write-Host ""
    Write-Host "$esc[1m$($_.BaseName)$esc[0m"
    Write-Host ""
    oh-my-posh --config $($_.FullName) --pwd $PWD
    Write-Host ""
    }
    ```

    You can also browse the [prompt themes in the Oh My Posh docs](https://ohmyposh.dev/docs/themes).

3. Choose a theme and update your PowerShell `$PROFILE` file and WSL distribution `.bashrc` file.

    For PowerShell, open your profile file with `notepad $PROFILE` and add the following:

    ```powershell
    oh-my-posh --init --shell pwsh --config ~/AppData/Local/Programs/oh-my-posh/themes/jandedobbeleer.omp.json | Invoke-Expression
    ```

    Once added, reload your profile for the changes to take effect.

4. For your WSL distribution, open your profile file with `nano .bashrc` and add the following. (Replacing `paradox.omp.json` with the theme of your choice. You can view the list in the `.poshthemes` folder that was added to your distributions directory.)

    ```bash
    eval "$(oh-my-posh-wsl --init --shell bash --config ~/.poshthemes/paradox.omp.json)"
    ```

Learn more by visiting the [Oh My Posh documentation](https://ohmyposh.dev/docs/windows).

## Set your font in Windows Terminal settings

To set a Nerd Font for use with Oh My Posh and Terminal Icons, open the Windows Terminal settings UI by selecting **Settings** (Ctrl+,) from your Windows Terminal dropdown menu. Select the Windows PowerShell profile, and then the **Appearance** tab. In the **Font face** drop-down menu, select *CaskaydiaCove Nerd Font* or whichever Nerd font you would like to use with your customized prompt.

<!-- ![Windows Terminal Settings UI Font face menu](../../images/settings-powershell-font.png) -->

> [!NOTE]
> If you decide to use [Cascadia Code PL](https://github.com/microsoft/cascadia-code/releases) as your terminal font, you may consider using an Oh My Posh theme that contains the `minimal` function, indicating that additional icons aren't required.

## Set up posh-git in PowerShell

Posh-git adds a Git status summary to your Windows Terminal prompt with information and tab completion for Git commands, parameters, remotes and branch names.

1. Install posh-git using PowerShell with the command:

    ```powershell
    Install-Module posh-git -Scope CurrentUser
    ```

2. Update your PowerShell profile file: `notepad $PROFILE`. (You can replace nodepad with the text editor of your choice).

    In your PowerShell profile, add the following to the end of the file:

    ```powershell
    Import-Module posh-git
    ```

Your PowerShell command prompt will now display a status whenever you are inside of a Git directory. Learn more in the [posh-git repo on GitHub](https://github.com/dahlbyk/posh-git#using-posh-git).

## Additional resources

* [Oh my Posh documentation](https://ohmyposh.dev)
* [Terminal-Icons Repository](https://github.com/devblackops/Terminal-Icons)
