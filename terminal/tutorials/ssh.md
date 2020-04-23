---
title: Windows Terminal SSH
description: Learn how to set up an SSH connection in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# SSH in Windows Terminal

## Create a profile

Normally, you can set up an ssh session by executing `ssh user@machine` and you'll be prompted to enter your password. You can create a profile that does this on startup by adding the `commandline` setting like this:

```js
"commandline": "ssh cinnamon@roll"
```
