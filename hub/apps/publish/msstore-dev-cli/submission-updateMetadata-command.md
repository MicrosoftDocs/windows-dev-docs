---
description: How to run the Microsoft Store Developer CLI (preview) submission updateMetadata command.
title: Microsoft Store Developer CLI (preview) - Submission - UpdateMetadata Command
ms.date: 12/02/2022
ms.topic: article
ms.localizationpriority: medium
---

# Submission - UpdateMetadata Command

Updates the existing draft submission metadata with the provided JSON.

## Usage

```console
msstore submission updateMetadata <productId> <metadata>
```

## Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `metadata`  | The updated JSON metadata representation. |

## Options

| Option | Description |
|--------|-------------|
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |

## Help

```console
msstore submission updateMetadata --help
```
