---
title: Set up your development environment on Windows 10
description: A guide to help you setup your development environment on Windows and install your prefered tools and code languages. Whether you prefer using Python, NodeJS, VS Code, Git, Bash, Linux tools and commands, Android Studio, we've got your covered with great new tools like Windows Terminal and WSL.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
ms.technology: windows-nodejs
keywords: Set up windows, Dev Environment, Dev tools, development paths, Microsoft, Windows, Developer, Tips, Performance, WSL, terminal, nodejs, python
ms.localizationpriority: medium
ms.date: 07/24/2020
---

# Set up your development environment on Windows 10

This guide will help you get started with installing and setting up the languages and tools you need to develop on Windows or Windows Subsystem for Linux.

## Development paths

:::row:::
    :::column:::
       [![JavaScrip / NodeJS](../images/nodejs-logo.png)](../nodejs/index.yml)<br>
        **[Get started with NodeJS](../nodejs/index.yml)**<br>
        Install NodeJS and get your development environment setup on Windows or Windows Subsystem for Linux.
    :::column-end:::
    :::column:::
       [![Python](../images/python-logo.png)](../python/index.yml)<br>
        **[Get started with Python](../python/index.yml)**<br>
        Install Python and get your development environment setup on Windows or Windows Subsystem for Linux.
    :::column-end:::
    :::column:::
       [![Android](../images/android-logo.png)](/windows/android)<br>
        **[Get started with Android](/windows/android)**<br>
        Install Android Studio, or choose a cross-platform solution like Xamarin, React, or Cordova, and get your development environment setup on Windows.
    :::column-end:::
    :::column:::
       [![Windows Desktop](../images/windows-logo.png)](../apps/index.yml)<br>
        **[Get started with Windows Desktop](../apps/index.yml)**<br>
        Get started building desktop apps for Windows 10 using UWP, Win32, WPF, Windows Forms, or updating and deploying existing desktop apps with MSIX and XAML Islands.
    :::column-end:::
:::row-end:::

