---
title: Windows Terminal Troubleshooting
description: Learn fixes to common obstacles in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Troubleshooting in Windows Terminal

Here are some common errors/obstacles you may encounter when using Windows Terminal.

## How do I set my tab title?

If you'd like to have the shell automatically set your tab title, [visit the set the tab title tutorial](./tutorials/tab-title.md). If you want to set your tab title in your settings file, use the following steps:

1. Add `"suppressApplicationTitle": true` to the profile you want to suppress any title change events that get sent from the shell. Having only this setting added to your profile will set your tab title to the name of your profile.

2. If you want a custom tab title that is not the name of your profile, you can add `"tabTitle": "TITLE"` where TITLE is replaced with your preferred tab title.
