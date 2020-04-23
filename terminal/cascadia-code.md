---
title: Windows Terminal Cascadia Code
description: Learn about the different Cascadia Code fonts and how they work with Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Cascadia Code

Cascadia Code is a new monospaced font shipped from Microsoft and provides a fresh experience for command line experiences and code editors. Cascadia Code was developed hand-in-hand with Windows Terminal. This font is most recommended to be used with terminal application and text editors such as Visual Studio and Visual Studio Code.

## Cascadia Code versions

There are multiple versions of Cascadia Code available:

| Font Name        | Includes Ligatures | Includes Powerline Glyphs |
|------------------|--------------------|---------------------------|
| Cascadia Code    | ✔                  | ❌                       |
| Cascadia Mono    | ❌                 | ❌                       |
| Cascadia Code PL | ✔                  | ✔                        |
| Cascadia Mono PL | ❌                 | ✔                        |

Programming ligatures are glyphs that are created by combining characters. They are must useful when writing code. The "Code" variants include ligatures, whereas the "Mono" variants exclude them. Below are a few of the ligatures included in Cascadia Code:
http://devblogs.microsoft.com/commandline/wp-content/uploads/sites/33/2019/09/programming-ligatures.gif

Powerline is a common command line extension that allows you to display additional information in your prompt. It uses a few additional glyphs to display this information properly. To learn more about setting up Powerline in your command prompt, visit the [Powerline in Windows Terminal](./tutorials/powerline-setup.md) page.

Cascadia Code and Cascadia Mono are installed side-by-side with Windows Terminal. Cascadia Mono is the default font for all of your profiles.

You can install any of these versions on the GitHub releases page [here](https://github.com/microsoft/cascadia-code/releases).

## Contributing to Cascadia Code

Cascadia Code is licensed under the [SIL Open Font license](https://scripts.sil.org/cms/scripts/page.php?site_id=nrsi&id=OFL) on [GitHub](https://github.com/microsoft/cascadia-code).
