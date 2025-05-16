---
title: File Explorer Version Control Integration (PREVIEW)
description: Learn how to use File Explorer + version control integration inside Windows Advanced settings.
ms.reviewer: cinnamon
ms.topic: article
ms.date: 04/11/2025
---

# File Explorer version control integration (PREVIEW)

**File Explorer version control integration** provides version control information directly in File Explorer. This includes information such as the branch name, last commit author, last commit message, and more.

> [!NOTE]
> As of right now, File Explorer version control integration only supports Git. The Advanced Settings system component is extensible to allow for additional version control types.

![File Explorer version control integration](../images/fe-source.png)

## Prerequisites

File Explorer version control integration is available in the [Windows Beta channel](https://aka.ms/BetaLatest). See [Get started with the Windows Insider Program](/windows-insider/get-started) for more information about joining the program and selecting a channel. After you've joined the Beta channel, you can ([check for Windows updates](ms-settings:windowsupdate)).

## How to identify repositories

Windows has to know which folders are source code repositories so File Explorer can display the version control information. You can select your repository folders in Windows Advanced Settings > File Explorer settings under the File Explorer + version control header.

![File Explorer version control Settings](../images/fe-source-settings.png)
