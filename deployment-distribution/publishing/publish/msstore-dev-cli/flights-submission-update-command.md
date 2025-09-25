---
description: How to run the Microsoft Store Developer CLI (preview) flights submission update command.
title: Microsoft Store Developer CLI (preview) - Flights Submission Update Command
ms.date: 07/02/2024
ms.topic: article
ms.localizationpriority: medium
---

# Flights - Submission - Update Command

Updates the existing flight draft with the provided JSON.

## Usage

```console
msstore flights submission update <productId> <flightId> <product>
```

## Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |
| `product`   | The updated JSON product representation. |

## Options

| Option | Description |
|--------|-------------|
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |

## Help

```console
msstore flights submission update --help
```
