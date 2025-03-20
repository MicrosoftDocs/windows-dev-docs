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

There is a "Sample Project" template included with the Command Palette. This can be used to quickly generate a project that creates a new extension. This will include the `.sln`, `.csproj`, and `.appxmanifest` files needed to create a new extension, as well as the plumbing to get it ready to be published. You will then open the project to the **MyCommandProvider** class and implement your commands.

## Pre-requisites

The following tools are required to build and publish your extension:

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (Community, Professional, or Enterprise edition)

## Microsoft Store

Command Palette extensions can be published to the Microsoft Store. The process is similar to publishing other apps or extensions. You create a new submission in the Partner Center and upload your `.appx` package. The Command Palette automatically discovers your extension when it's installed from the Microsoft Store.

## WinGet

As a part of the project template, there's a WinGet GitHub Actions workflow that allows you to publish your extension to the WinGet repository with the necessary tags to make it discoverable by the Command Palette. So, you don't need to understand the details of packaging. You add the extension to your GitHub repository and let the your GitHub Actions pipeline handle the publishing.

## Related content

- [Extensibility overview](creating-an-extension.md)
- [Extension samples](samples.md)
- [PowerToys Command Palette utility](overview.md)