:::row:::
    :::column:::
       [![C / C++](../images/c-logo.png)](/cpp/)<br>
        **[Get started with C++ and C](/cpp/)**<br>
        Get started with C++, C, and assembly to develop apps, services, and tools.
    :::column-end:::
    :::column:::
       [![C#](../images/csharp-logo.png)](/dotnet/csharp/)<br>
        **[Get started with C#](/dotnet/csharp/)**<br>
        Get started building apps using C# and .NET Core.
    :::column-end:::
    :::column:::
       [![Azure for Java](../images/java-logo.png)](/azure/developer/java/)<br>
        **[Get started with Java on Azure](/azure/developer/java/)**<br>
        Get started building apps for the cloud with these tutorials and tools for Java developers.
    :::column-end:::
    :::column:::
       [![PowerShell](../images/powershell.png)](/powershell/)<br>
        **[Get started with PowerShell](/powershell/)**<br>
        Get started with cross-platform task automation and configuration management using PowerShell, a command-line shell and scripting language.
    :::column-end:::
:::row-end:::

## Tools and platforms

:::row:::
    :::column:::
       [![WSL](../images/windows-linux-dev-env.png)](/windows/wsl/)<br>
        **[Windows Subsystem for Linux](/windows/wsl/)**<br>
        Use your favorite Linux distribution fully integrated with Windows (no more need for dual-boot).<br>
        [Install WSL](/windows/wsl/install-win10)
    :::column-end:::
    :::column:::
       [![Windows Terminal](../images/terminal.png)](/windows/terminal/)<br>
        **[Windows Terminal](/windows/terminal/)**<br>
        Customize your terminal environment to work with multiple command line shells.
        <br>
        [Install Terminal](https://www.microsoft.com/p/windows-terminal/9n0dx20hk701?rtc=1&activetab=pivot:overviewtab)
    :::column-end:::
    :::column:::
       [![Windows Package Manager](../images/winget.png)](../package-manager/index.md)<br>
        **[Windows Package Manager](../package-manager/index.md)**<br>
        Use the winget.exe client, a comprehensive package manager, with your command line to install applications on Windows 10.<br>
        [Install Windows Package Manager (public preview)](../package-manager/winget/index.md#install-winget)
    :::column-end:::
    :::column:::
       [![PowerToys](../images/powertoys.png)](https://github.com/microsoft/PowerToys)<br>
        **[Windows PowerToys](https://github.com/microsoft/PowerToys)**<br>
        Tune and streamline your Windows experience for greater productivity with this set of power user utilities.<br>
        [Install PowerToys (public preview)](https://github.com/microsoft/PowerToys#installing-and-running-microsoft-powertoys)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
       [![VS Code](../images/Vscode.png)](https://code.visualstudio.com/docs)<br>
        **[VS Code](https://code.visualstudio.com/docs)**<br>
        A lightweight source code editor with built-in support for JavaScript, TypeScript, Node.js, a rich ecosystem of extensions (C++, C#, Java, Python, PHP, Go) and runtimes (such as .NET and Unity).<br>
        [Install VS Code](https://code.visualstudio.com/download)
    :::column-end:::
    :::column:::
       [![Visual Studio](../images/visualstudio.png)](/visualstudio/windows/)<br>
        **[Visual Studio](/visualstudio/windows/)**<br>
        An integrated development environment that you can use to edit, debug, build code, and publish apps, including compilers, intellisense code completion, and many more features.<br>
        [Install Visual Studio](/visualstudio/install/install-visual-studio)
    :::column-end:::
    :::column:::
       [![Azure](../images/Azure.png)](/azure/guides/developer/azure-developer-guide)<br>
        **[Azure](/azure/guides/developer/azure-developer-guide)**<br>
        A complete cloud platform to host your existing apps and streamline new development. Azure services integrate everything you need to develop, test, deploy, and manage your apps.<br>
        [Set up an Azure account](https://azure.microsoft.com/free/)
    :::column-end:::
    :::column:::
       [![.NET](../images/net.png)](https://dotnet.microsoft.com/)<br>
        **[.NET](/dotnet/standard/get-started/)**<br>
        An open source development platform with tools and libraries for building any type of app, including web, mobile, desktop, gaming, IoT, cloud, and microservices.<br>
        [Install .NET](https://dotnet.microsoft.com/download)
    :::column-end:::
:::row-end:::

<br>

## Run Windows and Linux

Windows Subsystem for Linux (WSL) allows developers to run a Linux operating system right alongside Windows. Both share the same hard drive (and can access each other’s files), the clipboard supports copy-and-paste between the two naturally, there's no need for dual-booting. WSL enables you to use BASH and will provide the kind of environment most familiar to Mac users.
- Learn more in the [WSL docs](/windows/wsl) or via [WSL videos on Channel 9](https://channel9.msdn.com/Search?term=wsl&lang-en=true).

> [!VIDEO https://channel9.msdn.com/Blogs/One-Dev-Minute/What-can-I-do-with-WSL--One-Dev-Question/player?format=ny]

You can also use Windows Terminal to open all of your favorite command line tools in the same window with multiple tabs, or in multiple panes, whether that's PowerShell, Windows Command Prompt, Ubuntu, Debian, Azure CLI, Oh-my-Zsh, Git Bash, or all of the above.

- Learn more in the [Windows Terminal docs](/windows/terminal) or via [WT videos on Channel 9](https://channel9.msdn.com/Search?term=windows%20terminal&lang-en=true).

> [!VIDEO https://channel9.msdn.com/Blogs/One-Dev-Minute/What-are-the-main-features-of-the-new-Terminal--One-Dev-Question/player?format=ny]

## Transitioning between Mac and Windows

Check out our [guide to transitioning between between a Mac and Windows](./mac-to-windows.md) (or Windows Subsystem for Linux) development environment. It can help you map the difference between:

* [Keyboard shortcuts](./mac-to-windows.md#keyboard-shortcuts)
* [Trackpad shortcuts](./mac-to-windows.md#trackpad-shortcuts)
* [Terminal and shell tools](./mac-to-windows.md#terminal-and-shell)
* [Apps and utilities](./mac-to-windows.md#apps-and-utilities)

![Office image](../images/flashy-office3.png)

## Additional resources

* [Tips for improving your workflow](./tips.md)
* [Stories from developers who have switched from Mac to Windows](./dev-stories.md)
* [Popular tutorials, courses, and code samples](./tutorials.md)
* [Microsoft's Game Stack documentation](/gaming/)