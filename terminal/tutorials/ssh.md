---
title: Windows Terminal SSH
description: In this tutorial, learn how to set up an SSH connection in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: tutorial
ms.service: terminal
#Customer intent: As a developer or IT admin, I want to set up am SSH connection in Windows Terminal so that I can connect to other servers.
---

# Tutorial: SSH in Windows Terminal

Windows 10 has a built-in SSH client that you can use in Windows Terminal.

In this tutorial, you'll learn how to set up a profile in Windows Terminal that uses SSH.

## Create a profile

You can start an SSH session in your command prompt by executing `ssh user@machine` and you will be prompted to enter your password. You can create a Windows Terminal profile that does this on startup by adding the `commandline` setting to a profile in your settings.json file.

```js
"commandline": "ssh cinnamon@roll"
```

## Resources

* [How to Enable and Use Windows 10’s New Built-in SSH Commands](https://www.howtogeek.com/336775/how-to-enable-and-use-windows-10s-built-in-ssh-commands/)
