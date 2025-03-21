---
title: Command Palette Extension Publishing
description: The Command Palette provides a full extension model, allowing you to create custom experiences for the palette. Find info about how to publish an extension.
ms.date: 2/28/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to publish an extension for the Command Palette.
---

# Publishing your extension

The Command Palette provides a full extension model, allowing developers to create their own experiences for the palette. This document provides information about how to publish an extension.

There is a "Sample Project" template included with the Command Palette. This can be used to quickly generate a project that creates a new extension. This will include the `.sln`, `.csproj`, and `.appxmanifest` files needed to create a new extension, as well as the plumbing to get it ready to be published. You will then open the project to the `{ExtensionName}CommandsProvider` class (where `{ExtensionName}` is replaced with the name of your extension project) and implement your commands.

## Pre-requisites

The following tools are required to build and publish your extension:

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (Community, Professional, or Enterprise edition)

## WinGet

Publishing packages to WinGet is the recommended way to share your extensions with users. Extension packages which are listed on WinGet can be discovered and installed directly from the Command Palette.

For the most part, following the steps on [Submit packages to Windows Package Manager](https://learn.microsoft.com/en-us/windows/package-manager/package/) will get your extension onto WinGet itself.

Before submitting your manifest to WinGet, you'll need to check two things:

### Add `windows-commandpalette-extension` tag

### Ensure WindowsAppSdk is listed as a dependency

If you're using Windows App SDK, then you'll need to make sure that it is listed as a dependency of your package

If you're not using the template project, then this may not apply to you. 

<!-- 

Some day soon:

As a part of the project template, there's a WinGet GitHub Actions workflow that allows you to publish your extension to the WinGet repository with the necessary tags to make it discoverable by the Command Palette. So, you don't need to understand the details of packaging. You add the extension to your GitHub repository and let the your GitHub Actions pipeline handle the publishing. 

-->

## Microsoft Store

Command Palette extensions can be published to the Microsoft Store. The process is similar to publishing other apps or extensions. You create a new submission in the Partner Center and upload your `.msix` package. The Command Palette automatically discovers your extension when it's installed from the Microsoft Store.

Command Palette cannot, however, search for & install extensions that are only listed in the store. You can find those by running the following command:

```cmd
ms-windows-store://assoc/?Tags=AppExtension-com.microsoft.commandpalette
```

You can run this from the "Run commands" command in Command Palette, or from the command-line, or from the Run dialog. 

## Related content

- [Extensibility overview](extensibility-overview.md)
- [Extension samples](samples.md)
- [PowerToys Command Palette utility](overview.md)
