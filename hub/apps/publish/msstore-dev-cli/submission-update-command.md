---
description: How to run the Microsoft Store Developer CLI (preview) submission update command.
title: Microsoft Store Developer CLI (preview) - Submission - Update Command
ms.date: 12/02/2022
ms.topic: article
ms.localizationpriority: medium
---

# Submission - Update Command

Updates the existing draft with the provided JSON.

## Usage

```console
msstore submission update <productId> <product>
```

## Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `product`   | The updated JSON product representation. |

## Options

| Option | Description |
|--------|-------------|
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |

## Help

```console
msstore submission update --help
```
