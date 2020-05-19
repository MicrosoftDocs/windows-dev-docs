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

Cascadia Code is a new monospaced font from Microsoft that provides a fresh experience for command line applications and text editors. Cascadia Code was developed alongside Windows Terminal. This font is most recommended to be used with terminal applications and text editors such as Visual Studio and Visual Studio Code.

## Cascadia Code versions

There are multiple versions of Cascadia Code available that include ligatures and glyphs. All versions of Cascadia Code can be downloaded from the [Cascadia Code GitHub releases page](https://github.com/microsoft/cascadia-code/releases). Windows Terminal ships Cascadia Code and Cascadia Mono in its package and uses Cascadia Mono by default.

| Font Name | Includes Ligatures | Includes Powerline Glyphs |
| --------- | ------------------ | ------------------------- |
| Cascadia Code | Yes | No |
| Cascadia Mono | No  | No |
| Cascadia Code PL | Yes | Yes |
| Cascadia Mono PL | No | Yes |

## Powerline and programming ligatures

Powerline is a common command line plugin that allows you to display additional information in your prompt. It uses a few additional glyphs to display this information properly. To learn more about setting up Powerline in your command prompt, visit the [Powerline in Windows Terminal](./tutorials/powerline-setup.md) page.

Programming ligatures are glyphs that are created by combining characters. They are most useful when writing code. The "Code" variants include ligatures, whereas the "Mono" variants exclude them.

![Cascadia Code programming ligatures](./images/programming-ligatures.gif)

## Contributing to Cascadia Code

Cascadia Code is licensed under the [SIL Open Font license](https://scripts.sil.org/cms/scripts/page.php?site_id=nrsi&id=OFL) on [GitHub](https://github.com/microsoft/cascadia-code).
